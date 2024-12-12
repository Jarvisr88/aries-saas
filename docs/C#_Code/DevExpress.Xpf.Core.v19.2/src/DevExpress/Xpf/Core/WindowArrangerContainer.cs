namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class WindowArrangerContainer : WindowContainer
    {
        private SplashScreenLocation arrangeLocation;

        public WindowArrangerContainer(DependencyObject parentObject, SplashScreenLocation arrangeLocation) : base(parentObject)
        {
            this.arrangeLocation = arrangeLocation;
        }

        protected override void CompleteInitializationOverride()
        {
            this.ControlStartupPosition = this.GetControlRect();
            this.WindowStartupPosition = this.GetWindowRect();
        }

        public override WindowRelationInfo CreateOwnerContainer() => 
            DXSplashScreen.UseLegacyLocationLogic ? ((WindowRelationInfo) new WindowArranger(this, this.arrangeLocation, this.ArrangeMode)) : ((WindowRelationInfo) new CompositionTargetBasedArranger(this, this.arrangeLocation, this.ArrangeMode));

        public Rect GetControlRect() => 
            GetRealRect(base.FrameworkObject);

        private static Rect GetRealRect(FrameworkElement element) => 
            ((element == null) || (!element.IsLoaded || (PresentationSource.FromDependencyObject(element) == null))) ? Rect.Empty : LayoutHelper.GetScreenRect(element);

        public Rect GetWindowRect() => 
            (base.Form == null) ? GetRealRect(base.Window) : new Rect((double) base.Form.Left, (double) base.Form.Top, (double) base.Form.Width, (double) base.Form.Height);

        public Rect ControlStartupPosition { get; private set; }

        public Rect WindowStartupPosition { get; private set; }

        public SplashScreenArrangeMode ArrangeMode { get; set; }
    }
}

