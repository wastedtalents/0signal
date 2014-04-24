using UnityEngine;
using System.Collections;
using ZS.Engine;
using ZS.Characters;
using ZS.Engine.Cam;
using ZS.Engine.Peripherials;
using ZS.HUD;
using ZS.Engine.Audio;
using ZS.Engine.Utilities;

namespace ZS.Engine { 

	// All stuff related to current user's input and how he interacts with the world.
	public class UserInputService : Singleton<UserInputService> {

		private Vector3 _tempVector;		
		private GameObject _hitObject;
		private Entity _hitEntity;

		// Update.
		void Update () {
			ParseMouseActivity();		
		}
			
		// Parse activity related to mouse which is global.
		private void ParseMouseActivity() {
			if(GameService.Instance.IsTactical) {
				if(Input.GetMouseButtonDown(0)) 
					TacticalLeftButtonDown();
	    		else if(Input.GetMouseButtonDown(1)) 
	    			TacticalRightButtonDown();

	    		MouseHover();
	    	} 
	   //  	else if(Input.GetMouseButtonDown(0)) // In non tactical check if we clicked
				// HandleBullets();
	   //  	}
		}

		// Left button was pressed in tactical mode.
		private void TacticalLeftButtonDown() {
			 if(Registry.Instance.hudManager.PointInClientBounds(InputService.Instance.MousePosition)) {
			        _hitObject = CameraManager.Instance.FindHitObject(InputService.Instance.MousePosition, out _tempVector);			        
			        
			        // If nothing was hit or an illegal point was hit, do nothing.
			        if(_tempVector == Registry.Instance.invalidHitPoint) { // we dont care. 
						if(GameService.Instance.selectedObject != null) {
							GameService.Instance.selectedObject.SetSelection(SelectionType.NotSelected);
							GameService.Instance.selectedObject = null;
						}
			        	return;
			        }

//			        var hitEntity = _hitObject.transform.root.GetComponent< Entity >();
			        var hitEntity = _hitObject.GetComponent< Entity >();

			        // Object hit was interactive.
			        if(hitEntity != null) { 
				        // If this entity is not already selected.
				        if(GameService.Instance.selectedObject != hitEntity) {
				        	if (GameService.Instance.selectedObject != null) { // Deselect if selected.
								GameService.Instance.selectedObject.SetSelection(SelectionType.NotSelected);
			       			} 
							hitEntity.SetSelection(SelectionType.Command);
				            GameService.Instance.selectedObject = hitEntity;
			        	}
			        }
			        else if (GameService.Instance.selectedObject != null) { // Deselect if selected.
						GameService.Instance.selectedObject.SetSelection(SelectionType.NotSelected);
						GameService.Instance.selectedObject = null;
			        }
			    }
		}

		// Right button was clicked in selection mode.
		private void TacticalRightButtonDown() {
			// For now, right click doesnt do anything if nothing is selected.
			if(GameService.Instance.selectedObject == null) {
				TacticalLeftButtonDown(); // treat it like a lbutton.
			}
			else if(Registry.Instance.hudManager.PointInClientBounds(InputService.Instance.MousePosition)) {
			    _hitObject = CameraManager.Instance.FindHitObject(InputService.Instance.MousePosition, out _tempVector);

				var hitEntity = _hitObject == null ? null : _hitObject.transform.root.GetComponent< Entity >();
			    
				DebugUtil.LogComponentsOf(_hitObject);
				//Debug.Log("HE " + hitEntity);


			    if(hitEntity == null && _hitObject.tag != Registry.GROUND_NAME)
			       	return;

			    // Action mouse clicked.
			    GameService.Instance.selectedObject.ActionInitiated(
			    	_hitObject,
			    	hitEntity,
			    	_tempVector);
			}
		}

		// Parse hovering.
		private void MouseHover() {
			if(Registry.Instance.hudManager.PointInClientBounds(InputService.Instance.MousePosition)) {
			    _hitObject = CameraManager.Instance.FindHitObject(InputService.Instance.MousePosition, out _tempVector);
			    if(_hitObject != null) { // We're hovering over sth.
			    	if(GameService.Instance.selectedObject != null) { // If we have sth selected. 
						GameService.Instance.selectedObject.SetHoverState(_hitObject);
			    	} else if(_hitObject.tag != Registry.GROUND_NAME) {			    			
			            _hitEntity = _hitObject.transform.root.GetComponent< Entity >();
			            if(_hitEntity != null && GameService.Instance.IsObjectSelectable(_hitEntity)) {
			           		Registry.Instance.hudManager.SetCursorState(CursorState.Select);
			            }
			        }
				}				
			}
		}
	}
}