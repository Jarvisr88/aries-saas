namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [TemplatePart(Name="PART_Left", Type=typeof(DockHintButton)), TemplatePart(Name="PART_Right", Type=typeof(DockHintButton)), TemplatePart(Name="PART_Top", Type=typeof(DockHintButton)), TemplatePart(Name="PART_Bottom", Type=typeof(DockHintButton)), TemplatePart(Name="PART_Fill", Type=typeof(DockHintButton))]
    public class CenterDockHintElement : DockHintElement
    {
        public static readonly DependencyProperty DockHintsVisibilityProperty;
        public static readonly DependencyProperty TabHintsVisibilityProperty;

        static CenterDockHintElement()
        {
            DependencyPropertyRegistrator<CenterDockHintElement> registrator = new DependencyPropertyRegistrator<CenterDockHintElement>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<Visibility>("DockHintsVisibility", ref DockHintsVisibilityProperty, Visibility.Visible, (dObj, e) => ((CenterDockHintElement) dObj).OnDockHintsVisibilityChanged((Visibility) e.OldValue, (Visibility) e.NewValue), null);
            registrator.Register<Visibility>("TabHintsVisibility", ref TabHintsVisibilityProperty, Visibility.Visible, (dObj, e) => ((CenterDockHintElement) dObj).OnTabHintsVisibilityChanged((Visibility) e.OldValue, (Visibility) e.NewValue), null);
        }

        public CenterDockHintElement() : base(DockVisualizerElement.Center)
        {
        }

        protected override Rect CalcBounds(DockingHintAdornerBase adorner) => 
            adorner.TargetRect;

        protected override bool CalcVisibleState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            return (dockHintsConfiguration.GetGuideIsVisible(base.Type) && dockHintsConfiguration.ShowSelfDockHint);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.LeftButton = base.GetTemplateChild("PART_Left") as DockHintButton;
            this.RightButton = base.GetTemplateChild("PART_Right") as DockHintButton;
            this.TopButton = base.GetTemplateChild("PART_Top") as DockHintButton;
            this.BottomButton = base.GetTemplateChild("PART_Bottom") as DockHintButton;
            this.FillButton = base.GetTemplateChild("PART_Fill") as DockHintButton;
            this.TabLeftButton = base.GetTemplateChild("PART_TabLeft") as DockHintButton;
            this.TabRightButton = base.GetTemplateChild("PART_TabRight") as DockHintButton;
            this.TabTopButton = base.GetTemplateChild("PART_TabTop") as DockHintButton;
            this.TabBottomButton = base.GetTemplateChild("PART_TabBottom") as DockHintButton;
            BindingHelper.SetBinding(this.BottomButton, UIElement.VisibilityProperty, this, "DockHintsVisibility");
            BindingHelper.SetBinding(this.LeftButton, UIElement.VisibilityProperty, this, "DockHintsVisibility");
            BindingHelper.SetBinding(this.RightButton, UIElement.VisibilityProperty, this, "DockHintsVisibility");
            BindingHelper.SetBinding(this.TopButton, UIElement.VisibilityProperty, this, "DockHintsVisibility");
            BindingHelper.SetBinding(this.TabBottomButton, UIElement.VisibilityProperty, this, "TabHintsVisibility");
            BindingHelper.SetBinding(this.TabLeftButton, UIElement.VisibilityProperty, this, "TabHintsVisibility");
            BindingHelper.SetBinding(this.TabRightButton, UIElement.VisibilityProperty, this, "TabHintsVisibility");
            BindingHelper.SetBinding(this.TabTopButton, UIElement.VisibilityProperty, this, "TabHintsVisibility");
        }

        protected virtual void OnDockHintsVisibilityChanged(Visibility oldValue, Visibility newValue)
        {
        }

        protected virtual void OnTabHintsVisibilityChanged(Visibility oldValue, Visibility newValue)
        {
        }

        public void UpdateAvailableState(DockHintsConfiguration configuration)
        {
            if (this.FillButton != null)
            {
                this.FillButton.IsAvailable = configuration.GetIsEnabled(DockHint.Center);
            }
            if (this.LeftButton != null)
            {
                this.LeftButton.IsAvailable = configuration.GetIsEnabled(DockHint.CenterLeft);
            }
            if (this.RightButton != null)
            {
                this.RightButton.IsAvailable = configuration.GetIsEnabled(DockHint.CenterRight);
            }
            if (this.TopButton != null)
            {
                this.TopButton.IsAvailable = configuration.GetIsEnabled(DockHint.CenterTop);
            }
            if (this.BottomButton != null)
            {
                this.BottomButton.IsAvailable = configuration.GetIsEnabled(DockHint.CenterBottom);
            }
            if (this.TabLeftButton != null)
            {
                this.TabLeftButton.IsAvailable = configuration.GetIsEnabled(DockHint.TabLeft);
            }
            if (this.TabRightButton != null)
            {
                this.TabRightButton.IsAvailable = configuration.GetIsEnabled(DockHint.TabRight);
            }
            if (this.TabTopButton != null)
            {
                this.TabTopButton.IsAvailable = configuration.GetIsEnabled(DockHint.TabTop);
            }
            if (this.TabBottomButton != null)
            {
                this.TabBottomButton.IsAvailable = configuration.GetIsEnabled(DockHint.TabBottom);
            }
        }

        public override void UpdateAvailableState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            this.UpdateAvailableState(dockHintsConfiguration);
        }

        public void UpdateEnabledState(DockHintsConfiguration configuration)
        {
            this.DockHintsVisibility = VisibilityHelper.Convert((configuration.DisplayMode != DockGuidDisplayMode.TabOnly) && (configuration.DisplayMode != DockGuidDisplayMode.FillOnly), Visibility.Collapsed);
            this.TabHintsVisibility = VisibilityHelper.Convert(configuration.DisplayMode != DockGuidDisplayMode.DockOnly, Visibility.Collapsed);
            if (this.FillButton != null)
            {
                this.FillButton.IsEnabled = configuration.GetIsVisible(DockHint.Center);
            }
            if (this.LeftButton != null)
            {
                this.LeftButton.IsEnabled = configuration.GetIsVisible(DockHint.CenterLeft);
            }
            if (this.RightButton != null)
            {
                this.RightButton.IsEnabled = configuration.GetIsVisible(DockHint.CenterRight);
            }
            if (this.TopButton != null)
            {
                this.TopButton.IsEnabled = configuration.GetIsVisible(DockHint.CenterTop);
            }
            if (this.BottomButton != null)
            {
                this.BottomButton.IsEnabled = configuration.GetIsVisible(DockHint.CenterBottom);
            }
            if (this.TabLeftButton != null)
            {
                this.TabLeftButton.IsEnabled = configuration.GetIsVisible(DockHint.TabLeft);
            }
            if (this.TabRightButton != null)
            {
                this.TabRightButton.IsEnabled = configuration.GetIsVisible(DockHint.TabRight);
            }
            if (this.TabTopButton != null)
            {
                this.TabTopButton.IsEnabled = configuration.GetIsVisible(DockHint.TabTop);
            }
            if (this.TabBottomButton != null)
            {
                this.TabBottomButton.IsEnabled = configuration.GetIsVisible(DockHint.TabBottom);
            }
        }

        public override void UpdateEnabledState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            this.UpdateEnabledState(dockHintsConfiguration);
        }

        public override void UpdateHotTrack(DockHintButton hotButton)
        {
            base.UpdateHotTrack(this.LeftButton, hotButton);
            base.UpdateHotTrack(this.RightButton, hotButton);
            base.UpdateHotTrack(this.TopButton, hotButton);
            base.UpdateHotTrack(this.BottomButton, hotButton);
            base.UpdateHotTrack(this.FillButton, hotButton);
            base.UpdateHotTrack(this.TabLeftButton, hotButton);
            base.UpdateHotTrack(this.TabRightButton, hotButton);
            base.UpdateHotTrack(this.TabTopButton, hotButton);
            base.UpdateHotTrack(this.TabBottomButton, hotButton);
        }

        private void UpdateOpactity(DockHintsConfiguration configuration)
        {
            bool flag = (configuration.DisplayMode == DockGuidDisplayMode.DockAndFill) || (configuration.DisplayMode == DockGuidDisplayMode.FillOnly);
            if (this.TabLeftButton != null)
            {
                this.TabLeftButton.Opacity = flag ? ((double) 0) : ((double) 1);
            }
            if (this.TabRightButton != null)
            {
                this.TabRightButton.Opacity = flag ? ((double) 0) : ((double) 1);
            }
            if (this.TabTopButton != null)
            {
                this.TabTopButton.Opacity = flag ? ((double) 0) : ((double) 1);
            }
            if (this.TabBottomButton != null)
            {
                this.TabBottomButton.Opacity = flag ? ((double) 0) : ((double) 1);
            }
        }

        public void UpdateState(DockHintsConfiguration configuration)
        {
            this.UpdateAvailableState(configuration);
            this.UpdateEnabledState(configuration);
            this.UpdateOpactity(configuration);
        }

        public override void UpdateState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            this.UpdateState(dockHintsConfiguration);
        }

        public Visibility DockHintsVisibility
        {
            get => 
                (Visibility) base.GetValue(DockHintsVisibilityProperty);
            set => 
                base.SetValue(DockHintsVisibilityProperty, value);
        }

        public Visibility TabHintsVisibility
        {
            get => 
                (Visibility) base.GetValue(TabHintsVisibilityProperty);
            set => 
                base.SetValue(TabHintsVisibilityProperty, value);
        }

        protected DockHintButton LeftButton { get; private set; }

        protected DockHintButton RightButton { get; private set; }

        protected DockHintButton TopButton { get; private set; }

        protected DockHintButton BottomButton { get; private set; }

        protected DockHintButton FillButton { get; private set; }

        protected DockHintButton TabBottomButton { get; private set; }

        protected DockHintButton TabLeftButton { get; private set; }

        protected DockHintButton TabRightButton { get; private set; }

        protected DockHintButton TabTopButton { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CenterDockHintElement.<>c <>9 = new CenterDockHintElement.<>c();

            internal void <.cctor>b__2_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((CenterDockHintElement) dObj).OnDockHintsVisibilityChanged((Visibility) e.OldValue, (Visibility) e.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((CenterDockHintElement) dObj).OnTabHintsVisibilityChanged((Visibility) e.OldValue, (Visibility) e.NewValue);
            }
        }
    }
}

