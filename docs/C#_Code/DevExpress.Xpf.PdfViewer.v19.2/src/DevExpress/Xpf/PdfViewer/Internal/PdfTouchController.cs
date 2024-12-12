namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer;
    using DevExpress.Xpf.PdfViewer.Extensions;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PdfTouchController : TouchController
    {
        private const double LockRotateAngle = 25.0;
        private Point selectionPoint;
        private double manipulationRotate;
        private bool isSelectingInitialized;

        public PdfTouchController(DocumentPresenterControl presenter) : base(presenter)
        {
        }

        private bool IsOutsidePresenter(Point position) => 
            position.X.GreaterThan(this.ItemsPanel.ActualWidth) || (position.X.LessThan(0.0) || (position.Y.GreaterThan(this.ItemsPanel.ActualHeight) || position.Y.LessThan(0.0)));

        public override void ProcessManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            base.ProcessManipulationCompleted(e);
            base.LockManipulationZooming = false;
            if (this.isSelectingInitialized)
            {
                if (this.CursorMode == CursorModeType.MarqueeZoom)
                {
                    this.KeyboardAndMouseController.ProcessMarqueeZoom();
                }
                else
                {
                    this.Document.DocumentStateController.MouseUp(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(this.selectionPoint), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 1));
                }
                this.KeyboardAndMouseController.ReleaseSelectionRectangle();
                this.isSelectingInitialized = false;
            }
            this.manipulationRotate = 0.0;
        }

        public override void ProcessManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if ((e.Manipulators.Count<IManipulator>() < 2) && e.DeltaManipulation.Translation.Length.GreaterThan(0.0))
            {
                e.Handled = true;
                if (this.CursorMode == CursorModeType.HandTool)
                {
                    this.ScrollViewer.ScrollToVerticalOffset(this.ScrollViewer.VerticalOffset - e.DeltaManipulation.Translation.Y);
                    this.ScrollViewer.ScrollToHorizontalOffset(this.ScrollViewer.HorizontalOffset - e.DeltaManipulation.Translation.X);
                }
                else
                {
                    this.ProcessSelectionStart();
                    Point selectionPoint = this.selectionPoint;
                    selectionPoint.Offset(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
                    this.selectionPoint = selectionPoint;
                    this.KeyboardAndMouseController.UpdateSelectionRectangle(selectionPoint);
                    if (!this.KeyboardAndMouseController.IsSelecting)
                    {
                        this.ProcessTouchMoveInternal(selectionPoint);
                    }
                    else
                    {
                        this.ProcessSelectionMouseMove(selectionPoint);
                    }
                }
            }
            else
            {
                this.manipulationRotate += e.DeltaManipulation.Rotation;
                if (Math.Abs(this.manipulationRotate).GreaterThan(25.0))
                {
                    base.LockManipulationZooming ??= true;
                }
                if (Math.Abs(this.manipulationRotate).GreaterThanOrClose(this.MinManipulationRotationAngle))
                {
                    if (this.manipulationRotate.LessThan(0.0))
                    {
                        Func<PdfCommandProvider, ICommand> evaluator = <>c.<>9__29_0;
                        if (<>c.<>9__29_0 == null)
                        {
                            Func<PdfCommandProvider, ICommand> local1 = <>c.<>9__29_0;
                            evaluator = <>c.<>9__29_0 = x => x.CounterClockwiseRotateCommand;
                        }
                        Action<ICommand> action = <>c.<>9__29_1;
                        if (<>c.<>9__29_1 == null)
                        {
                            Action<ICommand> local2 = <>c.<>9__29_1;
                            action = <>c.<>9__29_1 = x => x.Execute(null);
                        }
                        this.CommandProvider.With<PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    }
                    else
                    {
                        Func<PdfCommandProvider, ICommand> evaluator = <>c.<>9__29_2;
                        if (<>c.<>9__29_2 == null)
                        {
                            Func<PdfCommandProvider, ICommand> local3 = <>c.<>9__29_2;
                            evaluator = <>c.<>9__29_2 = x => x.ClockwiseRotateCommand;
                        }
                        Action<ICommand> action = <>c.<>9__29_3;
                        if (<>c.<>9__29_3 == null)
                        {
                            Action<ICommand> local4 = <>c.<>9__29_3;
                            action = <>c.<>9__29_3 = x => x.Execute(null);
                        }
                        this.CommandProvider.With<PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    }
                    this.manipulationRotate = 0.0;
                    e.Handled = true;
                }
                base.ProcessManipulationDelta(e);
            }
        }

        public override void ProcessManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            base.ProcessManipulationInertiaStarting(e);
            if (this.CursorMode == CursorModeType.HandTool)
            {
                e.TranslationBehavior.DesiredDeceleration = 0.00096;
                e.Handled = true;
            }
        }

        public override void ProcessManipulationStarted(ManipulationStartedEventArgs e)
        {
            base.ProcessManipulationStarted(e);
            Point position = e.Manipulators.First<IManipulator>().GetPosition(this.ItemsPanel);
            this.selectionPoint = position;
        }

        private void ProcessSelectionMouseMove(Point point)
        {
            bool isOutsideOfView = this.IsOutsidePresenter(point);
            this.Document.DocumentStateController.MouseMove(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(this.SelectionRectangle.AnchorPoint), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 0, isOutsideOfView));
        }

        private void ProcessSelectionStart()
        {
            if (!this.isSelectingInitialized)
            {
                this.isSelectingInitialized = true;
                if (!this.Presenter.IsInEditing)
                {
                    Action<PdfPresenterControl> action = <>c.<>9__28_0;
                    if (<>c.<>9__28_0 == null)
                    {
                        Action<PdfPresenterControl> local1 = <>c.<>9__28_0;
                        action = <>c.<>9__28_0 = x => x.Focus();
                    }
                    this.Presenter.Do<PdfPresenterControl>(action);
                }
                if ((this.Presenter.Document != null) && ((this.CursorMode != CursorModeType.MarqueeZoom) || ((!KeyboardHelper.IsControlPressed || this.BehaviorProvider.CanZoomOut()) && (KeyboardHelper.IsControlPressed || this.BehaviorProvider.CanZoomIn()))))
                {
                    if (this.CursorMode != CursorModeType.MarqueeZoom)
                    {
                        this.Document.DocumentStateController.MouseDown(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(this.selectionPoint), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 1));
                    }
                    this.KeyboardAndMouseController.SetupSelectionRectangle(this.selectionPoint);
                }
            }
        }

        public override void ProcessStylusSystemGesture(StylusSystemGestureEventArgs e)
        {
            base.ProcessStylusSystemGesture(e);
            Point position = e.GetPosition(this.ItemsPanel);
            SystemGesture systemGesture = e.SystemGesture;
            if (systemGesture == SystemGesture.Tap)
            {
                this.Document.DocumentStateController.MouseDown(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 1));
                this.Document.DocumentStateController.MouseUp(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 1));
                Func<IPopupControl, bool> evaluator = <>c.<>9__32_0;
                if (<>c.<>9__32_0 == null)
                {
                    Func<IPopupControl, bool> local1 = <>c.<>9__32_0;
                    evaluator = <>c.<>9__32_0 = x => x.IsPopupOpen;
                }
                if (BarManager.GetDXContextMenu(this.Presenter).Return<IPopupControl, bool>(evaluator, <>c.<>9__32_1 ??= () => false))
                {
                    BarManager.GetDXContextMenu(this.Presenter).ClosePopup();
                }
                e.Handled = true;
            }
            else if (systemGesture != SystemGesture.RightTap)
            {
                if (systemGesture == SystemGesture.HoldEnter)
                {
                    this.Document.DocumentStateController.MouseDown(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Right, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 1));
                    e.Handled = true;
                }
            }
            else
            {
                this.Document.DocumentStateController.MouseUp(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Right, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 1));
                e.Handled = true;
                BarManager.GetDXContextMenu(this.Presenter).Do<IPopupControl>(x => x.ShowPopup(this.Presenter));
                Func<IPopupControl, BarPopupBase> evaluator = <>c.<>9__32_3;
                if (<>c.<>9__32_3 == null)
                {
                    Func<IPopupControl, BarPopupBase> local3 = <>c.<>9__32_3;
                    evaluator = <>c.<>9__32_3 = x => x.Popup;
                }
                BarManager.GetDXContextMenu(this.Presenter).With<IPopupControl, BarPopupBase>(evaluator).Do<BarPopupBase>(x => x.VerticalOffset = -this.Presenter.ActualHeight + e.GetPosition(this.Presenter).Y);
                Func<IPopupControl, BarPopupBase> func3 = <>c.<>9__32_5;
                if (<>c.<>9__32_5 == null)
                {
                    Func<IPopupControl, BarPopupBase> local4 = <>c.<>9__32_5;
                    func3 = <>c.<>9__32_5 = x => x.Popup;
                }
                BarManager.GetDXContextMenu(this.Presenter).With<IPopupControl, BarPopupBase>(func3).Do<BarPopupBase>(x => x.HorizontalOffset = -this.Presenter.ActualWidth + e.GetPosition(this.Presenter).X);
            }
        }

        private void ProcessTouchMoveInternal(Point point)
        {
            bool isOutsideOfView = this.IsOutsidePresenter(point);
            this.Document.DocumentStateController.MouseMove(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(point), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 0, isOutsideOfView));
        }

        private PdfPresenterControl Presenter =>
            (PdfPresenterControl) base.Presenter;

        private PdfBehaviorProvider BehaviorProvider =>
            (PdfBehaviorProvider) base.BehaviorProvider;

        private PdfCommandProvider CommandProvider
        {
            get
            {
                Func<PdfPresenterControl, PdfViewerControl> evaluator = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<PdfPresenterControl, PdfViewerControl> local1 = <>c.<>9__6_0;
                    evaluator = <>c.<>9__6_0 = x => x.ActualPdfViewer;
                }
                Func<PdfViewerControl, DevExpress.Xpf.DocumentViewer.CommandProvider> func2 = <>c.<>9__6_1;
                if (<>c.<>9__6_1 == null)
                {
                    Func<PdfViewerControl, DevExpress.Xpf.DocumentViewer.CommandProvider> local2 = <>c.<>9__6_1;
                    func2 = <>c.<>9__6_1 = x => x.ActualCommandProvider;
                }
                return (PdfCommandProvider) this.Presenter.With<PdfPresenterControl, PdfViewerControl>(evaluator).With<PdfViewerControl, DevExpress.Xpf.DocumentViewer.CommandProvider>(func2);
            }
        }

        protected virtual double MinManipulationRotationAngle =>
            75.0;

        private DocumentViewerPanel ItemsPanel
        {
            get
            {
                Func<PdfPresenterControl, DocumentViewerPanel> evaluator = <>c.<>9__10_0;
                if (<>c.<>9__10_0 == null)
                {
                    Func<PdfPresenterControl, DocumentViewerPanel> local1 = <>c.<>9__10_0;
                    evaluator = <>c.<>9__10_0 = x => x.ItemsPanel;
                }
                return this.Presenter.With<PdfPresenterControl, DocumentViewerPanel>(evaluator);
            }
        }

        private CursorModeType CursorMode
        {
            get
            {
                Func<PdfPresenterControl, CursorModeType> evaluator = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<PdfPresenterControl, CursorModeType> local1 = <>c.<>9__12_0;
                    evaluator = <>c.<>9__12_0 = x => x.CursorMode;
                }
                return this.Presenter.Return<PdfPresenterControl, CursorModeType>(evaluator, (<>c.<>9__12_1 ??= () => CursorModeType.SelectTool));
            }
        }

        private PdfDocumentViewModel Document
        {
            get
            {
                Func<PdfPresenterControl, PdfDocumentViewModel> evaluator = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<PdfPresenterControl, PdfDocumentViewModel> local1 = <>c.<>9__14_0;
                    evaluator = <>c.<>9__14_0 = x => x.Document as PdfDocumentViewModel;
                }
                return this.Presenter.With<PdfPresenterControl, PdfDocumentViewModel>(evaluator);
            }
        }

        private PdfNavigationStrategy NavigationStrategy
        {
            get
            {
                Func<PdfPresenterControl, PdfNavigationStrategy> evaluator = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Func<PdfPresenterControl, PdfNavigationStrategy> local1 = <>c.<>9__16_0;
                    evaluator = <>c.<>9__16_0 = x => x.NavigationStrategy;
                }
                return this.Presenter.With<PdfPresenterControl, PdfNavigationStrategy>(evaluator);
            }
        }

        private System.Windows.Controls.ScrollViewer ScrollViewer
        {
            get
            {
                Func<PdfPresenterControl, System.Windows.Controls.ScrollViewer> evaluator = <>c.<>9__18_0;
                if (<>c.<>9__18_0 == null)
                {
                    Func<PdfPresenterControl, System.Windows.Controls.ScrollViewer> local1 = <>c.<>9__18_0;
                    evaluator = <>c.<>9__18_0 = x => x.ScrollViewer;
                }
                return this.Presenter.With<PdfPresenterControl, System.Windows.Controls.ScrollViewer>(evaluator);
            }
        }

        private DevExpress.Xpf.PdfViewer.SelectionRectangle SelectionRectangle
        {
            get
            {
                Func<PdfPresenterControl, DevExpress.Xpf.PdfViewer.SelectionRectangle> evaluator = <>c.<>9__20_0;
                if (<>c.<>9__20_0 == null)
                {
                    Func<PdfPresenterControl, DevExpress.Xpf.PdfViewer.SelectionRectangle> local1 = <>c.<>9__20_0;
                    evaluator = <>c.<>9__20_0 = x => x.SelectionRectangle;
                }
                return this.Presenter.With<PdfPresenterControl, DevExpress.Xpf.PdfViewer.SelectionRectangle>(evaluator);
            }
        }

        private PdfKeyboardAndMouseController KeyboardAndMouseController
        {
            get
            {
                Func<PdfPresenterControl, PdfKeyboardAndMouseController> evaluator = <>c.<>9__22_0;
                if (<>c.<>9__22_0 == null)
                {
                    Func<PdfPresenterControl, PdfKeyboardAndMouseController> local1 = <>c.<>9__22_0;
                    evaluator = <>c.<>9__22_0 = x => x.KeyboardAndMouseController as PdfKeyboardAndMouseController;
                }
                return this.Presenter.With<PdfPresenterControl, PdfKeyboardAndMouseController>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfTouchController.<>c <>9 = new PdfTouchController.<>c();
            public static Func<PdfPresenterControl, PdfViewerControl> <>9__6_0;
            public static Func<PdfViewerControl, CommandProvider> <>9__6_1;
            public static Func<PdfPresenterControl, DocumentViewerPanel> <>9__10_0;
            public static Func<PdfPresenterControl, CursorModeType> <>9__12_0;
            public static Func<CursorModeType> <>9__12_1;
            public static Func<PdfPresenterControl, PdfDocumentViewModel> <>9__14_0;
            public static Func<PdfPresenterControl, PdfNavigationStrategy> <>9__16_0;
            public static Func<PdfPresenterControl, ScrollViewer> <>9__18_0;
            public static Func<PdfPresenterControl, SelectionRectangle> <>9__20_0;
            public static Func<PdfPresenterControl, PdfKeyboardAndMouseController> <>9__22_0;
            public static Action<PdfPresenterControl> <>9__28_0;
            public static Func<PdfCommandProvider, ICommand> <>9__29_0;
            public static Action<ICommand> <>9__29_1;
            public static Func<PdfCommandProvider, ICommand> <>9__29_2;
            public static Action<ICommand> <>9__29_3;
            public static Func<IPopupControl, bool> <>9__32_0;
            public static Func<bool> <>9__32_1;
            public static Func<IPopupControl, BarPopupBase> <>9__32_3;
            public static Func<IPopupControl, BarPopupBase> <>9__32_5;

            internal PdfViewerControl <get_CommandProvider>b__6_0(PdfPresenterControl x) => 
                x.ActualPdfViewer;

            internal CommandProvider <get_CommandProvider>b__6_1(PdfViewerControl x) => 
                x.ActualCommandProvider;

            internal CursorModeType <get_CursorMode>b__12_0(PdfPresenterControl x) => 
                x.CursorMode;

            internal CursorModeType <get_CursorMode>b__12_1() => 
                CursorModeType.SelectTool;

            internal PdfDocumentViewModel <get_Document>b__14_0(PdfPresenterControl x) => 
                x.Document as PdfDocumentViewModel;

            internal DocumentViewerPanel <get_ItemsPanel>b__10_0(PdfPresenterControl x) => 
                x.ItemsPanel;

            internal PdfKeyboardAndMouseController <get_KeyboardAndMouseController>b__22_0(PdfPresenterControl x) => 
                x.KeyboardAndMouseController as PdfKeyboardAndMouseController;

            internal PdfNavigationStrategy <get_NavigationStrategy>b__16_0(PdfPresenterControl x) => 
                x.NavigationStrategy;

            internal ScrollViewer <get_ScrollViewer>b__18_0(PdfPresenterControl x) => 
                x.ScrollViewer;

            internal SelectionRectangle <get_SelectionRectangle>b__20_0(PdfPresenterControl x) => 
                x.SelectionRectangle;

            internal ICommand <ProcessManipulationDelta>b__29_0(PdfCommandProvider x) => 
                x.CounterClockwiseRotateCommand;

            internal void <ProcessManipulationDelta>b__29_1(ICommand x)
            {
                x.Execute(null);
            }

            internal ICommand <ProcessManipulationDelta>b__29_2(PdfCommandProvider x) => 
                x.ClockwiseRotateCommand;

            internal void <ProcessManipulationDelta>b__29_3(ICommand x)
            {
                x.Execute(null);
            }

            internal void <ProcessSelectionStart>b__28_0(PdfPresenterControl x)
            {
                x.Focus();
            }

            internal bool <ProcessStylusSystemGesture>b__32_0(IPopupControl x) => 
                x.IsPopupOpen;

            internal bool <ProcessStylusSystemGesture>b__32_1() => 
                false;

            internal BarPopupBase <ProcessStylusSystemGesture>b__32_3(IPopupControl x) => 
                x.Popup;

            internal BarPopupBase <ProcessStylusSystemGesture>b__32_5(IPopupControl x) => 
                x.Popup;
        }
    }
}

