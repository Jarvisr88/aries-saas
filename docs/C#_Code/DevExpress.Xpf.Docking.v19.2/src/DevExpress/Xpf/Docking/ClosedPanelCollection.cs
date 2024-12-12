namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ClosedPanelCollection : BaseLockableCollection<LayoutPanel>, IDisposable
    {
        private bool isDisposing;
        private DockLayoutManager Owner;

        public ClosedPanelCollection(DockLayoutManager owner)
        {
            this.Owner = owner;
        }

        public void AddRange(LayoutPanel[] panels)
        {
            Array.ForEach<LayoutPanel>(panels, new Action<LayoutPanel>(this.Add));
        }

        protected override void ClearItems()
        {
            LayoutPanel[] array = new LayoutPanel[base.Items.Count];
            base.Items.CopyTo(array, 0);
            base.ClearItems();
            foreach (LayoutPanel panel in array)
            {
                this.RemoveItemCore(panel);
            }
        }

        public void Dispose()
        {
            if (!this.isDisposing)
            {
                this.isDisposing = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        public override void EndUpdate()
        {
            base.EndUpdate();
            this.ToList<LayoutPanel>().ForEach(x => DockLayoutManager.AddLogicalChild(this.Owner, x));
        }

        protected override void InsertItem(int index, LayoutPanel item)
        {
            if (this.Owner != null)
            {
                item.Manager ??= this.Owner;
                DockLayoutManager.AddLogicalChild(this.Owner, item);
                item.DockLayoutManagerCore = this.Owner;
            }
            base.InsertItem(index, item);
            item.SetClosed(true);
        }

        protected virtual void OnDisposing()
        {
            this.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            LayoutPanel item = ((index < 0) || (index >= base.Count)) ? null : base[index];
            base.RemoveItem(index);
            this.RemoveItemCore(item);
        }

        private void RemoveItemCore(BaseLayoutItem item)
        {
            if (item != null)
            {
                item.SetClosed(false);
                if (!this.Owner.OptimizedLogicalTree)
                {
                    DockLayoutManager.RemoveLogicalChild(this.Owner, item);
                }
                item.DockLayoutManagerCore = null;
            }
        }

        protected override void SetItem(int index, LayoutPanel item)
        {
            item.SetClosed(true);
            base.SetItem(index, item);
        }

        public LayoutPanel[] ToArray()
        {
            LayoutPanel[] array = new LayoutPanel[base.Count];
            base.Items.CopyTo(array, 0);
            return array;
        }

        public LayoutPanel this[string name]
        {
            get
            {
                LayoutPanel panel2;
                using (IEnumerator<LayoutPanel> enumerator = base.Items.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            LayoutPanel current = enumerator.Current;
                            if (current.Name != name)
                            {
                                continue;
                            }
                            panel2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
                return panel2;
            }
        }
    }
}

