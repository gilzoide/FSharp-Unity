using UnityEngine;
using UnityEditor;

/// FSC options window
public class FSCompilerOptions : EditorWindow {
	/// The compiler command
	public static string compilerCommand = "fsharpc";
	/// The aditional command line arguments to the compiler
	public static string compilerAditionalArgs = "";
	/// The F# scripts directory
	public static string inputDir = "Assets/Scripts";
	/// The output directory
	public static string outputDir = "Assets/Frameworks";
	/// Should we output that F# scripts are up to date?
	public static bool outputUpToDate = true;

	[MenuItem ("F#/Compiler Options")]
	public static void ShowWindow () {
		EditorWindow.GetWindow (typeof (FSCompilerOptions), false, "FSC options");
	}

	void OnGUI () {
		inputDir = EditorGUILayout.TextField (new GUIContent ("Scripts directory",
				"Directory containing F# scripts"), inputDir);
		outputDir = EditorGUILayout.TextField (new GUIContent ("Output directory",
				"Directory where F# scripts will be built into"), outputDir);
		compilerCommand = EditorGUILayout.TextField (new GUIContent ("Compiler command",
				"The compiler command to be called"), compilerCommand);
		compilerAditionalArgs = EditorGUILayout.TextField (new GUIContent ("Extra arguments",
				"Additional command line arguments to the compiler. This doesn't include \"-a\" nor UnityEngine UnityEditor \"-r:\" options"),
				compilerAditionalArgs);
		outputUpToDate = GUILayout.Toggle (outputUpToDate, new GUIContent ("Output 'up to date'",
				"Should we output that some F# is up to date?"));
	}
}
