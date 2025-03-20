using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using MelonLoader;
using System.Reflection;
using UnityEngine;

namespace Odyssey_Buffs
{
	[HarmonyPatch(typeof(Board))]
	internal class Patch
	{
		[HarmonyPatch("Awake")]
		[HarmonyPostfix]
		private static void TravelPostfix(Board __instance)
		{
			// Ensure the config is loaded correctly
			Core.instance.ReloadConfig();
			MelonLogger.Msg("Odyssey Buffs is loaded!");

			if (Core.instance.configEnablePlant.Value)
			{
				// Enable travel plant feature in board tag
				object obj = __instance.boardTag;
				Type type = obj.GetType();
				FieldInfo field = type.GetField("enableTravelPlant");
				// MelonLogger.Msg($"configEnablePlant Value = {Core.instance.configEnablePlant.Value}");

				if (field != null)
				{
					object obj2 = Convert.ChangeType(true, field.FieldType);
					field.SetValue(obj, obj2);
					// MelonLogger.Msg("Field Null");
				}
				__instance.boardTag = (Board.BoardTag)obj;
			}

			GameObject gameObject = new GameObject("GameAPP");
			// MelonLogger.Msg("GameAPP created");
			TravelMgr travelMgr = gameObject.GetComponent<TravelMgr>();

			if (travelMgr == null)
			{
				travelMgr = gameObject.AddComponent<TravelMgr>();
			}

			// Handle IZ and travel entries configuration
			if (Core.instance.configEnableEntries.Value)
			{
				// MelonLogger.Msg("configEnableEntries Value = " + Core.instance.configEnableEntries.Value);
				if (GameAPP.theBoardLevel == 8 && GameAPP.theBoardType == 3)
				{
					// MelonLogger.Msg("Board Level and Board Type: " + GameAPP.theBoardLevel + " " + GameAPP.theBoardType);
					if (!Core.instance.configEnableTravel.Value)
					{
						// MelonLogger.Msg("configEnableTravel Value = " + Core.instance.configEnableTravel.Value);
						return;
					}
				}

				if (GameAPP.theBoardType == 2)
				{
					// MelonLogger.Msg("Board Type: " + GameAPP.theBoardType);
					if (!Core.instance.configEnableIZ.Value)
					{
						// MelonLogger.Msg("configEnableIZ Value = " + Core.instance.configEnableIZ.Value);
						return;
					}
				}

				if (travelMgr.advancedUpgrades == null)
                {
                    travelMgr.advancedUpgrades = new Il2CppStructArray<bool>(Core.instance.boolArrayadvancedConfig.Length);
                }
                if (travelMgr.ultimateUpgrades == null)
                {
                    travelMgr.ultimateUpgrades = new Il2CppStructArray<bool>(Core.instance.boolArrayultimateConfig.Length);
                }

				#if TESTING
                MelonLogger.Msg($"Ultimate Upgrades Array Size: {travelMgr.ultimateUpgrades.Length}");
                MelonLogger.Msg($"Ultimate Config Array Size: {Core.instance.boolArrayultimateConfig.Length}");

                // Apply ultimate upgrades với kiểm tra kích thước
                if (Core.instance.boolArrayultimateConfig.Length != travelMgr.ultimateUpgrades.Length)
                {
                    MelonLogger.Warning($"Config size mismatch! Expected {travelMgr.ultimateUpgrades.Length} but got {Core.instance.boolArrayultimateConfig.Length}");
                }

				
                for (int i = 0; i < Math.Min(travelMgr.ultimateUpgrades.Length, Core.instance.boolArrayultimateConfig.Length); i++)
                {
                    travelMgr.ultimateUpgrades[i] = Core.instance.boolArrayultimateConfig[i].Value;
                }
				#endif

				// Apply advanced and ultimate upgrade configurations

				MelonLogger.Msg("");
				MelonLogger.Msg("Loading advanced upgrades...");
				for (int i = 0; i < Math.Min(travelMgr.advancedUpgrades.Length, Core.instance.boolArrayadvancedConfig.Length); i++)
				{
					travelMgr.advancedUpgrades[i] = Core.instance.boolArrayadvancedConfig[i].Value;
					MelonLogger.Msg($"Advanced Upgrade {Core.instance.boolArrayadvancedConfig[i].DisplayName} = {Core.instance.boolArrayadvancedConfig[i].Value} applied.");
				}
				MelonLogger.Msg("Advanced upgrades loaded!");

				MelonLogger.Msg("");
				MelonLogger.Msg("Loading ultimate upgrades...");
				for (int i = 0; i < Math.Min(travelMgr.ultimateUpgrades.Length, Core.instance.boolArrayultimateConfig.Length); i++)
				{
					travelMgr.ultimateUpgrades[i] = Core.instance.boolArrayultimateConfig[i].Value;
					MelonLogger.Msg($"Ultimate Upgrade {Core.instance.boolArrayultimateConfig[i].DisplayName} = {Core.instance.boolArrayultimateConfig[i].Value} applied.");
				}
				MelonLogger.Msg("Ultimate upgrades loaded!");
				
				if (Core.instance.configEnableDebuffs.Value)
				{
					MelonLogger.Msg("");
					MelonLogger.Msg("Loading debuffs...");
					for (int i = 0; i < travelMgr.debuff.Count; i++)
					{
						if (Core.debuffsKeys[i].StartsWith("Skip_"))
						{
							travelMgr.debuff[i] = false;
							continue;
						}
						travelMgr.debuff[i] = Core.instance.boolArraydebuffsConfig[i].Value;
						MelonLogger.Msg($"Debuff {Core.instance.boolArraydebuffsConfig[i].DisplayName} = {Core.instance.boolArraydebuffsConfig[i].Value} applied.");
					}
				}
			}
		}
	}
}