namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal interface IStoryboardWrapper
    {
        event EventHandler Completed;

        void Begin();
        IEnumerable<Timeline> GetChildren();
        void Seek(TimeSpan offset);
        void Stop();
    }
}

