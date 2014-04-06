using UnityEngine;
using System.Collections;

namespace ZS.Characters {

	public abstract class BuildableEntity : Entity {

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

		#region Progress.

		// Max build progress of the building.
		public float maxBuildProgress;

		// Current build progress of the building.
		protected float _currentBuildProgress;

		#endregion

		// Get build percentage of the building.
		public float getBuildPercentage() {
    		return _currentBuildProgress / maxBuildProgress;
		}
	}

}