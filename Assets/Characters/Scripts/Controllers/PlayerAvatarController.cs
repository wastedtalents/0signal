using UnityEngine;
using System.Collections;
using ZS.Engine.Cam;
using ZS.Engine.Peripherials;
using ZS.Engine;
using ZS.Characters;
using ZS.Engine.Utilities;

namespace Characters {

	// Controller of a currently selected avatar.
	public class PlayerAvatarController : MonoBehaviour {

		// Prefab for bullets.
		public GameObject bullet;

		private GameObject _head;
		private Animator _animator;
		private Rigidbody2D _rigidBody2D;

		public float flSpeed = 0.0f;
		private float _moveX, _moveY;
		protected Vector3 _inputRotation;
		protected Vector3 _tempVector;
		private Quaternion _rotation;
		private bool _isShooting;
		private Vector3 mouse_pos  ;

		// Use this for initialization
		void Start () {
			_head = transform.Find("Head").gameObject;
			_animator = GetComponent<Animator>();
			_rigidBody2D = GetComponent<Rigidbody2D>();
		}
		
		void Update () {
			// Activity related to base input.
			ParseInput();
			
			// Parse mouse activity.
			ParseMouseActivity();
		}

		// Handle input activity.
		private void ParseInput() {
			if(GameService.Instance.IsTactical && !InputService.Instance.IsOpsModeKey)
				return;

			if(InputService.Instance.IsOpsModeKey) {
				GameService.Instance.ToggleOpsMode();
			}
			else {
				_moveX = InputService.Instance.MoveDX * Time.deltaTime;
				_moveY = InputService.Instance.MoveDY * Time.deltaTime;
			}

			// Move the player.
			MovePlayer();
		}

		// Move player and change opacity of layers above.
		private void MovePlayer() {
			_rigidBody2D.velocity = new Vector2(_moveX * flSpeed, _moveY * flSpeed);
			_rotation = LookHelper.SmoothLookAtMouse(transform, 0.05f, Registry.ENTITY_SPRITE_OFFSET);
		//	_head.transform.rotation = _rotation;

			_tempVector.x = transform.position.x;
			_tempVector.y = transform.position.y;
			_tempVector.z = Registry.Instance.CEILING_LAYER_Z;
			
			// MakeObstructingObjectsTransparent();

			_animator.SetBool("isRunning", _moveX != 0 || _moveY != 0);
			_animator.SetFloat("speed" , Mathf.Abs(Mathf.Max(_moveX, _moveY)));
		}


		private void HandleBullets()
		{
			_tempVector = Quaternion.AngleAxis(8f, Vector3.up) * mouse_pos;
			_tempVector = (transform.position + (_tempVector.normalized * 0.3f));
			GameObject objCreatedBullet = 
				(GameObject) Instantiate(
				bullet, 
				_tempVector, 
				_rotation ); // create a bullet, and rotate it based on the vector inputRotation
		//	Physics.IgnoreCollision(objCreatedBullet.collider, collider);
			var comp = Camera.main.GetComponent<CameraController>();
			StartCoroutine(comp.Shake());
		}

		// Parase all stuff related to mouse.
		private void ParseMouseActivity() {
			if(!GameService.Instance.IsTactical) {
				_isShooting = Input.GetMouseButtonDown(0);
				if(_isShooting && !GameService.Instance.IsTactical)
					HandleBullets();
	    	}	    
		}

		
	}
}