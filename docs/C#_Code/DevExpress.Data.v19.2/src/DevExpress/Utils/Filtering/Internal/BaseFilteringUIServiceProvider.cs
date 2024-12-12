namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.IoC;
    using System;

    public class BaseFilteringUIServiceProvider : IServiceProvider
    {
        protected readonly IntegrityContainer serviceContainer = new IntegrityContainer();

        protected BaseFilteringUIServiceProvider()
        {
            this.RegisterServices();
        }

        protected virtual IDisplayTemplatesCustomizationServiceFactory GetDisplayTemplatesCustomizationServiceFactory() => 
            DefaultDisplayTemplatesCustomizationServiceFactory.Instance;

        protected virtual IDisplayTemplatesServiceFactory GetDisplayTemplatesServiceFactory() => 
            DefaultDisplayTemplatesServiceFactory.Instance;

        protected virtual IDisplayTextServiceFactory GetDisplayTextServiceFactory() => 
            DefaultDisplayTextServiceFactory.Instance;

        protected virtual IEndUserFilteringMetricAttributesFactory GetEndUserFilteringMetricAttributesFactory() => 
            DefaultEndUserFilteringMetricAttributesFactory.Instance;

        protected virtual IEndUserFilteringMetricViewModelFactory GetEndUserFilteringMetricViewModelFactory() => 
            DefaultEndUserFilteringMetricViewModelFactory.Instance;

        protected virtual IEndUserFilteringSettingsFactory GetEndUserFilteringSettingsFactory() => 
            DefaultEndUserFilteringSettingsFactory.Instance;

        protected virtual IEndUserFilteringViewModelDataContext GetEndUserFilteringViewModelDataContext() => 
            DefaultEndUserFilteringViewModelDataContext.Instance;

        protected virtual IFilterCriteriaParseFactory GetFilterCriteriaParseFactory() => 
            DefaultFilterCriteriaParseFactory.Instance;

        protected virtual IFilterCriteriaQueryFactory GetFilterCriteriaQueryFactory() => 
            DefaultFilterCriteriaQueryFactory.Instance;

        protected virtual IFilterUIEditorPropertiesFactory GetFilterUIEditorPropertiesFactory() => 
            DefaultFilterUIEditorPropertiesFactory.Instance;

        protected virtual IMetricAttributesQueryFactory GetMetricAttributesQueryFactory() => 
            DefaultMetricAttributesQueryFactory.Instance;

        public TService GetService<TService>() where TService: class => 
            this.serviceContainer.Resolve<TService>();

        protected virtual IValueTypeResolver GetValueBoxTypeResolver() => 
            DefaultValueTypeResolver.Instance;

        protected virtual IViewModelBuilderResolver GetViewModelBuilderResolver() => 
            DefaultViewModelBuilderResolver.Instance;

        protected virtual IViewModelFactory GetViewModelFactory() => 
            DefaultViewModelFactory.Instance;

        public void RegisterService<TService>(TService service) where TService: class
        {
            this.serviceContainer.RegisterInstance<TService>(service);
        }

        protected virtual void RegisterServices()
        {
            this.RegisterService<IViewModelFactory>(this.GetViewModelFactory());
            this.RegisterService<IValueTypeResolver>(this.GetValueBoxTypeResolver());
            this.RegisterService<IEndUserFilteringSettingsFactory>(this.GetEndUserFilteringSettingsFactory());
            this.RegisterService<IEndUserFilteringMetricAttributesFactory>(this.GetEndUserFilteringMetricAttributesFactory());
            this.RegisterService<IMetricAttributesQueryFactory>(this.GetMetricAttributesQueryFactory());
            this.RegisterService<IEndUserFilteringMetricViewModelFactory>(this.GetEndUserFilteringMetricViewModelFactory());
            this.RegisterService<IViewModelBuilderResolver>(this.GetViewModelBuilderResolver());
            this.RegisterService<IFilterCriteriaQueryFactory>(this.GetFilterCriteriaQueryFactory());
            this.RegisterService<IFilterCriteriaParseFactory>(this.GetFilterCriteriaParseFactory());
            this.RegisterService<IFilterUIEditorPropertiesFactory>(this.GetFilterUIEditorPropertiesFactory());
            this.RegisterService<IDisplayTextServiceFactory>(this.GetDisplayTextServiceFactory());
            this.RegisterService<IDisplayTemplatesServiceFactory>(this.GetDisplayTemplatesServiceFactory());
            this.RegisterService<IDisplayTemplatesCustomizationServiceFactory>(this.GetDisplayTemplatesCustomizationServiceFactory());
            this.RegisterService<IEndUserFilteringViewModelDataContext>(this.GetEndUserFilteringViewModelDataContext());
        }

        object IServiceProvider.GetService(Type serviceType) => 
            ((IServiceProvider) this.serviceContainer).GetService(serviceType);
    }
}

