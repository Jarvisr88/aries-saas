namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    public interface IUIElementAPI : IAnimatable
    {
        event DragEventHandler DragEnter;

        event DragEventHandler DragLeave;

        event DragEventHandler DragOver;

        event DragEventHandler Drop;

        event GiveFeedbackEventHandler GiveFeedback;

        event EventHandler<TouchEventArgs> GotTouchCapture;

        event DependencyPropertyChangedEventHandler IsKeyboardFocusedChanged;

        event DependencyPropertyChangedEventHandler IsKeyboardFocusWithinChanged;

        event DependencyPropertyChangedEventHandler IsMouseCapturedChanged;

        event DependencyPropertyChangedEventHandler IsMouseDirectlyOverChanged;

        event DependencyPropertyChangedEventHandler IsStylusCapturedChanged;

        event DependencyPropertyChangedEventHandler IsStylusCaptureWithinChanged;

        event DependencyPropertyChangedEventHandler IsStylusDirectlyOverChanged;

        event EventHandler<TouchEventArgs> LostTouchCapture;

        event DragEventHandler PreviewDragEnter;

        event DragEventHandler PreviewDragLeave;

        event DragEventHandler PreviewDragOver;

        event DragEventHandler PreviewDrop;

        event GiveFeedbackEventHandler PreviewGiveFeedback;

        event QueryContinueDragEventHandler PreviewQueryContinueDrag;

        event EventHandler<TouchEventArgs> PreviewTouchDown;

        event EventHandler<TouchEventArgs> PreviewTouchMove;

        event EventHandler<TouchEventArgs> PreviewTouchUp;

        event QueryContinueDragEventHandler QueryContinueDrag;

        event QueryCursorEventHandler QueryCursor;

        event EventHandler<TouchEventArgs> TouchDown;

        event EventHandler<TouchEventArgs> TouchEnter;

        event EventHandler<TouchEventArgs> TouchLeave;

        event EventHandler<TouchEventArgs> TouchMove;

        event EventHandler<TouchEventArgs> TouchUp;

        void AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo);
        void AddToEventRoute(EventRoute route, RoutedEventArgs e);
        bool ShouldSerializeCommandBindings();
        bool ShouldSerializeInputBindings();

        bool IsVisible { get; }

        InputBindingCollection InputBindings { get; }

        CommandBindingCollection CommandBindings { get; }
    }
}

