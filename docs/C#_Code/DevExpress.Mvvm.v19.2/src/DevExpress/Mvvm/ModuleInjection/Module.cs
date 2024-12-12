namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public class Module : IModule
    {
        public Module(string key, Func<object> viewModelFactory) : this(key, viewModelFactory, null, null, null)
        {
            Verifier.VerifyViewModelFactory(viewModelFactory);
        }

        public Module(string key, Func<object> viewModelFactory, string viewName) : this(key, viewModelFactory, null, viewName, null)
        {
            Verifier.VerifyViewModelFactory(viewModelFactory);
        }

        public Module(string key, Func<object> viewModelFactory, Type viewType) : this(key, viewModelFactory, null, null, viewType)
        {
            Verifier.VerifyViewModelFactory(viewModelFactory);
        }

        public Module(string key, string viewModelName, string viewName) : this(key, null, viewModelName, viewName, null)
        {
            Verifier.VerifyViewModelName(viewModelName);
        }

        private Module(string key, Func<object> viewModelFactory, string viewModelName, string viewName, Type viewType)
        {
            Verifier.VerifyKey(key);
            this.Key = key;
            this.ViewModelFactory = viewModelFactory;
            this.ViewModelName = viewModelName;
            this.ViewName = viewName;
            this.ViewType = viewType;
        }

        public string Key { get; private set; }

        public Func<object> ViewModelFactory { get; private set; }

        public string ViewModelName { get; private set; }

        public string ViewName { get; private set; }

        public Type ViewType { get; private set; }
    }
}

