namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IEndUserFilteringElement
    {
        string Path { get; }

        string Caption { get; }

        string Description { get; }

        string Layout { get; }

        int Order { get; }

        bool ApplyFormatInEditMode { get; }

        string DataFormatString { get; }

        string NullDisplayText { get; }

        System.ComponentModel.DataAnnotations.DataType? DataType { get; }

        Type EnumDataType { get; }

        bool IsVisible { get; }

        bool IsEnabled { get; }
    }
}

