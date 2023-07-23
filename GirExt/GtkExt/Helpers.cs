using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GirExt;

/// <summary>
/// Extensions for classes in Gtk namespace
/// </summary>
public unsafe static partial class GtkExt {
    [StructLayout(LayoutKind.Sequential)]
    private struct GLibList
    {
        public nint data;
        public GLibList* next;
        public GLibList* prev;
    }

    [LibraryImport("gtk")]
    private static partial GLibList* gtk_list_box_get_selected_rows(nint box);
    [LibraryImport("gtk")]
    private static partial int gtk_list_box_row_get_index(nint row);
    
    /// <summary>
    /// Helper extension method for Gtk.ListBox to get indeces of selected row
    /// </summary>
    /// <param name="box">List box</param>
    /// <returns>List of indeces</returns>
    public static List<int> GetSelectedRowsIndeces(this Gtk.ListBox box)
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