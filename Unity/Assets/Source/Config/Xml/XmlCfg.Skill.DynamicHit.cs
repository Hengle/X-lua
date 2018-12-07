using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class DynamicHit : XmlCfg.Skill.StaticHit
	{
		/// <summary>
		/// 碰撞体绑定对象路径
		/// <summary>
		public string Target = "";

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "Target", this.Target);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Target": this.Target = ReadString(_2); break;
			}
		}
	}
}
