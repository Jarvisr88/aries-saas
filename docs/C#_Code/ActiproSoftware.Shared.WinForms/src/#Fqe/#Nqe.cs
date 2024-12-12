namespace #Fqe
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;

    internal class #Nqe : ExpandableNullableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext #Uj, Type #zYf) => 
            !(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) ? base.CanConvertTo(#Uj, #zYf) : true;

        public override object ConvertTo(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld, Type #zYf)
        {
            if (!(#zYf == Type.GetTypeFromHandle(typeof(InstanceDescriptor).TypeHandle)) || !(#Ld is TwoColorLinearGradient))
            {
                return base.ConvertTo(#Uj, #uwf, #Ld, #zYf);
            }
            TwoColorLinearGradient gradient = (TwoColorLinearGradient) #Ld;
            if (!gradient.ShouldSerializeFocus() && !gradient.ShouldSerializeScale())
            {
                Type[] typeArray2 = new Type[] { typeof(Color), typeof(Color), typeof(float), typeof(TwoColorLinearGradientStyle), typeof(BackgroundFillRotationType) };
                object[] objArray2 = new object[] { gradient.StartColor, gradient.EndColor, gradient.Angle, gradient.Style, gradient.RotationType };
                return new InstanceDescriptor(typeof(TwoColorLinearGradient).GetConstructor(typeArray2), objArray2, true);
            }
            Type[] types = new Type[] { typeof(Color), typeof(Color), typeof(float), typeof(TwoColorLinearGradientStyle), typeof(float), typeof(float), typeof(BackgroundFillRotationType) };
            object[] arguments = new object[] { gradient.StartColor, gradient.EndColor, gradient.Angle, gradient.Style, gradient.Focus, gradient.Scale, gradient.RotationType };
            return new InstanceDescriptor(typeof(TwoColorLinearGradient).GetConstructor(types), arguments, true);
        }
    }
}

