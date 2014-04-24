using UnityEngine;
using ZS.Engine;
using System.Collections;
using ZS.Resources;
using ZS.HUD;

namespace ZS.Characters { 

	public class Harvester : MovingUnit {

		// Capacity.
		public float capacity;

		private bool _isHarvesting = false, _isEmptying = false;
		private float _currentLoad = 0.0f;
		private PlayerResourceType _harvestType;
		private Resource _resourceDeposit;

		protected override void Start () {
			base.Start();
			_harvestType = PlayerResourceType.None;
		}

		protected override void Update () {
			base.Update();

		}

		// Set the state of hovering.
		public override void SetHoverState(GameObject hoverObject) {
			base.SetHoverState(hoverObject);
			 if(_currentSelection != SelectionType.NotSelected && Owner != null && Owner.playerType == PlayerType.Current) {
                if(hoverObject.tag == Registry.GROUND_NAME) {
					var resource = hoverObject.transform.parent.GetComponent< Resource >();
					if(resource != null && !resource.isDepleted()) 
					Registry.Instance.hudManager.SetCursorState(CursorState.Harvest);
				}
			}
		}

		// Action was initiated.
		public override void ActionInitiated(GameObject hitObject, Entity entity, Vector3 hitPoint) {		
			base.ActionInitiated(hitObject, entity, hitPoint);	
   			//only handle input if owned by a human player
			  if(_owner != null && _owner.playerType == PlayerType.Current && _currentSelection != SelectionType.NotSelected) {
              if(hitObject.tag == Registry.GROUND_NAME && hitPoint != Registry.Instance.invalidHitPoint) {
					// If its a resource - go fo' it.
					var resource = hitObject.transform.parent.GetComponent< Resource >();
					if(resource != null && !resource.isDepleted()) {
	                    //make sure that we select harvester remains selected
						// if(player.SelectedObject) 
						// 	player.SelectedObject.SetSelection(false, playingArea);
						SetSelection(SelectionType.Command);
						GameService.Instance.selectedObject = this;
						StartHarvest(resource);
					}
				} 
				else
					StopHarvest();
			}
		}

		// Start harvesting.
		private void StartHarvest(Resource resource) {
			_resourceDeposit = resource;

			// Start moving towards an object.
			StartMoving(resource.transform.position, resource.gameObject);
			// If thats a new kinf of resource- reload.
			if(_harvestType == PlayerResourceType.None || _harvestType != resource.ResourceType) {
				_harvestType = resource.ResourceType;
				_currentLoad = 0.0f;
			}
			_isHarvesting = true;
			_isEmptying = false;
		}

		private void StopHarvest() {

		}

	}

}