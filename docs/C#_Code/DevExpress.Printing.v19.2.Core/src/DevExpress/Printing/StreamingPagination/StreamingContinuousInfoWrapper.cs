namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;

    internal class StreamingContinuousInfoWrapper : ContinuousExportInfo, IStreamingContinuousInfo
    {
        private object syncObject;
        private ContinuousExportInfo info;

        public StreamingContinuousInfoWrapper(ContinuousExportInfo info) : base(info)
        {
            this.syncObject = new object();
            this.info = info;
        }

        public void Add(Brick brick, RectangleDF rect)
        {
            throw new NotSupportedException();
        }

        public void EndCollecting()
        {
            throw new NotSupportedException();
        }

        public void FixChunk()
        {
            object syncObject = this.SyncObject;
            lock (syncObject)
            {
                base.Bricks = (this.info == null) ? ((ICollection) new object[0]) : this.info.Bricks;
                this.info = null;
            }
        }

        public void StartCollecting()
        {
            throw new NotSupportedException();
        }

        public object SyncObject =>
            this.syncObject;
    }
}

