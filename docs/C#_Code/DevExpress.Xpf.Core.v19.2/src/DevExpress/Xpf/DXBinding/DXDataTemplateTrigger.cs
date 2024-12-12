namespace DevExpress.Xpf.DXBinding
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Template")]
    public class DXDataTemplateTrigger : DXTriggerBase
    {
        private DataTemplate template;

        public DataTemplate Template
        {
            get => 
                this.template;
            set
            {
                base.WritePreamble();
                this.template = value;
            }
        }
    }
}

