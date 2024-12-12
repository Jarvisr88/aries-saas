namespace ActiproSoftware.WinUICore
{
    using #H;
    using ActiproSoftware.ComponentModel;
    using System;
    using System.Collections;
    using System.ComponentModel;

    [TypeConverter(typeof(UIElementCollectionConverter))]
    public class UIElementCollection : LogicalTreeNodeCollection, IList, ICollection, IEnumerable
    {
        public UIElementCollection(IUIElement owner) : base(owner)
        {
        }

        public bool Contains(IUIElement value) => 
            this.InnerList.Contains(value);

        protected override void Dispose(bool disposing)
        {
            this.Dispose(disposing);
            if (disposing)
            {
                base.DisposeAllChildObjects();
            }
        }

        public int IndexOf(IUIElement value) => 
            this.InnerList.IndexOf(value);

        protected override void OnObjectAdded(int index, ILogicalTreeNode value)
        {
            this.OnObjectAdded(index, value);
            IUIElement ownerCore = base.OwnerCore as IUIElement;
            if (ownerCore != null)
            {
                ownerCore.NotifyChildDesiredSizeChanged();
            }
        }

        protected override void OnObjectRemoved(int index, ILogicalTreeNode value)
        {
            this.OnObjectRemoved(index, value);
            IUIElement ownerCore = base.OwnerCore as IUIElement;
            if (ownerCore != null)
            {
                ownerCore.NotifyChildDesiredSizeChanged();
            }
        }

        protected override void OnObjectRemoving(int index, ILogicalTreeNode value)
        {
            if (this.AutoAssignParent && (value is IInputElement))
            {
                ((IInputElement) value).ReleaseMouseCapture();
            }
            base.OnObjectRemoving(index, value);
        }

        protected internal virtual string PluralElementName =>
            this.SingularElementName + #G.#eg(0x5d2);

        protected internal virtual string SingularElementName =>
            #G.#eg(0x5d7);
    }
}

