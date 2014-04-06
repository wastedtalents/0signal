using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ZS.Engine.Unity {

	public class DialogueEditor  : EditorWindow {

		private static DialogueEditor window;


		// Add menu named "TowerEditor" to the Window menu
	    //[MenuItem ("TDTK/SpawnEditor")]
	    public static void Init(){
	        // Get existing open window or if none, make a new one:
	        window = (DialogueEditor)EditorWindow.GetWindow(typeof (DialogueEditor));
			window.minSize=new Vector2(620, 620);
	
	//		GetSpawnManager();
	    }
	}

}