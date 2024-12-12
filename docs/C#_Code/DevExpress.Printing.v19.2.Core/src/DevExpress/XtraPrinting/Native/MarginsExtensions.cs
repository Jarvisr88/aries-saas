namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    internal static class MarginsExtensions
    {
        public static Margins SetBottom(this Margins margins, int value);
        public static Margins SetLeft(this Margins margins, int value);
        public static Margins SetRight(this Margins margins, int value);
        public static Margins SetTop(this Margins margins, int value);
    }
}

