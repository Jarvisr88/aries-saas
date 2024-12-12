namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class DocumentHelper : IDisposable
    {
        private int pageIndex;
        protected PrintingDocument document;
        protected PageBuildEngine pageBuildEngine;

        public DocumentHelper(PrintingDocument document, PageBuildEngine pageBuildEngine)
        {
            this.document = document;
            this.pageBuildEngine = pageBuildEngine;
        }

        public virtual void AddPage(PSPage page)
        {
            this.document.Pages.AddPageInternal(page);
            this.document.PrintingSystem.OnAfterPagePrint(new PageEventArgs(page, null));
            this.OnAfterPagePrint(page);
        }

        public virtual void BuildPages()
        {
            this.BuildPagesCore();
            this.OnAfterBuildPages();
        }

        protected void BuildPagesCore()
        {
            this.document.Pages.Clear();
            Action<GroupingManager> callback = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Action<GroupingManager> local1 = <>c.<>9__4_0;
                callback = <>c.<>9__4_0 = groupingManager => groupingManager.Clear();
            }
            this.document.PrintingSystem.PerformIfNotNull<GroupingManager>(callback);
            IPageDataService service = this.document.PrintingSystem.GetService<IPageDataService>();
            if (service == null)
            {
                IPageDataService local3 = service;
            }
            else
            {
                service.Clear();
            }
            this.pageBuildEngine.BuildPages(this.document.Root);
        }

        public virtual void Dispose()
        {
            if (this.pageBuildEngine != null)
            {
                this.pageBuildEngine.Abort();
            }
        }

        protected virtual void OnAfterBuildPages()
        {
            this.pageBuildEngine.AfterBuildPages();
            this.document.AfterBuild();
        }

        protected void OnAfterPagePrint(PSPage page)
        {
            GroupingManager service = this.document.PrintingSystem.GetService<GroupingManager>();
            if (service != null)
            {
                if (page.X == 0f)
                {
                    this.pageIndex = service.TryBuildPageGroups(page.ID, page.Index) ? page.Index : -1;
                }
                else if (this.pageIndex >= 0)
                {
                    service.UpdatePageGroups(this.pageIndex);
                }
                service.PageBands.Remove(page.ID);
            }
            MergeBrickHelper helper = this.document.PrintingSystem.GetService<MergeBrickHelper>();
            if (helper != null)
            {
                helper.ProcessPage(this.document.PrintingSystem, page);
            }
        }

        public void StopPageBuilding()
        {
            if (this.pageBuildEngine != null)
            {
                this.pageBuildEngine.Stop();
            }
        }

        public virtual int PageCount =>
            this.document.Pages.Count;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentHelper.<>c <>9 = new DocumentHelper.<>c();
            public static Action<GroupingManager> <>9__4_0;

            internal void <BuildPagesCore>b__4_0(GroupingManager groupingManager)
            {
                groupingManager.Clear();
            }
        }
    }
}

