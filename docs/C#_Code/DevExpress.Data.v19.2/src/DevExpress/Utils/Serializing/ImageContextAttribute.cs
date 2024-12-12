namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Drawing;

    public class ImageContextAttribute : XmlContextItem
    {
        public ImageContextAttribute(string name, Image val, Image defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            ObjectConverter.ObjectToString(base.Value as Image);
    }
}

