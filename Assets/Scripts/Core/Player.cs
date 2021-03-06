using UnityEngine;
using System.Collections.Generic;
using ZS.Characters;

namespace ZS.Engine { 

	// Type of the player.
	public enum PlayerType {
		// Current player.
		Current,
		// Human opponent.
		Human,
		// Bot opponent.
		Bot
	}

	// Represents a current player.
	public class Player : PlayerSettingsBase {
		
		public string username;
		private RallyPoint _rallyPoint;
		public PlayerType playerType; // Is this a current player or a different 

		public Player() {
		}

		// Update is called once per frame
		void Update () {
		
		}

		// Related rally point.
		public RallyPoint RallyPoint { 
			get { return _rallyPoint; }
		}

		// Recompile.
		protected override void InitResources() {
			_rallyPoint = (RallyPoint)gameObject.GetComponentInChildren(typeof(RallyPoint));
			_rallyPoint.Disable();
			
			_resources = InitResourceList();
   			_resourceLimits = InitResourceList();
		}

		protected override void InitResourceLimits() {
			AddStartResourceLimits();
			AddStartResources();

			if(playerType == PlayerType.Current) {
    			Registry.Instance.hudManager.SetResourceCollections(_resources, _resourceLimits);
			}
		}

		private Dictionary< PlayerResourceType, int > InitResourceList() {
			return new Dictionary< PlayerResourceType, int >() {
				{ PlayerResourceType.Organic, 0 },
				{ PlayerResourceType.Synthetic, 0},
				{ PlayerResourceType.Food, 0}
			};
		}

		private void AddStartResourceLimits() {
    		IncrementResourceLimit(PlayerResourceType.Organic, Registry.Instance.maxOrganic);
    		IncrementResourceLimit(PlayerResourceType.Synthetic, Registry.Instance.maxSynthetic);
    		IncrementResourceLimit(PlayerResourceType.Food, Registry.Instance.maxFood);
		}
 
		private void AddStartResources() {
   			AddResource(PlayerResourceType.Organic, Registry.Instance.playerStartOrganic);
   			AddResource(PlayerResourceType.Synthetic, Registry.Instance.playerStartSynthetic);
   			AddResource(PlayerResourceType.Food, Registry.Instance.playerStartFood);
		}

		// Add a unit for the player.
		public void AddUnit(string unitName, Vector3 spawnPoint, Quaternion rotation) {
			// Actually add the unit.
		    var units = GetComponentInChildren< PlayerUnits >();
			var newUnit = (GameObject)Instantiate(EntityRepository.Instance.GetMovingUnit(unitName),spawnPoint, rotation);
			newUnit.transform.parent = units.transform;

		    var unitObject = newUnit.GetComponent< MovingUnit >();
    		if(unitObject != null && spawnPoint != _rallyPoint.transform.position) {
    			unitObject.StartMoving(_rallyPoint.transform.position);
    		}
		}		
	}

}