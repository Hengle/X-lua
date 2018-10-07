using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class Sequence : XmlObject
	{
		/// <summary>
		/// 序列ID
		/// <summary>
		public int Id;
		/// <summary>
		/// 碰撞区域定义列表
		/// <summary>
		public List<HitZone> HitZones;
		/// <summary>
		/// 时间事件列表
		/// <summary>
		public List<Timeline> Timelines;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "HitZones", this.HitZones);
			Write(_1, "Timelines", this.Timelines);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "HitZones": GetChilds(_2).ForEach (_3 => this.HitZones.Add(ReadObject<XmlCode.Skill.HitZone>(_3, "XmlCode.Skill.HitZone"))); break;
				case "Timelines": GetChilds(_2).ForEach (_3 => this.Timelines.Add(ReadObject<XmlCode.Skill.Timeline>(_3, "XmlCode.Skill.Timeline"))); break;
			}
		}
	}
}
