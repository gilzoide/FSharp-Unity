using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class FSCompilerWindow : EditorWindow {
	/// The build log
	string buildLog;

	[MenuItem ("F#/Compiler %#3")]
	public static void ShowWindow () {
		EditorWindow.GetWindow (typeof (FSCompilerWindow), false, "F# Compiler");
	}

	void OnGUI () {
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Compile F#s")) {
			CompileFSs ();
		}
		if (GUILayout.Button ("Clear Build Log")) {
			buildLog = "";
			Repaint ();
		}
		GUILayout.EndHorizontal ();
		GUILayout.Label (buildLog);
	}


	void CompileFSs () {
		try {
			// get all F# source files
			string[] fsharps = Directory.GetFiles (Path.Combine ("Assets", "Scripts"), "*.fs");
			foreach (string file in fsharps) {
				string script = Path.GetFileNameWithoutExtension (file);
				string outputFile = Path.ChangeExtension (Path.Combine (FSCompilerOptions.outputDir, script), "dll");
				// only compile if source file is newer than it's dll
				if (File.GetLastWriteTime (file) > File.GetLastWriteTime (outputFile)) {
					Log ("Compiling " + script);
					FSCompilerProcess proc = new FSCompilerProcess (this);
					proc.Compile (script, file, outputFile);
				}
				else {
					Log (script + " is up to date");
				}
			}
		}
		catch (DirectoryNotFoundException ex) {
			Log ("Error on finding F#s: " + ex.Message);
		}
	}

	public void Log (string msg) {
		buildLog += msg + '\n';
	}
}
