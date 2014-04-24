using UnityEngine;
using System.Collections.Generic;
using ZS.Engine;

namespace ZS.Resources {

	// Manager for resources.
	public class ResourceManager : Singleton<ResourceManager> {
		
		private Vector3 _tempPosition;
		private GameObject _depositRef;

		// Maps resource type to sample prefab - change this.
		private Dictionary<PlayerResourceType, string> _resourceToPrefab;

		void Awake() {
			_resourceToPrefab = new Dictionary<PlayerResourceType, string>();
		}

		// Get prefab name for type.
		private string GetPrefabNameForType(PlayerResourceType resType) {
			if(!_resourceToPrefab.ContainsKey(resType))
				throw new System.ArgumentException("[GetPrefabNameForType] - illegal resType " + resType);
			return _resourceToPrefab[resType];
		}

		// Create resource at specific point - dont care which.
		public void CreateSingleResource(PlayerResourceType resourceType, Vector3 position, float randRadius = 0) {
			var prefName = GetPrefabNameForType(resourceType);
			CreateSingleResource(prefName, position, randRadius);
		}	

		// Create resource at specific point - dont care which.
		public GameObject CreateSingleResource(string prefabName, Vector3 position, float randRadius = 0,
			bool forceResize = false, bool isChild = false) {
			var element = ObjectFactory.Instance.GetObjectForType(prefabName, forceResize);
			if(element == null)
				throw new System.ArgumentException("[CreteSingleResource] No such prefab - " + prefabName);
			_tempPosition = position;
			if(randRadius != 0) {
				var r = randRadius / 2;
				_tempPosition.x += Random.value * randRadius - r;
				_tempPosition.y += Random.value * randRadius - r;
			}
			if(!isChild)
				element.transform.position = _tempPosition;
			else
				element.transform.localPosition = _tempPosition;
			return element;
		}

		// Create a resource depo.
		public void CreateResourceDepo(
			string depoName,
			string prefabName, 
			int quantity,
			Vector3 position,
			string depoComponentClass = null,
			float spread = 0) {

			// Instantiate a prefab deposit and add itms to it.
			if(!ObjectFactory.Instance.IsKnownPrefab(prefabName))
				throw new System.ArgumentException("[CreateResourceDepo] - Unknown prefab - " + prefabName);

			var newObj = new GameObject(depoName);
			newObj.layer = LayerMask.NameToLayer( Registry.ENTITIES_LAYER );
			if(!System.String.IsNullOrEmpty(depoComponentClass) != null)
				newObj.AddComponent(depoComponentClass);
			newObj.transform.position = position;

			for(int i=0;i< quantity; i++) {
				var element = CreateSingleResource(prefabName, Vector3.zero, spread, true, true);
				element.transform.parent = newObj.transform;
				element.layer = LayerMask.NameToLayer( Registry.ENTITIES_LAYER );
				
				// Add resource component depending on what it is.
				element.AddComponent("ResMetal");
			}
		}
	}

}