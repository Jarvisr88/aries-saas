namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal static class ViewModelInitializer
    {
        private static bool IsCycleDetected(object viewModel, object parentViewModel)
        {
            object obj2 = parentViewModel;
            while (obj2 != null)
            {
                if (obj2 == viewModel)
                {
                    return true;
                }
                Func<ISupportParentViewModel, object> evaluator = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<ISupportParentViewModel, object> local1 = <>c.<>9__8_0;
                    evaluator = <>c.<>9__8_0 = x => x.ParentViewModel;
                }
                obj2 = (obj2 as ISupportParentViewModel).With<ISupportParentViewModel, object>(evaluator);
                if (obj2 == parentViewModel)
                {
                    return false;
                }
            }
            return false;
        }

        private static void SetViewModelDocumentOwner(object viewModel, IDocumentOwner documentOwner)
        {
            if (viewModel != null)
            {
                Func<object, IDocumentContent> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<object, IDocumentContent> local1 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = x => x as IDocumentContent;
                }
                viewModel.With<object, IDocumentContent>(evaluator).Do<IDocumentContent>(x => x.DocumentOwner = documentOwner);
            }
        }

        public static void SetViewModelDocumentOwner(DependencyObject view, IDocumentOwner documentOwner)
        {
            SetViewModelDocumentOwner(ViewHelper.GetViewModelFromView(view), documentOwner);
        }

        private static void SetViewModelParameter(object viewModel, object parameter)
        {
            if (viewModel != null)
            {
                Func<object, ISupportParameter> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<object, ISupportParameter> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x as ISupportParameter;
                }
                viewModel.With<object, ISupportParameter>(evaluator).Do<ISupportParameter>(x => x.Parameter = parameter);
            }
        }

        public static void SetViewModelParameter(DependencyObject view, object parameter)
        {
            SetViewModelParameter(ViewHelper.GetViewModelFromView(view), parameter);
        }

        private static void SetViewModelParentViewModel(object viewModel, object parentViewModel)
        {
            if ((viewModel != null) && !IsCycleDetected(viewModel, parentViewModel))
            {
                Func<object, ISupportParentViewModel> evaluator = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<object, ISupportParentViewModel> local1 = <>c.<>9__6_0;
                    evaluator = <>c.<>9__6_0 = x => x as ISupportParentViewModel;
                }
                viewModel.With<object, ISupportParentViewModel>(evaluator).Do<ISupportParentViewModel>(x => x.ParentViewModel = parentViewModel);
            }
        }

        public static void SetViewModelParentViewModel(DependencyObject view, object parentViewModel)
        {
            SetViewModelParentViewModel(ViewHelper.GetViewModelFromView(view), parentViewModel);
        }

        public static void SetViewModelProperties(object viewModel, object parameter, object parentViewModel, IDocumentOwner documentOwner)
        {
            if (viewModel != null)
            {
                if (parameter != null)
                {
                    SetViewModelParameter(viewModel, parameter);
                }
                if (parentViewModel != null)
                {
                    SetViewModelParentViewModel(viewModel, parentViewModel);
                }
                if (documentOwner != null)
                {
                    SetViewModelDocumentOwner(viewModel, documentOwner);
                }
            }
        }

        public static void SetViewModelProperties(DependencyObject view, object parameter, object parentViewModel, IDocumentOwner documentOwner)
        {
            if (view != null)
            {
                if ((ViewModelExtensions.NotSetParameter == ViewModelExtensions.GetParameter(view)) || (parameter != null))
                {
                    ViewModelExtensions.SetParameter(view, parameter);
                }
                if ((ViewModelExtensions.GetParentViewModel(view) == null) || (parentViewModel != null))
                {
                    ViewModelExtensions.SetParentViewModel(view, parentViewModel);
                }
                if ((ViewModelExtensions.GetDocumentOwner(view) == null) || (documentOwner != null))
                {
                    ViewModelExtensions.SetDocumentOwner(view, documentOwner);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelInitializer.<>c <>9 = new ViewModelInitializer.<>c();
            public static Func<object, ISupportParameter> <>9__5_0;
            public static Func<object, ISupportParentViewModel> <>9__6_0;
            public static Func<object, IDocumentContent> <>9__7_0;
            public static Func<ISupportParentViewModel, object> <>9__8_0;

            internal object <IsCycleDetected>b__8_0(ISupportParentViewModel x) => 
                x.ParentViewModel;

            internal IDocumentContent <SetViewModelDocumentOwner>b__7_0(object x) => 
                x as IDocumentContent;

            internal ISupportParameter <SetViewModelParameter>b__5_0(object x) => 
                x as ISupportParameter;

            internal ISupportParentViewModel <SetViewModelParentViewModel>b__6_0(object x) => 
                x as ISupportParentViewModel;
        }
    }
}

