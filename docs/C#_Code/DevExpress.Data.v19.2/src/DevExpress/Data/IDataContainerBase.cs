namespace DevExpress.Data
{
    using System;

    public interface IDataContainerBase
    {
        string DataMember { get; set; }

        object DataSource { get; set; }
    }
}

