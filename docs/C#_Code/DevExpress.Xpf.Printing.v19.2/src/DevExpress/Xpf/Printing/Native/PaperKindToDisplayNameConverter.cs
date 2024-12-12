namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class PaperKindToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PaperKind)
            {
                return CreatePaperKindInfo((PaperKind) value);
            }
            if (!(value is IEnumerable<PaperKind>))
            {
                throw new NotSupportedException("value");
            }
            Func<PaperKind, PaperKindInfo> selector = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<PaperKind, PaperKindInfo> local1 = <>c.<>9__1_0;
                selector = <>c.<>9__1_0 = paperKind => CreatePaperKindInfo(paperKind);
            }
            Func<PaperKindInfo, string> keySelector = <>c.<>9__1_1;
            if (<>c.<>9__1_1 == null)
            {
                Func<PaperKindInfo, string> local2 = <>c.<>9__1_1;
                keySelector = <>c.<>9__1_1 = paperKindInfo => paperKindInfo.DisplayName;
            }
            return ((IEnumerable<PaperKind>) value).Select<PaperKind, PaperKindInfo>(selector).OrderBy<PaperKindInfo, string>(keySelector);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is PaperKindInfo))
            {
                throw new NotSupportedException("value");
            }
            return ((PaperKindInfo) value).PaperKind;
        }

        private static PaperKindInfo CreatePaperKindInfo(PaperKind paperKind)
        {
            string displayName = GetDisplayName(paperKind);
            PaperKindInfo info1 = new PaperKindInfo();
            info1.PaperKind = paperKind;
            info1.DisplayName = displayName;
            return info1;
        }

        private static string GetDisplayName(PaperKind paperKind)
        {
            string name = Enum.GetName(paperKind.GetType(), paperKind);
            string str2 = $"{paperKind.GetType().Name}_{name}";
            try
            {
                return PrintingLocalizer.GetString((PrintingStringId) Enum.Parse(typeof(PrintingStringId), str2, true));
            }
            catch
            {
                return name;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PaperKindToDisplayNameConverter.<>c <>9 = new PaperKindToDisplayNameConverter.<>c();
            public static Func<PaperKind, PaperKindToDisplayNameConverter.PaperKindInfo> <>9__1_0;
            public static Func<PaperKindToDisplayNameConverter.PaperKindInfo, string> <>9__1_1;

            internal PaperKindToDisplayNameConverter.PaperKindInfo <Convert>b__1_0(PaperKind paperKind) => 
                PaperKindToDisplayNameConverter.CreatePaperKindInfo(paperKind);

            internal string <Convert>b__1_1(PaperKindToDisplayNameConverter.PaperKindInfo paperKindInfo) => 
                paperKindInfo.DisplayName;
        }

        public class PaperKindInfo
        {
            public System.Drawing.Printing.PaperKind PaperKind { get; set; }

            public string DisplayName { get; set; }
        }
    }
}

