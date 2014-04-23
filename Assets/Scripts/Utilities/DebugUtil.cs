using UnityEngine;
using System.Collections;

namespace ZS.Engine.Utilities {

	public static class DebugUtil  {

		public static string V2s(Vector3 v) {
			return System.String.Format("x : {0} , y : {1} , z : {2}", v.x, v.y, v.z);
		}

		public static string V2s(Vector2 v) {
			return System.String.Format("x : {0} , y : {1}", v.x, v.y);
		}

	}

}