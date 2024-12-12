namespace DevExpress.Utils.Design
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DesignTimeBoundsProviderAttribute : Attribute
    {
        public virtual Rectangle GetBounds(object obj) => 
            Rectangle.Empty;

        public virtual Control GetOwnerControl(object obj) => 
            null;

        public virtual bool ShouldDrawSelection =>
            true;
    }
}

