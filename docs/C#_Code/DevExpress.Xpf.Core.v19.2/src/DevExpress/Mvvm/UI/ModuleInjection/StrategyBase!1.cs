namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class StrategyBase<T> : BaseStrategy<T>, IStrategy, IBaseStrategy where T: DependencyObject
    {
        private object selectedViewModel;
        private bool focusOnSelectedViewModelChanged;

        public StrategyBase()
        {
            this.focusOnSelectedViewModelChanged = true;
            this.ViewModels = new ObservableCollection<object>();
        }

        void IStrategy.Clear()
        {
            this.OnClearing();
            this.ViewModels.Clear();
            this.OnClear();
        }

        object IStrategy.GetParentViewModel() => 
            this.GetParentViewModel();

        void IStrategy.Inject(object viewModel, Type viewType)
        {
            if ((viewModel != null) && !this.ViewModels.Contains(viewModel))
            {
                base.ViewSelector.Add(viewModel, viewType);
                this.ViewModels.Add(viewModel);
                this.OnInjected(viewModel);
            }
        }

        void IStrategy.Remove(object viewModel)
        {
            if ((viewModel != null) && this.ViewModels.Contains(viewModel))
            {
                base.ViewSelector.Remove(viewModel);
                this.ViewModels.Remove(viewModel);
                this.OnRemoved(viewModel);
            }
        }

        void IStrategy.Select(object viewModel, bool focus)
        {
            this.focusOnSelectedViewModelChanged = focus;
            this.SelectedViewModel = viewModel;
            this.focusOnSelectedViewModelChanged = true;
        }

        protected virtual object GetParentViewModel()
        {
            Func<FrameworkElement, object> evaluator = <>c<T>.<>9__24_0;
            if (<>c<T>.<>9__24_0 == null)
            {
                Func<FrameworkElement, object> local1 = <>c<T>.<>9__24_0;
                evaluator = <>c<T>.<>9__24_0 = x => x.DataContext;
            }
            object local2 = (base.Target as FrameworkElement).With<FrameworkElement, object>(evaluator);
            object local5 = local2;
            if (local2 == null)
            {
                object local3 = local2;
                local5 = (base.Target as FrameworkContentElement).With<FrameworkContentElement, object>(<>c<T>.<>9__24_1 ??= x => x.DataContext);
            }
            return local5;
        }

        protected override void InitializeCore()
        {
            base.InitializeCore();
            base.Owner.ConfigureChild(base.Target);
        }

        protected virtual void OnClear()
        {
        }

        protected virtual void OnClearing()
        {
        }

        protected virtual void OnInjected(object viewModel)
        {
        }

        protected virtual void OnRemoved(object viewModel)
        {
        }

        protected virtual void OnSelectedViewModelChanged(object oldValue, object newValue)
        {
            base.Owner.SelectViewModel(newValue);
        }

        protected virtual void OnSelectedViewModelPropertyChanged(object oldValue, object newValue, bool focus)
        {
            if (oldValue != newValue)
            {
                this.OnSelectedViewModelChanged(oldValue, newValue);
            }
        }

        protected ObservableCollection<object> ViewModels { get; private set; }

        protected object SelectedViewModel
        {
            get => 
                this.selectedViewModel;
            set
            {
                object selectedViewModel = this.selectedViewModel;
                this.selectedViewModel = value;
                this.OnSelectedViewModelPropertyChanged(selectedViewModel, this.selectedViewModel, this.focusOnSelectedViewModelChanged);
            }
        }

        object IStrategy.SelectedViewModel =>
            this.SelectedViewModel;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StrategyBase<T>.<>c <>9;
            public static Func<FrameworkElement, object> <>9__24_0;
            public static Func<FrameworkContentElement, object> <>9__24_1;

            static <>c()
            {
                StrategyBase<T>.<>c.<>9 = new StrategyBase<T>.<>c();
            }

            internal object <GetParentViewModel>b__24_0(FrameworkElement x) => 
                x.DataContext;

            internal object <GetParentViewModel>b__24_1(FrameworkContentElement x) => 
                x.DataContext;
        }
    }
}

