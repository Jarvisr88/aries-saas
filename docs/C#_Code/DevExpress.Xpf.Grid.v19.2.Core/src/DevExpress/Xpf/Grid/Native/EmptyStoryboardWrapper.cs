namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class EmptyStoryboardWrapper : IStoryboardWrapper
    {
        event EventHandler IStoryboardWrapper.Completed
        {
            add
            {
            }
            remove
            {
            }
        }

        public void Begin()
        {
        }

        public IEnumerable<Timeline> GetChildren() => 
            Enumerable.Empty<Timeline>();

        public void Seek(TimeSpan offset)
        {
        }

        public void Stop()
        {
        }
    }
}

