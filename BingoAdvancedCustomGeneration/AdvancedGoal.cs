using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoAdvancedCustomGeneration
{
    internal enum Tag
    {
        Dreamnail,
        Cdash,
        Earlygame,
        Middlegame,
        Lategame,
        Bossfight,
        Geo,
        Deepnest,
        Dive,
    }

    internal class AdvancedGoal
    {
        public string Name = string.Empty;
        public List<string> FullExclusions = [];
        public List<string> LineExclusions = [];
        public List<Tag> Tags = [];
    }
}
