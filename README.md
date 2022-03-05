# Some Roslyn analyzers for Godot

This repository provides custom Roslyn analyzers for Godot.

## Analyzers

Rule ID | Category    | Default severity | Description
--------|-------------|------------------|-------------
GD0001  | Type Safety | Info             | Prefer the generic versions of `GetNode`, `GetNodeOrNull`, `GetChild`, `GetParent`, and `GetOwner` 
GD0002  | Usage	      | Warning          | Check if exported fields/properties are marshallable

## Suppressors

Suppressor ID | Description
--------------|-------------
GDSP0001      | Suppresses IDE0044 on exported members
GDSP0002      | Suppresses IDE0051 on exported members
GDSP0003      | Suppresses CA1823 on exported members
GDSP0004      | Suppresses CS0649 on exported members

## Compiling

```powershell
$> dotnet build -C Release .\src\Paulloz.Godot.Analyzers\
```

You may need to re-generate `Strings.Designer.cs`.
```powershell
$> cd .\src\Paulloz.Godot.Analyzers\Resources\
$> resgen Strings.resx /str:cs,Paulloz.Godot.Analyzers.Resources,,Strings.Designer.cs
```

## Usage

### Visual Studio Code

* Set `omnisharp.enableRoslynAnalyzers` to `true`
* Drop `Paulloz.Godot.Analyzers.dll` somewhere in your workspace
* At the root of your workspace, create a file named `omnisharp.json` containing the following:

```json
{
    "RoslynExtensionsOptions": {
        "EnableAnalyzersSupport": true,
        "LocationPaths": [
            "path/to/the/folder/containing/the/dll"
        ]
    }
}
```

* Restart OmniSharp
* Enjoy :shipit:


### Visual Studio

Coming soon...
