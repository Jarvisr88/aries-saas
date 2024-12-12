namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public interface IPageContentAlgorithm
    {
        void OnBeforeBuildPages();
        IList<Brick> Process(IListWrapper<Brick> bricks, RectangleF bounds);

        RectangleF ContentBounds { get; }

        bool ClipContent { get; }
    }
}

