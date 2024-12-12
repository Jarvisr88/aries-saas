namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class RegExMaskAttributeBase : MaskAttributeBase
    {
        public RegExMaskAttributeBase()
        {
            this.IgnoreBlank = true;
            this.PlaceHolder = '_';
            base.UseAsDisplayFormat = false;
        }

        public bool IgnoreBlank { get; set; }

        public char PlaceHolder { get; set; }

        internal override bool IgnoreBlankInternal =>
            this.IgnoreBlank;

        internal override char PlaceHolderInternal =>
            this.PlaceHolder;

        internal abstract DevExpress.Mvvm.Native.RegExMaskType RegExMaskType { get; }
    }
}

