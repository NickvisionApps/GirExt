using System;
using System.Threading.Tasks;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gtk namespace
/// </summary>
public static partial class GtkExt
{
    /// <summary>
    /// Extension method to show Gtk.AlertDialog
    /// </summary>
    /// <param name="dialog">Alert dialog</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if the dialog was cancelled</exception>
    /// <returns>Index of a button that was clicked or -1 if the dialog was cancelled and CancelButton is not set</returns>
    public static Task<int> ChooseAsync(this Gtk.AlertDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<int>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var index = Gtk.Internal.AlertDialog.ChooseFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    tcs.SetException(new Exception(error.ToString() ?? ""));
                }
                else
                {
                    tcs.SetResult(index);
                }
            }
        });
        
        Gtk.Internal.AlertDialog.Choose(
            self: dialog.Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );
        
        return tcs.Task;
    }
}