namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.HitTest;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class UserInputController : KeyboardAndMouseController
    {
        private readonly Point emptyPoint;
        private Point leftButtonDown;

        public UserInputController(DevExpress.Xpf.Printing.DocumentPresenterControl presenter) : base(presenter)
        {
        }

        private bool CheckMarginHitTest(Point point)
        {
            bool marginHitTested = false;
            IDocumentViewModel document = base.presenter.Document as IDocumentViewModel;
            if ((document != null) && !document.CanChangePageSettings)
            {
                return false;
            }
            VisualTreeHelper.HitTest(this.DocumentPresenter, null, delegate (System.Windows.Media.HitTestResult result) {
                if (LayoutHelper.FindLayoutOrVisualParentObject<MarginThumb>(result.VisualHit, false, this.DocumentPresenter.ScrollViewer) == null)
                {
                    return System.Windows.Media.HitTestResultBehavior.Continue;
                }
                marginHitTested = true;
                return System.Windows.Media.HitTestResultBehavior.Stop;
            }, new System.Windows.Media.PointHitTestParameters(point));
            return marginHitTested;
        }

        internal void ForceHideEditor()
        {
            if (this.InteractionProvider.EndEdit())
            {
                this.DocumentPresenter.MouseLeftButtonDown -= new MouseButtonEventHandler(this.OnPresenterMouseDown);
            }
        }

        private bool IsHitTestEditor(Point point) => 
            this.InteractionProvider.IsInEditing && (this.DocumentPresenter.ActiveEditorOwner.CurrentCellEditor.IsVisible && (HitTestHelper.HitTest(this.DocumentPresenter.ActiveEditorOwner.CurrentCellEditor, this.DocumentPresenter.TranslatePoint(point, this.DocumentPresenter.ActiveEditorOwner.CurrentCellEditor)) != null));

        private void OnPresenterMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsHitTestEditor(e.GetPosition(this.DocumentPresenter)))
            {
                this.ForceHideEditor();
                this.ProcessMouseLeftButtonDown(e);
            }
        }

        public override void ProcessKeyDown(KeyEventArgs e)
        {
            if (this.DocumentPresenter.IsInEditing)
            {
                this.DocumentPresenter.ActiveEditorOwner.ProcessKeyDown(e);
            }
            else if (!e.Handled)
            {
                this.ProcessKeyDownInternal(e);
            }
        }

        private void ProcessKeyDownInternal(KeyEventArgs e)
        {
            Key key = e.Key;
            switch (key)
            {
                case Key.Escape:
                    this.DocumentPresenter.Document.ResetMarkedBricks();
                    this.DocumentPresenter.InteractionProvider.ResetSelection(true);
                    e.Handled = true;
                    return;

                case Key.ImeConvert:
                case Key.ImeNonConvert:
                case Key.ImeAccept:
                case Key.ImeModeChange:
                case Key.Space:
                    break;

                case Key.Prior:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_8;
                    if (<>c.<>9__15_8 == null)
                    {
                        Func<CommandProvider, ICommand> local9 = <>c.<>9__15_8;
                        evaluator = <>c.<>9__15_8 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_9;
                    if (<>c.<>9__15_9 == null)
                    {
                        Action<ICommand> local10 = <>c.<>9__15_9;
                        action = <>c.<>9__15_9 = x => x.Execute(ScrollCommand.PageUp);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                case Key.Next:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_10;
                    if (<>c.<>9__15_10 == null)
                    {
                        Func<CommandProvider, ICommand> local11 = <>c.<>9__15_10;
                        evaluator = <>c.<>9__15_10 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_11;
                    if (<>c.<>9__15_11 == null)
                    {
                        Action<ICommand> local12 = <>c.<>9__15_11;
                        action = <>c.<>9__15_11 = x => x.Execute(ScrollCommand.PageDown);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                case Key.End:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_14;
                    if (<>c.<>9__15_14 == null)
                    {
                        Func<CommandProvider, ICommand> local15 = <>c.<>9__15_14;
                        evaluator = <>c.<>9__15_14 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_15;
                    if (<>c.<>9__15_15 == null)
                    {
                        Action<ICommand> local16 = <>c.<>9__15_15;
                        action = <>c.<>9__15_15 = x => x.Execute(ScrollCommand.End);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                case Key.Home:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_12;
                    if (<>c.<>9__15_12 == null)
                    {
                        Func<CommandProvider, ICommand> local13 = <>c.<>9__15_12;
                        evaluator = <>c.<>9__15_12 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_13;
                    if (<>c.<>9__15_13 == null)
                    {
                        Action<ICommand> local14 = <>c.<>9__15_13;
                        action = <>c.<>9__15_13 = x => x.Execute(ScrollCommand.Home);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                case Key.Left:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_4;
                    if (<>c.<>9__15_4 == null)
                    {
                        Func<CommandProvider, ICommand> local5 = <>c.<>9__15_4;
                        evaluator = <>c.<>9__15_4 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_5;
                    if (<>c.<>9__15_5 == null)
                    {
                        Action<ICommand> local6 = <>c.<>9__15_5;
                        action = <>c.<>9__15_5 = x => x.Execute(ScrollCommand.LineLeft);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                case Key.Up:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_0;
                    if (<>c.<>9__15_0 == null)
                    {
                        Func<CommandProvider, ICommand> local1 = <>c.<>9__15_0;
                        evaluator = <>c.<>9__15_0 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_1;
                    if (<>c.<>9__15_1 == null)
                    {
                        Action<ICommand> local2 = <>c.<>9__15_1;
                        action = <>c.<>9__15_1 = x => x.Execute(ScrollCommand.LineUp);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                case Key.Right:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_6;
                    if (<>c.<>9__15_6 == null)
                    {
                        Func<CommandProvider, ICommand> local7 = <>c.<>9__15_6;
                        evaluator = <>c.<>9__15_6 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_7;
                    if (<>c.<>9__15_7 == null)
                    {
                        Action<ICommand> local8 = <>c.<>9__15_7;
                        action = <>c.<>9__15_7 = x => x.Execute(ScrollCommand.LineRight);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                case Key.Down:
                {
                    Func<CommandProvider, ICommand> evaluator = <>c.<>9__15_2;
                    if (<>c.<>9__15_2 == null)
                    {
                        Func<CommandProvider, ICommand> local3 = <>c.<>9__15_2;
                        evaluator = <>c.<>9__15_2 = x => x.ScrollCommand;
                    }
                    Action<ICommand> action = <>c.<>9__15_3;
                    if (<>c.<>9__15_3 == null)
                    {
                        Action<ICommand> local4 = <>c.<>9__15_3;
                        action = <>c.<>9__15_3 = x => x.Execute(ScrollCommand.LineDown);
                    }
                    base.CommandProvider.With<CommandProvider, ICommand>(evaluator).Do<ICommand>(action);
                    e.Handled = true;
                    return;
                }
                default:
                    if (key != Key.F)
                    {
                        return;
                    }
                    if (ModifierKeysHelper.GetKeyboardModifiers() == ModifierKeys.Control)
                    {
                        base.CommandProvider.ShowFindTextCommand.TryExecute(true);
                    }
                    break;
            }
        }

        protected virtual void ProcessMouseClick(MouseButtonEventArgs e)
        {
            this.leftButtonDown = this.emptyPoint;
            Point position = e.GetPosition(this.DocumentPresenter);
            if ((this.CursorMode == CursorModeType.SelectTool) && this.InteractionProvider.CanSelect)
            {
                this.DocumentPresenter.InteractionProvider.EndSelection(position);
                this.DocumentPresenter.ActualDocumentViewer.UpdateSelectionState();
            }
            Pair<PageViewModel, Brick> pair = this.NavigationStrategy.GetPageModelBrickPair(position);
            if (pair != null)
            {
                Brick brick = pair.Second;
                if ((brick == null) || string.IsNullOrEmpty(brick.Url))
                {
                    this.DocumentPresenter.ActualDocumentViewer.CurrentPageNumber = pair.First.PageIndex + 1;
                }
                EditingField editingField = brick.With<Brick, EditingField>(x => this.DocumentPresenter.Document.EditingFields.FirstOrDefault<EditingField>(ef => ReferenceEquals(ef.Brick, x)));
                if (this.DocumentPresenter.ActualDocumentViewer.AllowDocumentEditing && ((editingField != null) && !editingField.ReadOnly))
                {
                    this.InteractionProvider.StartEdit(pair.First, editingField);
                    this.DocumentPresenter.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnPresenterMouseDown);
                }
                else
                {
                    this.InteractionProvider.EndEdit();
                    DocumentPreviewMouseEventArgs args = new DocumentPreviewMouseEventArgs(pair.First.PageIndex, pair.Second);
                    if (this.IsDoubleClicked)
                    {
                        this.DocumentPresenter.ActualDocumentViewer.RaiseDocumentPreiewMouseDoubleClick(args);
                    }
                    else
                    {
                        this.DocumentPresenter.ActualDocumentViewer.RaiseDocumentPreiewMouseClick(args);
                    }
                    (this.DocumentPresenter.Document as ReportDocumentViewModel).Do<ReportDocumentViewModel>(x => x.HandleBrickEvent(this.IsDoubleClicked ? "BrickDoubleClick" : "BrickClick", pair.First.Page, brick));
                    VisualBrick brick = brick as VisualBrick;
                    if ((brick != null) && ((brick.NavigationPair != null) && !ReferenceEquals(brick.NavigationPair, BrickPagePair.Empty)))
                    {
                        ScrollIntoViewMode? scrollIntoView = null;
                        this.NavigationStrategy.ShowBrick(brick.NavigationPair, scrollIntoView);
                    }
                    else if ((brick != null) && (!string.IsNullOrEmpty(brick.Url) && (string.Compare(brick.Url, "empty", true) != 0)))
                    {
                        ProcessLaunchHelper.StartProcess(brick.Url);
                    }
                }
            }
        }

        protected virtual void ProcessMouseDoubleClick(MouseButtonEventArgs e)
        {
            this.IsDoubleClicked = true;
            this.ProcessMouseClick(e);
        }

        public override void ProcessMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.ProcessMouseLeftButtonDown(e);
            Point position = e.GetPosition(this.DocumentPresenter);
            if (((!HitTestHelper.IsHitTest(this.DocumentPresenter, this.DocumentPresenter, e.GetPosition(this.DocumentPresenter)) && !this.InteractionProvider.IsInEditing) && ((this.DocumentPresenter.Document != null) && (this.CursorMode != CursorModeType.HandTool))) && !this.CheckMarginHitTest(position))
            {
                this.DocumentPresenter.Document.ResetMarkedBricks();
                if (e.ClickCount > 1)
                {
                    this.ProcessMouseDoubleClick(e);
                }
                else
                {
                    this.leftButtonDown = position;
                    if ((this.CursorMode == CursorModeType.SelectTool) && !this.InteractionProvider.IsInEditing)
                    {
                        this.DocumentPresenter.InteractionProvider.InitializeSelection(position);
                        this.DocumentPresenter.ActualDocumentViewer.UpdateSelectionState();
                    }
                }
            }
        }

        public override void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.ProcessMouseLeftButtonUp(e);
            if (this.IsDoubleClicked)
            {
                this.IsDoubleClicked = false;
            }
            else
            {
                Point position = e.GetPosition(this.DocumentPresenter);
                if (!this.InteractionProvider.IsInSelecting && (position == this.leftButtonDown))
                {
                    this.ProcessMouseClick(e);
                }
                else if ((this.CursorMode == CursorModeType.SelectTool) && !this.InteractionProvider.IsInEditing)
                {
                    this.DocumentPresenter.InteractionProvider.EndSelection(position);
                    this.DocumentPresenter.ActualDocumentViewer.UpdateSelectionState();
                }
            }
        }

        public override void ProcessMouseMove(MouseEventArgs e)
        {
            base.ProcessMouseMove(e);
            Point position = e.GetPosition(this.DocumentPresenter);
            if (!this.InteractionProvider.IsInSelecting)
            {
                Pair<PageViewModel, Brick> pageModelBrickPair = this.NavigationStrategy.GetPageModelBrickPair(position);
                if (pageModelBrickPair != null)
                {
                    this.DocumentPresenter.ActualDocumentViewer.RaiseDocumentPreiewMouseMove(new DocumentPreviewMouseEventArgs(pageModelBrickPair.First.PageIndex, pageModelBrickPair.Second));
                }
            }
            if ((this.CursorMode == CursorModeType.SelectTool) && this.InteractionProvider.CanSelect)
            {
                if (!this.InteractionProvider.IsInSelecting)
                {
                    this.DocumentPresenter.InteractionProvider.StartSelection(position);
                }
                if (position != this.leftButtonDown)
                {
                    this.DocumentPresenter.InteractionProvider.ContinueSelection(position);
                }
                this.DocumentPresenter.ActualDocumentViewer.UpdateSelectionState();
            }
        }

        public override void ProcessMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.ProcessMouseRightButtonDown(e);
            Point position = e.GetPosition(this.DocumentPresenter);
            if (!this.CheckMarginHitTest(position) && this.InteractionProvider.IsInSelecting)
            {
                this.InteractionProvider.EndSelection(e.GetPosition(base.presenter));
                this.InteractionProvider.ResetSelection(false);
                this.DocumentPresenter.ActualDocumentViewer.UpdateSelectionState();
            }
        }

        public override void ProcessMouseWheel(MouseWheelEventArgs e)
        {
            if (!this.InteractionProvider.IsInSelecting)
            {
                base.ProcessMouseWheel(e);
            }
        }

        private DevExpress.Xpf.Printing.DocumentPresenterControl DocumentPresenter =>
            base.presenter as DevExpress.Xpf.Printing.DocumentPresenterControl;

        private DevExpress.Xpf.Printing.PreviewControl.Native.InteractionProvider InteractionProvider =>
            this.DocumentPresenter.InteractionProvider;

        protected DocumentNavigationStrategy NavigationStrategy =>
            (DocumentNavigationStrategy) base.NavigationStrategy;

        private CursorModeType CursorMode =>
            this.DocumentPresenter.CursorMode;

        private bool IsDoubleClicked { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UserInputController.<>c <>9 = new UserInputController.<>c();
            public static Func<CommandProvider, ICommand> <>9__15_0;
            public static Action<ICommand> <>9__15_1;
            public static Func<CommandProvider, ICommand> <>9__15_2;
            public static Action<ICommand> <>9__15_3;
            public static Func<CommandProvider, ICommand> <>9__15_4;
            public static Action<ICommand> <>9__15_5;
            public static Func<CommandProvider, ICommand> <>9__15_6;
            public static Action<ICommand> <>9__15_7;
            public static Func<CommandProvider, ICommand> <>9__15_8;
            public static Action<ICommand> <>9__15_9;
            public static Func<CommandProvider, ICommand> <>9__15_10;
            public static Action<ICommand> <>9__15_11;
            public static Func<CommandProvider, ICommand> <>9__15_12;
            public static Action<ICommand> <>9__15_13;
            public static Func<CommandProvider, ICommand> <>9__15_14;
            public static Action<ICommand> <>9__15_15;

            internal ICommand <ProcessKeyDownInternal>b__15_0(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_1(ICommand x)
            {
                x.Execute(ScrollCommand.LineUp);
            }

            internal ICommand <ProcessKeyDownInternal>b__15_10(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_11(ICommand x)
            {
                x.Execute(ScrollCommand.PageDown);
            }

            internal ICommand <ProcessKeyDownInternal>b__15_12(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_13(ICommand x)
            {
                x.Execute(ScrollCommand.Home);
            }

            internal ICommand <ProcessKeyDownInternal>b__15_14(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_15(ICommand x)
            {
                x.Execute(ScrollCommand.End);
            }

            internal ICommand <ProcessKeyDownInternal>b__15_2(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_3(ICommand x)
            {
                x.Execute(ScrollCommand.LineDown);
            }

            internal ICommand <ProcessKeyDownInternal>b__15_4(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_5(ICommand x)
            {
                x.Execute(ScrollCommand.LineLeft);
            }

            internal ICommand <ProcessKeyDownInternal>b__15_6(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_7(ICommand x)
            {
                x.Execute(ScrollCommand.LineRight);
            }

            internal ICommand <ProcessKeyDownInternal>b__15_8(CommandProvider x) => 
                x.ScrollCommand;

            internal void <ProcessKeyDownInternal>b__15_9(ICommand x)
            {
                x.Execute(ScrollCommand.PageUp);
            }
        }
    }
}

