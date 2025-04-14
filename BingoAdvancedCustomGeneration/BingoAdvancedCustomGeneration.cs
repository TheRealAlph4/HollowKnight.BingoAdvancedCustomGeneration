using Modding;
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
            GoalManager.SetupCustomVanillaExclusions();

            Log("Initializing");
        }
    }
}