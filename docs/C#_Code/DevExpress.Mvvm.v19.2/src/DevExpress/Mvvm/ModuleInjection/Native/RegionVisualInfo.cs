namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class RegionVisualInfo
    {
        public RegionVisualInfo()
        {
            this.Items = new List<RegionItemVisualInfo>();
        }

        public string RegionName { get; set; }

        public List<RegionItemVisualInfo> Items { get; set; }
    }
}

