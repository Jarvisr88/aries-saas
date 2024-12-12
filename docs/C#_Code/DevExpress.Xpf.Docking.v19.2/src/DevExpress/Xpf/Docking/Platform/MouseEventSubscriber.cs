namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    internal class MouseEventSubscriber : ViewEventSubscriber<UIElement>
    {
        public MouseEventSubscriber(UIElement source, BaseView view) : base(source, view)
        {
        }

        private bool CanSkipMoveEvent(System.Windows.Input.MouseEventArgs e) => 
            (base.View.Adapter.DragService.DragItem == null) || ((base.View.Adapter.DragService.DragSource != null) && (e.LeftButton == MouseButtonState.Released));

        internal void OnDesignTimeEvent(object sender, RoutedEventArgs e)
        {
            if (e is System.Windows.Input.MouseEventArgs)
            {
                if (e is MouseButtonEventArgs)
                {
                    if (ReferenceEquals(e.RoutedEvent, UIElement.PreviewMouseDownEvent))
                    {
                        this.RootUIElementPreviewMouseDown(sender, (MouseButtonEventArgs) e);
                    }
                    if (ReferenceEquals(e.RoutedEvent, UIElement.MouseUpEvent))
                    {
                        this.RootUIElementMouseUp(sender, (MouseButtonEventArgs) e);
                    }
                }
                else if (ReferenceEquals(e.RoutedEvent, UIElement.MouseMoveEvent))
                {
                    this.RootUIElementMouseMove(sender, (System.Windows.Input.MouseEventArgs) e);
                }
            }
        }

        private void RootUIElementLostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            base.View.OnMouseEvent(MouseEventType.MouseCaptureLost, null);
        }

        private void RootUIElementMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = (base.View != null) && base.View.CanHandleMouseDown();
        }

        private void RootUIElementMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            base.View.OnMouseEvent(MouseEventType.MouseLeave, null);
        }

        private void RootUIElementMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ICustomDragProcessor view = base.View as ICustomDragProcessor;
            if (!base.View.IsDisposing)
            {
                Func<ICustomDragProcessor, bool> evaluator = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<ICustomDragProcessor, bool> local1 = <>c.<>9__4_0;
                    evaluator = <>c.<>9__4_0 = x => x.IsInEvent;
                }
                if (!view.Return<ICustomDragProcessor, bool>(evaluator, (<>c.<>9__4_1 ??= () => false)))
                {
                    if (this.CanSkipMoveEvent(e))
                    {
                        Action<ICustomDragProcessor> action = <>c.<>9__4_2;
                        if (<>c.<>9__4_2 == null)
                        {
                            Action<ICustomDragProcessor> local3 = <>c.<>9__4_2;
                            action = <>c.<>9__4_2 = x => x.CancelDragging();
                        }
                        view.Do<ICustomDragProcessor>(action);
                    }
                    else
                    {
                        base.View.OnMouseEvent(MouseEventType.MouseMove, EventArgsHelper.Convert(base.Root, e));
                        Action<ICustomDragProcessor> action = <>c.<>9__4_3;
                        if (<>c.<>9__4_3 == null)
                        {
                            Action<ICustomDragProcessor> local4 = <>c.<>9__4_3;
                            action = <>c.<>9__4_3 = x => x.StartDragging();
                        }
                        view.Do<ICustomDragProcessor>(action);
                    }
                }
            }
        }

        private void RootUIElementMouseUp(object sender, MouseButtonEventArgs e)
        {
            Func<ICustomDragProcessor, bool> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<ICustomDragProcessor, bool> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.IsInEvent;
            }
            if (!(base.View as ICustomDragProcessor).Return<ICustomDragProcessor, bool>(evaluator, (<>c.<>9__3_1 ??= () => false)))
            {
                DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea = EventArgsHelper.Convert(base.Root, e);
                base.View.OnMouseEvent(MouseEventType.MouseUp, ea);
                e.Handled = ea.Handled;
            }
        }

        private void RootUIElementPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Func<ICustomDragProcessor, bool> evaluator = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<ICustomDragProcessor, bool> local1 = <>c.<>9__5_0;
                evaluator = <>c.<>9__5_0 = x => x.IsInEvent;
            }
            if (!(base.View as ICustomDragProcessor).Return<ICustomDragProcessor, bool>(evaluator, (<>c.<>9__5_1 ??= () => false)) && ViewEventSubscriberHelper.CanHandlePreviewEvent(sender as DependencyObject, e))
            {
                HitTestHelper.ResetCache();
                base.View.OnMouseEvent(MouseEventType.MouseDown, EventArgsHelper.Convert(base.Root, e));
            }
        }

        protected override void Subscribe(UIElement element)
        {
            element.PreviewMouseDown += new MouseButtonEventHandler(this.RootUIElementPreviewMouseDown);
            element.MouseMove += new MouseEventHandler(this.RootUIElementMouseMove);
            element.MouseUp += new MouseButtonEventHandler(this.RootUIElementMouseUp);
            element.MouseDown += new MouseButtonEventHandler(this.RootUIElementMouseDown);
            element.MouseLeave += new MouseEventHandler(this.RootUIElementMouseLeave);
            element.LostMouseCapture += new MouseEventHandler(this.RootUIElementLostMouseCapture);
        }

        protected override void UnSubscribe(UIElement element)
        {
            element.PreviewMouseDown -= new MouseButtonEventHandler(this.RootUIElementPreviewMouseDown);
            element.MouseMove -= new MouseEventHandler(this.RootUIElementMouseMove);
            element.MouseUp -= new MouseButtonEventHandler(this.RootUIElementMouseUp);
            element.MouseLeave -= new MouseEventHandler(this.RootUIElementMouseLeave);
            element.MouseDown -= new MouseButtonEventHandler(this.RootUIElementMouseDown);
            element.LostMouseCapture -= new MouseEventHandler(this.RootUIElementLostMouseCapture);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MouseEventSubscriber.<>c <>9 = new MouseEventSubscriber.<>c();
            public static Func<ICustomDragProcessor, bool> <>9__3_0;
            public static Func<bool> <>9__3_1;
            public static Func<ICustomDragProcessor, bool> <>9__4_0;
            public static Func<bool> <>9__4_1;
            public static Action<ICustomDragProcessor> <>9__4_2;
            public static Action<ICustomDragProcessor> <>9__4_3;
            public static Func<ICustomDragProcessor, bool> <>9__5_0;
            public static Func<bool> <>9__5_1;

            internal bool <RootUIElementMouseMove>b__4_0(ICustomDragProcessor x) => 
                x.IsInEvent;

            internal bool <RootUIElementMouseMove>b__4_1() => 
                false;

            internal void <RootUIElementMouseMove>b__4_2(ICustomDragProcessor x)
            {
                x.CancelDragging();
            }

            internal void <RootUIElementMouseMove>b__4_3(ICustomDragProcessor x)
            {
                x.StartDragging();
            }

            internal bool <RootUIElementMouseUp>b__3_0(ICustomDragProcessor x) => 
                x.IsInEvent;

            internal bool <RootUIElementMouseUp>b__3_1() => 
                false;

            internal bool <RootUIElementPreviewMouseDown>b__5_0(ICustomDragProcessor x) => 
                x.IsInEvent;

            internal bool <RootUIElementPreviewMouseDown>b__5_1() => 
                false;
        }
    }
}

