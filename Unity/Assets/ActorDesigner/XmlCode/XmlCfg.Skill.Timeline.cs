using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public  class Timeline : XmlObject
	{
		/// <summary>
		/// 起始帧
		/// <summary>
		public int Start;
		/// <summary>
		/// 结束帧
		/// <summary>
		public int End;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Start", this.Start);
			Write(_1, "End", this.End);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Start": this.Start = ReadInt(_2); break;
				case "End": this.End = ReadInt(_2); break;
			}
		}
	}
}
