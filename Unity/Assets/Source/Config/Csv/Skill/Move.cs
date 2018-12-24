using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class Move : Cfg.Skill.Controller
	{
		/// <summary>
		/// 移动方式:0向目标移动 1按指定方向移动
		/// <summary>
		public readonly int Type;
		/// <summary>
		/// 是否相对于自己移动
		/// <summary>
		public readonly bool IsRelateSelf;
		/// <summary>
		/// 起始位置相对目标偏移
		/// <summary>
		public Cfg.Vector3 Offset;
		/// <summary>
		/// Y轴顺时针旋转角度
		/// <summary>
		public readonly float Angle;
		/// <summary>
		/// 位移速度m/s
		/// <summary>
		public readonly float Speed;

		public Move(DataStream data) : base(data)
		{
			this.Type = data.GetInt();
			this.IsRelateSelf = data.GetBool();
			this.Offset = new Vector3(data);
			this.Angle = data.GetFloat();
			this.Speed = data.GetFloat();
		}
	}
}
