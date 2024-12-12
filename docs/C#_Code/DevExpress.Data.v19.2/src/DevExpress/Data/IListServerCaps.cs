namespace DevExpress.Data
{
    using System;

    public interface IListServerCaps
    {
        bool CanFilter { get; }

        bool CanGroup { get; }

        bool CanSort { get; }
    }
}

