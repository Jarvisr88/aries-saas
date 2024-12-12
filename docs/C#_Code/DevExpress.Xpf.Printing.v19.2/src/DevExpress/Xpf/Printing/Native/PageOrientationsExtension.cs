namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class PageOrientationsExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new PageOrientationData[] { new PageOrientationData(false), new PageOrientationData(true) };

        public class PageOrientationData : ImmutableObject
        {
            public PageOrientationData(bool landscape)
            {
                this.Landscape = landscape;
                this.DisplayName = PrintingLocalizer.GetString(landscape ? PrintingStringId.PageSetupOrientationLandscape : PrintingStringId.PageSetupOrientationPortrait);
            }

            public bool Landscape { get; private set; }

            public string DisplayName { get; private set; }
        }
    }
}

