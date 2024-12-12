namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal abstract class StatelessObject : IServiceProvider
    {
        private Func<IServiceProvider> getServiceProvider;

        protected StatelessObject(Func<IServiceProvider> getServiceProvider);
        public sealed override bool Equals(object obj);
        protected TValue GetBehavior<TValue>(Func<IBehaviorProvider, Func<string, TValue>> accessor);
        public sealed override int GetHashCode();
        protected abstract string GetId();
        protected TValue GetMetadata<TValue>(Func<IMetadataProvider, Func<string, TValue>> accessor);
        protected TService GetService<TService>() where TService: class;
        protected TValue GetValue<TService, TValue>(Func<TService, Func<string, TValue>> accessor) where TService: class;
        object IServiceProvider.GetService(Type serviceType);
        public override string ToString();

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<TService> where TService: class
        {
            public static readonly StatelessObject.<>c__5<TService> <>9;
            public static Func<IServiceProvider, TService> <>9__5_0;

            static <>c__5();
            internal TService <GetService>b__5_0(IServiceProvider x);
        }
    }
}

