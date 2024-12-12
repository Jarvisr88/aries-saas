namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class FontFamiliesCollectionExtension : MarkupExtension
    {
        private static readonly IEnumerable<string> fontFamilies = (from x in FontFamily.Families select x.Name).ToList<string>().AsReadOnly();

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            fontFamilies;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontFamiliesCollectionExtension.<>c <>9 = new FontFamiliesCollectionExtension.<>c();

            internal string <.cctor>b__3_0(FontFamily x) => 
                x.Name;
        }
    }
}

