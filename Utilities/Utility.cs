using UnityEngine;
using Il2Cpp;
using System.Text;

namespace Utilities
{
	internal class Utility
	{
		public enum UtilityType
		{
			UnliSun, // F1 Done
			UnliCoins, // F2 Done
			NoCooldown, // F3 Done
			InvulPlants, // F4 Done
			InvulZombies, // F5 Done
			DoubleDamage, // F6 Done
			SuperDamage, // F7 Done
			StopZombieSpawn, // F8 Done
			StopGameOver, // F9 Done
			PlantEverywhere, // F10 Done
			DeveloperMode, // F11 Done
			ShowUtilities, // F12 Done
			ColumnPlants, // Semicolon Done
			ScaredyDream, // Quote 
			SeedRain, // Backslash 

			GenerateTrophy, // 0
			GenerateFertilizer, // 1
			GenerateBucket, // 2
			GenerateHelmet, // 3
			GenerateJack, // 4
			GeneratePickaxe, // 5
			GenerateMecha, // 6
			GenerateSuperMecha, // 7
			GenerateMeteor, // 8
			GenerateSprout, // 9
			CharmAll, // *
			KillAllZombies, // -
			KillAllPlants, // +

		}

		public enum GenerateType
		{
			GenerateTrophy, // 0
			GenerateFertilizer, // 1
			GenerateBucket, // 2
			GenerateHelmet, // 3
			GenerateJack, // 4
			GeneratePickaxe, // 5
			GenerateMecha, // 6
			GenerateSuperMecha, //7
			GenerateMeteor, // 8
			GenerateSprout, // 9
			CharmAll, // *
			KillAllZombies, // -
			KillAllPlants, // +
		}

		private class UtilityFeature
		{
			public string Name { get; private set; }
			public UtilityType UtilityType { get; private set; }
			public KeyCode KeyCode { get; private set; }
			public bool IsActive { get; set; }

			public UtilityFeature(string Name, UtilityType UtilityType, KeyCode KeyCode, bool defaultValue = false)
			{
				this.Name = Name;
				this.UtilityType = UtilityType;
				this.KeyCode = KeyCode;
				this.IsActive = defaultValue;
			}

			public void ToggleUtility()
			{

				IsActive = !IsActive;

				if (this.UtilityType == UtilityType.GenerateTrophy || this.UtilityType == UtilityType.GenerateFertilizer || this.UtilityType == UtilityType.GenerateBucket || this.UtilityType == UtilityType.GenerateHelmet || this.UtilityType == UtilityType.GenerateJack || this.UtilityType == UtilityType.GeneratePickaxe || this.UtilityType == UtilityType.GenerateMecha || this.UtilityType == UtilityType.GenerateSuperMecha || this.UtilityType == UtilityType.GenerateMeteor || this.UtilityType == UtilityType.GenerateSprout)
				{
					return;
				}

				if (this.UtilityType == UtilityType.CharmAll || this.UtilityType == UtilityType.KillAllPlants || this.UtilityType == UtilityType.KillAllZombies)
				{
					return;
				}

				if (this.UtilityType == UtilityType.ShowUtilities)
					return;

				Core.ShowToast(string.Format("{0} [{1}]", this.Name, IsActive ? "ON" : "OFF"));
			}

			public override string ToString()
			{
				if (this.UtilityType == UtilityType.ShowUtilities)
				{
					return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
				}

				if (this.UtilityType == UtilityType.GenerateTrophy || this.UtilityType == UtilityType.GenerateFertilizer || this.UtilityType == UtilityType.GenerateBucket || this.UtilityType == UtilityType.GenerateHelmet || this.UtilityType == UtilityType.GenerateJack || this.UtilityType == UtilityType.GeneratePickaxe || this.UtilityType == UtilityType.GenerateMecha || this.UtilityType == UtilityType.GenerateSuperMecha || this.UtilityType == UtilityType.GenerateMeteor || this.UtilityType == UtilityType.GenerateSprout)
				{
					return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
				}

				if (this.UtilityType == UtilityType.CharmAll || this.UtilityType == UtilityType.KillAllPlants || this.UtilityType == UtilityType.KillAllZombies)
				{
					return string.Format("[{0}] {1}", this.KeyCode.ToString(), this.Name);
				}

				return string.Format("[{0}] {1} [{2}]", this.KeyCode.ToString(), this.Name, IsActive ? "ON" : "OFF");
			}
		}

