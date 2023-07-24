using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gtk namespace
/// </summary>
public static partial class GtkExt
{
    /// <summary>
    /// Extension method for Gtk.ColorDialog to choose a color
    /// </summary>
    /// <param name="dialog">Color dialog</param>
    /// <param name="parent">Parent window</param>
    /// <exception cref="Exception">Thrown if failed to choose a color</exception>
    /// <returns>Color struct if successful, or null</returns>
    public static Task<GdkExt.RGBA?> ChooseRgbaAsync(this Gtk.ColorDialog dialog, Gtk.Window parent)
    {
        var tcs = new TaskCompletionSource<GdkExt.RGBA?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var color = Gtk.Internal.ColorDialog.ChooseRgbaFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    tcs.SetException(new Exception(error.ToString() ?? ""));
                }
                else if (color.DangerousGetHandle() == IntPtr.Zero)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    tcs.SetResult(Marshal.PtrToStructure<GdkExt.RGBA>(color.DangerousGetHandle()));
                }
            }
        });

        Gtk.Internal.ColorDialog.ChooseRgba(
            self: dialog.Handle,
            parent: parent.Handle,
            initialColor: new Gdk.Internal.RGBAOwnedHandle(IntPtr.Zero),
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
            );
        
        return tcs.Task;
    }
}