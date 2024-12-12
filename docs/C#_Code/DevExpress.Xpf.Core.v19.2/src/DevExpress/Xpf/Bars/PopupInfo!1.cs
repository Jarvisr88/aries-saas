namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public abstract class PopupInfo : FrameworkElement, IPopupControl
    {
        private PopupType popup;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty AllowsTransparencyProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ChildProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CustomPopupPlacementCallbackProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HorizontalOffsetProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsOpenProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PlacementProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PlacementRectangleProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PlacementTargetProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PopupAnimationProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty StaysOpenProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty VerticalOffsetProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PopupContentProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty DefaultVerticalOffsetProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IgnoreMenuDropAlignmentProperty;
        private readonly Dictionary<string, Locker> lockers;
        private bool initialized;
        private WeakReference contextElement;

        public event EventHandler Closed;

        public event EventHandler Opened;

        public event CancelEventHandler Opening;

        static PopupInfo();
        public PopupInfo();
        protected static DependencyProperty AddOwner(DependencyProperty prop, Type thisType, Type popupType, PropertyChangedCallback customCallback = null);
        private static DependencyProperty AddOwnerInternal(DependencyProperty prop, Type popupType, PropertyChangedCallback customCallback = null);
        public void ClosePopup();
        protected abstract PopupType CreatePopup();
        private void DoLockedActionIfNotLocked(DependencyProperty property, Action action);
        private Locker GetLocker(DependencyProperty property);
        protected static void OnClonnedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnContextElementChanged(WeakReference oldValue);
        protected virtual void OnContextElementChanging(WeakReference newValue);
        private void OnPlacementTargetChanged(UIElement oldValue, UIElement newValue);
        private static void OnPopupPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public virtual void ShowPopup(UIElement control);

        protected override IEnumerator LogicalChildren { get; }

        public bool AllowsTransparency { get; set; }

        public UIElement Child { get; set; }

        public System.Windows.Controls.Primitives.CustomPopupPlacementCallback CustomPopupPlacementCallback { get; set; }

        public double HorizontalOffset { get; set; }

        public bool IsOpen { get; set; }

        public PlacementMode Placement { get; set; }

        public Rect PlacementRectangle { get; set; }

        public UIElement PlacementTarget { get; set; }

        public System.Windows.Controls.Primitives.PopupAnimation PopupAnimation { get; set; }

        public double VerticalOffset { get; set; }

        public object PopupContent { get; set; }

        public DefaultBoolean IgnoreMenuDropAlignment { get; set; }

        public double DefaultVerticalOffset { get; set; }

        public PopupType Popup { get; }

        public bool IsPopupOpen { get; }

        BarPopupBase IPopupControl.Popup { get; }

        WeakReference IPopupControl.ContextElement { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupInfo<PopupType>.<>c <>9;

            static <>c();
            internal void <.cctor>b__15_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

