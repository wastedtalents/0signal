using UnityEngine;
using System.Collections;
using ZS.Engine;

namespace ZS.Resources { 

	// Metal deposit.
	public class ResMetalDeposit : Resource {

		// All resource blocks.
		private int _numBlocks;

		protected override void Start () {
			base.Start();
			_numBlocks = GetComponentsInChildren< ResMetal >().Length;
			_resourceType = PlayerResourceType.Synthetic;
		}

		protected override void Update () {
			base.Update();

			var percentLeft = (float)_amountLeft / (float)capacity;
			if(percentLeft < 0) 
				percentLeft = 0;
			var numBlocksToShow = (int)(percentLeft * _numBlocks);
			var blocks = GetComponentsInChildren< ResMetal >();
			if(numBlocksToShow >= 0 && numBlocksToShow < blocks.Length) {
				var sortedBlocks = new ResMetal[blocks.Length];
			
				// Calculate bounds.
				foreach(var metal in blocks) {
					sortedBlocks[blocks.Length - int.Parse(metal.name)] = metal;
				}
				for(int i = numBlocksToShow; i < sortedBlocks.Length; i++) {
					sortedBlocks[i].renderer.enabled = false;
				}
				CalculateBounds();
			}
		}

	}

}