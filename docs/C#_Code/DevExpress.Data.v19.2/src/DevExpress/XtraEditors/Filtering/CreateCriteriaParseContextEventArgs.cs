namespace DevExpress.XtraEditors.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    public class CreateCriteriaParseContextEventArgs : EventArgs
    {
        public IDisposable Context { get; set; }
    }
}

