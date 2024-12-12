namespace ActiproSoftware.Win32
{
    using #aXd;
    using System;
    using System.Drawing;

    public class MouseHookEventArgs : EventArgs
    {
        private IntPtr #Rdb;
        private bool #lXd;
        private uint #due;
        private int #eue;
        private uint #fue;
        private Point #2cc;

        internal MouseHookEventArgs(IntPtr handle, int message, uint hitTestCode, Point position, uint mouseData)
        {
            this.#Rdb = handle;
            this.#eue = message;
            this.#due = hitTestCode;
            this.#2cc = position;
            this.#fue = mouseData;
        }

        public IntPtr Handle =>
            this.#Rdb;

        public bool Handled
        {
            get => 
                this.#lXd;
            set => 
                this.#lXd = value;
        }

        public short HitTestCodeHiWord =>
            #Bi.#rbe(this.#due);

        public short HitTestCodeLoWord =>
            #Bi.#sbe(this.#due);

        public int Message =>
            this.#eue;

        public short MouseDataHiWord =>
            #Bi.#rbe(this.#fue);

        public short MouseDataLoWord =>
            #Bi.#sbe(this.#fue);

        public Point Position =>
            this.#2cc;
    }
}

