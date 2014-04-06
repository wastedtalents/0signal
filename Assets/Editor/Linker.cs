using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ZS.Engine.Unity { 

public class Linker : EditorWindow {

 	[MenuItem ("0Signal/DialogueEditor", false, 10)]
    static void OpenDialogueEditor () {
    	DialogueEditor.Init();
    }
}

}