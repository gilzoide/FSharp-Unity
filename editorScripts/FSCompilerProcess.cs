using UnityEngine;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;

/// F# Compiler for the Unity Engine, running external command `fsharpc` or `fsc.exe`
public class FSCompilerProcess {
	/// Paths
	static string unityEngineDllRef = UnityEditor.EditorApplication.applicationContentsPath + "/Managed/UnityEngine.dll";
	static string unityEditorDllRef = UnityEditor.EditorApplication.applicationContentsPath + "/Managed/UnityEditor.dll";

	/// Our process, which will compile F# stuff
	Process proc = new Process ();
	/// The Process Start Info, with arguments, and stderr redirection
	ProcessStartInfo startInfo = new ProcessStartInfo (FSCompilerOptions.compilerCommand);

	/// The FSCompilerWindow, for logging
	FSCompilerWindow win;
	
	/// The script which will be compiled, without the extension (for logging)
	string script;
	
	/// Ctor
	/// @param win The FSCompilerWindow, to log
	public FSCompilerProcess (FSCompilerWindow win) {
		this.win = win;
		// please, let us know our errors
		startInfo.RedirectStandardError = true;
		startInfo.UseShellExecute = false;
		proc.StartInfo = startInfo;
		// and enable the Exited event to be raised
		proc.EnableRaisingEvents = true;
		proc.Exited += new EventHandler (WhenFSCExit);
	}
	
	/// Compile the specified filename
	/// @param filename F# source file
	public void Compile (string script, string inputFile, string outputFile) {
		this.script = script;
		string args = "-a " + inputFile + " -o " + outputFile + " -r:" + unityEngineDllRef + 
				" -r:" + unityEditorDllRef + ' ' + FSCompilerOptions.compilerAditionalArgs;
		startInfo.Arguments = args;
		proc.Start ();
		Log ("Running `" + FSCompilerOptions.compilerCommand + ' ' + args + '`');
	}
	
	/// Runs when fsc exits, informing us if there were errors
	private void WhenFSCExit (object sender, EventArgs e) {
		if (proc.ExitCode == 0) {
			Log ("Finished");
		}
		else {
			LogError (script + proc.StandardError.ReadToEnd ());
		}
	}
	
	/// FSCompilerProcess' Error Log
	void LogError (string msg) {
		UnityEngine.Debug.LogError ('[' + script + "] " + msg);
		Log ("Error! Check out the console");
	}
	
	/// FSCompilerProcess' Message Log
	void Log (string msg) {
		win.Log ("[FSC " + script + "] " + msg);
	}
}
