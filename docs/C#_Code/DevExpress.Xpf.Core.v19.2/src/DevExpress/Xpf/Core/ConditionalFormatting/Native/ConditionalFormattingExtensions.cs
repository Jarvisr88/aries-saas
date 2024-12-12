namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;

    public static class ConditionalFormattingExtensions
    {
        public static IconSetElement[] GetSortedElements(this IconSetFormat format) => 
            format.GetSortedElements();
    }
}

