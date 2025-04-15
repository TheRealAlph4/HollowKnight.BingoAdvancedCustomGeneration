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

            List<string> goals = ["Buy 6 map pins from Iselda (All but two)", "Buy 8 map pins from Iselda (All)", "Desolate Dive", "Soul Master", "Descending Dark", "Dream Nail", "Xero", "Crystal Heart", "Crystal Guardian 1", "Kill Myla", "Lumafly Lantern", "Unlock Queen's Stag + King's Stag Stations", "Have 1500 geo in the bank", "Abyss Shriek", "All Grubs: Greenpath (4) + Fungal (2)", "All Grubs: Xroads (5) + Fog Canyon (1)", "Break the 420 geo rock in Kingdom's Edge", "Broken Vessel", "Collect 3 King's Idols", "Collect 500 essence", "Collector", "Colosseum 1", "Complete 4 full dream trees", "Cyclone Slash", "Dash Slash", "Deep Focus + Quick Focus", "Defeat Colosseum Zote", "Dream Gate", "Dream Nail", "Dream Wielder", "Dung Defender", "Elder Hu", "Failed Champion", "False Knight + Brooding Mawlek"];

            Log(goals.Count);

            BingoSync.Goals.AddGameMode(new AdvancedGameMode("Tournament", GoalManager.GetGoalsByName(goals)));

            Log("Initializing");
        }
    }
}