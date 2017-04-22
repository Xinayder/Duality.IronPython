# Duality.IronPython [![NuGet](https://img.shields.io/nuget/v/RockyTV.Duality.Plugins.IronPython.svg)](https://www.nuget.org/packages/RockyTV.Duality.Plugins.IronPython/1.0.1)
From their own website:

> IronPython is an open-source implementation of the Python programming language which is tightly integrated with the .NET Framework.
> IronPython can use the .NET Framework and Python libraries, and other .NET languages can use Python code just as easily.

This is a [Duality](https://github.com/adamslair/duality) plugin that allows you to write your game using Python.

The icons used are part of [Mark James](https://twitter.com/markjames)' [Silk Icons](http://famfamfam.com/lab/icons/silk/) collection.

## Usage

1. Create a `GameObject`
2. Add a `ScriptExecutor` component (`Scripts -> ScriptExecutor`)
3. Create a `PythonScript` resource in Project View (or just drag and drop a Python file)
4. Drag the `PythonScript` to the `Script` property of the `ScriptExecutor`
5. Have fun!
