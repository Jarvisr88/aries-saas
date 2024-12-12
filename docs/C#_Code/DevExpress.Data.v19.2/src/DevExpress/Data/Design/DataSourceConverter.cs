namespace DevExpress.Data.Design
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class DataSourceConverter : ReferenceConverter
    {
        private readonly ReferenceConverter listConverter;

        public DataSourceConverter();
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
        private static List<IComponent> ConvertToActualDataSources(ICollection lists, IDataContainerService svc, IDataContainerBase dataContainer);
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context);
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context);
        public static bool IsListBindable(object obj);
        public static bool IsListBindable(Type type);
    }
}

