namespace DevExpress.XtraEditors.Filtering
{
    using System;

    public interface IFilterParameter
    {
        string Name { get; }

        System.Type Type { get; }
    }
}

