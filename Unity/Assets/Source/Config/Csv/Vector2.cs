using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg
{
	public class Vector2 : CfgObject
	{
		/// <summary>
		/// 
		/// <summary>
		public readonly float X;
		/// <summary>
		/// 
		/// <summary>
		public readonly float Y;

		public Vector2(DataStream data)
		{
			this.X = data.GetFloat();
			this.Y = data.GetFloat();
		}
	}
}
