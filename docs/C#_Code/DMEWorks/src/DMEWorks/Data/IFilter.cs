namespace DMEWorks.Data
{
    using System;
    using System.Data;

    public interface IFilter
    {
        string GetKey();
        DataTable Process(DataTable Source);
    }
}

