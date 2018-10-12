using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public  class Active : XmlCfg.Skill.Controller
	{
		/// <summary>
		/// 对象激活隐藏控制
		/// <summary>
		public bool Enable;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Enable", this.Enable);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Enable": this.Enable = ReadBool(_2); break;
			}
		}
	}
}
