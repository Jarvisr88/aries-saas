namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.LayoutControl.Serialization;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml;

    [LicenseProvider(typeof(DX_WPF_LicenseProvider))]
    public abstract class LayoutControlBase : ScrollControl, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, ISerializableItem, ISerializableCollectionItem
    {
        public const double DefaultItemSpace = 4.0;
        public const double DefaultPadding = 12.0;
        public static readonly DependencyProperty ItemSpaceProperty;
        public static readonly DependencyProperty PaddingProperty;
        protected static readonly Brush DefaultMovingItemPlaceHolderBrush;
        protected const string XMLIDName = "ID";

        static LayoutControlBase()
        {
            ItemSpaceProperty = DependencyProperty.Register("ItemSpace", typeof(double), typeof(LayoutControlBase), new PropertyMetadata(4.0, (o, e) => ((LayoutControlBase) o).OnItemSpaceChanged((double) e.OldValue, (double) e.NewValue)));
            PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(LayoutControlBase), new PropertyMetadata(new Thickness(12.0), (o, e) => ((LayoutControlBase) o).OnPaddingChanged((Thickness) e.OldValue, (Thickness) e.NewValue)));
            DefaultMovingItemPlaceHolderBrush = new SolidColorBrush(Color.FromArgb(0x33, 0, 0, 0));
            DXSerializer.SerializationProviderProperty.OverrideMetadata(typeof(LayoutControlBase), new UIPropertyMetadata(new LayoutControlSerializationProviderBase()));
        }

        public LayoutControlBase()
        {
            if (this.Controller.IsScrollable())
            {
                this.SetScrollBehavior();
            }
            this.LayoutProvider = this.CreateLayoutProvider();
            SerializableItem.SetTypeName(this, base.GetType().Name);
        }

        protected virtual void AddChildFromXML(IList children, FrameworkElement element, int index)
        {
            if (children.IndexOf(element) != index)
            {
                element.SetParent(null);
                children.Insert(index, element);
            }
        }

        protected override PanelControllerBase CreateController() => 
            new LayoutControllerBase(this);

        protected abstract LayoutProviderBase CreateLayoutProvider();
        protected virtual LayoutParametersBase CreateLayoutProviderParameters() => 
            new LayoutParametersBase(this.ItemSpace);

        FrameworkElement ILayoutControlBase.GetMoveableItem(Point p) => 
            this.Controller.GetMoveableItem(p);

        protected virtual FrameworkElement FindByXMLID(string id) => 
            base.FindName(id) as FrameworkElement;

        protected override Rect GetChildBounds(FrameworkElement child)
        {
            Rect childBounds = base.GetChildBounds(child);
            this.LayoutProvider.UpdateChildBounds(child, ref childBounds);
            return childBounds;
        }

        protected override Thickness GetContentPadding()
        {
            Thickness contentPadding = base.GetContentPadding();
            ThicknessHelper.Inc(ref contentPadding, this.Padding);
            return contentPadding;
        }

        protected virtual string GetXMLID(FrameworkElement element) => 
            element.Name;

        protected void MoveLogicalChildrenToEnd(out int firstChildIndex)
        {
            firstChildIndex = 0;
            for (int i = 0; i < base.Children.Count; i++)
            {
                UIElement child = base.Children[i];
                if (!base.IsLogicalChild(child))
                {
                    if (i != firstChildIndex)
                    {
                        base.Children.RemoveAt(i);
                        base.Children.Insert(firstChildIndex, child);
                    }
                    firstChildIndex++;
                }
            }
        }

        protected override Size OnArrange(Rect bounds)
        {
            Rect viewPortBounds = bounds;
            if (this.Controller.IsScrollable())
            {
                RectHelper.Offset(ref bounds, -base.HorizontalOffset, -base.VerticalOffset);
            }
            return this.LayoutProvider.Arrange(base.GetLogicalChildren(true), bounds, viewPortBounds, this.CreateLayoutProviderParameters());
        }

        protected virtual void OnItemSpaceChanged(double oldValue, double newValue)
        {
            base.Changed();
        }

        protected override Size OnMeasure(Size availableSize)
        {
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                if (element.Visibility == Visibility.Collapsed)
                {
                    element.Measure(availableSize);
                }
            }
            return this.LayoutProvider.Measure(base.GetLogicalChildren(true), availableSize, this.CreateLayoutProviderParameters());
        }

        protected virtual void OnPaddingChanged(Thickness oldValue, Thickness newValue)
        {
            base.Changed();
        }

        protected virtual FrameworkElement ReadChildFromXML(XmlReader xml, IList children, int index)
        {
            string str = xml["ID"];
            FrameworkElement element = null;
            if (!string.IsNullOrEmpty(str))
            {
                element = this.FindByXMLID(str);
            }
            return this.ReadChildFromXMLCore(element, xml, children, index, str);
        }

        protected virtual FrameworkElement ReadChildFromXMLCore(FrameworkElement element, XmlReader xml, IList children, int index, string id)
        {
            if ((xml.Name != "Element") || (element == null))
            {
                return null;
            }
            this.AddChildFromXML(children, element, index);
            this.ReadCustomizablePropertiesFromXML(element, xml);
            return element;
        }

        protected virtual void ReadChildrenFromXML(XmlReader xml, out FrameworkElement lastChild)
        {
            int num;
            this.MoveLogicalChildrenToEnd(out num);
            this.ReadChildrenFromXML(base.Children, xml, num, out lastChild);
        }

        protected void ReadChildrenFromXML(IList children, XmlReader xml, int firstChildIndex, out FrameworkElement lastChild)
        {
            lastChild = null;
            if (!xml.IsEmptyElement)
            {
                int index = firstChildIndex;
                while (xml.Read() && (xml.NodeType != XmlNodeType.EndElement))
                {
                    FrameworkElement element = this.ReadChildFromXML(xml, children, index);
                    if (element != null)
                    {
                        lastChild = element;
                        index++;
                    }
                }
            }
        }

        protected virtual void ReadCustomizablePropertiesFromXML(XmlReader xml)
        {
        }

        protected virtual void ReadCustomizablePropertiesFromXML(FrameworkElement element, XmlReader xml)
        {
        }

        public virtual void ReadFromXML(XmlReader xml)
        {
            if (xml.IsStartElement(base.GetType().Name))
            {
                this.ReadFromXMLCore(xml);
            }
        }

        protected virtual void ReadFromXMLCore(XmlReader xml)
        {
            FrameworkElement element;
            this.ReadCustomizablePropertiesFromXML(xml);
            this.ReadChildrenFromXML(xml, out element);
        }

        private void SetScrollBehavior()
        {
            base.SetCurrentValue(ScrollBarExtensions.ScrollBehaviorProperty, new LayoutControlBaseScrollBehavior());
            base.SetCurrentValue(ScrollBarExtensions.AllowMouseScrollingProperty, true);
            base.SetCurrentValue(ScrollBarExtensions.AllowShiftKeyModeProperty, true);
        }

        protected void WriteChildrenToXML(IList children, XmlWriter xml)
        {
            foreach (FrameworkElement element in children)
            {
                this.WriteChildToXML(element, xml);
            }
        }

        protected virtual void WriteChildToXML(FrameworkElement child, XmlWriter xml)
        {
            xml.WriteStartElement("Element");
            xml.WriteAttributeString("ID", this.GetXMLID(child));
            this.WriteChildToXMLCore(child, xml);
            xml.WriteEndElement();
        }

        protected virtual void WriteChildToXMLCore(FrameworkElement child, XmlWriter xml)
        {
            this.WriteCustomizablePropertiesToXML(child, xml);
        }

        protected virtual void WriteCustomizablePropertiesToXML(XmlWriter xml)
        {
        }

        protected virtual void WriteCustomizablePropertiesToXML(FrameworkElement element, XmlWriter xml)
        {
        }

        public virtual void WriteToXML(XmlWriter xml)
        {
            xml.WriteStartElement(base.GetType().Name);
            this.WriteToXMLCore(xml);
            xml.WriteEndElement();
        }

        protected virtual void WriteToXMLCore(XmlWriter xml)
        {
            this.WriteCustomizablePropertiesToXML(xml);
            this.WriteChildrenToXML(base.GetLogicalChildren(false), xml);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public LayoutControllerBase Controller =>
            (LayoutControllerBase) base.Controller;

        [Description("Gets or sets the distance between child items.This is a dependency property."), XtraSerializableProperty]
        public double ItemSpace
        {
            get => 
                (double) base.GetValue(ItemSpaceProperty);
            set => 
                base.SetValue(ItemSpaceProperty, Math.Max(0.0, value));
        }

        [Description("Gets or sets padding settings for the current control.This is a dependency property."), XtraSerializableProperty]
        public Thickness Padding
        {
            get => 
                (Thickness) base.GetValue(PaddingProperty);
            set => 
                base.SetValue(PaddingProperty, value);
        }

        protected LayoutProviderBase LayoutProvider { get; private set; }

        bool ILayoutControlBase.AllowItemMoving =>
            false;

        LayoutProviderBase ILayoutControlBase.LayoutProvider =>
            this.LayoutProvider;

        Brush ILayoutControlBase.MovingItemPlaceHolderBrush =>
            null;

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
            public static readonly LayoutControlBase.<>c <>9 = new LayoutControlBase.<>c();

            internal void <.cctor>b__5_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlBase) o).OnItemSpaceChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__5_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlBase) o).OnPaddingChanged((Thickness) e.OldValue, (Thickness) e.NewValue);
            }
        }
    }
}

