namespace DevExpress.Utils.Serializing
{
    using System;

    public class BooleanContextAttribute : XmlContextItem
    {
        public BooleanContextAttribute(string name, bool val, bool defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            Convert.ToString(base.Value);
    }
}

