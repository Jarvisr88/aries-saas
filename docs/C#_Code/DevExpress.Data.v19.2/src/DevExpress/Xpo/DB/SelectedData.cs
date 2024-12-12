namespace DevExpress.Xpo.DB
{
    using System;

    [Serializable]
    public class SelectedData
    {
        public SelectStatementResult[] ResultSet;

        public SelectedData() : this(null)
        {
        }

        public SelectedData(params SelectStatementResult[] resultSet)
        {
            this.ResultSet = resultSet;
        }
    }
}

