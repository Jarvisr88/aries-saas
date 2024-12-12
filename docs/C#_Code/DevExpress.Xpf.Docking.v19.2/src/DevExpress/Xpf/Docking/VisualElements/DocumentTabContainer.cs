namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class DocumentTabContainer : LayoutTabControl
    {
        public static readonly DependencyProperty ActualBackgroundProperty;
        public static readonly DependencyProperty ActualBorderMarginProperty;
        public static readonly DependencyProperty ActualContentBorderThicknessProperty;
        public static readonly DependencyProperty ContentBorderThicknessProperty;
        public static readonly DependencyProperty BorderMarginProperty;
        public static readonly DependencyProperty HasTabColorProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty TabColorProperty;
        public static readonly DependencyProperty ThemeDependentBackgroundProperty;
        private static readonly DependencyPropertyKey ActualBorderMarginPropertyKey;
        private static readonly DependencyPropertyKey ActualContentBorderThicknessPropertyKey;
        private static readonly DependencyPropertyKey HasTabColorPropertyKey;

        static DocumentTabContainer()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DocumentTabContainer> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DocumentTabContainer>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.OverrideMetadata<bool>(LayoutTabControl.ShowTabForSinglePageProperty, true, null, null);
            registrator.Register<Brush>("ThemeDependentBackground", ref ThemeDependentBackgroundProperty, null, (dObj, e) => ((DocumentTabContainer) dObj).OnBackgroundChanged((Brush) e.NewValue), null);
            registrator.Register<Brush>("ActualBackground", ref ActualBackgroundProperty, null, null, (dObj, value) => ((DocumentTabContainer) dObj).CoerceActualBackground((Brush) value));
            registrator.Register<Color>("TabColor", ref TabColorProperty, Colors.Transparent, (dObj, e) => ((DocumentTabContainer) dObj).OnTabColorChanged((Color) e.OldValue, (Color) e.NewValue), null);
            registrator.RegisterReadonly<bool>("HasTabColor", ref HasTabColorPropertyKey, ref HasTabColorProperty, false, null, null);
            registrator.Register<bool>("IsActive", ref IsActiveProperty, false, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentTabContainer), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            Thickness defaultValue = new Thickness();
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DocumentTabContainer> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DocumentTabContainer>.New().OverrideMetadata<Brush>(Control.BackgroundProperty, (d, oldValue, newValue) => d.OnBackgroundChanged(newValue)).Register<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<DocumentTabContainer, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentTabContainer.get_BorderMargin)), parameters), out BorderMarginProperty, defaultValue, (d, oldValue, newValue) => d.OnBorderMarginChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentTabContainer), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            defaultValue = new Thickness();
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DocumentTabContainer> registrator2 = registrator1.Register<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<DocumentTabContainer, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentTabContainer.get_ContentBorderThickness)), expressionArray2), out ContentBorderThicknessProperty, defaultValue, (d, oldValue, newValue) => d.OnContentBorderThicknessChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentTabContainer), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            defaultValue = new Thickness();
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DocumentTabContainer> registrator3 = registrator2.RegisterReadOnly<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<DocumentTabContainer, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentTabContainer.get_ActualBorderMargin)), expressionArray3), out ActualBorderMarginPropertyKey, out ActualBorderMarginProperty, defaultValue, (Func<DocumentTabContainer, Thickness, Thickness>) ((d, value) => d.OnCoerceActualBorderMargin(value)), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DocumentTabContainer), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            defaultValue = new Thickness();
            frameworkOptions = null;
            registrator3.RegisterReadOnly<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<DocumentTabContainer, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DocumentTabContainer.get_ActualContentBorderThickness)), expressionArray4), out ActualContentBorderThicknessPropertyKey, out ActualContentBorderThicknessProperty, defaultValue, (Func<DocumentTabContainer, Thickness, Thickness>) ((d, value) => d.OnCoerceActualContentBorderThickness(value)), frameworkOptions);
        }

        protected virtual object CoerceActualBackground(Brush value) => 
            base.Background ?? this.ThemeDependentBackground;

        protected override psvSelectorItem CreateSelectorItem() => 
            new DocumentPaneItem();

        protected override IView GetView(DockLayoutManager container)
        {
            DocumentPane owner = null;
            DocumentPaneContentPresenter templatedParent = base.TemplatedParent as DocumentPaneContentPresenter;
            if (templatedParent != null)
            {
                owner = templatedParent.Owner;
            }
            return ((owner != null) ? container.GetView(owner.GetRootUIScope()) : null);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState();
        }

        protected virtual void OnBackgroundChanged(Brush newValue)
        {
            base.CoerceValue(ActualBackgroundProperty);
        }

        protected virtual void OnBorderMarginChanged(Thickness oldValue, Thickness newValue)
        {
            base.CoerceValue(ActualBorderMarginProperty);
        }

        protected override void OnCaptionLocationChanged(CaptionLocation captionLocation)
        {
            base.OnCaptionLocationChanged(captionLocation);
            base.CoerceValue(ActualContentBorderThicknessProperty);
            base.CoerceValue(ActualBorderMarginProperty);
            this.UpdateVisualState();
        }

        protected virtual Thickness OnCoerceActualBorderMargin(Thickness value) => 
            base.CaptionLocation.RotateThickness(this.BorderMargin);

        protected virtual Thickness OnCoerceActualContentBorderThickness(Thickness value)
        {
            Thickness thickness = new Thickness(0.0, this.ContentBorderThickness.Top, 0.0, 0.0);
            return base.CaptionLocation.RotateThickness(((base.ViewStyle == DockingViewStyle.Light) ? thickness : this.ContentBorderThickness));
        }

        protected virtual void OnContentBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            base.CoerceValue(ActualContentBorderThicknessProperty);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            this.UnsubscribeItem(base.SelectedContent);
        }

        protected override void OnHasItemsChanged(bool hasItems)
        {
            base.OnHasItemsChanged(hasItems);
            this.UpdateVisualState();
        }

        private void OnItemVisualChanged(object sender, EventArgs e)
        {
            this.UpdateVisualState();
        }

        protected override void OnSelectedContentChanged(BaseLayoutItem newValue, BaseLayoutItem oldValue)
        {
            base.OnSelectedContentChanged(newValue, oldValue);
            this.UnsubscribeItem(oldValue);
            this.SubscribeItem(newValue);
            this.UpdateVisualState();
        }

        protected virtual void OnTabColorChanged(Color oldValue, Color newValue)
        {
            base.SetValue(HasTabColorPropertyKey, newValue != Colors.Transparent);
        }

        protected override void OnViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
            base.OnViewStyleChanged(oldValue, newValue);
            base.CoerceValue(ActualContentBorderThicknessProperty);
        }

        private void SubscribeItem(BaseLayoutItem item)
        {
            if (item != null)
            {
                item.VisualChanged += new EventHandler(this.OnItemVisualChanged);
                item.Forward(this, TabColorProperty, "ActualTabBackgroundColor", BindingMode.OneWay);
                item.Forward(this, IsActiveProperty, "IsActive", BindingMode.OneWay);
            }
        }

        private void UnsubscribeItem(BaseLayoutItem item)
        {
            if (item != null)
            {
                item.VisualChanged -= new EventHandler(this.OnItemVisualChanged);
                base.ClearValue(TabColorProperty);
                base.ClearValue(IsActiveProperty);
            }
        }

        protected virtual void UpdateLocationState()
        {
            string stateName = "EmptyLocationState";
            VisualStateManager.GoToState(this, stateName, false);
            stateName = ((base.CaptionLocation == CaptionLocation.Default) ? CaptionLocation.Top : base.CaptionLocation).ToString();
            VisualStateManager.GoToState(this, stateName, false);
        }

        protected override void UpdateVisualState()
        {
            VisualStateManager.GoToState(this, "ActiveEmptyState", false);
            if (!base.HasItems)
            {
                VisualStateManager.GoToState(this, "Empty", false);
            }
            else if (base.SelectedContent != null)
            {
                if (base.SelectedContent.IsActive)
                {
                    VisualStateManager.GoToState(this, "Active", false);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Inactive", false);
                }
            }
            this.UpdateLocationState();
            base.UpdateVisualState();
        }

        public Brush ActualBackground
        {
            get => 
                (Brush) base.GetValue(ActualBackgroundProperty);
            set => 
                base.SetValue(ActualBackgroundProperty, value);
        }

        public Thickness ActualBorderMargin =>
            (Thickness) base.GetValue(ActualBorderMarginProperty);

        public Thickness ActualContentBorderThickness
        {
            get => 
                (Thickness) base.GetValue(ActualContentBorderThicknessProperty);
            private set => 
                base.SetValue(ActualContentBorderThicknessProperty, value);
        }

        public Thickness BorderMargin
        {
            get => 
                (Thickness) base.GetValue(BorderMarginProperty);
            set => 
                base.SetValue(BorderMarginProperty, value);
        }

        public Thickness ContentBorderThickness
        {
            get => 
                (Thickness) base.GetValue(ContentBorderThicknessProperty);
            set => 
                base.SetValue(ContentBorderThicknessProperty, value);
        }

        public bool HasTabColor =>
            (bool) base.GetValue(HasTabColorProperty);

        public bool IsActive
        {
            get => 
                (bool) base.GetValue(IsActiveProperty);
            set => 
                base.SetValue(IsActiveProperty, value);
        }

        public Color TabColor
        {
            get => 
                (Color) base.GetValue(TabColorProperty);
            set => 
                base.SetValue(TabColorProperty, value);
        }

        public Brush ThemeDependentBackground
        {
            get => 
                (Brush) base.GetValue(ThemeDependentBackgroundProperty);
            set => 
                base.SetValue(ThemeDependentBackgroundProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentTabContainer.<>c <>9 = new DocumentTabContainer.<>c();

            internal void <.cctor>b__13_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentTabContainer) dObj).OnBackgroundChanged((Brush) e.NewValue);
            }

            internal object <.cctor>b__13_1(DependencyObject dObj, object value) => 
                ((DocumentTabContainer) dObj).CoerceActualBackground((Brush) value);

            internal void <.cctor>b__13_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentTabContainer) dObj).OnTabColorChanged((Color) e.OldValue, (Color) e.NewValue);
            }

            internal void <.cctor>b__13_3(DocumentTabContainer d, Brush oldValue, Brush newValue)
            {
                d.OnBackgroundChanged(newValue);
            }

            internal void <.cctor>b__13_4(DocumentTabContainer d, Thickness oldValue, Thickness newValue)
            {
                d.OnBorderMarginChanged(oldValue, newValue);
            }

            internal void <.cctor>b__13_5(DocumentTabContainer d, Thickness oldValue, Thickness newValue)
            {
                d.OnContentBorderThicknessChanged(oldValue, newValue);
            }

            internal Thickness <.cctor>b__13_6(DocumentTabContainer d, Thickness value) => 
                d.OnCoerceActualBorderMargin(value);

            internal Thickness <.cctor>b__13_7(DocumentTabContainer d, Thickness value) => 
                d.OnCoerceActualContentBorderThickness(value);
        }
    }
}

