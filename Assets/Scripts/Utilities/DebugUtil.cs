using UnityEngine;
using System;
using System.Collections.Generic;

namespace ZS.Engine.Utilities {

	public static class DebugUtil  {

		public static string V2s(Vector3 v) {
			return System.String.Format("x : {0} , y : {1} , z : {2}", v.x, v.y, v.z);
		}

		public static string V2s(Vector2 v) {
			return System.String.Format("x : {0} , y : {1}", v.x, v.y);
		}

		public static void LogComponentsOf(GameObject obj) {
			var components = new List<Component>();
            foreach(var component in obj.GetComponents<Component>()) {
            	Debug.Log(String.Format("[{0}] - {1}" , obj.name, component));
            }
		}

	}

}