namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Controls;

    public class RenderRealTextBoxContext : RenderControlBaseContext
    {
        private string text;
        private bool shouldUpdateTextBox;

        public RenderRealTextBoxContext(RenderRealTextBox factory);
        protected virtual void UpdateTextBox();

        private TextBox EditBox { get; }

        public string Text { get; set; }
    }
}

