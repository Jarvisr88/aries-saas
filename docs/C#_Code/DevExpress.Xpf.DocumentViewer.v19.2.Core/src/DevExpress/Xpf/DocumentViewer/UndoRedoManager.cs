namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    public class UndoRedoManager : BindableBase
    {
        private readonly TimeSpan timeSpan = TimeSpan.FromMilliseconds(300.0);
        private readonly DispatcherTimer timer;
        private readonly Locker undoRedoLocker;

        public UndoRedoManager(Dispatcher dispatcher)
        {
            this.UndoStack = new Stack<UndoState>();
            this.RedoStack = new Stack<UndoState>();
            this.DeferredQueue = new Queue<UndoState>();
            this.undoRedoLocker = new Locker();
            this.timer = new DispatcherTimer(this.timeSpan, DispatcherPriority.Normal, (sender, args) => this.TimerTick(), dispatcher);
        }

        public void Flush()
        {
            this.CurrentState = null;
            this.UndoStack.Clear();
            this.RedoStack.Clear();
            this.DeferredQueue.Clear();
            this.RaiseUndoRedoPropertiesChanged();
        }

        private void FlushDeferredQueue()
        {
            UndoState state = this.DeferredQueue.FirstOrDefault<UndoState>();
            UndoState state2 = this.DeferredQueue.LastOrDefault<UndoState>();
            this.DeferredQueue.Clear();
            if ((state != null) && (state2 != null))
            {
                UndoState state1 = new UndoState();
                state1.ActionType = state.ActionType;
                state1.State = state2.State;
                state1.Perform = state.Perform;
                UndoState action = state1;
                this.RegisterImmediateAction(action);
            }
        }

        private bool IsDeferredAction(UndoState action) => 
            action.ActionType == UndoActionType.DeferredScroll;

        private bool IsSameAsCurrentState(UndoState action) => 
            action.Equals(this.CurrentState);

        private void RaiseUndoRedoPropertiesChanged()
        {
            base.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(UndoRedoManager)), (MethodInfo) methodof(UndoRedoManager.get_CanUndo)), new ParameterExpression[0]));
            base.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(UndoRedoManager)), (MethodInfo) methodof(UndoRedoManager.get_CanRedo)), new ParameterExpression[0]));
        }

        public void Redo()
        {
            this.FlushDeferredQueue();
            if (this.CanRedo)
            {
                UndoState action = this.RedoStack.Pop();
                this.undoRedoLocker.DoLockedAction(() => action.Perform(action.State));
                this.UndoStack.Push(this.CurrentState);
                this.CurrentState = action;
                this.RaiseUndoRedoPropertiesChanged();
            }
        }

        public void RegisterAction(UndoState action)
        {
            if (!this.undoRedoLocker.IsLocked)
            {
                if (this.CurrentState == null)
                {
                    this.CurrentState = action;
                }
                else if (!this.IsSameAsCurrentState(action))
                {
                    if (this.IsDeferredAction(action))
                    {
                        this.RegisterDeferredAction(action);
                    }
                    else
                    {
                        this.RegisterImmediateAction(action);
                    }
                }
            }
        }

        private void RegisterDeferredAction(UndoState action)
        {
            this.DeferredQueue.Enqueue(action);
            this.timer.Stop();
            this.timer.Start();
        }

        private void RegisterImmediateAction(UndoState action)
        {
            this.FlushDeferredQueue();
            this.CurrentState.Do<UndoState>(x => this.UndoStack.Push(x));
            this.CurrentState = action;
            this.RedoStack.Clear();
            this.RaiseUndoRedoPropertiesChanged();
        }

        private void TimerTick()
        {
            this.timer.Stop();
            this.FlushDeferredQueue();
        }

        public void Undo()
        {
            this.FlushDeferredQueue();
            if (this.CanUndo)
            {
                UndoState action = this.UndoStack.Pop();
                this.undoRedoLocker.DoLockedAction(() => action.Perform(action.State));
                this.RedoStack.Push(this.CurrentState);
                this.CurrentState = action;
                this.RaiseUndoRedoPropertiesChanged();
            }
        }

        public bool CanUndo =>
            (this.UndoStack.Count > 0) || (this.DeferredQueue.Count > 0);

        public bool CanRedo =>
            this.RedoStack.Count > 0;

        private Stack<UndoState> UndoStack { get; set; }

        private UndoState CurrentState { get; set; }

        private Stack<UndoState> RedoStack { get; set; }

        private Queue<UndoState> DeferredQueue { get; set; }
    }
}

