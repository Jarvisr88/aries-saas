namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;

    public static class FontWeightHelper
    {
        public static int Compare(FontWeight left, FontWeight right) => 
            FontWeight.Compare(left, right);
    }
}

