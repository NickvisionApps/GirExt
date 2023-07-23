[![Nuget](https://img.shields.io/nuget/v/Nickvision.GirExt)](https://www.nuget.org/packages/Nickvision.GirExt/)

# Nickvision.GirExt

<img width='128' height='128' alt='Logo' src='Nickvision.GirExt/Resources/logo-r.png'/>

 **Extensions library for gir.core**

[gir.core](https://github.com/gircore/gir.core) provides C# bindings for GObject based libraries like GTK, and while it is an awesome tool, it's not complete. It misses some methods that you might want to use in your apps, and this is the case for Nickvision applications. The goal of **GirExt** is to provide missing parts and helper functions to make writing gir.core apps easier today.

Currently implemented:

* Gdk
  * Clipboard
    * ReadTextAsync
* Gtk
  * FileDialog
    * OpenAsync
    * SaveAsync
    * SelectFolderAsync
  * FileLauncher
    * LaunchAsync
    * OpenContainingFolderAsync
  * UriLauncher
    * LaunchAsync
  * Helpers<sup>[1]</sup>
    * ListBox
      * GetSelectedRowsIndices

<sup>[1]</sup> Helpers are functions that do not follow gir.core API, they are workarounds created to avoid complexity that is required for proper implementation of related methods.

If you develop apps with gir.core and found something missing that is not implemented here, feel free to open an issue with request or send a PR with your implementation.

<!--# Installation
<a href='https://www.nuget.org/packages/Nickvision.MPVSharp/'><img width='140' alt='Download on Nuget' src='https://www.nuget.org/Content/gallery/img/logo-header.svg'/></a>-->

# Example

```csharp
using static Nickvision.GirExt.GtkExt;
...
try
{
    var file = await fileDialog.OpenAsync(window);
    var fileLauncher = Gtk.FileLauncher.New(file);
    await fileLauncher.LaunchAsync(window);
}
catch
{
    Console.WriteLine("Failed to open a file.");
}
```
