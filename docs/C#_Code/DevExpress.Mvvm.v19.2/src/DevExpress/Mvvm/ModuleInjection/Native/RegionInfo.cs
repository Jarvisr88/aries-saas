namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class RegionInfo
    {
        public RegionInfo()
        {
            this.Items = new List<RegionItemInfo>();
        }

        public string RegionName { get; set; }

        public string SelectedViewModelKey { get; set; }

        public List<RegionItemInfo> Items { get; set; }
    }
}

