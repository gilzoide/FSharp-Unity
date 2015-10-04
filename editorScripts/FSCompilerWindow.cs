using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;

public class FSCompilerWindow : EditorWindow {
	private static bool shouldRebuild = false;

	[MenuItem ("F#/Compile %3")]
	public static void ShowWindow () {
		EditorWindow.GetWindow (typeof (FSstuff));
		shouldRebuild = true;
	}

	void OnGUI () {
		if (shouldRebuild) {
			shouldRebuild = false;
			try {
				string[] fsharps = Directory.GetFiles (Path.Combine ("Assets", "Scripts"), "*.fs");
				foreach (string file in fsharps) {
					FSCompilerProcess proc = new FSCompilerProcess ();
					proc.Compile (file);
				}
			}
			catch (DirectoryNotFoundException ex) {
				Debug.Log ("Falha ao procurar F#s: " + ex.Message);
			}
		}
	}
}
