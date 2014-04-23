using UnityEngine;
using ZS.Engine;
using System.Collections;
using ZS.Resources;
using ZS.HUD;

namespace ZS.Characters { 

	public class Harverster : MovingUnit {

		// Capacity.
		public float capacity;

		private bool _isHarvesting = false, _isEmptying = false;
		private float _currentLoad = 0.0f;
		private PlayerResourceType _harvestType;

		protected override void Start () {
			base.Start();
			_harvestType = PlayerResourceType.None;
		}

		protected override void Update () {
			base.Update();
		}

		// Set the state of hovering.
		public override void SetHoverState(GameObject hoverObject) {
			base.SetHoverState(hoverObject);
			if(_owner != null && _owner.playerType == PlayerType.Current) {
				if(hoverObject.name != Registry.GROUND_NAME) {
					var resource = hoverObject.transform.parent.GetComponent< Resource >();
					if(resource != null && !resource.isDepleted()) 
						Registry.Instance.hudManager.SetCursorState(CursorState.Harvest);
				}
			}
		}

	}

}