using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class SoundEffect : XmlCode.Skill.Action
	{
		/// <summary>
		/// 触发概率
		/// <summary>
		public float Probability;
		/// <summary>
		/// 最小音量
		/// <summary>
		public float VolumeMin;
		/// <summary>
		/// 最大音量
		/// <summary>
		public float VolumeMax;
		/// <summary>
		/// 音效资源路径列表
		/// <summary>
		public List<string> PathList;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Probability", this.Probability);
			Write(_1, "VolumeMin", this.VolumeMin);
			Write(_1, "VolumeMax", this.VolumeMax);
			Write(_1, "PathList", this.PathList);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Probability": this.Probability = ReadFloat(_2); break;
				case "VolumeMin": this.VolumeMin = ReadFloat(_2); break;
				case "VolumeMax": this.VolumeMax = ReadFloat(_2); break;
				case "PathList": GetChilds(_2).ForEach (_3 => this.PathList.Add(ReadString(_3))); break;
			}
		}
	}
}
