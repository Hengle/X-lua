using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class Timeline : CfgObject
	{
		/// <summary>
		/// 起始帧
		/// <summary>
		public readonly int Start;
		/// <summary>
		/// 结束帧
		/// <summary>
		public readonly int End;

		public Timeline(DataStream data)
		{
			this.Start = data.GetInt();
			this.End = data.GetInt();
		}
	}
}
