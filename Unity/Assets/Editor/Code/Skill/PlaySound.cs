using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public  class PlaySound : Cfg.Skill.Timeline
	{
		/// <summary>
		/// 音效资源路径
		/// <summary>
		public readonly string Path;
		/// <summary>
		/// 音量
		/// <summary>
		public readonly float Volume;

		public PlaySound(DataStream data) : base(data)
		{
			this.Path = data.GetString();
			this.Volume = data.GetFloat();
		}
	}
}
