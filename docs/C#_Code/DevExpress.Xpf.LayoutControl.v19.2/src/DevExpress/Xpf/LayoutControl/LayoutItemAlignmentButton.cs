namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="RootElement", Type=typeof(Border)), TemplatePart(Name="GlyphLeft", Type=typeof(FrameworkElement)), TemplatePart(Name="GlyphHorizontalCenter", Type=typeof(FrameworkElement)), TemplatePart(Name="GlyphRight", Type=typeof(FrameworkElement)), TemplatePart(Name="GlyphHorizontalStretch", Type=typeof(FrameworkElement)), TemplatePart(Name="GlyphTop", Type=typeof(FrameworkElement)), TemplatePart(Name="GlyphVerticalCenter", Type=typeof(FrameworkElement)), TemplatePart(Name="GlyphBottom", Type=typeof(FrameworkElement)), TemplatePart(Name="GlyphVerticalStretch", Type=typeof(FrameworkElement))]
    public class LayoutItemAlignmentButton : RadioButton
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(System.Windows.CornerRadius), typeof(LayoutItemAlignmentButton), null);
        public static readonly DependencyProperty KindProperty;
        private const string RootElementName = "RootElement";
        private const string GlyphBaseName = "Glyph";
        private const string GlyphLeftName = "GlyphLeft";
        private const string GlyphHorizontalCenterName = "GlyphHorizontalCenter";
        private const string GlyphRightName = "GlyphRight";
        private const string GlyphHorizontalStretchName = "GlyphHorizontalStretch";
        private const string GlyphTopName = "GlyphTop";
        private const string GlyphVerticalCenterName = "GlyphVerticalCenter";
        private const string GlyphBottomName = "GlyphBottom";
        private const string GlyphVerticalStretchName = "GlyphVerticalStretch";

        static LayoutItemAlignmentButton()
        {
            KindProperty = DependencyProperty.Register("Kind", typeof(LayoutItemAlignmentButtonKind), typeof(LayoutItemAlignmentButton), new PropertyMetadata((o, e) => ((LayoutItemAlignmentButton) o).OnKindChanged()));
        }

        public LayoutItemAlignmentButton()
        {
            base.DefaultStyleKey = typeof(LayoutItemAlignmentButton);
        }

        public override void OnApplyTemplate()
        {
            if (this.RootElement != null)
            {
                BorderExtensions.SetClipChild(this.RootElement, false);
            }
            base.OnApplyTemplate();
            this.RootElement = base.GetTemplateChild("RootElement") as Border;
            this.Glyphs ??= new Dictionary<LayoutItemAlignmentButtonKind, FrameworkElement>();
            for (LayoutItemAlignmentButtonKind kind = LayoutItemAlignmentButtonKind.Left; kind <= LayoutItemAlignmentButtonKind.VerticalStretch; kind += 1)
            {
                this.Glyphs[kind] = base.GetTemplateChild("Glyph" + kind.ToString()) as FrameworkElement;
            }
            if (this.RootElement != null)
            {
                BorderExtensions.SetClipChild(this.RootElement, true);
            }
            this.UpdateTemplate();
        }

        protected virtual void OnKindChanged()
        {
            this.UpdateTemplate();
        }

        protected virtual void UpdateTemplate()
        {
            if (this.Glyphs != null)
            {
                for (LayoutItemAlignmentButtonKind kind = LayoutItemAlignmentButtonKind.Left; kind <= LayoutItemAlignmentButtonKind.VerticalStretch; kind += 1)
                {
                    FrameworkElement element = this.Glyphs[kind];
                    if (element != null)
                    {
                        element.SetVisible(kind == this.Kind);
                    }
                }
            }
        }

        public System.Windows.CornerRadius CornerRadius
        {
            get => 
                (System.Windows.CornerRadius) base.GetValue(CornerRadiusProperty);
            set => 
                base.SetValue(CornerRadiusProperty, value);
        }

        public LayoutItemAlignmentButtonKind Kind
        {
            get => 
                (LayoutItemAlignmentButtonKind) base.GetValue(KindProperty);
            set => 
                base.SetValue(KindProperty, value);
        }

        protected Dictionary<LayoutItemAlignmentButtonKind, FrameworkElement> Glyphs { get; private set; }

        protected Border RootElement { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemAlignmentButton.<>c <>9 = new LayoutItemAlignmentButton.<>c();

            internal void <.cctor>b__30_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemAlignmentButton) o).OnKindChanged();
            }
        }
    }
}

