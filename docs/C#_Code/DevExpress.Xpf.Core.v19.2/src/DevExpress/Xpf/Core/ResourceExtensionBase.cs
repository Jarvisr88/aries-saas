namespace DevExpress.Xpf.Core
{
    using System;

    public abstract class ResourceExtensionBase : ThemeResourceExtensionBase
    {
        protected ResourceExtensionBase(string resourcePath) : base(resourcePath)
        {
        }

        protected virtual string GetResourcePath() => 
            base.ResourcePath;

        protected sealed override string GetResourcePath(IServiceProvider serviceProvider) => 
            this.GetResourcePath();
    }
}

