namespace DevExpress.Utils.Serializing
{
    using System;

    public class StringContextAttribute : XmlContextItem
    {
        public StringContextAttribute(string name, string val, string defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            Convert.ToString(base.Value);
    }
}

