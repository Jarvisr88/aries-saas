namespace DevExpress.Office.Drawing
{
    using System;

    public class InvalidateProxy : ISupportsInvalidate
    {
        private ISupportsInvalidate target;
        private ISupportsInvalidateNotify notifyTarget;

        public void Invalidate()
        {
            if (this.target != null)
            {
                this.target.Invalidate();
                if (this.notifyTarget != null)
                {
                    this.notifyTarget.InvalidateNotify();
                }
            }
        }

        public ISupportsInvalidate Target
        {
            get => 
                this.target;
            set => 
                this.target = value;
        }

        public ISupportsInvalidateNotify NotifyTarget
        {
            get => 
                this.notifyTarget;
            set => 
                this.notifyTarget = value;
        }
    }
}

