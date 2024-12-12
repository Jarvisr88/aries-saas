namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Reflection;

    internal static class BamlHelper
    {
        public static readonly short UidType;
        public static readonly short BrushConverterType;
        public static readonly short ResourceDictionaryType;
        private static string defaultPrefix;
        private static string themeFullNamePrefix;
        private static MethodInfo getKnownPropertyMethod;

        static BamlHelper();
        private static void CreateGetKnownProperty();
        public static string GetKnownPropertyName(short id);
        public static bool IsResourceDictionary(short id);
        internal static void ResetThemePrefix();
        public static byte[] SerializeBrush(string brush);

        public static string ThemeFullNamePrefix { get; internal set; }
    }
}

