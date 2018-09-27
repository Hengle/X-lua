using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class ReplaceObject : XmlCode.Skill.Controller
	{
		/// <summary>
		/// 新对象
		/// <summary>
		public string NewObject;

		public override void Write(TextWriter _1)
		{
			Write(_1, "NewObject", this.NewObject);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "NewObject": this.NewObject = ReadString(_2); break;
			}
		}
	}
}
