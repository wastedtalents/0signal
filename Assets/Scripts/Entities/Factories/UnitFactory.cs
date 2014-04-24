using UnityEngine;
using System.Collections.Generic;
using ZS.Characters;
using System.Linq;
using ZS.Engine;
using ZS.HUD;

namespace ZS.Entities.Factories { 

	// A building - drone factory.	
	public abstract class UnitFactory : BuildableEntity {

		public float maxUnitBuildProgress; // maximal building progress of a unit.
		private float _currentUnitBuildProgress;
		protected Queue< string > _buildQueue; // queue of buildings to be built.
		private Vector3 _spawnPoint;
		protected Vector3 _rallyPoint;

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
			_rallyPoint = _spawnPoint;
		}

		void Update() {
			ProcessBuildQueue();
		}

		// Set the selection of object.
		public override void SetSelection(SelectionType selType) {
		    base.SetSelection(selType);
		    if(_owner != null) {
		    	var flag = _owner.RallyPoint;
		        if(selType == SelectionType.Command) {
		            if(flag != null && _owner.playerType == PlayerType.Current && HasSpawnPoint()) {

		                flag.transform.localPosition = _rallyPoint;
		                flag.transform.forward = transform.forward;
		                flag.Enable();
		            }
		        } else if (flag != null && _owner.playerType == PlayerType.Current) {
		            flag.Disable();
		        }
		    }
		}

		// Set hovering.
		public override void SetHoverState(GameObject hoverObject) {
		    base.SetHoverState(hoverObject);

		    //only handle input if owned by a human player and currently selected
		    if(_owner != null && _owner.playerType == PlayerType.Current) {
		        if(hoverObject.tag == Registry.GROUND_NAME) {
		            if(Registry.Instance.hudManager.GetPreviousCursorState() == CursorState.Rally) 
		            	Registry.Instance.hudManager.SetCursorState(CursorState.Rally);
		        }
		    }
		}

		public void Sell() {
			if(_owner != null) 
				_owner.AddResource(PlayerResourceType.Synthetic, syntheticSell);
			if(_owner.playerType == PlayerType.Current) 
				SetSelection(SelectionType.NotSelected);
			Destroy(this.gameObject);
		}

		public bool HasSpawnPoint() {
		    return _spawnPoint != Registry.Instance.invalidHitPoint && 
		    _rallyPoint != Registry.Instance.invalidHitPoint;
		}

		// Creates an unit.
		protected void CreateUnit(string unitName) {
			Debug.Log("AI");
			
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

		// While we were selected, an action was initiated!
		public override void ActionInitiated(GameObject hitObject, Entity entity, Vector3 hitPoint) { 
			base.ActionInitiated(hitObject, entity, hitPoint);
  			//only handle iput if owned by a human player and currently selected
			if(_owner != null && _owner.playerType == PlayerType.Current && hitObject.tag == Registry.GROUND_NAME) {
				if((Registry.Instance.hudManager.GetCursorState() == CursorState.Rally || 
					Registry.Instance.hudManager.GetPreviousCursorState() == CursorState.Rally) && 
					hitPoint != Registry.Instance.invalidHitPoint) {
					SetRallyPoint(hitPoint);
				}
			}
		}

		// Sets the rally point.
		public void SetRallyPoint(Vector3 position) {
			_rallyPoint = position;
			if(_owner != null && _owner.playerType == PlayerType.Current) {
				if(_owner.RallyPoint != null) 
					_owner.RallyPoint.transform.localPosition = _rallyPoint;
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
