namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    public sealed class InvalidFormatCache
    {
        private static readonly InvalidFormatCache instance = new InvalidFormatCache();

        private InvalidFormatCache()
        {
        }

        public static InvalidFormatCache Instance =>
            instance;
    }
}

