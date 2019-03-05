using UnityEngine;

namespace Geekbrains
{
	public class MiniMap : MonoBehaviour
	{
		private Transform _player;
		private void Start()
		{
			_player = Camera.main.transform;
			transform.parent = null;
		}

		private void LateUpdate()
		{
			var newPosition = _player.position;
			newPosition.y = transform.position.y;
			transform.position = newPosition;
			transform.rotation = Quaternion.Euler(90, _player.eulerAngles.y, 0);
		}
	}

}