using UnityEngine;

namespace Geekbrains
{
	public sealed class FlashLightModel : BaseObjectScene
	{
		private Light _light;
		private Transform _goFollow;
		private Vector3 _vecOffset;
		public float BatteryChargeCurrent { get; private set; }
		[SerializeField] private float _speed = 10;
		[SerializeField] private float _batteryChargeMax = 50;
		[SerializeField] private float _intensity = 1.5f;
		private float _share;
		private float _takeAwayTheIntensity;

		protected override void Awake()
		{
			base.Awake();
			_light = GetComponent<Light>();

			_goFollow = Camera.main.transform;
			_vecOffset = transform.position - _goFollow.position;
			BatteryChargeCurrent = _batteryChargeMax;
			_light.intensity = _intensity;
			_share = _batteryChargeMax / 4;
			_takeAwayTheIntensity = _intensity / (_batteryChargeMax * 100);
		}

		public void Switch(bool value)
		{
			_light.enabled = value;
			if (!value) return;
			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = _goFollow.rotation;
		}

		public void Rotation()
		{
			if (!_light) return;

			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = Quaternion.Lerp(transform.rotation, _goFollow.rotation, _speed * Time.deltaTime);
		}

		public bool EditBatteryCharge()
		{
			if (BatteryChargeCurrent > 0)
			{
				BatteryChargeCurrent -= Time.deltaTime;

				if (BatteryChargeCurrent < _share)
				{
					_light.enabled = Random.Range(0, 100) >= Random.Range(0, 10);
				}
				else
				{
					_light.intensity -= _takeAwayTheIntensity;
				}
				return true;
			}

			return false;
		}
	}
}