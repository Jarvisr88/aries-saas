namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WeakEventHandler<Arg1, Arg2, TBaseHandler> : WeakEventHandlerBase<WeakEventHandler<Arg1, Arg2, TBaseHandler>.TargetEventHandler>
    {
        [ThreadStatic]
        private static Dictionary<MethodInfo, WeakEventHandlerBase<TargetEventHandler<Arg1, Arg2, TBaseHandler>>.CreateData> creators;

        static WeakEventHandler()
        {
            WeakEventHandler<Arg1, Arg2, TBaseHandler>.creators = new Dictionary<MethodInfo, WeakEventHandlerBase<TargetEventHandler<Arg1, Arg2, TBaseHandler>>.CreateData>();
        }

        public WeakEventHandler() : base(typeof(EventHandler))
        {
        }

        protected static TargetEventHandler<Arg1, Arg2, TBaseHandler> CreateDelegate<T>(WeakReference e, Delegate method)
        {
            EventHandler<Arg1, Arg2, TBaseHandler, T> handler = (EventHandler<Arg1, Arg2, TBaseHandler, T>) method;
            return delegate (Arg1 a1, Arg2 a2) {
                object target = e.Target;
                if (target != null)
                {
                    handler((T) target, a1, a2);
                }
            };
        }

        public void Invoke(Arg1 a1, Arg2 a2)
        {
            base.Invoke(target => target(a1, a2));
        }

        protected override Dictionary<MethodInfo, WeakEventHandlerBase<TargetEventHandler<Arg1, Arg2, TBaseHandler>>.CreateData> Creators
        {
            get
            {
                WeakEventHandler<Arg1, Arg2, TBaseHandler>.creators ??= new Dictionary<MethodInfo, WeakEventHandlerBase<TargetEventHandler<Arg1, Arg2, TBaseHandler>>.CreateData>();
                return WeakEventHandler<Arg1, Arg2, TBaseHandler>.creators;
            }
        }

        private delegate void EventHandler<T>(T target, Arg1 a1, Arg2 a2);

        public delegate void TargetEventHandler(Arg1 a1, Arg2 a2);
    }
}

