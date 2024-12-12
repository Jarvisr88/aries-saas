namespace #Fqe
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;

    internal class #Gqe : ExpandableNullableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext #Uj, Type #zYf) => 
            !(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) ? base.CanConvertTo(#Uj, #zYf) : true;

        public override object ConvertTo(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld, Type #zYf)
        {
            if (!(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) || !(#Ld is HatchedBackgroundFill))
            {
                return base.ConvertTo(#Uj, #uwf, #Ld, #zYf);
            }
            HatchedBackgroundFill fill = (HatchedBackgroundFill) #Ld;
            Type[] types = new Type[] { typeof(Color), typeof(Color) };
            object[] arguments = new object[] { fill.Color1, fill.Color2 };
            return new InstanceDescriptor(typeof(HatchedBackgroundFill).GetConstructor(types), arguments, true);
        }
    }
}

