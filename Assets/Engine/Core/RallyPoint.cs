using UnityEngine;
using System.Collections;

namespace ZS.Engine {

	public class RallyPoint : MonoBehaviour {

		public void Enable () {
			var renderers = GetComponentsInChildren(typeof(Renderer));
			foreach(Renderer renderer in renderers) 
			renderer.enabled = true;
		}

		public void Disable () {
			var renderers = GetComponentsInChildren(typeof(Renderer));
			foreach(Renderer renderer in renderers) 
			renderer.enabled = false;
		}
	}

}
