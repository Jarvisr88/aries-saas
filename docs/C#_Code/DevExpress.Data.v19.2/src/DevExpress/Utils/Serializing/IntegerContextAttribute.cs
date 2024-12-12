namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Globalization;

    public class IntegerContextAttribute : XmlContextItem
    {
        public IntegerContextAttribute(string name, int val, int defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            Convert.ToInt32(base.Value).ToString(CultureInfo.InvariantCulture);
    }
}

