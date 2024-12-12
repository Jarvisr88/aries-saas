namespace DevExpress.Schedule.Serializing
{
    using DevExpress.Schedule;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Xml;

    public class HolidayXmlPersistenceHelper : XmlPersistenceHelper
    {
        private Holiday holiday;

        public HolidayXmlPersistenceHelper(Holiday holiday)
        {
            this.holiday = holiday;
        }

        public override ObjectXmlLoader CreateObjectXmlLoader(System.Xml.XmlNode root) => 
            new HolidayXmlLoader(root);

        protected override IXmlContext GetXmlContext()
        {
            DevExpress.Utils.Serializing.XmlContext context = new DevExpress.Utils.Serializing.XmlContext("Holiday");
            context.Attributes.Add(new DateTimeContextAttribute("Date", this.holiday.Date, DateTime.MinValue));
            context.Attributes.Add(new StringContextAttribute("DisplayName", this.holiday.DisplayName, string.Empty));
            context.Attributes.Add(new StringContextAttribute("Location", this.holiday.Location, string.Empty));
            return context;
        }

        public static Holiday ObjectFromXml(string xml) => 
            ObjectFromXml(GetRootElement(xml));

        public static Holiday ObjectFromXml(System.Xml.XmlNode root) => 
            (Holiday) new HolidayXmlPersistenceHelper(null).FromXmlNode(root);
    }
}

