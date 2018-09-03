using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace Csv
{
	public  class CfgManager
	{
		/// <summary>
		/// 配置文件文件夹路径
		/// <summary>
		public static string ConfigDir;

		public static readonly Dictionary<int, Csv.AllType.AllClass> AllClass = new Dictionary<int, Csv.AllType.AllClass>();
		public static readonly Dictionary<string, Csv.Skill.ModelActions> ModelActions = new Dictionary<string, Csv.Skill.ModelActions>();

		/// <summary>
		/// constructor参数为指定类型的构造函数
		/// <summary>
		public static List<T> Load<T>(string path, Func<DataStream, T> constructor)
		{
			if (!File.Exists(path))
			{
				UnityEngine.Debug.LogError(path + "配置路径不存在");
				return new List<T>();
			}
			DataStream data = new DataStream(path, Encoding.UTF8);
			List<T> list = new List<T>();
			for (int i = 0; i < data.Count; i++)
			{
				list.Add(constructor(data));
			}
			return list;
		}

		public static void LoadAll()
		{
			var allclasss = Load(ConfigDir + "AllType/AllClass.data", (d) => new AllType.AllClass(d));
			allclasss.ForEach(v => AllClass.Add(v.ID, v));
			var modelactionss = Load(ConfigDir + "Skill/ModelActions.data", (d) => new Skill.ModelActions(d));
			modelactionss.ForEach(v => ModelActions.Add(v.ModelName, v));
		}

		public static void Clear()
		{
			AllClass.Clear();
			ModelActions.Clear();
		}

	}
}
