namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class ScrollBarExtensions : DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyPropertyKey HorizontalMouseWheelListeningInitializedPropertyKey = DependencyProperty.RegisterAttachedReadOnly("HorizontalMouseWheelListeningInitialized", typeof(bool), typeof(ScrollBarExtensions), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(ScrollBarExtensions.HorizontalMouseWheelListeningInitializedChanged)));
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HorizontalMouseWheelListeningInitializedProperty = HorizontalMouseWheelListeningInitializedPropertyKey.DependencyProperty;
        public static readonly DependencyProperty HandlesDefaultMouseScrollingProperty = DependencyProperty.RegisterAttached("HandlesDefaultMouseScrolling", typeof(bool), typeof(ScrollBarExtensions), new PropertyMetadata(true));
        public static readonly DependencyProperty AllowMouseScrollingProperty = DependencyProperty.RegisterAttached("AllowMouseScrolling", typeof(bool), typeof(ScrollBarExtensions), new PropertyMetadata(false, new PropertyChangedCallback(ScrollBarExtensions.AllowMouseScrollingChanged)));
        public static readonly DependencyProperty ScrollBehaviorProperty = DependencyProperty.RegisterAttached("ScrollBehavior", typeof(ScrollBehaviorBase), typeof(ScrollBarExtensions), new PropertyMetadata(null));
        public static readonly DependencyProperty PreventParentScrollingProperty = DependencyProperty.RegisterAttached("PreventParentScrolling", typeof(bool), typeof(ScrollBarExtensions), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty ScrollBarModeProperty = DependencyProperty.RegisterAttached("ScrollBarMode", typeof(ScrollBarMode), typeof(ScrollBarExtensions), new FrameworkPropertyMetadata(ScrollBarMode.Standard, FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty IsTouchScrollBarModeProperty = DependencyProperty.RegisterAttached("IsTouchScrollBarMode", typeof(bool), typeof(ScrollBarExtensions), new PropertyMetadata(false));
        public static readonly DependencyProperty ScrollViewerMouseMovedProperty = DependencyProperty.RegisterAttached("ScrollViewerMouseMoved", typeof(bool), typeof(ScrollBarExtensions), new PropertyMetadata(false));
        public static readonly DependencyProperty ScrollViewerSizeChangedProperty = DependencyProperty.RegisterAttached("ScrollViewerSizeChanged", typeof(bool), typeof(ScrollBarExtensions), new PropertyMetadata(false));
        public static readonly DependencyProperty CurrentHorizontalScrollMarginProperty;
        public static readonly DependencyProperty ScrollViewerOrientationProperty;
        public static readonly DependencyProperty ListeningScrollBarThumbDragDeltaProperty;
        public static readonly DependencyProperty IsScrollBarThumbDragDeltaListenerProperty;
        public static readonly DependencyProperty AllowShiftKeyModeProperty;

        static ScrollBarExtensions()
        {
            Thickness defaultValue = new Thickness();
            CurrentHorizontalScrollMarginProperty = DependencyProperty.RegisterAttached("CurrentHorizontalScrollMargin", typeof(Thickness), typeof(ScrollBarExtensions), new PropertyMetadata(defaultValue));
            ScrollViewerOrientationProperty = DependencyProperty.RegisterAttached("ScrollViewerOrientation", typeof(Orientation?), typeof(ScrollBarExtensions), new PropertyMetadata(null));
            ListeningScrollBarThumbDragDeltaProperty = DependencyProperty.RegisterAttached("ListeningScrollBarThumbDragDelta", typeof(bool), typeof(ScrollBarExtensions), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(ScrollBarExtensions.ListeningScrollBarThumbDragDeltaChanged)));
            IsScrollBarThumbDragDeltaListenerProperty = DependencyProperty.RegisterAttached("IsScrollBarThumbDragDeltaListener", typeof(bool), typeof(ScrollBarExtensions), new FrameworkPropertyMetadata(false));
            AllowShiftKeyModeProperty = DependencyProperty.RegisterAttached("AllowShiftKeyMode", typeof(bool), typeof(ScrollBarExtensions), new PropertyMetadata(false));
        }

        private static void AllowMouseScrollingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                BehaviorCollection source = Interaction.GetBehaviors(element);
                if ((bool) e.NewValue)
                {
                    source.Add(new MouseScrollBehavior());
                }
                else
                {
                    Func<Behavior, bool> predicate = <>c.<>9__24_0;
                    if (<>c.<>9__24_0 == null)
                    {
                        Func<Behavior, bool> local1 = <>c.<>9__24_0;
                        predicate = <>c.<>9__24_0 = x => x is MouseScrollBehavior;
                    }
                    Behavior behavior = source.FirstOrDefault<Behavior>(predicate);
                    if (behavior != null)
                    {
                        source.Remove(behavior);
                    }
                }
            }
        }

        public static bool GetAllowMouseScrolling(DependencyObject d) => 
            (bool) d.GetValue(AllowMouseScrollingProperty);

        public static bool GetAllowShiftKeyMode(DependencyObject d) => 
            (bool) d.GetValue(AllowShiftKeyModeProperty);

        public static Thickness GetCurrentHorizontalScrollMargin(DependencyObject obj) => 
            (Thickness) obj.GetValue(CurrentHorizontalScrollMarginProperty);

        public static bool GetHandlesDefaultMouseScrolling(DependencyObject d) => 
            (bool) d.GetValue(HandlesDefaultMouseScrollingProperty);

        public static bool GetHorizontalMouseWheelListeningInitializedProperty(DependencyObject d) => 
            (bool) d.GetValue(HorizontalMouseWheelListeningInitializedProperty);

        public static bool GetIsScrollBarThumbDragDeltaListener(DependencyObject d) => 
            (bool) d.GetValue(IsScrollBarThumbDragDeltaListenerProperty);

        public static bool GetIsTouchScrollBarMode(DependencyObject obj) => 
            (bool) obj.GetValue(IsTouchScrollBarModeProperty);

        public static bool GetListeningScrollBarThumbDragDelta(DependencyObject d) => 
            (bool) d.GetValue(ListeningScrollBarThumbDragDeltaProperty);

        public static bool GetPreventParentScrolling(DependencyObject obj) => 
            (bool) obj.GetValue(PreventParentScrollingProperty);

        public static ScrollBarMode GetScrollBarMode(DependencyObject obj) => 
            (ScrollBarMode) obj.GetValue(ScrollBarModeProperty);

        public static ScrollBehaviorBase GetScrollBehavior(DependencyObject obj) => 
            (ScrollBehaviorBase) obj.GetValue(ScrollBehaviorProperty);

        public static bool GetScrollViewerMouseMoved(DependencyObject obj) => 
            (bool) obj.GetValue(ScrollViewerMouseMovedProperty);

        public static Orientation? GetScrollViewerOrientation(DependencyObject obj) => 
            (Orientation?) obj.GetValue(ScrollViewerOrientationProperty);

        public static bool GetScrollViewerSizeChanged(DependencyObject obj) => 
            (bool) obj.GetValue(ScrollViewerSizeChangedProperty);

        private static void HorizontalMouseWheelListeningInitializedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                BehaviorCollection source = Interaction.GetBehaviors(element);
                if ((bool) e.NewValue)
                {
                    source.Add(new HWndHostWMMouseHWheelBehavior());
                }
                else
                {
                    Func<Behavior, bool> predicate = <>c.<>9__25_0;
                    if (<>c.<>9__25_0 == null)
                    {
                        Func<Behavior, bool> local1 = <>c.<>9__25_0;
                        predicate = <>c.<>9__25_0 = x => x is HWndHostWMMouseHWheelBehavior;
                    }
                    Behavior behavior = source.FirstOrDefault<Behavior>(predicate);
                    if (behavior != null)
                    {
                        source.Remove(behavior);
                    }
                }
            }
        }

        private static void ListeningScrollBarThumbDragDeltaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Thumb)
            {
                BehaviorCollection source = Interaction.GetBehaviors(d);
                if ((bool) e.NewValue)
                {
                    source.Add(new ScrollBarThumbDragDeltaBehavior());
                }
                else
                {
                    Func<Behavior, bool> predicate = <>c.<>9__19_0;
                    if (<>c.<>9__19_0 == null)
                    {
                        Func<Behavior, bool> local1 = <>c.<>9__19_0;
                        predicate = <>c.<>9__19_0 = x => x is ScrollBarThumbDragDeltaBehavior;
                    }
                    Behavior behavior = source.FirstOrDefault<Behavior>(predicate);
                    if (behavior != null)
                    {
                        source.Remove(behavior);
                    }
                }
            }
        }

        public static void SetAllowMouseScrolling(DependencyObject d, bool value)
        {
            d.SetValue(AllowMouseScrollingProperty, value);
        }

        public static void SetAllowShiftKeyMode(DependencyObject d, bool value)
        {
            d.SetValue(AllowShiftKeyModeProperty, value);
        }

        public static void SetCurrentHorizontalScrollMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CurrentHorizontalScrollMarginProperty, value);
        }

        public static void SetHandlesDefaultMouseScrolling(DependencyObject d, bool value)
        {
            d.SetValue(HandlesDefaultMouseScrollingProperty, value);
        }

        internal static void SetHorizontalMouseWheelListeningInitializedProperty(DependencyObject d, bool value)
        {
            d.SetValue(HorizontalMouseWheelListeningInitializedPropertyKey, value);
        }

        public static void SetIsScrollBarThumbDragDeltaListener(DependencyObject d, bool value)
        {
            d.SetValue(IsScrollBarThumbDragDeltaListenerProperty, value);
        }

        public static void SetIsTouchScrollBarMode(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTouchScrollBarModeProperty, value);
        }

        public static void SetListeningScrollBarThumbDragDelta(DependencyObject d, bool value)
        {
            d.SetValue(ListeningScrollBarThumbDragDeltaProperty, value);
        }

        public static void SetPreventParentScrolling(DependencyObject obj, bool value)
        {
            obj.SetValue(PreventParentScrollingProperty, value);
        }

        public static void SetScrollBarMode(DependencyObject obj, ScrollBarMode value)
        {
            obj.SetValue(ScrollBarModeProperty, value);
        }

        public static void SetScrollBehavior(DependencyObject obj, ScrollBehaviorBase value)
        {
            obj.SetValue(ScrollBehaviorProperty, value);
        }

        public static void SetScrollViewerMouseMoved(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollViewerMouseMovedProperty, value);
        }

        public static void SetScrollViewerOrientation(DependencyObject obj, Orientation? value)
        {
            obj.SetValue(ScrollViewerOrientationProperty, value);
        }

        public static void SetScrollViewerSizeChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollViewerSizeChangedProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollBarExtensions.<>c <>9 = new ScrollBarExtensions.<>c();
            public static Func<Behavior, bool> <>9__19_0;
            public static Func<Behavior, bool> <>9__24_0;
            public static Func<Behavior, bool> <>9__25_0;

            internal bool <AllowMouseScrollingChanged>b__24_0(Behavior x) => 
                x is MouseScrollBehavior;

            internal bool <HorizontalMouseWheelListeningInitializedChanged>b__25_0(Behavior x) => 
                x is HWndHostWMMouseHWheelBehavior;

            internal bool <ListeningScrollBarThumbDragDeltaChanged>b__19_0(Behavior x) => 
                x is ScrollBarThumbDragDeltaBehavior;
        }
    }
}

