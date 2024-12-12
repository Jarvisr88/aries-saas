namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;

    public class PrinterViewModel
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ImageUri { get; set; }

        public string Status { get; set; }

        public bool IsOffline { get; set; }
    }
}

