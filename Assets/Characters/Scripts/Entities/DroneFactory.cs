using UnityEngine;
using System.Collections.Generic;
using ZS.Characters;
using System.Linq;

namespace ZS.Engine { 

	// A building - drone factory.	
	public class DroneFactory : BuildableEntity {

		private float _currentUnitBuildProgress;
		public float maxUnitBuildProgress;

		protected Queue< string > _buildQueue;
		private Vector3 _spawnPoint;

		void Awake() {
			// float spawnX = selectionBounds.center.x + transform.forward.x * selectionBounds.extents.x + transform.forward.x * 10;
			// float spawnZ = selectionBounds.center.z + transform.forward.z + selectionBounds.extents.z + transform.forward.z * 10;
			_buildQueue = new Queue< string >();
			var spawnX = transform.position.x;
			var spawnY = transform.position.y;
			_spawnPoint = new Vector3(spawnX, spawnY, 0.0f);
		}

		void Update() {
			ProcessBuildQueue();
		}

		// Creates an unit.
		protected void CreateUnit(string unitName) {
 		   _buildQueue.Enqueue(unitName);
		}

		// Processes the build queue.
		protected void ProcessBuildQueue() {
 	    	if(_buildQueue.Count > 0) {
        		_currentBuildProgress += Time.deltaTime * Registry.Instance.buildingBuildSpeed;
        		if(_currentBuildProgress > maxBuildProgress) {
            		if(_owner != null) 
            			_owner.AddUnit(_buildQueue.Dequeue(), _spawnPoint, transform.rotation);
            		_currentBuildProgress = 0.0f;
        		}
    		}
    	}

    	// Get build queue values.
    	public string[] GetBuildQueueValues() {
    		return _buildQueue.Select(s => s).ToArray();
		}
 	
		// Get build percentage of the building units.
		public float GetUnitBuildPercentage() {
    		return _currentUnitBuildProgress / maxUnitBuildProgress;
		}
	}

}
