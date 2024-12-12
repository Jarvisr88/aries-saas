namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public interface ILiveCustomizationAreasProvider
    {
        void GetLiveCustomizationAreas(IList<Rect> areas, FrameworkElement relativeTo);
    }
}

