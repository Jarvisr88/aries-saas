namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Styles")]
    public class TypeStyleSelector : StyleSelector
    {
        public TypeStyleSelector();
        public override Style SelectStyle(object item, DependencyObject container);

        public StylesDictionary Styles { get; set; }
    }
}

