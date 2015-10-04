using UnityEngine;
using UnityEditor;

/// FSC options window
public class FSCompilerOptions : EditorWindow {
	/// The compiler command
	public static string compilerCommand = "fsharpc";
	/// The aditional command line arguments to the compiler
	public static string compilerAditionalArgs = "";
	/// The output directory
	public static string outputDir = "Assets/Frameworks";

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
