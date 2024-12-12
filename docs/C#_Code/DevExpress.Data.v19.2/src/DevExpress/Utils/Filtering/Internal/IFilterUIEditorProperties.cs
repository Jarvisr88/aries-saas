namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IFilterUIEditorProperties
    {
        void Assign(IFilterUIEditorProperties properties);

        DevExpress.Utils.Filtering.RangeUIEditorType? RangeUIEditorType { get; }

        DevExpress.Utils.Filtering.DateTimeRangeUIEditorType? DateTimeRangeUIEditorType { get; }

        DevExpress.Utils.Filtering.LookupUIEditorType? LookupUIEditorType { get; }

        DevExpress.Utils.Filtering.BooleanUIEditorType? BooleanUIEditorType { get; }

        DevExpress.Utils.Filtering.GroupUIEditorType? GroupUIEditorType { get; }

        System.ComponentModel.DataAnnotations.DataType? DataType { get; }

        Type EnumType { get; }

        bool? UseFlags { get; }
    }
}

