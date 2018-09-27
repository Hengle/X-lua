using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class CubeZone : XmlCode.Skill.HitZone
	{
		/// <summary>
		/// 方盒缩放大小
		/// <summary>
		public XmlCode.Vector3 Scale;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Scale", this.Scale);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Scale": this.Scale = ReadObject<XmlCode.Vector3>(_2, "XmlCode.Vector3"); break;
			}
		}
	}
}
