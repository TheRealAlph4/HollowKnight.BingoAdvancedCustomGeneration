using BingoSync.CustomGoals;
using System.Collections.Generic;
using System.Linq;

namespace BingoAdvancedCustomGeneration
{
    internal static class GoalManager
    {
        private static Dictionary<string, Dictionary<string, AdvancedGoal>> goalGroups = [];

        public static void PreCopyGoalGroup(string groupName)
        {
            Dictionary<string, AdvancedGoal> advancedGoals = [];
            Dictionary<string, BingoGoal> basicGoals = BingoSync.Goals.GetGoalsByGroupName(groupName);
            foreach(BingoGoal basicGoal in basicGoals.Values)
            {
                advancedGoals.Add(basicGoal.name, new AdvancedGoal()
                {
                    Name = basicGoal.name,
                    FullExclusions = basicGoal.exclusions.Select(s => string.Copy(s)).ToList(),
                });
            }
            goalGroups.Add(groupName, advancedGoals);
        }

        public static void SetupCustomVanillaExclusions()
        {
            Dictionary<string, AdvancedGoal> advancedGoals = goalGroups["Vanilla"];
            bool lineExlusion = true;

            Exclude(advancedGoals, "Kill Myla", "Crystal Heart");

            Exclude(advancedGoals, "Lumafly Lantern", "Kill Myla", lineExlusion);
            Exclude(advancedGoals, "Lumafly Lantern", "Crystal Heart", lineExlusion);
            Exclude(advancedGoals, "Lumafly Lantern", "Crystal Guardian 1", lineExlusion);

            Exclude(advancedGoals, "Descending Dark", "Desolate Dive", lineExlusion);
            Exclude(advancedGoals, "Descending Dark", "Soul Master", lineExlusion);

            Exclude(advancedGoals, "Unlock Queen's Stag + King's Stag Stations", "Have 1500 geo in the bank", lineExlusion);
        }

        public static void Exclude(Dictionary<string, AdvancedGoal> goals, string goal1, string goal2, bool line = false)
        {
            if(line)
            {
                goals[goal1].LineExclusions.Add(goal2);
                goals[goal2].LineExclusions.Add(goal1);
            }
            else
            {
                goals[goal1].FullExclusions.Add(goal2);
                goals[goal2].FullExclusions.Add(goal1);
            }
        }
    }
}
