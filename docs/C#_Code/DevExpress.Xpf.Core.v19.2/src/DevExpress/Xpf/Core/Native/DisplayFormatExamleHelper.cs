namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Localization;
    using System;

    public static class DisplayFormatExamleHelper
    {
        private static XtraLocalizer<EditorStringId> localizer;

        static DisplayFormatExamleHelper();
        public static object GetExample(Type type);
        public static TypeCategory GetTypeCategory(Type type);
    }
}

