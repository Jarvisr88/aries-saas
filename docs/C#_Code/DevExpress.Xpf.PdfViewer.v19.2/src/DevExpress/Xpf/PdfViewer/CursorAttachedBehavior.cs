namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class CursorAttachedBehavior : Behavior<DevExpress.Xpf.PdfViewer.DXScrollViewer>
    {
        private readonly TimeSpan UpdateCursorTimeSpan;
        public static readonly DependencyProperty CursorModeProperty;

        static CursorAttachedBehavior()
        {
            CursorModeProperty = DependencyPropertyManager.Register("CursorMode", typeof(CursorModeType), typeof(CursorAttachedBehavior), new PropertyMetadata(CursorModeType.SelectTool, (obj, args) => ((CursorAttachedBehavior) obj).OnCursorModeChanged((CursorModeType) args.NewValue)));
        }

        public CursorAttachedBehavior()
        {
            // Unresolved stack state at '000001DF'
        }

        private Point GetMousePoint(MouseEventArgs e) => 
            ((e == null) || (this.PdfViewerRoot == null)) ? new Point(0.0, 0.0) : e.GetPosition(this.PdfViewerRoot);

        protected override void OnAttached()
        {
            base.OnAttached();
            this.UpdateCursorTimer = new DispatcherTimer(this.UpdateCursorTimeSpan, DispatcherPriority.Normal, new EventHandler(this.UpdateCursorTick), base.AssociatedObject.Dispatcher);
            this.UpdateCursorTimer.Stop();
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonDown);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonUp);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).PreviewMouseMove += new MouseEventHandler(this.OnPreviewMouseMove);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).MouseEnter += new MouseEventHandler(this.OnMouseEnter);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).MouseLeave += new MouseEventHandler(this.OnMouseLeave);
            this.CursorRoot = LayoutHelper.FindRoot(base.AssociatedObject, false) as FrameworkElement;
            this.CursorRoot.Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.PreviewKeyDown += this.PreviewKeyDownHandler.Handler;
            });
            this.CursorRoot.Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.PreviewKeyUp += this.PreviewKeyUpHandler.Handler;
            });
            this.PdfViewerRoot.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.SelectionStarted += this.SelectionStartedChangedHandler.Handler;
            });
            this.PdfViewerRoot.Do<PdfViewerControl>(delegate (PdfViewerControl x) {
                x.SelectionEnded += this.SelectionEndedChangedHandler.Handler;
            });
        }

        protected virtual void OnCursorModeChanged(CursorModeType newValue)
        {
            this.UpdateCursor();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonDown);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonUp);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).PreviewMouseMove -= new MouseEventHandler(this.OnPreviewMouseMove);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).MouseEnter -= new MouseEventHandler(this.OnMouseEnter);
            ((PdfPagesSelector) base.AssociatedObject.TemplatedParent).MouseLeave -= new MouseEventHandler(this.OnMouseLeave);
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
                this.UpdateCursor(e);
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
            this.UpdateCursor(e);
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.UpdateCursor(e);
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!MouseHelper.IsMouseLeftButtonPressed(e) && (this.PdfViewerRoot != null))
            {
                this.UpdateCursor();
            }
        }

        private void PreviewKeyDownHandlerInternal(KeyEventArgs args)
        {
            this.UpdateCursor();
            this.UpdateCursorTimer.Stop();
            this.UpdateCursorTimer.Start();
        }

        private void PreviewKeyUpHandlerInternal(KeyEventArgs args)
        {
            this.UpdateCursor();
            this.UpdateCursorTimer.Stop();
        }

        private void SelectionChangedHandlerInternal(SelectionEventArgs args)
        {
            if (this.PdfViewerRoot != null)
            {
                this.UpdateCursor(this.PdfViewerRoot.ConvertDocumentPositionToPixel(args.DocumentPosition));
            }
        }

        private void SetMarqueeZoomCursor()
        {
            if (KeyboardHelper.IsControlPressed)
            {
                Func<PdfViewerControl, BehaviorProvider> evaluator = <>c.<>9__53_0;
                if (<>c.<>9__53_0 == null)
                {
                    Func<PdfViewerControl, BehaviorProvider> local1 = <>c.<>9__53_0;
                    evaluator = <>c.<>9__53_0 = x => x.ActualBehaviorProvider;
                }
                Func<BehaviorProvider, bool> func3 = <>c.<>9__53_1;
                if (<>c.<>9__53_1 == null)
                {
                    Func<BehaviorProvider, bool> local2 = <>c.<>9__53_1;
                    func3 = <>c.<>9__53_1 = x => x.CanZoomOut();
                }
                if (this.PdfViewerRoot.With<PdfViewerControl, BehaviorProvider>(evaluator).Return<BehaviorProvider, bool>(func3, <>c.<>9__53_2 ??= () => true))
                {
                    CursorHelper.SetCursor(base.AssociatedObject, PdfCursors.ZoomOutCursor);
                }
                else
                {
                    CursorHelper.SetCursor(base.AssociatedObject, PdfCursors.ZoomLimitCursor);
                }
            }
            else
            {
                Func<PdfViewerControl, BehaviorProvider> evaluator = <>c.<>9__53_3;
                if (<>c.<>9__53_3 == null)
                {
                    Func<PdfViewerControl, BehaviorProvider> local4 = <>c.<>9__53_3;
                    evaluator = <>c.<>9__53_3 = x => x.ActualBehaviorProvider;
                }
                Func<BehaviorProvider, bool> func4 = <>c.<>9__53_4;
                if (<>c.<>9__53_4 == null)
                {
                    Func<BehaviorProvider, bool> local5 = <>c.<>9__53_4;
                    func4 = <>c.<>9__53_4 = x => x.CanZoomIn();
                }
                if (this.PdfViewerRoot.With<PdfViewerControl, BehaviorProvider>(evaluator).Return<BehaviorProvider, bool>(func4, <>c.<>9__53_5 ??= () => true))
                {
                    CursorHelper.SetCursor(base.AssociatedObject, PdfCursors.ZoomInCursor);
                }
                else
                {
                    CursorHelper.SetCursor(base.AssociatedObject, PdfCursors.ZoomLimitCursor);
                }
            }
        }

        private void UpdateCursor()
        {
            this.UpdateCursor((MouseEventArgs) null);
        }

        private void UpdateCursor(MouseEventArgs e)
        {
            Point mousePoint = this.GetMousePoint(e);
            this.UpdateCursor(mousePoint);
        }

        private void UpdateCursor(Point point)
        {
            Func<PdfViewerControl, PdfDocumentViewModel> evaluator = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                Func<PdfViewerControl, PdfDocumentViewModel> local1 = <>c.<>9__51_0;
                evaluator = <>c.<>9__51_0 = x => x.Document as PdfDocumentViewModel;
            }
            Func<PdfDocumentViewModel, PdfDocumentStateController> func2 = <>c.<>9__51_1;
            if (<>c.<>9__51_1 == null)
            {
                Func<PdfDocumentViewModel, PdfDocumentStateController> local2 = <>c.<>9__51_1;
                func2 = <>c.<>9__51_1 = x => x.DocumentStateController;
            }
            Func<PdfDocumentStateController, PdfCursor> func3 = <>c.<>9__51_2;
            if (<>c.<>9__51_2 == null)
            {
                Func<PdfDocumentStateController, PdfCursor> local3 = <>c.<>9__51_2;
                func3 = <>c.<>9__51_2 = x => x.CurrentCursor;
            }
            switch (this.PdfViewerRoot.With<PdfViewerControl, PdfDocumentViewModel>(evaluator).With<PdfDocumentViewModel, PdfDocumentStateController>(func2).Return<PdfDocumentStateController, PdfCursor>(func3, (<>c.<>9__51_3 ??= () => PdfCursor.Default)))
            {
                case PdfCursor.TextSelection:
                    CursorHelper.SetCursor(base.AssociatedObject, Cursors.IBeam);
                    return;

                case PdfCursor.ImageSelection:
                    CursorHelper.SetCursor(base.AssociatedObject, PdfCursors.CrossCursor);
                    return;

                case PdfCursor.Annotation:
                    CursorHelper.SetCursor(base.AssociatedObject, Cursors.Hand);
                    return;

                case PdfCursor.SelectionContext:
                    CursorHelper.SetCursor(base.AssociatedObject, PdfCursors.ContextCursor);
                    return;

                case PdfCursor.HandTool:
                    CursorHelper.SetCursor(base.AssociatedObject, (Mouse.LeftButton == MouseButtonState.Pressed) ? PdfCursors.HandDragCursor : PdfCursors.HandCursor);
                    return;

                case PdfCursor.MarqueZoomTool:
                    this.SetMarqueeZoomCursor();
                    return;

                case PdfCursor.TextMarkupTool:
                    CursorHelper.SetCursor(base.AssociatedObject, PdfCursors.TextMarkupCursor);
                    return;
            }
            CursorHelper.SetCursor(base.AssociatedObject, Cursors.Arrow);
        }

        private void UpdateCursorTick(object sender, EventArgs e)
        {
            if (!ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers))
            {
                this.UpdateCursorTimer.Stop();
                this.UpdateCursor();
            }
        }

        private void ZoomChangedHandlerInternal(RoutedEventArgs args)
        {
            this.UpdateCursor();
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

        private WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler> SelectionStartedChangedHandler { get; set; }

        private WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler> SelectionEndedChangedHandler { get; set; }

        private WeakEventHandler<CursorAttachedBehavior, RoutedEventArgs, RoutedEventHandler> ZoomChangedHandler { get; set; }

        private DispatcherTimer UpdateCursorTimer { get; set; }

        private FrameworkElement CursorRoot { get; set; }

        private PdfViewerControl PdfViewerRoot =>
            (PdfViewerControl) base.AssociatedObject.With<DevExpress.Xpf.PdfViewer.DXScrollViewer, IDocumentViewerControl>(new Func<DevExpress.Xpf.PdfViewer.DXScrollViewer, IDocumentViewerControl>(DocumentViewerControl.GetActualViewer));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CursorAttachedBehavior.<>c <>9 = new CursorAttachedBehavior.<>c();
            public static Action<CursorAttachedBehavior, object, KeyEventArgs> <>9__35_0;
            public static Action<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, object> <>9__35_1;
            public static Func<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, KeyEventHandler> <>9__35_2;
            public static Action<CursorAttachedBehavior, object, KeyEventArgs> <>9__35_3;
            public static Action<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, object> <>9__35_4;
            public static Func<WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler>, KeyEventHandler> <>9__35_5;
            public static Action<CursorAttachedBehavior, object, SelectionEventArgs> <>9__35_6;
            public static Action<WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler>, object> <>9__35_7;
            public static Func<WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler>, SelectionEventHandler> <>9__35_8;
            public static Action<CursorAttachedBehavior, object, SelectionEventArgs> <>9__35_9;
            public static Action<WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler>, object> <>9__35_10;
            public static Func<WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler>, SelectionEventHandler> <>9__35_11;
            public static Action<CursorAttachedBehavior, object, RoutedEventArgs> <>9__35_12;
            public static Action<WeakEventHandler<CursorAttachedBehavior, RoutedEventArgs, RoutedEventHandler>, object> <>9__35_13;
            public static Func<WeakEventHandler<CursorAttachedBehavior, RoutedEventArgs, RoutedEventHandler>, RoutedEventHandler> <>9__35_14;
            public static Func<PdfViewerControl, PdfDocumentViewModel> <>9__51_0;
            public static Func<PdfDocumentViewModel, PdfDocumentStateController> <>9__51_1;
            public static Func<PdfDocumentStateController, PdfCursor> <>9__51_2;
            public static Func<PdfCursor> <>9__51_3;
            public static Func<PdfViewerControl, BehaviorProvider> <>9__53_0;
            public static Func<BehaviorProvider, bool> <>9__53_1;
            public static Func<bool> <>9__53_2;
            public static Func<PdfViewerControl, BehaviorProvider> <>9__53_3;
            public static Func<BehaviorProvider, bool> <>9__53_4;
            public static Func<bool> <>9__53_5;

            internal void <.cctor>b__54_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((CursorAttachedBehavior) obj).OnCursorModeChanged((CursorModeType) args.NewValue);
            }

            internal void <.ctor>b__35_0(CursorAttachedBehavior behavior, object sender, KeyEventArgs args)
            {
                behavior.PreviewKeyDownHandlerInternal(args);
            }

            internal void <.ctor>b__35_1(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h, object sender)
            {
                ((FrameworkElement) sender).PreviewKeyDown -= h.Handler;
            }

            internal void <.ctor>b__35_10(WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler> h, object sender)
            {
                ((PdfViewerControl) sender).SelectionEnded -= h.Handler;
            }

            internal SelectionEventHandler <.ctor>b__35_11(WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler> h) => 
                new SelectionEventHandler(h.OnEvent);

            internal void <.ctor>b__35_12(CursorAttachedBehavior behavior, object sender, RoutedEventArgs args)
            {
                behavior.ZoomChangedHandlerInternal(args);
            }

            internal void <.ctor>b__35_13(WeakEventHandler<CursorAttachedBehavior, RoutedEventArgs, RoutedEventHandler> h, object sender)
            {
                ((PdfViewerControl) sender).ZoomChanged -= h.Handler;
            }

            internal RoutedEventHandler <.ctor>b__35_14(WeakEventHandler<CursorAttachedBehavior, RoutedEventArgs, RoutedEventHandler> h) => 
                new RoutedEventHandler(h.OnEvent);

            internal KeyEventHandler <.ctor>b__35_2(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h) => 
                new KeyEventHandler(h.OnEvent);

            internal void <.ctor>b__35_3(CursorAttachedBehavior behavior, object sender, KeyEventArgs args)
            {
                behavior.PreviewKeyUpHandlerInternal(args);
            }

            internal void <.ctor>b__35_4(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h, object sender)
            {
                ((FrameworkElement) sender).PreviewKeyUp -= h.Handler;
            }

            internal KeyEventHandler <.ctor>b__35_5(WeakEventHandler<CursorAttachedBehavior, KeyEventArgs, KeyEventHandler> h) => 
                new KeyEventHandler(h.OnEvent);

            internal void <.ctor>b__35_6(CursorAttachedBehavior behavior, object sender, SelectionEventArgs args)
            {
                behavior.SelectionChangedHandlerInternal(args);
            }

            internal void <.ctor>b__35_7(WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler> h, object sender)
            {
                ((PdfViewerControl) sender).SelectionStarted -= h.Handler;
            }

            internal SelectionEventHandler <.ctor>b__35_8(WeakEventHandler<CursorAttachedBehavior, SelectionEventArgs, SelectionEventHandler> h) => 
                new SelectionEventHandler(h.OnEvent);

            internal void <.ctor>b__35_9(CursorAttachedBehavior behavior, object sender, SelectionEventArgs args)
            {
                behavior.SelectionChangedHandlerInternal(args);
            }

            internal BehaviorProvider <SetMarqueeZoomCursor>b__53_0(PdfViewerControl x) => 
                x.ActualBehaviorProvider;

            internal bool <SetMarqueeZoomCursor>b__53_1(BehaviorProvider x) => 
                x.CanZoomOut();

            internal bool <SetMarqueeZoomCursor>b__53_2() => 
                true;

            internal BehaviorProvider <SetMarqueeZoomCursor>b__53_3(PdfViewerControl x) => 
                x.ActualBehaviorProvider;

            internal bool <SetMarqueeZoomCursor>b__53_4(BehaviorProvider x) => 
                x.CanZoomIn();

            internal bool <SetMarqueeZoomCursor>b__53_5() => 
                true;

            internal PdfDocumentViewModel <UpdateCursor>b__51_0(PdfViewerControl x) => 
                x.Document as PdfDocumentViewModel;

            internal PdfDocumentStateController <UpdateCursor>b__51_1(PdfDocumentViewModel x) => 
                x.DocumentStateController;

            internal PdfCursor <UpdateCursor>b__51_2(PdfDocumentStateController x) => 
                x.CurrentCursor;

            internal PdfCursor <UpdateCursor>b__51_3() => 
                PdfCursor.Default;
        }
    }
}

