namespace DevExpress.Data.Linq.Helpers
{
    using System;

    internal static class DataAccessTypeValidationHelper
    {
        private static bool IsAppSettingsOrAppResources(Type type);
        private static bool IsCompilerGenerated(Type type);
        private static bool IsNestedNotPublic(Type type);
        private static bool IsRelatedToAttributes(Type type);
        private static bool IsRelatedToEvents(Type type);
        private static bool IsRelatedToUI(Type type);
        private static bool IsStatic(Type type);
        public static bool IsValidForDataAccessComponents(Type type);
    }
}

