namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CollectionAction : BarManagerControllerActionBase, ICollectionAction
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty KindProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IndexProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ContainerNameProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ElementNameProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ContainerProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ElementProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CollectionTagProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ContextProperty;
        private CollectionActionWrapper wrapper;
        private readonly Locker wrapperInitializationLocker;

        static CollectionAction();
        public CollectionAction();
        private CollectionActionWrapper CreateWrapper();
        protected override void ExecuteCore(DependencyObject context);
        public static object GetCollectionTag(DependencyObject obj);
        public static object GetContainer(DependencyObject obj);
        public static string GetContainerName(DependencyObject obj);
        public static DependencyObject GetContext(DependencyObject obj);
        public static object GetElement(DependencyObject obj);
        public static string GetElementName(DependencyObject obj);
        public static int GetIndex(DependencyObject obj);
        public static CollectionActionKind GetKind(DependencyObject obj);
        public override object GetObjectCore();
        public static IDisposable InitializeContext(DependencyObject obj, DependencyObject value);
        private void OnChanged();
        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetCollectionTag(DependencyObject obj, object value);
        public static void SetContainer(DependencyObject obj, object value);
        public static void SetContainerName(DependencyObject obj, string value);
        public static void SetContext(DependencyObject obj, DependencyObject value);
        public static void SetElement(DependencyObject obj, object value);
        public static void SetElementName(DependencyObject obj, string value);
        public static void SetIndex(DependencyObject obj, int value);
        public static void SetKind(DependencyObject obj, CollectionActionKind value);

        public object Element { get; set; }

        public string ElementName { get; set; }

        public object Container { get; set; }

        public string ContainerName { get; set; }

        public int Index { get; set; }

        public CollectionActionKind Kind { get; set; }

        public object CollectionTag { get; set; }

        private CollectionActionWrapper Wrapper { get; }

        private CollectionActionWrapper UnlockedWrapper { get; }

        CollectionActionKind ICollectionAction.Kind { get; }

        object ICollectionAction.Element { get; }

        IList ICollectionAction.Collection { get; }

        int ICollectionAction.Index { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionAction.<>c <>9;
            public static Func<CollectionActionWrapper, CollectionActionKind> <>9__57_0;
            public static Func<CollectionActionKind> <>9__57_1;
            public static Func<CollectionActionWrapper, object> <>9__59_0;
            public static Func<object> <>9__59_1;
            public static Func<CollectionActionWrapper, IList> <>9__61_0;
            public static Func<IList> <>9__61_1;
            public static Func<CollectionActionWrapper, int> <>9__63_0;
            public static Func<int> <>9__63_1;
            public static Func<CollectionActionWrapper, object> <>9__64_0;
            public static Func<object> <>9__64_1;

            static <>c();
            internal IList <DevExpress.Xpf.Bars.ICollectionAction.get_Collection>b__61_0(CollectionActionWrapper x);
            internal IList <DevExpress.Xpf.Bars.ICollectionAction.get_Collection>b__61_1();
            internal object <DevExpress.Xpf.Bars.ICollectionAction.get_Element>b__59_0(CollectionActionWrapper x);
            internal object <DevExpress.Xpf.Bars.ICollectionAction.get_Element>b__59_1();
            internal int <DevExpress.Xpf.Bars.ICollectionAction.get_Index>b__63_0(CollectionActionWrapper x);
            internal int <DevExpress.Xpf.Bars.ICollectionAction.get_Index>b__63_1();
            internal CollectionActionKind <DevExpress.Xpf.Bars.ICollectionAction.get_Kind>b__57_0(CollectionActionWrapper x);
            internal CollectionActionKind <DevExpress.Xpf.Bars.ICollectionAction.get_Kind>b__57_1();
            internal object <GetObjectCore>b__64_0(CollectionActionWrapper x);
            internal object <GetObjectCore>b__64_1();
        }

        private class ContextData : IDisposable
        {
            private DependencyObject dObj;

            public ContextData(DependencyObject dObj, DependencyObject value);
            void IDisposable.Dispose();
        }
    }
}

