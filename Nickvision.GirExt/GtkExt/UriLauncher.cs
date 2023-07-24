using System;
using System.Threading.Tasks;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gtk namespace
/// </summary>
public static partial class GtkExt
{
    /// <summary>
    /// Extension method for Gtk.UriLauncher to launch a URI
    /// </summary>
    /// <param name="launcher">URI launcher</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if failed to launch</exception>
    /// <returns>True if successful, else false</returns>
    public static Task<bool> LaunchAsync(this Gtk.UriLauncher launcher, Gtk.Window parent)
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
                var launchValue = Gtk.Internal.UriLauncher.LaunchFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    tcs.SetException(new Exception(error.ToString() ?? ""));
                    return;
                }
                tcs.SetResult(launchValue);
            }
        });

        Gtk.Internal.UriLauncher.Launch(
            self: launcher.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }
}
