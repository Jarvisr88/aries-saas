namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class DelayedActionsHelper : DependencyObject, IDisposable
    {
        private List<DelayedAction> delayedActions = new List<DelayedAction>();

        public void AddDelayedAction(Action action)
        {
            this.AddDelayedAction(action, DelayedActionPriority.Default);
        }

        public void AddDelayedAction(Action action, DelayedActionPriority priority)
        {
            DelayedAction item = new DelayedAction(action, priority);
            if (this.delayedActions.Contains(item))
            {
                this.delayedActions.Remove(item);
            }
            this.delayedActions.Add(item);
        }

        public void Dispose()
        {
            this.delayedActions.Clear();
            GC.SuppressFinalize(this);
        }

        public void DoDelayedActions()
        {
            DelayedAction[] actionArray = this.delayedActions.ToArray();
            this.delayedActions.Clear();
            for (int i = 0; i < actionArray.Length; i++)
            {
                if (actionArray[i] != null)
                {
                    if (actionArray[i].Priority == DelayedActionPriority.Delayed)
                    {
                        base.Dispatcher.BeginInvoke(actionArray[i].Action, new object[0]);
                    }
                    else
                    {
                        actionArray[i].Action();
                    }
                }
            }
        }

        private class DelayedAction
        {
            private readonly int _hash;

            public DelayedAction(System.Action action, DelayedActionPriority priority)
            {
                this.Action = action;
                this.Priority = priority;
                this._hash = action.Method.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                DelayedActionsHelper.DelayedAction action = obj as DelayedActionsHelper.DelayedAction;
                return ((action != null) ? (action.Action == this.Action) : false);
            }

            public override int GetHashCode() => 
                this._hash;

            public static bool operator ==(DelayedActionsHelper.DelayedAction left, DelayedActionsHelper.DelayedAction right) => 
                Equals(left, right);

            public static bool operator !=(DelayedActionsHelper.DelayedAction left, DelayedActionsHelper.DelayedAction right) => 
                !Equals(left, right);

            public System.Action Action { get; set; }

            public DelayedActionPriority Priority { get; set; }
        }
    }
}

