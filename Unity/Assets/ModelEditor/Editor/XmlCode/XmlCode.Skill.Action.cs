using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class Action : XmlObject
	{
		/// <summary>
		/// 时间点
		/// <summary>
		public float Timeline;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Timeline", this.Timeline);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Timeline": this.Timeline = ReadFloat(_2); break;
			}
		}
	}
}
