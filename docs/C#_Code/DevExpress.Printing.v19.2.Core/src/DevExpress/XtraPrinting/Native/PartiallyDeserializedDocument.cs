namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Core.Native.Serialization;
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Caching;
    using DevExpress.XtraPrinting.Native.Caching;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    [SerializationContext(typeof(StreamingSerializationContext))]
    public class PartiallyDeserializedDocument : DeserializedDocument, IMaxPageSizeProvider
    {
        private PSUpdatedObjects updatedObjects;
        private StreamingDocumentSerializationOptions options;
        private ContinuousExportInfo continuousInfo;
        private DocumentStorage storage;
        private LimitedDeque<PartiallyDeserializedPage> restoringDeque;
        private Task restoringTask;
        private StreamingSerializationContext serializationContext;
        private IBuildTaskFactory buildTaskFactory;

        public event ContentChangedEventHandler ContentChanged;

        public event EventHandler PageBuildingStopped;

        public event EventHandler PageSettingsChanged;

        public event EventHandler ProgressReflectorIncremented;

        public event EventHandler ScaleFactorChanged;

        public PartiallyDeserializedDocument(PrintingSystemBase ps, DocumentStorage storage) : base(ps)
        {
            this.updatedObjects = new PSUpdatedObjects();
            this.restoringDeque = new LimitedDeque<PartiallyDeserializedPage>(0x100);
            this.serializationContext = new StreamingSerializationContext();
            if (storage == null)
            {
                throw new ArgumentNullException("storage");
            }
            this.storage = storage;
            this.options = new StreamingDocumentSerializationOptions(this, this.PageCount);
            base.SetRoot(new RootDocumentBand(ps));
            this.UpdateState(storage);
        }

        internal override void ClearContent()
        {
            this.Clear();
        }

        public void ClearPageBuffer()
        {
            PartiallyDeserializedInnerPageList innerList = base.Pages.InnerList as PartiallyDeserializedInnerPageList;
            if (innerList != null)
            {
                innerList.ClearPageBuffer();
            }
        }

        public static PartiallyDeserializedDocument CreateAndRestore(PrintingSystemBase ps, DocumentStorage storage)
        {
            PartiallyDeserializedDocument document = CreateWithoutRestore(ps, storage);
            document.Restore();
            return document;
        }

        public PartiallyDeserializedPage CreateEmptyPage(int index)
        {
            PartiallyDeserializedPage page;
            if (index >= this.storage.PageDataList.Count)
            {
                page = new PartiallyDeserializedPage(new ReadonlyPageData(base.ps.PageSettings.Data));
            }
            else
            {
                SerializedPageDataList.SerializedPageData data = this.storage.PageDataList[index];
                page = new PartiallyDeserializedPage(data.PageData) {
                    ID = data.PageID
                };
                if (data.OriginalPageCount <= 0)
                {
                    page.OriginalPageCount = this.storage.PageDataList.OriginalPageCount;
                    page.OriginalIndex = index;
                }
                else
                {
                    page.OriginalPageCount = data.OriginalPageCount;
                    page.OriginalIndex = data.OriginalPageIndex;
                }
            }
            page.SetOwner(base.Pages, index);
            return page;
        }

        protected override PageList CreatePageList() => 
            new PartiallyDeserializedPageList(this);

        protected override void CreateSerializationObjects()
        {
            base.styles = new ObjectCache();
            base.images = new ObjectCache(ImageEntry.ImageEntryEqualityComparer.Instance);
            ObjectCache bricks = base.bricks;
            if (base.bricks == null)
            {
                ObjectCache local1 = base.bricks;
                bricks = new ObjectCache();
            }
            this.bricks = bricks;
        }

        private DocumentStreamingDeserializationCollection CreateSerializedObjects()
        {
            DocumentStreamingDeserializationCollection deserializations = new DocumentStreamingDeserializationCollection(this);
            deserializations.Add(this.options);
            deserializations.Add(new XtraObjectInfo("UpdatedObjects", this.updatedObjects));
            deserializations.AddSerializedPageDataList(this.storage.PageDataList);
            return deserializations;
        }

        public static PartiallyDeserializedDocument CreateWithoutRestore(PrintingSystemBase ps, DocumentStorage storage)
        {
            PartiallyDeserializedDocument doc = new PartiallyDeserializedDocument(ps, storage);
            ps.SetDocument(doc);
            return doc;
        }

        protected override void DisposePages()
        {
            foreach (PartiallyDeserializedPage page in ((PartiallyDeserializedInnerPageList) base.fPages.InnerList).AlivePages)
            {
                if (page.Restored)
                {
                    ((IDisposable) page).Dispose();
                }
            }
        }

        protected internal override void End(bool buildPagesInBackground)
        {
            this.Restore();
        }

        protected override double GetBricksRightToPageWidthRatio() => 
            (double) (this.options.MaxBrickRight / base.UsefulPageRect.Width);

        protected internal override ContinuousExportInfo GetContinuousExportInfo()
        {
            ContinuousExportInfo continuousInfo = this.continuousInfo;
            if (this.continuousInfo == null)
            {
                ContinuousExportInfo local1 = this.continuousInfo;
                continuousInfo = this.continuousInfo = this.RestoreContinuousInfoFromPrnx(this.storage);
            }
            return continuousInfo;
        }

        protected internal override void HandleNewPageSettings()
        {
            if (this.PageSettingsChanged != null)
            {
                this.PageSettingsChanged(this, EventArgs.Empty);
            }
        }

        protected internal override void HandleNewScaleFactor()
        {
            if (this.ScaleFactorChanged != null)
            {
                this.ScaleFactorChanged(this, EventArgs.Empty);
            }
        }

        internal void IncProgressReflector()
        {
            EventHandler progressReflectorIncremented = this.ProgressReflectorIncremented;
            if (progressReflectorIncremented != null)
            {
                progressReflectorIncremented(this, EventArgs.Empty);
            }
        }

        protected override void NullCaches()
        {
        }

        private void OnContentChanged(bool guaranteed)
        {
            ContentChangedEventHandler contentChanged = this.ContentChanged;
            if (contentChanged != null)
            {
                contentChanged(new ContentChangedEventArgs(guaranteed));
            }
        }

        protected override void OnEndDeserializingCore()
        {
            if (base.Pages.Count > 0)
            {
                ((ISupportInitialize) base.PrintingSystem).BeginInit();
                try
                {
                    base.PrintingSystem.PageSettings.Assign(new PageData(base.Pages[0].PageData));
                }
                finally
                {
                    ((ISupportInitialize) base.PrintingSystem).EndInit();
                }
            }
            base.PrintingSystem.Graph.PageUnit = GraphicsUnit.Document;
            base.PrintingSystem.SetCommandVisibility(PSCommandHelper.ExportCommands, CommandVisibility.All, Priority.Low);
            base.PrintingSystem.SetCommandVisibility(PSCommandHelper.SendCommands, CommandVisibility.All, Priority.Low);
            this.OnContentChanged(false);
            base.Deserializing = false;
        }

        protected override void OnStartDeserializingCore()
        {
            this.CreateSerializationObjects();
            base.Deserializing = true;
        }

        public virtual void QueueRestoringPage(PartiallyDeserializedPage page)
        {
            if (!page.Restored)
            {
                this.restoringDeque.Push(page);
                this.restoringTask ??= this.BuildTaskFactory.CreateTask(delegate {
                    while (!this.restoringDeque.Empty)
                    {
                        if (!this.restoringDeque.Pop().RestorePage())
                        {
                            continue;
                        }
                        this.OnContentChanged(false);
                    }
                    this.restoringTask = null;
                    this.OnContentChanged(true);
                });
            }
        }

        public void Reset()
        {
            this.UpdateState(this.storage);
            base.bricks = null;
            this.CreateSerializationObjects();
            base.Pages.Clear();
            this.UpdatedObjects.Clear();
        }

        public void Restore()
        {
            if (!this.storage.HasDocument)
            {
                throw new InvalidOperationException("Storage hasn't any document stream");
            }
            using (Stream stream = this.storage.RestoreDocument())
            {
                new StreamingXmlSerializer(this.serializationContext).DeserializeObjects(this, this.CreateSerializedObjects(), stream, string.Empty, null);
            }
            base.Name = this.options.Name;
            base.state = DocumentState.Created;
        }

        private ContinuousExportInfo RestoreContinuousInfoFromPrnx(DocumentStorage storage)
        {
            if (!storage.HasDocument || !storage.HasContinuousInfo)
            {
                return ContinuousExportInfo.Empty;
            }
            Func<EditingField, bool> predicate = <>c.<>9__63_0;
            if (<>c.<>9__63_0 == null)
            {
                Func<EditingField, bool> local1 = <>c.<>9__63_0;
                predicate = <>c.<>9__63_0 = e => e.Brick != null;
            }
            Func<EditingField, int> keySelector = <>c.<>9__63_1;
            if (<>c.<>9__63_1 == null)
            {
                Func<EditingField, int> local2 = <>c.<>9__63_1;
                keySelector = <>c.<>9__63_1 = e => e.Brick.StoredId;
            }
            Dictionary<int, EditingField> dictionary = base.PrintingSystem.EditingFields.Where<EditingField>(predicate).ToDictionary<EditingField, int>(keySelector);
            using (Stream stream = storage.ExportInfoReadStream)
            {
                StreamingXmlSerializer serializer = new StreamingXmlSerializer(this.SerializationContext);
                DeserializeChildPropertiesEventHandler handler1 = <>c.<>9__63_2;
                if (<>c.<>9__63_2 == null)
                {
                    DeserializeChildPropertiesEventHandler local3 = <>c.<>9__63_2;
                    handler1 = <>c.<>9__63_2 = delegate (DeserializeChildPropertiesEventArgs e) {
                        e.DeserializeChildrenVirtually = e.Context.DeserializationPath == "ContinuousExportInfo.Bricks";
                    };
                }
                serializer.DeserializeChildProperties += handler1;
                ContinuousExportInfo deserializationContinuousExportInfo = this.GetDeserializationContinuousExportInfo();
                DocumentSerializationCollection objects = new DocumentSerializationCollection();
                objects.Add(deserializationContinuousExportInfo);
                serializer.DeserializeObjects(this, objects, stream, string.Empty, null);
                foreach (BrickLayoutInfo info2 in deserializationContinuousExportInfo.Bricks)
                {
                    Brick brick = info2.Brick;
                    if (brick != null)
                    {
                        EditingField field;
                        brick.Initialize(base.PrintingSystem, brick.Rect, false);
                        if (brick.InnerBrickList != null)
                        {
                            this.SetPrintingSystem(brick.InnerBrickList.OfType<Brick>());
                        }
                        this.UpdatedObjects.RestorePageBrickValues(brick, storage.PageDataList);
                        if (dictionary.TryGetValue(brick.StoredId, out field))
                        {
                            field.AssignEditValueToBrick((VisualBrick) brick);
                        }
                    }
                }
                return deserializationContinuousExportInfo;
            }
        }

        internal override void Serialize(Stream stream, XtraSerializer serializer)
        {
            List<Page> list = base.Pages.ToList<Page>();
            try
            {
                base.Serialize(stream, serializer);
            }
            finally
            {
                list.Clear();
            }
        }

        private void SetPrintingSystem(IEnumerable<Brick> bricks)
        {
            foreach (Brick brick in bricks)
            {
                brick.Initialize(base.PrintingSystem, brick.Rect, false);
                if (brick.InnerBrickList != null)
                {
                    this.SetPrintingSystem(brick.InnerBrickList.OfType<Brick>());
                }
            }
        }

        public void SetState(DocumentState state)
        {
            base.state = state;
        }

        protected internal override void StopPageBuilding()
        {
            if (this.PageBuildingStopped != null)
            {
                this.PageBuildingStopped(this, EventArgs.Empty);
            }
        }

        public void UpdatePages()
        {
            int count = base.Pages.Count;
            int num2 = Math.Max(this.storage.PageCount, this.storage.PageDataList.Count) - count;
            ((PartiallyDeserializedInnerPageList) base.Pages.InnerList).Expand(num2);
        }

        public void UpdatePages(int pageCount)
        {
            PartiallyDeserializedInnerPageList innerList = (PartiallyDeserializedInnerPageList) base.Pages.InnerList;
            int count = pageCount - innerList.Count;
            if (count < 0)
            {
                throw new InvalidOperationException();
            }
            innerList.Expand(count);
        }

        private void UpdateState(DocumentStorage storage)
        {
            this.state = storage.HasDocument ? DocumentState.Created : DocumentState.Empty;
        }

        private IBuildTaskFactory BuildTaskFactory
        {
            get
            {
                IBuildTaskFactory buildTaskFactory = this.buildTaskFactory;
                if (this.buildTaskFactory == null)
                {
                    IBuildTaskFactory local1 = this.buildTaskFactory;
                    buildTaskFactory = this.buildTaskFactory = base.PrintingSystem.GetService<IBuildTaskFactory>() ?? new DefaultBuildTaskFactory();
                }
                return buildTaskFactory;
            }
        }

        public override bool CanPerformContinuousExport =>
            this.storage.HasDocument && this.options.BuildContinuousInfo;

        internal PSUpdatedObjects UpdatedObjects =>
            this.updatedObjects;

        internal DocumentStorage Storage =>
            this.storage;

        internal StreamingSerializationContext SerializationContext =>
            this.serializationContext;

        internal StreamingDocumentSerializationOptions SerializationOptions =>
            this.options;

        public override bool IsEmpty =>
            base.Pages.Count == 0;

        SizeF IMaxPageSizeProvider.MaxPageSize =>
            this.Storage.PageDataList.MaxPageSize;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PartiallyDeserializedDocument.<>c <>9 = new PartiallyDeserializedDocument.<>c();
            public static Func<EditingField, bool> <>9__63_0;
            public static Func<EditingField, int> <>9__63_1;
            public static DeserializeChildPropertiesEventHandler <>9__63_2;

            internal bool <RestoreContinuousInfoFromPrnx>b__63_0(EditingField e) => 
                e.Brick != null;

            internal int <RestoreContinuousInfoFromPrnx>b__63_1(EditingField e) => 
                e.Brick.StoredId;

            internal void <RestoreContinuousInfoFromPrnx>b__63_2(DeserializeChildPropertiesEventArgs e)
            {
                e.DeserializeChildrenVirtually = e.Context.DeserializationPath == "ContinuousExportInfo.Bricks";
            }
        }
    }
}

