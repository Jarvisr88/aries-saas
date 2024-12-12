namespace DevExpress.Data.WcfLinq
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class GetSourceEventArgs : EventArgs
    {
        private ServerModeCoreExtender extender;

        public GetSourceEventArgs(ServerModeCoreExtender extender);

        public IQueryable Query { get; set; }

        public string KeyExpression { get; set; }

        public bool AreSourceRowsThreadSafe { get; set; }

        public object Tag { get; set; }

        public ServerModeCoreExtender Extender { get; }
    }
}

