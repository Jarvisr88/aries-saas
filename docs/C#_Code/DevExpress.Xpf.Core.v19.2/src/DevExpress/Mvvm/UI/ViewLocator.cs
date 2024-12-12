namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;

    public class ViewLocator : LocatorBase, IViewLocator
    {
        private static IViewLocator _default = null;
        internal static readonly IViewLocator Instance = new ViewLocator(Application.Current);
        private readonly IEnumerable<Assembly> assemblies;

        public ViewLocator(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public ViewLocator(Application application) : this(assemblyArray2)
        {
            Assembly[] assemblyArray2;
            if ((LocatorBase.EntryAssembly == null) || LocatorBase.EntryAssembly.IsInDesignMode())
            {
                assemblyArray2 = new Assembly[0];
            }
            else
            {
                assemblyArray2 = new Assembly[] { LocatorBase.EntryAssembly };
            }
        }

        public ViewLocator(params Assembly[] assemblies) : this((IEnumerable<Assembly>) assemblies)
        {
        }

        protected virtual object CreateFallbackView(string documentType) => 
            ViewLocatorExtensions.CreateFallbackView(this.GetErrorMessage(documentType));

        object IViewLocator.ResolveView(string viewName)
        {
            Type type = this.ResolveViewType(viewName);
            return ((type == null) ? this.CreateFallbackView(viewName) : this.CreateInstance(type, viewName));
        }

        protected string GetErrorMessage(string documentType) => 
            ViewLocatorExtensions.GetErrorMessage_CannotResolveViewType(documentType);

        public string GetViewTypeName(Type type) => 
            base.ResolveTypeName(type, null);

        public Type ResolveViewType(string viewName)
        {
            IDictionary<string, string> dictionary;
            return base.ResolveType(viewName, out dictionary);
        }

        public static IViewLocator Default
        {
            get => 
                _default ?? Instance;
            set => 
                _default = value;
        }

        protected override IEnumerable<Assembly> Assemblies =>
            this.assemblies;
    }
}

