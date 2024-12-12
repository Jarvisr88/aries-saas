namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="NoChangeElement", Type=typeof(FrameworkElement)), TemplatePart(Name="LeftElement", Type=typeof(FrameworkElement)), TemplatePart(Name="TopElement", Type=typeof(FrameworkElement)), TemplatePart(Name="RightElement", Type=typeof(FrameworkElement)), TemplatePart(Name="BottomElement", Type=typeof(FrameworkElement))]
    public class MaximizedElementPositionIndicator : ControlBase
    {
        public const double DefaultNoChangeAreaSize = 30.0;
        private MaximizedElementPosition _SelectedPosition;
        private const string NoChangeElementName = "NoChangeElement";
        private const string LeftElementName = "LeftElement";
        private const string TopElementName = "TopElement";
        private const string RightElementName = "RightElement";
        private const string BottomElementName = "BottomElement";

        public MaximizedElementPositionIndicator()
        {
            base.DefaultStyleKey = typeof(MaximizedElementPositionIndicator);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.NoChangeElement = base.GetTemplateChild("NoChangeElement") as FrameworkElement;
            this.LeftElement = base.GetTemplateChild("LeftElement") as FrameworkElement;
            this.TopElement = base.GetTemplateChild("TopElement") as FrameworkElement;
            this.RightElement = base.GetTemplateChild("RightElement") as FrameworkElement;
            this.BottomElement = base.GetTemplateChild("BottomElement") as FrameworkElement;
            this.UpdateTemplate();
        }

        protected virtual void UpdateTemplate()
        {
            if (this.LeftElement != null)
            {
                this.LeftElement.SetVisible(this.SelectedPosition == MaximizedElementPosition.Left);
            }
            if (this.TopElement != null)
            {
                this.TopElement.SetVisible(this.SelectedPosition == MaximizedElementPosition.Top);
            }
            if (this.RightElement != null)
            {
                this.RightElement.SetVisible(this.SelectedPosition == MaximizedElementPosition.Right);
            }
            if (this.BottomElement != null)
            {
                this.BottomElement.SetVisible(this.SelectedPosition == MaximizedElementPosition.Bottom);
            }
        }

        public double NoChangeAreaSize =>
            (this.NoChangeElement != null) ? this.NoChangeElement.ActualWidth : 30.0;

        public MaximizedElementPosition SelectedPosition
        {
            get => 
                this._SelectedPosition;
            set
            {
                if (this.SelectedPosition != value)
                {
                    this._SelectedPosition = value;
                    this.UpdateTemplate();
                }
            }
        }

        protected FrameworkElement NoChangeElement { get; set; }

        protected FrameworkElement LeftElement { get; set; }

        protected FrameworkElement TopElement { get; set; }

        protected FrameworkElement RightElement { get; set; }

        protected FrameworkElement BottomElement { get; set; }
    }
}

