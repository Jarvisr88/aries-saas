namespace #Fqe
{
    using #H;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class #Hqe : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext #Uj, Type #zYf) => 
            !(#zYf == Type.GetTypeFromHandle(typeof(string).TypeHandle)) ? base.CanConvertTo(#Uj, #zYf) : true;

        public override object ConvertTo(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld, Type #zYf)
        {
            if (!(#zYf == Type.GetTypeFromHandle(typeof(string).TypeHandle)))
            {
                return base.ConvertTo(#Uj, #uwf, #Ld, #zYf);
            }
            LinearGradientColorPosition[] positionArray = (LinearGradientColorPosition[]) #Ld;
            return (((positionArray == null) || (positionArray.Length == 0)) ? #G.#eg(0x2f11) : (#G.#eg(0x2f1a) + positionArray.Length.ToString() + #G.#eg(0x2f1f)));
        }
    }
}

