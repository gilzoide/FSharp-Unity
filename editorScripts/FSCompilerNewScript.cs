using UnityEngine;
using UnityEditor;
using System.IO;

/// New Script utility
public class FSCompilerNewScript {
	[MenuItem ("F#/New Script")]
	public static void NewFSScript () {
		string path = EditorUtility.SaveFilePanelInProject ("New F# Script", "myFsScript", "fs", "oie");
		string script = Path.GetFileNameWithoutExtension (path);
		string[] strs = new string[] {"namespace " + script + '\n', "open UnityEngine\n",
				"type " + script + " () =", "    inherit MonoBehaviour ()\n",
				"    member this.Start () = ()\n", "    member this.Update () = ()"};
		File.WriteAllLines (path, strs);
	}
}
