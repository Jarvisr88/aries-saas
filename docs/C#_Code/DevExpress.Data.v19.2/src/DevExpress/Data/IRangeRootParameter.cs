namespace DevExpress.Data
{
    using DevExpress.XtraEditors.Filtering;
    using System;

    public interface IRangeRootParameter : IParameter, IFilterParameter
    {
        bool IsRange { get; }

        IRangeBoundaryParameter StartParameter { get; }

        IRangeBoundaryParameter EndParameter { get; }
    }
}

