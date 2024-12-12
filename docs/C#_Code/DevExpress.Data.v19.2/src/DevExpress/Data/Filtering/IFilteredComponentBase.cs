namespace DevExpress.Data.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IFilteredComponentBase
    {
        event EventHandler PropertiesChanged;

        event EventHandler RowFilterChanged;

        CriteriaOperator RowCriteria { get; set; }
    }
}

