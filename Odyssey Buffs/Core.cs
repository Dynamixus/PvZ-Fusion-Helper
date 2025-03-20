using MelonLoader;

[assembly: MelonInfo(typeof(Odyssey_Buffs.Core), "Odyssey Buffs", "221.0.0", "dynaslash & TuanAnh2901", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Odyssey_Buffs
{
	public class Core : MelonMod
	{
		public static Core instance;

		//
		// Configuration entries
		public MelonPreferences_Entry<bool> configEnablePlant;
		public MelonPreferences_Entry<bool> configEnableEntries;
		public MelonPreferences_Entry<bool> configEnableDebuffs;
		public MelonPreferences_Entry<bool> configEnableTravel;
		public MelonPreferences_Entry<bool> configEnableIZ;

		public MelonPreferences_Entry<bool>[] boolArrayadvancedConfig;
		public MelonPreferences_Entry<bool>[] boolArrayultimateConfig;
		public MelonPreferences_Entry<bool>[] boolArraydebuffsConfig;

		public static string[] advancedUpgradesKeys = new string[]
		{
			"Empress_Buff1", "Empress_Buff2",
			"GatlingDoom_Buff1", "GatlingDoom_Buff2",
			"TwinSolar_Buff1", "TwinSolar_Buff2",
			"Spikesidian_Buff1", "Spikesidian_Buff2",
			"DoubleSun", "GloveHalfCooldown",
			"PlantDMGIncrease", "ZombiesReceiveDMGIncrease",
			"ZombieInstanceDamage", "SelectPlantLoadout",
			"RerollPlus2", "EnflamedDMGMultiplier",
			"GloveNoCooldown",
			"GatlingIcicle_Buff1", "GatlingIcicle_Buff2",
			"MeteorHalfCooldown", "Lumos4Levels",
			"RadiantPot_Buff1", "RadiantPot_Buff2",
			"MidasUmbrella_Buff1", "MidasUmbrella_Buff2",
			"Gain3000SunAndDoubled", "Mallet6SecCooldown",
			"PlantHalfRecharge",
			"Zombie1800DMGInstance", "Fission",
			"CharmedZombiesIncreasedDMG", "Phoenix_INFPierce",
			"ImitaterReducedRecharge", "IdenticalPlantDMGIncrease",
			"MagicCat_Buff1","MagicCat_Buff2",
			"TrueCherry", "TrueDoom",
			"CalamityShroom_Buff1","CalamityShroom_Buff2",
			"CherryAndObsidianBuff",
			"TycoonTorch_Buff1", "TycoonTorch_Buff2",
			"ZombieIncreasedDelayDamage", "ColumnPlanting",
			"MagnetarAndHeliosBuff",
			"TeslaUmbrella_Buff1", "TeslaUmbrella_Buff2",
		};

		public static string[] ultimateUpgradesKeys = new string[]
		{
			"Cherrizilla_Buff1", "Cherrizilla_Buff2",
			"GatlingCherrybomber_Buff1", "GatlingCherrybomber_Buff2",
			"Doominator_Buff1", "Doominator_Buff2",
			"SquashedInfernowood_Buff1", "SquashedInfernowood_Buff2",
			"Magnetar_Buff1","Magnetar_Buff2",
			"Oblivion_Buff1", "Oblivion_Buff2",
			"Wither_Buff1", "Wither_Buff2",
			"CoblivionCannon_Buff1", "CoblivionCannon_Buff2",
			"ObsidianTall_Buff1", "ObsidianTall_Buff2",
			"Charmatron_Buff1", "Charmatron_Buff2",
			"TitanApeacalypse_Buff1","TitanApeacalypse_Buff2",
			"Helios_Buff1", "Helios_Buff2",
			"LaserPumpkin_Buff1", "LaserPumpkin_Buff2",
		};

		public static string[] debuffsKeys = new string[]
		{
			"GatlingCherry_Debuff1", "GatlingCherry_Debuff2",
			"GigaRugby_Debuff1", "GigaRugby_Debuff2",
			"Sharkmarine_Debuff1", "Sharkmarine_Debuff2",
			"KirovZomp_Debuff1", "KirovZomp_Debuff2",
			"PogoClown_Debuff1", "PogoClown_Debuff2",
			"GigaTrident_Debuff1", "GigaTrident_Debuff2",
			"Superstar_Debuff1", "Superstar_Debuff2",
			"Wraith_Debuff1", "Wraith_Debuff2",
			"Hwacha_Debuff", 
			"Skip_LoseHalfPlants", "Skip_DivideSun10", 
			"DoubleZombieHealth", "IncreaseMax100Zombies",
			"MichaelZomboni_Debuff1", "MichaelZomboni_Debuff2",
			"SpawnNext_SpawnZomboss", "SpawnNext_SpawnPozeidon",
			"SpawnNext_SpawnUltraMecha", "SpawnNext_GigaRugbyStriker",
			"SpawnNext_SpawnSkystrider", "SpawnNext_SpawnJacksonWorldwide",
			"SpawnNext_SpawnCherryZ",
			"BungeeJumpers_Debuff",
			"SpawnNext_QueenJill"
		};

		public override void OnInitializeMelon()
		{

			MelonLogger.Msg("Odyssey Buffs is loaded!");

			instance = this;
			LoadConfig();
		}

		private void LoadConfig()
		{
			// Create a preference category for your mod
			var mainCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Main");
			var coverageCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Coverage");
			var advancedCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Advanced");
			var ultimateCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Ultimate");
			var debuffsCategory = MelonPreferences.CreateCategory("Odyssey Buffs - Debuffs");

			// Initialize each configuration entry with a default value if it doesn’t already exist
			configEnablePlant = mainCategory.CreateEntry("Enable Odyssey Plants", true);
			configEnableEntries = mainCategory.CreateEntry("Enable Odyssey Buffs", true);
			configEnableDebuffs = mainCategory.CreateEntry("Enable Odyssey Debuffs", true);
			configEnableTravel = coverageCategory.CreateEntry("Override Odyssey", true);
			configEnableIZ = coverageCategory.CreateEntry("Enable in Other Modes", true);

			// Initialize lists for advanced and ultimate upgrades as needed
			boolArrayadvancedConfig = new MelonPreferences_Entry<bool>[advancedUpgradesKeys.Length];
			for (int i = 0; i < advancedUpgradesKeys.Length; i++)
			{
				boolArrayadvancedConfig[i] = advancedCategory.CreateEntry(advancedUpgradesKeys[i], true);
			}

			boolArrayultimateConfig = new MelonPreferences_Entry<bool>[ultimateUpgradesKeys.Length];
			for (int i = 0; i < ultimateUpgradesKeys.Length; i++)
			{
				boolArrayultimateConfig[i] = ultimateCategory.CreateEntry(ultimateUpgradesKeys[i], true);
			}

			boolArraydebuffsConfig = new MelonPreferences_Entry<bool>[debuffsKeys.Length];
            for (int i = 0; i < debuffsKeys.Length; i++)
            {
				if (debuffsKeys[i].StartsWith("Skip_"))
				{
					continue;
				}
                boolArraydebuffsConfig[i] = debuffsCategory.CreateEntry(debuffsKeys[i], true);
            }

			MelonPreferences.Save();
		}

		public void ReloadConfig()
		{
			MelonPreferences.Load();
		}
	}
}