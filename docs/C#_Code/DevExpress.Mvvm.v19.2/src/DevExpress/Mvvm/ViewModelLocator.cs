namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ViewModelLocator : LocatorBase, IViewModelLocator
    {
        private static IViewModelLocator _defaultInstance = new ViewModelLocator(Application.Current);
        private static IViewModelLocator _default;
        private readonly IEnumerable<Assembly> assemblies;

        public ViewModelLocator(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public ViewModelLocator(Application application) : this(assemblyArray2)
        {
            Assembly[] assemblyArray2;
            if ((LocatorBase.EntryAssembly == null) || ViewModelBase.IsInDesignMode)
            {
                assemblyArray2 = new Assembly[0];
            }
            else
            {
                assemblyArray2 = new Assembly[] { LocatorBase.EntryAssembly };
            }
        }

        public ViewModelLocator(params Assembly[] assemblies) : this((IEnumerable<Assembly>) assemblies)
        {
        }

        object IViewModelLocator.ResolveViewModel(string name)
        {
            Type type = this.ResolveViewModelType(name);
            return ((type != null) ? this.CreateInstance(type, name) : null);
        }

        protected bool GetIsPOCOViewModelType(Type type, IDictionary<string, string> properties)
        {
            string str;
            return (!type.GetCustomAttributes(typeof(POCOViewModelAttribute), true).Any<object>() ? (properties.TryGetValue("IsPOCOViewModel", out str) && bool.Parse(str)) : true);
        }

        public virtual string GetViewModelTypeName(Type type)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            Func<Type, bool> predicate = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__12_0;
                predicate = <>c.<>9__12_0 = x => x == typeof(IPOCOViewModel);
            }
            if (type.GetInterfaces().Any<Type>(predicate))
            {
                this.SetIsPOCOViewModelType(properties, true);
                type = type.BaseType;
            }
            return base.ResolveTypeName(type, properties);
        }

        public virtual Type ResolveViewModelType(string name)
        {
            IDictionary<string, string> dictionary;
            Type type = base.ResolveType(name, out dictionary);
            return ((type != null) ? (this.GetIsPOCOViewModelType(type, dictionary) ? ViewModelSource.GetPOCOType(type, null) : type) : null);
        }

        protected void SetIsPOCOViewModelType(IDictionary<string, string> properties, bool value)
        {
            properties.Add("IsPOCOViewModel", value.ToString());
        }

        public static IViewModelLocator Default
        {
            get => 
                _default ?? _defaultInstance;
            set => 
                _default = value;
        }

        protected override IEnumerable<Assembly> Assemblies =>
            this.assemblies;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelLocator.<>c <>9 = new ViewModelLocator.<>c();
            public static Func<Type, bool> <>9__12_0;

            internal bool <GetViewModelTypeName>b__12_0(Type x) => 
                x == typeof(IPOCOViewModel);
        }
    }
}

