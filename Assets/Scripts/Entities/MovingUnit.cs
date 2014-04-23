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
		private Vector3 _destination, _origin, _targetPosition;
		private Quaternion _targetRotation;
        private GameObject _destinationObject;


        protected override void Start() {
            CalculateBounds();
        }

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
   _origin = transform.position;
   _destinationObject = null;

   var targetPosition = _destination - _origin;
   _targetRotation = Quaternion.LookRotation(targetPosition);
   _targetPosition = _destination - _origin;

   _rotating = true;
   _moving = true;
}

        // Starts moving while setting destination.
public void StartMoving(Vector3 destinationPoint, GameObject destinationObject) {
    StartMoving(destinationPoint);
    _destinationObject = destinationObject;
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
  if(_destinationObject != null)  {
      CalculateTargetDestination();    
  }        
}


        // Calc destination to target.
    private void CalculateTargetDestination() {
        //calculate number of unit vectors from unit centre to unit edge of bounds
        var originalExtents = _selectionBounds.extents;
        var normalExtents = originalExtents;
        normalExtents.Normalize();
        float numberOfExtents = originalExtents.x / normalExtents.x;
        int unitShift = Mathf.FloorToInt(numberOfExtents);

        //calculate number of unit vectors from target centre to target edge of bounds
        var entity = _destinationObject.GetComponent< Entity >();
        if(entity != null) 
            originalExtents = entity.SelectionBounds.extents;
        else 
            originalExtents = new Vector3(0.0f, 0.0f, 0.0f);
        normalExtents = originalExtents;
        normalExtents.Normalize();
        numberOfExtents = originalExtents.x / normalExtents.x;
        int targetShift = Mathf.FloorToInt(numberOfExtents);

        //calculate number of unit vectors between unit centre and destination centre with bounds just touching
        int shiftAmount = targetShift + unitShift;

        //calculate direction unit needs to travel to reach destination in straight line and normalize to unit vector
        var origin = transform.position;
        var direction = new Vector3(_destination.x -  _origin.x, 0.0f, _destination.y - _origin.y);
        direction.Normalize();

        //destination = center of destination - number of unit vectors calculated above
        //this should give us a destination where the unit will not quite collide with the target
        //giving the illusion of moving to the edge of the target and then stopping
        for(int i = 0; i < shiftAmount; i++) 
            _destination -= direction;
//        destination.y = _destinationdestinationTarget.transform.position.y;
}


private void MakeMove() {
    transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * moveSpeed);
    if(transform.position == _destination) 
    _moving = false;        
}

}

}