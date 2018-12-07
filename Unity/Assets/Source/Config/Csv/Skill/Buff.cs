using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class Buff : Cfg.Skill.PlayParticle
	{
		/// <summary>
		/// Buff参数集合ID
		/// <summary>
		public readonly int Id;

		public Buff(DataStream data) : base(data)
		{
			this.Id = data.GetInt();
		}
	}
}
