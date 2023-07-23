namespace GirExt;

/// <summary>
/// Extensions for classes in Gdk namespace
/// </summary>
public static partial class GdkExt
{
    /// <summary>
    /// Extension method for Gdk.Clipboard to read text from clipboard
    /// </summary>
    /// <param name="clipboard">Clipboard</param>
    /// <exception>Thrown if failed to read</exception>
    /// <returns>Text from clipboard or null</returns>
    public static Task<string?> ReadTextAsync(this Gdk.Clipboard clipboard)
    {
        var tcs = new TaskCompletionSource<string?>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var stringHandle = Gdk.Internal.Clipboard.ReadTextFinish(sourceObject, res, out var error);
            if (!error.IsInvalid)
            {
                tcs.SetException(new Exception("Failed to read text from clipboard."));
            }
            else
            {
                var value = stringHandle.ConvertToString();
                tcs.SetResult(value);
            }
        }

        Gdk.Internal.Clipboard.ReadTextAsync(
            clipboard: clipboard.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}