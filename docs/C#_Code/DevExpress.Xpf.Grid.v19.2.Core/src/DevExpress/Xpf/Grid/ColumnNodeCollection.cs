namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class ColumnNodeCollection : ObservableCollectionCore<ColumnNode>
    {
        private IColumnNodeOwner ownerCore;

        public virtual bool HasExplicitIndexes()
        {
            Func<IColumnNodeOwner, BandsMoverHierarchy> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<IColumnNodeOwner, BandsMoverHierarchy> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = o => o.GetRoot();
            }
            IModelItemCollection source = this.Owner.With<IColumnNodeOwner, BandsMoverHierarchy>(evaluator).With<BandsMoverHierarchy, IModelItemCollection>(x => x.FindCollectionModel(this));
            if (source == null)
            {
                return true;
            }
            Func<IModelItem, bool> predicate = <>c.<>9__9_2;
            if (<>c.<>9__9_2 == null)
            {
                Func<IModelItem, bool> local2 = <>c.<>9__9_2;
                predicate = <>c.<>9__9_2 = x => x.Properties[BaseColumn.VisibleIndexProperty.Name].IsSet;
            }
            return source.Any<IModelItem>(predicate);
        }

        private void InvalidateCollectionIndexes()
        {
            for (int i = 0; i < base.Count; i++)
            {
                base[i].CollectionIndex = i;
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (!base.IsLockUpdate && ((this.Owner == null) || !this.Owner.IsInitializing))
            {
                this.UpdateItems(e);
            }
        }

        private void OnOwnerChanged()
        {
            this.SetNodeOwner(this, this.Owner);
        }

        private void SetNodeOwner(IList nodes, IColumnNodeOwner owner)
        {
            if (nodes != null)
            {
                foreach (ColumnNode node in nodes)
                {
                    node.Owner = owner;
                }
            }
        }

        protected virtual void UpdateItems(NotifyCollectionChangedEventArgs e)
        {
            IList nodes = (e.Action == NotifyCollectionChangedAction.Reset) ? this : e.NewItems;
            this.SetNodeOwner(nodes, this.Owner);
            this.SetNodeOwner(e.OldItems, null);
            this.InvalidateCollectionIndexes();
            this.Owner.Do<IColumnNodeOwner>(x => x.OnNodeCollectionChanged(this, e));
        }

        public IColumnNodeOwner Owner
        {
            get => 
                this.ownerCore;
            set
            {
                if (!ReferenceEquals(this.ownerCore, value))
                {
                    this.ownerCore = value;
                    this.OnOwnerChanged();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnNodeCollection.<>c <>9 = new ColumnNodeCollection.<>c();
            public static Func<IColumnNodeOwner, BandsMoverHierarchy> <>9__9_0;
            public static Func<IModelItem, bool> <>9__9_2;

            internal BandsMoverHierarchy <HasExplicitIndexes>b__9_0(IColumnNodeOwner o) => 
                o.GetRoot();

            internal bool <HasExplicitIndexes>b__9_2(IModelItem x) => 
                x.Properties[BaseColumn.VisibleIndexProperty.Name].IsSet;
        }
    }
}

