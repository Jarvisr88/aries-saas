namespace DevExpress.Utils.Serializing
{
    using System.Drawing;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    public interface IXtraSerializableLayout2 : IXtraSerializableLayout
    {
        SizeF LayoutScaleFactor { get; }
    }
}

