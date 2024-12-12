namespace DevExpress.Xpf.Core.Internal
{
    using System;

    internal enum BamlEntryTypes : byte
    {
        public const BamlEntryTypes UnknownEntry = BamlEntryTypes.UnknownEntry;,
        public const BamlEntryTypes ConvertedTextEntry = BamlEntryTypes.ConvertedTextEntry;,
        public const BamlEntryTypes DeferrableAttributeEntry = BamlEntryTypes.DeferrableAttributeEntry;,
        public const BamlEntryTypes AttributeEntry = BamlEntryTypes.AttributeEntry;,
        public const BamlEntryTypes StringEntry = BamlEntryTypes.StringEntry;,
        public const BamlEntryTypes ConvertedPropertyEntry = BamlEntryTypes.ConvertedPropertyEntry;,
        public const BamlEntryTypes StartNamedElementEntry = BamlEntryTypes.StartNamedElementEntry;,
        public const BamlEntryTypes CustomPropertyEntry = BamlEntryTypes.CustomPropertyEntry;
    }
}

