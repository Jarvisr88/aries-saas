namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Core;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class TextBrickCreator : BrickCreator
    {
        public TextBrickCreator(PrintingSystemBase ps, Dictionary<BrickStyleKey, BrickStyle> brickStyles, Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters) : base(ps, brickStyles, onPageUpdaters)
        {
        }

        public override VisualBrick Create(UIElement source, UIElement parent)
        {
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(parent, "parent");
            ITextExportSettings exportSettings = new EffectiveTextExportSettings(source);
            TextBrick brick = exportSettings.NoTextExport ? new NoTextExportTextBrick() : new TextBrick();
            base.InitializeBrickCore(source, parent, brick, exportSettings);
            brick.Text = exportSettings.Text ?? ((source is TextBlock) ? ((string) source.GetValue(TextBlock.TextProperty)) : null);
            brick.TextValue = exportSettings.TextValue;
            brick.TextValueFormatString = exportSettings.TextValueFormatString;
            brick.XlsExportNativeFormat = exportSettings.XlsExportNativeFormat.ToDefaultBoolean();
            brick.XlsxFormatString = exportSettings.XlsxFormatString;
            return brick;
        }

        protected override Rect GetElementRect(UIElement element, UIElement parent)
        {
            Rect elementRect = base.GetElementRect(element, parent);
            if (element is TextBlock)
            {
                if (DependencyPropertyHelper.GetValueSource(element, FrameworkElement.WidthProperty).BaseValueSource > BaseValueSource.Default)
                {
                    elementRect.Width = ((FrameworkElement) element).Width;
                }
                if (DependencyPropertyHelper.GetValueSource(element, FrameworkElement.HeightProperty).BaseValueSource > BaseValueSource.Default)
                {
                    elementRect.Height = ((FrameworkElement) element).Height;
                }
            }
            return elementRect;
        }
    }
}

