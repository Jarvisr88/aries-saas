namespace DevExpress.Data
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    public static class ShellHelper
    {
        private static void CommitShortcutProperties(IPropertyStore newShortcutProperties);
        private static string GetProgramsFolder();
        private static string GetRegistryKeyName(Type activatorType);
        private static string GetShortcutPath(string name);
        private static string GetSourcePath();
        private static void InstallShortcut(string shortcutPath, string applicationId, string iconPath);
        private static void InstallShortcut(string exePath, string shortcutPath, string applicationId, string iconPath);
        private static void InstallShortcut(string exePath, string shortcutPath, string applicationId, string iconPath, Type activatorType);
        public static bool IsApplicationShortcutExist(string name);
        private static string PatchExePath(string exePath);
        public static void RegisterApplicationActivator(Type activatorType);
        public static void RegisterComServer(Type activatorType);
        public static void RegisterComServer(string exePath, Type activatorType);
        private static void SaveShortcut(string shortcutPath, IShellLinkW newShortcut);
        private static void SetActivatorId(Type activatorType, IPropertyStore newShortcutProperties);
        private static void SetAppId(string applicationId, IPropertyStore newShortcutProperties);
        private static void SetArguments(IShellLinkW newShortcut);
        private static void SetExePath(string exePath, IShellLinkW newShortcut);
        private static void SetIconLocation(string iconPath, IShellLinkW newShortcut);
        private static void SetShortcutPropertiesValue(IPropertyStore newShortcutProperties, PropertyKey key, PropVariant pv);
        public static void TryCreateShortcut(string applicationId, string name, string iconPath = null);
        public static void TryCreateShortcut(string exePath, string applicationId, string name, string iconPath = null);
        public static void TryCreateShortcut(string applicationId, string name, string iconPath, Type activatorType);
        public static void TryCreateShortcut(string exePath, string applicationId, string name, string iconPath, Type activatorType);
        public static void TryRemoveShortcut(string name);
        public static void UnregisterApplicationActivator();
        public static void UnregisterComServer(Type activatorType);
    }
}

