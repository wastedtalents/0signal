using UnityEngine;
using System;
using System.Collections.Generic;
using ZS.Engine;
using ZS.Engine.Peripherials;
using System.Collections.ObjectModel;

namespace ZS.HUD {

	// State of the cursor.
	public enum CursorState { 
		Select, 
		Move,
		Attack, 
		PanLeft, 
		PanRight, 
		PanUp, 
		PanDown, 
		Harvest 
	}

	public class HUDManager : MonoBehaviour {

		#region Textures.

		public Texture2D[] resources;
		public Texture2D activeCursor;
		public Texture2D selectCursor, leftCursor, rightCursor, upCursor, downCursor;
		public Texture2D[] moveCursors, attackCursors, harvestCursors;

		#endregion

		#region GUISkins.

		private Dictionary< PlayerResourceType, int > _resourceValues, _resourceLimits;
		private Dictionary< PlayerResourceType, Texture2D > _resourceImages;
		
		public GUISkin resourceSkin, ordersSkin, selectBoxSkin, mouseCursorSkin;
		public bool _insideWidth, _insideHeight;

		public int gui_ordersBarWidth = 150;
		public int gui_resourcesBarHeight = 40;
		public int gui_selectionNameHeight = 30;

		#endregion

		#region Prefabs.

		public GameObject selectionPrefab;

		#endregion

		private CursorState _activeCursorState;
		private int _currentFrame = 0;
		private bool _mouseOverHud;

		// Use this for initialization
		void Start () {
			if(_resourceValues == null)
				_resourceValues = new Dictionary< PlayerResourceType, int >();
			if(_resourceLimits == null)
				_resourceLimits = new Dictionary< PlayerResourceType, int >();
			_resourceImages = new Dictionary< PlayerResourceType, Texture2D > ();

			InitResources();
			SetArcadeMode();
		}
		
		// Update is called once per frame	
		void OnGUI () {
			DrawResources();
			DrawOrders();
			DrawMouseCursor();
		}

		// Draws the resources bar.
		private void DrawResources() {
			GUI.skin = resourceSkin;
   			GUI.BeginGroup(new Rect(0,0,Screen.width , gui_resourcesBarHeight));
  	 		GUI.Box(new Rect(0,0,Screen.width,gui_resourcesBarHeight),"");
  	 		int topPos = 4, iconLeft = 4, textLeft = 40;
			DrawResourceIcon(PlayerResourceType.Food, iconLeft, textLeft, topPos);
			iconLeft += Registry.TEXT_WIDTH;
			textLeft += Registry.TEXT_WIDTH;
			DrawResourceIcon(PlayerResourceType.Organic, iconLeft, textLeft, topPos);
			iconLeft += Registry.TEXT_WIDTH;
			textLeft += Registry.TEXT_WIDTH;
			DrawResourceIcon(PlayerResourceType.Synthetic, iconLeft, textLeft, topPos);
   			GUI.EndGroup();
		}

		private void DrawResourceIcon(PlayerResourceType type, int iconLeft, int textLeft, int topPos) {
			if(!_resourceImages.ContainsKey(type))
				return;
 		   	var icon = _resourceImages[type];
    		var text = String.Format("{0}/{1}", _resourceValues[type].ToString(),
    			_resourceLimits[type].ToString());
    		GUI.DrawTexture(new Rect(iconLeft, topPos, Registry.ICON_WIDTH, Registry.ICON_HEIGHT), icon);
    		GUI.Label (new Rect(textLeft, topPos, Registry.TEXT_WIDTH, Registry.TEXT_HEIGHT), text);
		}

		// Draws the orders bar.
		private void DrawOrders() {
			GUI.skin = ordersSkin;
	  		GUI.BeginGroup(new Rect(Screen.width - gui_ordersBarWidth , 
	  			gui_resourcesBarHeight, gui_ordersBarWidth,
	  			Screen.height - gui_resourcesBarHeight));
	  		GUI.Box(new Rect(0,0, gui_ordersBarWidth , Screen.height - gui_resourcesBarHeight),"");

			// Draw selected player.
			var selectionName = "";
			if(GameService.Instance.selectedObject != null) {
    			selectionName = GameService.Instance.selectedObject.displayName;
			}
			if(!selectionName.Equals("")) {
    			GUI.Label(new Rect(4,10, gui_ordersBarWidth, 
    				gui_selectionNameHeight), selectionName);
			}	   		

	   		GUI.EndGroup();
		}

		// Initialize resources like icons.
		private void InitResources() {
			Debug.Log("ss " + resources.Length);
			for(int i = 0; i < resources.Length; i++) {
    			switch(resources[i].name) {
        			case Registry.FOOD_ICON_NAME:
            			_resourceImages.Add(PlayerResourceType.Food, resources[i]);
            			break;
        			case Registry.ORGANIC_ICON_NAME:
            			_resourceImages.Add(PlayerResourceType.Organic, resources[i]);
            			break;
            		case Registry.SYNTHETIC_ICON_NAME:
            			_resourceImages.Add(PlayerResourceType.Synthetic, resources[i]);
            			break;
        			default: 
        				break;
    			}
			}
		}

