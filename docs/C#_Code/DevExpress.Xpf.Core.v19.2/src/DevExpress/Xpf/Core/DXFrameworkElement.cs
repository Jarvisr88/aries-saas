namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Threading;

    public class DXFrameworkElement : FrameworkElement
    {
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event ContextMenuEventHandler ContextMenuClosing
        {
            add
            {
                base.ContextMenuClosing += value;
            }
            remove
            {
                base.ContextMenuClosing -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event ContextMenuEventHandler ContextMenuOpening
        {
            add
            {
                base.ContextMenuOpening += value;
            }
            remove
            {
                base.ContextMenuOpening -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event DragEventHandler DragEnter
        {
            add
            {
                base.DragEnter += value;
            }
            remove
            {
                base.DragEnter -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DragEventHandler DragLeave
        {
            add
            {
                base.DragLeave += value;
            }
            remove
            {
                base.DragLeave -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public event DragEventHandler DragOver
        {
            add
            {
                base.DragOver += value;
            }
            remove
            {
                base.DragOver -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DragEventHandler Drop
        {
            add
            {
                base.Drop += value;
            }
            remove
            {
                base.Drop -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event DependencyPropertyChangedEventHandler FocusableChanged
        {
            add
            {
                base.FocusableChanged += value;
            }
            remove
            {
                base.FocusableChanged -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event GiveFeedbackEventHandler GiveFeedback
        {
            add
            {
                base.GiveFeedback += value;
            }
            remove
            {
                base.GiveFeedback -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event RoutedEventHandler GotFocus
        {
            add
            {
                base.GotFocus += value;
            }
            remove
            {
                base.GotFocus -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event KeyboardFocusChangedEventHandler GotKeyboardFocus
        {
            add
            {
                base.GotKeyboardFocus += value;
            }
            remove
            {
                base.GotKeyboardFocus -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event MouseEventHandler GotMouseCapture
        {
            add
            {
                base.GotMouseCapture += value;
            }
            remove
            {
                base.GotMouseCapture -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusEventHandler GotStylusCapture
        {
            add
            {
                base.GotStylusCapture += value;
            }
            remove
            {
                base.GotStylusCapture -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public event EventHandler<TouchEventArgs> GotTouchCapture
        {
            add
            {
                base.GotTouchCapture += value;
            }
            remove
            {
                base.GotTouchCapture -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler Initialized
        {
            add
            {
                base.Initialized += value;
            }
            remove
            {
                base.Initialized -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event DependencyPropertyChangedEventHandler IsEnabledChanged
        {
            add
            {
                base.IsEnabledChanged += value;
            }
            remove
            {
                base.IsEnabledChanged -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DependencyPropertyChangedEventHandler IsHitTestVisibleChanged
        {
            add
            {
                base.IsHitTestVisibleChanged += value;
            }
            remove
            {
                base.IsHitTestVisibleChanged -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DependencyPropertyChangedEventHandler IsKeyboardFocusedChanged
        {
            add
            {
                base.IsKeyboardFocusedChanged += value;
            }
            remove
            {
                base.IsKeyboardFocusedChanged -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event DependencyPropertyChangedEventHandler IsKeyboardFocusWithinChanged
        {
            add
            {
                base.IsKeyboardFocusWithinChanged += value;
            }
            remove
            {
                base.IsKeyboardFocusWithinChanged -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event DependencyPropertyChangedEventHandler IsMouseCapturedChanged
        {
            add
            {
                base.IsMouseCapturedChanged += value;
            }
            remove
            {
                base.IsMouseCapturedChanged -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event DependencyPropertyChangedEventHandler IsMouseCaptureWithinChanged
        {
            add
            {
                base.IsMouseCaptureWithinChanged += value;
            }
            remove
            {
                base.IsMouseCaptureWithinChanged -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DependencyPropertyChangedEventHandler IsMouseDirectlyOverChanged
        {
            add
            {
                base.IsMouseDirectlyOverChanged += value;
            }
            remove
            {
                base.IsMouseDirectlyOverChanged -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DependencyPropertyChangedEventHandler IsStylusCapturedChanged
        {
            add
            {
                base.IsStylusCapturedChanged += value;
            }
            remove
            {
                base.IsStylusCapturedChanged -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event DependencyPropertyChangedEventHandler IsStylusCaptureWithinChanged
        {
            add
            {
                base.IsStylusCaptureWithinChanged += value;
            }
            remove
            {
                base.IsStylusCaptureWithinChanged -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event DependencyPropertyChangedEventHandler IsStylusDirectlyOverChanged
        {
            add
            {
                base.IsStylusDirectlyOverChanged += value;
            }
            remove
            {
                base.IsStylusDirectlyOverChanged -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DependencyPropertyChangedEventHandler IsVisibleChanged
        {
            add
            {
                base.IsVisibleChanged += value;
            }
            remove
            {
                base.IsVisibleChanged -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event KeyEventHandler KeyDown
        {
            add
            {
                base.KeyDown += value;
            }
            remove
            {
                base.KeyDown -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event KeyEventHandler KeyUp
        {
            add
            {
                base.KeyUp += value;
            }
            remove
            {
                base.KeyUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event EventHandler LayoutUpdated
        {
            add
            {
                base.LayoutUpdated += value;
            }
            remove
            {
                base.LayoutUpdated -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event RoutedEventHandler Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event RoutedEventHandler LostFocus
        {
            add
            {
                base.LostFocus += value;
            }
            remove
            {
                base.LostFocus -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event KeyboardFocusChangedEventHandler LostKeyboardFocus
        {
            add
            {
                base.LostKeyboardFocus += value;
            }
            remove
            {
                base.LostKeyboardFocus -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseEventHandler LostMouseCapture
        {
            add
            {
                base.LostMouseCapture += value;
            }
            remove
            {
                base.LostMouseCapture -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public event StylusEventHandler LostStylusCapture
        {
            add
            {
                base.LostStylusCapture += value;
            }
            remove
            {
                base.LostStylusCapture -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<TouchEventArgs> LostTouchCapture
        {
            add
            {
                base.LostTouchCapture += value;
            }
            remove
            {
                base.LostTouchCapture -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public event EventHandler<ManipulationBoundaryFeedbackEventArgs> ManipulationBoundaryFeedback
        {
            add
            {
                base.ManipulationBoundaryFeedback += value;
            }
            remove
            {
                base.ManipulationBoundaryFeedback -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<ManipulationCompletedEventArgs> ManipulationCompleted
        {
            add
            {
                base.ManipulationCompleted += value;
            }
            remove
            {
                base.ManipulationCompleted -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<ManipulationDeltaEventArgs> ManipulationDelta
        {
            add
            {
                base.ManipulationDelta += value;
            }
            remove
            {
                base.ManipulationDelta -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<ManipulationInertiaStartingEventArgs> ManipulationInertiaStarting
        {
            add
            {
                base.ManipulationInertiaStarting += value;
            }
            remove
            {
                base.ManipulationInertiaStarting -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<ManipulationStartedEventArgs> ManipulationStarted
        {
            add
            {
                base.ManipulationStarted += value;
            }
            remove
            {
                base.ManipulationStarted -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event EventHandler<ManipulationStartingEventArgs> ManipulationStarting
        {
            add
            {
                base.ManipulationStarting += value;
            }
            remove
            {
                base.ManipulationStarting -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler MouseDown
        {
            add
            {
                base.MouseDown += value;
            }
            remove
            {
                base.MouseDown -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event MouseEventHandler MouseEnter
        {
            add
            {
                base.MouseEnter += value;
            }
            remove
            {
                base.MouseEnter -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event MouseEventHandler MouseLeave
        {
            add
            {
                base.MouseLeave += value;
            }
            remove
            {
                base.MouseLeave -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event MouseButtonEventHandler MouseLeftButtonDown
        {
            add
            {
                base.MouseLeftButtonDown += value;
            }
            remove
            {
                base.MouseLeftButtonDown -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler MouseLeftButtonUp
        {
            add
            {
                base.MouseLeftButtonUp += value;
            }
            remove
            {
                base.MouseLeftButtonUp -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event MouseEventHandler MouseMove
        {
            add
            {
                base.MouseMove += value;
            }
            remove
            {
                base.MouseMove -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler MouseRightButtonDown
        {
            add
            {
                base.MouseRightButtonDown += value;
            }
            remove
            {
                base.MouseRightButtonDown -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler MouseRightButtonUp
        {
            add
            {
                base.MouseRightButtonUp += value;
            }
            remove
            {
                base.MouseRightButtonUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler MouseUp
        {
            add
            {
                base.MouseUp += value;
            }
            remove
            {
                base.MouseUp -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event MouseWheelEventHandler MouseWheel
        {
            add
            {
                base.MouseWheel += value;
            }
            remove
            {
                base.MouseWheel -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event DragEventHandler PreviewDragEnter
        {
            add
            {
                base.PreviewDragEnter += value;
            }
            remove
            {
                base.PreviewDragEnter -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DragEventHandler PreviewDragLeave
        {
            add
            {
                base.PreviewDragLeave += value;
            }
            remove
            {
                base.PreviewDragLeave -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public event DragEventHandler PreviewDragOver
        {
            add
            {
                base.PreviewDragOver += value;
            }
            remove
            {
                base.PreviewDragOver -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event DragEventHandler PreviewDrop
        {
            add
            {
                base.PreviewDrop += value;
            }
            remove
            {
                base.PreviewDrop -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event GiveFeedbackEventHandler PreviewGiveFeedback
        {
            add
            {
                base.PreviewGiveFeedback += value;
            }
            remove
            {
                base.PreviewGiveFeedback -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus
        {
            add
            {
                base.PreviewGotKeyboardFocus += value;
            }
            remove
            {
                base.PreviewGotKeyboardFocus -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event KeyEventHandler PreviewKeyDown
        {
            add
            {
                base.PreviewKeyDown += value;
            }
            remove
            {
                base.PreviewKeyDown -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event KeyEventHandler PreviewKeyUp
        {
            add
            {
                base.PreviewKeyUp += value;
            }
            remove
            {
                base.PreviewKeyUp -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus
        {
            add
            {
                base.PreviewLostKeyboardFocus += value;
            }
            remove
            {
                base.PreviewLostKeyboardFocus -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler PreviewMouseDown
        {
            add
            {
                base.PreviewMouseDown += value;
            }
            remove
            {
                base.PreviewMouseDown -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler PreviewMouseLeftButtonDown
        {
            add
            {
                base.PreviewMouseLeftButtonDown += value;
            }
            remove
            {
                base.PreviewMouseLeftButtonDown -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event MouseButtonEventHandler PreviewMouseLeftButtonUp
        {
            add
            {
                base.PreviewMouseLeftButtonUp += value;
            }
            remove
            {
                base.PreviewMouseLeftButtonUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseEventHandler PreviewMouseMove
        {
            add
            {
                base.PreviewMouseMove += value;
            }
            remove
            {
                base.PreviewMouseMove -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event MouseButtonEventHandler PreviewMouseRightButtonDown
        {
            add
            {
                base.PreviewMouseRightButtonDown += value;
            }
            remove
            {
                base.PreviewMouseRightButtonDown -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event MouseButtonEventHandler PreviewMouseRightButtonUp
        {
            add
            {
                base.PreviewMouseRightButtonUp += value;
            }
            remove
            {
                base.PreviewMouseRightButtonUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event MouseButtonEventHandler PreviewMouseUp
        {
            add
            {
                base.PreviewMouseUp += value;
            }
            remove
            {
                base.PreviewMouseUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event MouseWheelEventHandler PreviewMouseWheel
        {
            add
            {
                base.PreviewMouseWheel += value;
            }
            remove
            {
                base.PreviewMouseWheel -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event QueryContinueDragEventHandler PreviewQueryContinueDrag
        {
            add
            {
                base.PreviewQueryContinueDrag += value;
            }
            remove
            {
                base.PreviewQueryContinueDrag -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event StylusButtonEventHandler PreviewStylusButtonDown
        {
            add
            {
                base.PreviewStylusButtonDown += value;
            }
            remove
            {
                base.PreviewStylusButtonDown -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusButtonEventHandler PreviewStylusButtonUp
        {
            add
            {
                base.PreviewStylusButtonUp += value;
            }
            remove
            {
                base.PreviewStylusButtonUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event StylusDownEventHandler PreviewStylusDown
        {
            add
            {
                base.PreviewStylusDown += value;
            }
            remove
            {
                base.PreviewStylusDown -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusEventHandler PreviewStylusInAirMove
        {
            add
            {
                base.PreviewStylusInAirMove += value;
            }
            remove
            {
                base.PreviewStylusInAirMove -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusEventHandler PreviewStylusInRange
        {
            add
            {
                base.PreviewStylusInRange += value;
            }
            remove
            {
                base.PreviewStylusInRange -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public event StylusEventHandler PreviewStylusMove
        {
            add
            {
                base.PreviewStylusMove += value;
            }
            remove
            {
                base.PreviewStylusMove -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event StylusEventHandler PreviewStylusOutOfRange
        {
            add
            {
                base.PreviewStylusOutOfRange += value;
            }
            remove
            {
                base.PreviewStylusOutOfRange -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusSystemGestureEventHandler PreviewStylusSystemGesture
        {
            add
            {
                base.PreviewStylusSystemGesture += value;
            }
            remove
            {
                base.PreviewStylusSystemGesture -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event StylusEventHandler PreviewStylusUp
        {
            add
            {
                base.PreviewStylusUp += value;
            }
            remove
            {
                base.PreviewStylusUp -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event TextCompositionEventHandler PreviewTextInput
        {
            add
            {
                base.PreviewTextInput += value;
            }
            remove
            {
                base.PreviewTextInput -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event EventHandler<TouchEventArgs> PreviewTouchDown
        {
            add
            {
                base.PreviewTouchDown += value;
            }
            remove
            {
                base.PreviewTouchDown -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<TouchEventArgs> PreviewTouchMove
        {
            add
            {
                base.PreviewTouchMove += value;
            }
            remove
            {
                base.PreviewTouchMove -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<TouchEventArgs> PreviewTouchUp
        {
            add
            {
                base.PreviewTouchUp += value;
            }
            remove
            {
                base.PreviewTouchUp -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event QueryContinueDragEventHandler QueryContinueDrag
        {
            add
            {
                base.QueryContinueDrag += value;
            }
            remove
            {
                base.QueryContinueDrag -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event QueryCursorEventHandler QueryCursor
        {
            add
            {
                base.QueryCursor += value;
            }
            remove
            {
                base.QueryCursor -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event RequestBringIntoViewEventHandler RequestBringIntoView
        {
            add
            {
                base.RequestBringIntoView += value;
            }
            remove
            {
                base.RequestBringIntoView -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event SizeChangedEventHandler SizeChanged
        {
            add
            {
                base.SizeChanged += value;
            }
            remove
            {
                base.SizeChanged -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event EventHandler<DataTransferEventArgs> SourceUpdated
        {
            add
            {
                base.SourceUpdated += value;
            }
            remove
            {
                base.SourceUpdated -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusButtonEventHandler StylusButtonDown
        {
            add
            {
                base.StylusButtonDown += value;
            }
            remove
            {
                base.StylusButtonDown -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusButtonEventHandler StylusButtonUp
        {
            add
            {
                base.StylusButtonUp += value;
            }
            remove
            {
                base.StylusButtonUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event StylusDownEventHandler StylusDown
        {
            add
            {
                base.StylusDown += value;
            }
            remove
            {
                base.StylusDown -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusEventHandler StylusEnter
        {
            add
            {
                base.StylusEnter += value;
            }
            remove
            {
                base.StylusEnter -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event StylusEventHandler StylusInAirMove
        {
            add
            {
                base.StylusInAirMove += value;
            }
            remove
            {
                base.StylusInAirMove -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusEventHandler StylusInRange
        {
            add
            {
                base.StylusInRange += value;
            }
            remove
            {
                base.StylusInRange -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusEventHandler StylusLeave
        {
            add
            {
                base.StylusLeave += value;
            }
            remove
            {
                base.StylusLeave -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event StylusEventHandler StylusMove
        {
            add
            {
                base.StylusMove += value;
            }
            remove
            {
                base.StylusMove -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event StylusEventHandler StylusOutOfRange
        {
            add
            {
                base.StylusOutOfRange += value;
            }
            remove
            {
                base.StylusOutOfRange -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event StylusSystemGestureEventHandler StylusSystemGesture
        {
            add
            {
                base.StylusSystemGesture += value;
            }
            remove
            {
                base.StylusSystemGesture -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event StylusEventHandler StylusUp
        {
            add
            {
                base.StylusUp += value;
            }
            remove
            {
                base.StylusUp -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event EventHandler<DataTransferEventArgs> TargetUpdated
        {
            add
            {
                base.TargetUpdated += value;
            }
            remove
            {
                base.TargetUpdated -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public event TextCompositionEventHandler TextInput
        {
            add
            {
                base.TextInput += value;
            }
            remove
            {
                base.TextInput -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event ToolTipEventHandler ToolTipClosing
        {
            add
            {
                base.ToolTipClosing += value;
            }
            remove
            {
                base.ToolTipClosing -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event ToolTipEventHandler ToolTipOpening
        {
            add
            {
                base.ToolTipOpening += value;
            }
            remove
            {
                base.ToolTipOpening -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<TouchEventArgs> TouchDown
        {
            add
            {
                base.TouchDown += value;
            }
            remove
            {
                base.TouchDown -= value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<TouchEventArgs> TouchEnter
        {
            add
            {
                base.TouchEnter += value;
            }
            remove
            {
                base.TouchEnter -= value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler<TouchEventArgs> TouchLeave
        {
            add
            {
                base.TouchLeave += value;
            }
            remove
            {
                base.TouchLeave -= value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event EventHandler<TouchEventArgs> TouchMove
        {
            add
            {
                base.TouchMove += value;
            }
            remove
            {
                base.TouchMove -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<TouchEventArgs> TouchUp
        {
            add
            {
                base.TouchUp += value;
            }
            remove
            {
                base.TouchUp -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event RoutedEventHandler Unloaded
        {
            add
            {
                base.Unloaded += value;
            }
            remove
            {
                base.Unloaded -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ActualHeight =>
            base.ActualHeight;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ActualWidth =>
            base.ActualWidth;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool AllowDrop
        {
            get => 
                base.AllowDrop;
            set => 
                base.AllowDrop = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.Media.CacheMode CacheMode
        {
            get => 
                base.CacheMode;
            set => 
                base.CacheMode = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Geometry Clip
        {
            get => 
                base.Clip;
            set => 
                base.Clip = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.Input.Cursor Cursor
        {
            get => 
                base.Cursor;
            set => 
                base.Cursor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size DesiredSize =>
            base.DesiredSize;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Threading.Dispatcher Dispatcher =>
            base.Dispatcher;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.Media.Effects.Effect Effect
        {
            get => 
                base.Effect;
            set => 
                base.Effect = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.FlowDirection FlowDirection
        {
            get => 
                base.FlowDirection;
            set => 
                base.FlowDirection = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public double Height
        {
            get => 
                base.Height;
            set => 
                base.Height = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.HorizontalAlignment HorizontalAlignment
        {
            get => 
                base.HorizontalAlignment;
            set => 
                base.HorizontalAlignment = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHitTestVisible
        {
            get => 
                base.IsHitTestVisible;
            set => 
                base.IsHitTestVisible = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XmlLanguage Language
        {
            get => 
                base.Language;
            set => 
                base.Language = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Thickness Margin
        {
            get => 
                base.Margin;
            set => 
                base.Margin = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public double MaxHeight
        {
            get => 
                base.MaxHeight;
            set => 
                base.MaxHeight = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public double MaxWidth
        {
            get => 
                base.MaxWidth;
            set => 
                base.MaxWidth = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double MinHeight
        {
            get => 
                base.MinHeight;
            set => 
                base.MinHeight = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double MinWidth
        {
            get => 
                base.MinWidth;
            set => 
                base.MinWidth = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public double Opacity
        {
            get => 
                base.Opacity;
            set => 
                base.Opacity = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Brush OpacityMask
        {
            get => 
                base.OpacityMask;
            set => 
                base.OpacityMask = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DependencyObject Parent =>
            base.Parent;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Transform RenderTransform
        {
            get => 
                base.RenderTransform;
            set => 
                base.RenderTransform = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point RenderTransformOrigin
        {
            get => 
                base.RenderTransformOrigin;
            set => 
                base.RenderTransformOrigin = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ResourceDictionary Resources
        {
            get => 
                base.Resources;
            set => 
                base.Resources = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Style Style
        {
            get => 
                base.Style;
            set => 
                base.Style = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public TriggerCollection Triggers =>
            base.Triggers;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool UseLayoutRounding
        {
            get => 
                base.UseLayoutRounding;
            set => 
                base.UseLayoutRounding = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.VerticalAlignment VerticalAlignment
        {
            get => 
                base.VerticalAlignment;
            set => 
                base.VerticalAlignment = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Windows.Visibility Visibility
        {
            get => 
                base.Visibility;
            set => 
                base.Visibility = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Width
        {
            get => 
                base.Width;
            set => 
                base.Width = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool AreAnyTouchesCaptured =>
            base.AreAnyTouchesCaptured;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool AreAnyTouchesCapturedWithin =>
            base.AreAnyTouchesCapturedWithin;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AreAnyTouchesDirectlyOver =>
            base.AreAnyTouchesDirectlyOver;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AreAnyTouchesOver =>
            base.AreAnyTouchesOver;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Data.BindingGroup BindingGroup
        {
            get => 
                base.BindingGroup;
            set => 
                base.BindingGroup = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Media.Effects.BitmapEffect BitmapEffect
        {
            get => 
                base.BitmapEffect;
            set => 
                base.BitmapEffect = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.Media.Effects.BitmapEffectInput BitmapEffectInput
        {
            get => 
                base.BitmapEffectInput;
            set => 
                base.BitmapEffectInput = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ClipToBounds
        {
            get => 
                base.ClipToBounds;
            set => 
                base.ClipToBounds = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CommandBindingCollection CommandBindings =>
            base.CommandBindings;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Controls.ContextMenu ContextMenu
        {
            get => 
                base.ContextMenu;
            set => 
                base.ContextMenu = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.DependencyObjectType DependencyObjectType =>
            base.DependencyObjectType;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool Focusable
        {
            get => 
                base.Focusable;
            set => 
                base.Focusable = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.Style FocusVisualStyle
        {
            get => 
                base.FocusVisualStyle;
            set => 
                base.FocusVisualStyle = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool ForceCursor
        {
            get => 
                base.ForceCursor;
            set => 
                base.ForceCursor = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasAnimatedProperties =>
            base.HasAnimatedProperties;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public InputBindingCollection InputBindings =>
            base.InputBindings;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.Input.InputScope InputScope
        {
            get => 
                base.InputScope;
            set => 
                base.InputScope = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsArrangeValid =>
            base.IsArrangeValid;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabled
        {
            get => 
                base.IsEnabled;
            set => 
                base.IsEnabled = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFocused =>
            base.IsFocused;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInitialized =>
            base.IsInitialized;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsInputMethodEnabled =>
            base.IsInputMethodEnabled;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeyboardFocused =>
            base.IsKeyboardFocused;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeyboardFocusWithin =>
            base.IsKeyboardFocusWithin;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsLoaded =>
            base.IsLoaded;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsManipulationEnabled
        {
            get => 
                base.IsManipulationEnabled;
            set => 
                base.IsManipulationEnabled = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMeasureValid =>
            base.IsMeasureValid;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMouseCaptured =>
            base.IsMouseCaptured;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsMouseCaptureWithin =>
            base.IsMouseCaptureWithin;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMouseDirectlyOver =>
            base.IsMouseDirectlyOver;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsMouseOver =>
            base.IsMouseOver;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsSealed =>
            base.IsSealed;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStylusCaptured =>
            base.IsStylusCaptured;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusCaptureWithin =>
            base.IsStylusCaptureWithin;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsStylusDirectlyOver =>
            base.IsStylusDirectlyOver;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStylusOver =>
            base.IsStylusOver;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsVisible =>
            base.IsVisible;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Transform LayoutTransform
        {
            get => 
                base.LayoutTransform;
            set => 
                base.LayoutTransform = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool OverridesDefaultStyle
        {
            get => 
                base.OverridesDefaultStyle;
            set => 
                base.OverridesDefaultStyle = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PersistId =>
            base.PersistId;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Size RenderSize
        {
            get => 
                base.RenderSize;
            set => 
                base.RenderSize = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SnapsToDevicePixels
        {
            get => 
                base.SnapsToDevicePixels;
            set => 
                base.SnapsToDevicePixels = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DependencyObject TemplatedParent =>
            base.TemplatedParent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public object ToolTip
        {
            get => 
                base.ToolTip;
            set => 
                base.ToolTip = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<TouchDevice> TouchesCaptured =>
            base.TouchesCaptured;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<TouchDevice> TouchesCapturedWithin =>
            base.TouchesCapturedWithin;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IEnumerable<TouchDevice> TouchesDirectlyOver =>
            base.TouchesDirectlyOver;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<TouchDevice> TouchesOver =>
            base.TouchesOver;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Uid
        {
            get => 
                base.Uid;
            set => 
                base.Uid = value;
        }
    }
}

