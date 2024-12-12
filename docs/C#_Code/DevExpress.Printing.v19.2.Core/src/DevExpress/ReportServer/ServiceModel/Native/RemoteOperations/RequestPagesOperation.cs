namespace DevExpress.ReportServer.ServiceModel.Native.RemoteOperations
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.ReportServer.ServiceModel.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class RequestPagesOperation : RemoteScalarOperation<DeserializedPrintingSystem>
    {
        private readonly int[] pageIndexes;
        private const int maximumPagesPerRequest = 50;
        private int indexesRequested;
        private readonly DeserializedPrintingSystem ps;

        [Category("Document Creation")]
        public event ExceptionEventHandler RequestPagesException;

        public RequestPagesOperation(IReportServiceClient client, DocumentId documentId, TimeSpan updateInterval, int[] pageIndexes, IBrickPagePairFactory factory) : base(client, updateInterval, documentId)
        {
            this.pageIndexes = pageIndexes;
            this.ps = new DeserializedPrintingSystem();
            this.ps.AddService(typeof(IBrickPagePairFactory), factory);
        }

        private void Client_GetPagesCompleted(object sender, ScalarOperationCompletedEventArgs<byte[]> e)
        {
            if (base.IsSameInstanceId(e.UserState))
            {
                if (e.Error != null)
                {
                    this.UnsubscribeClientEvents();
                    if (this.RequestPagesException != null)
                    {
                        this.RequestPagesException(this, new ExceptionEventArgs(e.Error));
                    }
                }
                else
                {
                    Deserialize(this.ps.Document, e.Result);
                    if (this.pageIndexes.Length > this.indexesRequested)
                    {
                        this.StartRequestPages();
                    }
                    else
                    {
                        this.UnsubscribeClientEvents();
                        ScalarOperationCompletedEventArgs<DeserializedPrintingSystem> eventArgs = new ScalarOperationCompletedEventArgs<DeserializedPrintingSystem>(this.ps, e.Error, e.Cancelled, e.UserState);
                        base.RaiseOperationCompleted(eventArgs);
                    }
                }
            }
        }

        private static void Deserialize(Document document, byte[] serializedData)
        {
            using (Stream stream = new MemoryStream(serializedData))
            {
                document.Deserialize(stream, new PrintingSystemXmlSerializer());
            }
        }

        public override void Start()
        {
            base.Client.GetPagesCompleted += new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.Client_GetPagesCompleted);
            this.StartRequestPages();
        }

        private void StartRequestPages()
        {
            int length = Math.Min(50, this.pageIndexes.Length - this.indexesRequested);
            int[] destinationArray = new int[length];
            Array.Copy(this.pageIndexes, this.indexesRequested, destinationArray, 0, length);
            this.indexesRequested += length;
            base.Client.GetPagesAsync(base.documentId, destinationArray, PageCompatibility.Prnx, base.instanceId);
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        protected override void UnsubscribeClientEvents()
        {
            base.Client.GetPagesCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.Client_GetPagesCompleted);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static int MaximumPagesPerRequest =>
            50;

        public override bool CanStop =>
            false;
    }
}

