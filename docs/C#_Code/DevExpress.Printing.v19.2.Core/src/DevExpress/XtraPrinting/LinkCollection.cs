namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;

    [ListBindable(BindableSupport.No)]
    public class LinkCollection : CollectionBase
    {
        protected PrintingSystemBase ps;

        internal LinkCollection()
        {
        }

        internal LinkCollection(PrintingSystemBase ps)
        {
            this.ps = ps;
        }

        public int Add(LinkBase val) => 
            base.List.Add(val);

        public void AddRange(object[] items)
        {
            foreach (LinkBase base2 in items)
            {
                this.Add(base2);
            }
        }

        public bool Contains(LinkBase val) => 
            base.List.Contains(val);

        public void CopyFrom(ArrayList array)
        {
            base.Clear();
            this.AddRange(array.ToArray());
        }

        public int IndexOf(LinkBase val) => 
            base.List.IndexOf(val);

        public void Insert(int index, LinkBase val)
        {
            base.List.Insert(index, val);
        }

        private void link_Disposed(object sender, EventArgs e)
        {
            this.Remove((LinkBase) sender);
        }

        protected override void OnClear()
        {
            base.OnClear();
            foreach (LinkBase base2 in this)
            {
                base2.Disposed -= new EventHandler(this.link_Disposed);
            }
        }

        protected override void OnInsertComplete(int index, object val)
        {
            base.OnInsertComplete(index, val);
            ((LinkBase) val).PrintingSystemBase = this.ps;
            ((LinkBase) val).Disposed += new EventHandler(this.link_Disposed);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            ((LinkBase) value).Disposed -= new EventHandler(this.link_Disposed);
        }

        public virtual void Remove(LinkBase val)
        {
            base.List.Remove(val);
        }

        [Description("Provides indexed access to individual items in the collection.")]
        public LinkBase this[int index]
        {
            get => 
                (LinkBase) base.InnerList[index];
            set => 
                base.InnerList[index] = value;
        }
    }
}

