namespace DevExpress.Utils.Serializing
{
    using System;

    public class XtraObjectInfo
    {
        private object instance;
        private string name;

        public XtraObjectInfo(string name, object instance)
        {
            this.name = name;
            this.instance = instance;
        }

        public virtual bool Skip =>
            false;

        public object Instance =>
            this.instance;

        public string Name =>
            this.name;
    }
}

