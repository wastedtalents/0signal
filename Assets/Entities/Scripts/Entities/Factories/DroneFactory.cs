using UnityEngine;
using ZS.Characters;
using System.Collections;

namespace ZS.Entities.Factories {

	// Concrete implemnetation - a drone factory.
	public class DroneFactory : UnitFactory {

		protected override void Start () {
     	    base.Start();
        	_actions = new string[] { "Drone" };
    	}

    	// Perform an action.
    	public override void PerformAction(GameObject targetObject, Entity targetEntity, string actionToPerform) {
    		base.PerformAction(targetObject, targetEntity, actionToPerform);
    		// Whatever the action - create a new unit.
    		CreateUnit(actionToPerform);
    	}

	}

}
