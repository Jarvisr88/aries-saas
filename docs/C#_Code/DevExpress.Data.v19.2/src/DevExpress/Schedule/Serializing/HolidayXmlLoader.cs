namespace DevExpress.Schedule.Serializing
{
    using DevExpress.Schedule;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Xml;

    public class HolidayXmlLoader : ObjectXmlLoader
    {
        public HolidayXmlLoader(System.Xml.XmlNode root) : base(root)
        {
        }

        public override object ObjectFromXml() => 
            new Holiday(base.ReadAttributeAsDateTime("Date", DateTime.MinValue), base.ReadAttributeAsString("DisplayName", string.Empty), base.ReadAttributeAsString("Location", string.Empty));
    }
}

