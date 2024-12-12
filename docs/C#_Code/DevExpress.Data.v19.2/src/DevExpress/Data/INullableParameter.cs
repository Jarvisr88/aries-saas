namespace DevExpress.Data
{
    using DevExpress.XtraEditors.Filtering;
    using System;

    public interface INullableParameter : IParameter, IFilterParameter
    {
        bool AllowNull { get; set; }
    }
}

