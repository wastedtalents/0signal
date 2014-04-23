using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using ZS.Engine.Peripherials;
using ZS.Engine.Utilities;
using ZS.HUD;

namespace ZS.Engine.Cam { 

	// Mode of the camera.
	public enum CameraMode {
		Detached = 0,
		Fixed,
		Follow
	}

	// Manages camera bahaviour.
	public class CameraManager : Singleton<CameraManager>, IInitializable {

		#region Members.

		private Transform _cameraTransform;
		private Transform _target;
		private Dictionary<CameraMode, Action> _methods;
		private float _temp , _temp2;
		private float _dTime;
		private Vector3 _tempVector, _tempVector2;
		private Vector2 _tempVector2d;
		private bool _mouseScroll;

		private int _tempInt;
		private Collider2D[] _findColliders;

		#endregion

		#region Declarations.

		private CameraMode _currentMode;

		#endregion

		public void Initialize() {
			_cameraTransform = Registry.Instance.mainCameraTransform;
			_tempVector2 = _cameraTransform.position = _cameraTransform.position;
			_findColliders = new Collider2D[4]; // Magic max colliders.

			_methods = new  Dictionary<CameraMode, Action>();
			_methods[CameraMode.Detached] = DoDetached;
			_methods[CameraMode.Follow] = DoFollow;
			_methods[CameraMode.Fixed] = DoFix;
		}

		private void DoFollow() {
			_cameraTransform = Registry.Instance.mainCameraTransform;

        	_temp = Mathf.Lerp (_cameraTransform.position.x, _target.position.x, _dTime);
        	_temp2 = Mathf.Lerp (_cameraTransform.position.y, _target.position.y, _dTime);
        	_cameraTransform.position = new Vector3(_temp, _temp2, _cameraTransform.position.z);	
		}

		private void DoFix() {
			Registry.Instance.mainCameraTransform.position = _target.position;
		} 

		private void DoDetached() {

			float xpos = Input.mousePosition.x;
			float ypos = Input.mousePosition.y;
			Vector3 movement = new Vector3(0,0,-10);

			//horizontal camera movement
			_mouseScroll = false;
			if(xpos >= 0 && xpos < Registry.Instance.cameraScrollOffset) {
			    movement.x -= Registry.Instance.cameraScrollSpeed;
			    Registry.Instance.hudManager.SetCursorState(CursorState.PanLeft);
				_mouseScroll = true;
			} else if(xpos <= Screen.width && xpos > Screen.width - Registry.Instance.cameraScrollOffset) {
			    movement.x += Registry.Instance.cameraScrollSpeed;
			    Registry.Instance.hudManager.SetCursorState(CursorState.PanRight);
				_mouseScroll = true;
			}
			//vertical camera movement
			if(ypos >= 0 && ypos < Registry.Instance.cameraScrollOffset) {
			    movement.y -= Registry.Instance.cameraScrollSpeed;
			    Registry.Instance.hudManager.SetCursorState(CursorState.PanDown);
				_mouseScroll = true;
			} else if(ypos <= Screen.height && ypos > Screen.height - Registry.Instance.cameraScrollOffset) {
			    movement.y += Registry.Instance.cameraScrollSpeed;
			    Registry.Instance.hudManager.SetCursorState(CursorState.PanUp);
				_mouseScroll = true;
			}

			movement = Registry.Instance.mainCameraTransform.TransformDirection(movement);
			movement.z = 0;

			//calculate desired camera position based on received input
			Vector3 origin = Registry.Instance.mainCameraTransform.position;
			Vector3 destination = origin;
			destination.x += movement.x;
			destination.y += movement.y;
			destination.z += movement.z;

			if(destination != origin) {
  				Registry.Instance.mainCameraTransform.position = Vector3.MoveTowards(origin, destination, 
  					Time.deltaTime * Registry.Instance.cameraScrollSpeed);
			}

		}

		private void ParseZoom() {
			_cameraTransform = Registry.Instance.mainCameraTransform;
			
			_temp = InputService.Instance.GetZoom() * Registry.Instance.cameraZoom * Time.deltaTime;
			var tempV = _tempVector;
        	if (_temp != 0) {
        		_tempVector = Vector3.forward * Registry.Instance.cameraZoom * _temp;
        		_tempVector2 = _cameraTransform.position + _tempVector;
        	}
        	if(_tempVector2.z > Registry.Instance.cameraZoomMin &&
        		_tempVector2.z < Registry.Instance.cameraZoomMax) {
        		_tempVector2.x = _cameraTransform.position.x;
        		_tempVector2.y = _cameraTransform.position.y;
        		_cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _tempVector2 , _dTime);
        	}
		}

		// Follow a transform.
		public void Follow(Transform target) {
			_currentMode = CameraMode.Follow;
			_target = target;
		}

		// Fix on location.

		public void Fix(Transform point) {
			_currentMode = CameraMode.Fixed;
			_target = point;
		}

		// Camera will be detached.
		public void Detach() {
			_currentMode = CameraMode.Detached;
		}

		public void UpdatePosition() {
			ParseZoom();
 			_dTime = Time.deltaTime * Registry.Instance.cameraTrackingSpeed;
			_methods[_currentMode]();
		}

		#region Utilities.

		// Get hit object.
		// NOTE since this scans only the Z = 0 plane, all the colliders have to be there.
		public GameObject FindHitObject(Vector3 hitPoint, out Vector3 actualHit) {
			// NOTE: this will work only on Z = 0;
			_tempVector = InputService.Instance.MousePosition;
			_tempVector.z = -Registry.Instance.mainCameraTransform.position.z;
			_tempVector = Registry.Instance.mainCamera.ScreenToWorldPoint(_tempVector);
       		_tempVector2d = new Vector2(_tempVector.x, _tempVector.y);

       		// Get collider.
       	    _tempInt = Physics2D.OverlapPointNonAlloc(_tempVector2d, _findColliders);
			if(_tempInt > 0) {
 				actualHit = _tempVector2d;
 				return _findColliders[0].gameObject;
 			}
 			actualHit = Registry.Instance.invalidHitPoint;
			return null;
		}

		#endregion
	}

}
