namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Threading;

    public abstract class DXDesignTimeControl : Control
    {
        public const string DataCategory = "Data";
        private ControlTemplate controlTemplate;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event MouseButtonEventHandler MouseDoubleClick
        {
            add
            {
                base.MouseDoubleClick += value;
            }
            remove
            {
                base.MouseDoubleClick -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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
        public event MouseButtonEventHandler PreviewMouseDoubleClick
        {
            add
            {
                base.PreviewMouseDoubleClick += value;
            }
            remove
            {
                base.PreviewMouseDoubleClick -= value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        public DXDesignTimeControl()
        {
            this.Loaded += new RoutedEventHandler(this.DXDesignTimeControl_Loaded);
        }

        protected override Size ArrangeOverride(Size arrangeBounds) => 
            DesignerProperties.GetIsInDesignMode(this) ? base.ArrangeOverride(arrangeBounds) : new Size(0.0, 0.0);

        protected virtual ControlTemplate CreateControlTemplate() => 
            XamlHelper.GetControlTemplate("<Image x:Name=\"PART_Icon\" Stretch=\"None\">" + "<RenderOptions.BitmapScalingMode>NearestNeighbor</RenderOptions.BitmapScalingMode>" + "</Image>");

        private void DXDesignTimeControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResetSize();
            this.SetTemplate();
        }

        protected abstract string GetDesignTimeImageName();
        protected virtual Assembly GetImageAssembly() => 
            base.GetType().Assembly;

        protected override Size MeasureOverride(Size constraint) => 
            DesignerProperties.GetIsInDesignMode(this) ? base.MeasureOverride(constraint) : new Size(0.0, 0.0);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (DesignerProperties.GetIsInDesignMode(this) && (this.Template != null))
            {
                using (Stream stream = this.GetImageAssembly().GetManifestResourceStream(this.GetDesignTimeImageName()))
                {
                    ((Image) base.GetTemplateChild("PART_Icon")).Source = (ImageSource) ImageDataConverter.CreateImageFromStream(stream);
                }
            }
        }

        private void ResetSize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.Height = 0.0;
                this.Width = 0.0;
            }
        }

        internal void SetTemplate()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.controlTemplate ??= this.CreateControlTemplate();
                this.Template = this.controlTemplate;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double ActualHeight =>
            base.ActualHeight;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public double ActualWidth =>
            base.ActualWidth;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDrop
        {
            get => 
                base.AllowDrop;
            set => 
                base.AllowDrop = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Brush Background
        {
            get => 
                base.Background;
            set => 
                base.Background = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Brush BorderBrush
        {
            get => 
                base.BorderBrush;
            set => 
                base.BorderBrush = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Thickness BorderThickness
        {
            get => 
                base.BorderThickness;
            set => 
                base.BorderThickness = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.Input.Cursor Cursor
        {
            get => 
                base.Cursor;
            set => 
                base.Cursor = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Size DesiredSize =>
            base.DesiredSize;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.Threading.Dispatcher Dispatcher =>
            base.Dispatcher;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Media.Effects.Effect Effect
        {
            get => 
                base.Effect;
            set => 
                base.Effect = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Media.FontFamily FontFamily
        {
            get => 
                base.FontFamily;
            set => 
                base.FontFamily = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public double FontSize
        {
            get => 
                base.FontSize;
            set => 
                base.FontSize = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.FontStretch FontStretch
        {
            get => 
                base.FontStretch;
            set => 
                base.FontStretch = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.FontStyle FontStyle
        {
            get => 
                base.FontStyle;
            set => 
                base.FontStyle = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Windows.FontWeight FontWeight
        {
            get => 
                base.FontWeight;
            set => 
                base.FontWeight = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Brush Foreground
        {
            get => 
                base.Foreground;
            set => 
                base.Foreground = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Height
        {
            get => 
                base.Height;
            set => 
                base.Height = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.HorizontalAlignment HorizontalAlignment
        {
            get => 
                base.HorizontalAlignment;
            set => 
                base.HorizontalAlignment = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.HorizontalAlignment HorizontalContentAlignment
        {
            get => 
                base.HorizontalContentAlignment;
            set => 
                base.HorizontalContentAlignment = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsHitTestVisible
        {
            get => 
                base.IsHitTestVisible;
            set => 
                base.IsHitTestVisible = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsTabStop
        {
            get => 
                base.IsTabStop;
            set => 
                base.IsTabStop = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public XmlLanguage Language
        {
            get => 
                base.Language;
            set => 
                base.Language = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Thickness Margin
        {
            get => 
                base.Margin;
            set => 
                base.Margin = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public double MaxHeight
        {
            get => 
                base.MaxHeight;
            set => 
                base.MaxHeight = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double MaxWidth
        {
            get => 
                base.MaxWidth;
            set => 
                base.MaxWidth = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double MinHeight
        {
            get => 
                base.MinHeight;
            set => 
                base.MinHeight = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public double MinWidth
        {
            get => 
                base.MinWidth;
            set => 
                base.MinWidth = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public Thickness Padding
        {
            get => 
                base.Padding;
            set => 
                base.Padding = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DependencyObject Parent =>
            base.Parent;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Transform RenderTransform
        {
            get => 
                base.RenderTransform;
            set => 
                base.RenderTransform = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Windows.Style Style
        {
            get => 
                base.Style;
            set => 
                base.Style = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int TabIndex
        {
            get => 
                base.TabIndex;
            set => 
                base.TabIndex = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public ControlTemplate Template
        {
            get => 
                base.Template;
            set => 
                base.Template = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TriggerCollection Triggers =>
            base.Triggers;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseLayoutRounding
        {
            get => 
                base.UseLayoutRounding;
            set => 
                base.UseLayoutRounding = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.VerticalAlignment VerticalAlignment
        {
            get => 
                base.VerticalAlignment;
            set => 
                base.VerticalAlignment = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.VerticalAlignment VerticalContentAlignment
        {
            get => 
                base.VerticalContentAlignment;
            set => 
                base.VerticalContentAlignment = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AreAnyTouchesCaptured =>
            base.AreAnyTouchesCaptured;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AreAnyTouchesCapturedWithin =>
            base.AreAnyTouchesCapturedWithin;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AreAnyTouchesDirectlyOver =>
            base.AreAnyTouchesDirectlyOver;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AreAnyTouchesOver =>
            base.AreAnyTouchesOver;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Windows.Data.BindingGroup BindingGroup
        {
            get => 
                base.BindingGroup;
            set => 
                base.BindingGroup = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.Media.Effects.BitmapEffect BitmapEffect
        {
            get => 
                base.BitmapEffect;
            set => 
                base.BitmapEffect = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.Media.Effects.BitmapEffectInput BitmapEffectInput
        {
            get => 
                base.BitmapEffectInput;
            set => 
                base.BitmapEffectInput = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ClipToBounds
        {
            get => 
                base.ClipToBounds;
            set => 
                base.ClipToBounds = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public CommandBindingCollection CommandBindings =>
            base.CommandBindings;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Windows.Controls.ContextMenu ContextMenu
        {
            get => 
                base.ContextMenu;
            set => 
                base.ContextMenu = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public System.Windows.Style FocusVisualStyle
        {
            get => 
                base.FocusVisualStyle;
            set => 
                base.FocusVisualStyle = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public System.Windows.Input.InputScope InputScope
        {
            get => 
                base.InputScope;
            set => 
                base.InputScope = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsArrangeValid =>
            base.IsArrangeValid;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabled
        {
            get => 
                base.IsEnabled;
            set => 
                base.IsEnabled = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsFocused =>
            base.IsFocused;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsInitialized =>
            base.IsInitialized;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsInputMethodEnabled =>
            base.IsInputMethodEnabled;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeyboardFocused =>
            base.IsKeyboardFocused;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsKeyboardFocusWithin =>
            base.IsKeyboardFocusWithin;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsLoaded =>
            base.IsLoaded;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsManipulationEnabled
        {
            get => 
                base.IsManipulationEnabled;
            set => 
                base.IsManipulationEnabled = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsMeasureValid =>
            base.IsMeasureValid;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsMouseCaptured =>
            base.IsMouseCaptured;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsMouseCaptureWithin =>
            base.IsMouseCaptureWithin;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsMouseDirectlyOver =>
            base.IsMouseDirectlyOver;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMouseOver =>
            base.IsMouseOver;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsSealed =>
            base.IsSealed;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusCaptured =>
            base.IsStylusCaptured;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusCaptureWithin =>
            base.IsStylusCaptureWithin;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusDirectlyOver =>
            base.IsStylusDirectlyOver;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusOver =>
            base.IsStylusOver;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DependencyObject TemplatedParent =>
            base.TemplatedParent;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ToolTip
        {
            get => 
                base.ToolTip;
            set => 
                base.ToolTip = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<TouchDevice> TouchesCaptured =>
            base.TouchesCaptured;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public IEnumerable<TouchDevice> TouchesCapturedWithin =>
            base.TouchesCapturedWithin;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<TouchDevice> TouchesDirectlyOver =>
            base.TouchesDirectlyOver;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public IEnumerable<TouchDevice> TouchesOver =>
            base.TouchesOver;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public string Uid
        {
            get => 
                base.Uid;
            set => 
                base.Uid = value;
        }

        public System.Type Type =>
            base.GetType();
    }
}

