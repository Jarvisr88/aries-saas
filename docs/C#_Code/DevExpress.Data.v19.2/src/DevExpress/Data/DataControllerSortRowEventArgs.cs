namespace DevExpress.Data
{
    using System;

    public class DataControllerSortRowEventArgs : EventArgs
    {
        private int listSourceRow1;
        private int listSourceRow2;
        private int result;
        private bool handled;

        public DataControllerSortRowEventArgs();
        public DataControllerSortRowEventArgs(int listSourceRow1, int listSourceRow2);
        internal void Setup(int listSourceRow1, int listSourceRow2);

        public int ListSourceRow1 { get; }

        public int ListSourceRow2 { get; }

        public int Result { get; set; }

        public bool Handled { get; set; }
    }
}

