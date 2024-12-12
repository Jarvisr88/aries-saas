namespace ActiproSoftware.WinUICore
{
    using System;
    using System.Windows.Forms;

    public interface IImageListProvider
    {
        ImageList GetImageList(object requestor, object context);
    }
}

