namespace DevExpress.Utils.Design
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public interface ISmartTagClientBoundsProviderEx : ISmartTagClientBoundsProvider
    {
        ISmartTagGlyphObserver GetObserver(IComponent component, out Control relatedControl);
    }
}

