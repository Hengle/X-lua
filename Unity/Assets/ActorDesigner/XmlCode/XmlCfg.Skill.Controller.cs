using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public  class Controller : XmlCfg.Skill.Timeline
	{
		/// <summary>
		/// 资源对象路径
		/// <summary>
		public string Path = "";

		public override void Write(TextWriter _1)
		{
			Write(_1, "Path", this.Path);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Path": this.Path = ReadString(_2); break;
			}
		}
	}
}
