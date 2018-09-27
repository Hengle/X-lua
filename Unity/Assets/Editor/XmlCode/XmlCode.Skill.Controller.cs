using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class Controller : XmlCode.Skill.Timeline
	{
		/// <summary>
		/// 物体类型
		/// <summary>
		public XmlCode.Skill.SpawnType SpawnType;
		/// <summary>
		/// 资源对象路径
		/// <summary>
		public string Path;

		public override void Write(TextWriter _1)
		{
			Write(_1, "SpawnType", (int)this.SpawnType);
			Write(_1, "Path", this.Path);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "SpawnType": this.SpawnType = (XmlCode.Skill.SpawnType)ReadInt(_2); break;
				case "Path": this.Path = ReadString(_2); break;
			}
		}
	}
}
