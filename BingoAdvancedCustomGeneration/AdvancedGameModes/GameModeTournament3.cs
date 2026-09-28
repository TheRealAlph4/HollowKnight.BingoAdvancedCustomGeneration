using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoAdvancedCustomGeneration.AdvancedGameModes
{
    public sealed class GameModeTournament3 : AdvancedGameMode
    {
        public override string DisplayName => "Tournament 3";
        public override bool CanBeRenamed => false;

        public override string SetName(string _)
        {
            return DisplayName;
        }

        private readonly Dictionary<string, AdvancedGoal> _goals = [];

        public GameModeTournament3() : base()
        {
            const bool lineExclusion = true;
            const bool fullExclusion = false;

            _goals.AddRange(GoalGroups["Vanilla"]);
            _goals.AddRange(GoalGroups["Extended"]);

            _goals.Remove("Slash Millibelle in Pleasure House");
            _goals.Remove("Open 6 geo chests (not in junk pit)");
            _goals.Remove("Decipher Hunter's Notes: Maskfly + Shrumeling");
            _goals.Remove("Collect 4 Simple Keys");

            _goals["Save the 2 grubs in Hive"].Unexclude(_goals["Mask Shard  in the Hive"]);
            _goals["Tram Pass + Visit all 5 Tram Stations"].Unexclude(_goals["Hive Knight"]);
            _goals["Tram Pass + Visit all 5 Tram Stations"].Unexclude(_goals["Hiveblood"]);
            _goals["Tram Pass + Visit all 5 Tram Stations"].Unexclude(_goals["Mask Shard  in the Hive"]);

            _goals["Shade Soul"].Exclude(_goals["Kill 2 Soul Warriors"], fullExclusion);
            _goals["Read Bretta's diary"].Exclude(_goals["Sprintmaster + Dashmaster"], lineExclusion);

            _goals["Kill Myla"].Exclude(_goals["Crystal Heart"], fullExclusion);

            _goals["Crystal Guardian 1"].Exclude(_goals["Kill Myla"], lineExclusion);
            _goals["Crystal Guardian 1"].Exclude(_goals["Crystal Heart"], lineExclusion);

            _goals["Lumafly Lantern"].Exclude(_goals["Kill Myla"], lineExclusion);
            _goals["Lumafly Lantern"].Exclude(_goals["Crystal Heart"], lineExclusion);
            _goals["Lumafly Lantern"].Exclude(_goals["Crystal Guardian 1"], lineExclusion);

            _goals["Descending Dark"].Exclude(_goals["Desolate Dive"], lineExclusion);
            _goals["Descending Dark"].Exclude(_goals["Soul Master"], lineExclusion);

            _goals["Slash Zote's corpse in Greenpath"].Exclude(_goals["Defeat Colosseum Zote"], fullExclusion);
            _goals["Slash Zote's corpse in Greenpath"].Exclude(_goals["Rescue Zote in Deepnest"], fullExclusion);
            _goals["Slash Zote's corpse in Greenpath"].Exclude(_goals["Vengefly King + Massive Moss Charger"], fullExclusion);

            _goals["Unlock Queen's Stag + King's Stag Stations"].Exclude(_goals["Have 1500 geo in the bank"], lineExclusion);

            _goals["Save the 2 grubs in Hive"].Exclude(_goals["Mask Shard  in the Hive"], lineExclusion);
            _goals["Save the 2 grubs in Hive"].Exclude(_goals["Hive Knight"], lineExclusion);
            _goals["Save the 2 grubs in Hive"].Exclude(_goals["Hiveblood"], lineExclusion);

            _goals["Unlock Deepnest Stag"].Exclude(_goals["Talk to Midwife"], fullExclusion);
            _goals["Unlock Deepnest Stag"].Exclude(_goals["Talk to Mask Maker"], lineExclusion);
            _goals["Unlock Deepnest Stag"].Exclude(_goals["Herrah"], lineExclusion);
        }

        public override List<string> GenerateBoard(int seed)
        {
            Random rng = new(seed);

            List<AdvancedGoal> board = [];
            for (int i = 0; i < 25; ++i)
            {
                board.Add(null);
            }

            for (int goalNr = 0; goalNr < 25; ++goalNr)
            {
                int slot = PickRandomSlot(rng, board);
                AdvancedGoal goal = PickRandomGoal(rng, board, slot);
                board[slot] = goal;
            }

            return [.. board.Select(advancedGoal => advancedGoal.Name)];
        }

        private int PickRandomSlot(Random rng, List<AdvancedGoal> board)
        {
            List<int> availableSlots = [];
            for (int slot = 0; slot < 25; ++slot)
            {
                if (board[slot] == null)
                {
                    availableSlots.Add(slot);
                }
            }
            int slotID = rng.Next(availableSlots.Count);
            return availableSlots[slotID];
        }

        private AdvancedGoal PickRandomGoal(Random rng, List<AdvancedGoal> board, int slot)
        {
            List<AdvancedGoal> potentialGoals = [];
            double totalWeight = 0d;

            foreach (AdvancedGoal potentialGoal in _goals.Values)
            {
                if (!board.Contains(potentialGoal) && !GoalSlotIsExcluded(board, slot, potentialGoal))
                {
                    potentialGoals.Add(potentialGoal);
                    totalWeight += potentialGoal.Weight;
                }
            }

            int goalId = -1;
            double randomWeight = rng.NextDouble() * totalWeight;
            while (randomWeight > 0d)
            {
                ++goalId;
                randomWeight -= potentialGoals[goalId].Weight;
            }

            return potentialGoals[goalId];
        }

        private bool GoalSlotIsExcluded(List<AdvancedGoal> board, int slot, AdvancedGoal goal)
        {
            foreach (AdvancedGoal existingGoal in board)
            {
                if (GoalsExclude(existingGoal, goal, false))
                {
                    return true;
                }
            }

            int column = slot % 5;
            int row = slot / 5;
            int rowStart = slot - column;
            bool tlbr = column == row;
            bool trbl = column == 4 - row;
            List<int> tlbrSlots = [0, 6, 12, 18, 24];
            List<int> trblSlots = [4, 8, 12, 16, 20];
            for (int i = 0; i < 5; ++i)
            {
                if (GoalsExclude(board[column + 5 * i], goal, true))
                {
                    return true;
                }
                if (GoalsExclude(board[rowStart + i], goal, true))
                {
                    return true;
                }
                if(tlbr && GoalsExclude(board[tlbrSlots[i]], goal, true))
                {
                    return true;
                }
                if (trbl && GoalsExclude(board[trblSlots[i]], goal, true))
                {
                    return true;
                }
            }
            return false;
        }

        private bool GoalsExclude(AdvancedGoal goal1, AdvancedGoal goal2, bool line)
        {
            if(goal1 == null || goal2 == null)
            {
                return false;
            }
            if (line)
            {
                return goal1.LineExclusions.Contains(goal2.Name) ||
                    goal2.LineExclusions.Contains(goal1.Name);
            }
            return goal1.FullExclusions.Contains(goal2.Name) ||
                goal2.FullExclusions.Contains(goal1.Name);
        }
    }
}
