namespace DevExpress.Xpf.Docking
{
    using System;

    internal interface IMergingClient
    {
        void Merge();
        void QueueMerge();
        void QueueUnmerge();
    }
}

