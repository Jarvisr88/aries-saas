namespace DevExpress.Utils
{
    using DevExpress.Data.Helpers;
    using DevExpress.Internal;
    using System;

    public class WeakEventHandler<TArgs, TBaseHandler> where TArgs: EventArgs
    {
        private readonly IWeakEventHandlerStrategy<TArgs> strategy;

        public WeakEventHandler() : this(WeakEventHandler<TArgs, TBaseHandler>.CreateStrategy())
        {
        }

        protected internal WeakEventHandler(IWeakEventHandlerStrategy<TArgs> strategy)
        {
            this.strategy = strategy;
        }

        protected internal void Add(Delegate target)
        {
            this.strategy.Add(target);
        }

        protected static IWeakEventHandlerStrategy<TArgs> CreateStrategy() => 
            !SecurityHelper.IsPartialTrust ? ((IWeakEventHandlerStrategy<TArgs>) new WeakEventHandlerStrategy<TArgs, TBaseHandler>()) : ((IWeakEventHandlerStrategy<TArgs>) new NonWeakEventHandlerStrategy<TArgs>());

        public static WeakEventHandler<TArgs, TBaseHandler> operator +(WeakEventHandler<TArgs, TBaseHandler> target, Delegate value)
        {
            if (target == null)
            {
                target = new WeakEventHandler<TArgs, TBaseHandler>();
            }
            else
            {
                target.Purge();
            }
            target.Add(value);
            return target;
        }

        public static WeakEventHandler<TArgs, TBaseHandler> operator -(WeakEventHandler<TArgs, TBaseHandler> target, Delegate value)
        {
            if (target == null)
            {
                return null;
            }
            target.Remove(value);
            return (!target.IsEmpty ? target : null);
        }

        protected internal void Purge()
        {
            this.strategy.Purge();
        }

        public void Raise(object sender, TArgs args)
        {
            this.strategy.Raise(sender, args);
        }

        protected internal void Remove(Delegate target)
        {
            this.strategy.Remove(target);
        }

        protected internal bool IsEmpty =>
            this.strategy.IsEmpty;
    }
}

