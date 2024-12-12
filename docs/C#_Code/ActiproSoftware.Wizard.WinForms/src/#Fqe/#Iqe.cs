namespace #Fqe
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;

    internal class #Iqe : ExpandableNullableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext #Uj, Type #zYf) => 
            !(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) ? base.CanConvertTo(#Uj, #zYf) : true;

        public override object ConvertTo(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld, Type #zYf)
        {
            if (!(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) || !(#Ld is LinearGradientColorPosition))
            {
                return base.ConvertTo(#Uj, #uwf, #Ld, #zYf);
            }
            LinearGradientColorPosition position = (LinearGradientColorPosition) #Ld;
            Type[] types = new Type[] { typeof(Color), typeof(float) };
            object[] arguments = new object[] { position.Color, position.Position };
            return new InstanceDescriptor(typeof(LinearGradientColorPosition).GetConstructor(types), arguments, true);
        }
    }
}

