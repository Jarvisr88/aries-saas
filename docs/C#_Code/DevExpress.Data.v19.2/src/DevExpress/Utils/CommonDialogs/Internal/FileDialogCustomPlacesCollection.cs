namespace DevExpress.Utils.CommonDialogs.Internal
{
    using System;
    using System.Collections.ObjectModel;

    public class FileDialogCustomPlacesCollection : Collection<FileDialogCustomPlace>
    {
        public void Add(Guid knownFolderGuid)
        {
            base.Add(new FileDialogCustomPlace(knownFolderGuid));
        }

        public void Add(string path)
        {
            base.Add(new FileDialogCustomPlace(path));
        }
    }
}

