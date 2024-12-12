namespace DevExpress.Data.Filtering
{
    using System;
    using System.Xml.Serialization;

    [XmlType("DBNull")]
    public class NullValue
    {
        public static NullValue Value;

        static NullValue();
    }
}

