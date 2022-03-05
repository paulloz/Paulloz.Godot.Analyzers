# Some analyzers for Godot

## Compiling

```powershell
$> dotnet build -C Release .\src\Paulloz.Godot.Analyzers\
```

## Usage

### VSCode

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
