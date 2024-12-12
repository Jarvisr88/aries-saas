namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class RegionItemInfo
    {
        public RegionItemInfo()
        {
            this.IsInjected = true;
        }

        public string Key { get; set; }

        public string ViewModelName { get; set; }

        public string ViewName { get; set; }

        public string ViewModelStateType { get; set; }

        public bool IsInjected { get; set; }

        public SerializableState ViewModelState { get; set; }
    }
}

