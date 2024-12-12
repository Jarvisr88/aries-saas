namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="HorizontalRootElement", Type=typeof(FrameworkElement)), TemplatePart(Name="HorizontalStartElement", Type=typeof(RadioButton)), TemplatePart(Name="HorizontalCenterElement", Type=typeof(RadioButton)), TemplatePart(Name="HorizontalEndElement", Type=typeof(RadioButton)), TemplatePart(Name="HorizontalStretchElement", Type=typeof(RadioButton)), TemplatePart(Name="VerticalRootElement", Type=typeof(FrameworkElement)), TemplatePart(Name="VerticalStartElement", Type=typeof(RadioButton)), TemplatePart(Name="VerticalCenterElement", Type=typeof(RadioButton)), TemplatePart(Name="VerticalEndElement", Type=typeof(RadioButton)), TemplatePart(Name="VerticalStretchElement", Type=typeof(RadioButton))]
    public class LayoutItemAlignmentControl : ControlBase
    {
        public static readonly DependencyProperty AlignmentProperty;
        private System.Windows.Controls.Orientation _Orientation;
        private const string HorizontalRootElementName = "HorizontalRootElement";
        private const string HorizontalStartElementName = "HorizontalStartElement";
        private const string HorizontalCenterElementName = "HorizontalCenterElement";
        private const string HorizontalEndElementName = "HorizontalEndElement";
        private const string HorizontalStretchElementName = "HorizontalStretchElement";
        private const string VerticalRootElementName = "VerticalRootElement";
        private const string VerticalStartElementName = "VerticalStartElement";
        private const string VerticalCenterElementName = "VerticalCenterElement";
        private const string VerticalEndElementName = "VerticalEndElement";
        private const string VerticalStretchElementName = "VerticalStretchElement";

        public event Action AlignmentChanged;

        static LayoutItemAlignmentControl()
        {
            AlignmentProperty = DependencyProperty.Register("Alignment", typeof(ItemAlignment?), typeof(LayoutItemAlignmentControl), new PropertyMetadata((o, e) => ((LayoutItemAlignmentControl) o).OnAlignmentChanged()));
        }

        public LayoutItemAlignmentControl()
        {
            base.DefaultStyleKey = typeof(LayoutItemAlignmentControl);
        }

        private void AttachEventHandlers(RadioButton element)
        {
            if (element != null)
            {
                element.Checked += new RoutedEventHandler(this.ElementChecked);
            }
        }

        private void DetachEventHandlers(RadioButton element)
        {
            if (element != null)
            {
                element.Checked -= new RoutedEventHandler(this.ElementChecked);
            }
        }

        private void ElementChecked(object sender, RoutedEventArgs e)
        {
            if ((sender == this.HorizontalStartElement) || (sender == this.VerticalStartElement))
            {
                this.Alignment = 0;
            }
            if ((sender == this.HorizontalCenterElement) || (sender == this.VerticalCenterElement))
            {
                this.Alignment = 1;
            }
            if ((sender == this.HorizontalEndElement) || (sender == this.VerticalEndElement))
            {
                this.Alignment = 2;
            }
            if ((sender == this.HorizontalStretchElement) || (sender == this.VerticalStretchElement))
            {
                this.Alignment = 3;
            }
        }

        protected virtual void OnAlignmentChanged()
        {
            this.UpdateTemplate();
            if (this.AlignmentChanged != null)
            {
                this.AlignmentChanged();
            }
        }

        public override void OnApplyTemplate()
        {
            this.DetachEventHandlers(this.HorizontalStartElement);
            this.DetachEventHandlers(this.HorizontalCenterElement);
            this.DetachEventHandlers(this.HorizontalEndElement);
            this.DetachEventHandlers(this.HorizontalStretchElement);
            this.DetachEventHandlers(this.VerticalStartElement);
            this.DetachEventHandlers(this.VerticalCenterElement);
            this.DetachEventHandlers(this.VerticalEndElement);
            this.DetachEventHandlers(this.VerticalStretchElement);
            base.OnApplyTemplate();
            this.HorizontalRootElement = base.GetTemplateChild("HorizontalRootElement") as FrameworkElement;
            this.HorizontalStartElement = base.GetTemplateChild("HorizontalStartElement") as RadioButton;
            this.HorizontalCenterElement = base.GetTemplateChild("HorizontalCenterElement") as RadioButton;
            this.HorizontalEndElement = base.GetTemplateChild("HorizontalEndElement") as RadioButton;
            this.HorizontalStretchElement = base.GetTemplateChild("HorizontalStretchElement") as RadioButton;
            this.VerticalRootElement = base.GetTemplateChild("VerticalRootElement") as FrameworkElement;
            this.VerticalStartElement = base.GetTemplateChild("VerticalStartElement") as RadioButton;
            this.VerticalCenterElement = base.GetTemplateChild("VerticalCenterElement") as RadioButton;
            this.VerticalEndElement = base.GetTemplateChild("VerticalEndElement") as RadioButton;
            this.VerticalStretchElement = base.GetTemplateChild("VerticalStretchElement") as RadioButton;
            this.AttachEventHandlers(this.HorizontalStartElement);
            this.AttachEventHandlers(this.HorizontalCenterElement);
            this.AttachEventHandlers(this.HorizontalEndElement);
            this.AttachEventHandlers(this.HorizontalStretchElement);
            this.AttachEventHandlers(this.VerticalStartElement);
            this.AttachEventHandlers(this.VerticalCenterElement);
            this.AttachEventHandlers(this.VerticalEndElement);
            this.AttachEventHandlers(this.VerticalStretchElement);
            this.UpdateTemplate();
        }

        private void UpdateIsChecked(RadioButton element, ItemAlignment alignment)
        {
            if (element != null)
            {
                ItemAlignment? nullable = this.Alignment;
                ItemAlignment alignment2 = alignment;
                element.IsChecked = new bool?((((ItemAlignment) nullable.GetValueOrDefault()) == alignment2) ? (nullable != null) : false);
            }
        }

        private void UpdateIsChecked(RadioButton start, RadioButton center, RadioButton end, RadioButton stretch)
        {
            this.UpdateIsChecked(start, ItemAlignment.Start);
            this.UpdateIsChecked(center, ItemAlignment.Center);
            this.UpdateIsChecked(end, ItemAlignment.End);
            this.UpdateIsChecked(stretch, ItemAlignment.Stretch);
        }

        protected virtual void UpdateTemplate()
        {
            if (this.HorizontalRootElement != null)
            {
                this.HorizontalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Horizontal);
            }
            if (this.VerticalRootElement != null)
            {
                this.VerticalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Vertical);
            }
            this.UpdateIsChecked(this.HorizontalStartElement, this.HorizontalCenterElement, this.HorizontalEndElement, this.HorizontalStretchElement);
            this.UpdateIsChecked(this.VerticalStartElement, this.VerticalCenterElement, this.VerticalEndElement, this.VerticalStretchElement);
        }

        public ItemAlignment? Alignment
        {
            get => 
                (ItemAlignment?) base.GetValue(AlignmentProperty);
            set => 
                base.SetValue(AlignmentProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                this._Orientation;
            set
            {
                if (this.Orientation != value)
                {
                    this._Orientation = value;
                    this.UpdateTemplate();
                }
            }
        }

        protected FrameworkElement HorizontalRootElement { get; private set; }

        protected RadioButton HorizontalStartElement { get; private set; }

        protected RadioButton HorizontalCenterElement { get; private set; }

        protected RadioButton HorizontalEndElement { get; private set; }

        protected RadioButton HorizontalStretchElement { get; private set; }

        protected FrameworkElement VerticalRootElement { get; private set; }

        protected RadioButton VerticalStartElement { get; private set; }

        protected RadioButton VerticalCenterElement { get; private set; }

        protected RadioButton VerticalEndElement { get; private set; }

        protected RadioButton VerticalStretchElement { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemAlignmentControl.<>c <>9 = new LayoutItemAlignmentControl.<>c();

            internal void <.cctor>b__70_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemAlignmentControl) o).OnAlignmentChanged();
            }
        }
    }
}

