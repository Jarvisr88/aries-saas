namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class BarNameScope : IServiceProvider
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ScopeProperty;
        public static readonly DependencyProperty IsScopeOwnerProperty;
        public static readonly RoutedEvent ScopeChangedEvent;
        private static readonly List<DecoratorDataBase> decoratorDatas;
        private readonly Dictionary<object, ElementRegistrator> registrators;
        private readonly DependencyObject target;
        private readonly List<BarNameScope> children;
        private readonly Dictionary<Type, IBarNameScopeDecorator> decorators;
        private readonly bool isInBrokenTree;
        private BarNameScope parent;
        private BarNameScopeTreeWalker walker;
        private ScopeTreeNode tree;

        static BarNameScope();
        private BarNameScope(DependencyObject target, BarNameScope parent, BarNameScopeTreeWalker walker, bool isInBrokenTree);
        private static BarNameScope CreateInstance(DependencyObject root, BarNameScope parent, bool? brokenTreeElement);
        public static void CreateScope(DependencyObject root, BarNameScope parent);
        internal static void CreateScope(DependencyObject root, BarNameScope parent, bool? brokenTreeElement);
        private void Detach();
        private void Detach(BarNameScope child);
        public static void EnsureRegistrator();
        public static void EnsureRegistrator(DependencyObject element);
        [IteratorStateMachine(typeof(BarNameScope.<EnumerateKeysAndNames>d__55))]
        internal static IEnumerable<KeyValuePair<object, object>> EnumerateKeysAndNames(IBarNameScopeSupport element);
        internal static BarNameScope FindScope(DependencyObject obj);
        public static DependencyObject FindScopeTarget(DependencyObject d);
        public static DependencyObject FindScopeTarget(DependencyObject d, bool includeSelf);
        public static bool GetIsScopeOwner(DependencyObject obj);
        private static PresentationSource GetPresentationSourceEx(DependencyObject dObj);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal static BarNameScope GetScope(DependencyObject obj);
        public T GetService<T>();
        public static T GetService<T>(object target);
        private static object GetService(Type serviceType, BarNameScope scope);
        private static object GetService(Type serviceType, object target);
        public static bool IsInSameScope(object first, object second);
        private static bool IsRootVisual(Visual obj);
        public static bool IsScopeTarget(object obj);
        private static void OnIsScopeOwnerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnScopeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnWindowSizeChanged(object sender, RoutedEventArgs e);
        private static void RaiseScopeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void RegisterDecorator<TDecorator>(Func<TDecorator> createDecoratorCallback) where TDecorator: IBarNameScopeDecorator;
        public static void RegisterDecorator<TDecorator, TDecoratorService>(Func<TDecorator> createDecoratorCallback, Func<TDecorator, TDecoratorService> createServiceCallback, Func<TDecoratorService> getNullServiceCallback) where TDecorator: IBarNameScopeDecorator;
        public static void SetIsScopeOwner(DependencyObject obj, bool value);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal static void SetScope(DependencyObject obj, BarNameScope value);
        object IServiceProvider.GetService(Type serviceType);

        private Dictionary<object, ElementRegistrator> Registrators { get; }

        public ReadOnlyCollection<BarNameScope> Children { get; }

        public DependencyObject Target { get; }

        public ElementRegistrator this[object element] { get; }

        public BarNameScope Parent { get; }

        public ScopeTreeNode ScopeTree { get; }

        protected internal BarNameScopeTreeWalker Walker { get; }

        protected internal bool IsInBrokenTree { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarNameScope.<>c <>9;
            public static Func<BarNameScope, object, bool, ElementRegistrator> <>9__8_1;
            public static Func<DependencyObject, bool> <>9__41_0;
            public static Func<BarNameScope, DependencyObject> <>9__50_0;

            static <>c();
            internal RegistratorFactoryDecorator <.cctor>b__8_0();
            internal ElementRegistrator <.cctor>b__8_1(BarNameScope scope, object type, bool unique);
            internal IBarToContainerNameBinderService <.cctor>b__8_10();
            internal BarToContainerTypeBinder <.cctor>b__8_11();
            internal IBarToContainerTypeBinderService <.cctor>b__8_12(BarToContainerTypeBinder d);
            internal IBarToContainerTypeBinderService <.cctor>b__8_13();
            internal MergingElementBinder <.cctor>b__8_14();
            internal IMergingService <.cctor>b__8_15(MergingElementBinder d);
            internal IMergingService <.cctor>b__8_16();
            internal RadioGroupStrategy <.cctor>b__8_17();
            internal IRadioGroupService <.cctor>b__8_18(RadioGroupStrategy d);
            internal IRadioGroupService <.cctor>b__8_19();
            internal EventListenerDecorator <.cctor>b__8_2();
            internal ItemCommandSourceStrategy <.cctor>b__8_20();
            internal ICommandSourceService <.cctor>b__8_21(ItemCommandSourceStrategy d);
            internal ICommandSourceService <.cctor>b__8_22();
            internal CustomizationDecorator <.cctor>b__8_23();
            internal ICustomizationService <.cctor>b__8_24(CustomizationDecorator d);
            internal ICustomizationService <.cctor>b__8_25();
            internal ThemedWindowDecorator <.cctor>b__8_26();
            internal IThemedWindowService <.cctor>b__8_27(ThemedWindowDecorator d);
            internal IThemedWindowService <.cctor>b__8_28();
            internal IEventListenerDecoratorService <.cctor>b__8_3(EventListenerDecorator d);
            internal IEventListenerDecoratorService <.cctor>b__8_4();
            internal ItemToLinkBinder <.cctor>b__8_5();
            internal IItemToLinkBinderService <.cctor>b__8_6(ItemToLinkBinder d);
            internal IItemToLinkBinderService <.cctor>b__8_7();
            internal BarToContainerNameBinder <.cctor>b__8_8();
            internal IBarToContainerNameBinderService <.cctor>b__8_9(BarToContainerNameBinder d);
            internal DependencyObject <CreateInstance>b__50_0(BarNameScope x);
            internal bool <GetService>b__41_0(DependencyObject x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__38<TDecorator> where TDecorator: IBarNameScopeDecorator
        {
            public static readonly BarNameScope.<>c__38<TDecorator> <>9;
            public static Predicate<DecoratorDataBase> <>9__38_0;

            static <>c__38();
            internal bool <RegisterDecorator>b__38_0(DecoratorDataBase x);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__39<TDecorator, TDecoratorService> where TDecorator: IBarNameScopeDecorator
        {
            public static readonly BarNameScope.<>c__39<TDecorator, TDecoratorService> <>9;
            public static Predicate<DecoratorDataBase> <>9__39_0;

            static <>c__39();
            internal bool <RegisterDecorator>b__39_0(DecoratorDataBase x);
        }

        [CompilerGenerated]
        private sealed class <EnumerateKeysAndNames>d__55 : IEnumerable<KeyValuePair<object, object>>, IEnumerable, IEnumerator<KeyValuePair<object, object>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<object, object> <>2__current;
            private int <>l__initialThreadId;
            private IBarNameScopeSupport element;
            public IBarNameScopeSupport <>3__element;
            private IMultipleElementRegistratorSupport <ms>5__1;
            private IEnumerator<object> <>7__wrap1;

            [DebuggerHidden]
            public <EnumerateKeysAndNames>d__55(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<KeyValuePair<object, object>> IEnumerable<KeyValuePair<object, object>>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            KeyValuePair<object, object> IEnumerator<KeyValuePair<object, object>>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

