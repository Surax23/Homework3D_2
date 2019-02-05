using UnityEngine;

namespace Geekbrains
{
	public class FlashLightController : BaseController
	{
		private FlashLightModel _flashLight;
		
		public override void OnUpdate()
		{
			if (!IsActive) return;

			if (_flashLight == null)return;
			_flashLight.Rotation();
			if (_flashLight.EditBatteryCharge())
			{
				UiInterface.LightUiText.Text = _flashLight.BatteryChargeCurrent;
			}
			else
			{
				Off();
			}
		}

		public override void On()
		{
			if (IsActive)return;
			base.On();
			_flashLight = Main.Instance.ObjectManager.FlashLight;
			_flashLight.Switch(true);
			UiInterface.LightUiText.SetActive(true);
		}

		public sealed override void Off()
		{
			if (!IsActive) return;
			base.Off();
			_flashLight.Switch(false);
			_flashLight = null;
			UiInterface.LightUiText.SetActive(false);
		}
	}
}