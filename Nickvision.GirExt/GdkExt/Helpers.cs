using System.Runtime.InteropServices;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gdk namespace
/// </summary>
public static partial class GdkExt
{
    [LibraryImport("gtk", StringMarshalling = StringMarshalling.Utf8)] // Using "gdk" doesn't work here for some reason
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool gdk_rgba_parse(out RGBA rgba, string spec);
    
    /// <summary>
    /// Helper RGBA struct. Used instead of Gdk.RGBA
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RGBA
    {
        /// <summary>
        /// Red channel (0.0-1.0)
        /// </summary>
        public float Red;
        /// <summary>
        /// Green channel (0.0-1.0)
        /// </summary>
        public float Green;
        /// <summary>
        /// Blue channel (0.0-1.0)
        /// </summary>
        public float Blue;
        /// <summary>
        /// Alpha channel (0.0-1.0)
        /// </summary>
        public float Alpha;

        /// <summary>
        /// Helper method to parse color string to GdkExt.RGBA struct
        /// </summary>
        /// <param name="colorRGBA">Struct to write to</param>
        /// <param name="spec">Color string</param>
        /// <returns>Whether or not the string was parsed successfully</returns>
        public static bool Parse(out RGBA? colorRGBA, string spec)
        {
            Resolver.SetResolver();
            if(gdk_rgba_parse(out var val, spec))
            {
                colorRGBA = val;
                return true;
            }
            colorRGBA = null;
            return false;
        }
    }
}