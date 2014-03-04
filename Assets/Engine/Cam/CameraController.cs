using UnityEngine;
using System.Collections;
using ZS.Engine;

namespace ZS.Engine.Cam { 

    public class CameraController : MonoBehaviour {

        private Camera _mainCamera;

    	// Use this for initialization
    	void Start () {
            _mainCamera = Registry.Instance.mainCamera;
            CameraManager.Instance.Follow(Registry.Instance.avatar.transform);
    	}
    	
    	public IEnumerator Shake() {
            float elapsed = 0.0f;
            var magnitude = Registry.Instance.cameraShakeMagnitude;
            var shakeDuration = Registry.Instance.cameraShakeDuration;
        
            Vector3 originalCamPos = _mainCamera.transform.position;
            while (elapsed < shakeDuration) {
            
                elapsed += Time.deltaTime;          
                
                float percentComplete = elapsed / shakeDuration;         
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
                
                // map noise to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;
                x *= magnitude * damper;
                y *= magnitude * damper;
                
                var cPos = _mainCamera.transform.position;
                _mainCamera.transform.position = new Vector3(cPos.x + x, cPos.y + y, originalCamPos.z);
                    
                yield return null;
            }
            _mainCamera.transform.position = originalCamPos;
        }

    	// Update is called once per frame.
    	void Update () {
            CameraManager.Instance.UpdatePosition();
    	}
    }

}