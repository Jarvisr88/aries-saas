namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Internal;
    using DevExpress.Office.Model;
    using System;

    public class FakeDocumentModel : DocumentModelBase<int>, IDocumentModelPart
    {
        private static FakeDocumentModel instance;
        private static readonly object syncRoot = new object();

        protected FakeDocumentModel()
        {
            this.SwitchToEmptyHistory(true);
        }

        public override ExportHelper<int, bool> CreateDocumentExportHelper(int documentFormat) => 
            null;

        protected internal override ImportHelper<int, bool> CreateDocumentImportHelper() => 
            null;

        protected internal override IExportManagerService<int, bool> GetExportManagerService() => 
            null;

        protected internal override IImportManagerService<int, bool> GetImportManagerService() => 
            null;

        public override void OnBeginUpdate()
        {
        }

        public override void OnCancelUpdate()
        {
        }

        public override void OnEndUpdate()
        {
        }

        public override void OnFirstBeginUpdate()
        {
        }

        protected internal override void OnHistoryModifiedChanged(object sender, EventArgs e)
        {
        }

        protected internal override void OnHistoryOperationCompleted(object sender, EventArgs e)
        {
        }

        public override void OnLastCancelUpdate()
        {
        }

        public override void OnLastEndUpdate()
        {
        }

        public static FakeDocumentModel Instance
        {
            get
            {
                if (instance == null)
                {
                    object syncRoot = FakeDocumentModel.syncRoot;
                    lock (syncRoot)
                    {
                        instance ??= new FakeDocumentModel();
                    }
                }
                return instance;
            }
        }

        public override IDocumentModelPart MainPart =>
            this;

        public IDocumentModel DocumentModel =>
            this;
    }
}

