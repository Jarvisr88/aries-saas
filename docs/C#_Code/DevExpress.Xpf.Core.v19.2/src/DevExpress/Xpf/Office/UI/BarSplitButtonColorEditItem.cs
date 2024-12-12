namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class BarSplitButtonColorEditItem : BarButtonColorEditItem
    {
        static BarSplitButtonColorEditItem()
        {
            BarItemLinkCreator.Default.RegisterObject(typeof(BarSplitButtonColorEditItem), typeof(BarSplitButtonColorEditItemLink), (CreateObjectMethod<BarItemLink>) (arg => new BarSplitButtonColorEditItemLink()));
            BarSplitButtonEditItem.EditValueProperty.OverrideMetadata(typeof(BarSplitButtonColorEditItem), new FrameworkPropertyMetadata(Colors.Black));
        }

        public BarSplitButtonColorEditItem()
        {
            this.InitializeGlyphTemplate();
            base.ActAsDropDown = false;
            base.UseLightweightTemplates = false;
        }

        private void InitializeGlyphTemplate()
        {
            DataTemplate template = new DataTemplate();
            FrameworkElementFactory child = new FrameworkElementFactory(typeof(DXImage));
            Binding binding = new Binding("ActualGlyph") {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(BarSplitButtonItemLinkControl), 1)
            };
            child.SetBinding(Image.SourceProperty, binding);
            FrameworkElementFactory factory2 = new FrameworkElementFactory(typeof(Border));
            factory2.SetValue(FrameworkElement.NameProperty, "colorIndicator");
            factory2.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            factory2.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Bottom);
            FrameworkElementFactory factory3 = new FrameworkElementFactory(typeof(Grid));
            factory3.AppendChild(child);
            factory3.AppendChild(factory2);
            template.VisualTree = factory3;
            template.Seal();
            base.GlyphTemplate = template;
        }

        protected override void OnEditValueChanged(object oldValue, object newValue)
        {
            base.OnEditValueChanged(oldValue, newValue);
            LinkControlAction<BarSplitButtonColorEditItemLinkControl> action = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                LinkControlAction<BarSplitButtonColorEditItemLinkControl> local1 = <>c.<>9__4_0;
                action = <>c.<>9__4_0 = x => x.UpdateColorIndicatorColor();
            }
            this.ExecuteActionOnLinkControls<BarSplitButtonColorEditItemLinkControl>(action);
        }

        protected internal override void UpdateLinkControl(BarItemLinkBase link)
        {
            base.UpdateLinkControl(link);
            BarSplitButtonColorEditItemLink link2 = link as BarSplitButtonColorEditItemLink;
            if (link2 != null)
            {
                link2.UpdateLinkControl();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarSplitButtonColorEditItem.<>c <>9 = new BarSplitButtonColorEditItem.<>c();
            public static LinkControlAction<BarSplitButtonColorEditItemLinkControl> <>9__4_0;

            internal BarItemLink <.cctor>b__0_0(object arg) => 
                new BarSplitButtonColorEditItemLink();

            internal void <OnEditValueChanged>b__4_0(BarSplitButtonColorEditItemLinkControl x)
            {
                x.UpdateColorIndicatorColor();
            }
        }
    }
}

