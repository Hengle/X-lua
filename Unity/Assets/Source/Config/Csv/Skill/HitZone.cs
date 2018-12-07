using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class HitZone : CfgObject
	{
		/// <summary>
		/// 打击区域id
		/// <summary>
		public readonly int Id;
		/// <summary>
		/// 打击范围的形态，0：方盒，1:圆柱,2:球
		/// <summary>
		public readonly int Sharp;
		/// <summary>
		/// 坐标偏移量
		/// <summary>
		public Cfg.Vector3 Offset;
		/// <summary>
		/// 最大数量
		/// <summary>
		public readonly int MaxNum;

		public HitZone(DataStream data)
		{
			this.Id = data.GetInt();
			this.Sharp = data.GetInt();
			this.Offset = new Vector3(data);
			this.MaxNum = data.GetInt();
		}
	}
}
