namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.ReportServer.IndexedCache;
    using DevExpress.ReportServer.Printing;
    using DevExpress.ReportServer.ServiceModel.Native;
    using DevExpress.ReportServer.ServiceModel.Native.RemoteOperations;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class PageListService : DevExpress.ReportServer.IndexedCache.IndexedCache<RemotePageInfo>, IPageListService, ICache<Page>, IDisposable, IEnumerable
    {
        private readonly IServiceProvider serviceProvider;
        private bool isRequestActive;
        private int[] requestedPageIndexes;

        public event ExceptionEventHandler RequestPagesException;

        public PageListService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override RemotePageInfo CreateFakeValue(int index, int count)
        {
            Page fakedPage = this.EmptyPageFactory.CreateEmptyPage(index, count);
            if (this.DefaultPageSettings != null)
            {
                fakedPage.PageData = new PageData(this.DefaultPageSettings.MarginsF, this.DefaultPageSettings.PaperKind, this.DefaultPageSettings.Landscape);
            }
            fakedPage.SetOwner(this.PageOwnerProvider.PageOwner, index);
            return new RemotePageInfo(fakedPage);
        }

        public IEnumerator GetEnumerator() => 
            new PageListEnumerator(base.cache);

        public bool PagesShouldBeLoaded(params int[] pageIndexes) => 
            base.EnsureIndexes(pageIndexes).Length != 0;

        private void requestPageOperation_OperationCompleted(object sender, ScalarOperationCompletedEventArgs<DeserializedPrintingSystem> e)
        {
            ((RequestPagesOperation) sender).OperationCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<DeserializedPrintingSystem>>(this.requestPageOperation_OperationCompleted);
            DeserializedPrintingSystem result = e.Result;
            Dictionary<int, RemotePageInfo> dictionary = new Dictionary<int, RemotePageInfo>();
            if (!base.IsDisposed)
            {
                for (int i = 0; i < this.requestedPageIndexes.Length; i++)
                {
                    Page realPage = result.Pages[i];
                    realPage.SetOwner(this.PageOwnerProvider.PageOwner, this.requestedPageIndexes[i]);
                    realPage.AssignWatermarkReference(result.Watermark);
                    dictionary[this.requestedPageIndexes[i]] = new RemotePageInfo(realPage, result);
                }
                this.isRequestActive = false;
                this.requestedPageIndexes = null;
                base.OnRequestCompleted(dictionary);
            }
        }

        private void requestPageOperation_RequestPagesException(object sender, ExceptionEventArgs args)
        {
            ((RequestPagesOperation) sender).RequestPagesException -= new ExceptionEventHandler(this.requestPageOperation_RequestPagesException);
            if (this.RequestPagesException != null)
            {
                this.RequestPagesException(this, args);
            }
        }

        protected override void StartRequestIfNeeded()
        {
            if (!this.isRequestActive)
            {
                List<int> list = new List<int>();
                for (int i = 0; i < base.Capacity; i++)
                {
                    if (base.cache[i].State == IndexedCacheItemState.Requested)
                    {
                        list.Add(i);
                    }
                }
                if (list.Count == 0)
                {
                    this.isRequestActive = false;
                }
                else
                {
                    this.requestedPageIndexes = list.ToArray();
                    RequestPagesOperation operation = this.PageOperationFactory.CreateRequestPagesOperation(this.requestedPageIndexes, (IBrickPagePairFactory) this.serviceProvider.GetService(typeof(IBrickPagePairFactory)));
                    operation.RequestPagesException += new ExceptionEventHandler(this.requestPageOperation_RequestPagesException);
                    operation.OperationCompleted += new EventHandler<ScalarOperationCompletedEventArgs<DeserializedPrintingSystem>>(this.requestPageOperation_OperationCompleted);
                    this.isRequestActive = true;
                    operation.Start();
                }
            }
        }

        internal XtraPageSettingsBase DefaultPageSettings { get; set; }

        private RemoteOperationFactory PageOperationFactory =>
            this.serviceProvider.GetService<RemoteOperationFactory>();

        private IEmptyPageFactory EmptyPageFactory =>
            this.serviceProvider.GetService<IEmptyPageFactory>();

        private IPageOwnerProvider PageOwnerProvider =>
            this.serviceProvider.GetService<IPageOwnerProvider>();

        public Page this[int index]
        {
            get => 
                base[index].Page;
            set => 
                base[index].Page = value;
        }
    }
}

