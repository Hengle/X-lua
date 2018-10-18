using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg
{
	public  class Vector2 : XmlObject
	{
		/// <summary>
		/// 
		/// <summary>
		public float X;
		/// <summary>
		/// 
		/// <summary>
		public float Y;

		public override void Write(TextWriter _1)
		{
			Write(_1, "X", this.X);
			Write(_1, "Y", this.Y);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "X": this.X = ReadFloat(_2); break;
				case "Y": this.Y = ReadFloat(_2); break;
			}
		}
	}
}
