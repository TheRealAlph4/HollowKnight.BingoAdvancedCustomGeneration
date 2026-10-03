using BingoAdvancedCustomGeneration.AdvancedGameModes;
using BingoSync.Interfaces;
using Modding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BingoAdvancedCustomGeneration
{
    public class BingoAdvancedCustomGeneration : Mod
    {
        new public string GetName() => "BingoAdvancedCustomGeneration";

        public static string version = "1.2.1.1";
        public override string GetVersion() => version;

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            OrderedLoader.OnStandaloneGoalsGameModesLoaded += SetupGoalsGameModes;
            Log("Initializing");
        }

        private void SetupGoalsGameModes(object _, EventArgs __)
        {
            Goals.AddGameMode(new GameModeTournament3());
            Goals.AddGameMode(new GameModeStallball());
        }
    }
}