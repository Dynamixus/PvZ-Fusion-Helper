using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Better_Pumpkin_Fusion.Core), "Better Pumpkin Fusion", "231.0.0", "dynaslash & TuanAnh2901", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Better_Pumpkin_Fusion
{
    public class Core : MelonMod
    {
        private Dictionary<int, int> plantMixDictionary = new Dictionary<int, int>
        {
            { 20, 1087 }, // Plantern
            { 21, 1088 }, // Cactus
            { 22, 1091 }, // Blover
            { 23, 1089 }, // Starfruit
            { 25, 1092 }, // Magnet-shroom
            { 2, 1164 }, // Cherry Bomb
            { 4, 1190 }, // Potato Mine
        };

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Better Pumpkin Fusion is loaded!");
        }

        public override void OnUpdate()
        {
            if (Board.Instance != null && Mouse.Instance.theItemOnMouse != null && Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
            {
                TryFusion();
            }
        }

        private void TryFusion()
        {
            for (int i = 0; i < Board.Instance.plantArray.Count; i++)
            {
                var plant = Board.Instance.plantArray[i];
                if (plant != null && plant.thePlantColumn == Mouse.Instance.theMouseColumn && plant.thePlantRow == Mouse.Instance.theMouseRow)
                {
                    int targetPlantType = GetTargetPlantType(plant);

                    if (targetPlantType != 0)
                    {
                        if (CreatePlant.Instance.SetPlant(plant.thePlantColumn, plant.thePlantRow, (PlantType)targetPlantType, null, Vector2.zero, true, true) != null)
                        {
                            UpdateSunAndCooldowns();
                            plant.Die(0);
                        }
                    }
                    break;
                }
            }
        }

        private int GetTargetPlantType(Plant plant)
        {
            int plantTypeOnMouse = (int)Mouse.Instance.thePlantTypeOnMouse;

            if ((int)plant.thePlantType == 24)
                return GetMixData(plantTypeOnMouse);
            else if ((int)plant.thePlantType == 1110 && plantTypeOnMouse == 1102)
                return 911;
            else if ((int)plant.thePlantType == 1088 && plantTypeOnMouse == 22)
                return 1110;
            else if ((int)plant.thePlantType == 1091 && plantTypeOnMouse == 21)
                return 1110;
            else if ((int)plant.thePlantType == 911 && plantTypeOnMouse == 2)
                return 922;
            else if ((int)plant.thePlantType == 922 && plantTypeOnMouse == 21)
                return 911;
            else if ((int)plant.thePlantType == 1092 && plantTypeOnMouse == 25)
                return 935;

            return 0;
        }

        private int GetMixData(int plantTypeOnMouse)
        {
            return plantMixDictionary.TryGetValue(plantTypeOnMouse, out int mixPlantType) ? mixPlantType : 0;
        }

        private void UpdateSunAndCooldowns()
        {
            if (Mouse.Instance.thePlantOnGlove == null)
            {
                Board.Instance.theSun -= Mouse.Instance.theCardOnMouse.theSeedCost;
                Mouse.Instance.theCardOnMouse.CD = 0f;
                Mouse.Instance.theCardOnMouse.PutDown();
                UnityEngine.Object.Destroy(Mouse.Instance.theItemOnMouse);
                Mouse.Instance.ClearItemOnMouse(false);
            }
            else
            {
                Mouse.Instance.thePlantOnGlove.GetComponent<Plant>().Die(0);
                Mouse.Instance.thePlantOnGlove = null;
                GameObject.Find("Glove").GetComponent<GloveMgr>().CD = 0f;
                UnityEngine.Object.Destroy(Mouse.Instance.theItemOnMouse);
                Mouse.Instance.ClearItemOnMouse(true);
            }
        }
    }
}