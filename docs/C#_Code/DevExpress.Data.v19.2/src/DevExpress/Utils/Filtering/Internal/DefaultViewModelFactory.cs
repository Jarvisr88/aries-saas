namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultViewModelFactory : IViewModelFactory
    {
        internal static readonly IViewModelFactory Instance;

        static DefaultViewModelFactory();
        private DefaultViewModelFactory();
        public object Create(Type viewModelType, IViewModelBuilder builder);
    }
}

