namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    public class ObjectContextAttribute : XmlContextItem
    {
        public ObjectContextAttribute(string name, object val, object defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            ObjectConverter.ObjectToString(base.Value);
    }
}

