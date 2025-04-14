using System.Collections.Generic;

namespace BingoAdvancedCustomGeneration
{
    internal enum Tag
    {
        Earlygame,
        Middlegame,
        Lategame,

        Short,
        Long,

        Dreamnail,
        Cdash,
        Dive,

        Deepnest,
        Hornet2,

        Bossfight,
        Geo,
    }

    internal class AdvancedGoal
    {
        public string Name = string.Empty;
        public List<string> FullExclusions = [];
        public List<string> LineExclusions = [];
        public List<Tag> Tags = [];
        public double Weight = 1d;
    }
}
