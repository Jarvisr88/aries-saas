namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class InteractionProvider
    {
        private DispatcherTimer syncTimer;
        private const double ScrollXOffset = 10.0;
        private const double ScrollYOffset = 10.0;
        private const double LargeModifier = 3.0;
        private const double LargeStep = 100.0;

        public InteractionProvider(DocumentPresenterControl presenter)
        {
            this.DocumentPresenter = presenter;
        }

        private double CalcScrollXOffset(Point position)
        {
            double num = position.X.GreaterThan(this.DocumentPresenter.ItemsPanel.ActualWidth) ? (position.X - this.DocumentPresenter.ItemsPanel.ActualWidth) : 0.0;
            num = position.X.LessThan(0.0) ? position.X : num;
            return ((Math.Min(Math.Abs(num), 10.0) * Math.Sign(num)) * (this.IsLargeStep(num) ? 3.0 : 1.0));
        }

        private double CalcScrollYOffset(Point position)
        {
            double num = position.Y.GreaterThan(this.DocumentPresenter.ItemsPanel.ActualHeight) ? (position.Y - this.DocumentPresenter.ItemsPanel.ActualHeight) : 0.0;
            num = position.Y.LessThan(0.0) ? position.Y : num;
            return ((Math.Min(Math.Abs(num), 10.0) * Math.Sign(num)) * (this.IsLargeStep(num) ? 3.0 : 1.0));
        }

        public void ContinueSelection(Point point)
        {
            if (this.DocumentPresenter.CursorMode == CursorModeType.SelectTool)
            {
                Mouse.Capture(this.DocumentPresenter, CaptureMode.SubTree);
                this.UpdateSelectionRectangle(point);
                this.DocumentPreview.RaiseSelectionContinued();
                if (this.syncTimer == null)
                {
                    this.syncTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(10.0), DispatcherPriority.ApplicationIdle, delegate (object o, EventArgs e) {
                        if (!this.IsInSelecting)
                        {
                            this.EndSelection(point);
                        }
                        Point position = MouseHelper.GetPosition(this.DocumentPresenter);
                        this.UpdateSelectionRectangle(MouseHelper.GetPosition(this.DocumentPresenter));
                        if (Mouse.LeftButton != MouseButtonState.Pressed)
                        {
                            this.EndSelection(point);
                        }
                    }, this.DocumentPresenter.Dispatcher);
                    this.syncTimer.Start();
                }
            }
        }

        public bool EndEdit() => 
            this.DocumentPresenter.EditingStrategy.EndEditing();

        public void EndSelection(Point point)
        {
            if ((this.DocumentPresenter.CursorMode == CursorModeType.SelectTool) && this.CanSelect)
            {
                this.SelectionService.OnMouseUp(point.ToWinFormsPoint(), MouseButtons.Left, ModifierKeysHelper.GetKeyboardModifiers().ToWinFormsModifierKeys());
                Action<DispatcherTimer> action = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Action<DispatcherTimer> local1 = <>c.<>9__30_0;
                    action = <>c.<>9__30_0 = x => x.Stop();
                }
                this.syncTimer.Do<DispatcherTimer>(action);
                this.syncTimer = null;
                this.ReleaseSelectionRectangle();
                this.CanSelect = false;
                this.DocumentPreview.RaiseSelectionEnded();
            }
        }

        public void InitializeSelection(Point startPoint)
        {
            if (this.DocumentPresenter.CursorMode == CursorModeType.SelectTool)
            {
                this.SelectionService.OnMouseDown(startPoint.ToWinFormsPoint(), MouseButton.Left.ToWinFormsMouseButtons(), ModifierKeysHelper.GetKeyboardModifiers().ToWinFormsModifierKeys());
                this.CanSelect = true;
            }
        }

        private bool IsLargeStep(double delta) => 
            Math.Abs(delta) > 100.0;

        private bool IsMouseOutsidePresenter(Point cursorPosition) => 
            cursorPosition.X.GreaterThan(this.DocumentPresenter.ItemsPanel.ActualWidth) || (cursorPosition.X.LessThan(0.0) || (cursorPosition.Y.GreaterThan(this.DocumentPresenter.ItemsPanel.ActualHeight) || cursorPosition.Y.LessThan(0.0)));

        internal void OnScrollChanged(ScrollChangedEventArgs e)
        {
            if (this.DocumentPresenter.CursorMode == CursorModeType.SelectTool)
            {
                this.SelectionService.CorrectStartPoint(-e.VerticalChange);
            }
        }

        private void ReleaseSelectionRectangle()
        {
            this.IsInSelecting = false;
            this.SelectionRectangle.Reset();
            this.DocumentPresenter.ReleaseMouseCapture();
        }

        public void ResetSelection(bool resetAll = false)
        {
            if (resetAll)
            {
                this.SelectionService.ResetAll();
            }
            else
            {
                this.SelectionService.ResetSelectedBricks();
            }
        }

        private void SetupSelectionRectangle(Point cursorPosition)
        {
            this.IsInSelecting = true;
            this.SelectionRectangle.SetVerticalOffset(this.ScrollViewer.VerticalOffset, false);
            this.SelectionRectangle.SetHorizontalOffset(this.ScrollViewer.HorizontalOffset, false);
            this.SelectionRectangle.SetViewport(new Size(Math.Max(this.ScrollViewer.ViewportWidth, this.ScrollViewer.ExtentWidth), Math.Max(this.ScrollViewer.ViewportHeight, this.ScrollViewer.ExtentHeight)));
            this.SelectionRectangle.SetStartPoint(cursorPosition);
        }

        public void StartEdit(PageViewModel pageModel, EditingField editingField)
        {
            if ((this.DocumentPresenter.ActiveEditorOwner != null) && !ReferenceEquals(this.DocumentPresenter.ActiveEditorOwner.CurrentCellEditor.EditingField, editingField))
            {
                this.EndEdit();
            }
            else if ((this.DocumentPresenter.ActiveEditorOwner != null) && ReferenceEquals(this.DocumentPresenter.ActiveEditorOwner.CurrentCellEditor.EditingField, editingField))
            {
                return;
            }
            this.DocumentPresenter.EditingStrategy.StartEditing(pageModel, editingField);
        }

        public void StartSelection(Point point)
        {
            if (this.DocumentPresenter.CursorMode == CursorModeType.SelectTool)
            {
                this.SelectionService.OnMouseMove(point.ToWinFormsPoint(), MouseButtons.Left, ModifierKeysHelper.GetKeyboardModifiers().ToWinFormsModifierKeys());
                this.SetupSelectionRectangle(point);
                bool canSelect = this.SelectionService.CanSelect;
                this.DocumentPreview.RaiseSelectionStarted();
            }
        }

        private void UpdateSelectionRectangle(Point cursorPosition)
        {
            this.SelectionService.OnMouseMove(cursorPosition.ToWinFormsPoint(), MouseButtons.Left, ModifierKeysHelper.GetKeyboardModifiers().ToWinFormsModifierKeys());
            if (this.IsInSelecting)
            {
                this.SelectionRectangle.SetPointPosition(cursorPosition);
                if (this.IsMouseOutsidePresenter(cursorPosition))
                {
                    this.ScrollViewer.ScrollToHorizontalOffset(this.ScrollViewer.HorizontalOffset + this.CalcScrollXOffset(cursorPosition));
                    this.SelectionRectangle.SetHorizontalOffset(this.ScrollViewer.HorizontalOffset, true);
                    this.ScrollViewer.ScrollToVerticalOffset(this.ScrollViewer.VerticalOffset + this.CalcScrollYOffset(cursorPosition));
                    this.SelectionRectangle.SetVerticalOffset(this.ScrollViewer.VerticalOffset, true);
                    this.SelectionService.SetStartPoint(this.SelectionRectangle.StartPoint.ToWinFormsPoint());
                }
            }
        }

        private DocumentPresenterControl DocumentPresenter { get; set; }

        private DocumentPreviewControl DocumentPreview =>
            this.DocumentPresenter.ActualDocumentViewer;

        private DocumentNavigationStrategy NavigationStrategy
        {
            get
            {
                Func<DocumentPresenterControl, DocumentNavigationStrategy> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<DocumentPresenterControl, DocumentNavigationStrategy> local1 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = x => x.NavigationStrategy;
                }
                return this.DocumentPresenter.With<DocumentPresenterControl, DocumentNavigationStrategy>(evaluator);
            }
        }

        private System.Windows.Controls.ScrollViewer ScrollViewer =>
            this.DocumentPresenter.ScrollViewer;

        private DevExpress.Xpf.Printing.PreviewControl.Native.SelectionRectangle SelectionRectangle =>
            this.DocumentPresenter.SelectionRectangle;

        private DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService SelectionService =>
            this.DocumentPresenter.SelectionService;

        public bool CanSelect { get; private set; }

        public bool IsInSelecting { get; private set; }

        public bool IsInEditing =>
            this.DocumentPresenter.EditingStrategy.ActiveField != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InteractionProvider.<>c <>9 = new InteractionProvider.<>c();
            public static Func<DocumentPresenterControl, DocumentNavigationStrategy> <>9__7_0;
            public static Action<DispatcherTimer> <>9__30_0;

            internal void <EndSelection>b__30_0(DispatcherTimer x)
            {
                x.Stop();
            }

            internal DocumentNavigationStrategy <get_NavigationStrategy>b__7_0(DocumentPresenterControl x) => 
                x.NavigationStrategy;
        }
    }
}

