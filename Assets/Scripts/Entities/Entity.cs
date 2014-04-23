using UnityEngine;
using System.Collections;
using ZS.Engine;
using ZS.HUD;
using ZS.Engine.GUI;
using ZS.Engine.Utilities;

namespace ZS.Characters {

	// Base entity.
	// 
	public abstract class Entity : MonoBehaviour {

		#region Entity settings.

		public Texture2D resourceIcon; // Additional resource icon.
		public string uniqueName;
		public string displayName; // display name.
		public int hitPoints, maxHitPoints;
		
		#endregion

		public Player _owner; // Actual owner of this object if any.
		protected string[] _actions = {};
		protected SelectionType _currentSelection;
		protected Bounds _selectionBounds;

		protected Vector3 _tempVector;
		protected GameObject _selection;		

		public Player Owner { 
			get { return _owner; }
		}

		public Entity(string name) : this() { 
			displayName = name;
		}

		public Entity() {
			_tempVector = new Vector3();
		}

		// While this object was selected, someone hovered over "go".
		public virtual void SetHoverState(GameObject go) {
			// If I have an owner and this is CURRENT user then display.
			if(_currentSelection != SelectionType.NotSelected && Owner != null && Owner.playerType == PlayerType.Current) {
				Registry.Instance.hudManager.SetCursorState(CursorState.Select);
			}
		}

		// Set this object as selected.
		public virtual void SetSelection(SelectionType selection) {
		   _currentSelection = selection;
		   if(_currentSelection == SelectionType.Command)
 		   	 ShowSelection();
 		   else if(_selection != null)
 		   	 HideSelection();
		}

		// Calculates bounds of this object.
		protected void CalculateBounds() {
			_selectionBounds = new Bounds(transform.position, Vector3.zero);
			foreach(var r in GetComponentsInChildren< Renderer >()) {
				_selectionBounds.Encapsulate(r.bounds);
			}
		}

		protected void ShowSelection() {
			_selection = GUIService.Instance.GetSelection(gameObject.transform, _currentSelection);
		} 

		protected void HideSelection() {
			GUIService.Instance.ReturnSelection(_selection);
			_selection = null;
		} 

		// Gets available actions.
		public string[] GetActions() {
   			return _actions;
		}

		// Peform a given action.
		public virtual void PerformAction(GameObject targetObject, Entity targetEntity, string actionToPerform) {
			if(actionToPerform == Registry.ACTION_ATTACK) {
				Debug.Log(targetEntity.displayName + " : " + actionToPerform);
			}
		}

		// Mouse clicked at point.
		public virtual void ActionInitiated(GameObject hitObject, Entity entity, Vector3 hitPoint) {
			 // If were currently SELECTED as command mode, we can do stuff!
			 if(_currentSelection == SelectionType.Command && hitObject != null 
			 	&& hitObject.name != Registry.GROUND_NAME) {
	        	// This logic might be moved up BUT 
	        	if(entity != null) { 
	        		// GetActions
	        		// Perform action
	        		entity.PerformAction(hitObject, entity, Registry.ACTION_ATTACK);
	        	}
			 }
		}

		// Update.
		protected virtual void Update () { 
		}

		protected virtual void Start () {
		}
	}

}