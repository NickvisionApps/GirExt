using System.Runtime.InteropServices;

namespace Nickvision.GirExt.Unity;

/// <summary>
/// Unity Launcher Entry
/// </summary>
public partial class LauncherEntry
{
    [LibraryImport("libunity.so.9", StringMarshalling = StringMarshalling.Utf8)]
    private static partial nint unity_launcher_entry_get_for_desktop_id(string desktop_id);
    [LibraryImport("libunity.so.9", StringMarshalling = StringMarshalling.Utf8)]
    private static partial void unity_launcher_entry_set_progress_visible(nint launcher, [MarshalAs(UnmanagedType.I1)] bool visibility);
    [LibraryImport("libunity.so.9", StringMarshalling = StringMarshalling.Utf8)]
    private static partial void unity_launcher_entry_set_progress(nint launcher, double progress);
    
    private readonly nint _handle;

    /// <summary>
    /// Visibility of launcher's progress bar
    /// </summary>
    public bool ProgressVisible
    {
        set => SetProgressVisible(value);
    }

    /// <summary>
    /// Value for progress bar (0.0-1.0)
    /// </summary>
    public double Progress
    {
        set => SetProgress(value);
    }

    /// <summary>
    /// Construct Launcher Entry
    /// </summary>
    /// <param name="desktopId">Desktop entry ID</param>
    private LauncherEntry(string desktopId)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            throw new PlatformNotSupportedException();
        }
        _handle = unity_launcher_entry_get_for_desktop_id(desktopId);
    }

    /// <summary>
    /// Get Launcher Entry for specified desktope entry
    /// </summary>
    /// <param name="desktopId">Desktop entry ID</param>
    /// <returns>new Launcher Entry</returns>
    public static LauncherEntry GetForDesktopID(string desktopId) => new LauncherEntry(desktopId);
    
    /// <summary>
    /// Set visibility of launcher's progress bar
    /// </summary>
    /// <param name="visibility">Whether or not progress bar should be visible</param>
    public void SetProgressVisible(bool visibility) => unity_launcher_entry_set_progress_visible(_handle, visibility);
    
    /// <summary>
    /// Set value for progress bar
    /// </summary>
    /// <param name="progress">Progress value from 0.0 to 1.0</param>
    public void SetProgress(double progress) => unity_launcher_entry_set_progress(_handle, progress);
}