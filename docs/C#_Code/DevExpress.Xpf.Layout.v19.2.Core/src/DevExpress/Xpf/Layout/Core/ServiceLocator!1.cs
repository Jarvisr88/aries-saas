namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ServiceLocator<Service> where Service: class
    {
        private static CreateService<Service> createInstance;

        public static void Register<ServiceProvider>() where ServiceProvider: Service, new()
        {
            ServiceLocator<Service>.createInstance ??= (<>c__2<Service, ServiceProvider>.<>9__2_0 ??= () => ((ServiceProvider) Activator.CreateInstance<ServiceProvider>()));
        }

        public static Service Resolve() => 
            ServiceLocator<Service>.createInstance();

        [Serializable, CompilerGenerated]
        private sealed class <>c__2<ServiceProvider> where ServiceProvider: Service, new()
        {
            public static readonly ServiceLocator<Service>.<>c__2<ServiceProvider> <>9;
            public static ServiceLocator<Service>.CreateService <>9__2_0;

            static <>c__2()
            {
                ServiceLocator<Service>.<>c__2<ServiceProvider>.<>9 = new ServiceLocator<Service>.<>c__2<ServiceProvider>();
            }

            internal Service <Register>b__2_0() => 
                Activator.CreateInstance<ServiceProvider>();
        }

        private delegate Service CreateService();
    }
}

