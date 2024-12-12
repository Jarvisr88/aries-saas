namespace DevExpress.Data
{
    using DevExpress.XtraEditors.Filtering;
    using System;

    public interface IParameter : IFilterParameter
    {
        object Value { get; set; }
    }
}

