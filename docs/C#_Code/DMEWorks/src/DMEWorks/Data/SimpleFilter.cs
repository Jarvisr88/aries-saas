namespace DMEWorks.Data
{
    using System;
    using System.Data;

    public sealed class SimpleFilter : DMEWorks.Data.IFilter
    {
        private readonly string Filter;

        public SimpleFilter(string Filter)
        {
            this.Filter = Filter;
        }

        public string GetKey() => 
            this.Filter;

        public DataTable Process(DataTable Source)
        {
            DataTable table = Source.Clone();
            foreach (DataRow row in Source.Select(this.Filter))
            {
                table.ImportRow(row);
            }
            return table;
        }
    }
}

