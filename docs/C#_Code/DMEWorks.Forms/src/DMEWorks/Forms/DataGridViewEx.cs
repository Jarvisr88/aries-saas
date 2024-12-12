namespace DMEWorks.Forms
{
    using System;
    using System.Windows.Forms;

    internal class DataGridViewEx : DataGridView
    {
        public DataGridViewEx()
        {
            base.DoubleBuffered = true;
        }
    }
}

