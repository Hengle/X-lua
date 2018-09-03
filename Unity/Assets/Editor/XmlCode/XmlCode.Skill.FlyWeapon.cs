using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class FlyWeapon : XmlCode.Skill.TraceObject
	{
		/// <summary>
		/// 子弹半径
		/// <summary>
		public float BulletRadius;
		/// <summary>
		/// 是否穿透
		/// <summary>
		public bool PassBody;
		/// <summary>
		/// 被击效果ID
		/// <summary>
		public int BeAttackEffectId;

		public override void Write(TextWriter _1)
		{
			Write(_1, "BulletRadius", this.BulletRadius);
			Write(_1, "PassBody", this.PassBody);
			Write(_1, "BeAttackEffectId", this.BeAttackEffectId);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "BulletRadius": this.BulletRadius = ReadFloat(_2); break;
				case "PassBody": this.PassBody = ReadBool(_2); break;
				case "BeAttackEffectId": this.BeAttackEffectId = ReadInt(_2); break;
			}
		}
	}
}
