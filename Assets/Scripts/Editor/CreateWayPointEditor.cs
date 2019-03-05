#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace Geekbrains.Editor
{
	[CustomEditor(typeof(CreateWayPoint))]
	public class CreateWayPointEditor : UnityEditor.Editor
	{
		private CreateWayPoint _testTarget;

		private void OnEnable()
		{
			_testTarget = (CreateWayPoint)target;
		}

		private void OnSceneGUI()
		{
			if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
			{
				Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
					SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

				if (Physics.Raycast(ray, out var hit))
				{
					_testTarget.InstantiateObj(hit.point);
					EditorSceneManager.MarkSceneDirty(_testTarget.gameObject.scene);
				}
			}

			Selection.activeGameObject = FindObjectOfType<CreateWayPoint>().gameObject;

			SetObjectDirty(_testTarget.gameObject);
		}

		private void SetObjectDirty(GameObject obj)
		{
			if (GUI.changed && !Application.isPlaying)
			{
				EditorUtility.SetDirty(obj);
				EditorSceneManager.MarkSceneDirty(obj.scene);
			}
		}
	}
#endif
}