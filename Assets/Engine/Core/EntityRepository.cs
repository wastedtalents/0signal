using UnityEngine;
using System.Collections;
using ZS.Characters;

namespace ZS.Engine {

	// List of all possible entities a player can create, AVAILABLE ONES. Per player.
	public class EntityRepository : Singleton<EntityRepository> {

		public GameObject[] buildings;
		public GameObject[] units;
		public Player player;

		// Get building by type's unique name.
		public GameObject GetBuilding(string name) {
	    	for(int i = 0; i < buildings.Length; i++) {
	       		var building = buildings[i].GetComponent< BuildableEntity >();
	       		if(building != null && building.uniqueName == name) 
	       			return buildings[i];
	    	}
	    	return null;
		}
	 
	 	// Get moving unit by unique name.
		public GameObject GetMovingUnit(string name) {
	    	for(int i = 0; i < units.Length; i++) {
	        	var unit = units[i].GetComponent< MovingUnit >();
	        	if(unit != null && unit.uniqueName == name) return
	        		units[i];
	    	}
		    return null;
		}
 
		// Get player object.
		public Player GetOwner() {
	    	return player;
		}
 
	 	// Get build image for a building.
		public Texture2D GetBuildImage(string name) {
			// Look through the buldings.
		    foreach(var b in buildings) {
		     	var building = b.GetComponent< BuildableEntity >();
		     	if(building != null && building.uniqueName == name) 
		     		return building.resourceIcon;
		    }
		    // Look through the units.
		    foreach(var u in units) {
		    	var unit = u.GetComponent< MovingUnit >();
		    	if(unit != null && unit.uniqueName == name) {		
		    		return unit.resourceIcon;
		    	}
		    }
    		return null;
		}
 	}
}