using UnityEngine;

namespace Geekbrains
{
	public class RadarObj : MonoBehaviour
	{
		[SerializeField] private UnityEngine.UI.Image _ico;
		private void OnDisable()
		{
			Radar.RemoveRadarObject(gameObject);
		}

		private void OnEnable()
		{
			Radar.RegisterRadarObject(gameObject, _ico);
		}
	}
}