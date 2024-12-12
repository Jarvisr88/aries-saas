namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class TabbedDocumentUIServiceBase : DocumentUIServiceBase
    {
        public static readonly DependencyProperty TargetProperty;
        public static readonly DependencyProperty ActiveDocumentProperty;
        public static readonly DependencyProperty ShowNewItemOnStartupProperty;
        public static readonly DependencyProperty NewItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        private DXTabControl currentTarget;
        private bool isInitialized;
        private bool lockTabSelectionChanged;
        private bool lockActiveDocumentChanged;

        public event ActiveDocumentChangedEventHandler ActiveDocumentChanged;

        static TabbedDocumentUIServiceBase();
        protected TabbedDocumentUIServiceBase();
        protected DXTabItem CreateNewTabItem();
        protected DXTabItem CreateTabItem();
        protected virtual void Initialize();
        protected virtual void OnActiveDocumentChanged(IDocument oldValue, IDocument newValue);
        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e);
        protected override void OnAttached();
        private void OnCurrentTargetChanged(DXTabControl oldValue, DXTabControl newValue);
        protected override void OnDetaching();
        protected virtual void OnTabbedWindowActivated(object sender, EventArgs e);
        protected virtual void OnTabbedWindowClosed(object sender, EventArgs e);
        protected virtual void OnTabbedWindowClosing(object sender, CancelEventArgs e);
        protected virtual void OnTabbedWindowShown(object sender, EventArgs e);
        protected virtual void OnTabControlDeserialized(DXTabControl tabControl);
        private void OnTabControlLayoutUpdated(object sender, EventArgs e);
        private void OnTabControlSelectionChanged(object sender, int oldSelectedIndex, int newSelectedIndex);
        protected virtual void OnTabControlTabAdding(object sender, TabControlTabAddingEventArgs e);
        protected virtual void OnTabControlTabHidden(object sender, TabControlTabHiddenEventArgs e);
        protected virtual void OnTabControlTabHiding(object sender, TabControlTabHidingEventArgs e);
        protected virtual void OnTabControlTabRemoved(object sender, TabControlTabRemovedEventArgs e);
        protected virtual void OnTabControlTabRemoving(object sender, TabControlTabRemovingEventArgs e);
        protected virtual void OnTabControlTabShowing(object sender, TabControlTabShowingEventArgs e);
        protected virtual void OnTabControlTabShown(object sender, TabControlTabShownEventArgs e);
        private void OnTargetChanged(DXTabControl oldValue, DXTabControl newValue);
        private void SetCurrentTarget(Window w);
        private void SubscribeTabControl(DXTabControl tabControl);
        private void SubscribeWindow(Window window);

        public DXTabControl Target { get; set; }

        public IDocument ActiveDocument { get; set; }

        public bool ShowNewItemOnStartup { get; set; }

        public DataTemplate NewItemTemplate { get; set; }

        public DataTemplate ItemTemplate { get; set; }

        protected DXTabControl ActualTarget { get; }

        protected DXTabControl CurrentTarget { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabbedDocumentUIServiceBase.<>c <>9;
            public static Func<ContentControl, object> <>9__29_0;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlTabHidingEventArgs> <>9__33_0;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHidingEventArgs, TabControlTabHidingEventHandler>, object> <>9__33_1;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHidingEventArgs, TabControlTabHidingEventHandler>, TabControlTabHidingEventHandler> <>9__33_2;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlTabHiddenEventArgs> <>9__33_3;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHiddenEventArgs, TabControlTabHiddenEventHandler>, object> <>9__33_4;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHiddenEventArgs, TabControlTabHiddenEventHandler>, TabControlTabHiddenEventHandler> <>9__33_5;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlTabShowingEventArgs> <>9__33_6;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShowingEventArgs, TabControlTabShowingEventHandler>, object> <>9__33_7;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShowingEventArgs, TabControlTabShowingEventHandler>, TabControlTabShowingEventHandler> <>9__33_8;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlTabShownEventArgs> <>9__33_9;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShownEventArgs, TabControlTabShownEventHandler>, object> <>9__33_10;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShownEventArgs, TabControlTabShownEventHandler>, TabControlTabShownEventHandler> <>9__33_11;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlTabRemovingEventArgs> <>9__33_12;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovingEventArgs, TabControlTabRemovingEventHandler>, object> <>9__33_13;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovingEventArgs, TabControlTabRemovingEventHandler>, TabControlTabRemovingEventHandler> <>9__33_14;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlTabRemovedEventArgs> <>9__33_15;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovedEventArgs, TabControlTabRemovedEventHandler>, object> <>9__33_16;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovedEventArgs, TabControlTabRemovedEventHandler>, TabControlTabRemovedEventHandler> <>9__33_17;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlSelectionChangedEventArgs> <>9__33_18;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlSelectionChangedEventArgs, TabControlSelectionChangedEventHandler>, object> <>9__33_19;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlSelectionChangedEventArgs, TabControlSelectionChangedEventHandler>, TabControlSelectionChangedEventHandler> <>9__33_20;
            public static Action<TabbedDocumentUIServiceBase, object, TabControlTabAddingEventArgs> <>9__33_21;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabAddingEventArgs, TabControlTabAddingEventHandler>, object> <>9__33_22;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabAddingEventArgs, TabControlTabAddingEventHandler>, TabControlTabAddingEventHandler> <>9__33_23;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlNewTabbedWindowEventArgs, TabControlNewTabbedWindowEventHandler>, object> <>9__33_25;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, TabControlNewTabbedWindowEventArgs, TabControlNewTabbedWindowEventHandler>, TabControlNewTabbedWindowEventHandler> <>9__33_26;
            public static Action<TabbedDocumentUIServiceBase, object, XtraPropertyInfoEventArgs> <>9__33_27;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, XtraPropertyInfoEventArgs, XtraPropertyInfoEventHandler>, object> <>9__33_28;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, XtraPropertyInfoEventArgs, XtraPropertyInfoEventHandler>, XtraPropertyInfoEventHandler> <>9__33_29;
            public static Action<TabbedDocumentUIServiceBase, object, EventArgs> <>9__34_0;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler>, object> <>9__34_1;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler>, EventHandler> <>9__34_2;
            public static Action<TabbedDocumentUIServiceBase, object, CancelEventArgs> <>9__34_3;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, CancelEventArgs, CancelEventHandler>, object> <>9__34_4;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, CancelEventArgs, CancelEventHandler>, CancelEventHandler> <>9__34_5;
            public static Action<TabbedDocumentUIServiceBase, object, EventArgs> <>9__34_6;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler>, object> <>9__34_7;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler>, EventHandler> <>9__34_8;
            public static Action<TabbedDocumentUIServiceBase, object, RoutedEventArgs> <>9__34_9;
            public static Action<WeakEventHandler<TabbedDocumentUIServiceBase, RoutedEventArgs, RoutedEventHandler>, object> <>9__34_10;
            public static Func<WeakEventHandler<TabbedDocumentUIServiceBase, RoutedEventArgs, RoutedEventHandler>, RoutedEventHandler> <>9__34_11;
            public static Action<IDocument> <>9__40_0;

            static <>c();
            internal void <.cctor>b__59_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__59_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal object <CreateNewTabItem>b__29_0(ContentControl x);
            internal void <OnActiveDocumentChanged>b__40_0(IDocument x);
            internal void <SubscribeTabControl>b__33_0(TabbedDocumentUIServiceBase owner, object sender, TabControlTabHidingEventArgs args);
            internal void <SubscribeTabControl>b__33_1(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHidingEventArgs, TabControlTabHidingEventHandler> wh, object o);
            internal void <SubscribeTabControl>b__33_10(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShownEventArgs, TabControlTabShownEventHandler> wh, object o);
            internal TabControlTabShownEventHandler <SubscribeTabControl>b__33_11(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShownEventArgs, TabControlTabShownEventHandler> wh);
            internal void <SubscribeTabControl>b__33_12(TabbedDocumentUIServiceBase owner, object sender, TabControlTabRemovingEventArgs args);
            internal void <SubscribeTabControl>b__33_13(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovingEventArgs, TabControlTabRemovingEventHandler> wh, object o);
            internal TabControlTabRemovingEventHandler <SubscribeTabControl>b__33_14(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovingEventArgs, TabControlTabRemovingEventHandler> wh);
            internal void <SubscribeTabControl>b__33_15(TabbedDocumentUIServiceBase owner, object sender, TabControlTabRemovedEventArgs args);
            internal void <SubscribeTabControl>b__33_16(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovedEventArgs, TabControlTabRemovedEventHandler> wh, object o);
            internal TabControlTabRemovedEventHandler <SubscribeTabControl>b__33_17(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabRemovedEventArgs, TabControlTabRemovedEventHandler> wh);
            internal void <SubscribeTabControl>b__33_18(TabbedDocumentUIServiceBase owner, object sender, TabControlSelectionChangedEventArgs args);
            internal void <SubscribeTabControl>b__33_19(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlSelectionChangedEventArgs, TabControlSelectionChangedEventHandler> wh, object o);
            internal TabControlTabHidingEventHandler <SubscribeTabControl>b__33_2(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHidingEventArgs, TabControlTabHidingEventHandler> wh);
            internal TabControlSelectionChangedEventHandler <SubscribeTabControl>b__33_20(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlSelectionChangedEventArgs, TabControlSelectionChangedEventHandler> wh);
            internal void <SubscribeTabControl>b__33_21(TabbedDocumentUIServiceBase owner, object sender, TabControlTabAddingEventArgs args);
            internal void <SubscribeTabControl>b__33_22(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabAddingEventArgs, TabControlTabAddingEventHandler> wh, object o);
            internal TabControlTabAddingEventHandler <SubscribeTabControl>b__33_23(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabAddingEventArgs, TabControlTabAddingEventHandler> wh);
            internal void <SubscribeTabControl>b__33_25(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlNewTabbedWindowEventArgs, TabControlNewTabbedWindowEventHandler> wh, object o);
            internal TabControlNewTabbedWindowEventHandler <SubscribeTabControl>b__33_26(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlNewTabbedWindowEventArgs, TabControlNewTabbedWindowEventHandler> wh);
            internal void <SubscribeTabControl>b__33_27(TabbedDocumentUIServiceBase owner, object sender, XtraPropertyInfoEventArgs args);
            internal void <SubscribeTabControl>b__33_28(WeakEventHandler<TabbedDocumentUIServiceBase, XtraPropertyInfoEventArgs, XtraPropertyInfoEventHandler> wh, object o);
            internal XtraPropertyInfoEventHandler <SubscribeTabControl>b__33_29(WeakEventHandler<TabbedDocumentUIServiceBase, XtraPropertyInfoEventArgs, XtraPropertyInfoEventHandler> wh);
            internal void <SubscribeTabControl>b__33_3(TabbedDocumentUIServiceBase owner, object sender, TabControlTabHiddenEventArgs args);
            internal void <SubscribeTabControl>b__33_4(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHiddenEventArgs, TabControlTabHiddenEventHandler> wh, object o);
            internal TabControlTabHiddenEventHandler <SubscribeTabControl>b__33_5(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabHiddenEventArgs, TabControlTabHiddenEventHandler> wh);
            internal void <SubscribeTabControl>b__33_6(TabbedDocumentUIServiceBase owner, object sender, TabControlTabShowingEventArgs args);
            internal void <SubscribeTabControl>b__33_7(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShowingEventArgs, TabControlTabShowingEventHandler> wh, object o);
            internal TabControlTabShowingEventHandler <SubscribeTabControl>b__33_8(WeakEventHandler<TabbedDocumentUIServiceBase, TabControlTabShowingEventArgs, TabControlTabShowingEventHandler> wh);
            internal void <SubscribeTabControl>b__33_9(TabbedDocumentUIServiceBase owner, object sender, TabControlTabShownEventArgs args);
            internal void <SubscribeWindow>b__34_0(TabbedDocumentUIServiceBase owner, object sender, EventArgs args);
            internal void <SubscribeWindow>b__34_1(WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler> wh, object o);
            internal void <SubscribeWindow>b__34_10(WeakEventHandler<TabbedDocumentUIServiceBase, RoutedEventArgs, RoutedEventHandler> wh, object o);
            internal RoutedEventHandler <SubscribeWindow>b__34_11(WeakEventHandler<TabbedDocumentUIServiceBase, RoutedEventArgs, RoutedEventHandler> wh);
            internal EventHandler <SubscribeWindow>b__34_2(WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler> wh);
            internal void <SubscribeWindow>b__34_3(TabbedDocumentUIServiceBase owner, object sender, CancelEventArgs args);
            internal void <SubscribeWindow>b__34_4(WeakEventHandler<TabbedDocumentUIServiceBase, CancelEventArgs, CancelEventHandler> wh, object o);
            internal CancelEventHandler <SubscribeWindow>b__34_5(WeakEventHandler<TabbedDocumentUIServiceBase, CancelEventArgs, CancelEventHandler> wh);
            internal void <SubscribeWindow>b__34_6(TabbedDocumentUIServiceBase owner, object sender, EventArgs args);
            internal void <SubscribeWindow>b__34_7(WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler> wh, object o);
            internal EventHandler <SubscribeWindow>b__34_8(WeakEventHandler<TabbedDocumentUIServiceBase, EventArgs, EventHandler> wh);
            internal void <SubscribeWindow>b__34_9(TabbedDocumentUIServiceBase owner, object sender, RoutedEventArgs args);
        }
    }
}

