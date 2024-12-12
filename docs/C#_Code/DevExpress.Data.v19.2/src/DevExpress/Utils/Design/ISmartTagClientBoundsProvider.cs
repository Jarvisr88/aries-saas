namespace DevExpress.Utils.Design
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public interface ISmartTagClientBoundsProvider
    {
        Rectangle GetBounds(IComponent component);
        Control GetOwnerControl(IComponent component);
    }
}

