namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class GroupBoxContentControl : GroupContentControl
    {
        public static readonly DependencyProperty CaptionBackgroundProperty;

        static GroupBoxContentControl()
        {
            DependencyPropertyRegistrator<GroupBoxContentControl> registrator = new DependencyPropertyRegistrator<GroupBoxContentControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<Brush>("CaptionBackground", ref CaptionBackgroundProperty, null, null, null);
        }

        protected override void UpdateCaptionBackground()
        {
            base.UpdateCaptionBackground();
            AppearanceObject actualAppearanceObject = base.LayoutItem.ActualAppearanceObject;
            if (actualAppearanceObject != null)
            {
                this.CaptionBackground = actualAppearanceObject.Background;
            }
        }

        protected override void UpdateVisualState()
        {
            base.UpdateVisualState();
            if (base.LayoutGroup != null)
            {
                VisualStateManager.GoToState(this, base.LayoutGroup.IsExpanded ? "Expanded" : "Collapsed", false);
            }
        }

        protected override bool IsCaptionVisible =>
            (base.LayoutItem != null) && base.LayoutItem.IsCaptionVisible;

        public Brush CaptionBackground
        {
            get => 
                (Brush) base.GetValue(CaptionBackgroundProperty);
            set => 
                base.SetValue(CaptionBackgroundProperty, value);
        }
    }
}

