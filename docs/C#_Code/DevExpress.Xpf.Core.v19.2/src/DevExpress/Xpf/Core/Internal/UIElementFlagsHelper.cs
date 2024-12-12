namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Reflection;
    using System.Windows;

    public static class UIElementFlagsHelper
    {
        private static readonly Func<UIElement, UIElementFlags, bool> readFlag;
        private static readonly Action<UIElement, UIElementFlags, bool> writeFlag;

        static UIElementFlagsHelper()
        {
            int? parametersCount = null;
            readFlag = ReflectionHelper.CreateInstanceMethodHandler<UIElement, Func<UIElement, UIElementFlags, bool>>(null, "ReadFlag", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
            parametersCount = null;
            writeFlag = ReflectionHelper.CreateInstanceMethodHandler<UIElement, Action<UIElement, UIElementFlags, bool>>(null, "WriteFlag", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
        }

        public static bool ReadFlag(UIElement instance, UIElementFlags field) => 
            readFlag(instance, field);

        public static void WriteFlag(UIElement instance, UIElementFlags field, bool value)
        {
            writeFlag(instance, field, value);
        }
    }
}

