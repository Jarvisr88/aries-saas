namespace DevExpress.Utils.Design
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class BinaryTypeConverterExpandable : UniversalTypeConverterEx
    {
        public BinaryTypeConverterExpandable()
        {
            DXAssemblyResolverEx.Init();
        }

        [CompilerGenerated, DebuggerHidden]
        private object <>n__0(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            base.ConvertTo(context, culture, value, destinationType);

        [CompilerGenerated, DebuggerHidden]
        private object <>n__1(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            base.ConvertFrom(context, culture, value);

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            SafeBinaryFormatter.CanConvertTo(context, sourceType, new Func<ITypeDescriptorContext, Type, bool>(this.CanConvertFrom), true);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            SafeBinaryFormatter.CanConvertTo(context, destinationType, new Func<ITypeDescriptorContext, Type, bool>(this.CanConvertTo), true);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            SafeBinaryFormatter.ConvertFrom(value, x => this.<>n__1(context, culture, x), true);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            SafeBinaryFormatter.ConvertTo(value, destinationType, x => this.<>n__0(context, culture, x, destinationType), true);
    }
}

