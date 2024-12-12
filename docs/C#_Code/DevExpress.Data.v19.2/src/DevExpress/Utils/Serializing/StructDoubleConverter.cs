namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Xml;

    public abstract class StructDoubleConverter : StructConverter<double>
    {
        protected StructDoubleConverter()
        {
        }

        protected override string ElementToString(double obj) => 
            XmlConvert.ToString(obj);

        protected override double ToType(string str) => 
            XmlConvert.ToDouble(str);
    }
}

