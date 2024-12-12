namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DXTriggerManager : DependencyObject
    {
        public static readonly DependencyProperty TriggersProperty;
        public static readonly DependencyProperty TriggersInfoProperty;

        static DXTriggerManager();
        internal static FrameworkElement GetTemplatedParent(UIElement d);
        public static DXTriggerCollection GetTriggers(DependencyObject obj);
        public static DXTriggerInfoCollection GetTriggersInfo(DependencyObject obj);
        private static void OnTriggersInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetTriggers(DependencyObject obj, DXTriggerCollection value);
        public static void SetTriggersInfo(DependencyObject obj, Collection<DXTriggerInfoBase> value);

        internal static int InitializedTriggersCount { get; set; }
    }
}

