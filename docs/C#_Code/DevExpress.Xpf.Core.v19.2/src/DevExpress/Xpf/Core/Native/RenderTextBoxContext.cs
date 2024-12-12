namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderTextBoxContext : FrameworkRenderElementContext
    {
        private string text;

        public RenderTextBoxContext(RenderTextBox factory);
        private void InvalidateText();

        public string Text { get; set; }
    }
}

