namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public static class POCOViewModelExtensions
    {
        public static IAsyncCommand GetAsyncCommand<T>(this T viewModel, Expression<Func<T, Task>> methodExpression) => 
            GetAsyncCommandCore(viewModel, methodExpression);

        private static IAsyncCommand GetAsyncCommandCore(object viewModel, LambdaExpression methodExpression)
        {
            GetPOCOViewModel<object>(viewModel);
            ICommand commandCore = GetCommandCore(viewModel, methodExpression);
            if (!(commandCore is IAsyncCommand))
            {
                throw new ViewModelSourceException("Command is not async");
            }
            return (IAsyncCommand) commandCore;
        }

        public static IDelegateCommand GetCommand<T>(this T viewModel, Expression<Action<T>> methodExpression) => 
            GetCommandCore(viewModel, methodExpression);

        private static IDelegateCommand GetCommandCore(object viewModel, LambdaExpression methodExpression)
        {
            GetPOCOViewModel<object>(viewModel);
            string commandName = ViewModelSource.GetCommandName(ExpressionHelper.GetMethod(methodExpression));
            PropertyInfo property = viewModel.GetType().GetProperty(commandName);
            if (property == null)
            {
                throw new ViewModelSourceException($"Command not found: {commandName}.");
            }
            return (property.GetValue(viewModel, null) as IDelegateCommand);
        }

        [Obsolete("Use the GetAsyncCommand method instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool GetIsExecuting<T>(this T viewModel, Expression<Func<T, Task>> methodExpression) => 
            viewModel.GetAsyncCommand<T>(methodExpression).IsExecuting;

        public static T GetParentViewModel<T>(this object viewModel) where T: class => 
            (T) ((ISupportParentViewModel) viewModel).ParentViewModel;

        private static IPOCOViewModel GetPOCOViewModel<T>(T viewModel)
        {
            IPOCOViewModel model = viewModel as IPOCOViewModel;
            if (model == null)
            {
                throw new ViewModelSourceException("Object doesn't implement IPOCOViewModel.");
            }
            return model;
        }

        public static TService GetRequiredService<TService>(this object viewModel) where TService: class => 
            GetServiceContainer(viewModel).GetRequiredService<TService>(ServiceSearchMode.PreferLocal);

        public static TService GetRequiredService<TService>(this object viewModel, string key) where TService: class => 
            GetServiceContainer(viewModel).GetRequiredService<TService>(key, ServiceSearchMode.PreferLocal);

        public static TService GetService<TService>(this object viewModel) where TService: class => 
            GetServiceContainer(viewModel).GetService<TService>(ServiceSearchMode.PreferLocal);

        public static TService GetService<TService>(this object viewModel, string key) where TService: class => 
            GetServiceContainer(viewModel).GetService<TService>(key, ServiceSearchMode.PreferLocal);

        private static IServiceContainer GetServiceContainer(object viewModel)
        {
            if (!(viewModel is ISupportServices))
            {
                throw new ViewModelSourceException("Object doesn't implement ISupportServices.");
            }
            return ((ISupportServices) viewModel).ServiceContainer;
        }

        [Obsolete("Use the GetAsyncCommand method instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool GetShouldCancel<T>(this T viewModel, Expression<Func<T, Task>> methodExpression) => 
            viewModel.GetAsyncCommand<T>(methodExpression).ShouldCancel;

        public static bool HasError<T, TProperty>(this T viewModel, Expression<Func<T, TProperty>> propertyExpression)
        {
            IDataErrorInfo info = viewModel as IDataErrorInfo;
            return ((info != null) && !string.IsNullOrEmpty(info[BindableBase.GetPropertyNameFast(propertyExpression)]));
        }

        public static bool IsInDesignMode(this object viewModel) => 
            ViewModelBase.IsInDesignMode;

        public static void RaiseCanExecuteChanged<T>(this T viewModel, Expression<Action<T>> methodExpression)
        {
            RaiseCanExecuteChangedCore(viewModel, methodExpression);
        }

        public static void RaiseCanExecuteChanged<T>(this T viewModel, Expression<Func<T, Task>> methodExpression)
        {
            RaiseCanExecuteChangedCore(viewModel, methodExpression);
        }

        internal static void RaiseCanExecuteChangedCore(object viewModel, LambdaExpression methodExpression)
        {
            GetCommandCore(viewModel, methodExpression).RaiseCanExecuteChanged();
        }

        public static void RaisePropertiesChanged(this object viewModel)
        {
            GetPOCOViewModel<object>(viewModel).RaisePropertyChanged(string.Empty);
        }

        public static void RaisePropertyChanged<T, TProperty>(this T viewModel, Expression<Func<T, TProperty>> propertyExpression)
        {
            GetPOCOViewModel<T>(viewModel).RaisePropertyChanged(BindableBase.GetPropertyNameFast(propertyExpression));
        }

        public static T SetParentViewModel<T>(this T viewModel, object parentViewModel)
        {
            ((ISupportParentViewModel) viewModel).ParentViewModel = parentViewModel;
            return viewModel;
        }

        private static void UpdateFunctionBehaviorCore<T>(this T viewModel, Expression<Action<T>> methodExpression)
        {
            string name = ExpressionHelper.GetMethod(methodExpression).Name;
            GetPOCOViewModel<T>(viewModel).RaisePropertyChanged(name);
        }

        public static void UpdateFunctionBinding<T>(this T viewModel, Expression<Action<T>> methodExpression)
        {
            viewModel.UpdateFunctionBehaviorCore<T>(methodExpression);
        }

        public static void UpdateMethodToCommandCanExecute<T>(this T viewModel, Expression<Action<T>> methodExpression)
        {
            viewModel.UpdateFunctionBehaviorCore<T>(methodExpression);
        }
    }
}

