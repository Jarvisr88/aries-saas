namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Xml;

    public abstract class StructFloatConverter : StructConverter<float>
    {
        protected StructFloatConverter()
        {
        }

        protected override string ElementToString(float obj) => 
            XmlConvert.ToString(obj);

        protected override float ToType(string str) => 
            XmlConvert.ToSingle(str);
    }
}

