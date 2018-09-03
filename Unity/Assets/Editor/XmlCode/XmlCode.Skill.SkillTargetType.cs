using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public enum SkillTargetType
	{
		/// <summary>
		/// 敌方目标
		/// <summary>
		Enemy = 0,
		/// <summary>
		/// 己方目标
		/// <summary>
		Teammate = 1,
		/// <summary>
		/// 自己
		/// <summary>
		Self = 2,
	}
}
