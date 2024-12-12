namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FormatConditionCollection : ObservableCollectionCore<FormatConditionBase>
    {
        private ConditionalFormatSummaryInfo[] summaries;
        private Dictionary<string, IList<FormatConditionBase>> cache = new Dictionary<string, IList<FormatConditionBase>>();
        private bool? hasDataUpdateFormatCondition;
        private bool? hasNonEmptyTextDecorations;

        public FormatConditionCollection(IFormatConditionCollectionOwner owner)
        {
            this.Owner = owner;
        }

        protected override void ClearItems()
        {
            foreach (FormatConditionBase base2 in this)
            {
                this.NullOwner(base2);
            }
            base.ClearItems();
        }

        private ConditionalFormatSummaryInfo[] CollectSummaries()
        {
            Func<FormatConditionBase, bool> predicate = <>c.<>9__22_0;
            if (<>c.<>9__22_0 == null)
            {
                Func<FormatConditionBase, bool> local1 = <>c.<>9__22_0;
                predicate = <>c.<>9__22_0 = x => x.IsValid;
            }
            Func<FormatConditionBase, IEnumerable<ConditionalFormatSummaryInfo>> selector = <>c.<>9__22_1;
            if (<>c.<>9__22_1 == null)
            {
                Func<FormatConditionBase, IEnumerable<ConditionalFormatSummaryInfo>> local2 = <>c.<>9__22_1;
                selector = <>c.<>9__22_1 = x => x.CreateSummaryItems();
            }
            return this.Where<FormatConditionBase>(predicate).SelectMany<FormatConditionBase, ConditionalFormatSummaryInfo>(selector).Distinct<ConditionalFormatSummaryInfo>().ToArray<ConditionalFormatSummaryInfo>();
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<FormatConditionBaseInfo> GetInfoByFieldName(string fieldName)
        {
            IList<FormatConditionBase> source = this[fieldName];
            if (source == null)
            {
                return new List<FormatConditionBaseInfo>();
            }
            Func<FormatConditionBase, FormatConditionBaseInfo> selector = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<FormatConditionBase, FormatConditionBaseInfo> local1 = <>c.<>9__11_0;
                selector = <>c.<>9__11_0 = x => x.Info;
            }
            return source.Select<FormatConditionBase, FormatConditionBaseInfo>(selector).ToList<FormatConditionBaseInfo>();
        }

        private IList<FormatConditionBase> GetItemsByFieldName(string fieldName)
        {
            if (fieldName == string.Empty)
            {
                fieldName = null;
            }
            FormatConditionBase[] baseArray = (from x in this
                where (x.GetApplyToFieldName() == fieldName) && x.IsValid
                select x).ToArray<FormatConditionBase>();
            return ((baseArray.Length != 0) ? ((IList<FormatConditionBase>) baseArray) : null);
        }

        internal IEnumerable<IColumnInfo> GetUnboundColumns()
        {
            Func<FormatConditionBase, bool> predicate = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                Func<FormatConditionBase, bool> local1 = <>c.<>9__23_0;
                predicate = <>c.<>9__23_0 = x => x.IsValid;
            }
            Func<FormatConditionBase, IEnumerable<IColumnInfo>> selector = <>c.<>9__23_1;
            if (<>c.<>9__23_1 == null)
            {
                Func<FormatConditionBase, IEnumerable<IColumnInfo>> local2 = <>c.<>9__23_1;
                selector = <>c.<>9__23_1 = x => x.GetUnboundColumnInfo();
            }
            return this.Where<FormatConditionBase>(predicate).SelectMany<FormatConditionBase, IColumnInfo>(selector);
        }

        internal bool HasDataUpdateFormatConditions()
        {
            if (this.hasDataUpdateFormatCondition == null)
            {
                Func<FormatConditionBase, bool> predicate = <>c.<>9__26_0;
                if (<>c.<>9__26_0 == null)
                {
                    Func<FormatConditionBase, bool> local1 = <>c.<>9__26_0;
                    predicate = <>c.<>9__26_0 = x => x.Info.IsAnimationEnabled(false);
                }
                this.hasDataUpdateFormatCondition = new bool?(this.Any<FormatConditionBase>(predicate));
            }
            return this.hasDataUpdateFormatCondition.Value;
        }

        internal bool HasNonEmptyTextDecorations()
        {
            if (this.hasNonEmptyTextDecorations == null)
            {
                Func<FormatConditionBase, bool> predicate = <>c.<>9__28_0;
                if (<>c.<>9__28_0 == null)
                {
                    Func<FormatConditionBase, bool> local1 = <>c.<>9__28_0;
                    predicate = <>c.<>9__28_0 = x => x.Info.HasNonEmptyTextDecorations();
                }
                this.hasNonEmptyTextDecorations = new bool?(this.Any<FormatConditionBase>(predicate));
            }
            return this.hasNonEmptyTextDecorations.Value;
        }

        internal bool HasRowConditions()
        {
            Func<FormatConditionBase, bool> predicate = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<FormatConditionBase, bool> local1 = <>c.<>9__24_0;
                predicate = <>c.<>9__24_0 = x => x.IsValid;
            }
            Func<FormatConditionBase, bool> func2 = <>c.<>9__24_1;
            if (<>c.<>9__24_1 == null)
            {
                Func<FormatConditionBase, bool> local2 = <>c.<>9__24_1;
                func2 = <>c.<>9__24_1 = x => x.ApplyToRow;
            }
            return this.Where<FormatConditionBase>(predicate).Any<FormatConditionBase>(func2);
        }

        protected override void InsertItem(int index, FormatConditionBase item)
        {
            base.InsertItem(index, item);
            this.SetOwner(item);
        }

        private void NullOwner(FormatConditionBase item)
        {
            item.Owner = null;
        }

        private void OnChanged(FormatConditionChangeType changeType)
        {
            this.cache.Clear();
            this.hasDataUpdateFormatCondition = null;
            this.hasNonEmptyTextDecorations = null;
            if (changeType.HasFlag(FormatConditionChangeType.Summary))
            {
                this.summaries = null;
            }
            this.Owner.OnFormatConditionCollectionChanged(changeType);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (!base.IsLockUpdate)
            {
                this.OnChanged(FormatConditionChangeType.All);
                this.Owner.SyncFormatConditionCollectionWithDetails(e);
            }
        }

        internal void OnItemPropertyChanged(FormatConditionBase item, DependencyPropertyChangedEventArgs e, FormatConditionChangeType changeType)
        {
            if (!base.IsLockUpdate)
            {
                this.OnChanged(changeType);
                this.Owner.SyncFormatConditionPropertyWithDetails(item, e);
            }
        }

        protected override void RemoveItem(int index)
        {
            this.NullOwner(base[index]);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, FormatConditionBase item)
        {
            this.NullOwner(base[index]);
            base.SetItem(index, item);
            this.SetOwner(item);
        }

        private void SetOwner(FormatConditionBase item)
        {
            if ((item.Owner != null) && !ReferenceEquals(item.Owner, this))
            {
                throw new InvalidOperationException(typeof(FormatCondition).Name + " object cannot be added to more than one " + typeof(FormatConditionCollection).Name + ".");
            }
            item.Owner = this;
        }

        internal ConditionalFormatSummaryInfo[] Summaries
        {
            get
            {
                ConditionalFormatSummaryInfo[] summaries = this.summaries;
                if (this.summaries == null)
                {
                    ConditionalFormatSummaryInfo[] local1 = this.summaries;
                    summaries = this.summaries = this.CollectSummaries();
                }
                return summaries;
            }
        }

        public IFormatConditionCollectionOwner Owner { get; private set; }

        internal IList<FormatConditionBase> this[string fieldName] =>
            (base.Count != 0) ? this.cache.GetOrAdd<string, IList<FormatConditionBase>>(fieldName, () => this.GetItemsByFieldName(fieldName)) : null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatConditionCollection.<>c <>9 = new FormatConditionCollection.<>c();
            public static Func<FormatConditionBase, FormatConditionBaseInfo> <>9__11_0;
            public static Func<FormatConditionBase, bool> <>9__22_0;
            public static Func<FormatConditionBase, IEnumerable<ConditionalFormatSummaryInfo>> <>9__22_1;
            public static Func<FormatConditionBase, bool> <>9__23_0;
            public static Func<FormatConditionBase, IEnumerable<IColumnInfo>> <>9__23_1;
            public static Func<FormatConditionBase, bool> <>9__24_0;
            public static Func<FormatConditionBase, bool> <>9__24_1;
            public static Func<FormatConditionBase, bool> <>9__26_0;
            public static Func<FormatConditionBase, bool> <>9__28_0;

            internal bool <CollectSummaries>b__22_0(FormatConditionBase x) => 
                x.IsValid;

            internal IEnumerable<ConditionalFormatSummaryInfo> <CollectSummaries>b__22_1(FormatConditionBase x) => 
                x.CreateSummaryItems();

            internal FormatConditionBaseInfo <GetInfoByFieldName>b__11_0(FormatConditionBase x) => 
                x.Info;

            internal bool <GetUnboundColumns>b__23_0(FormatConditionBase x) => 
                x.IsValid;

            internal IEnumerable<IColumnInfo> <GetUnboundColumns>b__23_1(FormatConditionBase x) => 
                x.GetUnboundColumnInfo();

            internal bool <HasDataUpdateFormatConditions>b__26_0(FormatConditionBase x) => 
                x.Info.IsAnimationEnabled(false);

            internal bool <HasNonEmptyTextDecorations>b__28_0(FormatConditionBase x) => 
                x.Info.HasNonEmptyTextDecorations();

            internal bool <HasRowConditions>b__24_0(FormatConditionBase x) => 
                x.IsValid;

            internal bool <HasRowConditions>b__24_1(FormatConditionBase x) => 
                x.ApplyToRow;
        }
    }
}

