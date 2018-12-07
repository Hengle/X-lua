using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class CylinderZone : Cfg.Skill.HitZone
	{
		/// <summary>
		/// 圆半径
		/// <summary>
		public readonly float Radius;
		/// <summary>
		/// 圆柱高度
		/// <summary>
		public readonly float Height;
		/// <summary>
		/// 打击区域绕y轴旋转角度（顺时针:左手定则）,构成扇形
		/// <summary>
		public readonly float Angle;

		public CylinderZone(DataStream data) : base(data)
		{
			this.Radius = data.GetFloat();
			this.Height = data.GetFloat();
			this.Angle = data.GetFloat();
		}
	}
}
