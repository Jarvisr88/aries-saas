namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ReflectionExtensions
    {
        private static readonly PropertyInfo arrangeInProgress = typeof(UIElement).GetProperty("ArrangeInProgress", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly PropertyInfo isLogicalChildrenIterationInProgress = typeof(FrameworkElement).GetProperty("IsLogicalChildrenIterationInProgress", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly Func<DependencyObject, bool> isStyleUpdateInProgress;

        static ReflectionExtensions()
        {
            int? parametersCount = null;
            isStyleUpdateInProgress = ReflectionHelper.CreateInstanceMethodHandler<FrameworkElement, Func<DependencyObject, bool>>(null, "get_IsStyleUpdateInProgress", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
        }

        internal static bool GetArrangeInProgress(this UIElement element) => 
            (arrangeInProgress != null) && ((bool) arrangeInProgress.GetValue(element, null));

        internal static bool GetIsLogicalChildrenIterationInProgress(this FrameworkElement element) => 
            (isLogicalChildrenIterationInProgress != null) && ((bool) isLogicalChildrenIterationInProgress.GetValue(element, null));

        internal static bool GetIsStyleUpdateInProgress(this FrameworkElement element) => 
            (isStyleUpdateInProgress == null) ? false : isStyleUpdateInProgress(element);
    }
}

