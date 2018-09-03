using System;
using System.Collections.Generic;
using Csv;

namespace Csv.AllType
{
	public  class SingleClass : CfgObject
	{
		/// <summary>
		/// Var1
		/// <summary>
		public readonly string Var1;
		/// <summary>
		/// Var2
		/// <summary>
		public readonly float Var2;

		public SingleClass(DataStream data)
		{
			this.Var1 = data.GetString();
			this.Var2 = data.GetFloat();
		}
	}
}
