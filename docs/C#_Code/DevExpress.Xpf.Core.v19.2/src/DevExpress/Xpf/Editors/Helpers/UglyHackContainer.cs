namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;

    internal class UglyHackContainer
    {
        public DevExpress.Xpf.Core.Locker Locker = new DevExpress.Xpf.Core.Locker();
        private readonly HashSet<int> allowedIndices;
        private readonly Func<int, object> handler;

        public UglyHackContainer(IEnumerable<int> allowedIndices, Func<int, object> handler)
        {
            this.handler = handler;
            this.allowedIndices = new HashSet<int>(allowedIndices);
        }

        public object GetItemAt(int index) => 
            !this.allowedIndices.Contains(index) ? null : this.handler(index);
    }
}

