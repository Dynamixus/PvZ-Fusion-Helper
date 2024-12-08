using Il2Cpp;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using System.Linq;
using System.Reflection;

[assembly: MelonInfo(typeof(PlantConveyor.Core), PlantConveyor.AssemblyInfo.MODE_NAME, PlantConveyor.AssemblyInfo.VERSION, PlantConveyor.AssemblyInfo.AUTHOR)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]
[assembly: AssemblyVersion(PlantConveyor.AssemblyInfo.VERSION)]
[assembly: AssemblyFileVersion(PlantConveyor.AssemblyInfo.VERSION)]

namespace PlantConveyor
{
	public static class AssemblyInfo
	{
		public const string MODE_NAME = nameof(PlantConveyor);
		public const string VERSION = "1.2.0";
		public const string AUTHOR = "Dynaslash & Climeron";
	}

	public class Core : MelonMod
    {
		public static Core Instance { get; private set; }
		public int FirstCardIndex { get; private set; }
		public List<int> PlantIDs { get; internal set; }
		public enum SidesEnum { Right = -1, None = 0, Left = 1}
		private readonly float _switchTimeout = 0.1f;
		public float SwitchTimeout => _switchTimeout;
		public float Countdown { get; private set; }

		public override void OnInitializeMelon()
		{
			base.OnInitializeMelon();
			Instance = this;
		}
		public override void OnUpdate()
		{
			base.OnUpdate();
			CheckForCDReset();
			CheckForPlantReplacing();
		}
		private void CheckForPlantReplacing()
		{
			if (!DecreaseCountdownTimer() || !Board.Instance || !InGameUIMgr.Instance || InGameUIMgr.Instance.cardOnBank == null)
				return;
			SidesEnum side = GetSideByKeyHolding();
			if (side != SidesEnum.None)
			{
				Countdown = SwitchTimeout;
				FirstCardIndex += (int)side;
				GameObject[] cardsArray = InGameUIMgr.Instance.cardOnBank.Where(gameObject => gameObject).ToArray();
				for (int i = 0; i < cardsArray.Length; i++)
					SetPlantCard(cardsArray, i);
			}
		}
		/// <summary>Decreases the <see cref="Countdown"/> by <see cref="Time.deltaTime"/>.</summary>
		/// <returns>true - if the counter was set to 0; false - otherwise.</returns>
		private bool DecreaseCountdownTimer()
		{
			if (Countdown > 0)
				Countdown = Countdown < Time.deltaTime ? 0 : Countdown - Time.deltaTime;
			return Countdown == 0;
		}
		private SidesEnum GetSideByKeyHolding()
        {
			int sideIndex = 0;
			if (Input.GetKey(KeyCode.LeftArrow))
				sideIndex++;
			if (Input.GetKey(KeyCode.RightArrow))
				sideIndex--;
			return (SidesEnum)sideIndex;
		}
		private void SetPlantCard(GameObject[] cardsArray, int index)
        {
			int plantIndex = ((FirstCardIndex + index) % PlantIDs.Count + PlantIDs.Count) % PlantIDs.Count;
			CardUI component = cardsArray[index].GetComponent<CardUI>();
			Mouse.Instance.ChangeCardSprite((MixData.PlantType)PlantIDs[plantIndex], component);
			component.theSeedType = PlantIDs[plantIndex];
			component.theSeedCost = 0;
		}
		private void CheckForCDReset()
        {
			if (Input.GetKey(KeyCode.DownArrow) && InGameUIMgr.Instance && InGameUIMgr.Instance.cardOnBank != null)
			{
				InGameUIMgr.Instance.cardOnBank
					.Where(gameObject => gameObject)
					.Select(gameObject => gameObject.GetComponent<CardUI>())
					.ToList()
					.ForEach(card => card.CD = card.fullCD);
			}
		}
	}
}
