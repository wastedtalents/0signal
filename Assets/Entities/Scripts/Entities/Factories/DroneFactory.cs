using UnityEngine;
using ZS.Characters;
using System.Collections;
using ZS.Engine;

namespace ZS.Entities.Factories {

	// Concrete implemnetation - a drone factory.
	public class DroneFactory : UnitFactory {

		protected override void Start () {
     	    base.Start();
        	_actions = new string[] { "Drone" };

        		var obj = ObjectFactory.Instance.GetObjectForType("plant3");
			Debug.Log(obj);
    	}

    	// Perform an action.
    	public override void PerformAction(GameObject targetObject, Entity targetEntity, string actionToPerform) {
    		base.PerformAction(targetObject, targetEntity, actionToPerform);
    		// Whatever the action - create a new unit.
    		CreateUnit(actionToPerform);
    	}

	}

}