		public bool PointInClientBounds(Vector3 point) {
			 //Screen coordinates start in the lower-left corner of the screen
   			 //not the top-right of the screen like the drawing coordinates do
   			 _insideWidth = point.x >= 0 && point.x <= Screen.width - gui_ordersBarWidth;      
   			 _insideHeight = point.y >= 0 && point.y <= Screen.height - 	gui_resourcesBarHeight;
  			 return _insideWidth && _insideHeight;
		}

		// Draws resources.
		public void SetResourceCollections(Dictionary< PlayerResourceType, int > resourceValues, 
			Dictionary< PlayerResourceType, int > resourceLimits) {
    		_resourceValues = resourceValues;
    		_resourceLimits = resourceLimits;
		}

		// Gets the actual PLAYING area.
		public Rect GetPlayingArea() {
    		return new Rect(0, gui_resourcesBarHeight, Screen.width - gui_ordersBarWidth, 
    			Screen.height - gui_resourcesBarHeight);
		}

		// Draw a mouse cursor.
		private void DrawMouseCursor() {
			_mouseOverHud = !PointInClientBounds(InputService.Instance.MousePosition) && 
				_activeCursorState != CursorState.PanRight && _activeCursorState != CursorState.PanUp;

  			if(_mouseOverHud) {
	        	Screen.showCursor = true;
	    	} else {
        		Screen.showCursor = false; // dont use the regular cursor, just draw our skin.
        		GUI.skin = mouseCursorSkin;
        		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
        		UpdateCursorAnimation();
        		Rect cursorPosition = GetCursorDrawPosition();
        		// Draw label with texture.
        		GUI.Label(cursorPosition, activeCursor);
        		GUI.EndGroup();
    		}
		}

		//sequence animation for cursor (based on more than one image for the cursor)
    	//change once per second, loops through array of images
    	// Time.time =  This is the time in seconds since the start of the game. 
		private void UpdateCursorAnimation() {
    		if(_activeCursorState == CursorState.Move) {
        		_currentFrame = (int)Time.time % moveCursors.Length;
        		activeCursor = moveCursors[_currentFrame];
    		} else if(_activeCursorState == CursorState.Attack) {
        		_currentFrame = (int)Time.time % attackCursors.Length;
        		activeCursor = attackCursors[_currentFrame];
    		} else if(_activeCursorState == CursorState.Harvest) {
        		_currentFrame = (int)Time.time % harvestCursors.Length;
        		activeCursor = harvestCursors[_currentFrame];
    		}
		}

		// Get position where to draw a cursror.
		private Rect GetCursorDrawPosition() {
    		//set base position for custom cursor image
    		//screen draw coordinates are inverted
  			float leftPos = InputService.Instance.MousePosition.x;
    		float topPos = Screen.height - InputService.Instance.MousePosition.y; 

    		//adjust position base on the type of cursor being shown
    		if(_activeCursorState == CursorState.PanRight)
    			 leftPos = Screen.width - activeCursor.width;
   			else if(_activeCursorState == CursorState.PanDown) 
   				topPos = Screen.height - activeCursor.height;
    		else if(_activeCursorState == CursorState.Move || 
    			_activeCursorState == CursorState.Select || 
    			_activeCursorState == CursorState.Harvest) {
        			topPos -= activeCursor.height / 2;
        			leftPos -= activeCursor.width / 2;
   				}
    		return new Rect(leftPos, topPos, activeCursor.width, activeCursor.height);
		}

		public void SetArcadeMode() {
			SetCursorState(CursorState.Attack);
		} 

		public void SetTacticalMode() {
			SetCursorState(CursorState.Select);
		}

		public void SetCursorState(CursorState newState) {
    		_activeCursorState = newState;
    		switch(newState) {
			    case CursorState.Select:
        			activeCursor = selectCursor;
        			break;
    			case CursorState.Attack:
        			_currentFrame = (int)Time.time % attackCursors.Length;
        			activeCursor = attackCursors[_currentFrame];
        			break;
    			case CursorState.Harvest:
        			_currentFrame = (int)Time.time % harvestCursors.Length;
        			activeCursor = harvestCursors[_currentFrame];
        			break;
    			case CursorState.Move:
        			_currentFrame = (int)Time.time % moveCursors.Length;
        			activeCursor = moveCursors[_currentFrame];
        			break;
    			case CursorState.PanLeft:
        			activeCursor = leftCursor;
    				break;
    			case CursorState.PanRight:
        			activeCursor = rightCursor;
        			break;
    			case CursorState.PanUp:
        			activeCursor = upCursor;
        			break;
    			case CursorState.PanDown:
        			activeCursor = downCursor;
        			break;
    			default: break;
    		}
		}
	}

}