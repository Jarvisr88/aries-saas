namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Templates")]
    public class TypeTemplateSelector : DataTemplateSelector
    {
        public TypeTemplateSelector();
        public override DataTemplate SelectTemplate(object item, DependencyObject container);

        public bool FindDescendants { get; set; }

        public TemplatesDictionary Templates { get; set; }
    }
}

