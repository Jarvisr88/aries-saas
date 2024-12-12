namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public sealed class ContextDisplayTemplatesCustomizationServiceFactory : IDisplayTemplatesCustomizationServiceFactory
    {
        private readonly IDisplayTemplatesCustomizationServiceFactory factory;
        private readonly Func<IServiceProvider> getContext;

        public ContextDisplayTemplatesCustomizationServiceFactory(IDisplayTemplatesCustomizationServiceFactory factory, Func<IServiceProvider> getContext)
        {
            this.factory = factory;
            this.getContext = getContext;
        }

        IDisplayTemplatesCustomizationService IDisplayTemplatesCustomizationServiceFactory.Create(string path) => 
            new ContextDisplayTemplatesCustomizationService(path, this.factory, this.getContext);

        private sealed class ContextDisplayTemplatesCustomizationService : IDisplayTemplatesCustomizationService
        {
            private readonly string path;
            private readonly IDisplayTemplatesCustomizationServiceFactory factory;
            private readonly Func<IDisplayTemplatesCustomizationServiceFactory> getContextFactory;

            public ContextDisplayTemplatesCustomizationService(string path, IDisplayTemplatesCustomizationServiceFactory factory, Func<IServiceProvider> getContext)
            {
                this.path = path;
                this.factory = factory;
                this.getContextFactory = () => getContext().GetService<IDisplayTemplatesCustomizationServiceFactory>();
            }

            void IDisplayTemplatesCustomizationService.OnApplyTemplate(object template)
            {
                this.factory.Create(this.path).Do<IDisplayTemplatesCustomizationService>(svc => svc.OnApplyTemplate(template));
                this.getContextFactory().Do<IDisplayTemplatesCustomizationServiceFactory>(delegate (IDisplayTemplatesCustomizationServiceFactory ctx) {
                    Action<IDisplayTemplatesCustomizationService> <>9__2;
                    Action<IDisplayTemplatesCustomizationService> @do = <>9__2;
                    if (<>9__2 == null)
                    {
                        Action<IDisplayTemplatesCustomizationService> local1 = <>9__2;
                        @do = <>9__2 = svc => svc.OnApplyTemplate(template);
                    }
                    ctx.Create(this.path).Do<IDisplayTemplatesCustomizationService>(@do);
                });
            }

            object IDisplayTemplatesCustomizationService.PrepareTemplate(object template)
            {
                template = this.factory.Create(this.path).Get<IDisplayTemplatesCustomizationService, object>(svc => svc.PrepareTemplate(template), template);
                return this.getContextFactory().Get<IDisplayTemplatesCustomizationServiceFactory, object>(delegate (IDisplayTemplatesCustomizationServiceFactory ctx) {
                    Func<IDisplayTemplatesCustomizationService, object> <>9__2;
                    Func<IDisplayTemplatesCustomizationService, object> get = <>9__2;
                    if (<>9__2 == null)
                    {
                        Func<IDisplayTemplatesCustomizationService, object> local1 = <>9__2;
                        get = <>9__2 = svc => svc.PrepareTemplate(template);
                    }
                    return ctx.Create(this.path).Get<IDisplayTemplatesCustomizationService, object>(get, template);
                }, null);
            }
        }
    }
}

