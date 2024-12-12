namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    internal class TrackBarBrickCreator : BrickCreator
    {
        public TrackBarBrickCreator(PrintingSystemBase ps, Dictionary<BrickStyleKey, BrickStyle> brickStyles, Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters) : base(ps, brickStyles, onPageUpdaters)
        {
        }

        public override VisualBrick Create(UIElement source, UIElement parent)
        {
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(parent, "parent");
            ITrackBarExportSettings exportSettings = new EffectiveTrackBarExportSettings(source);
            TrackBarBrick brick = new TrackBarBrick(exportSettings.Position, exportSettings.Minimum, exportSettings.Maximum);
            base.InitializeBrickCore(source, parent, brick, exportSettings);
            return brick;
        }
    }
}

