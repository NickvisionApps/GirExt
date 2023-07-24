using System;
using System.Threading.Tasks;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gtk namespace
/// </summary>
public static partial class GtkExt
{
    /// <summary>
    /// Extension method for Gtk.FileDialog to open a file
    /// </summary>
    /// <param name="dialog">File dialog</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if failed to open a file</exception>
    /// <returns>File if successful, or null</returns>
    public static Task<Gio.File?> OpenAsync(this Gtk.FileDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var fileValue = Gtk.Internal.FileDialog.OpenFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    throw new Exception(error.ToString() ?? "");
                }
                if (fileValue == IntPtr.Zero)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = new Gio.FileHelper(fileValue, true);
                    tcs.SetResult(value);
                }
            }
        });

        Gtk.Internal.FileDialog.Open(
            self: dialog.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }

    /// <summary>
    /// Extension method for Gtk.FileDialog to save a file
    /// </summary>
    /// <param name="dialog">File dialog</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if failed to save a file</exception>
    /// <returns>File if successful, or null</returns>
    public static Task<Gio.File?> SaveAsync(this Gtk.FileDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var fileValue = Gtk.Internal.FileDialog.SaveFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    throw new Exception(error.ToString() ?? "");
                }
                if (fileValue == IntPtr.Zero)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = new Gio.FileHelper(fileValue, true);
                    tcs.SetResult(value);
                }
            }
        });

        Gtk.Internal.FileDialog.Save(
            self: dialog.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }

    /// <summary>
    /// Extension method for Gtk.FileDialog to select a folder
    /// </summary>
    /// <param name="dialog">File dialog</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if failed to select a folder</exception>
    /// <returns>File if successful, or null</returns>
    public static Task<Gio.File?> SelectFolderAsync(this Gtk.FileDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var fileValue = Gtk.Internal.FileDialog.SelectFolderFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    throw new Exception(error.ToString() ?? "");
                }
                if (fileValue == IntPtr.Zero)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = new Gio.FileHelper(fileValue, true);
                    tcs.SetResult(value);
                }
            }
        });

        Gtk.Internal.FileDialog.SelectFolder(
            self: dialog.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }
}
