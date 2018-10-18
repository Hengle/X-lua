using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public  class CubeZone : Cfg.Skill.HitZone
	{
		/// <summary>
		/// 方盒缩放大小
		/// <summary>
		public Cfg.Vector3 Scale;

		public CubeZone(DataStream data) : base(data)
		{
			this.Scale = (Vector3)data.GetObject(data.GetString());
		}
	}
}
