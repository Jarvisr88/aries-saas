namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class PageSizeInfo
    {
        public static readonly Size DefaultSize = new Size(850, 0x44c);
        private static readonly Dictionary<PaperKind, PageSize> pageSizes = CreatePageSizeDictionary();

        private static PageSize CreatePageSizeData(string width, string height)
        {
            char[] separator = new char[] { ' ' };
            string[] source = width.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            char[] chArray2 = new char[] { ' ' };
            string[] strArray2 = height.Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
            if ((source.Length != 2) || (source.Last<string>() != strArray2.Last<string>()))
            {
                throw new FormatException("page size");
            }
            return new PageSize(new SizeF(float.Parse(source.First<string>(), CultureInfo.InvariantCulture), float.Parse(strArray2.First<string>(), CultureInfo.InvariantCulture)), CreatePageUnit(source.Last<string>()));
        }

        private static Dictionary<PaperKind, PageSize> CreatePageSizeDictionary()
        {
            Dictionary<PaperKind, PageSize> dictionary = new Dictionary<PaperKind, PageSize>();
            try
            {
                foreach (System.Xml.XmlNode node in SafeXml.CreateDocument(typeof(PageSizeInfo).Assembly.GetManifestResourceStream("DevExpress.Data.Printing.Native.PaperKind.xml"), null, null).GetElementsByTagName("PaperKind"))
                {
                    string innerXml = node.ChildNodes[1].InnerXml;
                    string width = node.ChildNodes[2].InnerXml;
                    string height = node.ChildNodes[3].InnerXml;
                    try
                    {
                        dictionary.Add((PaperKind) Enum.Parse(typeof(PaperKind), innerXml, false), CreatePageSizeData(width, height));
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
            catch
            {
            }
            return dictionary;
        }

        private static GraphicsUnit CreatePageUnit(string pageUnit)
        {
            string str = pageUnit.ToLower();
            if (str == "mm")
            {
                return GraphicsUnit.Millimeter;
            }
            if (str != "in")
            {
                throw new NotSupportedException("pageUnit");
            }
            return GraphicsUnit.Inch;
        }

        public static PrinterSettings.PaperSizeCollection CreatePaperSizeCollection(bool addCustomPaper = false)
        {
            List<PaperSize> source = new List<PaperSize>();
            foreach (PaperKind kind in GetPaperKinds())
            {
                if ((kind != PaperKind.Custom) | addCustomPaper)
                {
                    Size pageSize = GetPageSize(kind, (float) 100f);
                    string displayTypeName = DisplayTypeNameHelper.GetDisplayTypeName(kind);
                    PaperSize item = new PaperSize();
                    item.PaperName = displayTypeName;
                    item.RawKind = (int) kind;
                    item.Width = pageSize.Width;
                    item.Height = pageSize.Height;
                    source.Add(item);
                }
            }
            Func<PaperSize, string> keySelector = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Func<PaperSize, string> local1 = <>c.<>9__21_0;
                keySelector = <>c.<>9__21_0 = x => x.ToString();
            }
            return new PrinterSettings.PaperSizeCollection(source.OrderBy<PaperSize, string>(keySelector).ToArray<PaperSize>());
        }

        private static void EnshureInitialized(TypeConverter converter)
        {
            RuntimeTypeDescriptorContext context = new RuntimeTypeDescriptorContext(null, null);
            object obj2 = converter.ConvertTo(context, null, PaperKind.Letter, typeof(string));
        }

        public static PaperSize GetAppropriatePaperSize(PrinterSettings.PaperSizeCollection paperSizes, PaperSize paperSize) => 
            GetAppropriatePaperSize(paperSizes, paperSize.Kind, paperSize.Width, paperSize.Height);

        public static PaperSize GetAppropriatePaperSize(PrinterSettings.PaperSizeCollection paperSizes, PaperKind paperKind, int paperWidth, int paperHeight)
        {
            PaperSize paperSizeByCondition = null;
            if (paperKind != PaperKind.Custom)
            {
                paperSizeByCondition = GetPaperSizeByCondition(paperSizes, p => GetRawKind(p) == paperKind);
            }
            return GetPaperSizeByCondition(paperSizes, p => (p.Width == paperWidth) && (p.Height == paperHeight));
        }

        private static SizeF GetDefaultPageSize(float targetDpi) => 
            (SizeF) GraphicsUnitConverter.Convert(DefaultSize, (float) 100f, targetDpi);

        public static Size GetPageSize(PaperKind paperKind) => 
            GetPageSize(paperKind, (float) 100f, DefaultSize);

        public static Size GetPageSize(PaperKind paperKind, GraphicsUnit targetUnit) => 
            GetPageSize(paperKind, GraphicsDpi.UnitToDpi(targetUnit));

        public static Size GetPageSize(PaperKind paperKind, Size defaultValue) => 
            GetPageSize(paperKind, (float) 100f, defaultValue);

        public static Size GetPageSize(PaperKind paperKind, float targetDpi) => 
            GetPageSize(paperKind, targetDpi, Size.Round(GetDefaultPageSize(targetDpi)));

        public static Size GetPageSize(PaperKind paperKind, GraphicsUnit targetUnit, Size defaultValue) => 
            GetPageSize(paperKind, GraphicsDpi.UnitToDpi(targetUnit), defaultValue);

        public static Size GetPageSize(PaperKind paperKind, float targetDpi, Size defaultValue) => 
            Size.Round(GetPageSizeF(paperKind, targetDpi, (SizeF) defaultValue));

        public static Size GetPageSize(string paperName, string printerName, Size defaultValue)
        {
            try
            {
                PrinterSettings settings1 = new PrinterSettings();
                settings1.PrinterName = printerName;
                using (IEnumerator enumerator = settings1.PaperSizes.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PaperSize current = (PaperSize) enumerator.Current;
                        if (Equals(current.PaperName, paperName))
                        {
                            return new Size(current.Width, current.Height);
                        }
                    }
                }
            }
            catch
            {
            }
            return defaultValue;
        }

        public static SizeF GetPageSizeF(PaperKind paperKind, float targetDpi) => 
            GetPageSizeF(paperKind, targetDpi, GetDefaultPageSize(targetDpi));

        public static SizeF GetPageSizeF(PaperKind paperKind, float targetDpi, SizeF defaultValue)
        {
            PageSize size;
            return (pageSizes.TryGetValue(paperKind, out size) ? size.GetPageSize(targetDpi) : defaultValue);
        }

        private static ICollection GetPaperKinds()
        {
            TypeConverter converter = new PaperKindConverter(typeof(PaperKind));
            EnshureInitialized(converter);
            return (RuntimeTypeDescriptorContext.GetStandardValues(null, converter) ?? typeof(PaperKind).GetEnumValues());
        }

        internal static PaperSize GetPaperSize(PaperKind paperKind, Size pageSize, PrinterSettings.PaperSizeCollection paperSizes)
        {
            PaperSize size = GetAppropriatePaperSize(paperSizes, paperKind, pageSize.Width, pageSize.Height);
            return ((size != null) ? size : new PaperSize(paperKind.ToString(), pageSize.Width, pageSize.Height));
        }

        private static PaperSize GetPaperSizeByCondition(PrinterSettings.PaperSizeCollection paperSizes, Predicate<PaperSize> match)
        {
            PaperSize size2;
            using (IEnumerator enumerator = paperSizes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PaperSize current = (PaperSize) enumerator.Current;
                        if (!match(current))
                        {
                            continue;
                        }
                        size2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return size2;
        }

        internal static PaperKind GetRawKind(PaperSize paperSize) => 
            (PaperKind) paperSize.RawKind;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageSizeInfo.<>c <>9 = new PageSizeInfo.<>c();
            public static Func<PaperSize, string> <>9__21_0;

            internal string <CreatePaperSizeCollection>b__21_0(PaperSize x) => 
                x.ToString();
        }

        public class PageSize
        {
            private readonly SizeF size;
            private readonly GraphicsUnit unit;
            private readonly float dpi;

            public PageSize(SizeF size, GraphicsUnit unit)
            {
                this.size = size;
                this.unit = unit;
                this.dpi = GraphicsDpi.UnitToDpi(unit);
            }

            public SizeF GetPageSize(GraphicsUnit targetUnit) => 
                (targetUnit != this.Unit) ? GraphicsUnitConverter.Convert(this.Size, this.Unit, targetUnit) : this.Size;

            public SizeF GetPageSize(float targetDpi) => 
                (targetDpi != this.Dpi) ? GraphicsUnitConverter.Convert(this.Size, this.Dpi, targetDpi) : this.Size;

            public SizeF Size =>
                this.size;

            public GraphicsUnit Unit =>
                this.unit;

            public float Dpi =>
                this.dpi;
        }
    }
}

