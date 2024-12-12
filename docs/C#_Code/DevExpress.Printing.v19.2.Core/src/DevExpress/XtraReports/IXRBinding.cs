namespace DevExpress.XtraReports
{
    using System;

    public interface IXRBinding
    {
        string PropertyName { get; }

        object DataSource { get; }

        string DataMember { get; }
    }
}

