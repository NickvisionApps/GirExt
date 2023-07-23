namespace GirExt;

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
    /// <exception>Thrown if failed to launch</exception>
    /// <returns>True if successfull, else false</returns>
    public static Task<bool> LaunchAsync(this Gtk.FileLauncher launcher, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var value = Gtk.Internal.FileLauncher.LaunchFinish(sourceObject, res, out var error);
            if (!error.IsInvalid)
            {
                tcs.SetException(new Exception("Failed to launch a file."));
            }
            else
            {
                tcs.SetResult(value);
            }
        }

        Gtk.Internal.FileLauncher.Launch(
            self: launcher.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }
    

    /// <summary>
    /// Extension method for Gtk.FileLauncher to open folder containg a file
    /// </summary>
    /// <param name="launcher">File launcher</param>
    /// <param name="parent">Parent window</param>
    /// <exception>Thrown if failed to launch</exception>
    /// <returns>True if successfull, else false</returns>
    public static Task<bool> OpenContainingFolderAsync(this Gtk.FileLauncher launcher, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var value = Gtk.Internal.FileLauncher.OpenContainingFolderFinish(sourceObject, res, out var error);
            if (!error.IsInvalid)
            {
                tcs.SetException(new Exception("Failed to open containing folder."));
            }
            else
            {
                tcs.SetResult(value);
            }
        }

        Gtk.Internal.FileLauncher.OpenContainingFolder(
            self: launcher.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}