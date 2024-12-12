namespace DevExpress.Office.Utils
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class MessageQueue : Control
    {
        public MessageQueue()
        {
            this.CreateHandle();
        }

        public void EnsureHandleCreated()
        {
            if (!base.IsHandleCreated)
            {
                this.CreateHandle();
            }
        }
    }
}

