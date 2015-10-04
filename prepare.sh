#!/bin/sh

editorDir=../Assets/Editor
frameworksDir=../Assets/Frameworks

mkdir -p $editorDir
mkdir -p $frameworksDir

echo Copying Editor scripts
cp editorScripts/*.cs $editorDir
echo Copying FSharp.Core.dll
cp dlls/FSharp.Core.dll $frameworksDir
echo Done

exit
