namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;

    public class BaseLayoutItemSerializationInfo
    {
        public BaseLayoutItemSerializationInfo(BaseLayoutItem owner)
        {
            this.Owner = owner;
        }

        public BaseLayoutItem Owner { get; private set; }
    }
}

