namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.UI.ModuleInjection;
    using DevExpress.Xpf;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Xml;

    [LicenseProvider(typeof(DX_WPF_LicenseProvider)), DefaultBindingProperty("ItemsSource"), StyleTypedProperty(Property="LayerSeparatorStyle", StyleTargetType=typeof(LayerSeparator)), StyleTypedProperty(Property="MaximizedElementPositionIndicatorStyle", StyleTargetType=typeof(MaximizedElementPositionIndicator)), DXToolboxBrowsable]
    public class FlowLayoutControl : LayoutControlBase, IFlowLayoutControl, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, IFlowLayoutModel, IMaximizingContainer
    {
        public const int DefaultItemMovingAnimationDuration = 200;
        public static double DefaultLayerMinWidth = 20.0;
        public const double DefaultLayerSpace = 7.0;
        public static TimeSpan ItemDropAnimationDuration = TimeSpan.FromMilliseconds(500.0);
        public static TimeSpan ItemMaximizationAnimationDuration = TimeSpan.FromMilliseconds(500.0);
        private static bool _IgnoreMaximizedElementChange;
        private static readonly DependencyProperty ItemsAttachedBehaviorProperty = DependencyProperty.Register("ItemsAttachedBehavior", typeof(object), typeof(FlowLayoutControl), null);
        public static readonly DependencyProperty AllowAddFlowBreaksDuringItemMovingProperty = DependencyProperty.Register("AllowAddFlowBreaksDuringItemMoving", typeof(bool), typeof(FlowLayoutControl), null);
        public static readonly DependencyProperty AllowItemMovingProperty = DependencyProperty.Register("AllowItemMoving", typeof(bool), typeof(FlowLayoutControl), null);
        public static readonly DependencyProperty AnimateItemMaximizationProperty = DependencyProperty.Register("AnimateItemMaximization", typeof(bool), typeof(FlowLayoutControl), new PropertyMetadata(true));
        public static readonly DependencyProperty AnimateItemMovingProperty = DependencyProperty.Register("AnimateItemMoving", typeof(bool), typeof(FlowLayoutControl), null);
        public static readonly DependencyProperty BreakFlowToFitProperty;
        public static readonly DependencyProperty IsFlowBreakProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;
        public static readonly DependencyProperty ItemMovingAnimationDurationProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty LayerSeparatorStyleProperty;
        public static readonly DependencyProperty LayerSizingCoverBrushProperty;
        public static readonly DependencyProperty LayerSpaceProperty;
        public static readonly DependencyProperty MaximizedElementProperty;
        public static readonly DependencyProperty MaximizedElementPositionProperty;
        public static readonly DependencyProperty MaximizedElementPositionIndicatorStyleProperty;
        public static readonly DependencyProperty MovingItemPlaceHolderBrushProperty;
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty ShowLayerSeparatorsProperty;
        public static readonly DependencyProperty StretchContentProperty;

        public event ValueChangedEventHandler<int> ItemPositionChanged;

        public event ValueChangedEventHandler<Size> ItemsSizeChanged;

        public event ValueChangedEventHandler<FrameworkElement> MaximizedElementChanged;

        public event ValueChangedEventHandler<DevExpress.Xpf.LayoutControl.MaximizedElementPosition> MaximizedElementPositionChanged;

        static FlowLayoutControl()
        {
            BreakFlowToFitProperty = DependencyProperty.Register("BreakFlowToFit", typeof(bool), typeof(FlowLayoutControl), new PropertyMetadata(true, (o, e) => ((FlowLayoutControl) o).OnBreakFlowToFitChanged()));
            IsFlowBreakProperty = DependencyProperty.RegisterAttached("IsFlowBreak", typeof(bool), typeof(FlowLayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            ItemContainerStyleProperty = DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).OnItemContainerStyleChanged(e)));
            ItemMovingAnimationDurationProperty = DependencyProperty.Register("ItemMovingAnimationDuration", typeof(TimeSpan), typeof(FlowLayoutControl), new PropertyMetadata(TimeSpan.FromMilliseconds(200.0)));
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).OnItemsSourceChanged(e)));
            ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).OnItemTemplateChanged(e)));
            ItemTemplateSelectorProperty = DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).OnItemTemplateSelectorChanged(e)));
            LayerSeparatorStyleProperty = DependencyProperty.Register("LayerSeparatorStyle", typeof(Style), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).LayerSeparators.ItemStyle = (Style) e.NewValue));
            LayerSizingCoverBrushProperty = DependencyProperty.Register("LayerSizingCoverBrush", typeof(Brush), typeof(FlowLayoutControl), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0x7f, 0xff, 0xff, 0xff))));
            LayerSpaceProperty = DependencyProperty.Register("LayerSpace", typeof(double), typeof(FlowLayoutControl), new PropertyMetadata(7.0, (o, e) => ((FlowLayoutControl) o).OnLayerSpaceChanged((double) e.OldValue, (double) e.NewValue)));
            MaximizedElementProperty = DependencyProperty.Register("MaximizedElement", typeof(FrameworkElement), typeof(FlowLayoutControl), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                if (!_IgnoreMaximizedElementChange)
                {
                    FlowLayoutControl objB = (FlowLayoutControl) o;
                    if ((e.NewValue == null) || ReferenceEquals(((FrameworkElement) e.NewValue).Parent, objB))
                    {
                        objB.OnMaximizedElementChanged((FrameworkElement) e.OldValue, (FrameworkElement) e.NewValue);
                    }
                    else
                    {
                        _IgnoreMaximizedElementChange = true;
                        o.SetValue(e.Property, e.OldValue);
                        _IgnoreMaximizedElementChange = false;
                        throw new ArgumentOutOfRangeException("MaximizedElement");
                    }
                }
            }));
            MaximizedElementPositionProperty = DependencyProperty.Register("MaximizedElementPosition", typeof(DevExpress.Xpf.LayoutControl.MaximizedElementPosition), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).OnMaximizedElementPositionChanged((DevExpress.Xpf.LayoutControl.MaximizedElementPosition) e.OldValue)));
            MaximizedElementPositionIndicatorStyleProperty = DependencyProperty.Register("MaximizedElementPositionIndicatorStyle", typeof(Style), typeof(FlowLayoutControl), null);
            MovingItemPlaceHolderBrushProperty = DependencyProperty.Register("MovingItemPlaceHolderBrush", typeof(Brush), typeof(FlowLayoutControl), new PropertyMetadata(LayoutControlBase.DefaultMovingItemPlaceHolderBrush));
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(FlowLayoutControl), new PropertyMetadata(System.Windows.Controls.Orientation.Vertical, (o, e) => ((FlowLayoutControl) o).OnOrientationChanged()));
            ShowLayerSeparatorsProperty = DependencyProperty.Register("ShowLayerSeparators", typeof(bool), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).OnShowLayerSeparatorsChanged()));
            StretchContentProperty = DependencyProperty.Register("StretchContent", typeof(bool), typeof(FlowLayoutControl), new PropertyMetadata((o, e) => ((FlowLayoutControl) o).OnStretchContentChanged()));
            LayoutControlStrategyRegistrator.RegisterFlowLayoutControlStrategy();
        }

        public FlowLayoutControl()
        {
            About.CheckLicenseShowNagScreen(typeof(FlowLayoutControl));
            this.LayerSeparators = new DevExpress.Xpf.LayoutControl.LayerSeparators(this);
            this.LayerSeparators.AreVisible = this.ShowLayerSeparators;
        }

        private IEnumerable<UIElement> BaseGetInternalElements() => 
            base.GetInternalElements();

        protected override PanelControllerBase CreateController() => 
            new FlowLayoutController(this);

        protected virtual FrameworkElement CreateItem() => 
            new ContentPresenter();

        protected override LayoutProviderBase CreateLayoutProvider() => 
            new FlowLayoutProvider(this);

        protected override LayoutParametersBase CreateLayoutProviderParameters() => 
            new FlowLayoutParameters(base.ItemSpace, this.LayerSpace, this.BreakFlowToFit, this.StretchContent, this.ShowLayerSeparators, this.LayerSeparators);

        void IFlowLayoutControl.BringSeparatorsToFront()
        {
            this.LayerSeparators.BringToFront();
        }

        bool IFlowLayoutControl.IsLayerSeparator(UIElement element) => 
            this.LayerSeparators.IsItem(element);

        void IFlowLayoutControl.OnAllowLayerSizingChanged()
        {
            this.LayerSeparators.AreInteractive = this.Controller.AllowLayerSizing;
        }

        void IFlowLayoutControl.OnItemPositionChanged(int oldPosition, int newPosition)
        {
            this.OnItemPositionChanged(oldPosition, newPosition);
        }

        void IFlowLayoutControl.SendSeparatorsToBack()
        {
            this.LayerSeparators.SendToBack();
        }

        protected override Rect GetChildBounds(FrameworkElement child) => 
            !ReferenceEquals(child, this.MaximizedElement) ? base.GetChildBounds(child) : Rect.Empty;

        protected override Rect GetChildrenBounds()
        {
            Rect childrenBounds = base.GetChildrenBounds();
            if (this.MaximizedElement != null)
            {
                this.LayoutProvider.UpdateChildrenBounds(ref childrenBounds, base.GetChildBounds(this.MaximizedElement));
            }
            return childrenBounds;
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__128))]
        protected override IEnumerable<UIElement> GetInternalElements()
        {
            IEnumerator<UIElement> enumerator = this.BaseGetInternalElements().GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                UIElement current = enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                enumerator = this.LayerSeparators.GetInternalElements().GetEnumerator();
            }
            if (!enumerator.MoveNext())
            {
                enumerator = null;
                yield break;
            }
            else
            {
                UIElement current = enumerator.Current;
                yield return current;
                yield break;
            }
        }

        public static bool GetIsFlowBreak(UIElement element) => 
            (bool) element.GetValue(IsFlowBreakProperty);

        protected virtual FrameworkElement GetNeighborChild(FrameworkElement child)
        {
            FrameworkElements logicalChildren = base.GetLogicalChildren(true);
            int index = logicalChildren.IndexOf(child);
            if (index < (logicalChildren.Count - 1))
            {
                index++;
            }
            else
            {
                if (index <= 0)
                {
                    return null;
                }
                index--;
            }
            return logicalChildren[index];
        }

        protected virtual void InitItem(FrameworkElement item)
        {
            Binding binding = new Binding("DataContext");
            binding.Source = item;
            item.SetBinding(ContentPresenter.ContentProperty, binding);
        }

        protected override bool IsTempChild(UIElement child) => 
            base.IsTempChild(child) || this.LayerSeparators.IsItem(child);

        protected void MoveLogicalChildrenToStart()
        {
            for (int i = base.Children.Count - 1; i >= 0; i--)
            {
                UIElement child = base.Children[i];
                if (!base.IsLogicalChild(child))
                {
                    base.Children.RemoveAt(i);
                    base.Children.Add(child);
                }
            }
        }

        protected override Size OnArrange(Rect bounds)
        {
            this.LayerSeparators.MarkItemsAsUnused();
            Size size = base.OnArrange(bounds);
            this.LayerSeparators.DeleteUnusedItems();
            return size;
        }

        protected override void OnAttachedPropertyChanged(FrameworkElement child, DependencyProperty property, object oldValue, object newValue)
        {
            base.OnAttachedPropertyChanged(child, property, oldValue, newValue);
            if (ReferenceEquals(property, IsFlowBreakProperty))
            {
                this.OnIsFlowBreakChanged(child);
            }
        }

        protected virtual void OnBreakFlowToFitChanged()
        {
            base.SetOffset(new Point(0.0, 0.0));
            base.Changed();
        }

        protected override void OnChildRemoved(FrameworkElement child)
        {
            base.OnChildRemoved(child);
            if (ReferenceEquals(child, this.MaximizedElement))
            {
                this.MaximizedElement = null;
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new FlowLayoutControlAutomationPeer(this);

        protected virtual void OnIsFlowBreakChanged(FrameworkElement child)
        {
            base.Changed();
        }

        protected virtual void OnItemContainerStyleChanged(DependencyPropertyChangedEventArgs e)
        {
            ItemsAttachedBehaviorCore<FlowLayoutControl, FrameworkElement>.OnItemsGeneratorTemplatePropertyChanged(this, e, ItemsAttachedBehaviorProperty);
        }

        protected virtual void OnItemPositionChanged(int oldPosition, int newPosition)
        {
            if (newPosition != oldPosition)
            {
                ItemsAttachedBehaviorCore<FlowLayoutControl, FrameworkElement>.OnTargetItemPositionChanged(this, ItemsAttachedBehaviorProperty, oldPosition, newPosition);
            }
            if (this.ItemPositionChanged != null)
            {
                this.ItemPositionChanged(this, new ValueChangedEventArgs<int>(oldPosition, newPosition));
            }
        }

        protected virtual void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Func<FlowLayoutControl, IList> getTargetFunction = <>c.<>9__157_0;
            if (<>c.<>9__157_0 == null)
            {
                Func<FlowLayoutControl, IList> local1 = <>c.<>9__157_0;
                getTargetFunction = <>c.<>9__157_0 = control => control.Children;
            }
            ItemsAttachedBehaviorCore<FlowLayoutControl, FrameworkElement>.OnItemsSourcePropertyChanged(this, e, ItemsAttachedBehaviorProperty, ItemTemplateProperty, ItemTemplateSelectorProperty, ItemContainerStyleProperty, getTargetFunction, <>c.<>9__157_1 ??= control => control.CreateItem(), null, item => this.InitItem(item), null, null, false, true, null, false);
        }

        protected virtual void OnItemTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            ItemsAttachedBehaviorCore<FlowLayoutControl, FrameworkElement>.OnItemsGeneratorTemplatePropertyChanged(this, e, ItemsAttachedBehaviorProperty);
        }

        protected virtual void OnItemTemplateSelectorChanged(DependencyPropertyChangedEventArgs e)
        {
            ItemsAttachedBehaviorCore<FlowLayoutControl, FrameworkElement>.OnItemsGeneratorTemplatePropertyChanged(this, e, ItemsAttachedBehaviorProperty);
        }

        protected virtual void OnLayerSpaceChanged(double oldValue, double newValue)
        {
            base.Changed();
        }

        protected virtual void OnMaximizedElementChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {
            if (!this.IsInDesignTool() && (this.IsInVisualTree() && this.AnimateItemMaximization))
            {
                if (this.ItemMaximizationAnimation != null)
                {
                    this.ItemMaximizationAnimation.Stop();
                }
                this.ItemMaximizationAnimation = new ElementBoundsAnimation(base.GetLogicalChildren(true));
                this.ItemMaximizationAnimation.StoreOldElementBounds(null);
            }
            if (oldValue == null)
            {
                this.OffsetBeforeElementMaximization = base.Offset;
            }
            if (oldValue != null)
            {
                if (double.IsPositiveInfinity(this.MaximizedElementOriginalSize.Width))
                {
                    oldValue.ClearValue(FrameworkElement.WidthProperty);
                }
                else
                {
                    oldValue.Width = this.MaximizedElementOriginalSize.Width;
                }
                if (double.IsPositiveInfinity(this.MaximizedElementOriginalSize.Height))
                {
                    oldValue.ClearValue(FrameworkElement.HeightProperty);
                }
                else
                {
                    oldValue.Height = this.MaximizedElementOriginalSize.Height;
                }
                if (oldValue is IMaximizableElement)
                {
                    ((IMaximizableElement) oldValue).AfterNormalization();
                }
            }
            if (newValue != null)
            {
                if (newValue is IMaximizableElement)
                {
                    ((IMaximizableElement) newValue).BeforeMaximization();
                }
                Size size2 = new Size(newValue.Width, newValue.Height);
                if (!newValue.IsPropertyAssigned(FrameworkElement.WidthProperty))
                {
                    size2.Width = double.PositiveInfinity;
                }
                if (!newValue.IsPropertyAssigned(FrameworkElement.HeightProperty))
                {
                    size2.Height = double.PositiveInfinity;
                }
                this.MaximizedElementOriginalSize = size2;
                newValue.Width = double.NaN;
                newValue.Height = double.NaN;
            }
            base.Changed();
            if (oldValue == null)
            {
                base.BringChildIntoView(this.GetNeighborChild(newValue), false);
            }
            if (newValue == null)
            {
                base.SetOffset(this.OffsetBeforeElementMaximization);
                base.BringChildIntoView(oldValue, false);
            }
            if (this.ItemMaximizationAnimation != null)
            {
                base.UpdateLayout();
                this.ItemMaximizationAnimation.StoreNewElementBounds(null);
                this.ItemMaximizationAnimation.Begin(ItemMaximizationAnimationDuration, new CubicEase(), () => this.ItemMaximizationAnimation = null);
            }
            if (this.MaximizedElementChanged != null)
            {
                this.MaximizedElementChanged(this, new ValueChangedEventArgs<FrameworkElement>(oldValue, newValue));
            }
        }

        protected virtual void OnMaximizedElementPositionChanged(DevExpress.Xpf.LayoutControl.MaximizedElementPosition oldValue)
        {
            base.Changed();
            if (this.MaximizedElementPositionChanged != null)
            {
                this.MaximizedElementPositionChanged(this, new ValueChangedEventArgs<DevExpress.Xpf.LayoutControl.MaximizedElementPosition>(oldValue, this.MaximizedElementPosition));
            }
        }

        protected virtual void OnOrientationChanged()
        {
            base.SetOffset(new Point(0.0, 0.0));
            base.Changed();
        }

        protected virtual void OnShowLayerSeparatorsChanged()
        {
            this.LayerSeparators.AreVisible = this.ShowLayerSeparators;
            base.Changed();
        }

        protected virtual void OnStretchContentChanged()
        {
            base.Changed();
        }

        protected override void ReadChildrenFromXML(XmlReader xml, out FrameworkElement lastChild)
        {
            base.ReadChildrenFromXML(xml, out lastChild);
            this.MoveLogicalChildrenToStart();
        }

        protected override void ReadCustomizablePropertiesFromXML(XmlReader xml)
        {
            base.ReadCustomizablePropertiesFromXML(xml);
            this.ReadPropertyFromXML(xml, MaximizedElementProperty, "MaximizedElement", typeof(FrameworkElement));
            this.MaximizedElementOriginalSize = SizeHelper.Parse(xml["MaximizedElementOriginalSize"]);
            this.ReadPropertyFromXML(xml, MaximizedElementPositionProperty, "MaximizedElementPosition", typeof(DevExpress.Xpf.LayoutControl.MaximizedElementPosition));
        }

        protected override void ReadCustomizablePropertiesFromXML(FrameworkElement element, XmlReader xml)
        {
            base.ReadCustomizablePropertiesFromXML(element, xml);
            element.ReadPropertyFromXML(xml, IsFlowBreakProperty, "IsFlowBreak", typeof(bool));
            element.ReadPropertyFromXML(xml, FrameworkElement.WidthProperty, "Width", typeof(double));
            element.ReadPropertyFromXML(xml, FrameworkElement.HeightProperty, "Height", typeof(double));
            if (element is DevExpress.Xpf.LayoutControl.GroupBox)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) element).ReadCustomizablePropertiesFromXML(xml);
            }
        }

        public static void SetIsFlowBreak(UIElement element, bool value)
        {
            element.SetValue(IsFlowBreakProperty, value);
        }

        protected override void WriteCustomizablePropertiesToXML(XmlWriter xml)
        {
            base.WriteCustomizablePropertiesToXML(xml);
            this.WritePropertyToXML(xml, MaximizedElementProperty, "MaximizedElement");
            xml.WriteAttributeString("MaximizedElementOriginalSize", this.MaximizedElementOriginalSize.ToString(CultureInfo.InvariantCulture));
            this.WritePropertyToXML(xml, MaximizedElementPositionProperty, "MaximizedElementPosition");
        }

        protected override void WriteCustomizablePropertiesToXML(FrameworkElement element, XmlWriter xml)
        {
            base.WriteCustomizablePropertiesToXML(element, xml);
            element.WritePropertyToXML(xml, IsFlowBreakProperty, "IsFlowBreak");
            element.WritePropertyToXML(xml, FrameworkElement.WidthProperty, "Width");
            element.WritePropertyToXML(xml, FrameworkElement.HeightProperty, "Height");
            if (element is DevExpress.Xpf.LayoutControl.GroupBox)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) element).WriteCustomizablePropertiesToXML(xml);
            }
        }

        [Description("Gets or sets if adding flow breaks during item movement is enabled. This is a dependency property.")]
        public bool AllowAddFlowBreaksDuringItemMoving
        {
            get => 
                (bool) base.GetValue(AllowAddFlowBreaksDuringItemMovingProperty);
            set => 
                base.SetValue(AllowAddFlowBreaksDuringItemMovingProperty, value);
        }

        [Description("Gets or sets whether items are allowed to be moved to new positions via drag-and-drop.This is a dependency property.")]
        public bool AllowItemMoving
        {
            get => 
                (bool) base.GetValue(AllowItemMovingProperty);
            set => 
                base.SetValue(AllowItemMovingProperty, value);
        }

        [Description("Gets or sets whether columns or rows of items are allowed to be resized via built-in separators. Separators must be enabled via the FlowLayoutControl.ShowLayerSeparators property.")]
        public bool AllowLayerSizing
        {
            get => 
                this.Controller.AllowLayerSizing;
            set => 
                this.Controller.AllowLayerSizing = value;
        }

        [Description("Gets or sets whether a maximized element's position can be changed at runtime via drag-and-drop.")]
        public bool AllowMaximizedElementMoving
        {
            get => 
                this.Controller.AllowMaximizedElementMoving;
            set => 
                this.Controller.AllowMaximizedElementMoving = value;
        }

        [Description("Gets or sets if changing a FlowLayoutControl's maximized item should be animated. This is a dependency property.")]
        public bool AnimateItemMaximization
        {
            get => 
                (bool) base.GetValue(AnimateItemMaximizationProperty);
            set => 
                base.SetValue(AnimateItemMaximizationProperty, value);
        }

        [Description("Gets or sets if FlowLayoutControl animation is enabled.This is a dependency property.")]
        public bool AnimateItemMoving
        {
            get => 
                (bool) base.GetValue(AnimateItemMovingProperty);
            set => 
                base.SetValue(AnimateItemMovingProperty, value);
        }

        [Description("Gets or sets whether automatic item wrapping is enabled.This is a dependency property.")]
        public bool BreakFlowToFit
        {
            get => 
                (bool) base.GetValue(BreakFlowToFitProperty);
            set => 
                base.SetValue(BreakFlowToFitProperty, value);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public FlowLayoutController Controller =>
            (FlowLayoutController) base.Controller;

        [Description("Gets if the FlowLayoutControl's item maximization animation is currently performed.")]
        public bool IsItemMaximizationAnimationActive =>
            (this.ItemMaximizationAnimation != null) && this.ItemMaximizationAnimation.IsActive;

        [Description("Gets or sets the style that is applied to the container element generated for each item. This is a dependency property.")]
        public Style ItemContainerStyle
        {
            get => 
                (Style) base.GetValue(ItemContainerStyleProperty);
            set => 
                base.SetValue(ItemContainerStyleProperty, value);
        }

        [Description("Gets or sets the duration of item movement animation in the FlowLayoutControl. This is a dependency property.")]
        public TimeSpan ItemMovingAnimationDuration
        {
            get => 
                (TimeSpan) base.GetValue(ItemMovingAnimationDurationProperty);
            set => 
                base.SetValue(ItemMovingAnimationDurationProperty, value);
        }

        [Description("Gets or sets a collection of objects providing information to generate and initialize layout items for the current FlowLayoutControl container.This is a dependency property.")]
        public IEnumerable ItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        [Description("Gets or sets the template used to visualize LayoutItems stored as elements in the FlowLayoutControl.ItemsSource collection.This is a dependency property.")]
        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        [Description("Gets or sets an object that chooses a template used to visualize LayoutItems stored as elements in the FlowLayoutControl.ItemsSource collection. This is a dependency property.")]
        public DataTemplateSelector ItemTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ItemTemplateSelectorProperty);
            set => 
                base.SetValue(ItemTemplateSelectorProperty, value);
        }

        [Description("Gets the minimum width of columns (if items are arranged in columns) or height of rows (if items are arranged in rows).")]
        public double LayerMinWidth =>
            Math.Max(DefaultLayerMinWidth, this.LayoutProvider.GetLayerMinWidth(base.ChildrenMinSize));

        [Description("Gets the maximum width of columns (if items are arranged in columns) or height of rows (if items are arranged in rows).")]
        public double LayerMaxWidth =>
            this.LayoutProvider.GetLayerMaxWidth(base.ChildrenMaxSize);

        [Description("Gets or sets the style applied to separators.This is a dependency property.")]
        public Style LayerSeparatorStyle
        {
            get => 
                (Style) base.GetValue(LayerSeparatorStyleProperty);
            set => 
                base.SetValue(LayerSeparatorStyleProperty, value);
        }

        [Description("Gets or sets the brush used to fill the FlowLayoutControl when columns/rows are being resized.This is a dependency property.")]
        public Brush LayerSizingCoverBrush
        {
            get => 
                (Brush) base.GetValue(LayerSizingCoverBrushProperty);
            set => 
                base.SetValue(LayerSizingCoverBrushProperty, value);
        }

        [Description("Gets or sets the outer space for columns/rows.This is a dependency property.")]
        public double LayerSpace
        {
            get => 
                (double) base.GetValue(LayerSpaceProperty);
            set => 
                base.SetValue(LayerSpaceProperty, Math.Max(0.0, value));
        }

        [Description("Gets or sets the width/height of layers (columns/rows) into which items are arranged.")]
        public double LayerWidth
        {
            get
            {
                Size size;
                return this.LayoutProvider.GetLayerWidth(base.GetLogicalChildren(false), out size);
            }
            set
            {
                Size size;
                value = Math.Max(this.LayerMinWidth, Math.Min(value, this.LayerMaxWidth));
                FrameworkElements logicalChildren = base.GetLogicalChildren(false);
                if (this.LayoutProvider.GetLayerWidth(logicalChildren, out size) != value)
                {
                    Size size2;
                    this.LayoutProvider.SetLayerWidth(logicalChildren, value, out size2);
                    if (this.MaximizedElement != null)
                    {
                        this.MaximizedElementOriginalSize = new Size(double.IsInfinity(size2.Width) ? this.MaximizedElementOriginalSize.Width : size2.Width, double.IsInfinity(size2.Height) ? this.MaximizedElementOriginalSize.Height : size2.Height);
                    }
                    if (this.ItemsSizeChanged != null)
                    {
                        this.ItemsSizeChanged(this, new ValueChangedEventArgs<Size>(size, size2));
                    }
                }
            }
        }

        [Description("Gets or sets the FlowLayoutControl's item that is maximized.This is a dependency property.")]
        public FrameworkElement MaximizedElement
        {
            get => 
                (FrameworkElement) base.GetValue(MaximizedElementProperty);
            set => 
                base.SetValue(MaximizedElementProperty, value);
        }

        [Description("Gets or sets the original size of the maximized element, exhibited before it was maximized.")]
        public Size MaximizedElementOriginalSize { get; set; }

        [Description("Gets or sets the position of the FlowLayoutControl.MaximizedElement relative to the remaining items. This is a dependency property.")]
        public DevExpress.Xpf.LayoutControl.MaximizedElementPosition MaximizedElementPosition
        {
            get => 
                (DevExpress.Xpf.LayoutControl.MaximizedElementPosition) base.GetValue(MaximizedElementPositionProperty);
            set => 
                base.SetValue(MaximizedElementPositionProperty, value);
        }

        [Description("Gets or sets the style applied to the maximized element's drag-and-drop indicator.This is a dependency property.")]
        public Style MaximizedElementPositionIndicatorStyle
        {
            get => 
                (Style) base.GetValue(MaximizedElementPositionIndicatorStyleProperty);
            set => 
                base.SetValue(MaximizedElementPositionIndicatorStyleProperty, value);
        }

        [Description("Gets or sets the brush used to paint the placeholder for the item that is being dragged.This is a dependency property.")]
        public Brush MovingItemPlaceHolderBrush
        {
            get => 
                (Brush) base.GetValue(MovingItemPlaceHolderBrushProperty);
            set => 
                base.SetValue(MovingItemPlaceHolderBrushProperty, value);
        }

        [Description("Gets or sets whether the control's items are arranged in columns or rows. This is a dependency property.")]
        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        [Description("Gets or sets whether separators between columns or rows of items are visible.This is a dependency property.")]
        public bool ShowLayerSeparators
        {
            get => 
                (bool) base.GetValue(ShowLayerSeparatorsProperty);
            set => 
                base.SetValue(ShowLayerSeparatorsProperty, value);
        }

        [Description("Gets or sets whether items are stretched to fit the control's width/height.This is a dependency property.")]
        public bool StretchContent
        {
            get => 
                (bool) base.GetValue(StretchContentProperty);
            set => 
                base.SetValue(StretchContentProperty, value);
        }

        protected override bool NeedsChildChangeNotifications =>
            true;

        protected FlowLayoutProvider LayoutProvider =>
            (FlowLayoutProvider) base.LayoutProvider;

        protected Point OffsetBeforeElementMaximization { get; set; }

        protected ElementBoundsAnimation ItemMaximizationAnimation { get; private set; }

        protected DevExpress.Xpf.LayoutControl.LayerSeparators LayerSeparators { get; private set; }

        FrameworkElement IFlowLayoutModel.MaximizedElement =>
            ((this.MaximizedElement == null) || !this.MaximizedElement.GetVisible()) ? null : this.MaximizedElement;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FlowLayoutControl.<>c <>9 = new FlowLayoutControl.<>c();
            public static Func<FlowLayoutControl, IList> <>9__157_0;
            public static Func<FlowLayoutControl, FrameworkElement> <>9__157_1;

            internal void <.cctor>b__30_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnBreakFlowToFitChanged();
            }

            internal void <.cctor>b__30_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnItemContainerStyleChanged(e);
            }

            internal void <.cctor>b__30_10(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnShowLayerSeparatorsChanged();
            }

            internal void <.cctor>b__30_11(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnStretchContentChanged();
            }

            internal void <.cctor>b__30_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnItemsSourceChanged(e);
            }

            internal void <.cctor>b__30_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnItemTemplateChanged(e);
            }

            internal void <.cctor>b__30_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnItemTemplateSelectorChanged(e);
            }

            internal void <.cctor>b__30_5(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).LayerSeparators.ItemStyle = (Style) e.NewValue;
            }

            internal void <.cctor>b__30_6(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnLayerSpaceChanged((double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__30_7(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                if (!FlowLayoutControl._IgnoreMaximizedElementChange)
                {
                    FlowLayoutControl objB = (FlowLayoutControl) o;
                    if ((e.NewValue == null) || ReferenceEquals(((FrameworkElement) e.NewValue).Parent, objB))
                    {
                        objB.OnMaximizedElementChanged((FrameworkElement) e.OldValue, (FrameworkElement) e.NewValue);
                    }
                    else
                    {
                        FlowLayoutControl._IgnoreMaximizedElementChange = true;
                        o.SetValue(e.Property, e.OldValue);
                        FlowLayoutControl._IgnoreMaximizedElementChange = false;
                        throw new ArgumentOutOfRangeException("MaximizedElement");
                    }
                }
            }

            internal void <.cctor>b__30_8(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnMaximizedElementPositionChanged((MaximizedElementPosition) e.OldValue);
            }

            internal void <.cctor>b__30_9(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((FlowLayoutControl) o).OnOrientationChanged();
            }

            internal IList <OnItemsSourceChanged>b__157_0(FlowLayoutControl control) => 
                control.Children;

            internal FrameworkElement <OnItemsSourceChanged>b__157_1(FlowLayoutControl control) => 
                control.CreateItem();
        }

    }
}

