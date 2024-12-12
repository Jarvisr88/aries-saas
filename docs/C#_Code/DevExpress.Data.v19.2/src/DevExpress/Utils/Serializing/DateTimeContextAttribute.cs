namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Globalization;

    public class DateTimeContextAttribute : XmlContextItem
    {
        public DateTimeContextAttribute(string name, DateTime val, DateTime defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            Convert.ToDateTime(base.Value).ToString(CultureInfo.InvariantCulture.DateTimeFormat);
    }
}

