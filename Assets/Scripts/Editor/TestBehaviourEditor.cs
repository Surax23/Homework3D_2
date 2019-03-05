using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Geekbrains.Editor
{
	[CustomEditor(typeof(TestBehaviour))]
	public class TestBehaviourEditor : UnityEditor.Editor
	{
		private bool _isPressButtonOk;
		private TestBehaviour _testTarget;

		private void OnEnable()
		{
			_testTarget = (TestBehaviour)target;
		}
		public override void OnInspectorGUI()
		{
			//DrawDefaultInspector();

			_testTarget.count = EditorGUILayout.IntSlider(_testTarget.count, 10, 50);
			_testTarget.offset = EditorGUILayout.IntSlider(_testTarget.offset, 10, 50);

			_testTarget.obj =
				EditorGUILayout.ObjectField("Объект который хотим вставить",
						_testTarget.obj, typeof(GameObject), false)
					as GameObject;

			var isPressButton = GUILayout.Button("Создание объектов по кнопке",
				EditorStyles.miniButtonLeft);

			_isPressButtonOk = GUILayout.Toggle(_isPressButtonOk, "Ok");

			if (isPressButton)
			{
				_testTarget.CreateObj();
				_isPressButtonOk = true;
			}

			if (_isPressButtonOk)
			{
				_testTarget.Test = EditorGUILayout.Slider(_testTarget.Test, 10, 50);
				EditorGUILayout.HelpBox("Вы нажали на кнопку", MessageType.Warning);
				if (GUILayout.Button("Add Com",
				EditorStyles.miniButtonLeft))
				{
					_testTarget.AddComponent();
				}
				if (GUILayout.Button("Rem Com",
					EditorStyles.miniButtonLeft))
				{
					_testTarget.RemoveComponent();
				}
			}

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

}