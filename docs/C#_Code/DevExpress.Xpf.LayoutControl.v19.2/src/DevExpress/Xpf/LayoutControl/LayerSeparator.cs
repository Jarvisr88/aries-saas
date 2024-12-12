namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="HorizontalRootElement", Type=typeof(FrameworkElement)), TemplatePart(Name="VerticalRootElement", Type=typeof(FrameworkElement))]
    public class LayerSeparator : ControlBase
    {
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(LayerSeparator), null);
        private bool _IsInteractive;
        private Orientation _Kind = Orientation.Vertical;
        private const string HorizontalRootElementName = "HorizontalRootElement";
        private const string VerticalRootElementName = "VerticalRootElement";

        public LayerSeparator()
        {
            base.DefaultStyleKey = typeof(LayerSeparator);
            this.OnIsInteractiveChanged();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.HorizontalRootElement = base.GetTemplateChild("HorizontalRootElement") as FrameworkElement;
            this.VerticalRootElement = base.GetTemplateChild("VerticalRootElement") as FrameworkElement;
            this.UpdateTemplate();
        }

        protected virtual void OnIsInteractiveChanged()
        {
            base.IsHitTestVisible = this.IsInteractive;
        }

        protected virtual void UpdateTemplate()
        {
            if (this.HorizontalRootElement != null)
            {
                this.HorizontalRootElement.SetVisible(this.Kind == Orientation.Horizontal);
            }
            if (this.VerticalRootElement != null)
            {
                this.VerticalRootElement.SetVisible(this.Kind == Orientation.Vertical);
            }
            if (this.RootElement != null)
            {
                base.Cursor = this.RootElement.Cursor;
            }
            base.InvalidateMeasure();
        }

        public Orientation Kind
        {
            get => 
                this._Kind;
            set
            {
                if (this._Kind != value)
                {
                    this._Kind = value;
                    this.UpdateTemplate();
                }
            }
        }

        public bool IsInteractive
        {
            get => 
                this._IsInteractive;
            set
            {
                if (this._IsInteractive != value)
                {
                    this._IsInteractive = value;
                    this.OnIsInteractiveChanged();
                }
            }
        }

        public double Thickness
        {
            get => 
                (double) base.GetValue(ThicknessProperty);
            set => 
                base.SetValue(ThicknessProperty, value);
        }

        protected FrameworkElement HorizontalRootElement { get; set; }

        protected FrameworkElement VerticalRootElement { get; set; }

        protected FrameworkElement RootElement =>
            (this.Kind == Orientation.Horizontal) ? this.HorizontalRootElement : this.VerticalRootElement;
    }
}

