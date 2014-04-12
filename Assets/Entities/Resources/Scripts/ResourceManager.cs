// using System;
// using UnityEngine;
// using System.Collections.Generic;
// using ZS.Engine;

// namespace ZS.Resources {

// 	// Manager for resources.
// 	public class ResourceManager : Singleton<ResourceManager>, IInitializable {
// 		private Dictionary<Type, Func<Resource>> _factory;
// 		private ObjectPool _objectPool;

// 		public void Initialize() {
// 			_factory = new Dictionary<Type,Func<Resource>>();
// 			_factory.Add(typeof(ResMetalDeposit, _ => )))
// 		}

// 		// Spwans a new resource.
// 		public Resource Spawn(Type resourceType) {
// 			if(!_factory.ContainsKey(resourceType))
// 				throw new ArgumentException("resourceType");
// 			return  _factory[resourceType]();
// 		}

// 		// Spawns resource at a given position.
// 		public Resource SpawnAt(Type resourceType, Vector3 position) {
// 			var res = Spawn(resourceType);
// 			res.transform.position = position;
// 			return res;
// 		}
// 	}

// }