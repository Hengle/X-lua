using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class ReplaceObject : XmlCfg.Skill.Controller
	{
		/// <summary>
		/// 新对象
		/// <summary>
		public string NewObject = "";
		/// <summary>
		/// 相对位置
		/// <summary>
		public XmlCfg.Vector3 Offset;
		/// <summary>
		/// 相对旋转
		/// <summary>
		public XmlCfg.Vector3 EulerAngles;

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "NewObject", this.NewObject);
			Write(_1, "Offset", this.Offset);
			Write(_1, "EulerAngles", this.EulerAngles);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "NewObject": this.NewObject = ReadString(_2); break;
				case "Offset": this.Offset = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
				case "EulerAngles": this.EulerAngles = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
			}
		}
	}
}
