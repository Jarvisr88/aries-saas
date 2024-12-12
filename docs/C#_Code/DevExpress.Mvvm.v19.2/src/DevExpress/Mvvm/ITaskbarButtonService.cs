namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shell;

    public interface ITaskbarButtonService
    {
        void UpdateThumbnailClipMargin();

        double ProgressValue { get; set; }

        TaskbarItemProgressState ProgressState { get; set; }

        ImageSource OverlayIcon { get; set; }

        string Description { get; set; }

        IList<TaskbarThumbButtonInfo> ThumbButtonInfos { get; }

        Thickness ThumbnailClipMargin { get; set; }

        Func<Size, Thickness> ThumbnailClipMarginCallback { get; set; }
    }
}

