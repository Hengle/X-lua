using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class Active : Cfg.Skill.Controller
	{
		/// <summary>
		/// 对象激活隐藏控制
		/// <summary>
		public readonly bool Enable;

		public Active(DataStream data) : base(data)
		{
			this.Enable = data.GetBool();
		}
	}
}
