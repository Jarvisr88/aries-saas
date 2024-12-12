namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class TabCaptionControl : CaptionControl
    {
        public static readonly DependencyProperty HasCaptionImageProperty;
        private static readonly DependencyPropertyKey HasCaptionImagePropertyKey;

        static TabCaptionControl()
        {
            DependencyPropertyRegistrator<TabCaptionControl> registrator = new DependencyPropertyRegistrator<TabCaptionControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.RegisterReadonly<bool>("HasCaptionImage", ref HasCaptionImagePropertyKey, ref HasCaptionImageProperty, false, null, null);
        }

        protected override DependencyProperty GetCaptionTextProperty() => 
            BaseLayoutItem.ActualTabCaptionProperty;

        [Obsolete("No longer in use")]
        public bool HasCaptionImage =>
            (bool) base.GetValue(HasCaptionImageProperty);

        public DockLayoutManager Manager =>
            DockLayoutManager.GetDockLayoutManager(this);

        protected override bool RecognizeAccessKey =>
            false;
    }
}

