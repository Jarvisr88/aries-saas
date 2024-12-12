namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.InteropServices;

    public abstract class GridDataBase : EditableDataObject
    {
        private object data;
        protected CellEditorBase editor;
        private bool contentChangedRaised;
        protected internal Locker UpdateValueLocker = new Locker();

        protected GridDataBase()
        {
        }

        protected virtual bool CanRaiseContentChangedWhenDataChanged() => 
            false;

        protected internal virtual void ClearBindingValue()
        {
        }

        protected internal virtual void OnDataChanged()
        {
            this.contentChangedRaised = false;
            this.UpdateValueLocker.DoLockedAction(delegate {
                this.UpdateValue(true);
                if (!this.contentChangedRaised && this.CanRaiseContentChangedWhenDataChanged())
                {
                    this.RaiseContentChanged();
                }
            });
        }

        protected virtual void OnEditorChanged()
        {
        }

        protected override void RaiseContentChanged()
        {
            base.RaiseContentChanged();
            this.contentChangedRaised = true;
        }

        protected internal virtual void UpdateValue(bool forceUpdate = false)
        {
        }

        public object Data
        {
            get => 
                this.data;
            set
            {
                if (this.data != value)
                {
                    this.data = value;
                    this.OnDataChanged();
                    base.RaisePropertyChanged("Data");
                }
            }
        }

        internal CellEditorBase Editor
        {
            get => 
                this.editor;
            set
            {
                if (!ReferenceEquals(this.editor, value))
                {
                    this.editor = value;
                    this.OnEditorChanged();
                }
            }
        }
    }
}

