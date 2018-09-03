using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class SoundEffect : Csv.Skill.Action
	{
		/// <summary>
		/// 触发概率
		/// <summary>
		public readonly float Probability;
		/// <summary>
		/// 最小音量
		/// <summary>
		public readonly float VolumeMin;
		/// <summary>
		/// 最大音量
		/// <summary>
		public readonly float VolumeMax;
		/// <summary>
		/// 音效资源路径列表
		/// <summary>
		public readonly List<string> PathList = new List<string>();

		public SoundEffect(DataStream data) : base(data)
		{
			this.Probability = data.GetFloat();
			this.VolumeMin = data.GetFloat();
			this.VolumeMax = data.GetFloat();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.PathList.Add(data.GetString());
			}
		}
	}
}
