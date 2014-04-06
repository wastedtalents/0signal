using UnityEngine;
using System.Collections;

namespace ZS.Engine.Audio { 

	// Single audio object.
	[System.Serializable]
	public class AudioObject {

		public AudioSource source;
		public bool inUse=false;
		public Transform thisT;

		public AudioObject(AudioSource src, Transform t){
			source=src;
			thisT=t;
		}
	}

}