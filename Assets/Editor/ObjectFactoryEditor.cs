// using UnityEngine;
// using System.Collections;
// using UnityEditor;

// namespace ZS.Engine.Unity { 
	
// 	[CustomEditor(typeof(ObjectFactory))]
// 	public class ObjectFactoryEditor : Editor {

// 		private ObjectFactory _target;

// 		public override void OnInspectorGUI() {
// 			_target = (ObjectFactory)target;

// 			for(int i=0; i < _target.pooledTypes.Length ; i++) {
// 				var pooledType = _target.pooledTypes[i];

// 				EditorGUILayout.BeginHorizontal("Obj");
// 				pooledType.poolPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab : " , pooledType.poolPrefab, typeof(GameObject));
// 				pooledType.limit = EditorGUILayout.IntField("Size : " , pooledType.limit);
// 				EditorGUILayout.EndHorizontal ();
// 			}
// 		}
// 	}
// }