using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace Cfg
{
	public  class CfgManager
	{
		/// <summary>
		/// 配置文件文件夹路径
		/// <summary>
		public static string ConfigDir;

		public static readonly Dictionary<int, Cfg.AllType.AllClass> AllClass = new Dictionary<int, Cfg.AllType.AllClass>();
		public static readonly Dictionary<string, Cfg.Character.Model> Model = new Dictionary<string, Cfg.Character.Model>();
		public static readonly Dictionary<string, Cfg.Skill.ActorConfig> ActorConfig = new Dictionary<string, Cfg.Skill.ActorConfig>();

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
			var models = Load(ConfigDir + "Character/Model.data", (d) => new Character.Model(d));
			models.ForEach(v => Model.Add(v.Name, v));
			var actorconfigs = Load(ConfigDir + "Skill/ActorConfig.data", (d) => new Skill.ActorConfig(d));
			actorconfigs.ForEach(v => ActorConfig.Add(v.ModelName, v));
		}

		public static void Clear()
		{
			AllClass.Clear();
			Model.Clear();
			ActorConfig.Clear();
		}

	}
}
