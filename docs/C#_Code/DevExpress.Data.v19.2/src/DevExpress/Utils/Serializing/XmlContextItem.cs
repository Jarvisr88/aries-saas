namespace DevExpress.Utils.Serializing
{
    using System;

    public abstract class XmlContextItem : IXmlContextItem
    {
        private string name = string.Empty;
        private object val;
        private object defaultValue;

        protected XmlContextItem(string name, object val, object defaultValue)
        {
            this.name = (name == null) ? string.Empty : name;
            this.val = val;
            this.defaultValue = defaultValue;
        }

        public void SetValue(object val)
        {
            this.val = val;
        }

        public abstract string ValueToString();

        public string Name =>
            this.name;

        public object Value =>
            this.val;

        public object DefaultValue =>
            this.defaultValue;
    }
}

