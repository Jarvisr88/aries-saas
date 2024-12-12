namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;

    [DXToolboxBrowsable, ContentProperty("Bars"), ToolboxTabName("DX.19.2: Navigation & Layout")]
    public class BarContainerControl : BarItemsControl, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement
    {
        public static readonly DependencyProperty BarVertIndentProperty;
        public static readonly DependencyProperty BarHorzIndentProperty;
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty DrawBorderProperty;
        internal static readonly DependencyPropertyKey IsFloatingPropertyKey;
        public static readonly DependencyProperty IsFloatingProperty;
        public static readonly DependencyProperty ContainerTypeProperty;
        internal static readonly DependencyPropertyKey ActualPaddingPropertyKey;
        public static readonly DependencyProperty ActualPaddingProperty;
        protected static readonly DependencyPropertyKey BarsPropertyKey;
        public static readonly DependencyProperty BarsProperty;
        public static readonly DependencyProperty BarItemDisplayModeProperty;
        private ObservableCollection<IBar> barsCore;
        private BarContainerControlPanel clientPanel;
        private readonly List<IBar> barsInLogicalChildren;

        static BarContainerControl();
        public BarContainerControl();
        private void BarContainerControl_DragEvent(object sender, DragEventArgs e);
        protected virtual void ClearBarControl(Bar bar);
        protected override void ClearContainerForItemOverride(DependencyObject element, object item);
        protected virtual void CreateBars();
        protected virtual void CreateBindedBars();
        protected virtual void CreateItemsSource();
        object IMultipleElementRegistratorSupport.GetName(object registratorKey);
        protected override DependencyObject GetContainerForItemOverride();
        protected internal virtual Bar GetFirstNonEmptyBar(bool skipStatusBar);
        private ToolBarControlBase GetToolBar(IBar bar);
        private bool HasToolBar(IBar bar);
        protected virtual void InitializeClientPanel();
        protected override bool IsItemItsOwnContainerOverride(object item);
        protected bool IsPointInControl(Control c, MouseButtonEventArgs e);
        public void Link(IBar bar);
        public override void OnApplyTemplate();
        protected virtual void OnBarItemDisplayModeChanged(DevExpress.Xpf.Bars.BarItemDisplayMode oldValue);
        protected virtual void OnBarsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnBindedBarsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnClientPanelChanged();
        protected virtual void OnContainerTypeChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnContainerTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected override AutomationPeer OnCreateAutomationPeer();
        protected virtual void OnDrawBorderChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnDrawBorderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected static object OnIndentCoerce(DependencyObject d, object baseValue);
        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsFloatingChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnIsFloatingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemsPresenterLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e);
        protected virtual void OnNameChanged(string oldValue, string newValue);
        protected virtual void OnOrientationChanged(DependencyPropertyChangedEventArgs e);
        protected virtual object OnOrientationCoerce(object baseValue);
        protected static void OnOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected static object OnOrientationPropertyCoerce(DependencyObject d, object baseValue);
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item);
        protected internal void Refresh();
        protected virtual bool ShouldProcessDoubleClick(MouseButtonEventArgs e);
        public void Unlink(IBar bar);
        protected virtual void UpdateBorderVisualState();
        protected virtual void UpdatePaddings();

        public ObservableCollection<IBar> Bars { get; }

        public DevExpress.Xpf.Bars.BarItemDisplayMode BarItemDisplayMode { get; set; }

        protected internal ObservableCollection<IBar> BindedBars { get; }

        protected IEnumerable ActualSource { get; set; }

        [Obsolete("Use BarManager.Bars property instead.")]
        public ItemCollection Items { get; }

        protected internal bool ShouldRestoreOnActivate { get; set; }

        protected internal BarContainerControlPanel ClientPanel { get; set; }

        protected internal FloatingBarPopup OwnerPopup { get; }

        [Description("Gets or sets the type of the current container.This is a dependency property.")]
        public BarContainerType ContainerType { get; set; }

        [Description("Gets or sets the vertical distance between adjacent bars.This is a dependency property.")]
        public double BarVertIndent { get; set; }

        [Description("Gets or sets the horizontal distance between adjacent bars.This is a dependency property.")]
        public double BarHorzIndent { get; set; }

        [Description("Gets or sets the orientation of the current BarContainerControl object. This property is in effect when the BarContainerControl.ContainerType is set to BarContainerType.None.This is a dependency property.")]
        public System.Windows.Controls.Orientation Orientation { get; set; }

        [Description("Gets or sets whether a border is drawn for the BarContainerControl.This is a dependency property.")]
        public bool DrawBorder { get; set; }

        [Description("Gets whether the container is floating.This is a dependency property.")]
        public bool IsFloating { get; internal set; }

        [Description("Gets or sets the amount of space between the control's borders and its contents.This is a dependency property.")]
        public Thickness ActualPadding { get; internal set; }

        [Description("Gets the BarManager that owns the current BarContainerControl object.")]
        public BarManager Manager { get; }

        protected override IEnumerator LogicalChildren { get; }

        protected internal ContentControl Border { get; private set; }

        protected internal ContentControl BackControl { get; private set; }

        IEnumerable<object> IMultipleElementRegistratorSupport.RegistratorKeys { [IteratorStateMachine(typeof(BarContainerControl.<DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__115))] get; }

        protected internal virtual bool CanBind { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarContainerControl.<>c <>9;
            public static Action<BarContainerControl> <>9__14_2;
            public static Func<object, IBar> <>9__37_0;
            public static Action<ListCollectionView> <>9__38_0;
            public static Action<BarControl> <>9__39_0;
            public static Action<BarControl> <>9__39_1;
            public static Func<FrameworkElement, bool> <>9__87_0;
            public static Func<ToolBarControlBase, Bar> <>9__93_0;

            static <>c();
            internal void <.cctor>b__12_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__12_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal IBar <CreateItemsSource>b__37_0(object x);
            internal void <Link>b__14_2(BarContainerControl x);
            internal void <OnBarItemDisplayModeChanged>b__39_0(BarControl x);
            internal void <OnBarItemDisplayModeChanged>b__39_1(BarControl x);
            internal Bar <PrepareContainerForItemOverride>b__93_0(ToolBarControlBase x);
            internal void <Refresh>b__38_0(ListCollectionView x);
            internal bool <ShouldProcessDoubleClick>b__87_0(FrameworkElement x);
        }


        private sealed class BarComparer : IComparer<IBar>, IComparer
        {
            private static BarContainerControl.BarComparer instance;

            private BarComparer();
            private bool IsBinded(IBar bar);
            int IComparer<IBar>.Compare(IBar x, IBar y);
            int IComparer.Compare(object x, object y);

            public static BarContainerControl.BarComparer Instance { get; }
        }
    }
}

