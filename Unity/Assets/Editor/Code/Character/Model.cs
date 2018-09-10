using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Character
{
	public  class Model : CfgObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public readonly string Name;
		/// <summary>
		/// 模型级别
		/// <summary>
		public readonly int Level;

		public Model(DataStream data)
		{
			this.Name = data.GetString();
			this.Level = data.GetInt();
		}
	}
}
