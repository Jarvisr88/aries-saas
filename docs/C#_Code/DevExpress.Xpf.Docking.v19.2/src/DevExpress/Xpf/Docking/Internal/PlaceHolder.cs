namespace DevExpress.Xpf.Docking.Internal
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class PlaceHolder : ISerializableItem, ISupportInitialize
    {
        private string parentName;
        private string ownerName;
        private int index;
        private int _hash;

        internal PlaceHolder()
        {
        }

        public PlaceHolder(BaseLayoutItem owner, LayoutGroup parent)
        {
            this.Owner = owner;
            this.Parent = parent;
            this.DockState = owner.IsFloating ? PlaceHolderState.Floating : (owner.IsAutoHidden ? PlaceHolderState.AutoHidden : PlaceHolderState.Docked);
            this.InvalidateHash();
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            this.InvalidateHash();
        }

        public override bool Equals(object obj)
        {
            PlaceHolder holder = obj as PlaceHolder;
            return ((holder != null) && (EqualityComparer<LayoutGroup>.Default.Equals(this.Parent, holder.Parent) && (EqualityComparer<BaseLayoutItem>.Default.Equals(this.Owner, holder.Owner) && (this.DockState == holder.DockState))));
        }

        public int GetDockIndex()
        {
            int index = -1;
            if (this.Parent != null)
            {
                index = this.Parent.PlaceHolderHelper.IndexOf(this);
            }
            return ((index == -1) ? this.GetRestoredIndex() : index);
        }

        public override int GetHashCode() => 
            this._hash;

        private int GetRestoredIndex() => 
            this.index;

        private void InvalidateHash()
        {
            this._hash = HashCodeHelper.CalculateGeneric<PlaceHolderState, LayoutGroup, BaseLayoutItem>(this.DockState, this.Parent, this.Owner);
        }

        public static bool operator ==(PlaceHolder left, PlaceHolder right) => 
            Equals(left, right);

        public static bool operator !=(PlaceHolder left, PlaceHolder right) => 
            !Equals(left, right);

        public LayoutGroup Parent { get; internal set; }

        public BaseLayoutItem Owner { get; internal set; }

        [XtraSerializableProperty]
        public PlaceHolderState DockState { get; set; }

        [XtraSerializableProperty]
        public string ParentName
        {
            get => 
                (this.Parent != null) ? this.Parent.Name : this.parentName;
            set => 
                this.parentName = value;
        }

        [XtraSerializableProperty]
        public string OwnerName
        {
            get => 
                (this.Owner != null) ? this.Owner.Name : this.ownerName;
            set => 
                this.ownerName = value;
        }

        [XtraSerializableProperty]
        public int Index
        {
            get => 
                (this.Parent != null) ? this.Parent.PlaceHolderHelper.IndexOf(this) : this.GetRestoredIndex();
            set => 
                this.index = value;
        }

        public bool IsFloating =>
            this.DockState == PlaceHolderState.Floating;

        public bool IsAutoHidden =>
            this.DockState == PlaceHolderState.AutoHidden;

        public bool IsDocked =>
            this.DockState == PlaceHolderState.Docked;

        string ISerializableItem.Name { get; set; }

        string ISerializableItem.TypeName =>
            typeof(PlaceHolder).Name;

        string ISerializableItem.ParentName { get; set; }

        string ISerializableItem.ParentCollectionName { get; set; }
    }
}

