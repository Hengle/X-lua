using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public  class StaticHit : Cfg.Skill.Timeline
	{
		/// <summary>
		/// 碰撞区域
		/// <summary>
		public Cfg.Skill.HitZone Zone;
		/// <summary>
		/// 触发序列容器ID
		/// <summary>
		public readonly int SequeueID;

		public StaticHit(DataStream data) : base(data)
		{
			this.Zone = new Skill.HitZone(data);
			this.SequeueID = data.GetInt();
		}
	}
}
