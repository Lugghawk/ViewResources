using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;

namespace ViewResources
{

    [BepInPlugin("com.acidic.viewresources", "ViewResources", "0.0.1")]
    public class ViewResources : BaseUnityPlugin
    {
        Harmony harmony;
        new internal static ManualLogSource Logger;

        public void Awake()
        {
            try
            {
                Logger = base.Logger;
                Logger.LogDebug("Starting Up!");

                harmony = new Harmony("com.acidic.viewresources");
                harmony.PatchAll(typeof(ViewResources));
            } catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }
        }
        [HarmonyPrefix, HarmonyPatch(typeof(UIStarDetail), "OnStarDataSet")]
        public static void Patch_OnStarDataSet(UIStarDetail __instance)
        {
            if (__instance == null) return;
            if (__instance.star == null) return;
            Logger.LogInfo("OnStarDataSet ... patched");
            // Star's loaded status currently seems to be solely dependant on the planet's loaded status.
            foreach (PlanetData planet in __instance.star.planets)
            {
                Logger.LogInfo("Planet: " + planet.displayName);
                planet.loaded = true;
            }


        }

    }

    
}
