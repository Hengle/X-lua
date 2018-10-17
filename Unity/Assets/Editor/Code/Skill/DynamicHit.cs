using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public  class DynamicHit : Cfg.Skill.StaticHit
	{
		/// <summary>
		/// 碰撞体绑定对象路径
		/// <summary>
		public readonly string Target;

		public DynamicHit(DataStream data) : base(data)
		{
			this.Target = data.GetString();
		}
	}
}
