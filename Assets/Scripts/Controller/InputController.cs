using UnityEngine;

namespace Geekbrains
{
	public class InputController : BaseController
	{

		private KeyCode _activeFlashLight = KeyCode.F;
		private KeyCode _cancel = KeyCode.Escape;
		private KeyCode _reloadClip = KeyCode.R;
		private int _indexWeapons;

		public InputController()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		public override void OnUpdate()
		{
			if (!IsActive) return;
			if (Input.GetKeyDown(_activeFlashLight))
			{
				Main.Instance.FlashLightController.Switch();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				if (_indexWeapons < Main.Instance.ObjectManager.Weapons.Length - 1)
				{
					_indexWeapons++;
				}
				else
				{
					_indexWeapons = -1;
				}
				SelectWeapon(_indexWeapons);
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				if (_indexWeapons <= 0)
				{
					_indexWeapons = Main.Instance.ObjectManager.Weapons.Length;
				}
				else
				{
					_indexWeapons--;
				}
				SelectWeapon(_indexWeapons);
			}

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SelectWeapon(0);
			}

			if (Input.GetKeyDown(_cancel))
			{
				Main.Instance.WeaponController.Off();
				Main.Instance.FlashLightController.Off();
			}

			if (Input.GetKeyDown(_reloadClip))
			{
				Main.Instance.WeaponController.ReloadClip();
			}
		}

		private void SelectWeapon(int i)
		{
			Main.Instance.WeaponController.Off();
			if (i < 0 || i >= Main.Instance.ObjectManager.Weapons.Length) return;
			var tempWeapon = Main.Instance.ObjectManager.Weapons[i];
			if (tempWeapon != null) Main.Instance.WeaponController.On(tempWeapon);
		}
	}
}