namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    internal class q : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !ReferenceEquals(destinationType, typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentException("Invalid destination type");
            }
            if (!ReferenceEquals(destinationType, typeof(InstanceDescriptor)) || !(value is ParentDataRelation))
            {
                if (ReferenceEquals(destinationType, typeof(string)))
                {
                    return "(ParentRelation)";
                }
            }
            else
            {
                ParentDataRelation relation = (ParentDataRelation) value;
                Type[] types = new Type[] { typeof(DbDataTable), typeof(string[]), typeof(string[]) };
                ConstructorInfo constructor = typeof(ParentDataRelation).GetConstructor(types);
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, new object[] { relation.ParentTable, relation.ParentColumnNames, relation.ChildColumnNames });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

