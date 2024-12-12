namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    internal class QualifierListener : DependencyObject
    {
        private DependencyObject target;
        private PostponedAction resetAction;
        private static volatile bool initialized;
        private static object olock;
        private static List<IBaseUriQualifier> initializedQualifiers;
        private static Dictionary<string, DependencyProperty> dProps;
        private static WeakList<QualifierListener> eventListeners;

        static QualifierListener();
        private QualifierListener(DependencyObject target);
        public static Binding CreateBinding(IServiceProvider serviceProvider, Uri uri, Func<ICollection<UriInfo>> uriCandidates);
        internal static QualifierListener CreateInstance(object element, Uri uri, Func<ICollection<UriInfo>> uriCandidates);
        private static void InitializeDPropNotification(IBindableUriQualifier qualifier);
        private static void InitializeEventNotification(IUriQualifier qualifier);
        private static void InitializeProperties();
        private void InvokeReset();
        private static void LockedInitialize();
        private static void OnEventQualifierActiveValueChanged(object sender, EventArgs eventArgs);
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e);
        private void ResetForce();
        internal static void ResetInitialization();
        internal static void ResetListeners();
        private void ResetPostponed();
        private void SubscribeNotifications(IEnumerable<UriQualifierValue> enumerable);

        public DependencyObject Target { get; }

        private static WeakList<QualifierListener> EventListeners { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly QualifierListener.<>c <>9;
            public static Func<bool> <>9__4_0;
            public static Func<UriInfo, IEnumerable<UriQualifierValue>> <>9__6_0;

            static <>c();
            internal bool <.ctor>b__4_0();
            internal IEnumerable<UriQualifierValue> <CreateInstance>b__6_0(UriInfo x);
        }
    }
}

