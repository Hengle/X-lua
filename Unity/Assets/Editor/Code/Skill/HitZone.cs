using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class HitZone : CfgObject
	{
		/// <summary>
		/// 打击区域id
		/// <summary>
		public readonly int Id;
		/// <summary>
		/// 打击范围的形态，0：方盒，1:圆柱,2:三棱柱
		/// <summary>
		public readonly int Sharp;
		/// <summary>
		/// 打击范围的中心点在Z轴上的偏移量，向前为正
		/// <summary>
		public readonly float Zoffset;
		/// <summary>
		/// X方向长度
		/// <summary>
		public readonly float Xlength;
		/// <summary>
		/// 底边距地面的高度
		/// <summary>
		public readonly float BottomHeight;
		/// <summary>
		/// 顶部距地面的高度
		/// <summary>
		public readonly float TopHeight;
		/// <summary>
		/// Z方向长度
		/// <summary>
		public readonly float Zlength;
		/// <summary>
		/// 以y轴为中心的切面角度
		/// <summary>
		public readonly float YAngle;
		/// <summary>
		/// 打击区域绕y轴旋转角度（顺时针）,构成扇形
		/// <summary>
		public readonly float YRotation;
		/// <summary>
		/// 最大数量
		/// <summary>
		public readonly int MaxTarget;

		public HitZone(DataStream data)
		{
			this.Id = data.GetInt();
			this.Sharp = data.GetInt();
			this.Zoffset = data.GetFloat();
			this.Xlength = data.GetFloat();
			this.BottomHeight = data.GetFloat();
			this.TopHeight = data.GetFloat();
			this.Zlength = data.GetFloat();
			this.YAngle = data.GetFloat();
			this.YRotation = data.GetFloat();
			this.MaxTarget = data.GetInt();
		}
	}
}
