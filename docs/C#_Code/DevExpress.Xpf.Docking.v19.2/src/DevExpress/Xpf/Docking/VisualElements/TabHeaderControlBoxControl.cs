namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [TemplatePart(Name="PART_ScrollPrevButton", Type=typeof(ControlBoxButtonPresenter)), TemplatePart(Name="PART_ScrollNextButton", Type=typeof(ControlBoxButtonPresenter)), TemplatePart(Name="PART_DropDownButton", Type=typeof(ControlBoxButtonPresenter)), TemplatePart(Name="PART_RestoreButton", Type=typeof(ControlBoxButtonPresenter))]
    public class TabHeaderControlBoxControl : BaseControlBoxControl
    {
        public static readonly DependencyProperty ScrollPrevButtonTemplateProperty;
        public static readonly DependencyProperty ScrollNextButtonTemplateProperty;
        public static readonly DependencyProperty DropDownButtonTemplateProperty;
        public static readonly DependencyProperty RestoreButtonTemplateProperty;
        public static readonly DependencyProperty LocationProperty;
        public static readonly DependencyProperty CanScrollNextProperty;
        public static readonly DependencyProperty CanScrollPrevProperty;
        private Size _measureResult;

        static TabHeaderControlBoxControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TabHeaderControlBoxControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TabHeaderControlBoxControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("ScrollPrevButtonTemplate", ref ScrollPrevButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("ScrollNextButtonTemplate", ref ScrollNextButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("DropDownButtonTemplate", ref DropDownButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("RestoreButtonTemplate", ref RestoreButtonTemplateProperty, null, null, null);
            registrator.RegisterAttachedInherited<CaptionLocation>("Location", ref LocationProperty, CaptionLocation.Default, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TabHeaderControlBoxControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TabHeaderControlBoxControl> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TabHeaderControlBoxControl>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<TabHeaderControlBoxControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TabHeaderControlBoxControl.get_CanScrollNext)), parameters), out CanScrollNextProperty, false, d => d.OnCanScrollNextChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TabHeaderControlBoxControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<TabHeaderControlBoxControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TabHeaderControlBoxControl.get_CanScrollPrev)), expressionArray2), out CanScrollPrevProperty, false, d => d.OnCanScrollPrevChanged(), frameworkOptions);
        }

        protected override void ClearControlBoxBindings()
        {
            if (this.PartScrollPrevButton != null)
            {
                this.PartScrollPrevButton.ClearValue(UIElement.VisibilityProperty);
                this.PartScrollPrevButton.ClearValue(UIElement.IsEnabledProperty);
            }
            if (this.PartScrollNextButton != null)
            {
                this.PartScrollNextButton.ClearValue(UIElement.VisibilityProperty);
                this.PartScrollNextButton.ClearValue(UIElement.IsEnabledProperty);
            }
            if (this.PartDropDownButton != null)
            {
                this.PartDropDownButton.ClearValue(UIElement.VisibilityProperty);
            }
            if (this.PartRestoreButton != null)
            {
                this.PartRestoreButton.ClearValue(UIElement.VisibilityProperty);
            }
            if (this.PartStackPanel != null)
            {
                this.PartStackPanel.ClearValue(StackPanel.OrientationProperty);
            }
            base.ClearControlBoxBindings();
        }

        protected override void EnsureTemplateChildren()
        {
            base.EnsureTemplateChildren();
            this.PartScrollPrevButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartScrollPrevButton, "PART_ScrollPrevButton");
            this.PartScrollNextButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartScrollPrevButton, "PART_ScrollNextButton");
            this.PartDropDownButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartDropDownButton, "PART_DropDownButton");
            this.PartRestoreButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartRestoreButton, "PART_RestoreButton");
            this.PartStackPanel = base.GetTemplateChild("PART_StackPanel") as StackPanel;
        }

        public static CaptionLocation GetLocation(DependencyObject obj) => 
            (CaptionLocation) obj.GetValue(LocationProperty);

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = base.MeasureOverride(constraint);
            if (size != this._measureResult)
            {
                Func<UIElement, bool> evaluator = <>c.<>9__52_0;
                if (<>c.<>9__52_0 == null)
                {
                    Func<UIElement, bool> local1 = <>c.<>9__52_0;
                    evaluator = <>c.<>9__52_0 = x => x.GetArrangeInProgress();
                }
                Action<UIElement> action = <>c.<>9__52_1;
                if (<>c.<>9__52_1 == null)
                {
                    Action<UIElement> local2 = <>c.<>9__52_1;
                    action = <>c.<>9__52_1 = delegate (UIElement x) {
                        x.InvalidateMeasure();
                    };
                }
                (base.Parent as UIElement).If<UIElement>(evaluator).Do<UIElement>(action);
                this._measureResult = size;
            }
            return size;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.PartScrollPrevButton != null)
            {
                this.PartScrollPrevButton.AttachToolTip(GetToolTip(DockingStringId.ControlButtonScrollPrev));
            }
            if (this.PartScrollNextButton != null)
            {
                this.PartScrollNextButton.AttachToolTip(GetToolTip(DockingStringId.ControlButtonScrollNext));
            }
            if (this.PartRestoreButton != null)
            {
                this.PartRestoreButton.AttachToolTip(GetToolTip(DockingStringId.ControlButtonRestore));
            }
        }

        protected virtual void OnCanScrollNextChanged()
        {
            base.InvalidateContextCommand();
        }

        protected virtual void OnCanScrollPrevChanged()
        {
            base.InvalidateContextCommand();
        }

        protected override void OnDispose()
        {
            if (this.PartScrollPrevButton != null)
            {
                this.PartScrollPrevButton.Dispose();
                this.PartScrollPrevButton = null;
            }
            if (this.PartScrollNextButton != null)
            {
                this.PartScrollNextButton.Dispose();
                this.PartScrollNextButton = null;
            }
            if (this.PartDropDownButton != null)
            {
                this.PartDropDownButton.Dispose();
                this.PartDropDownButton = null;
            }
            if (this.PartRestoreButton != null)
            {
                this.PartRestoreButton.Dispose();
                this.PartRestoreButton = null;
            }
            base.OnDispose();
        }

        protected override void SetControlBoxBindings()
        {
            base.SetControlBoxBindings();
            if (this.PartScrollPrevButton != null)
            {
                BindingHelper.SetBinding(this.PartScrollPrevButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsScrollPrevButtonVisibleProperty, new BooleanToVisibilityConverter());
                BindingHelper.SetBinding(this.PartScrollPrevButton, UIElement.IsEnabledProperty, base.LayoutItem, LayoutGroup.TabHeaderCanScrollPrevProperty, BindingMode.OneWay);
            }
            if (this.PartScrollNextButton != null)
            {
                BindingHelper.SetBinding(this.PartScrollNextButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsScrollNextButtonVisibleProperty, new BooleanToVisibilityConverter());
                BindingHelper.SetBinding(this.PartScrollNextButton, UIElement.IsEnabledProperty, base.LayoutItem, LayoutGroup.TabHeaderCanScrollNextProperty, BindingMode.OneWay);
            }
            if (this.PartDropDownButton != null)
            {
                BindingHelper.SetBinding(this.PartDropDownButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsDropDownButtonVisibleProperty, new BooleanToVisibilityConverter());
            }
            if (this.PartRestoreButton != null)
            {
                BindingHelper.SetBinding(this.PartRestoreButton, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsRestoreButtonVisibleProperty, new BooleanToVisibilityConverter());
            }
            if (this.PartStackPanel != null)
            {
                BindingHelper.SetBinding(this.PartStackPanel, StackPanel.OrientationProperty, base.LayoutItem, BaseLayoutItem.CaptionLocationProperty, new TabHeaderCaptionLocationToOrientationConverter());
            }
            BindingHelper.SetBinding(this, CanScrollNextProperty, base.LayoutItem, LayoutGroup.TabHeaderCanScrollNextProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(this, CanScrollPrevProperty, base.LayoutItem, LayoutGroup.TabHeaderCanScrollPrevProperty, BindingMode.OneWay);
        }

        public static void SetLocation(DependencyObject obj, DockLayoutManager value)
        {
            obj.SetValue(LocationProperty, value);
        }

        public bool CanScrollNext
        {
            get => 
                (bool) base.GetValue(CanScrollNextProperty);
            set => 
                base.SetValue(CanScrollNextProperty, value);
        }

        public bool CanScrollPrev
        {
            get => 
                (bool) base.GetValue(CanScrollPrevProperty);
            set => 
                base.SetValue(CanScrollPrevProperty, value);
        }

        public DataTemplate DropDownButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DropDownButtonTemplateProperty);
            set => 
                base.SetValue(DropDownButtonTemplateProperty, value);
        }

        public ControlBoxButtonPresenter PartDropDownButton { get; private set; }

        public ControlBoxButtonPresenter PartRestoreButton { get; private set; }

        public ControlBoxButtonPresenter PartScrollNextButton { get; private set; }

        public ControlBoxButtonPresenter PartScrollPrevButton { get; private set; }

        public StackPanel PartStackPanel { get; set; }

        public DataTemplate RestoreButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(RestoreButtonTemplateProperty);
            set => 
                base.SetValue(RestoreButtonTemplateProperty, value);
        }

        public DataTemplate ScrollNextButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ScrollNextButtonTemplateProperty);
            set => 
                base.SetValue(ScrollNextButtonTemplateProperty, value);
        }

        public DataTemplate ScrollPrevButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ScrollPrevButtonTemplateProperty);
            set => 
                base.SetValue(ScrollPrevButtonTemplateProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabHeaderControlBoxControl.<>c <>9 = new TabHeaderControlBoxControl.<>c();
            public static Func<UIElement, bool> <>9__52_0;
            public static Action<UIElement> <>9__52_1;

            internal void <.cctor>b__7_0(TabHeaderControlBoxControl d)
            {
                d.OnCanScrollNextChanged();
            }

            internal void <.cctor>b__7_1(TabHeaderControlBoxControl d)
            {
                d.OnCanScrollPrevChanged();
            }

            internal bool <MeasureOverride>b__52_0(UIElement x) => 
                x.GetArrangeInProgress();

            internal void <MeasureOverride>b__52_1(UIElement x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

