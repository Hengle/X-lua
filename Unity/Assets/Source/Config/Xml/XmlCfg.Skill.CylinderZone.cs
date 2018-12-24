using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class CylinderZone : XmlCfg.Skill.HitZone
	{
		/// <summary>
		/// 圆半径
		/// <summary>
		public float Radius;
		/// <summary>
		/// 圆柱高度
		/// <summary>
		public float Height;
		/// <summary>
		/// 打击区域绕y轴旋转角度（顺时针:左手定则）,构成扇形
		/// <summary>
		public float Angle;

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "Radius", this.Radius);
			Write(_1, "Height", this.Height);
			Write(_1, "Angle", this.Angle);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Radius": this.Radius = ReadFloat(_2); break;
				case "Height": this.Height = ReadFloat(_2); break;
				case "Angle": this.Angle = ReadFloat(_2); break;
			}
		}
	}
}
