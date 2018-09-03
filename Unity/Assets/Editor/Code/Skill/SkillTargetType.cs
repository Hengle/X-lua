using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public sealed class SkillTargetType
	{
		/// <summary>
		/// 敌方目标
		/// <summary>
		public const int Enemy = 0;
		/// <summary>
		/// 己方目标
		/// <summary>
		public const int Teammate = 1;
		/// <summary>
		/// 自己
		/// <summary>
		public const int Self = 2;
	}
}
