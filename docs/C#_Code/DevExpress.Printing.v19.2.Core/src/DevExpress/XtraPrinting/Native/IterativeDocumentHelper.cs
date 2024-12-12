namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public class IterativeDocumentHelper : DocumentHelper
    {
        private List<PSPage> pageBuffer;
        private bool isDisposed;

        public IterativeDocumentHelper(PrintingDocument document, IterativePageBuildEngine pageBuildEngine) : base(document, pageBuildEngine)
        {
            this.pageBuffer = new List<PSPage>();
            this.PageBuildEngine.AfterBuild += new Action0(this.AfterBuildProc);
        }

        public override void AddPage(PSPage page)
        {
            PrintingDocument document = base.document;
            lock (document)
            {
                this.pageBuffer.Add(page);
            }
            this.ContentChangedProc();
            base.document.PrintingSystem.OnAfterPagePrint(new PageEventArgs(page, null));
        }

        private void AfterBuildProc()
        {
            if (!this.isDisposed && base.document.IsCreating)
            {
                this.UpdatePages();
                this.PageBuildEngine.AfterBuildPages();
                base.document.AfterBuild();
            }
        }

        public override void BuildPages()
        {
            base.BuildPagesCore();
        }

        private void ContentChangedProc()
        {
            if (!this.isDisposed)
            {
                int count = base.document.Pages.Count;
                PrintingDocument document = base.document;
                lock (document)
                {
                    if (this.pageBuffer.Count >= this.PageBuildEngine.GetBufferSize(count))
                    {
                        this.UpdatePages();
                    }
                    else
                    {
                        return;
                    }
                }
                if (count != base.document.Pages.Count)
                {
                    base.document.OnContentChanged();
                }
            }
        }

        public override void Dispose()
        {
            if (this.PageBuildEngine != null)
            {
                this.PageBuildEngine.AfterBuild -= new Action0(this.AfterBuildProc);
            }
            base.Dispose();
            this.isDisposed ??= true;
        }

        private void UpdatePages()
        {
            for (int i = 0; i < this.pageBuffer.Count; i++)
            {
                PSPage page = this.pageBuffer[i];
                base.document.Pages.AddPageInternal(page);
                base.OnAfterPagePrint(page);
            }
            this.pageBuffer.Clear();
        }

        private IterativePageBuildEngine PageBuildEngine =>
            (IterativePageBuildEngine) base.pageBuildEngine;

        public override int PageCount =>
            base.document.Pages.Count + this.pageBuffer.Count;
    }
}

