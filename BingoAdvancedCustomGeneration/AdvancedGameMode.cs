using BingoSync.CustomGoals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoAdvancedCustomGeneration
{
    internal class AdvancedGameMode : GameMode
    {
        public AdvancedGameMode(string name, Dictionary<string, BingoGoal> goals) : base(name, [])
        {
        }
    }
}
