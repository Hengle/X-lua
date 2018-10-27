using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class Sequence : CfgObject
	{
		/// <summary>
		/// 序列ID
		/// <summary>
		public readonly int Id;
		/// <summary>
		/// 碰撞区域定义列表
		/// <summary>
		public readonly List<HitZone> HitZones = new List<HitZone>();
		/// <summary>
		/// 时间事件列表
		/// <summary>
		public readonly List<Timeline> Timelines = new List<Timeline>();

		public Sequence(DataStream data)
		{
			this.Id = data.GetInt();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.HitZones.Add((HitZone)data.GetObject(data.GetString()));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.Timelines.Add((Timeline)data.GetObject(data.GetString()));
			}
		}
	}
}
