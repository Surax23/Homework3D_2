using UnityEngine;

namespace Geekbrains
{
	public class CreateWayPoint : MonoBehaviour
	{
		[SerializeField] private WayPoint _prefab;
		private Transform _rootWayPoint;

		public void InstantiateObj(Vector3 pos)
		{
			if (!_rootWayPoint)
			{
				_rootWayPoint = new GameObject("WayPoint").transform;
				_rootWayPoint.gameObject.AddComponent<PathBot>();
			}
			Instantiate(_prefab, pos, Quaternion.identity, _rootWayPoint);
		}

	}
}