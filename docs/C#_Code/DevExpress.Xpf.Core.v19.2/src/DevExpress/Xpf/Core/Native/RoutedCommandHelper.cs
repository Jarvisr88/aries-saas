namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class RoutedCommandHelper
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CommandProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CommandParameterProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CommandTargetProperty;

        static RoutedCommandHelper();
        public static object GetCommand(DependencyObject d);
        public static object GetCommandParameter(DependencyObject d);
        public static object GetCommandTarget(DependencyObject d);
        private static void PropertyChangedCommand(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void PropertyChangedCommandParameter(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void PropertyChangedCommandTarget(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetCommand(DependencyObject d, ICommandProvider value);
        public static void SetCommandParameter(DependencyObject d, object value);
        public static void SetCommandTarget(DependencyObject d, object value);
    }
}

