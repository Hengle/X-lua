using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class CubeZone : XmlCfg.Skill.HitZone
	{
		/// <summary>
		/// 方盒缩放大小
		/// <summary>
		public XmlCfg.Vector3 Scale;

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "Scale", this.Scale);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Scale": this.Scale = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
			}
		}
	}
}
