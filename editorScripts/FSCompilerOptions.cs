using UnityEngine;
using UnityEditor;

public class FSCompilerOptions : EditorWindow {
	public static string compilerCommand = "fsharpc";
	public static string compilerAditionalArgs = "";

	[MenuItem ("F#/Compiler Options")]
	public static void ShowWindow () {
		EditorWindow.GetWindow (typeof (FSCompilerOptions), false, "FSC options");
	}

	void OnGUI () {
		compilerCommand = EditorGUILayout.TextField (new GUIContent ("Compiler command",
				"The compiler command to be called"), compilerCommand);
		compilerAditionalArgs = EditorGUILayout.TextField (new GUIContent ("Extra arguments",
				"Additional command line arguments to the compiler. This doesn't include \"-a\" nor UnityEngine UnityEditor \"-r:\" options"),
				compilerAditionalArgs);
	}
}
