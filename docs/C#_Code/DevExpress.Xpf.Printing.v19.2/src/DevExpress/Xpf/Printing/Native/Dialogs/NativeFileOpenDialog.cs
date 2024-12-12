namespace DevExpress.Xpf.Printing.Native.Dialogs
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [ComImport, Browsable(false), EditorBrowsable(EditorBrowsableState.Never), CoClass(typeof(FileOpenDialogRCW)), Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
    internal interface NativeFileOpenDialog : IFileDialog
    {
    }
}

