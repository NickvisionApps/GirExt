using System;
using System.Threading.Tasks;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gdk namespace
/// </summary>
public static partial class GdkExt
{
    /// <summary>
    /// Extension method for Gdk.Clipboard to read text from clipboard
    /// </summary>
    /// <param name="clipboard">Clipboard</param>
    /// <exception cref="Exception">Thrown if failed to read</exception>
    /// <returns>Text from clipboard or null</returns>
    public static Task<string?> ReadTextAsync(this Gdk.Clipboard clipboard)
    {
        var tcs = new TaskCompletionSource<string?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var stringHandle = Gdk.Internal.Clipboard.ReadTextFinish(sourceObject.Handle, res.Handle, out var error);
                if (!error.IsInvalid)
                {
                    throw new Exception(error.ToString() ?? "");
                }
                if (stringHandle.IsInvalid)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = stringHandle.ConvertToString();
                    tcs.SetResult(value);
                }
            }
        });

        Gdk.Internal.Clipboard.ReadTextAsync(
            clipboard: clipboard.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}