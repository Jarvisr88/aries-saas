namespace DevExpress.Xpf.Core.ConditionalFormatting.Themes
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class QuickIconSetFormatExtension : MarkupExtension
    {
        public static Uri GetUri(string name, bool? isSvg = new bool?())
        {
            Uri uri;
            return (!Uri.TryCreate(((isSvg != null) ? ((string) isSvg.Value) : ((string) ApplicationThemeHelper.UseDefaultSvgImages)) ? (SvgDefaultPath + name + ".svg") : (DefaultPath + name + ".png"), UriKind.Absolute, out uri) ? null : uri);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IconSetFormat format = new IconSetFormat();
            this.TryPopulate(format);
            format.Freeze();
            return format;
        }

        private void TryPopulate(IconSetFormat format)
        {
            if ((this.ElementCount != 0) && (!string.IsNullOrEmpty(this.Path) && !string.IsNullOrEmpty(this.Name)))
            {
                foreach (int num in Enumerable.Range(0, this.ElementCount))
                {
                    IconSetElement element1 = new IconSetElement();
                    element1.Threshold = (100.0 * ((this.ElementCount - 1) - num)) / ((double) this.ElementCount);
                    int num2 = num + 1;
                    IconSetExtension extension1 = new IconSetExtension();
                    extension1.Name = this.Name + num2.ToString();
                    element1.Icon = (ImageSource) extension1.ProvideValue(null);
                    IconSetElement element = element1;
                    format.Elements.Add(element);
                }
                format.IconSetType = this.XlIconSetType;
            }
        }

        public static string DefaultPath =>
            ConditionalFormatResourceHelper.DefaultPathCore;

        public static string SvgDefaultPath =>
            ConditionalFormatResourceHelper.SvgDefaultPathCore;

        public int ElementCount { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string SvgPath { get; set; }

        public XlCondFmtIconSetType? XlIconSetType { get; set; }
    }
}

