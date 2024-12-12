namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.Drawing.ImageItem"), TypeConverter(typeof(ImageItemTypeConverter)), Editor("DevExpress.XtraReports.Design.ImageItemEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
    public class ImageItem : IDisposable
    {
        private string id;

        internal event EventHandler<ImageItemIdChangingEventArgs> IdChanging;

        internal ImageItem()
        {
        }

        public ImageItem(string id, DevExpress.XtraPrinting.Drawing.ImageSource imageSource)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }
            this.Id = id;
            this.ImageSource = imageSource;
        }

        public void Dispose()
        {
            if (this.ImageSource != null)
            {
                this.ImageSource.Dispose();
                this.ImageSource = null;
            }
        }

        private void SetId(string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, this.id))
            {
                ImageItemIdChangingEventArgs e = new ImageItemIdChangingEventArgs(value);
                if (this.IdChanging != null)
                {
                    this.IdChanging(this, e);
                }
                if (!e.Cancel)
                {
                    this.id = value;
                }
            }
        }

        [XtraSerializableProperty, Description(""), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageItem.Id")]
        public string Id
        {
            get => 
                this.id;
            set => 
                this.SetId(value);
        }

        [DefaultValue((string) null), XtraSerializableProperty, Description(""), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ImageItem.ImageSource")]
        public DevExpress.XtraPrinting.Drawing.ImageSource ImageSource { get; set; }
    }
}

