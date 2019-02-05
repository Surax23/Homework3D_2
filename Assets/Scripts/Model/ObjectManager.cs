using UnityEngine;

namespace Geekbrains
{
	public class ObjectManager
	{
		private Weapon[] _weapons = new Weapon[5];

		public Weapon[] Weapons => _weapons;

		public FlashLightModel FlashLight { get; private set; }

		public void Start()
		{
			_weapons = Main.Instance.Player.GetComponentsInChildren<Weapon>();

			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

			FlashLight = MonoBehaviour.FindObjectOfType<FlashLightModel>();
			FlashLight.Switch(false);
		}

		// Добавить функционал
	}
}