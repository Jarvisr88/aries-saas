namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class CollectionActionHelper
    {
        public static readonly DependencyProperty ActionExecutedProperty;
        public static readonly DependencyProperty ExtraItemsProperty;
        public static readonly DependencyProperty CollectionProperty;
        private static CollectionActionHelper instance;
        private readonly List<CollectionActionHelper.CollectionGetterData> collectionGetters;
        private readonly List<CollectionActionHelper.DefaultContainerGetterData> containerGetters;
        private readonly List<CollectionActionHelper.ParentGetterData> parentGetters;

        static CollectionActionHelper();
        private CollectionActionHelper();
        public static bool CanRemoveElement(object element);
        public static void Execute(IBarManagerControllerAction action);
        public static void Execute(ICollectionAction action);
        public object FindElementByName(DependencyObject context, string elementName, IActionContainer controller, ScopeSearchSettings searchSettings, Func<IFrameworkInputElement, bool> predicate = null);
        private object FindElementByNameCore(object node, string elementName, ScopeSearchSettings searchSettings, Func<IFrameworkInputElement, bool> predicate);
        public static bool GetActionExecuted(DependencyObject obj);
        public static WeakReference GetCollection(DependencyObject obj);
        public IList GetCollectionForElement(object container, object element, IBarManagerControllerAction action);
        public IList GetCollectionForElement<TContainer, TElement>(object container, object element, IBarManagerControllerAction action);
        public IList GetCollectionForType(Type containerType, Type elementType, object container, object element, IBarManagerControllerAction action);
        public object GetDefaultContainer(object obj, IBarManagerControllerAction action, DependencyObject context);
        private object GetDefaultContainerImpl(object obj, IBarManagerControllerAction action, DependencyObject context);
        public static IEnumerable GetExtraItems(DependencyObject obj);
        private DependencyObject GetParent(object obj);
        private static bool IsValidIndex(IList list, int index);
        public void RegisterCollectionGetter<TContainer, TElement>(Func<IBarManagerControllerAction, object, object, IList> getter);
        public void RegisterDefaultContainerGetter<TElement>(Func<IBarManagerControllerAction, object, object> getter);
        public void RegisterDefaultContainerGetter<TElement, TContainer>(Func<IBarManagerControllerAction, object, DependencyObject, object> getter);
        public void RegisterParentGetter<TElement>(Func<TElement, DependencyObject> getter);
        private static void Revert(ICollectionAction action);
        private static void SaveCollection(ICollectionAction action);
        public static void SetActionExecuted(DependencyObject obj, bool value);
        public static void SetCollection(DependencyObject obj, WeakReference value);
        public static void SetExtraItems(DependencyObject obj, IEnumerable value);
        public static void SyncContainerNameProperty(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SyncContainerProperty(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SyncElementNameProperty(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SyncElementProperty(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SyncIndexProperty(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SyncKindProperty(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void SyncProperty(DependencyObject d, DependencyPropertyChangedEventArgs e, DependencyProperty dependencyProperty);

        public static CollectionActionHelper Instance { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionActionHelper.<>c <>9;
            public static Func<IBarItem, DependencyObject> <>9__18_0;
            public static Func<IBarManagerControllerAction, object, DependencyObject, object> <>9__18_1;
            public static Func<IBarManagerControllerAction, object, DependencyObject, object> <>9__18_2;
            public static Func<IBarManagerControllerAction, object, DependencyObject, object> <>9__18_3;
            public static Func<IBarManagerControllerAction, object, DependencyObject, object> <>9__18_4;
            public static Func<IBarManagerControllerAction, object, object> <>9__18_5;
            public static Func<IBarManagerControllerAction, object, object> <>9__18_6;
            public static Func<IBarManagerControllerAction, object, object, IList> <>9__18_7;
            public static Func<IBarManagerControllerAction, object, object, IList> <>9__18_8;
            public static Func<IBarManagerControllerAction, object, object, IList> <>9__18_9;
            public static Func<IBarManagerControllerAction, object, object, IList> <>9__18_10;
            public static Func<IBarManagerControllerAction, object, object, IList> <>9__18_11;
            public static Func<DependencyObject, WeakReference> <>9__22_0;
            public static Func<WeakReference, bool> <>9__22_1;
            public static Func<IControllerAction, bool> <>9__24_0;
            public static Func<IControllerAction, bool> <>9__24_1;
            public static Func<bool> <>9__24_2;
            public static Func<IEnumerable, IEnumerable<IFrameworkInputElement>> <>9__30_0;
            public static Func<object, Type> <>9__35_0;
            public static Func<object, Type> <>9__35_1;

            static <>c();
            internal DependencyObject <.ctor>b__18_0(IBarItem x);
            internal object <.ctor>b__18_1(IBarManagerControllerAction action, object _object, DependencyObject context);
            internal IList <.ctor>b__18_10(IBarManagerControllerAction action, object tbcontrol, object iBarItem);
            internal IList <.ctor>b__18_11(IBarManagerControllerAction action, object bContainer, object bar);
            internal object <.ctor>b__18_2(IBarManagerControllerAction action, object _object, DependencyObject context);
            internal object <.ctor>b__18_3(IBarManagerControllerAction action, object _object, DependencyObject context);
            internal object <.ctor>b__18_4(IBarManagerControllerAction action, object _object, DependencyObject context);
            internal object <.ctor>b__18_5(IBarManagerControllerAction action, object bar);
            internal object <.ctor>b__18_6(IBarManagerControllerAction action, object bar);
            internal IList <.ctor>b__18_7(IBarManagerControllerAction action, object manager, object bar);
            internal IList <.ctor>b__18_8(IBarManagerControllerAction action, object manager, object barItem);
            internal IList <.ctor>b__18_9(IBarManagerControllerAction action, object holder, object iBarItem);
            internal bool <CanRemoveElement>b__24_0(IControllerAction x);
            internal bool <CanRemoveElement>b__24_1(IControllerAction x);
            internal bool <CanRemoveElement>b__24_2();
            internal IEnumerable<IFrameworkInputElement> <FindElementByNameCore>b__30_0(IEnumerable x);
            internal Type <GetCollectionForElement>b__35_0(object x);
            internal Type <GetCollectionForElement>b__35_1(object x);
            internal WeakReference <Revert>b__22_0(DependencyObject x);
            internal bool <Revert>b__22_1(WeakReference x);
        }

        private class CollectionGetterData
        {
            private CollectionGetterData();
            public static CollectionActionHelper.CollectionGetterData Create<TContainer, TElement>(Func<IBarManagerControllerAction, object, object, IList> getter);

            public Type ContainerType { get; private set; }

            public Type ElementType { get; private set; }

            public Func<IBarManagerControllerAction, object, object, IList> Getter { get; private set; }
        }

        private class DefaultContainerGetterData
        {
            private DefaultContainerGetterData();
            public static CollectionActionHelper.DefaultContainerGetterData Create<TElement, TContainer>(Func<IBarManagerControllerAction, object, DependencyObject, object> getter);

            public Type ElementType { get; private set; }

            public Type ContainerType { get; private set; }

            public Func<IBarManagerControllerAction, object, DependencyObject, object> Getter { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c__13<TElement, TContainer>
            {
                public static readonly CollectionActionHelper.DefaultContainerGetterData.<>c__13<TElement, TContainer> <>9;
                public static Func<Type, bool> <>9__13_0;
                public static Func<Type, Type> <>9__13_1;
                public static Func<Type> <>9__13_2;

                static <>c__13();
                internal bool <Create>b__13_0(Type x);
                internal Type <Create>b__13_1(Type x);
                internal Type <Create>b__13_2();
            }
        }

        private class ParentGetterData
        {
            public ParentGetterData(Type elementType, Func<object, DependencyObject> getter);

            public Type ElementType { get; private set; }

            public Func<object, DependencyObject> Getter { get; private set; }
        }
    }
}

