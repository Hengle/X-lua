using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public enum SpawnType
	{
		/// <summary>
		/// 需要加载的资源对象
		/// <summary>
		LoadObject = 0,
		/// <summary>
		/// 已存在的游戏对象
		/// <summary>
		InstanceObj = 1,
	}
}
