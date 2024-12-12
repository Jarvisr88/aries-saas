namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.LayoutControl.Serialization;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Markup;
    using System.Xml;

    [ContentProperty("Content"), StyleTypedProperty(Property="LabelStyle", StyleTargetType=typeof(LayoutItemLabel)), TemplatePart(Name="LabelElement", Type=typeof(LayoutItemLabel)), DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class LayoutItem : ControlBase, ILayoutControlCustomizableItem, ISerializableItem, ISerializableCollectionItem
    {
        public static readonly DependencyProperty AddColonToLabelProperty;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ElementSpaceProperty;
        public static readonly DependencyProperty IsRequiredProperty;
        public static readonly DependencyProperty LabelProperty;
        public static readonly DependencyProperty LabelHorizontalAlignmentProperty;
        public static readonly DependencyProperty LabelVerticalAlignmentProperty;
        public static readonly DependencyProperty LabelPositionProperty;
        public static readonly DependencyProperty LabelStyleProperty;
        public static readonly DependencyProperty LabelTemplateProperty;
        public static readonly DependencyProperty ActualLabelProperty;
        public static readonly DependencyProperty CalculatedLabelVisibilityProperty;
        public static readonly DependencyProperty IsActuallyRequiredProperty;
        private const string LabelElementName = "LabelElement";

        static LayoutItem()
        {
            AddColonToLabelProperty = DependencyProperty.Register("AddColonToLabel", typeof(bool), typeof(LayoutItem), new PropertyMetadata((o, e) => ((LayoutItem) o).OnAddColonToLabelChanged()));
            ContentProperty = DependencyProperty.Register("Content", typeof(UIElement), typeof(LayoutItem), new PropertyMetadata((o, e) => ((LayoutItem) o).OnContentChanged((UIElement) e.OldValue, (UIElement) e.NewValue)));
            ElementSpaceProperty = DependencyProperty.Register("ElementSpace", typeof(double), typeof(LayoutItem), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
            IsRequiredProperty = DependencyProperty.Register("IsRequired", typeof(bool), typeof(LayoutItem), new PropertyMetadata((o, e) => ((LayoutItem) o).OnIsRequiredChanged()));
            LabelProperty = DependencyProperty.Register("Label", typeof(object), typeof(LayoutItem), new PropertyMetadata((o, e) => ((LayoutItem) o).OnLabelChanged(e.OldValue)));
            LabelHorizontalAlignmentProperty = DependencyProperty.Register("LabelHorizontalAlignment", typeof(HorizontalAlignment), typeof(LayoutItem), new PropertyMetadata(HorizontalAlignment.Left));
            LabelVerticalAlignmentProperty = DependencyProperty.Register("LabelVerticalAlignment", typeof(VerticalAlignment), typeof(LayoutItem), new PropertyMetadata(VerticalAlignment.Center));
            LabelPositionProperty = DependencyProperty.Register("LabelPosition", typeof(LayoutItemLabelPosition), typeof(LayoutItem), new PropertyMetadata((o, e) => ((LayoutItem) o).OnLabelPositionChanged((LayoutItemLabelPosition) e.OldValue)));
            LabelStyleProperty = DependencyProperty.Register("LabelStyle", typeof(Style), typeof(LayoutItem), new PropertyMetadata((o, e) => ((LayoutItem) o).OnLabelStyleChanged()));
            LabelTemplateProperty = DependencyProperty.Register("LabelTemplate", typeof(DataTemplate), typeof(LayoutItem), null);
            ActualLabelProperty = DependencyProperty.Register("ActualLabel", typeof(object), typeof(LayoutItem), null);
            CalculatedLabelVisibilityProperty = DependencyProperty.Register("CalculatedLabelVisibility", typeof(Visibility), typeof(LayoutItem), null);
            IsActuallyRequiredProperty = DependencyProperty.Register("IsActuallyRequired", typeof(bool), typeof(LayoutItem), null);
            DXSerializer.SerializationProviderProperty.OverrideMetadata(typeof(LayoutItem), new UIPropertyMetadata(new LayoutControlSerializationProviderBase()));
            DependencyPropertyRegistrator<LayoutItem>.New().OverrideDefaultStyleKey();
        }

        public LayoutItem()
        {
            this.UpdateActualLabel();
            this.CalculateLabelVisibility();
            this.UpdateIsActuallyRequired();
            SerializableItem.SetTypeName(this, base.GetType().Name);
        }

        protected void CalculateLabelVisibility()
        {
            this.SetValue(CalculatedLabelVisibilityProperty, this.IsLabelVisible ? Visibility.Visible : Visibility.Collapsed);
        }

        FrameworkElement ILayoutControlCustomizableItem.AddNewItem() => 
            null;

        protected virtual object GetActualLabel()
        {
            string label = this.Label as string;
            if (label == null)
            {
                return this.Label;
            }
            if (!string.IsNullOrEmpty(label) && this.AddColonToLabel)
            {
                label = label + ":";
            }
            return label;
        }

        protected virtual bool GetIsActuallyRequired() => 
            this.IsRequired;

        protected virtual bool GetIsLabelVisible(object label) => 
            (label != null) && (!(label is string) || !string.IsNullOrEmpty((string) label));

        protected internal virtual FrameworkElement GetSelectableElement(Point p) => 
            (((this.Content == null) || !this.Content.GetVisible()) || !((FrameworkElement) this.Content).GetBounds(this).Contains(p)) ? this : ((FrameworkElement) this.Content);

        protected override Size MeasureOverride(Size availableSize)
        {
            this.AvailableSize = availableSize;
            return base.MeasureOverride(availableSize);
        }

        protected virtual void OnAddColonToLabelChanged()
        {
            this.UpdateActualLabel();
        }

        public override void OnApplyTemplate()
        {
            if (this.LabelElement != null)
            {
                this.LabelElement.DesiredWidthChanged = null;
                this.LabelElement.ClearValue(FrameworkElement.StyleProperty);
            }
            base.OnApplyTemplate();
            this.LabelElement = base.GetTemplateChild("LabelElement") as LayoutItemLabel;
            this.UpdateLabelElementStyle();
            if (this.LabelElement != null)
            {
                this.LabelElement.DesiredWidthChanged = new Action(this.OnLabelWidthChanged);
            }
        }

        protected virtual void OnContentChanged(UIElement oldValue, UIElement newValue)
        {
            base.RemoveLogicalChild(oldValue);
            base.AddLogicalChild(newValue);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new LayoutItemAutomationPeer(this);

        internal virtual void OnEndDeserializing()
        {
        }

        protected virtual void OnIsRequiredChanged()
        {
            this.UpdateIsActuallyRequired();
        }

        protected virtual void OnLabelChanged(object oldValue)
        {
            base.RemoveLogicalChild(oldValue);
            base.AddLogicalChild(this.Label);
            this.UpdateActualLabel();
            this.CalculateLabelVisibility();
        }

        protected virtual void OnLabelPositionChanged(LayoutItemLabelPosition oldValue)
        {
            this.LabelWidth = double.NaN;
            base.InvalidateMeasure();
        }

        protected virtual void OnLabelStyleChanged()
        {
            this.UpdateLabelElementStyle();
        }

        protected virtual void OnLabelWidthChanged()
        {
            ILayoutGroup parent = base.Parent as ILayoutGroup;
            if (parent != null)
            {
                parent.LayoutItemLabelWidthChanged(this);
            }
        }

        protected internal virtual void ReadCustomizablePropertiesFromXML(XmlReader xml)
        {
            this.ReadPropertyFromXML(xml, LabelProperty, "Label", typeof(object));
        }

        protected void UpdateActualLabel()
        {
            this.ActualLabel = this.GetActualLabel();
        }

        protected void UpdateIsActuallyRequired()
        {
            this.IsActuallyRequired = this.GetIsActuallyRequired();
        }

        private void UpdateLabelElementStyle()
        {
            if (this.LabelElement != null)
            {
                if (this.LabelStyle != null)
                {
                    this.LabelElement.Style = this.LabelStyle;
                }
                else
                {
                    this.LabelElement.ClearValue(FrameworkElement.StyleProperty);
                }
            }
        }

        protected internal virtual void WriteCustomizablePropertiesToXML(XmlWriter xml)
        {
            this.WritePropertyToXML(xml, LabelProperty, "Label");
        }

        [Description("Gets the layout item's actual label.This is a dependency property.")]
        public object ActualLabel
        {
            get => 
                base.GetValue(ActualLabelProperty);
            private set => 
                base.SetValue(ActualLabelProperty, value);
        }

        [Description("Gets or sets whether the colon character is appended to the current layout item's label.This is a dependency property.")]
        public bool AddColonToLabel
        {
            get => 
                (bool) base.GetValue(AddColonToLabelProperty);
            set => 
                base.SetValue(AddColonToLabelProperty, value);
        }

        [Description("Specifies the content of the LayoutItem.This is a dependency property.")]
        public UIElement Content
        {
            get => 
                (UIElement) base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Description("Gets or sets the distance between the objects specified by the LayoutItem.Label and LayoutItem.Content properties.This is a dependency property.")]
        public double ElementSpace
        {
            get => 
                (double) base.GetValue(ElementSpaceProperty);
            set => 
                base.SetValue(ElementSpaceProperty, value);
        }

        [Description("Gets whether the layout item's field is required.This is a dependency property.")]
        public bool IsActuallyRequired
        {
            get => 
                (bool) base.GetValue(IsActuallyRequiredProperty);
            private set => 
                base.SetValue(IsActuallyRequiredProperty, value);
        }

        [Description("Gets whether the label is visible.")]
        public bool IsLabelVisible =>
            this.GetIsLabelVisible(this.Label);

        [Description("Gets or sets whether the current item's label is painted bold, indicating to an end-user that the current field is required.This is a dependency property.")]
        public bool IsRequired
        {
            get => 
                (bool) base.GetValue(IsRequiredProperty);
            set => 
                base.SetValue(IsRequiredProperty, value);
        }

        [Description("Gets or sets a label for the current LayoutItem.This is a dependency property."), XtraSerializableProperty]
        public object Label
        {
            get => 
                base.GetValue(LabelProperty);
            set => 
                base.SetValue(LabelProperty, value);
        }

        [Description("Gets or sets the horizontal alignment of the label within the layout item's label region.This is a dependency property.")]
        public HorizontalAlignment LabelHorizontalAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(LabelHorizontalAlignmentProperty);
            set => 
                base.SetValue(LabelHorizontalAlignmentProperty, value);
        }

        [Description("Gets or sets the vertical alignment of the label within the layout item's label region.This is a dependency property.")]
        public VerticalAlignment LabelVerticalAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(LabelVerticalAlignmentProperty);
            set => 
                base.SetValue(LabelVerticalAlignmentProperty, value);
        }

        [Description("Gets or sets the position of the LayoutItem.Label relative to the LayoutItem.Content.This is a dependency property.")]
        public LayoutItemLabelPosition LabelPosition
        {
            get => 
                (LayoutItemLabelPosition) base.GetValue(LabelPositionProperty);
            set => 
                base.SetValue(LabelPositionProperty, value);
        }

        [Description("Gets or sets a style used by the LayoutItem.Label object when it is rendered.This is a dependency property.")]
        public Style LabelStyle
        {
            get => 
                (Style) base.GetValue(LabelStyleProperty);
            set => 
                base.SetValue(LabelStyleProperty, value);
        }

        [Description("Gets or sets a data template used to display the LayoutItem.Label.This is a dependency property.")]
        public DataTemplate LabelTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LabelTemplateProperty);
            set => 
                base.SetValue(LabelTemplateProperty, value);
        }

        protected LayoutItemLabel LabelElement { get; private set; }

        protected Size AvailableSize { get; private set; }

        protected internal double LabelWidth
        {
            get => 
                (this.LabelElement != null) ? this.LabelElement.DesiredWidth : 0.0;
            set
            {
                if (this.LabelElement != null)
                {
                    double positiveInfinity = double.PositiveInfinity;
                    double size = (base.Margin.Left + base.ActualWidth) + base.Margin.Right;
                    LayoutGroup parent = base.Parent as LayoutGroup;
                    bool flag = (parent != null) && (parent.GetItemAlignment(this, true) == ItemAlignment.Stretch);
                    if (double.IsInfinity(this.AvailableSize.Width) && ((size > base.DesiredSize.Width) && !flag))
                    {
                        positiveInfinity = UIElementExtensions.GetRoundedSize(size) - (base.DesiredSize.Width - this.LabelElement.DesiredSize.Width);
                    }
                    this.LabelElement.CustomWidth = ((value - positiveInfinity) < 1E-09) ? value : double.NaN;
                }
            }
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                if (this.Content != null)
                {
                    list.Add(this.Content);
                }
                if (this.Label != null)
                {
                    list.Add(this.Label);
                }
                return list.GetEnumerator();
            }
        }

        bool ILayoutControlCustomizableItem.CanAddNewItems =>
            false;

        bool ILayoutControlCustomizableItem.HasHeader =>
            true;

        object ILayoutControlCustomizableItem.Header
        {
            get => 
                this.Label;
            set => 
                this.Label = value;
        }

        bool ILayoutControlCustomizableItem.IsLocked =>
            false;

        FrameworkElement ISerializableItem.Element =>
            this;

        string ISerializableCollectionItem.Name
        {
            get => 
                base.Name;
            set => 
                base.Name = value;
        }

        string ISerializableCollectionItem.TypeName =>
            SerializableItem.GetTypeName(this);

        string ISerializableCollectionItem.ParentName
        {
            get => 
                SerializableItem.GetParentName(this);
            set => 
                SerializableItem.SetParentName(this, value);
        }

        string ISerializableCollectionItem.ParentCollectionName
        {
            get => 
                SerializableItem.GetParentCollectionName(this);
            set => 
                SerializableItem.SetParentCollectionName(this, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItem.<>c <>9 = new LayoutItem.<>c();

            internal void <.cctor>b__13_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItem) o).OnAddColonToLabelChanged();
            }

            internal void <.cctor>b__13_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItem) o).OnContentChanged((UIElement) e.OldValue, (UIElement) e.NewValue);
            }

            internal void <.cctor>b__13_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItem) o).OnIsRequiredChanged();
            }

            internal void <.cctor>b__13_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItem) o).OnLabelChanged(e.OldValue);
            }

            internal void <.cctor>b__13_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItem) o).OnLabelPositionChanged((LayoutItemLabelPosition) e.OldValue);
            }

            internal void <.cctor>b__13_5(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItem) o).OnLabelStyleChanged();
            }
        }
    }
}

