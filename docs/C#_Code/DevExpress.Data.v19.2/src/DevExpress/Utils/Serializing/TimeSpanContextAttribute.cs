namespace DevExpress.Utils.Serializing
{
    using System;

    public class TimeSpanContextAttribute : XmlContextItem
    {
        public TimeSpanContextAttribute(string name, TimeSpan val, TimeSpan defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            ((TimeSpan) base.Value).ToString();
    }
}

