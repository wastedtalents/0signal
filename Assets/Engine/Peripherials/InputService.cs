using UnityEngine;
using System.Collections;
using  ZS.Engine;

namespace ZS.Engine.Peripherials {

	// Usługi odpowiedzialne za przetwarzanie logiki inputów.	
	public class InputService : Singleton<InputService> {

		public float GetZoom() {
			return Input.GetAxis("Mouse ScrollWheel");
		}

		public float MoveDX {
			get { return Input.GetAxis("Horizontal"); }
		}

		public float MoveDY {
			get { return Input.GetAxis("Vertical"); }
		}

		public float MouseDX {
			get { return Input.GetAxis("Mouse X"); }
		}

		public float MouseDY {
			get { return Input.GetAxis("Mouse X"); }
		}

		public Vector3 MousePosition {
			get { return Input.mousePosition; }
		}

		public bool HasMouseMovedX {
			get { return MouseDX != 0; }
		}

		public bool HasMouseMovedY {
			get { return MouseDY != 0; }
		}		

		public bool IsOpsModeKey { 
			get { return Input.GetKeyUp(Registry.Instance.TOGGLE_OPS_KEY); }
		}
	}

}