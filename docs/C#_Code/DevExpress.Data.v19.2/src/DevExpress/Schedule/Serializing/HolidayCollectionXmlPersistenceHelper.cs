namespace DevExpress.Schedule.Serializing
{
    using DevExpress.Schedule;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Xml;

    public class HolidayCollectionXmlPersistenceHelper : CollectionXmlPersistenceHelper
    {
        public HolidayCollectionXmlPersistenceHelper(HolidayBaseCollection holidays) : base(holidays)
        {
        }

        protected override ObjectCollectionXmlLoader CreateObjectCollectionXmlLoader(System.Xml.XmlNode root) => 
            new HolidayCollectionXmlLoader(root, (HolidayBaseCollection) base.Collection);

        protected override IXmlContextItem CreateXmlContextItem(object obj) => 
            new HolidayContextElement((Holiday) obj);

        public static HolidayBaseCollection ObjectFromXml(string xml) => 
            ObjectFromXml(GetRootElement(xml));

        public static HolidayBaseCollection ObjectFromXml(System.Xml.XmlNode root) => 
            (HolidayBaseCollection) new HolidayCollectionXmlPersistenceHelper(new HolidayBaseCollection()).FromXmlNode(root);

        protected override string XmlCollectionName =>
            "Holidays";
    }
}

