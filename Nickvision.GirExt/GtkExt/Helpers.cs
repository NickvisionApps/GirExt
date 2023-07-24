using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Nickvision.GirExt;

/// <summary>
/// Extensions for classes in Gtk namespace
/// </summary>
public unsafe static partial class GtkExt 
{
    [StructLayout(LayoutKind.Sequential)]
    private struct GLibList
    {
        public nint data;
        public GLibList* next;
        public GLibList* prev;
    }

    [LibraryImport("gtk")]
    private static partial void gtk_color_dialog_button_set_rgba(nint button, ref GdkExt.RGBA rgba);
    [LibraryImport("gtk")]
    private static partial GLibList* gtk_list_box_get_selected_rows(nint box);
    [LibraryImport("gtk")]
    private static partial int gtk_list_box_row_get_index(nint row);

    /// <summary>
    /// Helper extension method for Gtk.ColorButton to get color as GdkExt.RGBA
    /// </summary>
    /// <param name="button">Color button</param>
    /// <returns>Color as GdkExt.RGBA</returns>
    public static GdkExt.RGBA GetExtRgba(this Gtk.ColorDialogButton button)
    {
        Gdk.RGBA gdkColor = button.GetRgba();
        return Marshal.PtrToStructure<GdkExt.RGBA>(gdkColor.Handle.DangerousGetHandle());
    }

    /// <summary>
    /// Helper extension method for Gtk.ColorButton to set color as GdkExt.RGBA
    /// </summary>
    /// <param name="button">Color button</param>
    /// <param name="color">Color as GdkExt.RGBA</param>
    public static void SetExtRgba(this Gtk.ColorDialogButton button, GdkExt.RGBA color)
    {
        Resolver.SetResolver();
        gtk_color_dialog_button_set_rgba(button.Handle, ref color);
    }

    /// <summary>
    /// Helper extension method for Gtk.ListBox to get indices of selected row
    /// </summary>
    /// <param name="box">List box</param>
    /// <returns>List of indices</returns>
    public static List<int> GetSelectedRowsIndices(this Gtk.ListBox box)
    {
        Resolver.SetResolver();
        var list = new List<int>();
        var firstSelectedRowPtr = gtk_list_box_get_selected_rows(box.Handle);
        for(var ptr = firstSelectedRowPtr; ptr != null; ptr = ptr->next)
        {
            list.Add(gtk_list_box_row_get_index(ptr->data));
        }
        return list;
    }
}
