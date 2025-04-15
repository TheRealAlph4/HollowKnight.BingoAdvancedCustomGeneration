using BingoSync.CustomGoals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoAdvancedCustomGeneration
{
    internal class AdvancedGameMode(string name, Dictionary<string, AdvancedGoal> goals) : GameMode(name, [])
    {
        public override string GenerateBoard(int seed)
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

            return Jsonify(board);
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

            foreach (AdvancedGoal potentialGoal in goals.Values)
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

        private static string Jsonify(List<AdvancedGoal> board)
        {
            string output = "[";
            for (int i = 0; i < board.Count; i++)
            {
                output += "{\"name\": \"" + board.ElementAt(i).Name + "\"}" + (i < 24 ? "," : "");
            }
            output += "]";
            return output;
        }
    }
}
