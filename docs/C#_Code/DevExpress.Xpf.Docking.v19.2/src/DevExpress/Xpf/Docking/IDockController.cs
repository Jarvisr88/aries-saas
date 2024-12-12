namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public interface IDockController : IActiveItemOwner, IDisposable
    {
        DocumentGroup AddDocumentGroup(DockType type);
        DocumentPanel AddDocumentPanel(DocumentGroup group);
        DocumentPanel AddDocumentPanel(DocumentGroup group, Uri uri);
        DocumentPanel AddDocumentPanel(Point floatLocation, Size floatSize);
        DocumentPanel AddDocumentPanel(Point floatLocation, Size floatSize, Uri uri);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        void AddItem(BaseLayoutItem item, BaseLayoutItem target, DockType type);
        LayoutPanel AddPanel(DockType type);
        LayoutPanel AddPanel(Point floatLocation, Size floatSize);
        bool Close(BaseLayoutItem item);
        bool CloseAllButThis(BaseLayoutItem item);
        T CreateCommand<T>(BaseLayoutItem item) where T: DockControllerCommand, new();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        bool CreateNewDocumentGroup(DocumentPanel item, Orientation orientation);
        bool CreateNewDocumentGroup(LayoutPanel item, Orientation orientation);
        bool Dock(BaseLayoutItem item);
        bool Dock(BaseLayoutItem item, BaseLayoutItem target, DockType type);
        FloatGroup Float(BaseLayoutItem item);
        bool Hide(BaseLayoutItem item);
        bool Hide(BaseLayoutItem item, AutoHideGroup target);
        bool Hide(BaseLayoutItem item, System.Windows.Controls.Dock dock);
        bool Insert(LayoutGroup group, BaseLayoutItem item, int index);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        bool MoveToDocumentGroup(DocumentPanel item, bool next);
        bool MoveToDocumentGroup(LayoutPanel item, bool next);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        void RemoveItem(BaseLayoutItem item);
        void RemovePanel(LayoutPanel panel);
        bool Rename(BaseLayoutItem item);
        bool Restore(BaseLayoutItem item);
    }
}

