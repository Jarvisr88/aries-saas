namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class WeakEventHandlerBase<THandler>
    {
        private readonly List<WeakEvent<THandler>> events;
        private readonly MethodInfo createMethod;
        private readonly Type eventType;

        protected WeakEventHandlerBase(Type eventType)
        {
            this.events = new List<WeakEvent<THandler>>();
            this.eventType = eventType;
            this.createMethod = base.GetType().GetMethod("CreateDelegate", BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Static);
        }

        protected void AddEvent(Delegate value)
        {
            this.events.Add(this.CreateDynamicEvent(value));
        }

        private WeakEvent<THandler> CreateDynamicEvent(Delegate target)
        {
            CreateData<THandler> data;
            WeakEvent<THandler> event2;
            Delegate del = target;
            MethodInfo methodInfo = del.GetMethodInfo();
            Dictionary<MethodInfo, CreateData<THandler>> creators = this.Creators;
            if (!creators.TryGetValue(methodInfo, out data))
            {
                if (del.Target == null)
                {
                    data.Invoker = methodInfo.CreateDelegate(typeof(THandler));
                }
                else
                {
                    Type declaringType = methodInfo.DeclaringType;
                    Type[] typeArguments = new Type[] { declaringType };
                    data.Creator = (CreateDelegateHandler<THandler>) this.createMethod.MakeGenericMethod(typeArguments).CreateDelegate(typeof(CreateDelegateHandler<THandler>), null);
                    List<Type> list = new List<Type>(typeof(THandler).GetGenericArguments()) {
                        declaringType
                    };
                    data.Invoker = methodInfo.CreateDelegate(this.eventType.MakeGenericType(list.ToArray()), null);
                }
                creators.Add(methodInfo, data);
            }
            if (del.Target == null)
            {
                event2 = new WeakEvent<THandler>(null, methodInfo, (THandler) data.Invoker);
            }
            else
            {
                WeakReference reference = new WeakReference(del.Target);
                event2 = new WeakEvent<THandler>(reference, methodInfo, data.Creator(reference, data.Invoker));
            }
            return event2;
        }

        protected void Invoke(Invoker<THandler> invoker)
        {
            foreach (WeakEvent<THandler> event2 in this.events.ToArray())
            {
                invoker(event2.Handler);
            }
        }

        protected void PurgeEvents()
        {
            for (int i = this.events.Count - 1; i >= 0; i--)
            {
                if (this.events[i].CanPurge)
                {
                    this.events.RemoveAt(i);
                }
            }
        }

        protected void RemoveEvent(Delegate value)
        {
            for (int i = this.events.Count - 1; i >= 0; i--)
            {
                if (this.events[i].Equals(value))
                {
                    this.events.RemoveAt(i);
                    return;
                }
            }
        }

        public bool HasSubscribers =>
            this.events.Count > 0;

        protected abstract Dictionary<MethodInfo, CreateData<THandler>> Creators { get; }

        [StructLayout(LayoutKind.Sequential)]
        protected struct CreateData
        {
            public Delegate Invoker;
            public WeakEventHandlerBase<THandler>.CreateDelegateHandler Creator;
        }

        protected delegate THandler CreateDelegateHandler(WeakReference e, Delegate method);

        protected delegate void Invoker(THandler target);

        private sealed class WeakEvent
        {
            private MethodInfo method;
            private THandler handler;
            private WeakReference reference;

            public WeakEvent(WeakReference target, MethodInfo method, THandler handler)
            {
                this.method = method;
                this.reference = target;
                this.handler = handler;
            }

            public bool Equals(Delegate target)
            {
                Delegate del = target;
                return ((this.method == del.GetMethodInfo()) && (((this.reference != null) || (del.Target != ((Delegate) this.handler).Target)) ? (del.Target == this.reference.Target) : true));
            }

            public THandler Handler =>
                this.handler;

            public bool CanPurge =>
                (this.reference != null) && !this.reference.IsAlive;
        }
    }
}

