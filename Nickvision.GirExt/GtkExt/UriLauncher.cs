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
    /// <exception>Thrown if failed to launch</exception>
    /// <returns>True if successful, else false</returns>
    public static Task<bool> LaunchAsync(this Gtk.UriLauncher launcher, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var value = Gtk.Internal.UriLauncher.LaunchFinish(sourceObject, res, out var error);
            if (!error.IsInvalid)
            {
                tcs.SetException(new Exception("Failed to launch a URI."));
            }
            else
            {
                tcs.SetResult(value);
            }
        }

        Gtk.Internal.UriLauncher.Launch(
            self: launcher.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
