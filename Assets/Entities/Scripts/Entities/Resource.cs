using UnityEngine;
using System.Collections;
using ZS.Engine;

namespace ZS.Characters { 

	// Simple resource.
	public class Resource : Entity {

		public float capacity;

		protected float _amountLeft;
		protected PlayerResourceType _resourceType;

		public PlayerResourceType ResourceType { 
			get { return _resourceType; }
		}

		protected override void Start () {
			base.Start();
			_amountLeft = capacity;
			_resourceType = PlayerResourceType.None;
		}

		public void Remove(float amount) {
			_amountLeft -= amount;
			if(_amountLeft < 0) 
				_amountLeft = 0;
		}

		public bool isDepleted() {
			return _amountLeft <= 0;
		}

	}

}