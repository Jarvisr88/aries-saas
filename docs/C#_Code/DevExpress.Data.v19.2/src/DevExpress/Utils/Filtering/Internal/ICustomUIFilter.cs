namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Threading.Tasks;

    public interface ICustomUIFilter : ILocalizableUIElement<CustomUIFilterType>
    {
        bool Allow(ICustomUIFiltersOptions userOptions);
        Task Edit(object owner);
        void EndEdit();
        bool Reset();

        int Order { get; }

        string Group { get; }

        string ParentGroup { get; }

        bool Visible { get; }

        bool IsActive { get; }

        ICustomUIFilterValue Value { get; }
    }
}

