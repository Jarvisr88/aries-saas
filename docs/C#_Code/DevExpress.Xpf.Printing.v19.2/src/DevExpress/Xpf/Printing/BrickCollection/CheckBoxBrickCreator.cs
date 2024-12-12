namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Forms;

    internal class CheckBoxBrickCreator : BrickCreator
    {
        public CheckBoxBrickCreator(PrintingSystemBase ps, Dictionary<BrickStyleKey, BrickStyle> brickStyles, Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters) : base(ps, brickStyles, onPageUpdaters)
        {
        }

        private CheckState ConvertToCheckState(bool? value) => 
            (value == null) ? CheckState.Indeterminate : (value.Value ? CheckState.Checked : CheckState.Unchecked);

        public override VisualBrick Create(UIElement source, UIElement parent)
        {
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(parent, "parent");
            IBooleanExportSettings exportSettings = new EffectiveBooleanExportSettings(source);
            XECheckBoxBrick brick = new XECheckBoxBrick();
            base.InitializeBrickCore(source, parent, brick, exportSettings);
            brick.CheckState = this.ConvertToCheckState(exportSettings.BooleanValue);
            brick.CheckText = exportSettings.CheckText;
            return brick;
        }
    }
}

