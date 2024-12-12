namespace DevExpress.Schedule.Serializing
{
    using DevExpress.Schedule;
    using DevExpress.Utils.Serializing;
    using System;

    public class HolidayCollectionContextElement : XmlContextItem
    {
        public HolidayCollectionContextElement(HolidayBaseCollection holidays) : base("Holidays", holidays, null)
        {
        }

        public override string ValueToString() => 
            new HolidayCollectionXmlPersistenceHelper(this.Holidays).ToXml();

        protected HolidayBaseCollection Holidays =>
            (HolidayBaseCollection) base.Value;
    }
}

