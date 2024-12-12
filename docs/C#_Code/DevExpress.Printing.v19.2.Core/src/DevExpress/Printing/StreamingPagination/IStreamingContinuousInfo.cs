namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;

    public interface IStreamingContinuousInfo
    {
        void Add(Brick brick, RectangleDF rect);
        void EndCollecting();
        void FixChunk();
        void StartCollecting();

        object SyncObject { get; }

        ICollection Bricks { get; }
    }
}

