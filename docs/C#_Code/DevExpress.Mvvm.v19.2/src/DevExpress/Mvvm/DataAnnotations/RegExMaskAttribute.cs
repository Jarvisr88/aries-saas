namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class RegExMaskAttribute : RegExMaskAttributeBase
    {
        public RegExMaskAttribute()
        {
            this.ShowPlaceHolders = true;
        }

        public bool ShowPlaceHolders { get; set; }

        internal override bool ShowPlaceHoldersInternal =>
            this.ShowPlaceHolders;

        internal override DevExpress.Mvvm.Native.RegExMaskType RegExMaskType =>
            DevExpress.Mvvm.Native.RegExMaskType.RegEx;
    }
}

