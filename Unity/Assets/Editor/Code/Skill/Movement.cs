using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class Movement : Csv.Skill.Action
	{
		/// <summary>
		/// 移动方式
		/// <summary>
		public readonly int Type;
		/// <summary>
		/// 持续时间
		/// <summary>
		public readonly float Duration;
		/// <summary>
		/// 速度
		/// <summary>
		public readonly float Speed;
		/// <summary>
		/// 加速度
		/// <summary>
		public readonly float Acceleration;

		public Movement(DataStream data) : base(data)
		{
			this.Type = data.GetInt();
			this.Duration = data.GetFloat();
			this.Speed = data.GetFloat();
			this.Acceleration = data.GetFloat();
		}
	}
}
