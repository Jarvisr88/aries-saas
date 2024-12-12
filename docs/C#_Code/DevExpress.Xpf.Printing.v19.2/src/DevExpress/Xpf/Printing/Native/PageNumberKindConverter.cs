namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    internal static class PageNumberKindConverter
    {
        public static PageInfo ToPageInfo(this PageNumberKind kind)
        {
            switch (kind)
            {
                case PageNumberKind.None:
                    return PageInfo.None;

                case PageNumberKind.Number:
                    return PageInfo.Number;

                case PageNumberKind.NumberOfTotal:
                    return PageInfo.NumberOfTotal;

                case PageNumberKind.RomanLowNumber:
                    return PageInfo.RomLowNumber;

                case PageNumberKind.RomanHiNumber:
                    return PageInfo.RomHiNumber;
            }
            throw new ArgumentException("Unexpected PageNumberKind value", "kind");
        }
    }
}