		private static Dictionary<UtilityType, UtilityFeature> utilityLists = new Dictionary<UtilityType, UtilityFeature>()
		{
			{UtilityType.UnliSun, new UtilityFeature("Unlimited Sun", UtilityType.UnliSun, Core.unliSunKeybind.Value)},
			{UtilityType.UnliCoins, new UtilityFeature("Unlimited Coins", UtilityType.UnliCoins, Core.unliCoinsKeybind.Value)},
			{UtilityType.NoCooldown, new UtilityFeature("No Cooldown", UtilityType.NoCooldown, Core.noCooldownKeybind.Value)},
			{UtilityType.InvulPlants, new UtilityFeature("Invulnerable Plants", UtilityType.InvulPlants, Core.invulPlantsKeybind.Value)},
			{UtilityType.InvulZombies, new UtilityFeature("Invulnerable Zombies", UtilityType.InvulZombies, Core.invulZombiesKeybind.Value)},
			{UtilityType.DoubleDamage, new UtilityFeature("Double Plant Damage", UtilityType.DoubleDamage, Core.doubleDamageKeybind.Value)},
			{UtilityType.SuperDamage, new UtilityFeature("10x Plant Damage", UtilityType.SuperDamage, Core.superDamageKeybind.Value)},
			{UtilityType.StopZombieSpawn, new UtilityFeature("Stop Zombie Spawn", UtilityType.StopZombieSpawn, Core.stopZombieSpawnKeybind.Value)},
			{UtilityType.StopGameOver, new UtilityFeature("Stop Game Over", UtilityType.StopGameOver, Core.stopGameOverKeybind.Value)},
			{UtilityType.PlantEverywhere, new UtilityFeature("Plant Everywhere", UtilityType.PlantEverywhere, Core.plantEverywhereKeybind.Value)},
			{UtilityType.DeveloperMode, new UtilityFeature("Developer Mode", UtilityType.DeveloperMode, Core.developerModeKeybind.Value)},

			{UtilityType.ColumnPlants, new UtilityFeature("Column Plants", UtilityType.ColumnPlants, Core.columnPlantsKeybind.Value)},
			{UtilityType.ScaredyDream, new UtilityFeature("Scaredy Dream", UtilityType.ScaredyDream, Core.scaredyDreamKeybind.Value)},
			{UtilityType.SeedRain, new UtilityFeature("Seed Rain", UtilityType.SeedRain, Core.seedRainKeybind.Value)},

			{UtilityType.GenerateTrophy, new UtilityFeature("Generate Trophy", UtilityType.GenerateTrophy, Core.generateTrophyKeybind.Value)},
			{UtilityType.GenerateFertilizer, new UtilityFeature("Generate Fertilizer", UtilityType.GenerateFertilizer, Core.generateFertilizerKeybind.Value)},
			{UtilityType.GenerateBucket, new UtilityFeature("Generate Bucket", UtilityType.GenerateBucket, Core.generateBucketKeybind.Value)},
			{UtilityType.GenerateHelmet, new UtilityFeature("Generate Helmet", UtilityType.GenerateHelmet, Core.generateHelmetKeybind.Value)},
			{UtilityType.GenerateJack, new UtilityFeature("Generate Jack-in-the-Box", UtilityType.GenerateJack, Core.generateJackKeybind.Value)},
			{UtilityType.GeneratePickaxe, new UtilityFeature("Generate Pickaxe", UtilityType.GeneratePickaxe, Core.generatePickaxeKeybind.Value)},
			{UtilityType.GenerateMecha, new UtilityFeature("Generate Mecha Fragment", UtilityType.GenerateMecha, Core.generateMechaKeybind.Value)},
			{UtilityType.GenerateSuperMecha, new UtilityFeature("Generate Giga Mecha Fragment", UtilityType.GenerateSuperMecha, Core.generateSuperMechaKeybind.Value)},
			{UtilityType.GenerateMeteor, new UtilityFeature("Generate Meteor", UtilityType.GenerateMeteor, Core.generateMeteorKeybind.Value)},
			{UtilityType.GenerateSprout, new UtilityFeature("Generate Sprout", UtilityType.GenerateSprout, Core.generateSproutKeybind.Value)},
			{UtilityType.CharmAll, new UtilityFeature("Charm All Zombies", UtilityType.CharmAll, Core.charmAllKeybind.Value)},
			{UtilityType.KillAllZombies, new UtilityFeature("Kill All Zombies", UtilityType.KillAllZombies, Core.killAllZombiesKeybind.Value)},
			{UtilityType.KillAllPlants, new UtilityFeature("Kill All Plants", UtilityType.KillAllPlants, Core.killAllPlantsKeybind.Value)},

			{UtilityType.ShowUtilities, new UtilityFeature("Utilities List", UtilityType.ShowUtilities, Core.showUtilitiesKeybind.Value, false)},
		};

		public static string GetUtilities()
		{
			StringBuilder status = new StringBuilder();
			status.AppendLine("Utilities: ");
			foreach (var utility in utilityLists.Values)
			{
				status.AppendLine(utility.ToString());
			}
			return status.ToString();
		}

		public static void ToggleUtility(UtilityType UtilityType)
		{
			if (!utilityLists.ContainsKey(UtilityType))
				return;

			utilityLists[UtilityType].ToggleUtility();
		}

		public static bool GetActive(UtilityType UtilityType)
		{
			if (!utilityLists.ContainsKey(UtilityType))
				return false;

			return utilityLists[UtilityType].IsActive;
		}

		public static void SetActive(UtilityType UtilityType, bool value)
		{
			if (!utilityLists.ContainsKey(UtilityType))
				return;

			utilityLists[UtilityType].IsActive = value;
		}

		public static void OnLateUpdate()
		{
			foreach (var (type, utility) in utilityLists)
			{
				if (utility.KeyCode != KeyCode.None && Enum.IsDefined(typeof(KeyCode), utility.KeyCode))
				{
					if (Input.GetKeyDown(utility.KeyCode))
					{
						utility.ToggleUtility();
					}
				}
			}
		}
		public static void SpawnItem(string resourcePath)
		{
			GameObject gameObject = Resources.Load<GameObject>(resourcePath);
			if (gameObject != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(gameObject, new Vector2(0f, 0f), Quaternion.identity, GameAPP.board.transform);
				return;
			}
		}
	}
}
