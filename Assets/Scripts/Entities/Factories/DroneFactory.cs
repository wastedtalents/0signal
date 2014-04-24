using UnityEngine;
using ZS.Characters;
using System.Collections;
using ZS.Engine;
using ZS.Resources;

namespace ZS.Entities.Factories {

	// Concrete implemnetation - a drone factory.
	public class DroneFactory : UnitFactory {

		protected override void Start () {
     	    base.Start();
        	_actions = new string[] { Registry.Commands.CREATE_DRONE };

        //	ResourceManager.Instance.CreateSingleResource("plant3", new Vector3(10,10,0), 10);
        	ResourceManager.Instance.CreateResourceDepo(
			"Metal Depo",
			"resMetal", 
			12,
			new Vector3(10,10,0),
			null,
			1);
    	}

    	// Perform an action.
    	public override void PerformAction(GameObject targetObject, Entity targetEntity, string actionToPerform) {
            base.PerformAction(targetObject, targetEntity, actionToPerform);
    		// Whatever the action - create a new unit.
            if(actionToPerform == Registry.Commands.CREATE_DRONE)
    		  CreateUnit(actionToPerform);
    	}

	}

}
