using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class FSCompilerWindow : EditorWindow {
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
			string[] fsharps = Directory.GetFiles (Path.Combine ("Assets", "Scripts"), "*.fs");
			foreach (string file in fsharps) {
				Log ("Compiling " + Path.GetFileNameWithoutExtension (file));
				FSCompilerProcess proc = new FSCompilerProcess (this);
				proc.Compile (file);
			}
		}
		catch (DirectoryNotFoundException ex) {
			Log ("Falha ao procurar F#s: " + ex.Message);
		}
	}

	public void Log (string msg) {
		buildLog += msg + '\n';
	}
}
