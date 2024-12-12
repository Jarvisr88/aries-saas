namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Markup;

    [XamlSetTypeConverter("ReceiveClrTypeConverter")]
    public class ClrSetter : Setter
    {
        public static void ReceiveClrTypeConverter(object targetObject, XamlSetTypeConverterEventArgs eventArgs);

        [TypeConverter(typeof(ClrSetterPropertyTypeConverter))]
        public DependencyProperty Property { get; set; }
    }
}

