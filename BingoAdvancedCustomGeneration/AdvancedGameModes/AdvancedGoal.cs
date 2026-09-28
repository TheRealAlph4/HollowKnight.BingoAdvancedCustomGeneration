using System.Collections.Generic;
using System.Linq;

namespace BingoAdvancedCustomGeneration.AdvancedGameModes
{
    public enum Tag
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

    public class AdvancedGoal
    {
        public string Name = string.Empty;
        public List<string> FullExclusions = [];
        public List<string> LineExclusions = [];
        public List<Tag> Tags = [];
        public double Weight = 1d;

        public AdvancedGoal DeepCopy()
        {
            return AdvancedGoal.DeepCopy(this);
        }

        public static AdvancedGoal DeepCopy(AdvancedGoal other)
        {
            return new AdvancedGoal()
            {
                Name = string.Copy(other.Name),
                FullExclusions = [.. other.FullExclusions.Select(str => string.Copy(str))],
                LineExclusions = [.. other.LineExclusions.Select(str => string.Copy(str))],
                Tags = [.. other.Tags],
                Weight = other.Weight,
            };
        }

        public void Exclude(AdvancedGoal other, bool line = false)
        {
            if (line)
            {
                LineExclusions.Add(other.Name);
                other.LineExclusions.Add(Name);
            }
            else
            {
                FullExclusions.Add(other.Name);
                other.FullExclusions.Add(Name);
            }
        }

        public void Unexclude(AdvancedGoal other)
        {
            LineExclusions.Remove(other.Name);
            other.LineExclusions.Remove(Name);

            FullExclusions.Remove(other.Name);
            other.FullExclusions.Remove(Name);
        }
    }
}
