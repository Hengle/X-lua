using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public  class PlaySound : XmlCfg.Skill.Timeline
	{
		/// <summary>
		/// 音效资源路径
		/// <summary>
		public string Path = "";
		/// <summary>
		/// 音量
		/// <summary>
		public float Volume;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Path", this.Path);
			Write(_1, "Volume", this.Volume);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Path": this.Path = ReadString(_2); break;
				case "Volume": this.Volume = ReadFloat(_2); break;
			}
		}
	}
}
