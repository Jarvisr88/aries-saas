namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ToggleSwitchButtonChrome : ButtonChrome
    {
        private readonly int duration = 200;
        private readonly string ThumbContainerElementName = "PART_ThumbContainer";
        private readonly string RootPanelElementName = "PART_RootPanel";
        private bool? lastValue = false;
        private Action postponedMoveThumb;

        public ToggleSwitchButtonChrome()
        {
            base.DataContextChanged += new DependencyPropertyChangedEventHandler(this.ToggleSwitchButtonChrome_DataContextChanged);
            base.Unloaded += new RoutedEventHandler(this.ToggleSwitchButtonChromeUnloaded);
        }

        private double GetOffsetByValue(bool? isChecked) => 
            (isChecked != null) ? (!isChecked.Value ? 0.0 : 1.0) : 0.5;

        public void MoveSwitchThumb(bool? isChecked, bool shouldAnimate)
        {
            if (this.ThumbContainer == null)
            {
                this.postponedMoveThumb = () => this.MoveSwitchThumb(isChecked, shouldAnimate);
            }
            else
            {
                double offsetByValue = this.GetOffsetByValue(isChecked);
                if (!shouldAnimate)
                {
                    if ((this.Animator != null) && this.Animator.InProcess)
                    {
                        this.Animator.Stop();
                    }
                    this.ThumbContainer.Offset = offsetByValue;
                    this.ThumbContainer.UpdateLayout(FREInvalidateOptions.UpdateVisual);
                }
                else
                {
                    double from = this.GetOffsetByValue(this.lastValue);
                    if ((this.Animator != null) && this.Animator.InProcess)
                    {
                        from = this.Animator.Value;
                        this.Animator.Stop();
                    }
                    this.ThumbContainer.Offset = from;
                    this.Animator = new ToggleSwitchAnimator(from, offsetByValue, this.duration, new Action(this.OnAnimatorTick));
                    this.Animator.Start();
                }
                this.lastValue = isChecked;
            }
        }

        private void OnAnimatorTick()
        {
            if (this.ThumbContainer != null)
            {
                this.ThumbContainer.Offset = this.Animator.Value;
                this.ThumbContainer.UpdateLayout(FREInvalidateOptions.UpdateVisual);
            }
        }

        protected override void OnApplyRenderTemplate()
        {
            base.OnApplyRenderTemplate();
            this.ThumbContainer = (RenderToggleSwitchThumbContainerContext) base.Namescope.GetElement(this.ThumbContainerElementName);
            this.RootPanel = (RenderPanelContext) base.Namescope.GetElement(this.RootPanelElementName);
            this.UpdateLayoutProvider();
            Action<Action> action = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Action<Action> local1 = <>c.<>9__25_0;
                action = <>c.<>9__25_0 = x => x();
            }
            this.postponedMoveThumb.Do<Action>(action);
        }

        private void ToggleSwitchButtonChrome_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void ToggleSwitchButtonChromeUnloaded(object sender, RoutedEventArgs e)
        {
            if (this.Animator != null)
            {
                this.Animator.Stop();
            }
        }

        internal void UpdateLayoutProvider()
        {
            if (this.RootPanel != null)
            {
                this.RootPanel.LayoutProvider = new ToggleSwitchLayoutProvider(this.OwnerToggleSwitch);
            }
        }

        private FrameworkRenderElementContext Root =>
            this.Root;

        private RenderToggleSwitchThumbContainerContext ThumbContainer { get; set; }

        private RenderPanelContext RootPanel { get; set; }

        private ToggleSwitch OwnerToggleSwitch =>
            (ToggleSwitch) base.Owner;

        internal ToggleSwitchAnimator Animator { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ToggleSwitchButtonChrome.<>c <>9 = new ToggleSwitchButtonChrome.<>c();
            public static Action<Action> <>9__25_0;

            internal void <OnApplyRenderTemplate>b__25_0(Action x)
            {
                x();
            }
        }
    }
}

