using UnityEngine;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;

/// <summary>
/// F# Compiler for the Unity Engine, running external command `fsharpc` or `fsc.exe`
/// </summary>
public class FSCompilerProcess {
	/// <summary>
	/// Our process, which will compile F# stuff
	/// </summary>
	private Process proc = new Process ();
	/// <summary>
	/// The Process Start Info, with arguments, and stderr redirection
	/// </summary>
	private ProcessStartInfo startInfo = new ProcessStartInfo ("fsharpc");
	
	private string outputDir = Path.Combine ("Assets", "Frameworks");
	private string unityEngineDllRef = Path.Combine (Path.Combine ("FSharp-Unity", "dlls"), "UnityEngine.dll");
	private string script;

	/// <summary>
	/// Initializes a new instance of the <see cref="FSCompilerProcess"/> class.
	/// </summary>
	public FSCompilerProcess () {
		// please, let us know our errors
		startInfo.RedirectStandardError = true;
		startInfo.UseShellExecute = false;
		proc.StartInfo = startInfo;
		// and enable the Exited event to be raised
		proc.EnableRaisingEvents = true;
		proc.Exited += new EventHandler (WhenFSCExit);
	}

	/// <summary>
	/// Compile the specified filename
	/// </summary>
	/// <param name="filename">Filename: F# source code</param>
	public void Compile (string filename) {
		script = Path.GetFileNameWithoutExtension (filename);
		Log (script + " got");
		string outfile = Path.Combine (outputDir, Path.ChangeExtension (script, "dll"));
		string args = "-a " + filename + " -o " + outfile + " -r:" + unityEngineDllRef;
		startInfo.Arguments = args;
		Log ("Running `fsharpc " + args + '`');
		proc.Start ();
	}

	/// <summary>
	/// Runs when fsc exits, informing us if there were errors
	/// </summary>
	private void WhenFSCExit (object sender, EventArgs e) {
		if (proc.ExitCode == 0) {
			Log (script + " finished");
		}
		else {
			LogError (script + proc.StandardError.ReadToEnd ());
		}
	}

	/// <summary>
	/// FSCompilerProcess' Error Log
	/// </summary>
	private void LogError (string msg) {
		UnityEngine.Debug.LogError ("[FSCompiler] " + msg);
	}

	/// <summary>
	/// FSCompilerProcess' Message Log
	/// </summary>
	private void Log (string msg) {
		UnityEngine.Debug.Log ("[FSCompiler] " + msg);
	}
}
