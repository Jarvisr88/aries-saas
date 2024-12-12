namespace #Fqe
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;

    internal class #Lqe : ExpandableNullableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext #Uj, Type #zYf) => 
            !(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) ? base.CanConvertTo(#Uj, #zYf) : true;

        public override object ConvertTo(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld, Type #zYf)
        {
            if (!(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) || !(#Ld is SimpleBorder))
            {
                return base.ConvertTo(#Uj, #uwf, #Ld, #zYf);
            }
            SimpleBorder border = (SimpleBorder) #Ld;
            Type[] types = new Type[] { typeof(SimpleBorderStyle), typeof(Color) };
            object[] arguments = new object[] { border.Style, border.Color };
            return new InstanceDescriptor(typeof(SimpleBorder).GetConstructor(types), arguments, true);
        }
    }
}

