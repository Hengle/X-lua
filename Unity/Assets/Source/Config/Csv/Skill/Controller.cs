using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class Controller : Cfg.Skill.Timeline
	{
		/// <summary>
		/// 资源对象路径
		/// <summary>
		public readonly string Path;

		public Controller(DataStream data) : base(data)
		{
			this.Path = data.GetString();
		}
	}
}
