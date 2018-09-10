using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class Attack : XmlCode.Skill.Action
	{
		/// <summary>
		/// 打击点id
		/// <summary>
		public int Id;
		/// <summary>
		/// 打击区域id
		/// <summary>
		public int HitZoneId;
		/// <summary>
		/// 被击效果id
		/// <summary>
		public int BeAttackEffectId;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "HitZoneId", this.HitZoneId);
			Write(_1, "BeAttackEffectId", this.BeAttackEffectId);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "HitZoneId": this.HitZoneId = ReadInt(_2); break;
				case "BeAttackEffectId": this.BeAttackEffectId = ReadInt(_2); break;
			}
		}
	}
}
