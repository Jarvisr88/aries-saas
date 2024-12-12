namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public static class DropDownOptions
    {
        static DropDownOptions()
        {
            DropAlignment = DropDownAlignment.Default;
        }

        public static DropDownAlignment DropAlignment { get; set; }
    }
}

