namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.Windows.Forms;

    internal class FileDialogCustomPlacesCollectionWrapper : DevExpress.Utils.CommonDialogs.Internal.FileDialogCustomPlacesCollection
    {
        private readonly System.Windows.Forms.FileDialogCustomPlacesCollection nativeDialogCollection;

        public FileDialogCustomPlacesCollectionWrapper(System.Windows.Forms.FileDialogCustomPlacesCollection nativeDialogCollection)
        {
            this.nativeDialogCollection = nativeDialogCollection;
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            this.nativeDialogCollection.Clear();
        }

        private System.Windows.Forms.FileDialogCustomPlace Convert(DevExpress.Utils.CommonDialogs.Internal.FileDialogCustomPlace item) => 
            !(item.KnownFolderGuid != Guid.Empty) ? new System.Windows.Forms.FileDialogCustomPlace(item.Path) : new System.Windows.Forms.FileDialogCustomPlace(item.KnownFolderGuid);

        protected override void InsertItem(int index, DevExpress.Utils.CommonDialogs.Internal.FileDialogCustomPlace item)
        {
            base.InsertItem(index, item);
            this.nativeDialogCollection.Insert(index, this.Convert(item));
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            this.nativeDialogCollection.RemoveAt(index);
        }

        protected override void SetItem(int index, DevExpress.Utils.CommonDialogs.Internal.FileDialogCustomPlace item)
        {
            base.SetItem(index, item);
            this.nativeDialogCollection[index] = this.Convert(item);
        }
    }
}

