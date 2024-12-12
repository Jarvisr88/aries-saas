namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RowViewInfo
    {
        public RowViewInfo(DataTemplate template, object content)
        {
            Guard.ArgumentNotNull(template, "template");
            this.Template = template;
            this.Content = content;
        }

        public DataTemplate Template { get; private set; }

        public object Content { get; private set; }
    }
}

