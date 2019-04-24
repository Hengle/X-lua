using System;
using System.Collections.Generic;
using Cfg;

namespace AllType
{
	/// <summary>
	/// 
	/// <summary>
	public class SingleClass : CfgObject
	{
		/// <summary>
		/// 
		/// <summary>
		public readonly string Var1;
		/// <summary>
		/// 
		/// <summary>
		public readonly float Var2;
		
		public SingleClass(DataStream data)
		{
			Var1 = data.GetString();
			Var2 = data.GetFloat();
		}
	}
}
