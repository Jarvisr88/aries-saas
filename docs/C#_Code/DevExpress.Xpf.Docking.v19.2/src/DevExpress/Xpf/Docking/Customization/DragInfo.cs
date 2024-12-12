namespace DevExpress.Xpf.Docking.Customization
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DragInfo
    {
        private readonly int _hash;

        public DragInfo(BaseLayoutItem item, BaseLayoutItem target, DropType type)
        {
            this.Item = item;
            this.Target = target;
            this.Type = type;
            this._hash = HashCodeHelper.CalculateGeneric<DropType, BaseLayoutItem, BaseLayoutItem>(type, item, target);
        }

        public override bool Equals(object obj)
        {
            DragInfo info = obj as DragInfo;
            return ((info != null) && (EqualityComparer<BaseLayoutItem>.Default.Equals(this.Item, info.Item) && (EqualityComparer<BaseLayoutItem>.Default.Equals(this.Target, info.Target) && (this.Type == info.Type))));
        }

        public override int GetHashCode() => 
            this._hash;

        public static bool operator ==(DragInfo left, DragInfo right) => 
            Equals(left, right);

        public static bool operator !=(DragInfo left, DragInfo right) => 
            !Equals(left, right);

        public BaseLayoutItem Item { get; private set; }

        public BaseLayoutItem Target { get; private set; }

        public DropType Type { get; private set; }
    }
}

