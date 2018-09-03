using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class TraceObject : Csv.Skill.SpawnObject
	{
		/// <summary>
		/// 身体矫正值
		/// <summary>
		public const float BODY_CORRECT = 0.7f;
		/// <summary>
		/// 头部矫正值
		/// <summary>
		public const float HEAD_CORRECT = 1.3f;
		/// <summary>
		/// 特效ID
		/// <summary>
		public readonly int EffectId;
		/// <summary>
		/// 是否追踪目标
		/// <summary>
		public readonly bool IsTraceTarget;
		/// <summary>
		/// 飞行参数ID,数据有配置表
		/// <summary>
		public readonly int TraceCurveId;
		/// <summary>
		/// 目标偏移X
		/// <summary>
		public readonly float OffsetX;
		/// <summary>
		/// 目标偏移Y
		/// <summary>
		public readonly float OffsetY;
		/// <summary>
		/// 目标偏移Z
		/// <summary>
		public readonly float OffsetZ;
		/// <summary>
		/// 追踪类型
		/// <summary>
		public readonly int TraceType;
		/// <summary>
		/// 释放者绑定位置
		/// <summary>
		public readonly int CasterBindType;
		/// <summary>
		/// 被击者绑定位置
		/// <summary>
		public readonly int TargetBindType;

		public TraceObject(DataStream data) : base(data)
		{
			this.EffectId = data.GetInt();
			this.IsTraceTarget = data.GetBool();
			this.TraceCurveId = data.GetInt();
			this.OffsetX = data.GetFloat();
			this.OffsetY = data.GetFloat();
			this.OffsetZ = data.GetFloat();
			this.TraceType = data.GetInt();
			this.CasterBindType = data.GetInt();
			this.TargetBindType = data.GetInt();
		}
	}
}
