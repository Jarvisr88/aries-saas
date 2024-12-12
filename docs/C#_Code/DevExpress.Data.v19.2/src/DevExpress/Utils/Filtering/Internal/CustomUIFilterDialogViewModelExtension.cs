namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public static class CustomUIFilterDialogViewModelExtension
    {
        internal static ICustomUIFilterValue CreateValue(this ICustomUIFilterDialogViewModel viewModel, CustomUIFilterType filterType, params object[] values) => 
            (viewModel.GetService(typeof(ICustomUIFilterValuesFactory)) as ICustomUIFilterValuesFactory).Get<ICustomUIFilterValuesFactory, ICustomUIFilterValue>(factory => factory.Create(filterType, values), null);

        public static void SetResult(this ICustomUIFilterDialogViewModel viewModel, params object[] values)
        {
            viewModel.Result = viewModel.CreateValue(viewModel.FilterType, values);
        }
    }
}

