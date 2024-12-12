namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows;

    public sealed class ImmediateActionsManager
    {
        private readonly FrameworkElement owner;
        private readonly Queue<IAction> actionsQueue = new Queue<IAction>();
        private readonly Queue<IAction> tempQueue = new Queue<IAction>();
        private readonly Locker executeLocker = new Locker();

        public ImmediateActionsManager(FrameworkElement owner = null)
        {
            this.owner = owner;
        }

        public void EnqueueAction(IAction action)
        {
            if (this.executeLocker.IsLocked)
            {
                this.tempQueue.Enqueue(action);
            }
            else
            {
                this.EnqueueActionInternal(action);
                if (this.owner != null)
                {
                    this.owner.InvalidateMeasure();
                }
            }
        }

        public void EnqueueAction(Action action)
        {
            this.EnqueueAction(new DelegateAction(action));
        }

        private void EnqueueActionInternal(IAction action)
        {
            IAggregateAction action2 = action as IAggregateAction;
            if (action2 != null)
            {
                Queue<IAction> source = new Queue<IAction>();
                while (true)
                {
                    if (this.actionsQueue.Count <= 0)
                    {
                        source.ForEach<IAction>(x => this.actionsQueue.Enqueue(x));
                        break;
                    }
                    IAction action3 = this.actionsQueue.Dequeue();
                    if (!action2.CanAggregate(action3))
                    {
                        source.Enqueue(action3);
                    }
                }
            }
            this.actionsQueue.Enqueue(action);
        }

        public void ExecuteActions()
        {
            if ((this.actionsQueue.Count != 0) && !this.executeLocker.IsLocked)
            {
                this.executeLocker.DoLockedAction(delegate {
                    IAction objB = this.actionsQueue.Last<IAction>();
                    while (true)
                    {
                        IAction objA = this.actionsQueue.Dequeue();
                        objA.Execute();
                        if (ReferenceEquals(objA, objB))
                        {
                            return;
                        }
                    }
                });
                if (this.tempQueue.Count > 0)
                {
                    this.tempQueue.ForEach<IAction>(new Action<IAction>(this.EnqueueActionInternal));
                    this.tempQueue.Clear();
                    if (this.owner != null)
                    {
                        this.owner.InvalidateMeasure();
                    }
                }
            }
        }

        public IAction FindAction(Predicate<IAction> predicate) => 
            this.FindAction(this.actionsQueue, predicate) ?? this.FindAction(this.tempQueue, predicate);

        private IAction FindAction(Queue<IAction> queue, Predicate<IAction> predicate)
        {
            IAction action2;
            using (Queue<IAction>.Enumerator enumerator = queue.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IAction current = enumerator.Current;
                        if (!predicate(current))
                        {
                            continue;
                        }
                        action2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return action2;
        }

        public IAction FindActionOfType(Type actionType) => 
            this.FindAction(new Predicate<IAction>(actionType.IsInstanceOfType));

        public int Count =>
            this.actionsQueue.Count;
    }
}

