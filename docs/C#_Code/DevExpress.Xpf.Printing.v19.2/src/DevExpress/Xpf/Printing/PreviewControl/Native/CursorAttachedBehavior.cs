namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class CursorAttachedBehavior : Behavior<DXScrollViewer>
    {
        private readonly TimeSpan UpdateCursorTimeSpan;
        public static readonly DependencyProperty CursorModeProperty;

        static CursorAttachedBehavior()
        {
            CursorModeProperty = DependencyPropertyManager.Register("CursorMode", typeof(CursorModeType), typeof(CursorAttachedBehavior), new PropertyMetadata(CursorModeType.SelectTool, (obj, args) => ((CursorAttachedBehavior) obj).OnCursorModeChanged((CursorModeType) args.NewValue)));
        }

        public CursorAttachedBehavior()
        {
            // Unresolved stack state at '000000A4'
        }

        private Brick GetHitTestBrick()
        {
            Point position = Mouse.GetPosition(this.PreviewRoot.DocumentPresenter);
            return this.PreviewRoot.DocumentPresenter.NavigationStrategy.GetBrick(position);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.UpdateCursorTimer = new DispatcherTimer(this.UpdateCursorTimeSpan, DispatcherPriority.Normal, new EventHandler(this.UpdateCursorTick), base.AssociatedObject.Dispatcher);
            this.UpdateCursorTimer.Stop();
            ((PageSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonDown);
            ((PageSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonUp);
            ((PageSelector) base.AssociatedObject.TemplatedParent).PreviewMouseMove += new MouseEventHandler(this.OnPreviewMouseMove);
            ((PageSelector) base.AssociatedObject.TemplatedParent).MouseEnter += new MouseEventHandler(this.OnMouseEnter);
            ((PageSelector) base.AssociatedObject.TemplatedParent).MouseLeave += new MouseEventHandler(this.OnMouseLeave);
            this.CursorRoot = LayoutHelper.FindRoot(base.AssociatedObject, false) as FrameworkElement;
            this.CursorRoot.Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.PreviewKeyDown += this.PreviewKeyDownHandler.Handler;
            });
            this.CursorRoot.Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.PreviewKeyUp += this.PreviewKeyUpHandler.Handler;
            });
            this.PreviewRoot = LayoutHelper.FindParentObject<DocumentPreviewControl>(base.AssociatedObject);
        }

        protected virtual void OnCursorModeChanged(CursorModeType newValue)
        {
            this.SetDefaultCursor();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ((PageSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonDown);
            ((PageSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonUp);
            ((PageSelector) base.AssociatedObject.TemplatedParent).PreviewMouseMove -= new MouseEventHandler(this.OnPreviewMouseMove);
            ((PageSelector) base.AssociatedObject.TemplatedParent).MouseEnter -= new MouseEventHandler(this.OnMouseEnter);
            ((PageSelector) base.AssociatedObject.TemplatedParent).MouseLeave -= new MouseEventHandler(this.OnMouseLeave);
            this.CursorRoot.Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.PreviewKeyDown -= this.PreviewKeyDownHandler.Handler;
            });
            this.CursorRoot.Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.PreviewKeyUp -= this.PreviewKeyUpHandler.Handler;
            });
            this.UpdateCursorTimer.Stop();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!MouseHelper.IsMouseLeftButtonPressed(e))
            {
                this.SetDefaultCursor(e);
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!MouseHelper.IsMouseLeftButtonPressed(e))
            {
                base.AssociatedObject.Cursor = Cursors.Arrow;
            }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SetDragCursor(e);
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.SetDefaultCursor(e);
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.CursorMode == CursorModeType.SelectTool)
            {
                this.SetSelectToolModeCursor(e.GetPosition(this.PreviewRoot));
            }
        }

        private void PreviewKeyDownHandlerInternal(KeyEventArgs args)
        {
            this.SetDefaultCursor();
            this.UpdateCursorTimer.Stop();
            this.UpdateCursorTimer.Start();
        }

        private void PreviewKeyUpHandlerInternal(KeyEventArgs args)
        {
            this.SetDefaultCursor();
            this.UpdateCursorTimer.Stop();
        }

        private void SetDefaultCursor()
        {
            this.SetDefaultCursor(null);
        }

        private void SetDefaultCursor(MouseEventArgs e)
        {
            CursorModeType cursorMode = this.CursorMode;
            if (cursorMode == CursorModeType.HandTool)
            {
                CursorHelper.SetCursor(base.AssociatedObject, PreviewCursors.Hand);
            }
            else if (cursorMode == CursorModeType.SelectTool)
            {
                Point p = (e != null) ? e.GetPosition(this.PreviewRoot) : new Point(0.0, 0.0);
                this.SetSelectToolModeCursor(p);
            }
        }

        private void SetDragCursor(MouseEventArgs e)
        {
            CursorModeType cursorMode = this.CursorMode;
            if (cursorMode == CursorModeType.HandTool)
            {
                CursorHelper.SetCursor(base.AssociatedObject, PreviewCursors.HandDrag);
            }
            else if (cursorMode == CursorModeType.SelectTool)
            {
                Point p = (e != null) ? e.GetPosition(this.PreviewRoot) : new Point(0.0, 0.0);
                this.SetSelectToolModeCursor(p);
            }
        }

        private void SetSelectToolModeCursor(Point p)
        {
            if ((this.PreviewRoot != null) && this.PreviewRoot.IsLoaded)
            {
                Func<IDocumentViewModel, bool> evaluator = <>c.<>9__36_0;
                if (<>c.<>9__36_0 == null)
                {
                    Func<IDocumentViewModel, bool> local1 = <>c.<>9__36_0;
                    evaluator = <>c.<>9__36_0 = x => x.IsLoaded;
                }
                if (this.PreviewRoot.Document.Return<IDocumentViewModel, bool>(evaluator, (<>c.<>9__36_1 ??= () => false)) && ((this.PreviewRoot.DocumentPresenter.ItemsPanel != null) && this.PreviewRoot.Document.Pages.Any<PageViewModel>()))
                {
                    if ((this.PreviewRoot.InputHitTest(p) == base.AssociatedObject) && (Mouse.LeftButton == MouseButtonState.Pressed))
                    {
                        Func<DocumentPresenterControl, bool> func2 = <>c.<>9__36_2;
                        if (<>c.<>9__36_2 == null)
                        {
                            Func<DocumentPresenterControl, bool> local3 = <>c.<>9__36_2;
                            func2 = <>c.<>9__36_2 = x => x.SelectionService.CanSelect;
                        }
                        if (this.PreviewRoot.DocumentPresenter.Return<DocumentPresenterControl, bool>(func2, <>c.<>9__36_3 ??= () => false))
                        {
                            CursorHelper.SetCursor(base.AssociatedObject, PreviewCursors.Cross);
                            return;
                        }
                    }
                    Brick hitTestBrick = this.GetHitTestBrick();
                    if ((hitTestBrick != null) && !string.IsNullOrEmpty(hitTestBrick.Url))
                    {
                        CursorHelper.SetCursor(base.AssociatedObject, Cursors.Hand);
                    }
                    else
                    {
                        CursorHelper.SetCursor(base.AssociatedObject, Cursors.Arrow);
                    }
                }
            }
        }

        private void UpdateCursorTick(object sender, EventArgs e)
        {
            if (!ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers))
            {
                this.UpdateCursorTimer.Stop();
                this.SetDefaultCursor();
            }
        }

        public CursorModeType CursorMode
        {
            get => 
                (CursorModeType) base.GetValue(CursorModeProperty);
            set => 
                base.SetValue(CursorModeProperty, value);
        }

        private WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> PreviewKeyDownHandler { get; set; }

        private WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> PreviewKeyUpHandler { get; set; }

        private DispatcherTimer UpdateCursorTimer { get; set; }

        private FrameworkElement CursorRoot { get; set; }

        private DocumentPreviewControl PreviewRoot { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CursorAttachedBehavior.<>c <>9 = new CursorAttachedBehavior.<>c();
            public static Action<CursorAttachedBehavior, object, KeyEventArgs> <>9__25_0;
            public static Action<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, object> <>9__25_1;
            public static Func<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, KeyEventHandler> <>9__25_2;
            public static Action<CursorAttachedBehavior, object, KeyEventArgs> <>9__25_3;
            public static Action<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, object> <>9__25_4;
            public static Func<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, KeyEventHandler> <>9__25_5;
            public static Func<IDocumentViewModel, bool> <>9__36_0;
            public static Func<bool> <>9__36_1;
            public static Func<DocumentPresenterControl, bool> <>9__36_2;
            public static Func<bool> <>9__36_3;

            internal void <.cctor>b__42_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((CursorAttachedBehavior) obj).OnCursorModeChanged((CursorModeType) args.NewValue);
            }

            internal void <.ctor>b__25_0(CursorAttachedBehavior behavior, object sender, KeyEventArgs args)
            {
                behavior.PreviewKeyDownHandlerInternal(args);
            }

            internal void <.ctor>b__25_1(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h, object sender)
            {
                ((FrameworkElement) sender).PreviewKeyDown -= h.Handler;
            }

            internal KeyEventHandler <.ctor>b__25_2(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h) => 
                new KeyEventHandler(h.OnEvent);

            internal void <.ctor>b__25_3(CursorAttachedBehavior behavior, object sender, KeyEventArgs args)
            {
                behavior.PreviewKeyUpHandlerInternal(args);
            }

            internal void <.ctor>b__25_4(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h, object sender)
            {
                ((FrameworkElement) sender).PreviewKeyUp -= h.Handler;
            }

            internal KeyEventHandler <.ctor>b__25_5(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h) => 
                new KeyEventHandler(h.OnEvent);

            internal bool <SetSelectToolModeCursor>b__36_0(IDocumentViewModel x) => 
                x.IsLoaded;

            internal bool <SetSelectToolModeCursor>b__36_1() => 
                false;

            internal bool <SetSelectToolModeCursor>b__36_2(DocumentPresenterControl x) => 
                x.SelectionService.CanSelect;

            internal bool <SetSelectToolModeCursor>b__36_3() => 
                false;
        }
    }
}

