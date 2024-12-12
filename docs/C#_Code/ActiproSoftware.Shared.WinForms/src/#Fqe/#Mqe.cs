namespace #Fqe
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;

    internal class #Mqe : ExpandableNullableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext #Uj, Type #zYf) => 
            !(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) ? base.CanConvertTo(#Uj, #zYf) : true;

        public override object ConvertTo(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld, Type #zYf)
        {
            if (!(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) || !(#Ld is SolidColorBackgroundFill))
            {
                return base.ConvertTo(#Uj, #uwf, #Ld, #zYf);
            }
            SolidColorBackgroundFill fill = (SolidColorBackgroundFill) #Ld;
            Type[] types = new Type[] { typeof(Color) };
            object[] arguments = new object[] { fill.Color };
            return new InstanceDescriptor(typeof(SolidColorBackgroundFill).GetConstructor(types), arguments, true);
        }
    }
}

