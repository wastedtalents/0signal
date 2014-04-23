using UnityEngine;
using System.Collections;

namespace ZS.Characters {

	// Collection of parameters for an entity that is a resource - building, etc.
	public sealed class ResourceEntityParams  {
		
		#region Cost.

		// One time cost at buy.
		public int organicBuy;
		public int syntheticBuy;
		public int foodBuy;

		// One time gain at sell.
		public int organicSell;
		public int syntheticSell;
		public int foodSell;

		// Upkeep per "unit of time".
		public int organicUpkeep;
		public int syntheticUpkeep;
		public int foodUpkeep;

		#endregion
	}

}