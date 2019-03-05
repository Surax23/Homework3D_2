using System.Collections.Generic;

namespace Geekbrains
{
	public interface IData<T>
	{
		void Save(List<T> data,  string path = null);
        List<T> Load(string path = null);
	}
}