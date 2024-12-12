namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public interface IIconStorage
    {
        bool TryStoreIconToFile(ImageSource icon, string storageFolder, out string iconID, out string iconPath);
    }
}

