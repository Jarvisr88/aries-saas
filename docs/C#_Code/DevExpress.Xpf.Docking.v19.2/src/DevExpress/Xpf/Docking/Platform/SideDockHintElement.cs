namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_Dock", Type=typeof(DockHintButton)), TemplatePart(Name="PART_Hide", Type=typeof(DockHintButton))]
    public class SideDockHintElement : DockHintElement
    {
        public static readonly DependencyProperty LeftTemplateProperty;
        public static readonly DependencyProperty RightTemplateProperty;
        public static readonly DependencyProperty TopTemplateProperty;
        public static readonly DependencyProperty BottomTemplateProperty;

        static SideDockHintElement()
        {
            DependencyPropertyRegistrator<SideDockHintElement> registrator = new DependencyPropertyRegistrator<SideDockHintElement>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<ControlTemplate>("LeftTemplate", ref LeftTemplateProperty, null, (dObj, e) => ((SideDockHintElement) dObj).OnHintTemplateChanged(), null);
            registrator.Register<ControlTemplate>("RightTemplate", ref RightTemplateProperty, null, (dObj, e) => ((SideDockHintElement) dObj).OnHintTemplateChanged(), null);
            registrator.Register<ControlTemplate>("TopTemplate", ref TopTemplateProperty, null, (dObj, e) => ((SideDockHintElement) dObj).OnHintTemplateChanged(), null);
            registrator.Register<ControlTemplate>("BottomTemplate", ref BottomTemplateProperty, null, (dObj, e) => ((SideDockHintElement) dObj).OnHintTemplateChanged(), null);
        }

        public SideDockHintElement(DockVisualizerElement type) : base(type)
        {
            this.IsHorizontal = (base.Type == DockVisualizerElement.Left) || (base.Type == DockVisualizerElement.Right);
        }

        protected override Rect CalcBounds(DockingHintAdornerBase adorner) => 
            adorner.SurfaceRect;

        protected override bool CalcVisibleState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            return (dockHintsConfiguration.GetGuideIsVisible(base.Type) && dockHintsConfiguration.ShowSideDockHints);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.DockButton = base.GetTemplateChild("PART_Dock") as DockHintButton;
            this.HideButton = base.GetTemplateChild("PART_Hide") as DockHintButton;
        }

        protected virtual void OnHintTemplateChanged()
        {
            if ((this.LeftTemplate != null) && ((this.RightTemplate != null) && ((this.TopTemplate != null) && (this.BottomTemplate != null))))
            {
                this.SelectTemplate(base.Type);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.SelectTemplate(base.Type);
        }

        protected virtual void SelectTemplate(DockVisualizerElement type)
        {
            if (!base.IsDisposing)
            {
                switch (type)
                {
                    case DockVisualizerElement.Left:
                        base.Template = this.LeftTemplate;
                        return;

                    case DockVisualizerElement.Right:
                        base.Template = this.RightTemplate;
                        return;

                    case DockVisualizerElement.Top:
                        base.Template = this.TopTemplate;
                        return;

                    case DockVisualizerElement.Bottom:
                        base.Template = this.BottomTemplate;
                        return;
                }
                base.Template = null;
            }
        }

        public void UpdateAvailableState(DockHintsConfiguration configuration)
        {
            bool sideIsEnabled = configuration.GetSideIsEnabled(base.Type);
            bool autoHideIsEnabled = configuration.GetAutoHideIsEnabled(base.Type);
            if (this.DockButton != null)
            {
                this.DockButton.IsAvailable = sideIsEnabled;
            }
            if (this.HideButton != null)
            {
                this.HideButton.IsAvailable = autoHideIsEnabled;
                this.HideButton.Visibility = autoHideIsEnabled ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public override void UpdateAvailableState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            this.UpdateAvailableState(dockHintsConfiguration);
        }

        public override void UpdateAvailableState(bool dock, bool hide, bool fill)
        {
            if (this.DockButton != null)
            {
                this.DockButton.IsAvailable = dock;
            }
            if (this.HideButton != null)
            {
                this.HideButton.IsAvailable = hide;
                this.HideButton.Visibility = hide ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdateEnabledState(DockHintsConfiguration configuration)
        {
            bool sideIsVisible = configuration.GetSideIsVisible(base.Type);
            bool autoHideIsVisible = configuration.GetAutoHideIsVisible(base.Type);
            if (this.DockButton != null)
            {
                this.DockButton.IsEnabled = sideIsVisible;
            }
            if (this.HideButton != null)
            {
                this.HideButton.IsEnabled = autoHideIsVisible;
            }
        }

        public override void UpdateEnabledState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            this.UpdateEnabledState(dockHintsConfiguration);
        }

        public override void UpdateHotTrack(DockHintButton hotButton)
        {
            base.UpdateHotTrack(this.DockButton, hotButton);
            base.UpdateHotTrack(this.HideButton, hotButton);
        }

        public override void UpdateState(DockingHintAdornerBase adorner)
        {
            DockHintsConfiguration dockHintsConfiguration = adorner.DockHintsConfiguration;
            this.UpdateAvailableState(dockHintsConfiguration);
            this.UpdateEnabledState(dockHintsConfiguration);
        }

        public ControlTemplate LeftTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(LeftTemplateProperty);
            set => 
                base.SetValue(LeftTemplateProperty, value);
        }

        public ControlTemplate RightTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(RightTemplateProperty);
            set => 
                base.SetValue(RightTemplateProperty, value);
        }

        public ControlTemplate TopTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(TopTemplateProperty);
            set => 
                base.SetValue(TopTemplateProperty, value);
        }

        public ControlTemplate BottomTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(BottomTemplateProperty);
            set => 
                base.SetValue(BottomTemplateProperty, value);
        }

        protected DockHintButton DockButton { get; private set; }

        protected DockHintButton HideButton { get; private set; }

        public bool IsHorizontal { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SideDockHintElement.<>c <>9 = new SideDockHintElement.<>c();

            internal void <.cctor>b__4_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((SideDockHintElement) dObj).OnHintTemplateChanged();
            }

            internal void <.cctor>b__4_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((SideDockHintElement) dObj).OnHintTemplateChanged();
            }

            internal void <.cctor>b__4_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((SideDockHintElement) dObj).OnHintTemplateChanged();
            }

            internal void <.cctor>b__4_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((SideDockHintElement) dObj).OnHintTemplateChanged();
            }
        }
    }
}

