using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class ShakeScreen : Cfg.Skill.Timeline
	{
		/// <summary>
		/// 震屏方式:0水平 1垂直 2混合
		/// <summary>
		public readonly int Type;
		/// <summary>
		/// 每秒震动的次数
		/// <summary>
		public readonly int Frequency;
		/// <summary>
		/// 初始频率维持时间
		/// <summary>
		public readonly float FrequencyDuration;
		/// <summary>
		/// 频率衰减
		/// <summary>
		public readonly float FrequencyAtten;
		/// <summary>
		/// 单次振幅
		/// <summary>
		public readonly float Amplitude;
		/// <summary>
		/// 单次震动的衰减幅度
		/// <summary>
		public readonly float AmplitudeAtten;
		/// <summary>
		/// 最小完整影响范围
		/// <summary>
		public readonly float MinRange;
		/// <summary>
		/// 最大影响范围
		/// <summary>
		public readonly float MaxRange;

		public ShakeScreen(DataStream data) : base(data)
		{
			this.Type = data.GetInt();
			this.Frequency = data.GetInt();
			this.FrequencyDuration = data.GetFloat();
			this.FrequencyAtten = data.GetFloat();
			this.Amplitude = data.GetFloat();
			this.AmplitudeAtten = data.GetFloat();
			this.MinRange = data.GetFloat();
			this.MaxRange = data.GetFloat();
		}
	}
}
