namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Collections.Generic;

    internal static class BamlEntryContextFactory
    {
        private const byte EndElementType = 4;
        private const byte StartElementType = 3;
        private const byte DContentStartType = 0x25;
        private const byte DAttributeKeyStringType = 0x26;
        private const byte DAttributeKeyTypeType = 0x27;
        public const byte KeyElementStartType = 40;
        public const byte KeyElementEndType = 0x29;
        public const byte StaticResourceStartType = 0x30;
        public const byte StaticResourceEndType = 0x31;
        private const byte StaticResourceOptimizedType = 0x37;
        public static readonly HashSet<byte> SingleByteEntryTypes;
        public static readonly HashSet<byte> CommonEntryTypes;
        public static readonly HashSet<byte> ExtendedEntryTypes;
        public static readonly HashSet<byte> DeferrableEntryTypes;

        static BamlEntryContextFactory();
        public static BamlEntryContext Create(byte type);
        public static bool IsCommonContext(byte type);
        public static bool IsDeferrableAttributeKeyString(BamlEntryContext context);
        public static bool IsDeferrableAttributeKeyType(BamlEntryContext context);
        public static bool IsDeferrableAttributeKeyTypeOrKeyStart(BamlEntryContext context);
        public static bool IsDeferrableContentStart(BamlEntryContext context);
        public static bool IsDeferrableEntry(BamlEntryContext context);
        public static bool IsEndElement(BamlEntryContext context);
        public static bool IsExtendedContext(byte type);
        public static bool IsKeyStart(BamlEntryContext context);
        public static bool IsSingleByteContext(byte type);
        public static bool IsStartElement(BamlEntryContext context);
        public static bool IsStaticResourceOptimized(BamlEntryContext context);
    }
}

