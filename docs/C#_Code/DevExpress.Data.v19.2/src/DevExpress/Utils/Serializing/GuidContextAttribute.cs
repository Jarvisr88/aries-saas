namespace DevExpress.Utils.Serializing
{
    using System;

    public class GuidContextAttribute : XmlContextItem
    {
        public GuidContextAttribute(string name, Guid val, Guid defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            Convert.ToString(base.Value);
    }
}

