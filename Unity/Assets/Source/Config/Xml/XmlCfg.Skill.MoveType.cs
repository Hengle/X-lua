using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public enum MoveType
	{
		/// <summary>
		/// 向目标移动
		/// <summary>
		MoveToTarget = 0,
		/// <summary>
		/// 按当前方向移动
		/// <summary>
		MoveInDirection = 1,
	}
}
