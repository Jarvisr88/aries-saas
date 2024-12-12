namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class SimpleMaskAttribute : RegExMaskAttributeBase
    {
        public SimpleMaskAttribute()
        {
            this.SaveLiteral = true;
        }

        public bool SaveLiteral { get; set; }

        internal override bool SaveLiteralInternal =>
            this.SaveLiteral;

        internal override DevExpress.Mvvm.Native.RegExMaskType RegExMaskType =>
            DevExpress.Mvvm.Native.RegExMaskType.Simple;
    }
}

