using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class FSCompilerWindow : EditorWindow {
	/// The build log
	string buildLog;
	/// The scrolling Vector2
	Vector2 scrollPosition;

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

		scrollPosition = GUILayout.BeginScrollView (scrollPosition);
		GUILayout.Label (buildLog);
		GUILayout.EndScrollView ();
	}


	void CompileFSs () {
		try {
			// get all F# source files
			string[] fsharps = Directory.GetFiles (FSCompilerOptions.inputDir, "*.fs");
			foreach (string file in fsharps) {
				string script = Path.GetFileNameWithoutExtension (file);
				string outputFile = Path.ChangeExtension (Path.Combine (FSCompilerOptions.outputDir, script), "dll");
				// only compile if source file is newer than it's dll
				if (File.GetLastWriteTime (file) > File.GetLastWriteTime (outputFile)) {
					Log ("Compiling " + script);
					FSCompilerProcess proc = new FSCompilerProcess (this);
					proc.Compile (script, file, outputFile);
				}
				else if (FSCompilerOptions.outputUpToDate) {
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
