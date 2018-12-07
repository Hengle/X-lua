using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class SphereZone : Cfg.Skill.HitZone
	{
		/// <summary>
		/// 球半径
		/// <summary>
		public readonly float Radius;

		public SphereZone(DataStream data) : base(data)
		{
			this.Radius = data.GetFloat();
		}
	}
}
