namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class DockControllerBase : IDockController, IActiveItemOwner, IDisposable, ILockOwner
    {
        private readonly DevExpress.Xpf.Docking.DockControllerImpl dockControllerImpl;
        private ActiveItemHelper ActivationHelper;
        private DockLayoutManager containerCore;
        private bool isDisposingCore;

        public DockControllerBase(DockLayoutManager container)
        {
            this.containerCore = container;
            this.ActivationHelper = new ActiveItemHelper(this);
            this.dockControllerImpl = container.DockControllerImpl;
        }

        public void Activate(BaseLayoutItem item)
        {
            this.dockControllerImpl.Activate(item);
        }

        public void Activate(BaseLayoutItem item, bool focus)
        {
            this.dockControllerImpl.Activate(item, focus);
        }

        public DocumentGroup AddDocumentGroup(DockType type) => 
            this.dockControllerImpl.AddDocumentGroup(type);

        public DocumentPanel AddDocumentPanel(DocumentGroup group) => 
            this.dockControllerImpl.AddDocumentPanel(group);

        public DocumentPanel AddDocumentPanel(DocumentGroup group, Uri uri) => 
            this.dockControllerImpl.AddDocumentPanel(group, uri);

        public DocumentPanel AddDocumentPanel(Point floatLocation, Size floatSize) => 
            this.dockControllerImpl.AddDocumentPanel(floatLocation, floatSize);

        public DocumentPanel AddDocumentPanel(Point floatLocation, Size floatSize, Uri uri) => 
            this.dockControllerImpl.AddDocumentPanel(floatLocation, floatSize, uri);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void AddItem(BaseLayoutItem item, BaseLayoutItem target, DockType type)
        {
            this.dockControllerImpl.AddItem(item, target, type);
        }

        public LayoutPanel AddPanel(DockType type) => 
            this.dockControllerImpl.AddPanel(type);

        public LayoutPanel AddPanel(Point floatLocation, Size floatSize) => 
            this.dockControllerImpl.AddPanel(floatLocation, floatSize);

        public bool Close(BaseLayoutItem item) => 
            this.dockControllerImpl.Close(item);

        public bool CloseAllButThis(BaseLayoutItem item) => 
            this.dockControllerImpl.CloseAllButThis(item);

        public T CreateCommand<T>(BaseLayoutItem item) where T: DockControllerCommand, new()
        {
            T local1 = Activator.CreateInstance<T>();
            local1.Controller = this;
            local1.Item = item;
            return local1;
        }

        public bool CreateNewDocumentGroup(DocumentPanel document, Orientation orientation) => 
            this.dockControllerImpl.CreateNewDocumentGroup(document, orientation);

        public bool CreateNewDocumentGroup(LayoutPanel document, Orientation orientation) => 
            this.dockControllerImpl.CreateNewDocumentGroup(document, orientation);

        void ILockOwner.Lock()
        {
            ((ILockOwner) this.dockControllerImpl).Lock();
        }

        void ILockOwner.Unlock()
        {
            ((ILockOwner) this.dockControllerImpl).Unlock();
        }

        public bool Dock(BaseLayoutItem item) => 
            this.dockControllerImpl.Dock(item);

        public bool Dock(BaseLayoutItem item, BaseLayoutItem target, DockType type) => 
            this.dockControllerImpl.Dock(item, target, type);

        public FloatGroup Float(BaseLayoutItem item) => 
            this.dockControllerImpl.Float(item);

        public bool Hide(BaseLayoutItem item) => 
            this.dockControllerImpl.Hide(item);

        public bool Hide(BaseLayoutItem item, AutoHideGroup target) => 
            this.dockControllerImpl.Hide(item, target);

        public bool Hide(BaseLayoutItem item, System.Windows.Controls.Dock dock) => 
            this.dockControllerImpl.Hide(item, dock);

        public bool Insert(LayoutGroup group, BaseLayoutItem item, int index) => 
            this.dockControllerImpl.Insert(group, item, index);

        public bool MoveToDocumentGroup(DocumentPanel document, bool next) => 
            this.dockControllerImpl.MoveToDocumentGroup(document, next);

        public bool MoveToDocumentGroup(LayoutPanel document, bool next) => 
            this.dockControllerImpl.MoveToDocumentGroup(document, next);

        protected void OnDisposing()
        {
            Ref.Dispose<ActiveItemHelper>(ref this.ActivationHelper);
            this.containerCore = null;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveItem(BaseLayoutItem item)
        {
            this.dockControllerImpl.RemoveItem(item);
        }

        public void RemovePanel(LayoutPanel panel)
        {
            this.dockControllerImpl.RemovePanel(panel);
        }

        public bool Rename(BaseLayoutItem item) => 
            this.dockControllerImpl.Rename(item);

        public bool Restore(BaseLayoutItem item) => 
            this.dockControllerImpl.Restore(item);

        void IDisposable.Dispose()
        {
            if (!this.IsDisposing)
            {
                this.isDisposingCore = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        public BaseLayoutItem ActiveItem
        {
            get => 
                this.dockControllerImpl.ActiveItem;
            set => 
                this.dockControllerImpl.ActiveItem = value;
        }

        public DockLayoutManager Container =>
            this.containerCore;

        internal DevExpress.Xpf.Docking.DockControllerImpl DockControllerImpl =>
            this.dockControllerImpl;

        protected bool IsDisposing =>
            this.isDisposingCore;
    }
}

