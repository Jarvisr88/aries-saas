namespace DevExpress.XtraPrinting.Native.Interaction
{
    using DevExpress.Data;
    using System;
    using System.Runtime.InteropServices;

    public interface IInteractionService : IInteractionServiceBase
    {
        void AppendSorting(string sortData);
        void ApplySorting(string sortData);
        void RemoveSorting(string sortData);
        bool TryGetAppliedSorting(string sortData, out ColumnSortOrder sortOrder);
    }
}

