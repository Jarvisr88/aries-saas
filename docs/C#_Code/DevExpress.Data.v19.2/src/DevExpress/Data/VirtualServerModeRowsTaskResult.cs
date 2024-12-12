namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class VirtualServerModeRowsTaskResult
    {
        public VirtualServerModeRowsTaskResult();
        public VirtualServerModeRowsTaskResult(ICollection rows, bool moreRowsAvailable = false, object userData = null);

        public ICollection Rows { get; set; }

        public bool MoreRowsAvailable { get; set; }

        public object UserData { get; set; }
    }
}

