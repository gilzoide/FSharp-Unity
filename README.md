How to use F# libraries with Unity
==================================

The IDE project way
-------------------

Note: this has only been tested out with Xamarin Studio on Mac OS X

1. Create a Unity Project
2. In the Unity 'Assets' folder, create a folder called 'Frameworks' and add the FSharp.Core.dll to it
3. Create a F# project (lib) and put it in the root of the Unity project
4. Open the preferences for the F# project and change the 'Target Framework' to 'Mono / .NET 3.5'
5. Remove all References in the project, except FSharp.Core (2.3.0), System and System.Core
6. Add the unity mscorlib.dll to somewhere in the F# project folder, and then to the project References
7. Make a custom build command (working dir: ${TargetDir}) that copies the resulting dll to the Unity 'Frameworks' folder

```bash
    cp MyFsharpLib.dll ../../../../Assets/Frameworks/
```

The command line way
--------------------

Note: this has only been tested out on Linux (fsharpc command, which calls fsc.exe)

1. Create a Unity Project
2. Clone this repo in the root folder of your Unity Project
3. Call the prepare.sh script, that'll create the 'Assets/Frameworks' and 'Assets/Editor' folder, and copy the needed files
4. Put your F# source code (.fs file) in the 'Assets/Scripts' folder, edit it with your favorite text editor
5. Open the F# Compiler Window (from the 'F#/Compiler' menu, or with Shift+Ctrl+3)
6. Click the 'Compile F#s' button. Build errors are logged in the Editor's Console
7. Wait until complete. Dlls will be put in the 'Assets/Frameworks' folder


* You can access namespaces/classes from C# components just like you'd expect
* The component will show up under the DLL in Unity's Project view, click the little
arrow to fold it out (or just add it to a game object using the Add Component menu)

How to write Unity components in F#
===================================

* Set the namespace as the file name

```fsharp
namespace MyFsharpLib
```

* Import the Unity namespace

```fsharp
open UnityEngine
```

* Inherit from MonoBehaviour, as usual

```fsharp
type SimpleComponent() =
    inherit MonoBehaviour()
    member this.stuff = 42
```

* To show properties in the inspector, make them mutable and serializable

```fsharp
    [<SerializeField>]
    let mutable changeSpeed = 2.0f
```

* Override member functions

```fsharp
    member this.Start () = 
        this.transform.Translate(1.0f, -1.0f, 2.0f)
```

* Mutate members

```fsharp
    this.renderer.material.color <- Color.red
```

