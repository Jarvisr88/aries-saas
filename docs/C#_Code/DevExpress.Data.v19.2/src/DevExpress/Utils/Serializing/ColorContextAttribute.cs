namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class ColorContextAttribute : XmlContextItem
    {
        public ColorContextAttribute(string name, Color val, Color defaultValue) : base(name, val, defaultValue)
        {
        }

        public override string ValueToString() => 
            Convert.ToString(DXColor.ToArgb((Color) base.Value));
    }
}

