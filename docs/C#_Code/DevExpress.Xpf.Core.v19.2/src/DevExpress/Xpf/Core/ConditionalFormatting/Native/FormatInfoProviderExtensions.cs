namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class FormatInfoProviderExtensions
    {
        public static FormatValueProvider GetValueProvider(this IFormatInfoProvider provider, string fieldName) => 
            new FormatValueProvider(provider, provider.GetCellValue(fieldName), fieldName);
    }
}

