using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

[assembly: MelonInfo(typeof(totallyaccuratecheatthing.Class1), "HAHAHA", "1", "jo && rsm")]

namespace totallyaccuratecheatthing
{
    public class Class1 : MelonMod
    {

        public override void OnApplicationStart()
        {

            var originalMethod = typeof(equ8.client).GetMethod(nameof(equ8.client.initialize), BindingFlags.NonPublic | BindingFlags.Instance);
            var prefixMethod = typeof(Class1).GetMethod(nameof(Class1.yeah), BindingFlags.NonPublic | BindingFlags.Static);
            HarmonyInstance.Patch(originalMethod, new HarmonyMethod(prefixMethod));
            AntiCheatClient.Deinitialize();
            MelonLogger.Msg("Goodbye");

        }

        private static bool yeah(string __instance, equ8.equ8_mode __0)
        {

            __0 = equ8.equ8_mode.disabled;

            return false;
        }

    }

    [HarmonyPatch(typeof(equ8.client), "initialize")]
    public class TotallyAccurateAntiCheatDisabler
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            MelonLogger.Msg("EQU8 INIT");
            return false;
        }
    }

    //just to be safe
    [HarmonyPatch(typeof(AntiCheatClient), "Initialize")]
    public class TotallyAccurateAntiCheatDisablerEnsure
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            MelonLogger.Msg("ACC INIT");
            equ8.client.deinitialize();
            AntiCheatClient.Deinitialize();
            return false;
        }
    }
}