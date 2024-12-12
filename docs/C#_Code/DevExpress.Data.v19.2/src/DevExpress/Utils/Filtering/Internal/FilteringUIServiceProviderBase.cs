namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public class FilteringUIServiceProviderBase : BaseFilteringUIServiceProvider
    {
        protected virtual IEndUserFilteringViewModelTypeBuilder GetEndUserFilteringViewModelTypeBuilder() => 
            DefaultEndUserFilteringViewModelTypeBuilder.Instance;

        protected override void RegisterServices()
        {
            base.RegisterServices();
            base.RegisterService<IEndUserFilteringViewModelTypeBuilder>(this.GetEndUserFilteringViewModelTypeBuilder());
        }
    }
}

