namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarNameScopeTreeWalker
    {
        [ThreadStatic]
        private static Locker updateScopeLocker;
        [ThreadStatic]
        private static HashSet<DependencyObject> changedElements;
        [ThreadStatic]
        private static Queue<List<DependencyObject>> processQueue;
        [ThreadStatic]
        private static Queue<Action> afterUpdateQueue;
        [ThreadStatic]
        private static DevExpress.Xpf.Bars.DescendentsWalkerHelper descendentsWalkerHelper;
        public static readonly DependencyProperty WalkerProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty PulseProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty InheritancePulseProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsSelfInheritanceParentProperty;
        [ThreadStatic]
        private static Func<Window, HwndSource> getTargetSource;

        static BarNameScopeTreeWalker();
        private static void ActOnWalkerChange(object element);
        private static void CheckKillAdornerLayer(DependencyObject root);
        public static void DoWhenUnlocked(Action action);
        public static void EnsureWalker(DependencyObject element);
        private static void EnsureWalker(DependencyObject element, DependencyObject root);
        public static DependencyObject GetInheritanceRoot(DependencyObject dObj);
        private static DependencyObject GetInheritanceRootFW40(DependencyObject dObj);
        private static DependencyObject GetInheritanceRootFW45(DependencyObject dObj);
        public static BarNameScopeTreeWalker GetWalker(DependencyObject obj);
        private static void OnPresentationSourceChanged(object sender, SourceChangedEventArgs e);
        protected static void OnPulsePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnUpdateScopeLockerUnlocked(object sender, EventArgs e);
        protected static void OnWalkerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void ProcessChangedElements();
        private static void ProcessChangedElementsLocked();
        public static void Pulse(object d);
        private static void SetIsSelfInheritanceParent(DependencyObject element);
        public static void SetWalker(DependencyObject obj, BarNameScopeTreeWalker value);
        private static bool ShouldIgnoreRoot(DependencyObject root);
        private static void SubscribePresentationSourceChanged(DependencyObject dObj);
        private static void UnsubscribePresentationSourceChanged(DependencyObject dObj);

        private static Locker UpdateScopeLocker { get; }

        private static HashSet<DependencyObject> ChangedElements { get; }

        private static Queue<List<DependencyObject>> ProcessQueue { get; }

        private static Queue<Action> AfterUpdateQueue { get; }

        private static DevExpress.Xpf.Bars.DescendentsWalkerHelper DescendentsWalkerHelper { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarNameScopeTreeWalker.<>c <>9;
            public static Action<Locker> <>9__6_0;
            public static Func<DependencyObject, bool> <>9__38_0;
            public static Action<DependencyObject> <>9__38_1;
            public static Action<DependencyObject> <>9__38_2;
            public static Action<DependencyObject> <>9__38_3;
            public static Func<BarNameScope, DependencyObject> <>9__41_0;

            static <>c();
            internal void <get_UpdateScopeLocker>b__6_0(Locker x);
            internal bool <GetInheritanceRootFW40>b__38_0(DependencyObject x);
            internal void <GetInheritanceRootFW40>b__38_1(DependencyObject x);
            internal void <GetInheritanceRootFW40>b__38_2(DependencyObject x);
            internal void <GetInheritanceRootFW40>b__38_3(DependencyObject x);
            internal DependencyObject <OnPresentationSourceChanged>b__41_0(BarNameScope x);
        }
    }
}

