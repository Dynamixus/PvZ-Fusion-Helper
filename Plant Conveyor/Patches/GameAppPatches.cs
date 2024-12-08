using HarmonyLib;
using Il2Cpp;

namespace PlantConveyor.Patches
{
    [HarmonyPatch(typeof(GameAPP))]
    public static class GameAppPatches
    {
        [HarmonyPatch(nameof(GameAPP.Awake))]
        [HarmonyPostfix]
        private static void PostAwake() => DefinePlantIDsList();
        private static void DefinePlantIDsList()
        {
            Core.Instance.PlantIDs = new();
            for (int i = 0; i < GameAPP.plantPrefab.Length; i++)
                if (GameAPP.plantPrefab[i])
                    Core.Instance.PlantIDs.Add(i);
        }
    }
}
