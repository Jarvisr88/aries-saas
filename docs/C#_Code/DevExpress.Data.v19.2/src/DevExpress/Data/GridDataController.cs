namespace DevExpress.Data
{
    using System;
    using System.Collections;

    public class GridDataController : BaseGridControllerEx
    {
        protected virtual IList GetListSource();
        protected override void OnDataSourceChanged();
    }
}

