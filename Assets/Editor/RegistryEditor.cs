using UnityEngine;
using UnityEditor;
using System.Collections;

namespace ZS.Engine.Unity { 

	[CustomEditor(typeof(Registry))]
	public class RegistryEditor : Editor {

		private const string RESOURCE_LIMITS = "Resource Limits";
		private const string RESOURCE_STARTS = "Resource Initial Vals";
		private const string CAMERA_TAB = "Camera Settings";
		private const string ICONS_TAB = "GUI Settings";
		private const string SETTINGS_TAB = "Player Settings";
		private const string AUDIO_TAB = "Audio Settings";
		private const string DEBUG_TAB = "Debug Settings";

		private bool _showResourceLimits, _showResourceStarts, _showCameraTab, _showIconsTab, 
		_showAudioTab, _showPlayerSettingsTab, _showDebugTab;
		private Registry _target;

		public override void OnInspectorGUI() {
			_target = (Registry)target;

			// Create player foldout.
			CreateResourceLimitsTab();
			CreateResourceStartsTab();
			CreateCameraTab();
			CreateGUITab();
			CreatePlayerSettingsTab();
			CreateAudioTab();
			CreateDebugTab();
		}

		private void CreatePlayerSettingsTab() {
			//buildingBuildSpeed
			//
			_showPlayerSettingsTab = EditorGUILayout.Foldout(_showPlayerSettingsTab, SETTINGS_TAB);
			if(_showPlayerSettingsTab) {
				if(Selection.activeTransform) {
					_target.buildingBuildSpeed = EditorGUILayout.IntField("Building Speed : ", _target.buildingBuildSpeed);
				}
			}
			if(!Selection.activeTransform) {
				_showPlayerSettingsTab = false;	
			}
		}

		private void CreateGUITab() {
			_showIconsTab = EditorGUILayout.Foldout(_showIconsTab, ICONS_TAB);
			if(_showIconsTab) {
				if(Selection.activeTransform) {
					EditorGUILayout.LabelField("Icon Width : ", Registry.ICON_WIDTH.ToString());
					EditorGUILayout.LabelField("Icon Height : ", Registry.ICON_HEIGHT.ToString());
					EditorGUILayout.LabelField("Text Width : ", Registry.TEXT_WIDTH.ToString());
					EditorGUILayout.LabelField("Text Height : ", Registry.TEXT_HEIGHT.ToString());
				}
			}
			if(!Selection.activeTransform) {
				_showIconsTab = false;	
			}
		}

		private void CreateAudioTab() {
			_showAudioTab = EditorGUILayout.Foldout(_showAudioTab, AUDIO_TAB);
			if(_showAudioTab) {
				if(Selection.activeTransform) {
					_target.minFallOffRange = EditorGUILayout.FloatField("Min fall-off : " , _target.minFallOffRange);
				}
			}
		}

		//showDebugBounds
		public void CreateDebugTab() {
			_showDebugTab = EditorGUILayout.Foldout(_showDebugTab, DEBUG_TAB);
			if(_showDebugTab) {
				if(Selection.activeTransform) {
					_target.showDebugBounds = EditorGUILayout.Toggle("Show debug gizmo : " , _target.showDebugBounds);
				}
			}
		}
	
		private void CreateCameraTab() {
			_showCameraTab = EditorGUILayout.Foldout(_showCameraTab, CAMERA_TAB);
			if(_showCameraTab) {
				if(Selection.activeTransform) {
					// _target.mainCameraTransform = (Transform)EditorGUILayout.ObjectField("Camera transform : ",
					// 	_target.mainCameraTransform, 
					// 	typeof(Transform), true);
					_target.cameraShakeMagnitude = EditorGUILayout.FloatField("Shake magnitude : " , _target.cameraShakeMagnitude);
					_target.cameraShakeDuration = EditorGUILayout.FloatField("Shake duration : " , _target.cameraShakeDuration);
					_target.cameraTrackingSpeed = EditorGUILayout.FloatField("Track speed : " , _target.cameraTrackingSpeed);
					_target.cameraDrag = EditorGUILayout.FloatField("Camera Drag : " , _target.cameraDrag);
					_target.cameraZoom = EditorGUILayout.FloatField("Camera Zoom : " , _target.cameraZoom);
					_target.cameraZoomMin = EditorGUILayout.FloatField("Camera Zoom Min : " , _target.cameraZoomMin);
					_target.cameraZoomMax = EditorGUILayout.FloatField("Camera Zoom Max : " , _target.cameraZoomMax);
					_target.cameraScrollOffset = EditorGUILayout.FloatField("Camera Scroll Offset : " , _target.cameraScrollOffset);
					_target.cameraScrollSpeed = EditorGUILayout.FloatField("Camera Scroll Speed : " , _target.cameraScrollSpeed);
				}
			}
			if(!Selection.activeTransform) {
				_showCameraTab = false;	
			}
		}

		private void CreateResourceStartsTab() {
			_showResourceStarts = EditorGUILayout.Foldout(_showResourceStarts, RESOURCE_STARTS);
			if(_showResourceStarts) {
				if(Selection.activeTransform) {
					_target.playerStartOrganic = EditorGUILayout.IntField("Start Organic : ", _target.playerStartOrganic);
					_target.playerStartSynthetic = EditorGUILayout.IntField("Start Synthetic : ", _target.playerStartSynthetic);
					_target.playerStartFood = EditorGUILayout.IntField("Start Food : ", _target.playerStartFood);
				}
			}
			if(!Selection.activeTransform) {
				_showResourceStarts = false;	
			}
		}		

		private void CreateResourceLimitsTab() {
			_showResourceLimits = EditorGUILayout.Foldout(_showResourceLimits, RESOURCE_LIMITS);
			if(_showResourceLimits) {
				if(Selection.activeTransform) {
					EditorGUILayout.LabelField("Max Organic : ", _target.maxOrganic.ToString());
					EditorGUILayout.LabelField("Max Synthetic : ", _target.maxSynthetic.ToString());
					EditorGUILayout.LabelField("Max Food : ", _target.maxFood.ToString());
				}
			}
			if(!Selection.activeTransform) {
				_showResourceLimits = false;	
			}
		}
	}

}