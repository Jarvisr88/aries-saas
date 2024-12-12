namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public abstract class TargetPropertyUpdaterFactory : ITargetPropertyResolverFactory
    {
        protected TargetPropertyUpdaterFactory();
        public abstract TargetPropertyUpdater CreateTargetPropertyResolver(FrameworkElement owner);
    }
}

