namespace DevExpress.Office
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;

    public class OfficeDataController : GridDataController
    {
        protected override IList GetListSource()
        {
            if (base.DataSource == null)
            {
                return null;
            }
            DataSet dataSource = base.DataSource as DataSet;
            if ((dataSource != null) && (!string.IsNullOrEmpty(base.DataMember) && (dataSource.Tables != null)))
            {
                int index = dataSource.Tables.IndexOf(base.DataMember);
                if (index >= 0)
                {
                    IListSource source = dataSource.Tables[index];
                    if (source != null)
                    {
                        return source.GetList();
                    }
                }
            }
            return base.GetListSource();
        }
    }
}

