using UnityEngine;
using System.Collections;

namespace ZS.Engine.Utilities {

	// All the utilities regarding look transformations.
	public static class LookHelper {

		#region Members.

		private static Vector3 _tempVector;
		private static Vector3 _mousePos;
	 	private static Vector3 _objectPos;
		private static float _angle;
		private static Quaternion _rotation;

		#endregion

		static LookHelper() {
			_tempVector = new Vector3(0, 0, 0);		
		}

		// Turn to rotation.
		private static Quaternion CalcRotation(Vector3 source, Vector3 target) {
			_tempVector.z = 0;
	 		_tempVector.x = target.x - source.x;
			_tempVector.y = target.y - source.y;
			_angle = Mathf.Atan2(_tempVector.y, _tempVector.x) * Mathf.Rad2Deg;

			_tempVector.x = 0;
			_tempVector.y = 0;
			_tempVector.z = _angle;

			return Quaternion.Euler(_tempVector);
		}

		public static Quaternion LookAtMouse(Transform source, float offset = 0) {
			_mousePos = Input.mousePosition;
	 		_objectPos = Camera.main.WorldToScreenPoint(source.position);

			var quaternion = CalcRotation(_objectPos, _mousePos);
			source.rotation = quaternion;
			if(offset != 0)
	 	  		source.eulerAngles = new Vector3(0,0,quaternion.eulerAngles.z - 90);
	 	   	return quaternion;
		}

		// Look into the direction of mouse smoothly.
		 public static Quaternion SmoothLookAtMouse(Transform source, float speed, float offset = 0) {
			_mousePos = Input.mousePosition;
	 		_objectPos = Camera.main.WorldToScreenPoint(source.position);

			var quaternion = CalcRotation(_objectPos, _mousePos);
		    var q = Quaternion.Euler(0,0, quaternion.eulerAngles.z + offset);
			source.rotation = Quaternion.Slerp(source.rotation, q, speed);
			return quaternion;
		 }

		 // Look into the direction of target.
		 public static Quaternion LookAt(Transform source, Vector3 target, float offset = 0) {
		 	var quaternion = CalcRotation(source.position, target);
		 	source.rotation = quaternion;
			if(offset != 0)
	 	  		source.eulerAngles = new Vector3(0,0,quaternion.eulerAngles.z + offset);
	 	   	return quaternion;	
		 }

		// Look into the direction of target.
		public static Quaternion LookAt(Transform source, Transform target, float offset = 0) {
			return LookAt(source, target.position, offset);
		}		 

		 // Look smoothly into the direction of target.
		 public static Quaternion SmoothLookAt(Transform source, Transform target, float speed, float offset = 0) {
		 	return SmoothLookAt(source, target.position, speed, offset);
		 }

		 // Look at.
		 public static Quaternion SmoothLookAt(Transform source, Vector3 target, float speed, float offset = 0) {
		 	var quaternion = CalcRotation(source.position, target);
		 	var q = Quaternion.Euler(0,0, quaternion.eulerAngles.z + offset);
			source.rotation = Quaternion.Slerp(source.rotation, q, speed);
			return quaternion;	
		 }

		 // Look at a target UNTIL. If it reaches theta angle difference, we will return false.
		 public static bool SmoothLookAtUntil(Transform source, Vector3 target, float speed, float offset, float theta) {
		 	// Calculate target rotation quaternion.
		 	var quat = SmoothLookAt(source, target, speed, offset);
		 	// Angle between target rotation and current rotation.
		 	_angle = Mathf.Abs(quat.eulerAngles.z - source.rotation.eulerAngles.z + offset) % 360;
		 	return _angle > theta && _angle < (360 - theta);		 	
		 }
	}

}