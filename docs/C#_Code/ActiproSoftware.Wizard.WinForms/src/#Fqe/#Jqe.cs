namespace #Fqe
{
    using #H;
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    internal class #Jqe : ExpandableNullableObjectConverter
    {
        public override bool #H9e(ITypeDescriptorContext #Uj, Type #gIf) => 
            !(#gIf == Type.GetTypeFromHandle(typeof(string).TypeHandle)) ? base.CanConvertTo(#Uj, #gIf) : true;

        public override object #I9e(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld)
        {
            if (#Ld is string)
            {
                string s = #Ld.ToString();
                try
                {
                    return new Padding(int.Parse(s));
                }
                catch
                {
                }
            }
            return base.ConvertFrom(#Uj, #uwf, #Ld);
        }

        public override PropertyDescriptorCollection #M9e(ITypeDescriptorContext #Uj, object #Ld, Attribute[] #6xc)
        {
            string[] names = new string[] { #G.#eg(0x2f39), #G.#eg(0x2f3e), #G.#eg(0x2f47), #G.#eg(0x2f4c), #G.#eg(0x2f55) };
            return TypeDescriptor.GetProperties(Type.GetTypeFromHandle(typeof(Padding).TypeHandle), #6xc).Sort(names);
        }

        public override bool #N9e(ITypeDescriptorContext #Uj) => 
            true;

        public override bool CanConvertTo(ITypeDescriptorContext #Uj, Type #zYf) => 
            !(#zYf == Type.GetTypeFromHandle(typeof(string).TypeHandle)) ? (!(#zYf == typeof(InstanceDescriptor)) ? base.CanConvertTo(#Uj, #zYf) : true) : true;

        public override object ConvertTo(ITypeDescriptorContext #Uj, CultureInfo #uwf, object #Ld, Type #zYf)
        {
            if (#zYf == Type.GetTypeFromHandle(typeof(string).TypeHandle))
            {
                if (#Ld is Padding)
                {
                    Padding padding = (Padding) #Ld;
                    if (padding.IsEmpty)
                    {
                        return #G.#eg(0x2f2c);
                    }
                    if (padding.AllSidesEqual)
                    {
                        return padding.All.ToString();
                    }
                    string[] textArray1 = new string[] { padding.Left.ToString(), #G.#eg(0xc31), padding.Top.ToString(), #G.#eg(0xc31), padding.Right.ToString(), #G.#eg(0xc31), padding.Bottom.ToString() };
                    return string.Concat(textArray1);
                }
            }
            else if ((#zYf == typeof(InstanceDescriptor)) && (#Ld is Padding))
            {
                Padding padding2 = (Padding) #Ld;
                if (padding2.AllSidesEqual)
                {
                    Type[] typeArray1 = new Type[] { typeof(int) };
                    object[] objArray1 = new object[] { padding2.Left };
                    return new InstanceDescriptor(#Ld.GetType().GetConstructor(typeArray1), objArray1, true);
                }
                Type[] types = new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) };
                object[] arguments = new object[] { padding2.Left, padding2.Top, padding2.Right, padding2.Bottom };
                return new InstanceDescriptor(#Ld.GetType().GetConstructor(types), arguments, true);
            }
            return base.ConvertTo(#Uj, #uwf, #Ld, #zYf);
        }
    }
}

