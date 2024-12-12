namespace DevExpress.Utils
{
    using DevExpress.Data.Helpers;
    using DevExpress.Internal;
    using System;

    public class PublicWeakEventHandler<TArgs, TBaseHandler> : WeakEventHandler<TArgs, TBaseHandler> where TArgs: EventArgs
    {
        public PublicWeakEventHandler() : base(PublicWeakEventHandler<TArgs, TBaseHandler>.CreateStrategy())
        {
        }

        protected static IWeakEventHandlerStrategy<TArgs> CreateStrategy() => 
            !SecurityHelper.IsPartialTrust ? WeakEventHandler<TArgs, TBaseHandler>.CreateStrategy() : new WeakEventHandlerMediumTrustStrategy<TArgs>();

        public static PublicWeakEventHandler<TArgs, TBaseHandler> operator +(PublicWeakEventHandler<TArgs, TBaseHandler> target, Delegate value)
        {
            if (target == null)
            {
                target = new PublicWeakEventHandler<TArgs, TBaseHandler>();
            }
            else
            {
                target.Purge();
            }
            target.Add(value);
            return target;
        }

        public static PublicWeakEventHandler<TArgs, TBaseHandler> operator -(PublicWeakEventHandler<TArgs, TBaseHandler> target, Delegate value)
        {
            if (target == null)
            {
                return null;
            }
            target.Remove(value);
            return (!target.IsEmpty ? target : null);
        }
    }
}

