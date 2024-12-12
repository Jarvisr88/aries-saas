namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [TemplatePart(Name="PART_PinButton", Type=typeof(ControlBoxButtonPresenter))]
    public class PanelControlBoxControl : BaseControlBoxControl
    {
        public static readonly DependencyProperty PinButtonTemplateProperty;
        public static readonly DependencyProperty MaximizeButtonTemplateProperty;
        public static readonly DependencyProperty MinimizeButtonTemplateProperty;
        public static readonly DependencyProperty RestoreButtonTemplateProperty;
        public static readonly DependencyProperty HideButtonTemplateProperty;
        public static readonly DependencyProperty ExpandButtonTemplateProperty;
        public static readonly DependencyProperty CollapseButtonTemplateProperty;
        public static readonly DependencyProperty UseOptimizedTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty DockItemStateProperty;
        public static readonly DependencyProperty ActualDockItemStateProperty;
        private static readonly DependencyPropertyKey ActualDockItemStatePropertyKey;

        static PanelControlBoxControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<PanelControlBoxControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<PanelControlBoxControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("PinButtonTemplate", ref PinButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("MaximizeButtonTemplate", ref MaximizeButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("MinimizeButtonTemplate", ref MinimizeButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("RestoreButtonTemplate", ref RestoreButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("HideButtonTemplate", ref HideButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("ExpandButtonTemplate", ref ExpandButtonTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("CollapseButtonTemplate", ref CollapseButtonTemplateProperty, null, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PanelControlBoxControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<PanelControlBoxControl> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<PanelControlBoxControl>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<PanelControlBoxControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PanelControlBoxControl.get_UseOptimizedTemplate)), parameters), out UseOptimizedTemplateProperty, true, d => d.OnUseOptimizedTemplateChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PanelControlBoxControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<PanelControlBoxControl> registrator2 = registrator1.Register<DevExpress.Xpf.Docking.DockItemState>(System.Linq.Expressions.Expression.Lambda<Func<PanelControlBoxControl, DevExpress.Xpf.Docking.DockItemState>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PanelControlBoxControl.get_DockItemState)), expressionArray2), out DockItemStateProperty, DevExpress.Xpf.Docking.DockItemState.Undefined, (d, oldValue, newValue) => d.OnDockItemStateChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PanelControlBoxControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.RegisterReadOnly<DevExpress.Xpf.Docking.DockItemState>(System.Linq.Expressions.Expression.Lambda<Func<PanelControlBoxControl, DevExpress.Xpf.Docking.DockItemState>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PanelControlBoxControl.get_ActualDockItemState)), expressionArray3), out ActualDockItemStatePropertyKey, out ActualDockItemStateProperty, DevExpress.Xpf.Docking.DockItemState.Undefined, frameworkOptions);
        }

        protected override void ClearControlBoxBindings()
        {
            Action<ControlBoxButtonPresenter> action = <>c.<>9__71_0;
            if (<>c.<>9__71_0 == null)
            {
                Action<ControlBoxButtonPresenter> local1 = <>c.<>9__71_0;
                action = <>c.<>9__71_0 = x => x.ClearValue(UIElement.VisibilityProperty);
            }
            this.PartPinButton.Do<ControlBoxButtonPresenter>(action);
            Action<ControlBoxButtonPresenter> action2 = <>c.<>9__71_1;
            if (<>c.<>9__71_1 == null)
            {
                Action<ControlBoxButtonPresenter> local2 = <>c.<>9__71_1;
                action2 = <>c.<>9__71_1 = x => x.ClearValue(UIElement.VisibilityProperty);
            }
            this.PartMaximizeButton.Do<ControlBoxButtonPresenter>(action2);
            Action<ControlBoxButtonPresenter> action3 = <>c.<>9__71_2;
            if (<>c.<>9__71_2 == null)
            {
                Action<ControlBoxButtonPresenter> local3 = <>c.<>9__71_2;
                action3 = <>c.<>9__71_2 = x => x.ClearValue(UIElement.VisibilityProperty);
            }
            this.PartMinimizeButton.Do<ControlBoxButtonPresenter>(action3);
            Action<ControlBoxButtonPresenter> action4 = <>c.<>9__71_3;
            if (<>c.<>9__71_3 == null)
            {
                Action<ControlBoxButtonPresenter> local4 = <>c.<>9__71_3;
                action4 = <>c.<>9__71_3 = x => x.ClearValue(UIElement.VisibilityProperty);
            }
            this.PartRestoreButton.Do<ControlBoxButtonPresenter>(action4);
            Action<ControlBoxButtonPresenter> action5 = <>c.<>9__71_4;
            if (<>c.<>9__71_4 == null)
            {
                Action<ControlBoxButtonPresenter> local5 = <>c.<>9__71_4;
                action5 = <>c.<>9__71_4 = x => x.ClearValue(UIElement.VisibilityProperty);
            }
            this.PartHideButton.Do<ControlBoxButtonPresenter>(action5);
            Action<ControlBoxButtonPresenter> action6 = <>c.<>9__71_5;
            if (<>c.<>9__71_5 == null)
            {
                Action<ControlBoxButtonPresenter> local6 = <>c.<>9__71_5;
                action6 = <>c.<>9__71_5 = x => x.ClearValue(UIElement.VisibilityProperty);
            }
            this.PartExpandButton.Do<ControlBoxButtonPresenter>(action6);
            Action<ControlBoxButtonPresenter> action7 = <>c.<>9__71_6;
            if (<>c.<>9__71_6 == null)
            {
                Action<ControlBoxButtonPresenter> local7 = <>c.<>9__71_6;
                action7 = <>c.<>9__71_6 = x => x.ClearValue(UIElement.VisibilityProperty);
            }
            this.PartCollapseButton.Do<ControlBoxButtonPresenter>(action7);
            base.ClearControlBoxBindings();
        }

        protected override void EnsureTemplateChildren()
        {
            base.EnsureTemplateChildren();
            this.PartPinButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartPinButton, "PART_PinButton");
            this.PartMaximizeButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartMaximizeButton, "PART_MaximizeButton");
            this.PartMinimizeButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartMinimizeButton, "PART_MinimizeButton");
            this.PartRestoreButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartRestoreButton, "PART_RestoreButton");
            this.PartHideButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartHideButton, "PART_HideButton");
            this.PartExpandButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartExpandButton, "PART_ExpandButton");
            this.PartCollapseButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartCollapseButton, "PART_CollapseButton");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Action<ControlBoxButtonPresenter> action = <>c.<>9__70_0;
            if (<>c.<>9__70_0 == null)
            {
                Action<ControlBoxButtonPresenter> local1 = <>c.<>9__70_0;
                action = <>c.<>9__70_0 = x => x.AttachToolTip(GetToolTip(DockingStringId.ControlButtonAutoHide));
            }
            this.PartPinButton.Do<ControlBoxButtonPresenter>(action);
            Action<ControlBoxButtonPresenter> action2 = <>c.<>9__70_1;
            if (<>c.<>9__70_1 == null)
            {
                Action<ControlBoxButtonPresenter> local2 = <>c.<>9__70_1;
                action2 = <>c.<>9__70_1 = x => x.AttachToolTip(GetToolTip(DockingStringId.ControlButtonMaximize));
            }
            this.PartMaximizeButton.Do<ControlBoxButtonPresenter>(action2);
            Action<ControlBoxButtonPresenter> action3 = <>c.<>9__70_2;
            if (<>c.<>9__70_2 == null)
            {
                Action<ControlBoxButtonPresenter> local3 = <>c.<>9__70_2;
                action3 = <>c.<>9__70_2 = x => x.AttachToolTip(GetToolTip(DockingStringId.ControlButtonMinimize));
            }
            this.PartMinimizeButton.Do<ControlBoxButtonPresenter>(action3);
            Action<ControlBoxButtonPresenter> action4 = <>c.<>9__70_3;
            if (<>c.<>9__70_3 == null)
            {
                Action<ControlBoxButtonPresenter> local4 = <>c.<>9__70_3;
                action4 = <>c.<>9__70_3 = x => x.AttachToolTip(GetToolTip(DockingStringId.ControlButtonRestore));
            }
            this.PartRestoreButton.Do<ControlBoxButtonPresenter>(action4);
            Action<ControlBoxButtonPresenter> action5 = <>c.<>9__70_4;
            if (<>c.<>9__70_4 == null)
            {
                Action<ControlBoxButtonPresenter> local5 = <>c.<>9__70_4;
                action5 = <>c.<>9__70_4 = x => x.AttachToolTip(GetToolTip(DockingStringId.ControlButtonHide));
            }
            this.PartHideButton.Do<ControlBoxButtonPresenter>(action5);
            Action<ControlBoxButtonPresenter> action6 = <>c.<>9__70_5;
            if (<>c.<>9__70_5 == null)
            {
                Action<ControlBoxButtonPresenter> local6 = <>c.<>9__70_5;
                action6 = <>c.<>9__70_5 = x => x.AttachToolTip(GetToolTip(DockingStringId.ControlButtonExpand));
            }
            this.PartExpandButton.Do<ControlBoxButtonPresenter>(action6);
            Action<ControlBoxButtonPresenter> action7 = <>c.<>9__70_6;
            if (<>c.<>9__70_6 == null)
            {
                Action<ControlBoxButtonPresenter> local7 = <>c.<>9__70_6;
                action7 = <>c.<>9__70_6 = x => x.AttachToolTip(GetToolTip(DockingStringId.ControlButtonCollapse));
            }
            this.PartCollapseButton.Do<ControlBoxButtonPresenter>(action7);
        }

        protected override void OnDispose()
        {
            Action<ControlBoxButtonPresenter> action = <>c.<>9__73_0;
            if (<>c.<>9__73_0 == null)
            {
                Action<ControlBoxButtonPresenter> local1 = <>c.<>9__73_0;
                action = <>c.<>9__73_0 = x => x.Dispose();
            }
            this.PartPinButton.Do<ControlBoxButtonPresenter>(action);
            Action<ControlBoxButtonPresenter> action2 = <>c.<>9__73_1;
            if (<>c.<>9__73_1 == null)
            {
                Action<ControlBoxButtonPresenter> local2 = <>c.<>9__73_1;
                action2 = <>c.<>9__73_1 = x => x.Dispose();
            }
            this.PartMaximizeButton.Do<ControlBoxButtonPresenter>(action2);
            Action<ControlBoxButtonPresenter> action3 = <>c.<>9__73_2;
            if (<>c.<>9__73_2 == null)
            {
                Action<ControlBoxButtonPresenter> local3 = <>c.<>9__73_2;
                action3 = <>c.<>9__73_2 = x => x.Dispose();
            }
            this.PartRestoreButton.Do<ControlBoxButtonPresenter>(action3);
            Action<ControlBoxButtonPresenter> action4 = <>c.<>9__73_3;
            if (<>c.<>9__73_3 == null)
            {
                Action<ControlBoxButtonPresenter> local4 = <>c.<>9__73_3;
                action4 = <>c.<>9__73_3 = x => x.Dispose();
            }
            this.PartHideButton.Do<ControlBoxButtonPresenter>(action4);
            Action<ControlBoxButtonPresenter> action5 = <>c.<>9__73_4;
            if (<>c.<>9__73_4 == null)
            {
                Action<ControlBoxButtonPresenter> local5 = <>c.<>9__73_4;
                action5 = <>c.<>9__73_4 = x => x.Dispose();
            }
            this.PartExpandButton.Do<ControlBoxButtonPresenter>(action5);
            Action<ControlBoxButtonPresenter> action6 = <>c.<>9__73_5;
            if (<>c.<>9__73_5 == null)
            {
                Action<ControlBoxButtonPresenter> local6 = <>c.<>9__73_5;
                action6 = <>c.<>9__73_5 = x => x.Dispose();
            }
            this.PartCollapseButton.Do<ControlBoxButtonPresenter>(action6);
            this.PartPinButton = null;
            this.PartMaximizeButton = null;
            this.PartRestoreButton = null;
            this.PartHideButton = null;
            this.PartExpandButton = null;
            this.PartCollapseButton = null;
            base.OnDispose();
        }

        protected virtual void OnDockItemStateChanged(object oldValue, object newValue)
        {
            this.UpdateActualDockItemState();
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem item)
        {
            base.OnLayoutItemChanged(item);
            if (base.LayoutItem is LayoutPanel)
            {
                BindingHelper.SetBinding(this, DockItemStateProperty, base.LayoutItem, LayoutPanel.DockItemStateProperty, BindingMode.OneWay);
            }
            else
            {
                base.ClearValue(DockItemStateProperty);
            }
        }

        protected virtual void OnUseOptimizedTemplateChanged()
        {
            this.UpdateActualDockItemState();
        }

        protected override void SetControlBoxBindings()
        {
            base.SetControlBoxBindings();
            this.PartPinButton.Do<ControlBoxButtonPresenter>(x => BindingHelper.SetBinding(x, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsPinButtonVisibleProperty, new BooleanToVisibilityConverter()));
            this.PartMaximizeButton.Do<ControlBoxButtonPresenter>(x => BindingHelper.SetBinding(x, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsMaximizeButtonVisibleProperty, new BooleanToVisibilityConverter()));
            this.PartMinimizeButton.Do<ControlBoxButtonPresenter>(x => BindingHelper.SetBinding(x, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsMinimizeButtonVisibleProperty, new BooleanToVisibilityConverter()));
            this.PartRestoreButton.Do<ControlBoxButtonPresenter>(x => BindingHelper.SetBinding(x, UIElement.VisibilityProperty, base.LayoutItem, BaseLayoutItem.IsRestoreButtonVisibleProperty, new BooleanToVisibilityConverter()));
            if (base.LayoutItem is LayoutPanel)
            {
                this.PartHideButton.Do<ControlBoxButtonPresenter>(x => BindingHelper.SetBinding(x, UIElement.VisibilityProperty, base.LayoutItem, LayoutPanel.IsHideButtonVisibleProperty, new BooleanToVisibilityConverter()));
                this.PartExpandButton.Do<ControlBoxButtonPresenter>(x => BindingHelper.SetBinding(x, UIElement.VisibilityProperty, base.LayoutItem, LayoutPanel.IsExpandButtonVisibleProperty, new BooleanToVisibilityConverter()));
                this.PartCollapseButton.Do<ControlBoxButtonPresenter>(x => BindingHelper.SetBinding(x, UIElement.VisibilityProperty, base.LayoutItem, LayoutPanel.IsCollapseButtonVisibleProperty, new BooleanToVisibilityConverter()));
            }
        }

        private void UpdateActualDockItemState()
        {
            LayoutPanel layoutItem = base.LayoutItem as LayoutPanel;
            if ((layoutItem != null) && this.UseOptimizedTemplate)
            {
                this.ActualDockItemState = layoutItem.IsFloating ? DevExpress.Xpf.Docking.DockItemState.Floating : layoutItem.DockItemState;
            }
            else
            {
                base.ClearValue(ActualDockItemStatePropertyKey);
            }
        }

        public DevExpress.Xpf.Docking.DockItemState ActualDockItemState
        {
            get => 
                (DevExpress.Xpf.Docking.DockItemState) base.GetValue(ActualDockItemStateProperty);
            private set => 
                base.SetValue(ActualDockItemStatePropertyKey, value);
        }

        public DataTemplate CollapseButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CollapseButtonTemplateProperty);
            set => 
                base.SetValue(CollapseButtonTemplateProperty, value);
        }

        public DevExpress.Xpf.Docking.DockItemState DockItemState
        {
            get => 
                (DevExpress.Xpf.Docking.DockItemState) base.GetValue(DockItemStateProperty);
            set => 
                base.SetValue(DockItemStateProperty, value);
        }

        public DataTemplate ExpandButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ExpandButtonTemplateProperty);
            set => 
                base.SetValue(ExpandButtonTemplateProperty, value);
        }

        public DataTemplate HideButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HideButtonTemplateProperty);
            set => 
                base.SetValue(HideButtonTemplateProperty, value);
        }

        public DataTemplate MaximizeButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MaximizeButtonTemplateProperty);
            set => 
                base.SetValue(MaximizeButtonTemplateProperty, value);
        }

        public DataTemplate MinimizeButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(MinimizeButtonTemplateProperty);
            set => 
                base.SetValue(MinimizeButtonTemplateProperty, value);
        }

        public ControlBoxButtonPresenter PartCollapseButton { get; private set; }

        public ControlBoxButtonPresenter PartExpandButton { get; private set; }

        public ControlBoxButtonPresenter PartHideButton { get; private set; }

        public ControlBoxButtonPresenter PartMaximizeButton { get; private set; }

        public ControlBoxButtonPresenter PartMinimizeButton { get; private set; }

        public ControlBoxButtonPresenter PartPinButton { get; private set; }

        public ControlBoxButtonPresenter PartRestoreButton { get; private set; }

        public DataTemplate PinButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PinButtonTemplateProperty);
            set => 
                base.SetValue(PinButtonTemplateProperty, value);
        }

        public DataTemplate RestoreButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(RestoreButtonTemplateProperty);
            set => 
                base.SetValue(RestoreButtonTemplateProperty, value);
        }

        public bool UseOptimizedTemplate
        {
            get => 
                (bool) base.GetValue(UseOptimizedTemplateProperty);
            set => 
                base.SetValue(UseOptimizedTemplateProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PanelControlBoxControl.<>c <>9 = new PanelControlBoxControl.<>c();
            public static Action<ControlBoxButtonPresenter> <>9__70_0;
            public static Action<ControlBoxButtonPresenter> <>9__70_1;
            public static Action<ControlBoxButtonPresenter> <>9__70_2;
            public static Action<ControlBoxButtonPresenter> <>9__70_3;
            public static Action<ControlBoxButtonPresenter> <>9__70_4;
            public static Action<ControlBoxButtonPresenter> <>9__70_5;
            public static Action<ControlBoxButtonPresenter> <>9__70_6;
            public static Action<ControlBoxButtonPresenter> <>9__71_0;
            public static Action<ControlBoxButtonPresenter> <>9__71_1;
            public static Action<ControlBoxButtonPresenter> <>9__71_2;
            public static Action<ControlBoxButtonPresenter> <>9__71_3;
            public static Action<ControlBoxButtonPresenter> <>9__71_4;
            public static Action<ControlBoxButtonPresenter> <>9__71_5;
            public static Action<ControlBoxButtonPresenter> <>9__71_6;
            public static Action<ControlBoxButtonPresenter> <>9__73_0;
            public static Action<ControlBoxButtonPresenter> <>9__73_1;
            public static Action<ControlBoxButtonPresenter> <>9__73_2;
            public static Action<ControlBoxButtonPresenter> <>9__73_3;
            public static Action<ControlBoxButtonPresenter> <>9__73_4;
            public static Action<ControlBoxButtonPresenter> <>9__73_5;

            internal void <.cctor>b__11_0(PanelControlBoxControl d)
            {
                d.OnUseOptimizedTemplateChanged();
            }

            internal void <.cctor>b__11_1(PanelControlBoxControl d, DockItemState oldValue, DockItemState newValue)
            {
                d.OnDockItemStateChanged(oldValue, newValue);
            }

            internal void <ClearControlBoxBindings>b__71_0(ControlBoxButtonPresenter x)
            {
                x.ClearValue(UIElement.VisibilityProperty);
            }

            internal void <ClearControlBoxBindings>b__71_1(ControlBoxButtonPresenter x)
            {
                x.ClearValue(UIElement.VisibilityProperty);
            }

            internal void <ClearControlBoxBindings>b__71_2(ControlBoxButtonPresenter x)
            {
                x.ClearValue(UIElement.VisibilityProperty);
            }

            internal void <ClearControlBoxBindings>b__71_3(ControlBoxButtonPresenter x)
            {
                x.ClearValue(UIElement.VisibilityProperty);
            }

            internal void <ClearControlBoxBindings>b__71_4(ControlBoxButtonPresenter x)
            {
                x.ClearValue(UIElement.VisibilityProperty);
            }

            internal void <ClearControlBoxBindings>b__71_5(ControlBoxButtonPresenter x)
            {
                x.ClearValue(UIElement.VisibilityProperty);
            }

            internal void <ClearControlBoxBindings>b__71_6(ControlBoxButtonPresenter x)
            {
                x.ClearValue(UIElement.VisibilityProperty);
            }

            internal void <OnApplyTemplate>b__70_0(ControlBoxButtonPresenter x)
            {
                x.AttachToolTip(BaseControlBoxControl.GetToolTip(DockingStringId.ControlButtonAutoHide));
            }

            internal void <OnApplyTemplate>b__70_1(ControlBoxButtonPresenter x)
            {
                x.AttachToolTip(BaseControlBoxControl.GetToolTip(DockingStringId.ControlButtonMaximize));
            }

            internal void <OnApplyTemplate>b__70_2(ControlBoxButtonPresenter x)
            {
                x.AttachToolTip(BaseControlBoxControl.GetToolTip(DockingStringId.ControlButtonMinimize));
            }

            internal void <OnApplyTemplate>b__70_3(ControlBoxButtonPresenter x)
            {
                x.AttachToolTip(BaseControlBoxControl.GetToolTip(DockingStringId.ControlButtonRestore));
            }

            internal void <OnApplyTemplate>b__70_4(ControlBoxButtonPresenter x)
            {
                x.AttachToolTip(BaseControlBoxControl.GetToolTip(DockingStringId.ControlButtonHide));
            }

            internal void <OnApplyTemplate>b__70_5(ControlBoxButtonPresenter x)
            {
                x.AttachToolTip(BaseControlBoxControl.GetToolTip(DockingStringId.ControlButtonExpand));
            }

            internal void <OnApplyTemplate>b__70_6(ControlBoxButtonPresenter x)
            {
                x.AttachToolTip(BaseControlBoxControl.GetToolTip(DockingStringId.ControlButtonCollapse));
            }

            internal void <OnDispose>b__73_0(ControlBoxButtonPresenter x)
            {
                x.Dispose();
            }

            internal void <OnDispose>b__73_1(ControlBoxButtonPresenter x)
            {
                x.Dispose();
            }

            internal void <OnDispose>b__73_2(ControlBoxButtonPresenter x)
            {
                x.Dispose();
            }

            internal void <OnDispose>b__73_3(ControlBoxButtonPresenter x)
            {
                x.Dispose();
            }

            internal void <OnDispose>b__73_4(ControlBoxButtonPresenter x)
            {
                x.Dispose();
            }

            internal void <OnDispose>b__73_5(ControlBoxButtonPresenter x)
            {
                x.Dispose();
            }
        }
    }
}

