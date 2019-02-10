using System;
using UnityEngine;

namespace Geekbrains
{
	public class SelectionController : BaseController
	{
		private Camera _mainCamera;
		private Vector2 _center;
		private readonly float _dedicateDistance = 20;
		private GameObject _dedicatedObj;
		private ISelectObj _selectedObj;
		private bool _nullString;

		public SelectionController()
		{
			_mainCamera = Camera.main;
			_center = new Vector2(Screen.width / 2, Screen.height / 2);
		}

		public override void OnUpdate()
		{
			if (Physics.Raycast(_mainCamera.ScreenPointToRay(_center), out var hit, _dedicateDistance))
			{
				SelectObject(hit.collider.gameObject);
				_nullString = false;
			}
			else if(!_nullString)
			{
				UiInterface.SelectionObjMessageUi.Text = String.Empty;
				_nullString = true;
				_dedicatedObj = null;
			}
			if (_selectedObj != null) // оптимизировать
			{
				if (Input.GetKeyDown(_selectedObj.GetKeyCode()))
				{
					 // в инвентарь
				}
			}
		}

		private void SelectObject(GameObject obj)
		{
			if (obj == _dedicatedObj) return;
			_selectedObj = obj.GetComponent<ISelectObj>();
			UiInterface.SelectionObjMessageUi.Text = _selectedObj != null ? _selectedObj.GetMessage() : String.Empty;
			_dedicatedObj = obj;
		}
	}
}