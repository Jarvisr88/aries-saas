namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class DocumentSelectorPreview : psvControl
    {
        public static readonly DependencyProperty TargetProperty;
        private static readonly DependencyPropertyKey PreviewBrushPropertyKey;
        public static readonly DependencyProperty PreviewBrushProperty;
        private static readonly DependencyPropertyKey PreviewWidthPropertyKey;
        public static readonly DependencyProperty PreviewWidthProperty;
        private static readonly DependencyPropertyKey PreviewHeightPropertyKey;
        public static readonly DependencyProperty PreviewHeightProperty;
        private static readonly DependencyPropertyKey CutHorizontalPropertyKey;
        public static readonly DependencyProperty CutHorizontalProperty;
        private static readonly DependencyPropertyKey CutVerticalPropertyKey;
        public static readonly DependencyProperty CutVerticalProperty;
        private static readonly DependencyPropertyKey BorderTemplatePropertyKey;
        public static readonly DependencyProperty BorderTemplateProperty;
        public static readonly DependencyProperty DocumentBorderTemplateProperty;
        public static readonly DependencyProperty PanelBorderTemplateProperty;
        public static readonly DependencyProperty IsInPreviewProperty;
        private DetachedElementDecorator detachedElementDecoratorCore;
        private Dictionary<BaseLayoutItem, FrameworkElement> elementCache = new Dictionary<BaseLayoutItem, FrameworkElement>(4);

        static DocumentSelectorPreview()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DocumentSelectorPreview> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DocumentSelectorPreview>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<LayoutPanel>("Target", ref TargetProperty, null, (dObj, e) => ((DocumentSelectorPreview) dObj).OnTargetChanged((LayoutPanel) e.OldValue, (LayoutPanel) e.NewValue), null);
            registrator.RegisterReadonly<VisualBrush>("PreviewBrush", ref PreviewBrushPropertyKey, ref PreviewBrushProperty, null, null, null);
            registrator.RegisterReadonly<double>("PreviewWidth", ref PreviewWidthPropertyKey, ref PreviewWidthProperty, 150.0, null, null);
            registrator.RegisterReadonly<double>("PreviewHeight", ref PreviewHeightPropertyKey, ref PreviewHeightProperty, 250.0, null, null);
            registrator.RegisterReadonly<bool>("CutHorizontal", ref CutHorizontalPropertyKey, ref CutHorizontalProperty, false, null, null);
            registrator.RegisterReadonly<bool>("CutVertical", ref CutVerticalPropertyKey, ref CutVerticalProperty, false, null, null);
            registrator.RegisterReadonly<DataTemplate>("BorderTemplate", ref BorderTemplatePropertyKey, ref BorderTemplateProperty, null, null, (CoerceValueCallback) ((dObj, value) => ((DocumentSelectorPreview) dObj).CoerceBorderTemplate((DataTemplate) value)));
            registrator.Register<DataTemplate>("DocumentBorderTemplate", ref DocumentBorderTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("PanelBorderTemplate", ref PanelBorderTemplateProperty, null, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DocumentSelectorPreview>.New().RegisterAttached<DependencyObject, bool>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, bool>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DocumentSelectorPreview.GetIsInPreview), arguments), parameters), out IsInPreviewProperty, false, 0x20);
        }

        public DocumentSelectorPreview()
        {
            VisualBrush brush1 = new VisualBrush();
            brush1.AlignmentX = AlignmentX.Left;
            brush1.AlignmentY = AlignmentY.Top;
            brush1.Stretch = Stretch.None;
            this.PreviewBrush = brush1;
        }

        protected virtual DataTemplate CoerceBorderTemplate(DataTemplate value)
        {
            BaseLayoutItem target = this.Target;
            return ((target != null) ? ((target.ItemType == LayoutItemType.Document) ? this.DocumentBorderTemplate : this.PanelBorderTemplate) : value);
        }

        public static bool GetIsInPreview(DependencyObject target) => 
            (bool) target.GetValue(IsInPreviewProperty);

        private FrameworkElement GetPreviewElement(LayoutPanel item)
        {
            FrameworkElement contentPresenter = item.ContentPresenter as FrameworkElement;
            if (contentPresenter == null)
            {
                if (this.elementCache.ContainsKey(item))
                {
                    contentPresenter = this.elementCache[item];
                }
                else
                {
                    ContentPresenter presenter1 = new ContentPresenter();
                    presenter1.Content = item.Content;
                    presenter1.ContentTemplate = item.ContentTemplate;
                    presenter1.ContentTemplateSelector = item.ContentTemplateSelector;
                    contentPresenter = presenter1;
                    base.AddLogicalChild(contentPresenter);
                    this.elementCache.Add(item, contentPresenter);
                }
            }
            return contentPresenter;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartPreview = base.GetTemplateChild("PART_Preview") as Border;
            if (this.PartPreview != null)
            {
                this.PartPreview.FlowDirection = FlowDirection.LeftToRight;
            }
            this.PartView = base.GetTemplateChild("PART_View") as FrameworkElement;
            this.UpdateCut();
        }

        internal void OnClosing()
        {
            if (this.detachedElementDecoratorCore != null)
            {
                this.detachedElementDecoratorCore.Child = null;
            }
            this.elementCache.Clear();
        }

        protected virtual void OnTargetChanged(LayoutPanel oldTarget, LayoutPanel target)
        {
            if (oldTarget != null)
            {
                SetIsInPreview(oldTarget, false);
            }
            if (target != null)
            {
                SetIsInPreview(target, true);
            }
            base.CoerceValue(BorderTemplateProperty);
            FrameworkElement reference = null;
            if (target != null)
            {
                reference = this.GetPreviewElement(target);
                if (reference != null)
                {
                    if (!reference.IsMeasureValid)
                    {
                        reference.Measure(new Size(1000.0, 1000.0));
                    }
                    if (!reference.IsArrangeValid)
                    {
                        Point location = new Point();
                        reference.Arrange(new Rect(location, new Size(1000.0, 1000.0)));
                    }
                    this.PreviewWidth = Math.Max(48.0, Math.Max(reference.DesiredSize.Width, reference.ActualWidth));
                    this.PreviewHeight = Math.Max(48.0, Math.Max(reference.DesiredSize.Height, reference.ActualHeight));
                    this.UpdateCut();
                }
            }
            if (reference == null)
            {
                TextBlock block1 = new TextBlock();
                block1.Text = "Preview unavailable";
                block1.HorizontalAlignment = HorizontalAlignment.Center;
                block1.VerticalAlignment = VerticalAlignment.Center;
                reference = block1;
                base.ClearValue(PreviewWidthPropertyKey);
                base.ClearValue(PreviewHeightPropertyKey);
                base.ClearValue(CutHorizontalPropertyKey);
                base.ClearValue(CutVerticalPropertyKey);
            }
            if (this.detachedElementDecoratorCore != null)
            {
                this.detachedElementDecoratorCore.Child = null;
            }
            if (VisualTreeHelper.GetParent(reference) != null)
            {
                if (this.PartPreview != null)
                {
                    this.PartPreview.FlowDirection = base.FlowDirection;
                }
                this.PreviewBrush.Visual = reference;
            }
            else
            {
                if (this.PartPreview != null)
                {
                    this.PartPreview.FlowDirection = FlowDirection.LeftToRight;
                }
                DetachedElementDecorator decorator1 = new DetachedElementDecorator();
                decorator1.Width = this.PreviewWidth;
                decorator1.Height = this.PreviewHeight;
                this.detachedElementDecoratorCore = decorator1;
                this.detachedElementDecoratorCore.Child = reference;
                this.PreviewBrush.Visual = this.detachedElementDecoratorCore;
            }
            base.InvalidateMeasure();
            base.UpdateLayout();
        }

        public static void SetIsInPreview(DependencyObject target, bool value)
        {
            target.SetValue(IsInPreviewProperty, value);
        }

        private void UpdateCut()
        {
            if (this.PartView != null)
            {
                this.CutHorizontal = this.PreviewWidth > this.PartView.ActualWidth;
                this.CutVertical = this.PreviewHeight > this.PartView.ActualHeight;
            }
        }

        public LayoutPanel Target
        {
            get => 
                (LayoutPanel) base.GetValue(TargetProperty);
            set => 
                base.SetValue(TargetProperty, value);
        }

        public VisualBrush PreviewBrush
        {
            get => 
                (VisualBrush) base.GetValue(PreviewBrushProperty);
            private set => 
                base.SetValue(PreviewBrushPropertyKey, value);
        }

        public double PreviewHeight
        {
            get => 
                (double) base.GetValue(PreviewHeightProperty);
            private set => 
                base.SetValue(PreviewHeightPropertyKey, value);
        }

        public double PreviewWidth
        {
            get => 
                (double) base.GetValue(PreviewWidthProperty);
            private set => 
                base.SetValue(PreviewWidthPropertyKey, value);
        }

        public bool CutHorizontal
        {
            get => 
                (bool) base.GetValue(CutHorizontalProperty);
            private set => 
                base.SetValue(CutHorizontalPropertyKey, value);
        }

        public bool CutVertical
        {
            get => 
                (bool) base.GetValue(CutVerticalProperty);
            private set => 
                base.SetValue(CutVerticalPropertyKey, value);
        }

        public DataTemplate BorderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(BorderTemplateProperty);
            private set => 
                base.SetValue(BorderTemplatePropertyKey, value);
        }

        public DataTemplate DocumentBorderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DocumentBorderTemplateProperty);
            set => 
                base.SetValue(DocumentBorderTemplateProperty, value);
        }

        public DataTemplate PanelBorderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PanelBorderTemplateProperty);
            set => 
                base.SetValue(PanelBorderTemplateProperty, value);
        }

        public Border PartPreview { get; private set; }

        public FrameworkElement PartView { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentSelectorPreview.<>c <>9 = new DocumentSelectorPreview.<>c();

            internal void <.cctor>b__16_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentSelectorPreview) dObj).OnTargetChanged((LayoutPanel) e.OldValue, (LayoutPanel) e.NewValue);
            }

            internal object <.cctor>b__16_1(DependencyObject dObj, object value) => 
                ((DocumentSelectorPreview) dObj).CoerceBorderTemplate((DataTemplate) value);
        }

        internal class DetachedElementDecorator : Decorator
        {
            private UIElement childCore;

            public DetachedElementDecorator()
            {
                base.InheritanceBehavior = InheritanceBehavior.SkipToAppNow;
            }

            protected override Visual GetVisualChild(int index) => 
                this.childCore;

            private static bool IsInfiniteSize(Size size) => 
                double.IsPositiveInfinity(size.Width) && double.IsPositiveInfinity(size.Height);

            protected override Size MeasureOverride(Size constraint) => 
                this.MeasureOverride(IsInfiniteSize(constraint) ? new Size(1000.0, 1000.0) : constraint);

            public override UIElement Child
            {
                get => 
                    this.childCore;
                set
                {
                    if (!ReferenceEquals(this.childCore, value))
                    {
                        if (this.childCore != null)
                        {
                            base.RemoveVisualChild(this.childCore);
                        }
                        this.childCore = value;
                        if (this.childCore != null)
                        {
                            base.AddVisualChild(this.childCore);
                        }
                    }
                }
            }

            protected override int VisualChildrenCount =>
                (this.childCore == null) ? 0 : 1;
        }
    }
}

