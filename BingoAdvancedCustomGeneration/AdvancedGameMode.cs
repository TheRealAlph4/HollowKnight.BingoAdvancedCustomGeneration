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
            for(int i = 0; i < 25; ++i)
            {
                board.Add(null);
            }

            for(int goalNr = 0; goalNr < 25; ++goalNr)
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
                if(board[slot] == null)
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

            foreach(AdvancedGoal potentialGoal in goals.Values)
            {
            }


            AdvancedGoal goal = null;

            return goal;
        }

        private bool GoalSlotIsExcluded(List<AdvancedGoal> board, int slot, AdvancedGoal goal)
        {
            return false;
        }

        public static string Jsonify(List<AdvancedGoal> board)
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
