using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class Attack : Csv.Skill.Action
	{
		/// <summary>
		/// 打击点id
		/// <summary>
		public readonly int Id;
		/// <summary>
		/// 打击区域id
		/// <summary>
		public readonly int HitZoneId;
		/// <summary>
		/// 被击效果id
		/// <summary>
		public readonly int BeAttackEffectId;

		public Attack(DataStream data) : base(data)
		{
			this.Id = data.GetInt();
			this.HitZoneId = data.GetInt();
			this.BeAttackEffectId = data.GetInt();
		}
	}
}
