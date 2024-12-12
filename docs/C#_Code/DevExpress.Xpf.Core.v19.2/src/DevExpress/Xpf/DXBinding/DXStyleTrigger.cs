namespace DevExpress.Xpf.DXBinding
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Style")]
    public class DXStyleTrigger : DXTriggerBase
    {
        private System.Windows.Style style;

        public System.Windows.Style Style
        {
            get => 
                this.style;
            set
            {
                base.WritePreamble();
                this.style = value;
            }
        }
    }
}

