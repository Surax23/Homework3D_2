using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class Main : MonoBehaviour
	{
		public FlashLightController FlashLightController { get; private set; }
		public InputController InputController { get; private set; }
		public PlayerController PlayerController { get; private set; }
		public WeaponController WeaponController { get; private set; }
		public ObjectManager ObjectManager { get; private set; }
		public Transform Player { get; private set; }
		public Transform MainCamera { get; private set; }
		private BaseController[] Controllers;

		public static Main Instance { get; private set; }

		private void Awake()
		{
			Instance = this;

			MainCamera = Camera.main.transform;
			Player = GameObject.FindGameObjectWithTag("Player").transform;

			ObjectManager = new ObjectManager();
			ObjectManager.Start();
			
			PlayerController = new PlayerController(new UnitMotor(
				GameObject.FindObjectOfType<CharacterController>().transform));
			PlayerController.On();
			FlashLightController = new FlashLightController();
			InputController = new InputController();
			InputController.On();
			WeaponController = new WeaponController();
			Controllers = new BaseController[4];
			Controllers[0] = FlashLightController;
			Controllers[1] = InputController;
			Controllers[2] = PlayerController;
			Controllers[3] = WeaponController;
		}

		private void Update()
		{
			for (var index = 0; index < Controllers.Length; index++)
			{
				var controller = Controllers[index];
				controller.OnUpdate();
			}
		}
		private void OnGUI()
		{
			GUI.Label(new Rect(0, 0, 100, 100), $"{1 / Time.deltaTime:0.0}");
		}
	}
}