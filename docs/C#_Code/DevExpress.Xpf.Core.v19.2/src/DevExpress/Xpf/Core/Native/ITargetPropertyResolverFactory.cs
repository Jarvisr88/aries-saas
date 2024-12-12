namespace DevExpress.Xpf.Core.Native
{
    using System.Windows;

    public interface ITargetPropertyResolverFactory
    {
        TargetPropertyUpdater CreateTargetPropertyResolver(FrameworkElement owner);
    }
}

