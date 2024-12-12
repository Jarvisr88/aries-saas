namespace DevExpress.Data.Linq
{
    using System;

    public class LinqServerModeInconsistencyDetectedEventArgs : EventArgs
    {
        private bool _handled;

        public bool Handled { get; set; }
    }
}

