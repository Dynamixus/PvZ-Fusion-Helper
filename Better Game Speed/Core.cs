using HarmonyLib;
using MelonLoader;
using UnityEngine;
using Il2Cpp;

[assembly: MelonInfo(typeof(Better_Game_Speed.Core), "Better Game Speed", "221.0.0", "dynaslash", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Better_Game_Speed
{
	public class Core : MelonMod
	{
		public static Core instance;
		public static MelonPreferences_Entry<KeyCode> configDecrease;
		public static MelonPreferences_Entry<KeyCode> configIncrease;
		public static MelonPreferences_Entry<KeyCode> configPauseKey;

		public static MelonPreferences_Entry<float> configSpeed;
		public static MelonPreferences_Entry<bool> configEnable;
		public static MelonPreferences_Entry<bool> configPause;
		public override void OnInitializeMelon()
		{
			MelonLogger.Msg("Better Game Speed is loaded!");
			var category = MelonPreferences.CreateCategory("Better_Game_Speed", " ");

			configDecrease = category.CreateEntry("Decrease_Speed_Hotkey", KeyCode.Comma, "Decrease game speed");
			configIncrease = category.CreateEntry("Increase_Speed_Hotkey", KeyCode.Slash, "Increase game speed");
			configPauseKey = category.CreateEntry("Pause_Hotkey", KeyCode.Period, "Pause game");

			configEnable = category.CreateEntry("EnableKeys", true, "Enable shortcut keys for adjusting game speed");
			configSpeed = category.CreateEntry("Time", 1f, "Game speed");
			configPause = category.CreateEntry("Pause", false, "Pause game");
			instance = this;
		}

		public override void OnUpdate()
		{
			if (configEnable.Value)
			{
				if (configPause.Value == false)
				{ 
					if (Input.GetKeyDown(configIncrease.Value) && configSpeed.Value < 3f)
					{
						configSpeed.Value += 0.5f;
						// MelonPreferences.Save();
						UpdateGameSpeed();
					}
					if (Input.GetKeyDown(configDecrease.Value) && configSpeed.Value > 1f)
					{
						configSpeed.Value -= 0.5f;
						// MelonPreferences.Save();
						UpdateGameSpeed();
					}
				}

				if (Input.GetKeyDown(configPauseKey.Value))
				{
					configPause.Value = !configPause.Value;
					// MelonPreferences.Save();
					UpdateGameSpeed();
				}
			}

			// Clamping game speed within set bounds
			if (configPause.Value == false)
			{
				if (configSpeed.Value < 1f && configSpeed.Value > 0f)
				{
					configSpeed.Value = 1f;
					// MelonPreferences.Save();
				}
				if (configSpeed.Value > 3f)
				{
					configSpeed.Value = 3f;
					// MelonPreferences.Save();
				}
			}
		}

		private void UpdateGameSpeed()
		{
			if (configPause.Value == true)
			{
				GameAPP.gameSpeed = 0f;
				Time.timeScale = 0f;
			}
			else
			{ 
				GameAPP.gameSpeed = configSpeed.Value;
				Time.timeScale = GameAPP.gameSpeed;
			}

		}

		public void ReloadConfig()
		{
			MelonPreferences.Load();
		}
	}

	[HarmonyPatch(typeof(GameSpeedMgr))]
	public class GameSpeed_Patch
	{
		[HarmonyPrefix]
		[HarmonyPatch(nameof(GameSpeedMgr.Update))]
		private static bool Update(GameSpeedMgr __instance)
		{

			bool isPaused = GameAPP.theGameStatus == 1;
			bool isAlmanac = GameAPP.theGameStatus == 4;
			if (isPaused || isAlmanac)
			{
				Time.timeScale = 0f;
				return true;
			}
			else
			{
				GameAPP.gameSpeed = Core.configSpeed.Value;
				Time.timeScale = Core.configSpeed.Value;

				__instance.slider.value = Core.configSpeed.Value;
				return false;
			}
			
		}
	}
}