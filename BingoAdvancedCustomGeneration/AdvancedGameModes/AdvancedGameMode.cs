using BingoSync.CustomGoals;
using BingoSync.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BingoAdvancedCustomGeneration.AdvancedGameModes
{
    public abstract class AdvancedGameMode : IGameMode
    {
        public abstract bool CanBeRenamed { get; }
        public abstract string DisplayName { get; }

        public abstract List<string> GenerateBoard(int seed);
        public abstract string SetName(string newName);

        private static readonly Dictionary<string, Dictionary<string, AdvancedGoal>> _goalGroups = [];
        protected readonly Dictionary<string, Dictionary<string, AdvancedGoal>> GoalGroups = [];

        static AdvancedGameMode()
        {
            List<string> groupNames = ["Vanilla", "Item Rando", "Extended", "Extended+"];
            foreach (string groupName in groupNames)
            {
                PreCopyGoalGroup(groupName);
            }
            TagVanillaGoals();
            TagItemRandoGoals();
            TagExtendedGoals();
            TagExtendedPlusGoals();
        }

        public static void PreCopyGoalGroup(string groupName)
        {
            Dictionary<string, AdvancedGoal> advancedGoals = [];
            Dictionary<string, BingoGoal> basicGoals = Goals.GetGoalsByGroupName(groupName);
            foreach (BingoGoal basicGoal in basicGoals.Values)
            {
                advancedGoals.Add(basicGoal.Name, new AdvancedGoal()
                {
                    Name = basicGoal.Name,
                    FullExclusions = [.. basicGoal.Exclusions.Select(s => string.Copy(s))],
                });
            }
            _goalGroups.Add(groupName, advancedGoals);
        }

        private static void TagVanillaGoals()
        {
            // Dictionary<string, AdvancedGoal> goals = _goalGroups["Vanilla"];
        }

        private static void TagItemRandoGoals()
        {

        }

        private static void TagExtendedGoals()
        {

        }

        private static void TagExtendedPlusGoals()
        {

        }

        public AdvancedGameMode()
        {
            foreach (string groupName in _goalGroups.Keys)
            {
                GoalGroups[groupName] = [];
                foreach (string goalName in _goalGroups[groupName].Keys)
                {
                    GoalGroups[groupName][goalName] = _goalGroups[groupName][goalName].DeepCopy();
                }
            }
        }
    }
}
