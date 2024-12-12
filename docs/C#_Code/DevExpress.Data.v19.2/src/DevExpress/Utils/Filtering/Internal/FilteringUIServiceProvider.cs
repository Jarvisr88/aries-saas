namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public class FilteringUIServiceProvider : FilteringUIServiceProviderBase
    {
        public IEndUserFilteringViewModelProvider CreateViewModelProvider() => 
            this.CreateViewModelProvider(base.serviceContainer);

        protected virtual IEndUserFilteringViewModelProvider CreateViewModelProvider(IServiceProvider serviceProvider) => 
            new FilteringUIViewModelProvider(serviceProvider);
    }
}

