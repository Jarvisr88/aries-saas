namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Data;

    public class BindingSourceDataControllerHelper : BaseDataViewControllerHelper
    {
        public BindingSourceDataControllerHelper(DataControllerBase controller);

        public override DataView View { get; }
    }
}

