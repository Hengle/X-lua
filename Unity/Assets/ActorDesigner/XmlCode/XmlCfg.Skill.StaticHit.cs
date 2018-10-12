using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public  class StaticHit : XmlCfg.Skill.Timeline
	{
		/// <summary>
		/// 碰撞区域
		/// <summary>
		public XmlCfg.Skill.HitZone Zone;
		/// <summary>
		/// 触发序列容器ID
		/// <summary>
		public int SequeueID;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Zone", this.Zone);
			Write(_1, "SequeueID", this.SequeueID);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Zone": this.Zone = ReadObject<XmlCfg.Skill.HitZone>(_2, "XmlCfg.Skill.HitZone"); break;
				case "SequeueID": this.SequeueID = ReadInt(_2); break;
			}
		}
	}
}
