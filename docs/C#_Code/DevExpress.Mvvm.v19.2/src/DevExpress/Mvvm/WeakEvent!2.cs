namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class WeakEvent<TEventHandler, TEventArgs>
    {
        private readonly List<WeakHandler<TEventHandler, TEventArgs>> weakHandlers;
        [ThreadStatic]
        private static Dictionary<MethodInfo, WeakEventHandler<TEventHandler, TEventArgs>> weakEventHandlerCache;

        public WeakEvent()
        {
            this.weakHandlers = new List<WeakHandler<TEventHandler, TEventArgs>>();
        }

        [DebuggerStepThrough]
        public void Add(TEventHandler handler)
        {
            List<WeakHandler<TEventHandler, TEventArgs>> collection = new List<WeakHandler<TEventHandler, TEventArgs>>();
            foreach (Delegate delegate2 in ((Delegate) handler).GetInvocationList())
            {
                collection.Add(new WeakHandler<TEventHandler, TEventArgs>(delegate2));
            }
            List<WeakHandler<TEventHandler, TEventArgs>> weakHandlers = this.weakHandlers;
            lock (weakHandlers)
            {
                this.weakHandlers.AddRange(collection);
            }
        }

        private static WeakEventHandler<TEventHandler, TEventArgs> CreateWeakEventHandler(MethodInfo method)
        {
            ParameterExpression expression = Expression.Parameter(typeof(object), "target");
            ParameterExpression expression2 = Expression.Parameter(typeof(object), "sender");
            ParameterExpression expression3 = Expression.Parameter(typeof(TEventArgs), "e");
            ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2, expression3 };
            return Expression.Lambda<WeakEventHandler<TEventHandler, TEventArgs>>(method.IsStatic ? Expression.Call(method, expression2, expression3) : Expression.Call(Expression.Convert(expression, method.DeclaringType), method, expression2, expression3), parameters).Compile();
        }

        private static bool Invoke(WeakHandler<TEventHandler, TEventArgs> wh, object sender, TEventArgs e)
        {
            if (wh.IsStatic)
            {
                wh.Handler(null, sender, e);
                return true;
            }
            object target = wh.Target;
            if (target == null)
            {
                return false;
            }
            wh.Handler(target, sender, e);
            return true;
        }

        private static bool Match(WeakHandler<TEventHandler, TEventArgs> wh, Delegate d) => 
            (wh.Target == d.Target) && wh.Method.Equals(d.Method);

        public void Raise(object sender, TEventArgs e)
        {
            List<WeakHandler<TEventHandler, TEventArgs>> list2 = (from x in this.weakHandlers.ToList<WeakHandler<TEventHandler, TEventArgs>>()
                where !WeakEvent<TEventHandler, TEventArgs>.Invoke(x, sender, e)
                select x).ToList<WeakHandler<TEventHandler, TEventArgs>>();
            List<WeakHandler<TEventHandler, TEventArgs>> weakHandlers = this.weakHandlers;
            lock (weakHandlers)
            {
                list2.ForEach(x => base.weakHandlers.Remove(x));
            }
        }

        public void Remove(TEventHandler handler)
        {
            Delegate d = (Delegate) handler;
            List<WeakHandler<TEventHandler, TEventArgs>> weakHandlers = this.weakHandlers;
            lock (weakHandlers)
            {
                WeakHandler<TEventHandler, TEventArgs> item = this.weakHandlers.LastOrDefault<WeakHandler<TEventHandler, TEventArgs>>(x => WeakEvent<TEventHandler, TEventArgs>.Match(x, d));
                if (item != null)
                {
                    this.weakHandlers.Remove(item);
                }
            }
        }

        private static Dictionary<MethodInfo, WeakEventHandler<TEventHandler, TEventArgs>> WeakEventHandlerCache =>
            WeakEvent<TEventHandler, TEventArgs>.weakEventHandlerCache ??= new Dictionary<MethodInfo, WeakEventHandler<TEventHandler, TEventArgs>>();

        private delegate void WeakEventHandler(object target, object sender, TEventArgs e);

        private class WeakHandler
        {
            private readonly WeakReference target;

            public WeakHandler(Delegate handler)
            {
                WeakEvent<TEventHandler, TEventArgs>.WeakEventHandler handler2;
                this.target = (handler.Target != null) ? new WeakReference(handler.Target) : null;
                this.Method = handler.Method;
                if (!WeakEvent<TEventHandler, TEventArgs>.WeakEventHandlerCache.TryGetValue(this.Method, out handler2))
                {
                    WeakEvent<TEventHandler, TEventArgs>.WeakEventHandlerCache.Add(this.Method, handler2 = WeakEvent<TEventHandler, TEventArgs>.CreateWeakEventHandler(this.Method));
                }
                this.Handler = handler2;
            }

            public bool IsStatic =>
                ReferenceEquals(this.target, null);

            public object Target =>
                this.target?.Target;

            public MethodInfo Method { get; private set; }

            public WeakEvent<TEventHandler, TEventArgs>.WeakEventHandler Handler { get; private set; }
        }
    }
}

