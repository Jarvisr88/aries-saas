namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    internal class ProgressBarBrickCreator : BrickCreator
    {
        public ProgressBarBrickCreator(PrintingSystemBase ps, Dictionary<BrickStyleKey, BrickStyle> brickStyles, Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters) : base(ps, brickStyles, onPageUpdaters)
        {
        }

        public override VisualBrick Create(UIElement source, UIElement parent)
        {
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(parent, "parent");
            IProgressBarExportSettings exportSettings = new EffectiveProgressBarExportSettings(source);
            ProgressBarBrick brick = new ProgressBarBrick();
            base.InitializeBrickCore(source, parent, brick, exportSettings);
            brick.Position = exportSettings.Position;
            return brick;
        }
    }
}

