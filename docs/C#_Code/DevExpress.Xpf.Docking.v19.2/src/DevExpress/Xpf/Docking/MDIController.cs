namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Threading;

    public class MDIController : IMDIController, IActiveItemOwner, IDisposable, ILockOwner
    {
        internal int lockMinimizeBoundsCalculation;
        private ActiveItemHelper ActivationHelper;
        private DocumentPanel activeItemCore;
        private DockLayoutManager containerCore;
        private bool isDisposingCore;
        private int lockActivateCounter;
        private DevExpress.Xpf.Docking.MDIMenuBar mdiMenuBarCore;
        private BarManager mdiMenuManagerCore;

        public MDIController(DockLayoutManager container)
        {
            this.containerCore = container;
            this.ActivationHelper = new ActiveItemHelper(this);
        }

        public void Activate(BaseLayoutItem item)
        {
            this.Activate(item, true);
        }

        public void Activate(BaseLayoutItem item, bool focus)
        {
            if ((this.lockActivateCounter <= 0) && ((item is DocumentPanel) && (item.AllowActivate && !item.IsClosed)))
            {
                this.lockActivateCounter++;
                try
                {
                    if (this.RaiseItemCancelEvent(item, DockLayoutManager.MDIItemActivatingEvent))
                    {
                        item.InvokeCancelActivation(this.ActiveItem);
                    }
                    else
                    {
                        this.ActivationHelper.ActivateItem(item, focus);
                    }
                }
                finally
                {
                    this.lockActivateCounter--;
                }
            }
        }

        private static void ApplyState(DocumentGroup documentGroup, DocumentPanel document, MDIState state)
        {
            if ((documentGroup == null) || (!documentGroup.IsMaximized && (state == MDIState.Normal)))
            {
                MDIStateHelper.SetMDIState(document, GetActualMdiState(document, state));
            }
            else
            {
                foreach (BaseLayoutItem item in documentGroup.Items)
                {
                    DocumentPanel target = item as DocumentPanel;
                    if (target != null)
                    {
                        if (state == MDIState.Maximized)
                        {
                            target.IsMinimizedBeforeMaximize = target.IsMinimized;
                        }
                        MDIStateHelper.SetMDIState(target, GetActualMdiState(target, state));
                    }
                }
            }
        }

        public bool ArrangeIcons(BaseLayoutItem item)
        {
            if ((item == null) || item.IsClosed)
            {
                return false;
            }
            DocumentGroup documentGroup = this.GetDocumentGroup(item);
            if ((documentGroup == null) || documentGroup.IsUngroupped)
            {
                return false;
            }
            this.lockMinimizeBoundsCalculation++;
            BaseLayoutItem[] items = documentGroup.GetItems();
            for (int i = 0; i < items.Length; i++)
            {
                DocumentPanel document = items[i] as DocumentPanel;
                if ((document != null) && !document.IsMinimized)
                {
                    this.Minimize(document);
                }
            }
            UpdateLayout(documentGroup);
            double x = 0.0;
            double height = this.GetMDIAreaSize(documentGroup).Height;
            for (int j = 0; j < items.Length; j++)
            {
                DocumentPanel dObj = items[j] as DocumentPanel;
                if (dObj != null)
                {
                    DocumentPanel.SetMDILocation(dObj, new Point(x, height - dObj.MDIDocumentSize.Height));
                    x += dObj.MDIDocumentSize.Width;
                }
            }
            this.lockMinimizeBoundsCalculation--;
            return true;
        }

        private Point CalcMinimizeLocation(DocumentGroup dGroup, DocumentPanel document, Size mdiArea)
        {
            if (document.MinimizeLocation != null)
            {
                return document.MinimizeLocation.Value;
            }
            BaseLayoutItem[] items = dGroup.GetItems();
            List<Rect> rects = new List<Rect>();
            for (int i = 0; i < items.Length; i++)
            {
                DocumentPanel objA = items[i] as DocumentPanel;
                if (((objA != null) && (objA.IsMinimized && !ReferenceEquals(objA, document))) && (objA.MinimizeLocation == null))
                {
                    rects.Add(new Rect(objA.MDILocation, objA.MDIDocumentSize));
                }
            }
            return ((rects.Count != 0) ? this.CalcMinimizeLocationCore(rects, document.MDIDocumentSize, mdiArea) : new Point(0.0, mdiArea.Height - document.MDIDocumentSize.Height));
        }

        private Point CalcMinimizeLocationCore(List<Rect> rects, Size docSize, Size mdiArea)
        {
            Point topLeft;
            Comparison<Rect> comparison = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Comparison<Rect> local1 = <>c.<>9__38_0;
                comparison = <>c.<>9__38_0 = (a, b) => a.Left.CompareTo(b.Left);
            }
            rects.Sort(comparison);
            double y = mdiArea.Height - docSize.Height;
            Rect test = new Rect(new Point(0.0, y), docSize);
            if (this.IsMinimizeLocationValid(test, rects, mdiArea))
            {
                return test.TopLeft;
            }
            using (List<Rect>.Enumerator enumerator = rects.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Rect current = enumerator.Current;
                        test = new Rect(new Point(current.Right, y), docSize);
                        if (this.IsMinimizeLocationValid(test, rects, mdiArea))
                        {
                            topLeft = test.TopLeft;
                        }
                        else
                        {
                            test = new Rect(new Point(current.Left - docSize.Width, y), docSize);
                            if (!this.IsMinimizeLocationValid(test, rects, mdiArea))
                            {
                                continue;
                            }
                            topLeft = test.TopLeft;
                        }
                    }
                    else
                    {
                        return rects.Last<Rect>().TopRight;
                    }
                    break;
                }
            }
            return topLeft;
        }

        private bool CanMaximizeItem(BaseLayoutItem item) => 
            (item != null) && (!item.IsClosed && (item.AllowMaximize && item.IsMaximizable));

        private bool CanMinimizeItem(BaseLayoutItem item) => 
            (item != null) && (!item.IsClosed && (item.AllowMinimize && item.IsMinimizable));

        public bool Cascade(BaseLayoutItem item)
        {
            if ((item == null) || item.IsClosed)
            {
                return false;
            }
            DocumentGroup documentGroup = this.GetDocumentGroup(item);
            if ((documentGroup == null) || documentGroup.IsUngroupped)
            {
                return false;
            }
            UpdateLayout(documentGroup);
            BaseLayoutItem[] itemArray = LayoutItemsHelper.SortByZIndex(CascadeHelper.GetItemsToCascade(documentGroup, ReferenceEquals(this.ActiveItem, documentGroup.SelectedItem)));
            double mDIDocumentHeaderHeight = this.GetMDIDocumentHeaderHeight(documentGroup);
            Rect[] rectArray = CascadeHelper.GetBounds(this.GetMDIAreaSize(documentGroup), itemArray.Length, mDIDocumentHeaderHeight);
            for (int i = 0; i < itemArray.Length; i++)
            {
                DocumentPanel target = itemArray[i] as DocumentPanel;
                if (target != null)
                {
                    if (MDIStateHelper.GetMDIState(target) != MDIState.Normal)
                    {
                        this.Restore(target);
                    }
                    DocumentPanel.SetMDISize(target, rectArray[i].Size());
                    DocumentPanel.SetMDILocation(target, rectArray[i].Location());
                }
            }
            return true;
        }

        public bool ChangeMDIStyle(BaseLayoutItem item)
        {
            if ((item == null) || item.IsClosed)
            {
                return false;
            }
            DocumentGroup documentGroup = this.GetDocumentGroup(item);
            if ((documentGroup == null) || documentGroup.IsUngroupped)
            {
                return false;
            }
            documentGroup.ChangeMDIStyle();
            UpdateLayout(documentGroup);
            return true;
        }

        public T CreateCommand<T>(BaseLayoutItem[] items) where T: MDIControllerCommand, new()
        {
            T local1 = Activator.CreateInstance<T>();
            local1.Controller = this;
            local1.Items = items;
            return local1;
        }

        void ILockOwner.Lock()
        {
            this.LockActivate();
        }

        void ILockOwner.Unlock()
        {
            this.UnlockActivate();
        }

        private static MDIState GetActualMdiState(DocumentPanel document, MDIState state)
        {
            if ((state == MDIState.Normal) && (document.IsMaximized && document.IsMinimizedBeforeMaximize))
            {
                state = MDIState.Minimized;
            }
            return state;
        }

        private DocumentGroup GetDocumentGroup(BaseLayoutItem item)
        {
            DocumentPanel panel = item as DocumentPanel;
            DocumentGroup parent = item as DocumentGroup;
            if ((parent == null) && (panel != null))
            {
                parent = panel.Parent as DocumentGroup;
            }
            return parent;
        }

        private Size GetMDIAreaSize(DocumentGroup dGroup) => 
            dGroup.MDIAreaSize;

        private double GetMDIDocumentHeaderHeight(DocumentGroup dGroup)
        {
            double mDIDocumentHeaderHeight = dGroup.MDIDocumentHeaderHeight;
            return (double.IsNaN(mDIDocumentHeaderHeight) ? 24.0 : mDIDocumentHeaderHeight);
        }

        private bool IsMinimizeLocationValid(Rect test, List<Rect> rects, Size mdiArea)
        {
            bool flag;
            if (test.Left < 0.0)
            {
                return false;
            }
            using (List<Rect>.Enumerator enumerator = rects.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Rect current = enumerator.Current;
                        if (!this.IsRectsIntersect(test, current))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool IsRectsIntersect(Rect a, Rect b)
        {
            a.Intersect(b);
            return (!a.IsEmpty && (!MathHelper.IsZero(a.Width) && !MathHelper.IsZero(a.Height)));
        }

        private void LockActivate()
        {
            this.lockActivateCounter++;
        }

        public bool Maximize(BaseLayoutItem item)
        {
            bool flag;
            if (!this.CanMaximizeItem(item))
            {
                return false;
            }
            FloatGroup root = item.GetRoot() as FloatGroup;
            Func<FloatGroup, FloatingWindowLock> evaluator = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Func<FloatGroup, FloatingWindowLock> local1 = <>c.<>9__28_0;
                evaluator = <>c.<>9__28_0 = x => x.FloatingWindowLock;
            }
            Action<FloatingWindowLock> action = <>c.<>9__28_1;
            if (<>c.<>9__28_1 == null)
            {
                Action<FloatingWindowLock> local2 = <>c.<>9__28_1;
                action = <>c.<>9__28_1 = delegate (FloatingWindowLock x) {
                    x.Lock(FloatingWindowLock.LockerKey.WindowState);
                };
            }
            root.With<FloatGroup, FloatingWindowLock>(evaluator).Do<FloatingWindowLock>(action);
            try
            {
                if (item is DocumentPanel)
                {
                    flag = this.Maximize((DocumentPanel) item);
                }
                else
                {
                    (root ?? item).SetFloatState(FloatState.Maximized);
                    this.Container.InvalidateView(item.GetRoot());
                    flag = true;
                }
            }
            finally
            {
                Func<FloatGroup, FloatingWindowLock> func1 = <>c.<>9__28_2;
                if (<>c.<>9__28_2 == null)
                {
                    Func<FloatGroup, FloatingWindowLock> local4 = <>c.<>9__28_2;
                    func1 = <>c.<>9__28_2 = x => x.FloatingWindowLock;
                }
                Action<FloatingWindowLock> action1 = <>c.<>9__28_3;
                if (<>c.<>9__28_3 == null)
                {
                    Action<FloatingWindowLock> local5 = <>c.<>9__28_3;
                    action1 = <>c.<>9__28_3 = delegate (FloatingWindowLock x) {
                        x.Unlock(FloatingWindowLock.LockerKey.WindowState);
                    };
                }
                root.With<FloatGroup, FloatingWindowLock>(func1).Do<FloatingWindowLock>(action1);
            }
            return flag;
        }

        public bool Maximize(DocumentPanel document)
        {
            bool flag;
            if (!this.CanMaximizeItem(document))
            {
                return false;
            }
            FloatGroup root = document.GetRoot() as FloatGroup;
            root?.FloatingWindowLock.Do<FloatingWindowLock>(<>c.<>9__29_0 ??= delegate (FloatingWindowLock x) {
                x.Lock(FloatingWindowLock.LockerKey.WindowState);
            });
            try
            {
                DocumentGroup parent = document.Parent as DocumentGroup;
                FloatGroup group = document.Parent as FloatGroup;
                if ((group != null) || (parent != null))
                {
                    ApplyState(parent, document, MDIState.Maximized);
                    if (group != null)
                    {
                        group.SetFloatState(FloatState.Maximized);
                        this.Container.InvalidateView(group);
                    }
                }
                flag = true;
            }
            finally
            {
                FloatingWindowLock @lock;
                Action<FloatingWindowLock> action = <>c.<>9__29_1;
                if (<>c.<>9__29_1 == null)
                {
                    Action<FloatingWindowLock> local2 = <>c.<>9__29_1;
                    action = <>c.<>9__29_1 = delegate (FloatingWindowLock x) {
                        x.Unlock(FloatingWindowLock.LockerKey.WindowState);
                    };
                }
                @lock.Do<FloatingWindowLock>(action);
            }
            return flag;
        }

        public bool Minimize(BaseLayoutItem item)
        {
            bool flag;
            if (!this.CanMinimizeItem(item))
            {
                return false;
            }
            FloatGroup root = item.GetRoot() as FloatGroup;
            Func<FloatGroup, FloatingWindowLock> evaluator = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Func<FloatGroup, FloatingWindowLock> local1 = <>c.<>9__30_0;
                evaluator = <>c.<>9__30_0 = x => x.FloatingWindowLock;
            }
            Action<FloatingWindowLock> action = <>c.<>9__30_1;
            if (<>c.<>9__30_1 == null)
            {
                Action<FloatingWindowLock> local2 = <>c.<>9__30_1;
                action = <>c.<>9__30_1 = delegate (FloatingWindowLock x) {
                    x.Lock(FloatingWindowLock.LockerKey.WindowState);
                };
            }
            root.With<FloatGroup, FloatingWindowLock>(evaluator).Do<FloatingWindowLock>(action);
            try
            {
                if (item is DocumentPanel)
                {
                    flag = this.Minimize((DocumentPanel) item);
                }
                else
                {
                    (root ?? item).SetFloatState(FloatState.Minimized);
                    this.Container.InvalidateView(item.GetRoot());
                    flag = true;
                }
            }
            finally
            {
                Func<FloatGroup, FloatingWindowLock> func1 = <>c.<>9__30_2;
                if (<>c.<>9__30_2 == null)
                {
                    Func<FloatGroup, FloatingWindowLock> local4 = <>c.<>9__30_2;
                    func1 = <>c.<>9__30_2 = x => x.FloatingWindowLock;
                }
                Action<FloatingWindowLock> action1 = <>c.<>9__30_3;
                if (<>c.<>9__30_3 == null)
                {
                    Action<FloatingWindowLock> local5 = <>c.<>9__30_3;
                    action1 = <>c.<>9__30_3 = delegate (FloatingWindowLock x) {
                        x.Unlock(FloatingWindowLock.LockerKey.WindowState);
                    };
                }
                root.With<FloatGroup, FloatingWindowLock>(func1).Do<FloatingWindowLock>(action1);
            }
            return flag;
        }

        public bool Minimize(DocumentPanel document)
        {
            if (!this.CanMinimizeItem(document))
            {
                return false;
            }
            bool flag = MDIStateHelper.GetMDIState(document) == MDIState.Maximized;
            if (flag && document.IsMinimizedBeforeMaximize)
            {
                return this.Restore(document);
            }
            DocumentGroup documentGroup = document.Parent as DocumentGroup;
            if (documentGroup == null)
            {
                if (!document.IsMinimizable)
                {
                    return false;
                }
                FloatGroup parent = document.Parent as FloatGroup;
                if (parent != null)
                {
                    ApplyState(null, document, MDIState.Minimized);
                    parent.SetFloatState(FloatState.Minimized);
                    this.Container.InvalidateView(parent);
                }
                return true;
            }
            if (documentGroup.IsTabbed)
            {
                return false;
            }
            Size mdiArea = Size.Empty;
            if ((this.lockMinimizeBoundsCalculation == 0) || (document.MinimizeLocation == null))
            {
                UpdateLayout(documentGroup);
                mdiArea = this.GetMDIAreaSize(documentGroup);
            }
            MDIStateHelper.SetMDIState(document, MDIState.Minimized);
            if (this.lockMinimizeBoundsCalculation == 0)
            {
                UpdateLayout(document);
                Action method = delegate {
                    this.MinimizeCore(document, documentGroup, mdiArea);
                };
                if (flag)
                {
                    DevExpress.Xpf.Docking.InvokeHelper.BeginInvoke(document, method, DispatcherPriority.Render, new object[0]);
                }
                else
                {
                    method();
                }
                this.RestoreOtherDocuments(documentGroup, document);
            }
            return true;
        }

        private void MinimizeCore(DocumentPanel document, DocumentGroup documentGroup, Size mdiArea)
        {
            Point point = this.CalcMinimizeLocation(documentGroup, document, mdiArea);
            DocumentPanel.SetMDILocation(document, point);
        }

        protected void OnDisposing()
        {
            Ref.Dispose<ActiveItemHelper>(ref this.ActivationHelper);
            this.activeItemCore = null;
            this.containerCore = null;
            this.mdiMenuManagerCore = null;
            this.mdiMenuBarCore = null;
        }

        private void OnMDIMenuBarItemLinksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.MDIMenuBar.IsMerged)
            {
                MDIControllerHelper.UnMergeMDIMenuItems(this.MDIMenuBar);
                MDIControllerHelper.MergeMDIMenuItems(this.MDIMenuBar);
            }
        }

        private void RaiseActiveItemChanged(DocumentPanel item, DocumentPanel oldItem)
        {
            MDIItemActivatedEventArgs e = new MDIItemActivatedEventArgs(item, oldItem);
            e.Source = this.Container;
            this.Container.RaiseEvent(e);
        }

        private bool RaiseItemCancelEvent(BaseLayoutItem item, RoutedEvent routedEvent) => 
            this.Container.RaiseItemCancelEvent(item, routedEvent);

        public bool Restore(BaseLayoutItem item)
        {
            bool flag;
            if ((item == null) || item.IsClosed)
            {
                return false;
            }
            FloatGroup root = item.GetRoot() as FloatGroup;
            if (!LayoutItemsHelper.IsFloatingRootItem(item) && (!(item is FloatGroup) && !(item is DocumentPanel)))
            {
                return false;
            }
            Func<FloatGroup, FloatingWindowLock> evaluator = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Func<FloatGroup, FloatingWindowLock> local1 = <>c.<>9__32_0;
                evaluator = <>c.<>9__32_0 = x => x.FloatingWindowLock;
            }
            Action<FloatingWindowLock> action = <>c.<>9__32_1;
            if (<>c.<>9__32_1 == null)
            {
                Action<FloatingWindowLock> local2 = <>c.<>9__32_1;
                action = <>c.<>9__32_1 = delegate (FloatingWindowLock x) {
                    x.Lock(FloatingWindowLock.LockerKey.WindowState);
                };
            }
            root.With<FloatGroup, FloatingWindowLock>(evaluator).Do<FloatingWindowLock>(action);
            try
            {
                if (item is DocumentPanel)
                {
                    flag = this.Restore((DocumentPanel) item);
                }
                else
                {
                    (root ?? item).SetFloatState(FloatState.Normal);
                    this.Container.InvalidateView(item.GetRoot());
                    flag = true;
                }
            }
            finally
            {
                Func<FloatGroup, FloatingWindowLock> func1 = <>c.<>9__32_2;
                if (<>c.<>9__32_2 == null)
                {
                    Func<FloatGroup, FloatingWindowLock> local4 = <>c.<>9__32_2;
                    func1 = <>c.<>9__32_2 = x => x.FloatingWindowLock;
                }
                Action<FloatingWindowLock> action1 = <>c.<>9__32_3;
                if (<>c.<>9__32_3 == null)
                {
                    Action<FloatingWindowLock> local5 = <>c.<>9__32_3;
                    action1 = <>c.<>9__32_3 = delegate (FloatingWindowLock x) {
                        x.Unlock(FloatingWindowLock.LockerKey.WindowState);
                    };
                }
                root.With<FloatGroup, FloatingWindowLock>(func1).Do<FloatingWindowLock>(action1);
            }
            return flag;
        }

        public bool Restore(DocumentPanel document)
        {
            bool flag;
            if ((document == null) || document.IsClosed)
            {
                return false;
            }
            FloatGroup root = document.GetRoot() as FloatGroup;
            root?.FloatingWindowLock.Do<FloatingWindowLock>(<>c.<>9__33_0 ??= delegate (FloatingWindowLock x) {
                x.Lock(FloatingWindowLock.LockerKey.WindowState);
            });
            try
            {
                if (!document.SupportsFloatOrMDIState)
                {
                    flag = false;
                }
                else
                {
                    DocumentGroup parent = document.Parent as DocumentGroup;
                    FloatGroup group = document.Parent as FloatGroup;
                    if ((group != null) || (parent != null))
                    {
                        ApplyState(parent, document, MDIState.Normal);
                        if (group != null)
                        {
                            group.SetFloatState(FloatState.Normal);
                            this.Container.InvalidateView(group);
                        }
                        this.ActivationHelper.RestoreKeyboardFocus(document);
                    }
                    flag = true;
                }
            }
            finally
            {
                FloatingWindowLock @lock;
                Action<FloatingWindowLock> action = <>c.<>9__33_1;
                if (<>c.<>9__33_1 == null)
                {
                    Action<FloatingWindowLock> local2 = <>c.<>9__33_1;
                    action = <>c.<>9__33_1 = delegate (FloatingWindowLock x) {
                        x.Unlock(FloatingWindowLock.LockerKey.WindowState);
                    };
                }
                @lock.Do<FloatingWindowLock>(action);
            }
            return flag;
        }

        private void RestoreOtherDocuments(DocumentGroup dGroup, DocumentPanel document)
        {
            BaseLayoutItem[] items = dGroup.GetItems();
            for (int i = 0; i < items.Length; i++)
            {
                DocumentPanel objA = items[i] as DocumentPanel;
                if ((objA != null) && (!ReferenceEquals(objA, document) && objA.IsMaximized))
                {
                    this.Restore(objA);
                }
            }
        }

        private void SetActive(bool value)
        {
            if (this.ActiveItem != null)
            {
                this.ActiveItem.SetActive(value);
                if (value && (this.lockActivateCounter == 0))
                {
                    this.ActivationHelper.SelectInGroup(this.ActiveItem);
                }
            }
        }

        private void SetActiveItemCore(DocumentPanel value)
        {
            this.SetActive(false);
            DocumentPanel activeItemCore = this.activeItemCore;
            this.activeItemCore = value;
            this.SetActive(true);
            DockLayoutManager container = this.Container;
            container.isMDIItemActivation++;
            this.Container.ActiveMDIItem = this.ActiveItem;
            DockLayoutManager manager2 = this.Container;
            manager2.isMDIItemActivation--;
            this.RaiseActiveItemChanged(value, activeItemCore);
        }

        void IDisposable.Dispose()
        {
            if (!this.IsDisposing)
            {
                this.isDisposingCore = true;
                this.OnDisposing();
            }
            GC.SuppressFinalize(this);
        }

        public bool TileHorizontal(BaseLayoutItem item)
        {
            if ((item == null) || item.IsClosed)
            {
                return false;
            }
            DocumentGroup documentGroup = this.GetDocumentGroup(item);
            if ((documentGroup == null) || documentGroup.IsUngroupped)
            {
                return false;
            }
            BaseLayoutItem[] items = documentGroup.GetItems();
            Point location = new Point();
            Rect[] rectArray = TileHelper.TileLayout(items.Length, new Rect(location, this.GetMDIAreaSize(documentGroup)), true);
            for (int i = 0; i < items.Length; i++)
            {
                DocumentPanel target = items[i] as DocumentPanel;
                if (target != null)
                {
                    if (MDIStateHelper.GetMDIState(target) != MDIState.Normal)
                    {
                        this.Restore(target);
                    }
                    DocumentPanel.SetMDILocation(target, rectArray[i].Location());
                    DocumentPanel.SetMDISize(target, rectArray[i].Size());
                }
            }
            UpdateLayout(documentGroup);
            return true;
        }

        public bool TileVertical(BaseLayoutItem item)
        {
            if ((item == null) || item.IsClosed)
            {
                return false;
            }
            DocumentGroup documentGroup = this.GetDocumentGroup(item);
            if ((documentGroup == null) || documentGroup.IsUngroupped)
            {
                return false;
            }
            BaseLayoutItem[] items = documentGroup.GetItems();
            Point location = new Point();
            Rect[] rectArray = TileHelper.TileLayout(items.Length, new Rect(location, this.GetMDIAreaSize(documentGroup)), false);
            for (int i = 0; i < items.Length; i++)
            {
                DocumentPanel target = items[i] as DocumentPanel;
                if (target != null)
                {
                    if (MDIStateHelper.GetMDIState(target) != MDIState.Normal)
                    {
                        this.Restore(target);
                    }
                    DocumentPanel.SetMDILocation(target, rectArray[i].Location());
                    DocumentPanel.SetMDISize(target, rectArray[i].Size());
                }
            }
            UpdateLayout(documentGroup);
            return true;
        }

        private void UnlockActivate()
        {
            this.lockActivateCounter--;
        }

        private static void UpdateLayout(DocumentGroup dGroup)
        {
            dGroup.UpdateLayout();
        }

        private static void UpdateLayout(DocumentPanel document)
        {
            document.UpdateLayout();
        }

        [Description("")]
        public BaseLayoutItem ActiveItem
        {
            get => 
                this.activeItemCore;
            set
            {
                if (!ReferenceEquals(this.ActiveItem, value))
                {
                    this.SetActiveItemCore(value as DocumentPanel);
                }
            }
        }

        [Description("")]
        public DockLayoutManager Container =>
            this.containerCore;

        public DevExpress.Xpf.Docking.MDIMenuBar MDIMenuBar
        {
            get
            {
                if ((this.mdiMenuBarCore == null) && !this.IsDisposing)
                {
                    BarManager manager1 = new BarManager();
                    manager1.Name = UniqueNameHelper.GetMDIBarManagerName();
                    manager1.KeyGestureWorkingMode = KeyGestureWorkingMode.AllKeyGesture;
                    manager1.CreateStandardLayout = false;
                    this.mdiMenuManagerCore = manager1;
                    DXSerializer.SetEnabled(this.mdiMenuManagerCore, false);
                    this.mdiMenuBarCore = new DevExpress.Xpf.Docking.MDIMenuBar(this.Container, this.mdiMenuManagerCore);
                    DockLayoutManager.AddToVisualTree(this.Container, this.mdiMenuManagerCore);
                    DockLayoutManager.AddLogicalChild(this.Container, this.mdiMenuManagerCore);
                    this.mdiMenuBarCore.ItemLinks.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnMDIMenuBarItemLinksCollectionChanged);
                }
                return this.mdiMenuBarCore;
            }
        }

        protected bool IsDisposing =>
            this.isDisposingCore;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MDIController.<>c <>9 = new MDIController.<>c();
            public static Func<FloatGroup, FloatingWindowLock> <>9__28_0;
            public static Action<FloatingWindowLock> <>9__28_1;
            public static Func<FloatGroup, FloatingWindowLock> <>9__28_2;
            public static Action<FloatingWindowLock> <>9__28_3;
            public static Action<FloatingWindowLock> <>9__29_0;
            public static Action<FloatingWindowLock> <>9__29_1;
            public static Func<FloatGroup, FloatingWindowLock> <>9__30_0;
            public static Action<FloatingWindowLock> <>9__30_1;
            public static Func<FloatGroup, FloatingWindowLock> <>9__30_2;
            public static Action<FloatingWindowLock> <>9__30_3;
            public static Func<FloatGroup, FloatingWindowLock> <>9__32_0;
            public static Action<FloatingWindowLock> <>9__32_1;
            public static Func<FloatGroup, FloatingWindowLock> <>9__32_2;
            public static Action<FloatingWindowLock> <>9__32_3;
            public static Action<FloatingWindowLock> <>9__33_0;
            public static Action<FloatingWindowLock> <>9__33_1;
            public static Comparison<Rect> <>9__38_0;

            internal int <CalcMinimizeLocationCore>b__38_0(Rect a, Rect b) => 
                a.Left.CompareTo(b.Left);

            internal FloatingWindowLock <Maximize>b__28_0(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <Maximize>b__28_1(FloatingWindowLock x)
            {
                x.Lock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal FloatingWindowLock <Maximize>b__28_2(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <Maximize>b__28_3(FloatingWindowLock x)
            {
                x.Unlock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal void <Maximize>b__29_0(FloatingWindowLock x)
            {
                x.Lock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal void <Maximize>b__29_1(FloatingWindowLock x)
            {
                x.Unlock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal FloatingWindowLock <Minimize>b__30_0(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <Minimize>b__30_1(FloatingWindowLock x)
            {
                x.Lock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal FloatingWindowLock <Minimize>b__30_2(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <Minimize>b__30_3(FloatingWindowLock x)
            {
                x.Unlock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal FloatingWindowLock <Restore>b__32_0(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <Restore>b__32_1(FloatingWindowLock x)
            {
                x.Lock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal FloatingWindowLock <Restore>b__32_2(FloatGroup x) => 
                x.FloatingWindowLock;

            internal void <Restore>b__32_3(FloatingWindowLock x)
            {
                x.Unlock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal void <Restore>b__33_0(FloatingWindowLock x)
            {
                x.Lock(FloatingWindowLock.LockerKey.WindowState);
            }

            internal void <Restore>b__33_1(FloatingWindowLock x)
            {
                x.Unlock(FloatingWindowLock.LockerKey.WindowState);
            }
        }

        internal static class CascadeHelper
        {
            private static readonly double minWidth = 200.0;
            private static readonly double minHeight = 100.0;

            public static Rect[] GetBounds(Size size, int count, double offset)
            {
                Rect[] rectArray = new Rect[count];
                double width = Math.Max(size.Width - (count * offset), minWidth);
                double height = Math.Max(size.Height - (count * offset), minHeight);
                int num5 = (int) Math.Max(1.0, Math.Min(Math.Round((double) ((size.Width - width) / offset)), Math.Round((double) ((size.Height - height) / offset))));
                int num6 = (int) Math.Round((double) (((double) count) / ((double) num5)));
                int index = 0;
                int num8 = 0;
                while (num8 < num6)
                {
                    int num9 = 0;
                    while (true)
                    {
                        if (num9 < num5)
                        {
                            rectArray[index] = new Rect(new Point(offset * num9, offset * num9), new Size(width, height));
                            if (++index != rectArray.Length)
                            {
                                num9++;
                                continue;
                            }
                        }
                        num8++;
                        break;
                    }
                }
                return rectArray;
            }

            public static BaseLayoutItem[] GetItemsToCascade(DocumentGroup group, bool f = false)
            {
                if (group == null)
                {
                    return new BaseLayoutItem[0];
                }
                BaseLayoutItem[] items = group.GetItems();
                int index = items.Length - 1;
                if (f && (group.SelectedItem != null))
                {
                    int destinationIndex = group.Items.IndexOf(group.SelectedItem);
                    if (destinationIndex < index)
                    {
                        Array.Copy(items, destinationIndex + 1, items, destinationIndex, index - destinationIndex);
                        items[index] = group.SelectedItem;
                    }
                }
                return items;
            }
        }

        internal static class TileHelper
        {
            private static int GetColumnCount(int childrenCount, bool isHorizontal) => 
                !isHorizontal ? (childrenCount / ((int) Math.Sqrt((double) childrenCount))) : ((int) Math.Sqrt((double) childrenCount));

            public static Rect[] GetTiles(Size size, int count, bool horz)
            {
                Rect[] rectArray = new Rect[count];
                if ((size.Width * size.Height) > 0.0)
                {
                    if (count < 4)
                    {
                        double num = 0.0;
                        double num2 = 0.0;
                        double num3 = (horz ? size.Width : size.Height) / ((double) count);
                        for (int j = 0; j < count; j++)
                        {
                            rectArray[j] = new Rect(num, num2, horz ? num3 : size.Width, horz ? size.Height : num3);
                            if (horz)
                            {
                                num += num3;
                            }
                            else
                            {
                                num2 += num3;
                            }
                        }
                        return rectArray;
                    }
                    int num5 = (int) (Math.Sqrt((double) count) + 0.5);
                    int num6 = (int) (Math.Sqrt((count * size.Width) / size.Height) + 0.5);
                    int num7 = (int) ((((double) count) / ((double) num6)) + 0.5);
                    if ((num5 * num5) == count)
                    {
                        num6 = num7 = num5;
                    }
                    double x = 0.0;
                    double y = 0.0;
                    double width = size.Width / ((double) num6);
                    double height = size.Height / ((double) num7);
                    int num12 = horz ? num6 : num7;
                    for (int i = 0; i < count; i++)
                    {
                        rectArray[i] = new Rect(x, y, width, height);
                        if (horz)
                        {
                            x += width;
                            if (((i + 1) % num6) == 0)
                            {
                                x = 0.0;
                                y += height;
                            }
                            if ((num7 > 1) && ((((i + 1) / num6) + 1) == num7))
                            {
                                width = size.Width / ((double) (count - (num6 * (num7 - 1))));
                            }
                        }
                        else
                        {
                            y += height;
                            if (((i + 1) % num7) == 0)
                            {
                                y = 0.0;
                                x += width;
                            }
                            if ((num6 > 1) && ((((i + 1) / num7) + 1) == num6))
                            {
                                height = size.Height / ((double) (count - (num7 * (num6 - 1))));
                            }
                        }
                    }
                }
                return rectArray;
            }

            public static unsafe Rect[] TileLayout(int childrenCount, Rect bounds, bool isHorizontal)
            {
                Rect[] rectArray = new Rect[childrenCount];
                double width = bounds.Width;
                double height = bounds.Height;
                Point location = bounds.Location;
                Size size = new Size();
                int num3 = 0;
                if (isHorizontal)
                {
                    int columnCount = GetColumnCount(childrenCount, isHorizontal);
                    while (columnCount > 0)
                    {
                        size.Width = width / ((double) columnCount);
                        width -= size.Width;
                        int num6 = (childrenCount - num3) / columnCount;
                        height = bounds.Height;
                        int index = num3;
                        while (true)
                        {
                            if (index >= (num6 + num3))
                            {
                                num3 += num6;
                                Point* pointPtr2 = &location;
                                pointPtr2.X += size.Width;
                                location.Y = bounds.Y;
                                columnCount--;
                                break;
                            }
                            size.Height = height / ((double) ((num6 + num3) - index));
                            height -= size.Height;
                            rectArray[index] = new Rect(location, size);
                            Point* pointPtr1 = &location;
                            pointPtr1.Y += size.Height;
                            index++;
                        }
                    }
                }
                else
                {
                    int columnCount = GetColumnCount(childrenCount, !isHorizontal);
                    while (columnCount > 0)
                    {
                        size.Height = height / ((double) columnCount);
                        height -= size.Height;
                        int num10 = (childrenCount - num3) / columnCount;
                        width = bounds.Width;
                        int index = num3;
                        while (true)
                        {
                            if (index >= (num10 + num3))
                            {
                                num3 += num10;
                                Point* pointPtr4 = &location;
                                pointPtr4.Y += size.Height;
                                location.X = bounds.X;
                                columnCount--;
                                break;
                            }
                            size.Width = width / ((double) ((num10 + num3) - index));
                            width -= size.Width;
                            rectArray[index] = new Rect(location, size);
                            Point* pointPtr3 = &location;
                            pointPtr3.X += size.Width;
                            index++;
                        }
                    }
                }
                return rectArray;
            }
        }
    }
}

