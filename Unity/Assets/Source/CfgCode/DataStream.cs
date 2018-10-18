using System;
using System.Text;
using System.IO;
using System.Reflection;
namespace Cfg
{
	public  class DataStream
	{
		public DataStream(string path, Encoding encoding)
		{
			_rows = File.ReadAllLines(path, encoding);
			_rIndex = _cIndex = 0;
			if (_rows.Length > 0)
			_columns = _rows[_rIndex].Split("▃".ToCharArray(),  StringSplitOptions.RemoveEmptyEntries);
		}

		public int Count { get { return _rows.Length; } }

		public int GetInt()
		{
			int result;
			int.TryParse(Next(), out result);
			return result;
		}
		public long GetLong()
		{
			long result;
			long.TryParse(Next(), out result);
			return result;
		}
		public float GetFloat()
		{
			float result;
			float.TryParse(Next(), out result);
			return result;
		}
		public bool GetBool()
		{
			string v = Next();
			if (string.IsNullOrEmpty(v)) return false;
			return !v.Equals("0");
		}
		public string GetString()
		{
			return Next();
		}
		/// <summary>
		/// 支持多态,直接反射类型
		/// <summary>
		public CfgObject GetObject(string fullName)
		{
			Type type = Type.GetType(fullName);
			if (type == null)
			{
				UnityEngine.Debug.LogErrorFormat("DataStream 解析{0}类型失败!", fullName);
			}
			return (CfgObject)Activator.CreateInstance(type, new object[] { this });
		}


		private int _rIndex;
		private int _cIndex;
		private string[] _rows;
		private string[] _columns;

		private void NextRow()
		{
			if (_rIndex >= _rows.Length) return;
			_rIndex++;
			_cIndex = 0;
			_columns = _rows[_rIndex].Split("▃".ToCharArray(),  StringSplitOptions.RemoveEmptyEntries);
		}

		private string Next()
		{
			if (_cIndex >= _columns.Length) NextRow();
			return _columns[_cIndex++];
		}

	}
}
