namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutItemDragAndDropInsertionPointIndicator : ControlBase
    {
        private bool _IsActive;

        public LayoutItemDragAndDropInsertionPointIndicator()
        {
            base.DefaultStyleKey = typeof(LayoutItemDragAndDropInsertionPointIndicator);
        }

        public LayoutItemDragAndDropInsertionPointIndicator(LayoutItemInsertionPoint insertionPoint, LayoutItemInsertionKind insertionKind)
        {
            base.DefaultStyleKey = typeof(LayoutItemDragAndDropInsertionPointIndicator);
            this.InsertionPoint = insertionPoint;
            this.InsertionKind = insertionKind;
        }

        protected virtual void OnIsActiveChanged()
        {
            this.UpdateState(true);
        }

        protected override void UpdateState(bool useTransitions)
        {
            this.GoToState(this.IsActive ? "MouseOver" : "Normal", useTransitions);
        }

        public LayoutItemInsertionKind InsertionKind { get; private set; }

        public LayoutItemInsertionPoint InsertionPoint { get; private set; }

        public bool IsActive
        {
            get => 
                this._IsActive;
            set
            {
                if (this._IsActive != value)
                {
                    this._IsActive = value;
                    this.OnIsActiveChanged();
                }
            }
        }
    }
}

