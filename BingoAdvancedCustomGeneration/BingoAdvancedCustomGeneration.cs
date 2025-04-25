using Modding;
using MonoMod.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BingoAdvancedCustomGeneration
{
    public class BingoAdvancedCustomGeneration : Mod
    {
        new public string GetName() => "BingoAdvancedCustomGeneration";

        public static string version = "1.0.0.0";
        public override string GetVersion() => version;

        public override int LoadPriority() => 10;

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            List<string> groupNames = ["Vanilla", "Extended", "Extended+"];
            foreach (string groupName in groupNames)
            {
                GoalManager.PreCopyGoalGroup(groupName);
            }

            Dictionary<string, AdvancedGoal> goals = [];
            goals.AddRange(GoalManager.GetGoalsByGroupName("Vanilla"));
            goals.AddRange(GoalManager.GetGoalsByGroupName("Extended"));
            
            GoalManager.SetupCustomTournamentExclusions(goals);

            goals.Remove("Slash Millibelle in Pleasure House");
            goals.Remove("Open 6 geo chests (not in junk pit)");
            goals.Remove("Decipher Hunter's Notes: Maskfly + Shrumeling");
            goals.Remove("Collect 4 Simple Keys");

            BingoSync.Goals.AddGameMode(new AdvancedGameMode("Tournament 3", goals));

            Log("Initializing");
        }
    }
}