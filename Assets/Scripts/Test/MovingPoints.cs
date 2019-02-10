using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
	public class MovingPoints : MonoBehaviour
	{
		[SerializeField] private Bot _agent;
		[SerializeField] private Transform _point;
		private readonly Queue<Vector3> _points = new Queue<Vector3>();
		private readonly Color _c1 = Color.red;
		private readonly Color _c = Color.blue;
		private LineRenderer _lineRenderer;
		private Camera _mainCamera;

		private NavMeshPath _path;
		private Vector3 _startPoint;

		private void Start()
		{
			var lineRendererGo = new GameObject("LineRenderer");
			_lineRenderer = lineRendererGo.AddComponent<LineRenderer>();
			_lineRenderer.startWidth = 0.5F;
			_lineRenderer.endWidth = 0.2F;
			_lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));

			_lineRenderer.startColor = _c;
			_lineRenderer.endColor = _c1;
			_startPoint = _agent.transform.position;
			_path = new NavMeshPath();

			_mainCamera = Camera.main;
		}
		private void Update()
		{
			if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
			{
				if (Input.GetMouseButtonDown(0))
				{
					DrawPoint(hit.point);
				}

				if (Time.frameCount % 2 == 0)
				{
					NavMesh.CalculatePath(_startPoint, hit.point, NavMesh.AllAreas, _path);
				}
				else
				{
					var cornersArray = _points.ToArray().Concat(_path.corners);
					
					_lineRenderer.positionCount = cornersArray.Length;
					_lineRenderer.SetPositions(cornersArray);
				}
			}

			if (_isMove)
			{
				if (_points.Count <= 0) return;
				if (!_agent.Agent.hasPath)
				{
					var point = _points.Dequeue();
					_agent.MovePoint(point);
				}
			}
		}

		private void DrawPoint(Vector3 position)
		{
			var point = Instantiate(_point, position, Quaternion.identity);
			point.GetComponent<DestroyPoint>().OnFinishChange += MovePoint;
			_points.Enqueue(point.position);
			_startPoint = point.position;
			MovePoint();
		}

		private bool _isMove;
		private void MovePoint()
		{
			_isMove = true;
		}
	}
}