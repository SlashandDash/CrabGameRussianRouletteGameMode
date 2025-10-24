using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.IL2CPP.Utils;
using BepInEx.Logging;
using CustomGameModes;
using HarmonyLib;
using SteamworksNative;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameModeRussianRoulette
{

    public sealed class CustomGameModeRussianRoulette : CustomGameModes.CustomGameMode
    {


        #region Fields and Properties

        internal static CustomGameModeRussianRoulette Instance;

        internal Harmony patches;

        public CustomGameModeRussianRoulette() : base
        (
            name: "RussianRoulette",
            description: "• Read the chat options\n\n• Choose your choice by sending cmd in chat\n\n• Good luck :)",
            gameModeType: GameModeType.SnowBrawl,
            vanillaGameModeType: GameModeType.SnowBrawl,
            waitForRoundOverToDeclareSoloWinner: true,

            shortModeTime: 40,
            mediumModeTime: 40,
            longModeTime: 40,

            compatibleMapNames: [
                "Peaceful Platform"
            ],

            smallMapPlayers: 5,
            mediumAndSmallMapPlayers: 5,
            largeAndMediumMapPlayers: 5,
            largeMapPlayers: 5
        )
            => Instance = this;


        public override void PreInit()
        {
            patches = Harmony.CreateAndPatchAll(GetType());

        }
        #endregion

        #region Harmony Patches

        [HarmonyPatch(typeof(GameMode), nameof(GameMode.Init))]
        [HarmonyPostfix]
        public static void GameModeInitPost(GameMode __instance)
        {
            if (!SteamManager.Instance.IsLobbyOwner())
                return;

        }

        [HarmonyPatch(typeof(GameMode), nameof(GameMode.EndRound))]
        [HarmonyPostfix]
        public static void GameModeEnd(GameMode __instance)
        {

            if (!SteamManager.Instance.IsLobbyOwner())
                return;

        }



        #endregion
        #region Player Utilities
        public static List<ulong> GetAlivePlayers()
        {
            List<ulong> list = new();
            foreach (var player in GameManager.Instance.activePlayers)
            {
                if (player == null || player.Value.dead) continue;
                list.Add(player.Key);
            }
            return list;
        }

        #endregion
    }
}
