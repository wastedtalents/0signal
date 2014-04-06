using UnityEngine;
using System.Collections.Generic;
using ZS.Characters;
using System.Linq;
using ZS.Engine;

namespace ZS.Entities.Factories { 

	// A building - drone factory.	
	public abstract class UnitFactory : BuildableEntity {

		public float maxUnitBuildProgress; // maximal building progress of a unit.
		private float _currentUnitBuildProgress;
		protected Queue< string > _buildQueue; // queue of buildings to be built.
		private Vector3 _spawnPoint;

/*
Awake is used to initialize any variables or game state before the game starts. 
Awake is called only once during the lifetime of the script instance.
Awake is called after all objects are initialized so you can safely speak to other objects or 
query them using eg. GameObject.FindWithTag.
*/
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

	// public override void SetSelection(bool selected, Rect playingArea) {
	//     base.SetSelection(selected, playingArea);
	//     if(_owner != null) {
	//         var flag = _owner.GetComponentInChildren();
	//         if(selected) {
	//             if(flag && _owner.human && spawnPoint != ResourceManager.InvalidPosition && rallyPoint != ResourceManager.InvalidPosition) {
	//                 flag.transform.localPosition = rallyPoint;
	//                 flag.transform.forward = transform.forward;
	//                 flag.Enable();
	//             }
	//         } else {
	//             if(flag && player.human) flag.Disable();
	//         }
	//     }
	// }

		// Creates an unit.
protected void CreateUnit(string unitName) {
	_buildQueue.Enqueue(unitName);
}

		// Processes the build queue.
protected void ProcessBuildQueue() {
			// For all items.
	if(_buildQueue.Count > 0) {
		_currentUnitBuildProgress += Time.deltaTime * Registry.Instance.buildingBuildSpeed;

       	// If build progress is ready - fire up a new unit.		
		if(_currentUnitBuildProgress > maxBuildProgress) {
			if(_owner != null) {				
            	// Creates a new unit.
				_owner.AddUnit(_buildQueue.Dequeue(), _spawnPoint, transform.rotation);
				_currentUnitBuildProgress = 0.0f;
			}
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
