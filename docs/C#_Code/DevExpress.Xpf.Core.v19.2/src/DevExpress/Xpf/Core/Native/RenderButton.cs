namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Controls;

    public class RenderButton : RenderContentControl
    {
        private System.Windows.Controls.ClickMode clickMode;
        private DevExpress.Xpf.Editors.ButtonKind buttonKind;

        public RenderButton();
        protected override FrameworkRenderElementContext CreateContextInstance();

        public System.Windows.Controls.ClickMode ClickMode { get; set; }

        public DevExpress.Xpf.Editors.ButtonKind ButtonKind { get; set; }
    }
}

