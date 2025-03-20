using HarmonyLib;
using MelonLoader;
using UnityEngine;
using Il2Cpp;

[assembly: MelonInfo(typeof(Utilities.Core), "Utilities Addon", "231.0.0", "dynaslash & TuanAnh2901", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Utilities
{
    public class Core : MelonMod
    {

        private static DateTime dtStart;
        private static DateTime? dtStartToast;
        private static string toast_txt;
        public static bool isSeedRain = false;
        public static bool isScaredyDream = false;

        public static MelonPreferences_Entry<KeyCode> unliSunKeybind;
        public static MelonPreferences_Entry<KeyCode> unliCoinsKeybind;
        public static MelonPreferences_Entry<KeyCode> noCooldownKeybind;
        public static MelonPreferences_Entry<KeyCode> invulPlantsKeybind;
        public static MelonPreferences_Entry<KeyCode> invulZombiesKeybind;
        public static MelonPreferences_Entry<KeyCode> doubleDamageKeybind;
        public static MelonPreferences_Entry<KeyCode> superDamageKeybind;
        public static MelonPreferences_Entry<KeyCode> stopZombieSpawnKeybind;
        public static MelonPreferences_Entry<KeyCode> stopGameOverKeybind;
        public static MelonPreferences_Entry<KeyCode> plantEverywhereKeybind;
        public static MelonPreferences_Entry<KeyCode> developerModeKeybind;
        public static MelonPreferences_Entry<KeyCode> columnPlantsKeybind;
        public static MelonPreferences_Entry<KeyCode> scaredyDreamKeybind;
        public static MelonPreferences_Entry<KeyCode> seedRainKeybind;
        public static MelonPreferences_Entry<KeyCode> generateTrophyKeybind;
        public static MelonPreferences_Entry<KeyCode> generateFertilizerKeybind;
        public static MelonPreferences_Entry<KeyCode> generateBucketKeybind;
        public static MelonPreferences_Entry<KeyCode> generateHelmetKeybind;
        public static MelonPreferences_Entry<KeyCode> generateJackKeybind;
        public static MelonPreferences_Entry<KeyCode> generatePickaxeKeybind;
        public static MelonPreferences_Entry<KeyCode> generateMechaKeybind;
        public static MelonPreferences_Entry<KeyCode> generateSuperMechaKeybind;
        public static MelonPreferences_Entry<KeyCode> generateMeteorKeybind;
        public static MelonPreferences_Entry<KeyCode> generateSproutKeybind;
        public static MelonPreferences_Entry<KeyCode> charmAllKeybind;
        public static MelonPreferences_Entry<KeyCode> killAllPlantsKeybind;
        public static MelonPreferences_Entry<KeyCode> killAllZombiesKeybind;
        public static MelonPreferences_Entry<KeyCode> showUtilitiesKeybind;

        public override void OnEarlyInitializeMelon() => dtStart = DateTime.Now;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Utilities Addon is loaded!");
            var category = MelonPreferences.CreateCategory("Utilities_Addon");
            unliSunKeybind = category.CreateEntry("Unli Sun Keybind", KeyCode.F1);
            unliCoinsKeybind = category.CreateEntry("Unli Coins Keybind", KeyCode.F2);
            noCooldownKeybind = category.CreateEntry("No Cooldown Keybind", KeyCode.F3);
            invulPlantsKeybind = category.CreateEntry("Invulnerable Plants Keybind", KeyCode.F4);
            invulZombiesKeybind = category.CreateEntry("Invulnerable Zombies Keybind", KeyCode.F5);
            doubleDamageKeybind = category.CreateEntry("Double Damage Keybind", KeyCode.F6);
            superDamageKeybind = category.CreateEntry("Super Damage Keybind", KeyCode.F7);
            stopZombieSpawnKeybind = category.CreateEntry("Stop Zombie Spawn Keybind", KeyCode.F8);
            stopGameOverKeybind = category.CreateEntry("Stop Game Over Keybind", KeyCode.F9);
            plantEverywhereKeybind = category.CreateEntry("Plant Everywhere Keybind", KeyCode.F10);
            developerModeKeybind = category.CreateEntry("Developer Mode Keybind", KeyCode.F11);

            columnPlantsKeybind = category.CreateEntry("Column Plants Keybind", KeyCode.Semicolon);
            scaredyDreamKeybind = category.CreateEntry("Scaredy Dream Keybind", KeyCode.Quote);
            seedRainKeybind = category.CreateEntry("Seed Rain Keybind", KeyCode.Backslash);

            generateTrophyKeybind = category.CreateEntry("Generate Trophy Keybind", KeyCode.Keypad0);
            generateFertilizerKeybind = category.CreateEntry("Generate Fertilizer Keybind", KeyCode.Keypad1);
            generateBucketKeybind = category.CreateEntry("Generate Bucket Keybind", KeyCode.Keypad2);
            generateHelmetKeybind = category.CreateEntry("Generate Helmet Keybind", KeyCode.Keypad3);
            generateJackKeybind = category.CreateEntry("Generate Jack-in-the-Box Keybind", KeyCode.Keypad4);
            generatePickaxeKeybind = category.CreateEntry("Generate Pickaxe Keybind", KeyCode.Keypad5);
            generateMechaKeybind = category.CreateEntry("Generate Mecha Keybind", KeyCode.Keypad6);
            generateSuperMechaKeybind = category.CreateEntry("Generate Super Mecha Keybind", KeyCode.Keypad7);
            generateMeteorKeybind = category.CreateEntry("Generate Meteor Keybind", KeyCode.Keypad8);
            generateSproutKeybind = category.CreateEntry("Generate Sprout Keybind", KeyCode.Keypad9);

            charmAllKeybind = category.CreateEntry("Charm All Zombies Keybind", KeyCode.KeypadMultiply);
            killAllZombiesKeybind = category.CreateEntry("Kill All Zombies Keybind", KeyCode.KeypadMinus);
            killAllPlantsKeybind = category.CreateEntry("Kill All Plants Keybind", KeyCode.KeypadPlus);

            showUtilitiesKeybind = category.CreateEntry("Show Utilities Keybind", KeyCode.F12);
        }

        public override void OnLateInitializeMelon() => dtStart = DateTime.Now;

        public override void OnLateUpdate()
        {
            Utility.OnLateUpdate();
        }

        public override void OnGUI()
        {
            if (Utility.GetActive(Utility.UtilityType.ShowUtilities) || DateTime.Now - dtStart < new TimeSpan(0 , 0, 0, 5))
            {
                string text = Utility.GetUtilities();
                int num = 0;
                int num2 = 20;
                foreach (string text2 in text.Split('\n', StringSplitOptions.None))
                {
                    if (text2.Length > num2)
                    {
                        num2 = text2.Length;
                    }
                    num++;
                }
                bool flag = GUI.Button(new Rect(10f, 30f, (float)num2 * 10f, (float)num * 16f + 15f), text);
            }

            if (dtStartToast != null)
            {
                GUI.Button(new Rect(10f, 10f, 200f, 20f), "\n" + toast_txt + "\n");
                TimeSpan? timeSpan = DateTime.Now - dtStartToast;
                TimeSpan t = new TimeSpan(0, 0, 0, 2);
                if (timeSpan > t)
                {
                    dtStartToast = null;
                }
            }
        }

        public static void ShowToast(string message)
        {
            toast_txt = message;
            dtStartToast = new DateTime?(DateTime.Now);
        }
    }
}