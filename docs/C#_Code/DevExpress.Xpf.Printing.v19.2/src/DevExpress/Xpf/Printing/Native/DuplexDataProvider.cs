namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Printing;
    using System.Windows.Markup;

    public class DuplexDataProvider : MarkupExtension
    {
        private static readonly IEnumerable<Duplex> duplexModes;

        static DuplexDataProvider()
        {
            Duplex[] duplexArray1 = new Duplex[] { Duplex.Simplex, Duplex.Horizontal, Duplex.Vertical };
            duplexModes = duplexArray1;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            duplexModes;
    }
}

