using UnityEngine;

namespace Geekbrains
{
	public class UiInterface
	{
		private FlashLightUiText _flashLightUiText;

		public FlashLightUiText LightUiText
		{
			get
			{
				if (!_flashLightUiText)
					_flashLightUiText = MonoBehaviour.FindObjectOfType<FlashLightUiText>();
				return _flashLightUiText;
			}
		}

		private WeaponUiText _weaponUiText;

		public WeaponUiText WeaponUiText
		{
			get
			{
				if (!_weaponUiText)
					_weaponUiText = MonoBehaviour.FindObjectOfType<WeaponUiText>();
				return _weaponUiText;
			}
		}

		private SelectionObjMessageUi _selectionObjMessageUi;

		public SelectionObjMessageUi SelectionObjMessageUi
		{
			get
			{
				if (!_selectionObjMessageUi)
					_selectionObjMessageUi = MonoBehaviour.FindObjectOfType<SelectionObjMessageUi>();
				return _selectionObjMessageUi;
			}
		}
	}
}