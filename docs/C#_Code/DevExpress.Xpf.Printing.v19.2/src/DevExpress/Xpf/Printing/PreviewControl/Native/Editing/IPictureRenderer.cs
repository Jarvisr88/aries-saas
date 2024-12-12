namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Xpf.Editors;
    using System;

    public interface IPictureRenderer : INativeImageRenderer
    {
        void AssignEditor(NativeImage editor);
    }
}

