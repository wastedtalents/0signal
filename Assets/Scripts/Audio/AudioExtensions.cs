using UnityEngine;
using System.Collections;

namespace ZS.Engine.Audio { 

	public static class AudioExtensions {

		// example of an extension method.
		public static void PlayGameWon(this AudioService service) {
			if(service.gameWonSound != null) 
				service.PlaySound(service.gameWonSound);
		}

		public static void PlayerShot(this AudioService service) { 
			if(service.playerShotSound != null) 
				service.PlaySound(service.playerShotSound);
		}

	}

}