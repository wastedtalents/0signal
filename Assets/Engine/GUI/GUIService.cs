using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZS.Engine.GUI {

	// All stuff related to GUI and graphical stuff.
	// <description>This service is used for all stuff related to GUI! It's like a VisualStateManager. </description>
	public class GUIService : Singleton<GUIService>, IInitializable {

		private SelectionManager _selectionManager;

		public void Initialize() {		
		   _selectionManager = new SelectionManager(Registry.Instance.hudManager.selectionPrefab);
		}

		#region Selection management.

		// Sets the selection on a transform and returns it.
		public GameObject GetSelection(Transform parent, SelectionType type) {
			return _selectionManager.Get(parent, type);
		}

		// Return the selection as we dont need it.
		public void ReturnSelection(GameObject selection) {
			_selectionManager.Return(selection);
		}

		// Hide all selectors.
		public void HideSelectors() {
			_selectionManager.HideSelectors();
		}

		#endregion

		// // Calculate selection bawx.
		// public static Rect CalculateSelectionBox(Bounds selectionBounds, Rect playingArea) {
		//     //shorthand for the coordinates of the centre of the selection bounds
		//     float cx = selectionBounds.center.x;
		//     float cy = selectionBounds.center.y;
		//     float cz = selectionBounds.center.z;
		//     //shorthand for the coordinates of the extents of the selection bounds
		//     float ex = selectionBounds.extents.x;
		//     float ey = selectionBounds.extents.y;
		//     float ez = selectionBounds.extents.z;
		    
		//     //Determine the screen coordinates for the corners of the selection bounds
		//     List<Vector3> corners = new List<Vector3>();
		//     corners.Add(Registry.Instance.mainCamera.WorldToScreenPoint(new Vector3(cx+ex, cy+ey, cz+ez)));
		//     corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy+ey, cz-ez)));
		//     corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy-ey, cz+ez)));
		//     corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy+ey, cz+ez)));
		//     corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx+ex, cy-ey, cz-ez)));
		//     corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy-ey, cz+ez)));
		//     corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy+ey, cz-ez)));
		//     corners.Add(Camera.main.WorldToScreenPoint(new Vector3(cx-ex, cy-ey, cz-ez)));
		             
		//     //Determine the bounds on screen for the selection bounds
		//     Bounds screenBounds = new Bounds(corners[0], Vector3.zero);
		//     for(int i=1; i<corners.Count; i++) {
		//         screenBounds.Encapsulate(corners[i]);
		//     }
		             
		//     //Screen coordinates start in the bottom right corner, rather than the top left corner
		//     //this correction is needed to make sure the selection box is drawn in the correct place
		// 	float selectBoxTop = playingArea.height - (screenBounds.center.y + screenBounds.extents.y);
		//     float selectBoxLeft = screenBounds.center.x - screenBounds.extents.x;
		//     float selectBoxWidth = 2 * screenBounds.extents.x;
		//     float selectBoxHeight = 2 * screenBounds.extents.y;
		             
		//     return new Rect(selectBoxLeft, selectBoxTop, selectBoxWidth, selectBoxHeight);
		// }	

	}

}