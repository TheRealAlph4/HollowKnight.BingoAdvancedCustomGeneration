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
                    FullExclusions = [.. basicGoal.exclusions.Select(s => string.Copy(s))],
                });
            }
            goalGroups.Add(groupName, advancedGoals);
        }

        public static void SetupCustomTournamentExclusions(Dictionary<string, AdvancedGoal> goals)
        {
            bool lineExclusion = true;
            bool fullExclusion = false;

            Exclude(goals, "Shade Soul", "Kill 2 Soul Warriors", fullExclusion);
            Exclude(goals, "Read Bretta's diary", "Sprintmaster + Dashmaster", lineExclusion);

            Exclude(goals, "Kill Myla", "Crystal Heart", fullExclusion);

            Exclude(goals, "Crystal Guardian 1", "Kill Myla", lineExclusion);
            Exclude(goals, "Crystal Guardian 1", "Crystal Heart", lineExclusion);

            Exclude(goals, "Lumafly Lantern", "Kill Myla", lineExclusion);
            Exclude(goals, "Lumafly Lantern", "Crystal Heart", lineExclusion);
            Exclude(goals, "Lumafly Lantern", "Crystal Guardian 1", lineExclusion);

            Exclude(goals, "Descending Dark", "Desolate Dive", lineExclusion);
            Exclude(goals, "Descending Dark", "Soul Master", lineExclusion);

            Exclude(goals, "Slash Zote's corpse in Greenpath", "Defeat Colosseum Zote", fullExclusion);
            Exclude(goals, "Slash Zote's corpse in Greenpath", "Rescue Zote in Deepnest", fullExclusion);
            Exclude(goals, "Slash Zote's corpse in Greenpath", "Vengefly King + Massive Moss Charger", fullExclusion);

            Exclude(goals, "Unlock Queen's Stag + King's Stag Stations", "Have 1500 geo in the bank", lineExclusion);

            Unexclude(goals, "Save the 2 grubs in Hive", "Mask Shard  in the Hive");
            Exclude(goals, "Save the 2 grubs in Hive", "Mask Shard  in the Hive", lineExclusion);
            Exclude(goals, "Save the 2 grubs in Hive", "Hive Knight", lineExclusion);
            Exclude(goals, "Save the 2 grubs in Hive", "Hiveblood", lineExclusion);

            Unexclude(goals, "Tram Pass + Visit all 5 Tram Stations", "Hive Knight");
            Unexclude(goals, "Tram Pass + Visit all 5 Tram Stations", "Hiveblood");
            Unexclude(goals, "Tram Pass + Visit all 5 Tram Stations", "Mask Shard  in the Hive");

            Exclude(goals, "Unlock Deepnest Stag", "Talk to Midwife", fullExclusion);
            Exclude(goals, "Unlock Deepnest Stag", "Talk to Mask Maker", lineExclusion);
            Exclude(goals, "Unlock Deepnest Stag", "Herrah", lineExclusion);
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

        public static void Unexclude(Dictionary<string, AdvancedGoal> goals, string goal1, string goal2)
        {
            goals[goal1].LineExclusions.Remove(goal2);
            goals[goal2].LineExclusions.Remove(goal1);
            goals[goal1].FullExclusions.Remove(goal2);
            goals[goal2].FullExclusions.Remove(goal1);
        }

        public static Dictionary<string, AdvancedGoal> GetGoalsByName(List<string> goalNames)
        {
            Dictionary<string, AdvancedGoal> goals = [];
            foreach(string goalName in goalNames)
            {
                foreach(Dictionary<string, AdvancedGoal> goalGroup in goalGroups.Values)
                {
                    if(goalGroup.ContainsKey(goalName))
                    {
                        goals[goalName] = goalGroup[goalName];
                    }
                }
            }
            return goals;
        }

        public static Dictionary<string, AdvancedGoal> GetGoalsByGroupName(string groupName)
        {
            return goalGroups[groupName];
        }
    }
}
