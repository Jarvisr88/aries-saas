namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer;
    using DevExpress.Xpf.PdfViewer.Extensions;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PdfKeyboardAndMouseController : KeyboardAndMouseController
    {
        private const double ScrollXOffset = 10.0;
        private const double ScrollYOffset = 10.0;
        private const double LargeModifier = 3.0;
        private const double LargeStep = 100.0;

        public PdfKeyboardAndMouseController(PdfPresenterControl presenter) : base(presenter)
        {
        }

        public void BringCurrentSelectionPointIntoView()
        {
            this.UpdateSelectionRectangle(MouseHelper.GetPosition(base.ItemsPanel));
            if ((this.CursorMode != CursorModeType.HandTool) && (this.CursorMode != CursorModeType.MarqueeZoom))
            {
                this.ProcessSelectionMouseMove();
            }
        }

        private double CalcScrollXOffset(Point position)
        {
            double num = position.X.GreaterThan(base.ItemsPanel.ActualWidth) ? (position.X - base.ItemsPanel.ActualWidth) : 0.0;
            num = position.X.LessThan(0.0) ? position.X : num;
            return ((Math.Min(Math.Abs(num), 10.0) * Math.Sign(num)) * (this.IsLargeStep(num) ? 3.0 : 1.0));
        }

        private double CalcScrollYOffset(Point position)
        {
            double num = position.Y.GreaterThan(base.ItemsPanel.ActualHeight) ? (position.Y - base.ItemsPanel.ActualHeight) : 0.0;
            num = position.Y.LessThan(0.0) ? position.Y : num;
            return ((Math.Min(Math.Abs(num), 10.0) * Math.Sign(num)) * (this.IsLargeStep(num) ? 3.0 : 1.0));
        }

        private PdfMouseButton GetMouseButton() => 
            (Mouse.LeftButton == MouseButtonState.Pressed) ? PdfMouseButton.Left : PdfMouseButton.None;

        private bool IsCaretCreated()
        {
            Func<IPdfDocument, PdfCaret> evaluator = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<IPdfDocument, PdfCaret> local1 = <>c.<>9__41_0;
                evaluator = <>c.<>9__41_0 = x => x.Caret;
            }
            return (this.Presenter.Document.With<IPdfDocument, PdfCaret>(evaluator) != null);
        }

        private bool IsLargeStep(double delta) => 
            Math.Abs(delta) > 100.0;

        private bool IsMarqueeSmallChange() => 
            this.SelectionRectangle.IsEmpty || (this.SelectionRectangle.Width.LessThan(20.0) && this.SelectionRectangle.Height.LessThan(20.0));

        private bool IsMouseOutsidePresenter(Point cursorPosition) => 
            cursorPosition.X.GreaterThan(base.ItemsPanel.ActualWidth) || (cursorPosition.X.LessThan(0.0) || (cursorPosition.Y.GreaterThan(base.ItemsPanel.ActualHeight) || cursorPosition.Y.LessThan(0.0)));

        private void MarqueeScrollSmallChange(Point anchorPoint)
        {
            this.NavigationStrategy.ZoomToAnchorPoint(!KeyboardHelper.IsControlPressed, anchorPoint);
        }

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            if (this.Presenter.IsInEditing)
            {
                this.Presenter.ActiveEditorOwner.ProcessKeyDown(e);
            }
            else
            {
                this.ProcessKeyDownInternal(e);
            }
        }

        protected virtual void ProcessKeyDownInternal(KeyEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }
            Key key = e.Key;
            if (key > Key.A)
            {
                if (key > Key.Add)
                {
                    if (key != Key.Subtract)
                    {
                        if (key == Key.OemPlus)
                        {
                            goto TR_0098;
                        }
                        else if (key != Key.OemMinus)
                        {
                            return;
                        }
                    }
                    if (KeyboardHelper.IsShiftPressed && KeyboardHelper.IsControlPressed)
                    {
                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_10;
                        if (<>c.<>9__26_10 == null)
                        {
                            Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local11 = <>c.<>9__26_10;
                            evaluator = <>c.<>9__26_10 = x => x.CounterClockwiseRotateCommand;
                        }
                        Action<ICommand> action = <>c.<>9__26_11;
                        if (<>c.<>9__26_11 == null)
                        {
                            Action<ICommand> local12 = <>c.<>9__26_11;
                            action = <>c.<>9__26_11 = x => x.Execute(null);
                        }
                        this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                        e.Handled = true;
                        return;
                    }
                    if (KeyboardHelper.IsControlPressed)
                    {
                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_12;
                        if (<>c.<>9__26_12 == null)
                        {
                            Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local13 = <>c.<>9__26_12;
                            evaluator = <>c.<>9__26_12 = x => x.ZoomOutCommand;
                        }
                        Action<ICommand> action = <>c.<>9__26_13;
                        if (<>c.<>9__26_13 == null)
                        {
                            Action<ICommand> local14 = <>c.<>9__26_13;
                            action = <>c.<>9__26_13 = x => x.Execute(null);
                        }
                        this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                        e.Handled = true;
                        return;
                    }
                    return;
                }
                else if (key == Key.C)
                {
                    if (this.Presenter.ActualPdfViewer.HasSelection && KeyboardHelper.IsControlPressed)
                    {
                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_4;
                        if (<>c.<>9__26_4 == null)
                        {
                            Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local5 = <>c.<>9__26_4;
                            evaluator = <>c.<>9__26_4 = x => x.CopyCommand;
                        }
                        Action<ICommand> action = <>c.<>9__26_5;
                        if (<>c.<>9__26_5 == null)
                        {
                            Action<ICommand> local6 = <>c.<>9__26_5;
                            action = <>c.<>9__26_5 = x => x.Execute(null);
                        }
                        this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                        e.Handled = true;
                        return;
                    }
                    return;
                }
                else if (key != Key.Add)
                {
                    return;
                }
            }
            else
            {
                if (key > Key.Return)
                {
                    switch (key)
                    {
                        case Key.Space:
                            break;

                        case Key.Prior:
                        {
                            Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_30;
                            if (<>c.<>9__26_30 == null)
                            {
                                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local29 = <>c.<>9__26_30;
                                evaluator = <>c.<>9__26_30 = x => x.ScrollCommand;
                            }
                            Action<ICommand> action = <>c.<>9__26_31;
                            if (<>c.<>9__26_31 == null)
                            {
                                Action<ICommand> local30 = <>c.<>9__26_31;
                                action = <>c.<>9__26_31 = x => x.Execute(ScrollCommand.PageUp);
                            }
                            this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                            e.Handled = true;
                            return;
                        }
                        case Key.Next:
                        {
                            Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_32;
                            if (<>c.<>9__26_32 == null)
                            {
                                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local31 = <>c.<>9__26_32;
                                evaluator = <>c.<>9__26_32 = x => x.ScrollCommand;
                            }
                            Action<ICommand> action = <>c.<>9__26_33;
                            if (<>c.<>9__26_33 == null)
                            {
                                Action<ICommand> local32 = <>c.<>9__26_33;
                                action = <>c.<>9__26_33 = x => x.Execute(ScrollCommand.PageDown);
                            }
                            this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                            e.Handled = true;
                            return;
                        }
                        case Key.End:
                            if (!this.Presenter.ActualPdfViewer.IsSearchControlVisible)
                            {
                                if (this.IsCaretCreated())
                                {
                                    PdfSelectionCommand selectionCommand = (!KeyboardHelper.IsShiftPressed || !KeyboardHelper.IsControlPressed) ? (!KeyboardHelper.IsControlPressed ? (!KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.MoveLineEnd : PdfSelectionCommand.SelectLineEnd) : PdfSelectionCommand.MoveDocumentEnd) : PdfSelectionCommand.SelectDocumentEnd;
                                    DevExpress.Xpf.PdfViewer.PdfCommandProvider pdfCommandProvider = this.PdfCommandProvider;
                                    if (<>c.<>9__26_38 == null)
                                    {
                                        DevExpress.Xpf.PdfViewer.PdfCommandProvider local36 = this.PdfCommandProvider;
                                        pdfCommandProvider = (DevExpress.Xpf.PdfViewer.PdfCommandProvider) (<>c.<>9__26_38 = x => x.SelectionCommand);
                                    }
                                    ((DevExpress.Xpf.PdfViewer.PdfCommandProvider) <>c.<>9__26_38).With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(((Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>) pdfCommandProvider)).Do<ICommand>(x => x.Execute(selectionCommand));
                                }
                                else
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_40;
                                    if (<>c.<>9__26_40 == null)
                                    {
                                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local37 = <>c.<>9__26_40;
                                        evaluator = <>c.<>9__26_40 = x => x.ScrollCommand;
                                    }
                                    Action<ICommand> action = <>c.<>9__26_41;
                                    if (<>c.<>9__26_41 == null)
                                    {
                                        Action<ICommand> local38 = <>c.<>9__26_41;
                                        action = <>c.<>9__26_41 = x => x.Execute(ScrollCommand.End);
                                    }
                                    this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                                }
                                e.Handled = true;
                                return;
                            }
                            return;

                        case Key.Home:
                            if (!this.Presenter.ActualPdfViewer.IsSearchControlVisible)
                            {
                                if (this.IsCaretCreated())
                                {
                                    PdfSelectionCommand command1 = (!KeyboardHelper.IsShiftPressed || !KeyboardHelper.IsControlPressed) ? (!KeyboardHelper.IsControlPressed ? (!KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.MoveLineStart : PdfSelectionCommand.SelectLineStart) : PdfSelectionCommand.MoveDocumentStart) : PdfSelectionCommand.SelectDocumentStart;
                                    DevExpress.Xpf.PdfViewer.PdfCommandProvider pdfCommandProvider = this.PdfCommandProvider;
                                    if (<>c.<>9__26_34 == null)
                                    {
                                        DevExpress.Xpf.PdfViewer.PdfCommandProvider local33 = this.PdfCommandProvider;
                                        pdfCommandProvider = (DevExpress.Xpf.PdfViewer.PdfCommandProvider) (<>c.<>9__26_34 = x => x.SelectionCommand);
                                    }
                                    ((DevExpress.Xpf.PdfViewer.PdfCommandProvider) <>c.<>9__26_34).With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(((Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>) pdfCommandProvider)).Do<ICommand>(x => x.Execute(command1));
                                }
                                else
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_36;
                                    if (<>c.<>9__26_36 == null)
                                    {
                                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local34 = <>c.<>9__26_36;
                                        evaluator = <>c.<>9__26_36 = x => x.ScrollCommand;
                                    }
                                    Action<ICommand> action = <>c.<>9__26_37;
                                    if (<>c.<>9__26_37 == null)
                                    {
                                        Action<ICommand> local35 = <>c.<>9__26_37;
                                        action = <>c.<>9__26_37 = x => x.Execute(ScrollCommand.Home);
                                    }
                                    this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                                }
                                e.Handled = true;
                                return;
                            }
                            return;

                        case Key.Left:
                            if (!this.Presenter.ActualPdfViewer.IsSearchControlVisible)
                            {
                                if (this.IsCaretCreated())
                                {
                                    PdfSelectionCommand command2 = (!KeyboardHelper.IsShiftPressed || !KeyboardHelper.IsControlPressed) ? (!KeyboardHelper.IsControlPressed ? (!KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.MoveLeft : PdfSelectionCommand.SelectLeft) : PdfSelectionCommand.MovePreviousWord) : PdfSelectionCommand.SelectPreviousWord;
                                    DevExpress.Xpf.PdfViewer.PdfCommandProvider pdfCommandProvider = this.PdfCommandProvider;
                                    if (<>c.<>9__26_26 == null)
                                    {
                                        DevExpress.Xpf.PdfViewer.PdfCommandProvider local26 = this.PdfCommandProvider;
                                        pdfCommandProvider = (DevExpress.Xpf.PdfViewer.PdfCommandProvider) (<>c.<>9__26_26 = x => x.SelectionCommand);
                                    }
                                    ((DevExpress.Xpf.PdfViewer.PdfCommandProvider) <>c.<>9__26_26).With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(((Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>) pdfCommandProvider)).Do<ICommand>(x => x.Execute(command2));
                                }
                                else
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_28;
                                    if (<>c.<>9__26_28 == null)
                                    {
                                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local27 = <>c.<>9__26_28;
                                        evaluator = <>c.<>9__26_28 = x => x.ScrollCommand;
                                    }
                                    Action<ICommand> action = <>c.<>9__26_29;
                                    if (<>c.<>9__26_29 == null)
                                    {
                                        Action<ICommand> local28 = <>c.<>9__26_29;
                                        action = <>c.<>9__26_29 = x => x.Execute(ScrollCommand.LineLeft);
                                    }
                                    this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                                }
                                e.Handled = true;
                                return;
                            }
                            return;

                        case Key.Up:
                            if (this.IsCaretCreated())
                            {
                                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_14;
                                if (<>c.<>9__26_14 == null)
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local15 = <>c.<>9__26_14;
                                    evaluator = <>c.<>9__26_14 = x => x.SelectionCommand;
                                }
                                Action<ICommand> action = <>c.<>9__26_15;
                                if (<>c.<>9__26_15 == null)
                                {
                                    Action<ICommand> local16 = <>c.<>9__26_15;
                                    action = <>c.<>9__26_15 = x => x.Execute(KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.SelectUp : PdfSelectionCommand.MoveUp);
                                }
                                this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                            }
                            else
                            {
                                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_16;
                                if (<>c.<>9__26_16 == null)
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local17 = <>c.<>9__26_16;
                                    evaluator = <>c.<>9__26_16 = x => x.ScrollCommand;
                                }
                                Action<ICommand> action = <>c.<>9__26_17;
                                if (<>c.<>9__26_17 == null)
                                {
                                    Action<ICommand> local18 = <>c.<>9__26_17;
                                    action = <>c.<>9__26_17 = x => x.Execute(ScrollCommand.LineUp);
                                }
                                this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                            }
                            e.Handled = true;
                            return;

                        case Key.Right:
                            if (!this.Presenter.ActualPdfViewer.IsSearchControlVisible)
                            {
                                if (this.IsCaretCreated())
                                {
                                    PdfSelectionCommand command3 = (!KeyboardHelper.IsShiftPressed || !KeyboardHelper.IsControlPressed) ? (!KeyboardHelper.IsControlPressed ? (!KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.MoveRight : PdfSelectionCommand.SelectRight) : PdfSelectionCommand.MoveNextWord) : PdfSelectionCommand.SelectNextWord;
                                    DevExpress.Xpf.PdfViewer.PdfCommandProvider pdfCommandProvider = this.PdfCommandProvider;
                                    if (<>c.<>9__26_22 == null)
                                    {
                                        DevExpress.Xpf.PdfViewer.PdfCommandProvider local23 = this.PdfCommandProvider;
                                        pdfCommandProvider = (DevExpress.Xpf.PdfViewer.PdfCommandProvider) (<>c.<>9__26_22 = x => x.SelectionCommand);
                                    }
                                    ((DevExpress.Xpf.PdfViewer.PdfCommandProvider) <>c.<>9__26_22).With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(((Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>) pdfCommandProvider)).Do<ICommand>(x => x.Execute(command3));
                                }
                                else
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_24;
                                    if (<>c.<>9__26_24 == null)
                                    {
                                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local24 = <>c.<>9__26_24;
                                        evaluator = <>c.<>9__26_24 = x => x.ScrollCommand;
                                    }
                                    Action<ICommand> action = <>c.<>9__26_25;
                                    if (<>c.<>9__26_25 == null)
                                    {
                                        Action<ICommand> local25 = <>c.<>9__26_25;
                                        action = <>c.<>9__26_25 = x => x.Execute(ScrollCommand.LineRight);
                                    }
                                    this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                                }
                                e.Handled = true;
                                return;
                            }
                            return;

                        case Key.Down:
                            if (this.IsCaretCreated())
                            {
                                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_18;
                                if (<>c.<>9__26_18 == null)
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local19 = <>c.<>9__26_18;
                                    evaluator = <>c.<>9__26_18 = x => x.SelectionCommand;
                                }
                                Action<ICommand> action = <>c.<>9__26_19;
                                if (<>c.<>9__26_19 == null)
                                {
                                    Action<ICommand> local20 = <>c.<>9__26_19;
                                    action = <>c.<>9__26_19 = x => x.Execute(KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.SelectDown : PdfSelectionCommand.MoveDown);
                                }
                                this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                            }
                            else
                            {
                                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_20;
                                if (<>c.<>9__26_20 == null)
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local21 = <>c.<>9__26_20;
                                    evaluator = <>c.<>9__26_20 = x => x.ScrollCommand;
                                }
                                Action<ICommand> action = <>c.<>9__26_21;
                                if (<>c.<>9__26_21 == null)
                                {
                                    Action<ICommand> local22 = <>c.<>9__26_21;
                                    action = <>c.<>9__26_21 = x => x.Execute(ScrollCommand.LineDown);
                                }
                                this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                            }
                            e.Handled = true;
                            return;

                        case Key.Select:
                        case Key.Print:
                        case Key.Execute:
                        case Key.Snapshot:
                        case Key.Insert:
                            return;

                        case Key.Delete:
                            if (!this.Presenter.ActualPdfViewer.IsSearchControlVisible)
                            {
                                this.Document.DocumentStateController.RemoveSelectedAnnotation();
                            }
                            return;

                        default:
                            if (key == Key.A)
                            {
                                if (this.IsCaretCreated() && (KeyboardHelper.IsShiftPressed && KeyboardHelper.IsControlPressed))
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_0;
                                    if (<>c.<>9__26_0 == null)
                                    {
                                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local1 = <>c.<>9__26_0;
                                        evaluator = <>c.<>9__26_0 = x => x.UnselectAllCommand;
                                    }
                                    Action<ICommand> action = <>c.<>9__26_1;
                                    if (<>c.<>9__26_1 == null)
                                    {
                                        Action<ICommand> local2 = <>c.<>9__26_1;
                                        action = <>c.<>9__26_1 = x => x.Execute(null);
                                    }
                                    this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                                    e.Handled = true;
                                    return;
                                }
                                if (KeyboardHelper.IsControlPressed)
                                {
                                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_2;
                                    if (<>c.<>9__26_2 == null)
                                    {
                                        Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local3 = <>c.<>9__26_2;
                                        evaluator = <>c.<>9__26_2 = x => x.SelectAllCommand;
                                    }
                                    Action<ICommand> action = <>c.<>9__26_3;
                                    if (<>c.<>9__26_3 == null)
                                    {
                                        Action<ICommand> local4 = <>c.<>9__26_3;
                                        action = <>c.<>9__26_3 = x => x.Execute(null);
                                    }
                                    this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                                    e.Handled = true;
                                    return;
                                }
                            }
                            return;
                    }
                }
                else if (key == Key.Tab)
                {
                    if (this.Presenter.ActualPdfViewer.AcceptsTab && (!this.Presenter.ActualPdfViewer.IsSearchControlVisible && !ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e))))
                    {
                        if (ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
                        {
                            this.Document.DocumentStateController.TabBackward();
                        }
                        else
                        {
                            this.Document.DocumentStateController.TabForward();
                        }
                        e.Handled = true;
                        return;
                    }
                    return;
                }
                else if (key != Key.Return)
                {
                    return;
                }
                if (!this.Presenter.ActualPdfViewer.IsSearchControlVisible)
                {
                    this.Document.DocumentStateController.SubmitFocus();
                    return;
                }
                return;
            }
        TR_0098:
            if (KeyboardHelper.IsShiftPressed && KeyboardHelper.IsControlPressed)
            {
                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_6;
                if (<>c.<>9__26_6 == null)
                {
                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local7 = <>c.<>9__26_6;
                    evaluator = <>c.<>9__26_6 = x => x.ClockwiseRotateCommand;
                }
                Action<ICommand> action = <>c.<>9__26_7;
                if (<>c.<>9__26_7 == null)
                {
                    Action<ICommand> local8 = <>c.<>9__26_7;
                    action = <>c.<>9__26_7 = x => x.Execute(null);
                }
                this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                e.Handled = true;
            }
            else if (KeyboardHelper.IsControlPressed)
            {
                Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> evaluator = <>c.<>9__26_8;
                if (<>c.<>9__26_8 == null)
                {
                    Func<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand> local9 = <>c.<>9__26_8;
                    evaluator = <>c.<>9__26_8 = x => x.ZoomInCommand;
                }
                Action<ICommand> action = <>c.<>9__26_9;
                if (<>c.<>9__26_9 == null)
                {
                    Action<ICommand> local10 = <>c.<>9__26_9;
                    action = <>c.<>9__26_9 = x => x.Execute(null);
                }
                this.PdfCommandProvider.With<DevExpress.Xpf.PdfViewer.PdfCommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                e.Handled = true;
            }
            else
            {
                return;
            }
        }

        internal void ProcessMarqueeZoom()
        {
            if (this.IsMarqueeSmallChange())
            {
                this.MarqueeScrollSmallChange(this.SelectionRectangle.StartPoint);
            }
            else
            {
                this.NavigationStrategy.ProcessMarqueeZoom(this.SelectionRectangle.Rectangle, this.SelectionRectangle.X, this.SelectionRectangle.Y);
            }
        }

        public override void ProcessMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.ProcessMouseLeftButtonDown(e);
            if (!this.Presenter.IsInEditing)
            {
                Action<PdfPresenterControl> action = <>c.<>9__27_0;
                if (<>c.<>9__27_0 == null)
                {
                    Action<PdfPresenterControl> local1 = <>c.<>9__27_0;
                    action = <>c.<>9__27_0 = x => x.Focus();
                }
                this.Presenter.Do<PdfPresenterControl>(action);
            }
            if ((this.Presenter.Document != null) && ((this.CursorMode != CursorModeType.MarqueeZoom) || ((!KeyboardHelper.IsControlPressed || this.BehaviorProvider.CanZoomOut()) && (KeyboardHelper.IsControlPressed || this.BehaviorProvider.CanZoomIn()))))
            {
                Point position = e.GetPosition(base.ItemsPanel);
                if (this.CursorMode != CursorModeType.MarqueeZoom)
                {
                    this.Document.DocumentStateController.MouseDown(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), e.ClickCount));
                }
                if (this.CursorMode != CursorModeType.HandTool)
                {
                    this.SetupSelectionRectangle(position);
                }
            }
        }

        public override void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.ProcessMouseLeftButtonUp(e);
            Point position = e.GetPosition(base.ItemsPanel);
            if (this.CursorMode == CursorModeType.MarqueeZoom)
            {
                this.ProcessMarqueeZoom();
            }
            else
            {
                this.Document.DocumentStateController.MouseUp(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), e.ClickCount));
            }
            this.ReleaseSelectionRectangle();
        }

        public override void ProcessMouseMove(MouseEventArgs e)
        {
            base.ProcessMouseMove(e);
            Point position = e.GetPosition(base.ItemsPanel);
            this.UpdateSelectionRectangle(position);
            if (!this.IsSelecting)
            {
                this.ProcessMouseMoveInternal();
            }
            else
            {
                this.ProcessSelectionMouseMove();
            }
        }

        private void ProcessMouseMoveInternal()
        {
            Point position = MouseHelper.GetPosition(base.presenter);
            bool isOutsideOfView = this.IsMouseOutsidePresenter(position);
            this.Document.DocumentStateController.MouseMove(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), this.GetMouseButton(), ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 0, isOutsideOfView));
        }

        public override void ProcessMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.ProcessMouseRightButtonDown(e);
            Point position = e.GetPosition(base.ItemsPanel);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Document.DocumentStateController.MouseUp(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), e.ClickCount));
                this.ReleaseSelectionRectangle();
            }
            this.Document.DocumentStateController.MouseDown(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Right, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), e.ClickCount));
        }

        public override void ProcessMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.ProcessMouseRightButtonUp(e);
            Point position = e.GetPosition(base.ItemsPanel);
            this.Document.DocumentStateController.MouseUp(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(position), PdfMouseButton.Right, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), e.ClickCount));
        }

        private void ProcessSelectionMouseMove()
        {
            bool isOutsideOfView = this.IsMouseOutsidePresenter(MouseHelper.GetPosition(base.presenter));
            this.Document.DocumentStateController.MouseMove(new PdfMouseAction(this.NavigationStrategy.CalcDocumentPosition(this.SelectionRectangle.AnchorPoint), PdfMouseButton.Left, ModifierKeysHelper.GetKeyboardModifiers().ToPdfModifierKeys(), 0, isOutsideOfView));
        }

        internal void ReleaseSelectionRectangle()
        {
            this.IsSelecting = false;
            this.SelectionRectangle.Reset();
            this.Presenter.ItemsControl.PresenterDecorator.ReleaseMouseCapture();
        }

        internal void SetupSelectionRectangle(Point cursorPosition)
        {
            PdfHitTestResult result = this.Presenter.HitTest(cursorPosition);
            if ((this.CursorMode == CursorModeType.MarqueeZoom) || ((result.ContentType == PdfDocumentContentType.Text) || (result.ContentType == PdfDocumentContentType.Image)))
            {
                this.IsSelecting = true;
                this.SelectionRectangle.SetVerticalOffset(this.ScrollViewer.VerticalOffset, false);
                this.SelectionRectangle.SetHorizontalOffset(this.ScrollViewer.HorizontalOffset, false);
                this.SelectionRectangle.SetViewport(new Size(Math.Max(this.ScrollViewer.ViewportWidth, this.ScrollViewer.ExtentWidth), Math.Max(this.ScrollViewer.ViewportHeight, this.ScrollViewer.ExtentHeight)));
                this.SelectionRectangle.SetStartPoint(cursorPosition);
                Mouse.Capture(this.Presenter.ItemsControl.PresenterDecorator, CaptureMode.SubTree);
            }
        }

        internal void UpdateSelectionRectangle(Point cursorPosition)
        {
            if (this.IsSelecting)
            {
                this.SelectionRectangle.SetPointPosition(cursorPosition);
                if (this.IsMouseOutsidePresenter(cursorPosition))
                {
                    this.ScrollViewer.ScrollToHorizontalOffset(this.ScrollViewer.HorizontalOffset + this.CalcScrollXOffset(cursorPosition));
                    this.SelectionRectangle.SetHorizontalOffset(this.ScrollViewer.HorizontalOffset, true);
                    this.ScrollViewer.ScrollToVerticalOffset(this.ScrollViewer.VerticalOffset + this.CalcScrollYOffset(cursorPosition));
                    this.SelectionRectangle.SetVerticalOffset(this.ScrollViewer.VerticalOffset, true);
                }
            }
        }

        private DevExpress.Xpf.PdfViewer.PdfCommandProvider PdfCommandProvider =>
            base.CommandProvider as DevExpress.Xpf.PdfViewer.PdfCommandProvider;

        private PdfPresenterControl Presenter =>
            base.presenter as PdfPresenterControl;

        private PdfNavigationStrategy NavigationStrategy =>
            base.NavigationStrategy as PdfNavigationStrategy;

        private PdfBehaviorProvider BehaviorProvider =>
            this.Presenter.PdfBehaviorProvider;

        private PdfDocumentViewModel Document =>
            base.presenter.Document as PdfDocumentViewModel;

        private DevExpress.Xpf.PdfViewer.SelectionRectangle SelectionRectangle =>
            this.Presenter.SelectionRectangle;

        private CursorModeType CursorMode =>
            this.Presenter.CursorMode;

        private System.Windows.Controls.ScrollViewer ScrollViewer =>
            this.Presenter.ScrollViewer;

        internal bool IsSelecting { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfKeyboardAndMouseController.<>c <>9 = new PdfKeyboardAndMouseController.<>c();
            public static Func<PdfCommandProvider, ICommand> <>9__26_0;
            public static Action<ICommand> <>9__26_1;
            public static Func<PdfCommandProvider, ICommand> <>9__26_2;
            public static Action<ICommand> <>9__26_3;
            public static Func<PdfCommandProvider, ICommand> <>9__26_4;
            public static Action<ICommand> <>9__26_5;
            public static Func<PdfCommandProvider, ICommand> <>9__26_6;
            public static Action<ICommand> <>9__26_7;
            public static Func<PdfCommandProvider, ICommand> <>9__26_8;
            public static Action<ICommand> <>9__26_9;
            public static Func<PdfCommandProvider, ICommand> <>9__26_10;
            public static Action<ICommand> <>9__26_11;
            public static Func<PdfCommandProvider, ICommand> <>9__26_12;
            public static Action<ICommand> <>9__26_13;
            public static Func<PdfCommandProvider, ICommand> <>9__26_14;
            public static Action<ICommand> <>9__26_15;
            public static Func<PdfCommandProvider, ICommand> <>9__26_16;
            public static Action<ICommand> <>9__26_17;
            public static Func<PdfCommandProvider, ICommand> <>9__26_18;
            public static Action<ICommand> <>9__26_19;
            public static Func<PdfCommandProvider, ICommand> <>9__26_20;
            public static Action<ICommand> <>9__26_21;
            public static Func<PdfCommandProvider, ICommand> <>9__26_22;
            public static Func<PdfCommandProvider, ICommand> <>9__26_24;
            public static Action<ICommand> <>9__26_25;
            public static Func<PdfCommandProvider, ICommand> <>9__26_26;
            public static Func<PdfCommandProvider, ICommand> <>9__26_28;
            public static Action<ICommand> <>9__26_29;
            public static Func<PdfCommandProvider, ICommand> <>9__26_30;
            public static Action<ICommand> <>9__26_31;
            public static Func<PdfCommandProvider, ICommand> <>9__26_32;
            public static Action<ICommand> <>9__26_33;
            public static Func<PdfCommandProvider, ICommand> <>9__26_34;
            public static Func<PdfCommandProvider, ICommand> <>9__26_36;
            public static Action<ICommand> <>9__26_37;
            public static Func<PdfCommandProvider, ICommand> <>9__26_38;
            public static Func<PdfCommandProvider, ICommand> <>9__26_40;
            public static Action<ICommand> <>9__26_41;
            public static Action<PdfPresenterControl> <>9__27_0;
            public static Func<IPdfDocument, PdfCaret> <>9__41_0;

            internal PdfCaret <IsCaretCreated>b__41_0(IPdfDocument x) => 
                x.Caret;

            internal ICommand <ProcessKeyDownInternal>b__26_0(PdfCommandProvider x) => 
                x.UnselectAllCommand;

            internal void <ProcessKeyDownInternal>b__26_1(ICommand x)
            {
                x.Execute(null);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_10(PdfCommandProvider x) => 
                x.CounterClockwiseRotateCommand;

            internal void <ProcessKeyDownInternal>b__26_11(ICommand x)
            {
                x.Execute(null);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_12(PdfCommandProvider x) => 
                x.ZoomOutCommand;

            internal void <ProcessKeyDownInternal>b__26_13(ICommand x)
            {
                x.Execute(null);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_14(PdfCommandProvider x) => 
                x.SelectionCommand;

            internal void <ProcessKeyDownInternal>b__26_15(ICommand x)
            {
                x.Execute(KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.SelectUp : PdfSelectionCommand.MoveUp);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_16(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_17(ICommand x)
            {
                x.Execute(ScrollCommand.LineUp);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_18(PdfCommandProvider x) => 
                x.SelectionCommand;

            internal void <ProcessKeyDownInternal>b__26_19(ICommand x)
            {
                x.Execute(KeyboardHelper.IsShiftPressed ? PdfSelectionCommand.SelectDown : PdfSelectionCommand.MoveDown);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_2(PdfCommandProvider x) => 
                x.SelectAllCommand;

            internal ICommand <ProcessKeyDownInternal>b__26_20(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_21(ICommand x)
            {
                x.Execute(ScrollCommand.LineDown);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_22(PdfCommandProvider x) => 
                x.SelectionCommand;

            internal ICommand <ProcessKeyDownInternal>b__26_24(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_25(ICommand x)
            {
                x.Execute(ScrollCommand.LineRight);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_26(PdfCommandProvider x) => 
                x.SelectionCommand;

            internal ICommand <ProcessKeyDownInternal>b__26_28(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_29(ICommand x)
            {
                x.Execute(ScrollCommand.LineLeft);
            }

            internal void <ProcessKeyDownInternal>b__26_3(ICommand x)
            {
                x.Execute(null);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_30(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_31(ICommand x)
            {
                x.Execute(ScrollCommand.PageUp);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_32(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_33(ICommand x)
            {
                x.Execute(ScrollCommand.PageDown);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_34(PdfCommandProvider x) => 
                x.SelectionCommand;

            internal ICommand <ProcessKeyDownInternal>b__26_36(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_37(ICommand x)
            {
                x.Execute(ScrollCommand.Home);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_38(PdfCommandProvider x) => 
                x.SelectionCommand;

            internal ICommand <ProcessKeyDownInternal>b__26_4(PdfCommandProvider x) => 
                x.CopyCommand;

            internal ICommand <ProcessKeyDownInternal>b__26_40(PdfCommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__26_41(ICommand x)
            {
                x.Execute(ScrollCommand.End);
            }

            internal void <ProcessKeyDownInternal>b__26_5(ICommand x)
            {
                x.Execute(null);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_6(PdfCommandProvider x) => 
                x.ClockwiseRotateCommand;

            internal void <ProcessKeyDownInternal>b__26_7(ICommand x)
            {
                x.Execute(null);
            }

            internal ICommand <ProcessKeyDownInternal>b__26_8(PdfCommandProvider x) => 
                x.ZoomInCommand;

            internal void <ProcessKeyDownInternal>b__26_9(ICommand x)
            {
                x.Execute(null);
            }

            internal void <ProcessMouseLeftButtonDown>b__27_0(PdfPresenterControl x)
            {
                x.Focus();
            }
        }
    }
}

