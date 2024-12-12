namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class WeakEventHandlerMediumTrustStrategy<TArgs> : IWeakEventHandlerStrategy<TArgs> where TArgs: EventArgs
    {
        private List<HandlerRecord<TArgs>> handlers;

        public void Add(Delegate target)
        {
            this.handlers ??= new List<HandlerRecord<TArgs>>();
            this.handlers.Add(new HandlerRecord<TArgs>(target));
        }

        public void Purge()
        {
            this.handlers.Clear();
        }

        public void Raise(object sender, TArgs args)
        {
            if (this.handlers != null)
            {
                for (int i = 0; i < this.handlers.Count; i++)
                {
                    HandlerRecord<TArgs> record = this.handlers[i];
                    if (record.IsAlive)
                    {
                        record.Invoke(sender, args);
                    }
                    else
                    {
                        this.handlers.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public void Remove(Delegate target)
        {
            if (this.handlers != null)
            {
                for (int i = this.handlers.Count - 1; i >= 0; i--)
                {
                    HandlerRecord<TArgs> record = this.handlers[i];
                    if (!record.IsAlive)
                    {
                        this.handlers.RemoveAt(i);
                    }
                    else if (record.Equals(target))
                    {
                        this.handlers.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public bool IsEmpty =>
            (this.handlers == null) || (this.handlers.Count == 0);

        private class HandlerRecord
        {
            private readonly WeakReference targetRef;
            private readonly MethodInfo methodInfo;

            public HandlerRecord(Delegate handler)
            {
                this.targetRef = new WeakReference(handler.Target);
                this.methodInfo = handler.GetMethodInfo();
                if (!this.methodInfo.IsPublic)
                {
                    throw new ArgumentException("Non-public method can't be used for weak events in the medium trust environment");
                }
            }

            public bool Equals(Delegate target) => 
                this.IsAlive ? ((this.targetRef.Target == target.Target) && (this.methodInfo == target.GetMethodInfo())) : false;

            public void Invoke(object sender, TArgs args)
            {
                if (!this.IsAlive)
                {
                    throw new Exception("Object doesn't exist any more");
                }
                object[] parameters = new object[] { sender, args };
                this.methodInfo.Invoke(this.targetRef.Target, parameters);
            }

            public bool IsAlive =>
                this.targetRef.IsAlive;
        }
    }
}

