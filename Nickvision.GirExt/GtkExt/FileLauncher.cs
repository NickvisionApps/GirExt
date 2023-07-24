using System;
using System.Threading.Tasks;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gtk namespace
/// </summary>
public static partial class GtkExt
{
    /// <summary>
    /// Extension method for Gtk.FileLauncher to launch a file
    /// </summary>
    /// <param name="launcher">File launcher</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if failed to launch</exception>
    /// <returns>True if successful, else false</returns>
    public static Task<bool> LaunchAsync(this Gtk.FileLauncher launcher, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var launchValue = Gtk.Internal.FileLauncher.LaunchFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    tcs.SetException(new Exception(error.ToString() ?? ""));
                    return;
                }
                tcs.SetResult(launchValue);
            }
        });

        Gtk.Internal.FileLauncher.Launch(
            self: launcher.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }
    

    /// <summary>
    /// Extension method for Gtk.FileLauncher to open folder containing a file
    /// </summary>
    /// <param name="launcher">File launcher</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if failed to launch</exception>
    /// <returns>True if successful, else false</returns>
    public static Task<bool> OpenContainingFolderAsync(this Gtk.FileLauncher launcher, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var launchValue = Gtk.Internal.FileLauncher.OpenContainingFolderFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    tcs.SetException(new Exception(error.ToString() ?? ""));
                    return;
                }
                tcs.SetResult(launchValue);
            }
        });

        Gtk.Internal.FileLauncher.OpenContainingFolder(
            self: launcher.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }
}
