namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Text;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(CollectionTypeConverter)), Editor("DevExpress.XtraReports.Design.ImageItemCollectionEditor, DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor))]
    public class ImageItemCollection : Collection<ImageItem>, IDisposable, IStringImageSourceProvider, IStringImageProvider
    {
        public void Add(string id, ImageSource imageSource)
        {
            base.Add(new ImageItem(id, imageSource));
        }

        public void AddRange(ImageItem[] items)
        {
            foreach (ImageItem item in items)
            {
                base.Add(item);
            }
        }

        public bool Contains(string id) => 
            base.Items.Any<ImageItem>(x => x.Id == id);

        internal void CopyFrom(Collection<ImageItem> source)
        {
            ImageItem[] array = new ImageItem[source.Count];
            source.CopyTo(array, 0);
            base.Clear();
            this.AddRange(array);
        }

        Image IStringImageProvider.GetImage(string id)
        {
            ImageSource imageSource = this.GetImageSource(id);
            if (imageSource != null)
            {
                return imageSource.Image;
            }
            ImageSource local1 = imageSource;
            return null;
        }

        public void Dispose()
        {
            if (this.DisposeImages)
            {
                for (int i = base.Count - 1; i >= 0; i--)
                {
                    base[i].Dispose();
                }
            }
            base.Clear();
        }

        public ImageSource GetImageSource(string id)
        {
            ImageItem item = this.FirstOrDefault<ImageItem>(x => x.Id == id);
            return item?.ImageSource;
        }

        protected override void InsertItem(int index, ImageItem item)
        {
            if (!this.Contains(item.Id))
            {
                base.InsertItem(index, item);
                item.IdChanging += new EventHandler<ImageItemIdChangingEventArgs>(this.Item_IdChanging);
            }
        }

        private void Item_IdChanging(object sender, ImageItemIdChangingEventArgs e)
        {
            e.Cancel = base.Items.Any<ImageItem>(x => x.Id == e.Id);
        }

        protected override void RemoveItem(int index)
        {
            base.Items[index].IdChanging -= new EventHandler<ImageItemIdChangingEventArgs>(this.Item_IdChanging);
            base.RemoveItem(index);
        }

        internal bool DisposeImages { get; set; }
    }
}

