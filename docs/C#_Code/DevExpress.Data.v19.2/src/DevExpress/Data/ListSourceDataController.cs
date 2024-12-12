namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class ListSourceDataController : BaseListSourceDataController
    {
        public override void AddNewRow();
        public virtual void CancelNewRowEdit();
        public virtual int EndNewRowEdit();
        public void SetListSource(BindingContext context, object dataSource, string dataMember);

        public IList ListSource { get; set; }
    }
}

