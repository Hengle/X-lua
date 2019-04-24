using System;
using System.Collections.Generic;
using Cfg;

namespace AllType
{
	/// <summary>
	/// 
	/// <summary>
	public class M2 : AllType.SingleClass
	{
		/// <summary>
		/// 
		/// <summary>
		public readonly bool V4;
		
		public M2(DataStream data) : base(data)
		{
			V4 = data.GetBool();
		}
	}
}
