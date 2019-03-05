using UnityEngine;

public class TrailController : MonoBehaviour
{
	private Camera _camera;

	private void Start()
	{
		_camera = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Plane plane = new Plane(_camera.transform.forward*-1, transform.position);

			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
			if (plane.Raycast(ray, out var dist))
			{
				transform.position = ray.GetPoint(dist);
			}
		}
	}
}