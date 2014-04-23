using UnityEngine;
using System.Collections;

namespace ZS.Engine.Audio { 

// Everything related to audio management.
	public class AudioService : Singleton<AudioService> {

		// Factory.
		private AudioObject[] _audioObjects;
		public AudioClip[] _musicList;

		// Bools.
		private bool _isInitialized;
		private AudioSource _musicSource;
		private int _currentTrackID = 0;
		public bool playMusic=true;
		public bool shuffle=false;

		#region Sounds.

		// Building.
		public AudioClip buildingBuildingSound;
		public AudioClip buildingBuiltSound;
		public AudioClip buildingSoldSound;

		// Sth lost.
		public AudioClip gameWonSound;
		public AudioClip gameLostSound;
		public AudioClip playerShotSound;

		#endregion

		void Awake() {
			InitResources();
		}

		// Prepare resources.
		void InitResources() {
			_audioObjects = new AudioObject[Registry.Sizes.AUDIO_OBJECTS];
			for(int i=0; i < _audioObjects.Length; i++){
				var obj = new GameObject();
				obj.name="AudioSource";
				
				var src = obj.AddComponent<AudioSource>();
				src.playOnAwake=false;
				src.loop=false;
				src.minDistance = Registry.Instance.minFallOffRange;
				
				var t = obj.transform;
				t.parent = this.transform;
				
				_audioObjects[i] = new AudioObject(src, t);
			}
		}

		// Set volume.
		public void SetSFXVolume(float val){
			AudioListener.volume = val;
		}

		public IEnumerator SoundRoutine2D(int ID, float duration){
			while(duration > 0){
				_audioObjects[ID].thisT.position = Registry.Instance.mainCamera.transform.position;
				yield return null;
			}
			
			//finish playing, clear the audioObject
			this.StartCoroutine(ClearAudioObject(ID, 0));
		}

		// Play tah sound at position !.
		public void PlaySound(AudioClip clip, Vector3 pos) {
			int ID = GetUnusedAudioObject();

			_audioObjects[ID].inUse=true;
			_audioObjects[ID].thisT.position=pos;
			_audioObjects[ID].source.clip=clip;
			_audioObjects[ID].source.Play();

			float duration = _audioObjects[ID].source.clip.length;
			this.StartCoroutine(this.ClearAudioObject(ID, duration));
		}

		//this no position has been given, assume this is a 2D sound
		public void PlaySound(AudioClip clip){
			int ID = GetUnusedAudioObject();
			
			_audioObjects[ID].inUse=true;
			_audioObjects[ID].source.clip=clip;
			_audioObjects[ID].source.Play();
			
			var duration = _audioObjects[ID].source.clip.length;
			this.StartCoroutine(this.ClearAudioObject(ID, duration));
		}

		// play music routine.
		public IEnumerator MusicPlaylistRoutine(){
			while(true){
				if(shuffle) 
					_musicSource.clip = _musicList[Random.Range(0, _musicList.Length)];
				else{
					_musicSource.clip = _musicList[_currentTrackID];
					_currentTrackID+=1;
					if(_currentTrackID == _musicList.Length) 
						_currentTrackID=0;
				}
				
				_musicSource.Play();
				yield return new WaitForSeconds(_musicSource.clip.length);
			}
		}

		// Get one of unused audio objects.
		private int GetUnusedAudioObject() {
			for(int i=0; i < _audioObjects.Length; i++){
				if(!_audioObjects[i].inUse){
					return i;
				}
			}
			return 0;
		}

		//function call to clear flag of an audioObject, indicate it's is free to be used again
		private IEnumerator ClearAudioObject(int ID, float duration){
			yield return new WaitForSeconds(duration);
			_audioObjects[ID].inUse = false;
		}
	}

}