using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ZS.Engine { 

	// Responsible for pooling new objects.
	public class ObjectFactory : Singleton<ObjectFactory> {
		private int tempAmount;
		private GameObject temp;

		// Types to be pooled.
		public GameObject[] pooledPrefabs;
		public int[] limits;
		// key is the name of prefab, then the list of actual objects.
		private Dictionary<string, List<GameObject>> _pool; 
		/// <summary>
	    /// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
	    /// </summary>
		protected GameObject _containerObject;

		void Start() {
			_containerObject = new GameObject(Registry.GLOBAL_POOL_NAME);
			_pool = new Dictionary<string, List<GameObject>>();

			for(int i = 0; i < pooledPrefabs.Length; i++) {
				temp = pooledPrefabs[i];
				_pool.Add(temp.name, new List<GameObject>());
				tempAmount = (i < limits.Length) ? limits[i] : Registry.Instance.defaultPoolSize;
				for ( int n=0; n<tempAmount; n++) {
					var newObj = Instantiate(temp) as GameObject;
					newObj.name = temp.name;
					newObj.SetActiveRecursively(false);
					newObj.transform.parent = _containerObject.transform;

					_pool[newObj.name].Add(newObj);
				}
			}
		}

		// Get object for type.
		public GameObject GetObjectForType ( string objectType ) {
			if(_pool.ContainsKey(objectType)) { 
				// Pick an object.
				temp = _pool[objectType].FirstOrDefault(o => !o.activeInHierarchy);

				if(temp == null)
					temp = _pool[objectType][0];
				temp.transform.parent = null;
				temp.SetActiveRecursively(true);
				return temp;
			}
			return null;
		}

    	// Returm object to pool.
		public void PoolObject ( GameObject obj ) {
			if(_pool.ContainsKey(obj.name)) {
				obj.SetActiveRecursively(false);
				obj.transform.parent = _containerObject.transform;
			}
		}
	}
}