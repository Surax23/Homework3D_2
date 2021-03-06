﻿using UnityEngine;

namespace Geekbrains
{
	public class InputController : BaseController
	{

		private KeyCode _activeFlashLight = KeyCode.F;
		private KeyCode _cancel = KeyCode.Escape;
		private KeyCode _reloadClip = KeyCode.R;
        private int _selectedWeapon = 0;

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
			// реализовать выбор оружия по колесику мыши

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
                SelectWeapon(0);
			}

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectWeapon(1);
            }

            var d = Input.GetAxis("Mouse ScrollWheel");
            if (d > 0f)
            {
                if (_selectedWeapon > 0)
                    _selectedWeapon--;
                else
                    _selectedWeapon = 0;
                SelectWeapon(_selectedWeapon);
            }
            else if (d < 0f)
            {
                if (_selectedWeapon < 1)
                    _selectedWeapon++;
                else
                    _selectedWeapon = 1;
                SelectWeapon(_selectedWeapon);
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
            _selectedWeapon = i;
            Main.Instance.WeaponController.Off();
			var tempWeapon = Main.Instance.ObjectManager.Weapons[i];
			if (tempWeapon != null) Main.Instance.WeaponController.On(tempWeapon);
		}
	}
}