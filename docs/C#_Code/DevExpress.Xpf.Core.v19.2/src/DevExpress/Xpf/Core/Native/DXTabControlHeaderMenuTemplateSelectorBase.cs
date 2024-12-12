namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DXTabControlHeaderMenuTemplateSelectorBase : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container);

        public DataTemplate ItemTemplate { get; set; }

        [Obsolete, EditorBrowsable(EditorBrowsableState.Never)]
        public DataTemplate OldTemplate { get; set; }

        public DataTemplate SeparatorTemplate { get; set; }
    }
}

