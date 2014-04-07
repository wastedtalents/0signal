using UnityEngine;
using System.Collections;
using ZS.Engine;
using ZS.HUD;
using ZS.Engine.Utilities;

namespace ZS.Characters {

	// Basic unit / person that player can acquire.
	// That can be an animal or a person.
	public class MovingUnit : Entity {

    #region Params.

		// Move / rotate params.
		public float moveSpeed, rotateSpeed;

    #endregion
        
		protected bool _moving, _rotating;
		private Vector3 _destination, _targetPosition;
		private Quaternion _targetRotation;


		// Override default hovering behavior.
		public override void SetHoverState(GameObject hoverObject) {
		    base.SetHoverState(hoverObject);

		    // For units we can make them move.
		    if(_currentSelection != SelectionType.NotSelected && Owner != null && Owner.playerType == PlayerType.Current) {
				 if(hoverObject.tag == Registry.GROUND_NAME) {				 	
		        	Registry.Instance.hudManager.SetCursorState(CursorState.Move);
		         } 
			}
		}

		// Action was initiated.
		public override void ActionInitiated(GameObject hitObject, Entity entity, Vector3 hitPoint) {
			base.ActionInitiated(hitObject, entity, hitPoint);
   			//only handle input if owned by a human player and currently selected
    		if(_owner != null && _owner.playerType == PlayerType.Current && _currentSelection != SelectionType.NotSelected) {
        		if(hitObject.tag == Registry.GROUND_NAME && 
        			hitPoint != Registry.Instance.invalidHitPoint) {
            		_tempVector.x = hitPoint.x;
            		_tempVector.y = hitPoint.y;
            		_tempVector.z = hitPoint.z;

            		// Start moving in this position.
            		StartMoving(_tempVector);
        		}
    		}
		}

		// Starts moving.
		public void StartMoving(Vector3 destination) {
 		   	_destination = destination;

			var targetPosition = _destination - transform.position;
			_targetRotation = Quaternion.LookRotation(targetPosition);
    		_targetPosition = _destination - transform.position;

    		_rotating = true;
    		_moving = true;
    	}

    	// Update this unit.
    	protected override void Update() {
			base.Update();
    		if(_rotating) 
    			TurnToTarget();
    		else if(_moving) 
    		 	MakeMove();
    	}

    	// Actually turn to face the target.
    	private void TurnToTarget() {
            if(!LookHelper.SmoothLookAtUntil(transform, _destination, rotateSpeed, Registry.ENTITY_SPRITE_OFFSET, .4f)) {
              _moving = true;
              _rotating = false;
            }
        }

      private void MakeMove() {
        transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * moveSpeed);
        if(transform.position == _destination) 
          _moving = false;        
      }

	}

}