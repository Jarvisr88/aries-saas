namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Shell;

    public class NativeJumpList : ObservableCollection<JumpItem>, INativeJumpList, IList<JumpItem>, ICollection<JumpItem>, IEnumerable<JumpItem>, IEnumerable, INotifyCollectionChanged
    {
        private Func<JumpItem, string> getIdFunc;
        private JumpList jumpList = new JumpList();
        private Dictionary<string, JumpItem> dictionary = new Dictionary<string, JumpItem>();
        private IDisposable jumpListBinding;

        public NativeJumpList(Func<JumpItem, string> getIdFunc)
        {
            this.getIdFunc = getIdFunc;
            this.SetJumpListBinding();
        }

        public void AddToRecentCategory(string path)
        {
            this.AddToRecentCategoryOverride(path);
        }

        protected virtual void AddToRecentCategoryOverride(string path)
        {
            JumpList.AddToRecentCategory(path);
        }

        public IEnumerable<Tuple<JumpItem, JumpItemRejectionReason>> Apply()
        {
            this.jumpListBinding.Dispose();
            IEnumerable<Tuple<JumpItem, JumpItemRejectionReason>> enumerable = this.ApplyOverride(this.jumpList);
            this.SetJumpListBinding();
            return enumerable;
        }

        protected virtual IEnumerable<Tuple<JumpItem, JumpItemRejectionReason>> ApplyOverride(JumpList jumpList)
        {
            IEnumerable<Tuple<JumpItem, JumpItemRejectionReason>> rejectedItems = null;
            EventHandler<JumpItemsRejectedEventArgs> handler = delegate (object s, JumpItemsRejectedEventArgs e) {
                Func<JumpItem, JumpItemRejectionReason, Tuple<JumpItem, JumpItemRejectionReason>> resultSelector = <>c.<>9__14_1;
                if (<>c.<>9__14_1 == null)
                {
                    Func<JumpItem, JumpItemRejectionReason, Tuple<JumpItem, JumpItemRejectionReason>> local1 = <>c.<>9__14_1;
                    resultSelector = <>c.<>9__14_1 = (i, r) => new Tuple<JumpItem, JumpItemRejectionReason>(i, r);
                }
                rejectedItems = e.RejectedItems.Zip<JumpItem, JumpItemRejectionReason, Tuple<JumpItem, JumpItemRejectionReason>>(e.RejectionReasons, resultSelector).ToArray<Tuple<JumpItem, JumpItemRejectionReason>>();
            };
            jumpList.JumpItemsRejected += handler;
            jumpList.Apply();
            jumpList.JumpItemsRejected -= handler;
            return rejectedItems;
        }

        protected override void ClearItems()
        {
            this.dictionary.Clear();
            base.ClearItems();
        }

        public JumpItem Find(string id)
        {
            JumpItem item;
            return (this.dictionary.TryGetValue(id, out item) ? item : null);
        }

        protected override void InsertItem(int index, JumpItem item)
        {
            string key = this.getIdFunc(item);
            if (key != null)
            {
                this.dictionary.Add(key, item);
            }
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            string key = this.getIdFunc(base[index]);
            if (key != null)
            {
                this.dictionary.Remove(key);
            }
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, JumpItem item)
        {
            string key = this.getIdFunc(base[index]);
            if (key != null)
            {
                this.dictionary.Remove(key);
            }
            key = this.getIdFunc(item);
            if (key != null)
            {
                this.dictionary.Add(key, item);
            }
            base.SetItem(index, item);
        }

        private void SetJumpListBinding()
        {
            Func<JumpItem, JumpItem> itemConverter = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                Func<JumpItem, JumpItem> local1 = <>c.<>9__20_0;
                itemConverter = <>c.<>9__20_0 = x => x;
            }
            this.jumpListBinding = SyncCollectionHelper.TwoWayBind<JumpItem, JumpItem>(this, this.jumpList.JumpItems, itemConverter, <>c.<>9__20_1 ??= x => x);
        }

        public bool ShowFrequentCategory
        {
            get => 
                this.jumpList.ShowFrequentCategory;
            set => 
                this.jumpList.ShowFrequentCategory = value;
        }

        public bool ShowRecentCategory
        {
            get => 
                this.jumpList.ShowRecentCategory;
            set => 
                this.jumpList.ShowRecentCategory = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NativeJumpList.<>c <>9 = new NativeJumpList.<>c();
            public static Func<JumpItem, JumpItemRejectionReason, Tuple<JumpItem, JumpItemRejectionReason>> <>9__14_1;
            public static Func<JumpItem, JumpItem> <>9__20_0;
            public static Func<JumpItem, JumpItem> <>9__20_1;

            internal Tuple<JumpItem, JumpItemRejectionReason> <ApplyOverride>b__14_1(JumpItem i, JumpItemRejectionReason r) => 
                new Tuple<JumpItem, JumpItemRejectionReason>(i, r);

            internal JumpItem <SetJumpListBinding>b__20_0(JumpItem x) => 
                x;

            internal JumpItem <SetJumpListBinding>b__20_1(JumpItem x) => 
                x;
        }
    }
}

