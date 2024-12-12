namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class InvalidInputValue : ICustomItem
    {
        public object EditValue { get; set; }

        public object DisplayValue { get; set; }
    }
}

