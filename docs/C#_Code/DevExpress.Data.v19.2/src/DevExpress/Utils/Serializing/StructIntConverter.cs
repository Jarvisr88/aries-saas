namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Xml;

    public abstract class StructIntConverter : StructConverter<int>
    {
        protected StructIntConverter()
        {
        }

        protected override string ElementToString(int obj) => 
            XmlConvert.ToString(obj);

        protected override int ToType(string str) => 
            XmlConvert.ToInt32(str);
    }
}

