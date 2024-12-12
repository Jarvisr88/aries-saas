namespace DevExpress.Data
{
    using System;

    public class CurrentRowChangedEventArgs : EventArgs
    {
        private object currentRow;
        private object previousRow;

        public CurrentRowChangedEventArgs(object previousRow, object currentRow);

        public object CurrentRow { get; }

        public object PreviousRow { get; }
    }
}

