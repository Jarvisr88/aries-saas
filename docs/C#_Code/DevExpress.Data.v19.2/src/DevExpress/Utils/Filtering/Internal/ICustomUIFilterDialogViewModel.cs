namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFilterDialogViewModel : IServiceProvider, ILocalizableUIElement<CustomUIFilterDialogType>
    {
        string Path { get; }

        CustomUIFiltersType FiltersType { get; }

        CustomUIFilterType FilterType { get; }

        ICustomUIFilterValue Parameter { get; }

        ICustomUIFilterValue Result { get; set; }
    }
}

