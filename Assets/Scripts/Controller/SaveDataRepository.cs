using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class SaveDataRepository
	{
		private IData<SerializableGameObject> _data;
        private List<SerializableGameObject> _save = new List<SerializableGameObject>();

        // Не знаю, как лучше организовать это для сохранения.
        // И вообще я тут напортачил немножко. -(
        List<Bot> _bots;


        private string _folderName = "dataSave";
		private string _fileName = "data.bat";
		private string _path;

		public SaveDataRepository()
		{
            _data = new JsonData<SerializableGameObject>();

            //if (Application.platform == RuntimePlatform.WebGLPlayer)
            //{
            //	//_data = new XMLData();
            //}
            //else
            //{
            //	_data = new List<JsonData<SerializableGameObject>>();
            //}
            _path = Path.Combine(Application.dataPath, _folderName);
		}

		public void Save()
		{
            _bots = Main.Instance.BotController.GetBotList;
            if (!Directory.Exists(Path.Combine(_path)))
			{
				Directory.CreateDirectory(_path);
			}
			var player = new SerializableGameObject
			{
				Pos = Main.Instance.Player.position,
				Name = "Roman the Greatest",
				IsEnable = true
			};
            _save.Add(player);
            foreach (Bot b in _bots)
            {
                _save.Add(new SerializableGameObject()
                {
                    IsEnable = b.enabled,
                    Name = b.name,
                    Pos = b.transform.position,
                    Rot = b.transform.rotation,
                    Scale = b.transform.localScale
                });
            }
            _data.Save(_save, Path.Combine(_path, _fileName));
		}

		public void Load()
		{
			var file = Path.Combine(_path, _fileName);
			if (!File.Exists(file)) return;
			var newLoad = _data.Load(file);
			//Main.Instance.Player.position = newPlayer.Pos;
			//Main.Instance.Player.name = newPlayer.Name;
			//Main.Instance.Player.gameObject.SetActive(newPlayer.IsEnable);

			Debug.Log(newLoad);
		}
	}
}