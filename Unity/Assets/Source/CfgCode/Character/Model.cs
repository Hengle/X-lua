using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Character
{
	public  class Model : CfgObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public readonly string Name;
		/// <summary>
		/// 模型分组类型
		/// <summary>
		public readonly int GroupType;
		/// <summary>
		/// 模型级别
		/// <summary>
		public readonly int Level;

		public Model(DataStream data)
		{
			this.Name = data.GetString();
			this.GroupType = data.GetInt();
			this.Level = data.GetInt();
		}
	}
}
