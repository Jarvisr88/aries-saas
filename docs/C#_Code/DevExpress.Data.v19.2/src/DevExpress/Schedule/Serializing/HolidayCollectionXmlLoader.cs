namespace DevExpress.Schedule.Serializing
{
    using DevExpress.Schedule;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.Xml;

    public class HolidayCollectionXmlLoader : ObjectCollectionXmlLoader
    {
        private HolidayBaseCollection holidays;

        public HolidayCollectionXmlLoader(System.Xml.XmlNode root, HolidayBaseCollection holidays) : base(root)
        {
            this.holidays = holidays;
        }

        protected override void AddObjectToCollection(object obj)
        {
            this.holidays.Add((Holiday) obj);
        }

        protected override void ClearCollectionObjects()
        {
            this.holidays.Clear();
        }

        protected override object LoadObject(System.Xml.XmlNode root) => 
            HolidayXmlPersistenceHelper.ObjectFromXml(root);

        protected override ICollection Collection =>
            this.holidays;

        protected override string XmlCollectionName =>
            "Holidays";
    }
}

