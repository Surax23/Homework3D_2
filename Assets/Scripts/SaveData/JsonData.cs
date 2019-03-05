using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class JsonData<T> : IData<T>
	{
		public void Save(List<T> data, string path = null)
		{
			//if(!typeof(T).IsSerializable) return;
			
			var str = JsonUtility.ToJson(data);
			File.WriteAllText(path, str);
		}

		public List<T> Load(string path = null)
		{
			var str = File.ReadAllText(path);
			return JsonUtility.FromJson<List<T>>(str);
		}
	}
}