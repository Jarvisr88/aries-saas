namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class ReportLayoutRevisionIdentityConverter : TypeConverter
    {
        private static readonly ConstructorInfo reportLayoutRevisionIdentityConstructor;

        static ReportLayoutRevisionIdentityConverter()
        {
            Type[] types = new Type[] { typeof(int) };
            reportLayoutRevisionIdentityConstructor = typeof(ReportLayoutRevisionIdentity).GetConstructor(types);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            ReportLayoutRevisionIdentity identity = value as ReportLayoutRevisionIdentity;
            if (!(destinationType == typeof(InstanceDescriptor)) || (identity == null))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            object[] arguments = new object[] { identity.Id };
            return new InstanceDescriptor(reportLayoutRevisionIdentityConstructor, arguments);
        }
    }
}

