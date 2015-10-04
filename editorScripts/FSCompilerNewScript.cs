using UnityEngine;
using UnityEditor;
using System.IO;

/// New Script utility
public class FSCompilerNewScript {
	[MenuItem ("F#/New Script")]
	public static void NewFSScript () {
		string path = EditorUtility.SaveFilePanelInProject ("New F# Script", "myFsScript", "fs", "oie");
		string script = Path.GetFileNameWithoutExtension (path);
		File.WriteAllText (path, "namespace " + script +
				"\n\nopen UnityEngine\n\ntype " + script + " () =\n    inherit MonoBehaviour ()\n\n    member this.Start () = ()\n\n    member this.Update () = ()\n");
	}
}
