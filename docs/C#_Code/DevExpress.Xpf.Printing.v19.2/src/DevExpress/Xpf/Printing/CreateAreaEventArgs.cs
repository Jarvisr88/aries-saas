namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Runtime.CompilerServices;

    public class CreateAreaEventArgs : EventArgs
    {
        public CreateAreaEventArgs(int detailIndex)
        {
            this.DetailIndex = detailIndex;
        }

        public object Data { get; set; }

        public int DetailIndex { get; private set; }
    }
}

