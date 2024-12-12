namespace DevExpress.Schedule.Serializing
{
    using DevExpress.Schedule;
    using DevExpress.Utils.Serializing;
    using System;

    public class HolidayContextElement : XmlContextItem
    {
        public HolidayContextElement(DevExpress.Schedule.Holiday holiday) : base("Holiday", holiday, null)
        {
        }

        public override string ValueToString() => 
            new HolidayXmlPersistenceHelper(this.Holiday).ToXml();

        protected DevExpress.Schedule.Holiday Holiday =>
            (DevExpress.Schedule.Holiday) base.Value;
    }
}

