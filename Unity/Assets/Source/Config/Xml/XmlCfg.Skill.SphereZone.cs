using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class SphereZone : XmlCfg.Skill.HitZone
	{
		/// <summary>
		/// 球半径
		/// <summary>
		public float Radius;

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "Radius", this.Radius);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Radius": this.Radius = ReadFloat(_2); break;
			}
		}
	}
}
