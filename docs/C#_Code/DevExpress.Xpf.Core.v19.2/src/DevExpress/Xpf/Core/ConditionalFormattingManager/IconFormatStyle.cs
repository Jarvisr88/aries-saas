namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class IconFormatStyle : Freezable
    {
        public IconFormatStyle(IconSetFormat format, string formatName)
        {
            this.Format = format;
            this.FormatName = formatName;
            Func<IconSetElement, ImageSource> selector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IconSetElement, ImageSource> local1 = <>c.<>9__0_0;
                selector = <>c.<>9__0_0 = x => x.Icon;
            }
            this.Icons = format.Elements.Select<IconSetElement, ImageSource>(selector).ToArray<ImageSource>();
        }

        protected override Freezable CreateInstanceCore() => 
            new IconFormatStyle(this.Format, this.FormatName);

        internal string FormatName { get; private set; }

        internal IconSetFormat Format { get; private set; }

        public ImageSource[] Icons { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IconFormatStyle.<>c <>9 = new IconFormatStyle.<>c();
            public static Func<IconSetElement, ImageSource> <>9__0_0;

            internal ImageSource <.ctor>b__0_0(IconSetElement x) => 
                x.Icon;
        }
    }
}

