using UnityEngine;
using System.Collections.Generic;

namespace ZS.Engine {

	// Base player settings.
	public abstract class PlayerSettingsBase : MonoBehaviour {

		#region Resources.

		// TODO: te rzeczy powinny byc zalezne od poziomu i pobierane ze SKRYPTU.
		protected Dictionary< PlayerResourceType, int > _resources, _resourceLimits;

		#endregion

		void Start() {
			InitResourceLimits();
		}

		void Awake() {
			InitResources();
		}

		protected void AddResource(PlayerResourceType type, int amount) {
    		_resources[type] += amount;
		}
 
		protected void IncrementResourceLimit(PlayerResourceType type, int amount) {
    		_resourceLimits[type] += amount;
		}

		protected abstract void InitResources();
		protected abstract void InitResourceLimits();
	}

}