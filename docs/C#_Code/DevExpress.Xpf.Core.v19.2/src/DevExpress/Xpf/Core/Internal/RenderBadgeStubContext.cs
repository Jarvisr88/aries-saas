namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class RenderBadgeStubContext : RenderControlBaseContext
    {
        public RenderBadgeStubContext(RenderBadgeStub factory) : base(factory)
        {
        }

        private FrameworkElement GetControl() => 
            null;

        protected internal override void UpdateControlFontSettings()
        {
        }

        protected internal override void UpdateControlForeground()
        {
        }

        protected internal override void UpdateOpacity()
        {
        }

        public override FrameworkElement Control
        {
            get => 
                this.GetControl();
            internal set
            {
            }
        }

        public override bool AttachToRoot =>
            false;
    }
}

