namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class CheckBoxGlyph : IXtraSupportAfterDeserialize
    {
        private DevExpress.XtraPrinting.Native.ImageEntry imageEntry;

        internal CheckBoxGlyph()
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
        }

        public CheckBoxGlyph(System.Windows.Forms.CheckState checkState, DevExpress.XtraPrinting.Drawing.ImageSource imageSource)
        {
            this.imageEntry = new DevExpress.XtraPrinting.Native.ImageEntry();
            this.CheckState = checkState;
            this.ImageSource = imageSource;
        }

        void IXtraSupportAfterDeserialize.AfterDeserialize(XtraItemEventArgs e)
        {
            if (e.Item.Name == "ImageEntry")
            {
                DocumentSerializationOptions.AddImageEntryToCache(e);
            }
        }

        [XtraSerializableProperty]
        public System.Windows.Forms.CheckState CheckState { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.Cached)]
        public DevExpress.XtraPrinting.Native.ImageEntry ImageEntry
        {
            get => 
                this.imageEntry;
            set => 
                this.imageEntry = value;
        }

        public DevExpress.XtraPrinting.Drawing.ImageSource ImageSource
        {
            get => 
                this.imageEntry.ImageSource;
            set => 
                this.imageEntry.ImageSource = value;
        }
    }
}

