using UnityEngine;
using System.Collections;
using ZS.HUD;

namespace ZS.Engine { 

	// Object registry.
	public class Registry : Singleton<Registry>, IInitializable {

		public const string GROUND_NAME = "Background";
		public readonly KeyCode TOGGLE_OPS_KEY = KeyCode.T;
		public float CEILING_LAYER_Z ;
		public int CEILING_LAYER_MASK_ID; // used for raycasting mapping as a layermask.

		// Resources.
		public const string FOOD_ICON_NAME = "food";
		public const string ORGANIC_ICON_NAME = "organic";
		public const string SYNTHETIC_ICON_NAME = "synthetic";

		public const float ENTITY_SPRITE_OFFSET = 90f;


		// ACTIONS.
		public const string ACTION_ATTACK = "A_Attack";

        public float cameraShakeMagnitude = 2.0f;
        public float cameraShakeDuration = 0.05f;
		public float cameraTrackingSpeed;
		public float cameraDrag = 40f;
		public float cameraZoom = 20f;

		public float cameraZoomMin = -18;
		public float cameraZoomMax = -2;
		public float cameraScrollOffset = 15f;
		public float cameraScrollSpeed = 25f;

		// icons.
		public const int ICON_WIDTH = 32;
		public const int ICON_HEIGHT = 32;
		public const int TEXT_WIDTH = 128;
		public const int TEXT_HEIGHT = 32;

		// TODO: this has to be scripted!!
		public readonly int maxOrganic = 100;
		public readonly int maxSynthetic = 100;
		public readonly int maxFood = 100;

		public int playerStartOrganic = 10;
		public int playerStartSynthetic = 0;
		public int playerStartFood = 10;

		public int buildingBuildSpeed;

		[HideInInspector]
		public Transform mainCameraTransform;

		[HideInInspector]
		public Camera mainCamera;
		
		[HideInInspector]
		public GameObject avatar;
		
		[HideInInspector]
		public GameObject hud;
		
		[HideInInspector]
		public HUDManager hudManager;

		[HideInInspector]
		public Vector3 invalidHitPoint;

		[HideInInspector]
		public Bounds invalidBounds;

	//	[HideInInspector]
//		public EntityList entityList;

		// Current player script.
		[HideInInspector]
		public Player player;

		public void Initialize() {
			mainCamera = Camera.main;
			mainCameraTransform = mainCamera.transform;
		//	entityList = EntityList.Instance;
			player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent< Player >();
			avatar = GameObject.FindGameObjectsWithTag("Avatar")[0];
			hud = GameObject.FindGameObjectsWithTag("HUD")[0];
			hudManager = hud.GetComponent<HUDManager>();
			invalidHitPoint = Vector3.forward;
			invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0, 0, 0));

			CEILING_LAYER_Z = GameObject.FindGameObjectsWithTag("CeilingPlane")[0].transform.position.z;
			CEILING_LAYER_MASK_ID = 1 << LayerMask.NameToLayer("Ceiling");

			Debug.Log(">" + CEILING_LAYER_MASK_ID);
		}
	}

}
