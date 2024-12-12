namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterDialogViewModel : FilterUIElement<CustomUIFilterDialogType>, ICustomUIFilterDialogViewModel, IServiceProvider, ILocalizableUIElement<CustomUIFilterDialogType>
    {
        private readonly Lazy<IDisplayTextService> displayTextServiceCore;
        private ICustomUIFilterValue resultCore;

        public CustomUIFilterDialogViewModel(string path, CustomUIFilterDialogType dialogType, Func<IServiceProvider> getServiceProvider, CustomUIFiltersType filtersType, CustomUIFilterType filterType, ICustomUIFilterValue parameter) : base(dialogType, getServiceProvider)
        {
            this.Path = path;
            this.FiltersType = filtersType;
            this.FilterType = filterType;
            ICustomUIFilterValue value1 = parameter;
            if (parameter == null)
            {
                ICustomUIFilterValue local1 = parameter;
                value1 = this.CreateValue(this.FilterType, new object[0]);
            }
            this.Parameter = value1;
            this.displayTextServiceCore = new Lazy<IDisplayTextService>(new Func<IDisplayTextService>(this.CreateDisplayTextService));
        }

        internal static bool AreEqualOrDefault(ICustomUIFilterValue x, ICustomUIFilterValue y) => 
            (x == null) ? ((y == null) || (y.Equals(x) || (y.IsDefault && ReferenceEquals(x, null)))) : (x.Equals(y) || (x.IsDefault && ReferenceEquals(y, null)));

        private IDisplayTextService CreateDisplayTextService() => 
            base.GetService<IDisplayTextServiceFactory>().Get<IDisplayTextServiceFactory, IDisplayTextService>(factory => factory.Create(this.Path), null);

        protected sealed override int GetHash(CustomUIFilterDialogType id) => 
            (int) id;

        protected override object GetServiceCore(Type serviceType) => 
            !(serviceType == typeof(IDisplayTextService)) ? base.GetServiceCore(serviceType) : this.displayTextServiceCore.Value;

        public string Path { get; private set; }

        public CustomUIFiltersType FiltersType { get; private set; }

        public CustomUIFilterType FilterType { get; private set; }

        public ICustomUIFilterValue Parameter { get; private set; }

        public ICustomUIFilterValue Result
        {
            get => 
                this.resultCore;
            set
            {
                if (!AreEqualOrDefault(this.resultCore, value))
                {
                    this.resultCore = value;
                    this.RaisePropertyChanged<ICustomUIFilterValue>(Expression.Lambda<Func<ICustomUIFilterValue>>(Expression.Property(Expression.Constant(this, typeof(CustomUIFilterDialogViewModel)), (MethodInfo) methodof(CustomUIFilterDialogViewModel.get_Result)), new ParameterExpression[0]));
                }
                else if (!AreEqualOrDefault(this.Parameter, value))
                {
                    this.RaisePropertyChanged<ICustomUIFilterValue>(Expression.Lambda<Func<ICustomUIFilterValue>>(Expression.Property(Expression.Constant(this, typeof(CustomUIFilterDialogViewModel)), (MethodInfo) methodof(CustomUIFilterDialogViewModel.get_Result)), new ParameterExpression[0]));
                }
            }
        }
    }
}

