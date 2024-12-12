namespace DevExpress.Data
{
    using DevExpress.XtraEditors.Filtering;
    using System;

    public interface IMultiValueParameter : IParameter, IFilterParameter
    {
        bool MultiValue { get; set; }
    }
}

