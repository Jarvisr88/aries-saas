namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarDockInfo : DependencyObject
    {
        public static readonly DependencyProperty RowProperty;
        public static readonly DependencyProperty ColumnProperty;
        public static readonly DependencyProperty OffsetProperty;
        public static readonly DependencyProperty ContainerNameProperty;
        public static readonly DependencyProperty ContainerProperty;
        public static readonly DependencyProperty FloatBarOffsetProperty;
        public static readonly DependencyProperty FloatBarWidthProperty;
        public static readonly DependencyProperty ShowHeaderInFloatBarProperty;
        public static readonly DependencyProperty ContainerTypeProperty;
        public static readonly DependencyProperty BarProperty;
        public static readonly DependencyProperty IsFloatingProperty;
        public const string FloatingContainerName = "PART_FloatingBarContainer";
        private static readonly HashSet<DependencyProperty> serializableProperties;
        private WeakList<EventHandler> handlersWeakContainerTypeChanged;
        private DevExpress.Xpf.Bars.BarControl barControl;
        private Rect barRect;
        private FloatingBarPopup floatingPopup;
        private readonly Locker locker;

        public event EventHandler ContainerTypeChanged;

        public event EventHandler WeakContainerTypeChanged;

        static BarDockInfo();
        public BarDockInfo();
        public BarDockInfo(DevExpress.Xpf.Bars.Bar bar);
        private void AddCustomization(DependencyProperty property, object oldValue, object newValue);
        private void AddFloatingPopupToLogicalTree(FloatingBarPopup value, DevExpress.Xpf.Bars.Bar owner);
        protected internal virtual int Compare(BarDockInfo info);
        protected virtual FloatingBarPopup CreateFloatingBar();
        protected internal IDisposable Lock();
        protected internal virtual void MakeBarFloating(bool shouldStartDrag);
        protected virtual void OnBarChanged(DevExpress.Xpf.Bars.Bar oldValue, DevExpress.Xpf.Bars.Bar newValue);
        protected virtual void OnBarControlChanged();
        protected virtual void OnBarControlChanging();
        protected virtual void OnBarControlLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnColumnChanged(int oldValue, int newValue);
        protected virtual void OnContainerChanged(BarContainerControl oldValue, BarContainerControl newValue);
        protected virtual void OnContainerNameChanged(string oldValue, string newValue);
        protected virtual void OnContainerTypeChanged(BarContainerType oldValue, BarContainerType newValue);
        protected virtual void OnFloatBarOffsetChanged(Point oldValue, Point newValue);
        protected virtual void OnFloatBarWidthChanged(double oldValue, double newValue);
        private void OnFloatingPopupChanged(FloatingBarPopup oldvalue, FloatingBarPopup newValue);
        protected virtual void OnIsFloatingChanged(bool oldValue, bool newValue);
        protected virtual void OnOffsetChanged(double oldValue, double newValue);
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnRowChanged(int oldValue, int newValue);
        protected internal virtual void OnShowCloseButtonInFloatBarChanged();
        protected virtual void OnShowHeaderInFloatBarChanged(bool oldValue, bool newValue);
        protected virtual void OnUnlocked(object sender, EventArgs e);
        private void RaiseWeakContainerTypeChanged(EventArgs args);
        private void RemoveFloatingPopupFromLogicalTree(FloatingBarPopup value, DevExpress.Xpf.Bars.Bar owner);
        public void Reset();
        public void Reset(bool force);
        protected virtual void TryMakeFloating();
        protected virtual void UpdateBarControlState();

        protected internal BarDockInfoData Data { get; private set; }

        public bool IsFloating { get; set; }

        [Description("Gets or sets the bar's zero-based row index, which defines the bar's vertical position among other bars within the bar container.This is a dependency property.")]
        public int Row { get; set; }

        [Description("Gets or sets the bar's zero-based column index, which defines the bar's horizontal position among other bars displayed in the same row.This is a dependency property.")]
        public int Column { get; set; }

        [Description("Gets or sets the bar's offset from the left or top border of the bar container, based on the container's orientation. This property is in effect when the bar is docked to a container.This is a dependency property.")]
        public double Offset { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Gets or sets the name of the container that displays the current bar.This is a dependency property.")]
        public string ContainerName { get; set; }

        [Description("Gets or sets the offset of the bar when it's in the floating state.This is a dependency property.")]
        public Point FloatBarOffset { get; set; }

        [Description("Gets or sets the bar's width when it's in the floating state.This is a dependency property.")]
        public double FloatBarWidth { get; set; }

        [Description("Gets or sets whether to display a title for a bar when it's floating.This is a dependency property.")]
        public bool ShowHeaderInFloatBar { get; set; }

        [Description("Gets whether a close ('x') button is displayed within a bar, when it's floating.This is a dependency property.")]
        public bool ShowCloseButtonInFloatBar { get; }

        [Description("Gets or sets the type of container that displays the current bar. This value matches the BarContainerControl.ContainerType property.This is a dependency property.")]
        public BarContainerType ContainerType { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Gets or sets the container that displays the current bar.This is a dependency property.")]
        public BarContainerControl Container { get; set; }

        protected internal DevExpress.Xpf.Bars.BarControl BarControl { get; set; }

        internal bool ContinueDragging { get; set; }

        [Description("Gets or sets a bar that owns the current object.This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DevExpress.Xpf.Bars.Bar Bar { get; set; }

        protected internal FloatingBarPopup FloatingPopup { get; set; }

        [Description("Gets the rectangle occupied by the bar.")]
        public Rect BarRect { get; internal set; }

        protected internal Point DragStartPoint { get; set; }

        protected internal bool IsLocked { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarDockInfo.<>c <>9;
            public static Func<Bar, BarManager> <>9__25_0;
            public static Func<Bar, ToolBarControlBase> <>9__66_0;
            public static Func<ToolBarControlBase, BarControl> <>9__66_1;
            public static Func<Bar, ToolBarControlBase> <>9__68_0;
            public static Func<ToolBarControlBase, BarControl> <>9__68_1;
            public static Action<BarControl> <>9__94_0;
            public static Action<BarControl> <>9__95_0;
            public static Action<BarControl> <>9__96_0;
            public static Action<BarControl> <>9__109_0;

            static <>c();
            internal void <.cctor>b__13_0(BarDockInfo x, int oldValue, int newValue);
            internal void <.cctor>b__13_1(BarDockInfo x, int oldValue, int newValue);
            internal void <.cctor>b__13_10(BarDockInfo x, bool oldValue, bool newValue);
            internal void <.cctor>b__13_2(BarDockInfo x, double oldValue, double newValue);
            internal void <.cctor>b__13_3(BarDockInfo x, string oldValue, string newValue);
            internal void <.cctor>b__13_4(BarDockInfo x, BarContainerControl oldValue, BarContainerControl newValue);
            internal void <.cctor>b__13_5(BarDockInfo x, Point oldValue, Point newValue);
            internal void <.cctor>b__13_6(BarDockInfo x, double oldValue, double newValue);
            internal void <.cctor>b__13_7(BarDockInfo x, bool oldValue, bool newValue);
            internal void <.cctor>b__13_8(BarDockInfo x, BarContainerType oldValue, BarContainerType newValue);
            internal void <.cctor>b__13_9(BarDockInfo x, Bar oldValue, Bar newValue);
            internal BarManager <AddCustomization>b__25_0(Bar x);
            internal ToolBarControlBase <OnBarControlChanged>b__68_0(Bar x);
            internal BarControl <OnBarControlChanged>b__68_1(ToolBarControlBase x);
            internal ToolBarControlBase <OnBarControlChanging>b__66_0(Bar x);
            internal BarControl <OnBarControlChanging>b__66_1(ToolBarControlBase x);
            internal void <OnColumnChanged>b__94_0(BarControl x);
            internal void <OnContainerChanged>b__109_0(BarControl x);
            internal void <OnOffsetChanged>b__96_0(BarControl x);
            internal void <OnRowChanged>b__95_0(BarControl x);
        }
    }
}

