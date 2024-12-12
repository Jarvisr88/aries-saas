namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.XtraPrinting.Caching;
    using DevExpress.XtraPrinting.Native.Caching;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    internal class PrnxContinuousInfoExporter
    {
        private IStreamingXmlSerializer serializer;
        private IStreamingItemsCollection continuousInfoProperties;
        private IStreamingDocument document;
        private StreamingSerializationRootObject rootObject;
        private DocumentStorage storage;
        private Stream continuousInfoStream;
        private ContinuousExportInfo info;
        private StreamingSerializationContext context;
        private IBuildTaskFactory taskFactory;
        private Task lastTask;

        public PrnxContinuousInfoExporter(IStreamingDocument document, DocumentStorage storage, StreamingSerializationContext context)
        {
            this.document = document;
            this.storage = storage;
            this.context = context;
            IBuildTaskFactory service = document.PrintingSystem.GetService<IBuildTaskFactory>();
            IBuildTaskFactory local3 = service;
            if (service == null)
            {
                IBuildTaskFactory local2 = service;
                local3 = new DefaultBuildTaskFactory();
            }
            this.taskFactory = local3;
            ICachedStreamingDocument document2 = document as ICachedStreamingDocument;
            IStoredIDProvider storedIdProvider = document2?.StoredIdProvider;
            this.rootObject = new StreamingSerializationRootObject(storedIdProvider);
        }

        public void EndExport()
        {
            this.WaitForExportFinished();
            this.ExportCollectedBricks();
            this.WaitForExportFinished();
            this.serializer.EndWrite();
            this.continuousInfoStream.Flush();
        }

        public void ExportCollectedBricks()
        {
            IStreamingContinuousInfo streamingInfo = this.info as IStreamingContinuousInfo;
            if ((streamingInfo != null) && ((this.lastTask == null) || this.lastTask.IsCompleted))
            {
                this.lastTask = this.taskFactory.CreateTask(delegate {
                    streamingInfo.FixChunk();
                    if (this.info.Bricks.Count != 0)
                    {
                        if (this.continuousInfoProperties != null)
                        {
                            this.continuousInfoProperties.SetItemsSource(this.info.Bricks);
                        }
                        else
                        {
                            DocumentSerializationCollection collection1 = new DocumentSerializationCollection();
                            collection1.Add(this.info);
                            DocumentSerializationCollection objects = collection1;
                            this.continuousInfoProperties = (IStreamingItemsCollection) this.serializer.SerializeInDepthToLevel(objects, this.info.Bricks);
                        }
                        try
                        {
                            StoredIDSerializationManager.BeginSerialize();
                            this.rootObject.PrepareSerialization(this.info);
                            this.serializer.SerializePart(this.continuousInfoProperties);
                            this.continuousInfoProperties.IncreaseStartIndex(this.info.Bricks.Count);
                        }
                        finally
                        {
                            StoredIDSerializationManager.EndSerialize();
                        }
                    }
                });
            }
        }

        public void StartExport()
        {
            this.serializer = new StreamingXmlSerializer(this.context);
            this.continuousInfoStream = this.storage.ExportInfoWriteStream;
            this.info = this.document.GetContinuousExportInfo();
            this.serializer.BeginWrite(this.rootObject, this.continuousInfoStream, string.Empty, null);
        }

        public void WaitForExportFinished()
        {
            if ((this.lastTask != null) && !this.lastTask.IsCompleted)
            {
                this.lastTask.Wait();
            }
        }
    }
}

