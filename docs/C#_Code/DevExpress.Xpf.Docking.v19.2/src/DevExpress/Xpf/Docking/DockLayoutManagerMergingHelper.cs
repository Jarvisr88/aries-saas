namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class DockLayoutManagerMergingHelper : FrameworkContentElement, IMergingSupport, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement, IDisposable
    {
        private static readonly Action<DependencyObject, bool> setForceElementMerging;
        private readonly ObservableCollection<DockLayoutManagerMergingHelper> mergedChildren;
        private readonly DockLayoutManager owner;
        private bool isDisposed;
        private MergingSupportNameStorage msNameStorage;
        private const string MergingID = "0C082948-C6A4-4274-BF91-A3039EF0092F";

        static DockLayoutManagerMergingHelper()
        {
            int? parametersCount = null;
            setForceElementMerging = ReflectionHelper.CreateInstanceMethodHandler<Action<DependencyObject, bool>>(null, "SetForceElementMerging", BindingFlags.NonPublic | BindingFlags.Static, typeof(MergingProperties), parametersCount, null, true);
        }

        internal DockLayoutManagerMergingHelper(DockLayoutManager owner)
        {
            this.owner = owner;
            this.mergedChildren = new ObservableCollection<DockLayoutManagerMergingHelper>();
            this.mergedChildren.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnMergedChildrenCollectionChanged);
            BarNameScope.SetIsScopeOwner(this, false);
            this.Changed();
        }

        public void Changed()
        {
            bool flag = this.Owner.AllowMergingAutoHidePanels && this.Owner.IsVisible;
            MergingProperties.SetElementMergingBehavior(this, flag ? ElementMergingBehavior.InternalWithExternal : ElementMergingBehavior.InternalWithInternal);
            setForceElementMerging(this, flag);
        }

        object IMultipleElementRegistratorSupport.GetName(object registratorKey)
        {
            if (!Equals(registratorKey, typeof(IMergingSupport)))
            {
                throw new ArgumentException("registratorKey");
            }
            return ((IMergingSupport) this).NameStorage.CurrentValue;
        }

        bool IMergingSupport.CanMerge(IMergingSupport second) => 
            true;

        bool IMergingSupport.IsMergedParent(IMergingSupport second) => 
            ReferenceEquals(this.MergedParent, second);

        void IMergingSupport.Merge(IMergingSupport second)
        {
            DockLayoutManagerMergingHelper item = second as DockLayoutManagerMergingHelper;
            if ((item != null) && !this.MergedChildren.Contains(item))
            {
                this.MergedChildren.Add(item);
            }
        }

        void IMergingSupport.Unmerge(IMergingSupport second)
        {
            DockLayoutManagerMergingHelper item = second as DockLayoutManagerMergingHelper;
            if (item != null)
            {
                this.MergedChildren.Remove(item);
            }
        }

        public void Dispose()
        {
            if (!this.isDisposed)
            {
                this.isDisposed = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDisposing()
        {
            this.MergedParent.Do<IMergingSupport>(x => x.Unmerge(this));
        }

        private void OnMergedChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (DockLayoutManagerMergingHelper helper in e.OldItems)
                {
                    helper.MergedParent = null;
                    this.Owner.OnUnmerge(helper.Owner);
                }
            }
            if (e.NewItems != null)
            {
                foreach (DockLayoutManagerMergingHelper helper2 in e.NewItems)
                {
                    helper2.MergedParent = this;
                    this.Owner.OnMerge(helper2.Owner);
                }
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            MergingPropertiesHelper.OnPropertyChanged(this, e, null);
        }

        internal DockLayoutManagerMergingHelper MergedParent { get; private set; }

        bool IMergingSupport.IsAutomaticallyMerged
        {
            get => 
                true;
            set
            {
            }
        }

        bool IMergingSupport.IsMerged =>
            this.MergedParent != null;

        private ObservableCollection<DockLayoutManagerMergingHelper> MergedChildren =>
            this.mergedChildren;

        object IMergingSupport.MergingKey =>
            typeof(DockLayoutManager);

        MergingSupportNameStorage IMergingSupport.NameStorage
        {
            get
            {
                MergingSupportNameStorage msNameStorage = this.msNameStorage;
                if (this.msNameStorage == null)
                {
                    MergingSupportNameStorage local1 = this.msNameStorage;
                    Func<DependencyObject, object> baseValueGetter = <>c.<>9__21_0;
                    if (<>c.<>9__21_0 == null)
                    {
                        Func<DependencyObject, object> local2 = <>c.<>9__21_0;
                        baseValueGetter = <>c.<>9__21_0 = x => "0C082948-C6A4-4274-BF91-A3039EF0092F";
                    }
                    msNameStorage = this.msNameStorage = new MergingSupportNameStorage(this, baseValueGetter);
                }
                return msNameStorage;
            }
        }

        private DockLayoutManager Owner =>
            this.owner;

        IEnumerable<object> IMultipleElementRegistratorSupport.RegistratorKeys =>
            new <DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__36(-2);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockLayoutManagerMergingHelper.<>c <>9 = new DockLayoutManagerMergingHelper.<>c();
            public static Func<DependencyObject, object> <>9__21_0;

            internal object <DevExpress.Xpf.Bars.Native.IMergingSupport.get_NameStorage>b__21_0(DependencyObject x) => 
                "0C082948-C6A4-4274-BF91-A3039EF0092F";
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__36 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__36(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<>2__current = typeof(IMergingSupport);
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                DockLayoutManagerMergingHelper.<DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__36 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new DockLayoutManagerMergingHelper.<DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__36(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

