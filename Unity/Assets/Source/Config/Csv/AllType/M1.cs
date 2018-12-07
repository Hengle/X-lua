using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.AllType
{
	public class M1 : Cfg.AllType.SingleClass
	{
		/// <summary>
		/// 继承1
		/// <summary>
		public readonly long V3;

		public M1(DataStream data) : base(data)
		{
			this.V3 = data.GetLong();
		}
	}
}
