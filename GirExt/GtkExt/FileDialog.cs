using System;
using System.Threading.Tasks;

namespace GirExt;

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
    /// <exception>Thrown if failed to open a file</exception>
    /// <returns>File if successfull, or null</returns>
    public static Task<Gio.File?> OpenAsync(this Gtk.FileDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var fileValue = Gtk.Internal.FileDialog.OpenFinish(sourceObject, res, out var error);
            if (!error.IsInvalid)
            {
                tcs.SetException(new Exception("Failed to open a file."));
            }
            else
            {
                var value = new Gio.FileHelper(fileValue, true);
                tcs.SetResult(value);
            }
        }

        Gtk.Internal.FileDialog.Open(
            self: dialog.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }

    /// <summary>
    /// Extension method for Gtk.FileDialog to save a file
    /// </summary>
    /// <param name="dialog">File dialog</param>
    /// <param name="parent">Parent window</param>
    /// <exception>Thrown if failed to save a file</exception>
    /// <returns>File if successfull, or null</returns>
    public static Task<Gio.File?> SaveAsync(this Gtk.FileDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var fileValue = Gtk.Internal.FileDialog.SaveFinish(sourceObject, res, out var error);
            if (!error.IsInvalid)
            {
                tcs.SetException(new Exception("Failed to save a file."));
            }
            else
            {
                var value = new Gio.FileHelper(fileValue, true);
                tcs.SetResult(value);
            }
        }

        Gtk.Internal.FileDialog.Save(
            self: dialog.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }

    /// <summary>
    /// Extension method for Gtk.FileDialog to select a folder
    /// </summary>
    /// <param name="dialog">File dialog</param>
    /// <param name="parent">Parent window</param>
    /// <exception>Thrown if failed to select a folder</exception>
    /// <returns>File if successfull, or null</returns>
    public static Task<Gio.File?> SelectFolderAsync(this Gtk.FileDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var fileValue = Gtk.Internal.FileDialog.SelectFolderFinish(sourceObject, res, out var error);
            if (!error.IsInvalid)
            {
                tcs.SetException(new Exception("Failed to select a folder."));
            }
            else
            {
                var value = new Gio.FileHelper(fileValue, true);
                tcs.SetResult(value);
            }
        }

        Gtk.Internal.FileDialog.SelectFolder(
            self: dialog.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
            );

        return tcs.Task;
    }
}