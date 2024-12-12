namespace DevExpress.Utils
{
    using DevExpress.Utils.KeyboardHandler;
    using System;
    using System.Windows.Forms;

    public abstract class MouseHandlerState
    {
        private bool isFinished;
        private DevExpress.Utils.MouseHandler mouseHandler;

        protected MouseHandlerState(DevExpress.Utils.MouseHandler mouseHandler)
        {
            this.mouseHandler = mouseHandler;
        }

        public virtual void Finish()
        {
            this.isFinished = true;
        }

        public virtual void OnCancelState()
        {
        }

        public virtual void OnDragDrop(DragEventArgs e)
        {
        }

        public virtual void OnDragEnter(DragEventArgs e)
        {
        }

        public virtual void OnDragLeave()
        {
        }

        public virtual void OnDragOver(DragEventArgs e)
        {
        }

        public virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
        }

        public virtual void OnKeyStateChanged(KeyState keyState)
        {
        }

        public virtual void OnLongMouseDown()
        {
        }

        public virtual void OnMouseCaptureChanged()
        {
        }

        public virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        public virtual void OnMouseTripleClick(MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
        }

        public virtual bool OnPopupMenu(MouseEventArgs e) => 
            false;

        public virtual bool OnPopupMenuShowing() => 
            true;

        public virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e)
        {
        }

        public virtual void Start()
        {
            if (this.StopClickTimerOnStart)
            {
                this.MouseHandler.StopClickTimer();
            }
        }

        public DevExpress.Utils.MouseHandler MouseHandler =>
            this.mouseHandler;

        public virtual bool AutoScrollEnabled =>
            true;

        public bool IsFinished =>
            this.isFinished;

        public virtual bool CanShowToolTip =>
            false;

        public virtual bool StopClickTimerOnStart =>
            true;
    }
}

