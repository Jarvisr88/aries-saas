namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class AutoHideTrayHeadersPanel : BaseHeadersPanel
    {
        public static readonly DependencyProperty BottomMarginProperty;
        public static readonly DependencyProperty LeftMarginProperty;
        public static readonly DependencyProperty RightMarginProperty;
        public static readonly DependencyProperty TopMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty DockTypeProperty;
        public static readonly DependencyProperty ViewStyleProperty;

        static AutoHideTrayHeadersPanel()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideTrayHeadersPanel> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHideTrayHeadersPanel>();
            registrator.Register<Thickness>("LeftMargin", ref LeftMarginProperty, new Thickness(0.0), (dObj, e) => ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged(), null);
            registrator.Register<Thickness>("TopMargin", ref TopMarginProperty, new Thickness(0.0), (dObj, e) => ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged(), null);
            registrator.Register<Thickness>("RightMargin", ref RightMarginProperty, new Thickness(0.0), (dObj, e) => ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged(), null);
            registrator.Register<Thickness>("BottomMargin", ref BottomMarginProperty, new Thickness(0.0), (dObj, e) => ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged(), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHideTrayHeadersPanel), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHideTrayHeadersPanel> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHideTrayHeadersPanel>.New().Register<Dock>(System.Linq.Expressions.Expression.Lambda<Func<AutoHideTrayHeadersPanel, Dock>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AutoHideTrayHeadersPanel.get_DockType)), parameters), out DockTypeProperty, Dock.Left, (d, oldValue, newValue) => d.OnDockTypeChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHideTrayHeadersPanel), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<AutoHideTrayHeadersPanel, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AutoHideTrayHeadersPanel.get_ViewStyle)), expressionArray2), out ViewStyleProperty, DockingViewStyle.Default, (d, oldValue, newValue) => d.OnViewStyleChanged(oldValue, newValue), frameworkOptions);
        }

        private void EnsureLayout()
        {
            base.Orientation = this.DockType.ToOrthogonalOrientation();
            this.UpdateActualMargin();
        }

        protected override Thickness GetClipMargin()
        {
            Thickness clipMargin = base.GetClipMargin();
            if (!base.IsHorizontal)
            {
                clipMargin = new Thickness(clipMargin.Top, clipMargin.Right, clipMargin.Bottom, clipMargin.Left);
            }
            return clipMargin;
        }

        protected override Geometry GetLayoutClip(Size layoutSlotSize) => 
            base.ClipToBounds ? base.GetLayoutClip(layoutSlotSize) : null;

        protected virtual void OnDockTypeChanged(Dock oldValue, Dock newValue)
        {
            this.EnsureLayout();
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            base.OnLayoutItemChanged(oldValue, newValue);
            AutoHideGroup source = newValue as AutoHideGroup;
            if (source != null)
            {
                BindingHelper.SetBinding(this, DockTypeProperty, source, AutoHideGroup.DockTypeProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, ViewStyleProperty, source, BaseLayoutItem.DockingViewStyleProperty, BindingMode.OneWay);
            }
            else
            {
                base.ClearValue(DockTypeProperty);
                base.ClearValue(ViewStyleProperty);
            }
            this.EnsureLayout();
        }

        protected virtual void OnSideMarginChanged()
        {
            this.UpdateActualMargin();
        }

        protected virtual void OnViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
            this.UpdateActualMargin();
        }

        protected internal void UpdateActualMargin()
        {
            this.HeadersGroup = LayoutItemsHelper.GetTemplateParent<AutoHideTrayHeadersGroup>(this);
            if ((this.HeadersGroup != null) && (this.HeadersGroup.Tray != null))
            {
                this.UpdateActualMargin(this.HeadersGroup.Tray);
            }
        }

        protected void UpdateActualMargin(AutoHideTray tray)
        {
            if (this.ViewStyle == DockingViewStyle.Light)
            {
                base.ClearValue(FrameworkElement.MarginProperty);
            }
            else
            {
                if (AutoHideTray.GetIsLeft(tray))
                {
                    base.Margin = this.LeftMargin;
                }
                if (AutoHideTray.GetIsTop(tray))
                {
                    base.Margin = this.TopMargin;
                }
                if (AutoHideTray.GetIsRight(tray))
                {
                    base.Margin = this.RightMargin;
                }
                if (AutoHideTray.GetIsBottom(tray))
                {
                    base.Margin = this.BottomMargin;
                }
            }
        }

        public Thickness BottomMargin
        {
            get => 
                (Thickness) base.GetValue(BottomMarginProperty);
            set => 
                base.SetValue(BottomMarginProperty, value);
        }

        public Dock DockType =>
            (Dock) base.GetValue(DockTypeProperty);

        public AutoHideTrayHeadersGroup HeadersGroup { get; private set; }

        public Thickness LeftMargin
        {
            get => 
                (Thickness) base.GetValue(LeftMarginProperty);
            set => 
                base.SetValue(LeftMarginProperty, value);
        }

        public Thickness RightMargin
        {
            get => 
                (Thickness) base.GetValue(RightMarginProperty);
            set => 
                base.SetValue(RightMarginProperty, value);
        }

        public Thickness TopMargin
        {
            get => 
                (Thickness) base.GetValue(TopMarginProperty);
            set => 
                base.SetValue(TopMarginProperty, value);
        }

        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        protected override bool StretchItems =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHideTrayHeadersPanel.<>c <>9 = new AutoHideTrayHeadersPanel.<>c();

            internal void <.cctor>b__6_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged();
            }

            internal void <.cctor>b__6_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged();
            }

            internal void <.cctor>b__6_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged();
            }

            internal void <.cctor>b__6_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideTrayHeadersPanel) dObj).OnSideMarginChanged();
            }

            internal void <.cctor>b__6_4(AutoHideTrayHeadersPanel d, Dock oldValue, Dock newValue)
            {
                d.OnDockTypeChanged(oldValue, newValue);
            }

            internal void <.cctor>b__6_5(AutoHideTrayHeadersPanel d, DockingViewStyle oldValue, DockingViewStyle newValue)
            {
                d.OnViewStyleChanged(oldValue, newValue);
            }
        }
    }
}

