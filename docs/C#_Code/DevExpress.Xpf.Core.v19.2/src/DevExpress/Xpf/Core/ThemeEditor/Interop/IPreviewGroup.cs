namespace DevExpress.Xpf.Core.ThemeEditor.Interop
{
    using System.Collections.Generic;

    public interface IPreviewGroup : IPreview
    {
        List<IPreview> Previews { get; }
    }
}

