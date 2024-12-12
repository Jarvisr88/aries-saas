namespace DevExpress.Utils.Serializing
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class XtraSerializablePropertyId : Attribute
    {
        private int id;

        public XtraSerializablePropertyId(int id)
        {
            this.id = id;
        }

        public int Id =>
            this.id;
    }
}

