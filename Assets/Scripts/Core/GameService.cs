using UnityEngine;
using System.Collections;
using ZS.Engine.Cam;
using ZS.Characters;
using ZS.Engine.GUI;
using ZS.Engine.Peripherials;

namespace ZS.Engine {

	// Service for all the in-game logic.
	// Manages all the rules of the game.
	public class GameService : Singleton<GameService> {

		// Currently selected object.
		// TODO - add more than one.
		[HideInInspector]
		public Entity selectedObject;

		private bool _isTactical;

		// Is in tactical mode.
		public bool IsTactical {
			get { return _isTactical; }
		}

		void Start() {
			SetOpsMode(false);
		}

		// Is entity owned by current player.
		public bool IsOwnedByPlayer(Entity entity) {
			return entity != null && entity.Owner == Registry.Instance.player;
		}

		public bool IsSelectedObjectOwnerByPlayer() {
			return IsOwnedByPlayer(selectedObject);
		}

		public void SetOpsMode(bool isTactical) {
			if(_isTactical == isTactical)
				return;
			_isTactical = isTactical;
			if(_isTactical)  {
				CameraManager.Instance.Detach();
				Registry.Instance.hudManager.SetTacticalMode();
			} 
			else  {
				// Hide selectors.
				Registry.Instance.hudManager.SetArcadeMode();
				GUIService.Instance.HideSelectors();
				CameraManager.Instance.Follow(Registry.Instance.avatar.transform);
			}
		}

		public void ToggleOpsMode() {
			SetOpsMode(!_isTactical);
		}

		#region Tactical Mode management.
		
		// TODO : implement this!
		private void MakeObstructingObjectsTransparent() {
			// 			// option 1.
			// // Collider2D coll = Physics2D.OverlapPoint(_tempVector, Registry.Instance.CEILING_LAYER_MASK_ID);			
			// // if(coll) {
			// // 	//var c = coll.gameObject.transform[renderer.material.color;
			// // 	var c =  coll.gameObject.GetComponentsInChildren<Transform>()[1].gameObject.renderer.material.color;
			// // 	c.a = 0.5f;
			// // 	coll.gameObject.GetComponentsInChildren<Transform>()[1].gameObject.renderer.material.color = c;
			// // }

			// Debug.DrawRay(Camera.main.transform.position, transform.position - Camera.main.transform.position, Color.red);

			// // option2.
			// RaycastHit2D[] hits = new RaycastHit2D[10];
			// Vector3 dir = transform.position - Camera.main.transform.position;
			// Vector3 dtransposed = Camera.main.projectionMatrix.MultiplyVector(dir);
			// var hitCount = Physics2D.GetRayIntersectionNonAlloc(new Ray(Camera.main.transform.position, dtransposed.normalized),
			// 	hits, 10f, Registry.Instance.CEILING_LAYER_MASK_ID);
			// for(int i = 0; i < hitCount; i++) {
			// 	var coll = hits[i];
			// 	Debug.Log("COL" + DebugUtil.V2s(coll.point));
				
			// 	Debug.DrawLine(Camera.main.transform.position, coll.point, Color.yellow);
			// 	var c =  coll.collider.gameObject.GetComponentsInChildren<Transform>()[1].gameObject.renderer.material.color;
			// 	c.a = 0.5f;
			// 	coll.collider.gameObject.GetComponentsInChildren<Transform>()[1].gameObject.renderer.material.color = c;
   //      		//h.collider.renderer.material.color = new Color(h.collider.renderer.material.color.r, h.collider.renderer.material.color.g, h.collider.renderer.material.color.b, 0.5f);
   //    		}
		}

		// Is this item selectable by current player.
		public bool IsObjectSelectable(Entity entity) {			
			// Check if it is in fact a player.
			if(entity.Owner != null && entity.Owner.username == Registry.Instance.player.username) { 
				//TODO: ENEMY BUILDING.  
				return entity is MovingUnit;
            }
            return false;
		}

		#endregion

	}
}