namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public sealed class ContextDisplayTemplatesCustomizationServiceFactoryForDataClient : IDisplayTemplatesCustomizationServiceFactory
    {
        private readonly Func<IServiceProvider> getContext;

        public ContextDisplayTemplatesCustomizationServiceFactoryForDataClient(Func<IServiceProvider> getContext)
        {
            this.getContext = getContext;
        }

        IDisplayTemplatesCustomizationService IDisplayTemplatesCustomizationServiceFactory.Create(string path) => 
            new ContextDisplayTemplatesCustomizationService(path, this.getContext);

        private sealed class ContextDisplayTemplatesCustomizationService : IDisplayTemplatesCustomizationService
        {
            private readonly string path;
            private readonly Func<IDisplayTemplatesCustomizationServiceFactory> getContextFactory;

            public ContextDisplayTemplatesCustomizationService(string path, Func<IServiceProvider> getContext)
            {
                this.path = path;
                this.getContextFactory = () => getContext().GetService<IDisplayTemplatesCustomizationServiceFactory>();
            }

            void IDisplayTemplatesCustomizationService.OnApplyTemplate(object template)
            {
                this.getContextFactory().Do<IDisplayTemplatesCustomizationServiceFactory>(delegate (IDisplayTemplatesCustomizationServiceFactory ctx) {
                    Action<IDisplayTemplatesCustomizationService> <>9__1;
                    Action<IDisplayTemplatesCustomizationService> @do = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action<IDisplayTemplatesCustomizationService> local1 = <>9__1;
                        @do = <>9__1 = svc => svc.OnApplyTemplate(template);
                    }
                    ctx.Create(this.path).Do<IDisplayTemplatesCustomizationService>(@do);
                });
            }

            object IDisplayTemplatesCustomizationService.PrepareTemplate(object template) => 
                this.getContextFactory().Get<IDisplayTemplatesCustomizationServiceFactory, object>(delegate (IDisplayTemplatesCustomizationServiceFactory ctx) {
                    Func<IDisplayTemplatesCustomizationService, object> <>9__1;
                    Func<IDisplayTemplatesCustomizationService, object> get = <>9__1;
                    if (<>9__1 == null)
                    {
                        Func<IDisplayTemplatesCustomizationService, object> local1 = <>9__1;
                        get = <>9__1 = svc => svc.PrepareTemplate(template);
                    }
                    return ctx.Create(this.path).Get<IDisplayTemplatesCustomizationService, object>(get, template);
                }, null);
        }
    }
}

