namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    internal class PageInfoBrickCreator : BrickCreator
    {
        public PageInfoBrickCreator(PrintingSystemBase ps, Dictionary<BrickStyleKey, BrickStyle> brickStyles, Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters) : base(ps, brickStyles, onPageUpdaters)
        {
        }

        public override VisualBrick Create(UIElement source, UIElement parent)
        {
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(parent, "parent");
            IPageNumberExportSettings exportSettings = new EffectivePageNumberExportSettings(source);
            PageInfoTextBrick brick = new PageInfoTextBrick();
            base.InitializeBrickCore(source, parent, brick, exportSettings);
            brick.Format = exportSettings.Format;
            brick.PageInfo = exportSettings.Kind.ToPageInfo();
            return brick;
        }
    }
}

