using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class BeAttackEffect : CfgObject
	{
		/// <summary>
		/// 被击效果id
		/// <summary>
		public readonly int Id;
		/// <summary>
		/// 被打击者的抛物曲线
		/// <summary>
		public readonly int Curve;
		/// <summary>
		/// 被打击者的受击动作，null为默认
		/// <summary>
		public readonly string DefencerAction;
		/// <summary>
		/// 被打击者身上出现的被击特效，Null为默认
		/// <summary>
		public readonly int DefencerEffectId;

		public BeAttackEffect(DataStream data)
		{
			this.Id = data.GetInt();
			this.Curve = data.GetInt();
			this.DefencerAction = data.GetString();
			this.DefencerEffectId = data.GetInt();
		}
	}
}
