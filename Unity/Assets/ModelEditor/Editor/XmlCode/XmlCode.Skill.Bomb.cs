using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class Bomb : XmlCode.Skill.TraceObject
	{
		/// <summary>
		/// 打击点组(HitPointGroup)ID
		/// <summary>
		public int Id;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
			}
		}
	}
}
