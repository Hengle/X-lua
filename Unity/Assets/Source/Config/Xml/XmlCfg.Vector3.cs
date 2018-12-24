using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg
{
	public class Vector3 : XmlObject
	{
		/// <summary>
		/// 
		/// <summary>
		public float X;
		/// <summary>
		/// 
		/// <summary>
		public float Y;
		/// <summary>
		/// 
		/// <summary>
		public float Z;

		public override void Write(TextWriter _1)
		{
			Write(_1, "X", this.X);
			Write(_1, "Y", this.Y);
			Write(_1, "Z", this.Z);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "X": this.X = ReadFloat(_2); break;
				case "Y": this.Y = ReadFloat(_2); break;
				case "Z": this.Z = ReadFloat(_2); break;
			}
		}
	}
}
