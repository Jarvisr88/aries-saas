namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class RenderElementStubContext : RenderControlBaseContext
    {
        public RenderElementStubContext(RenderElementStub factory);
        private FrameworkElement GetControl();
        protected internal override void UpdateControlFontSettings();
        protected internal override void UpdateControlForeground();
        protected internal override void UpdateOpacity();

        public override FrameworkElement Control { get; internal set; }

        public override bool AttachToRoot { get; }
    }
}

