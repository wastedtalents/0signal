using UnityEngine;
using System.Collections;
using ZS.Engine;

// Attach it to an object to display.
public class DebugShowSelf : MonoBehaviour {
	void OnDrawGizmosSelected()
    {
        if (Registry.Instance.showDebugBounds) {
            Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
           	Gizmos.DrawWireSphere(transform.position, 0.5f);
        }        
    }
}
