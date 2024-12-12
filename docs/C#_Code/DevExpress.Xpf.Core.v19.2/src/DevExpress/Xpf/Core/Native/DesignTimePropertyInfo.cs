namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class DesignTimePropertyInfo
    {
        public DesignTimePropertyInfo(string name, Type propertyType, bool isReadonly);

        public string Name { get; private set; }

        public Type PropertyType { get; private set; }

        public bool IsReadonly { get; private set; }
    }
}

