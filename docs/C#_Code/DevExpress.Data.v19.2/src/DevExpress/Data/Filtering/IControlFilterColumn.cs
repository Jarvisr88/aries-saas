namespace DevExpress.Data.Filtering
{
    using System;

    public interface IControlFilterColumn
    {
        System.Type Type { get; }

        string Name { get; }

        string Caption { get; }
    }
}

