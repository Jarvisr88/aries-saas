namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Resources;

    public class PrintingResXLocalizer : DXResXLocalizer<PrintingStringId>
    {
        public PrintingResXLocalizer() : base(new PrintingLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Xpf.Printing.LocalizationRes", typeof(PrintingResXLocalizer).Assembly);
    }
}

