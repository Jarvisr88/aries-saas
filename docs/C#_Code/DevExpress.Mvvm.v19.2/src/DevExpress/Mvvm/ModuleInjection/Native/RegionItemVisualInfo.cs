namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class RegionItemVisualInfo
    {
        public string Key { get; set; }

        public string ViewName { get; set; }

        public string ViewPart { get; set; }

        public SerializableState State { get; set; }
    }
}

