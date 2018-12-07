using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Character
{
	public enum GroupType
	{
		/// <summary>
		/// 临时组
		/// <summary>
		None = 0,
		/// <summary>
		/// 基础类型
		/// <summary>
		Base = 1,
		/// <summary>
		/// 玩家
		/// <summary>
		Player = 2,
		/// <summary>
		/// 怪物
		/// <summary>
		Monster = 3,
		/// <summary>
		/// NPC
		/// <summary>
		NPC = 4,
	}
}
