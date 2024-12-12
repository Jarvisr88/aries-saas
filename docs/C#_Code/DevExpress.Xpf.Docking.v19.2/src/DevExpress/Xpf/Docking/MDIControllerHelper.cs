namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class MDIControllerHelper
    {
        public static T CreateCommand<T>(DockLayoutManager container, BaseLayoutItem[] items) where T: MDIControllerCommand, new()
        {
            IMDIController mDIController = GetMDIController(container);
            if (mDIController != null)
            {
                return mDIController.CreateCommand<T>(items);
            }
            return default(T);
        }

        internal static void DoMerge(BaseDocument document)
        {
            DoMergeOrUnMerge(document, true);
        }

        private static void DoMergeOrUnMerge(BaseDocument document, bool merge)
        {
            ElementMergingBehavior elementMergingBehavior = GetElementMergingBehavior(document);
            if (merge || (elementMergingBehavior == ElementMergingBehavior.InternalWithExternal))
            {
                Func<BaseDocument, DocumentPanel> evaluator = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<BaseDocument, DocumentPanel> local1 = <>c.<>9__8_0;
                    evaluator = <>c.<>9__8_0 = x => x.DocumentPanel;
                }
                Func<DocumentPanel, DockLayoutManager> func2 = <>c.<>9__8_1;
                if (<>c.<>9__8_1 == null)
                {
                    Func<DocumentPanel, DockLayoutManager> local2 = <>c.<>9__8_1;
                    func2 = <>c.<>9__8_1 = x => x.Manager;
                }
                DockLayoutManager dobj = document.With<BaseDocument, DocumentPanel>(evaluator).With<DocumentPanel, DockLayoutManager>(func2);
                if (dobj != null)
                {
                    BarManager barManager = BarManager.GetBarManager(dobj);
                    BarManager child = FindChildBarManager(document);
                    if ((child == null) || (merge ? RaiseMerge(dobj, barManager, child) : RaiseUnMerge(dobj, barManager, child)))
                    {
                        SetElementMergingBehavior(document, merge ? ElementMergingBehavior.InternalWithExternal : ElementMergingBehavior.InternalWithInternal);
                    }
                }
            }
        }

        internal static void DoUnMerge(BaseDocument document)
        {
            DoMergeOrUnMerge(document, false);
        }

        public static BarManager FindChildBarManager(DependencyObject element)
        {
            BarManager current;
            if (element == null)
            {
                return null;
            }
            using (IEnumerator<DependencyObject> enumerator = LayoutItemsHelper.GetEnumerator(element, null))
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        if (enumerator.Current is BarManager)
                        {
                            current = enumerator.Current as BarManager;
                            break;
                        }
                        if (!(enumerator.Current is IUIElement) || (enumerator.Current == element))
                        {
                            continue;
                        }
                    }
                    return null;
                }
            }
            return current;
        }

        internal static MDIMergeStyle GetActualMDIMergeStyle(DockLayoutManager manager, BaseLayoutItem item)
        {
            MDIMergeStyle mDIMergeStyle = DocumentPanel.GetMDIMergeStyle(item);
            return (((mDIMergeStyle != MDIMergeStyle.Default) || (manager == null)) ? mDIMergeStyle : manager.MDIMergeStyle);
        }

        private static ElementMergingBehavior GetElementMergingBehavior(BaseDocument document)
        {
            Func<BaseDocument, BaseLayoutItem> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<BaseDocument, BaseLayoutItem> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => x.LayoutItem;
            }
            Func<BaseLayoutItem, ElementMergingBehavior> func2 = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<BaseLayoutItem, ElementMergingBehavior> local2 = <>c.<>9__9_1;
                func2 = <>c.<>9__9_1 = x => MergingProperties.GetElementMergingBehavior(x);
            }
            return document.With<BaseDocument, BaseLayoutItem>(evaluator).Return<BaseLayoutItem, ElementMergingBehavior>(func2, (<>c.<>9__9_2 ??= () => ElementMergingBehavior.Undefined));
        }

        private static IMDIController GetMDIController(DockLayoutManager container) => 
            container?.MDIController;

        public static IMDIController GetMDIController(object obj)
        {
            DockLayoutManager container = obj as DockLayoutManager;
            if (container == null)
            {
                DependencyObject obj2 = obj as DependencyObject;
                container = (obj2 != null) ? DockLayoutManager.GetDockLayoutManager(obj2) : null;
            }
            return GetMDIController(container);
        }

        public static bool MergeMDIMenuItems(DependencyObject dObj)
        {
            Func<IMDIController, MDIMenuBar> evaluator = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<IMDIController, MDIMenuBar> local1 = <>c.<>9__11_0;
                evaluator = <>c.<>9__11_0 = x => x.MDIMenuBar;
            }
            MDIMenuBar bar = dObj.With<DependencyObject, DockLayoutManager>(new Func<DependencyObject, DockLayoutManager>(DockLayoutManager.GetDockLayoutManager)).With<DockLayoutManager, IMDIController>(new Func<DockLayoutManager, IMDIController>(MDIControllerHelper.GetMDIController)).With<IMDIController, MDIMenuBar>(evaluator);
            if (bar == null)
            {
                return false;
            }
            MergingProperties.SetElementMergingBehavior(bar, ElementMergingBehavior.All);
            return bar.IsMerged;
        }

        public static void MergeMDITitles(DependencyObject dObj)
        {
            if (dObj != null)
            {
                DockLayoutManager dockLayoutManager = DockLayoutManager.GetDockLayoutManager(dObj);
                if ((dockLayoutManager != null) && dockLayoutManager.ShowMaximizedDocumentCaptionInWindowTitle)
                {
                    BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(dObj);
                    if (layoutItem != null)
                    {
                        dockLayoutManager.SetMDIChildrenTitle(layoutItem.Caption as string);
                    }
                }
            }
        }

        private static bool RaiseMerge(DockLayoutManager manager, BarManager parent, BarManager child)
        {
            BarMergeEventArgs args1 = new BarMergeEventArgs(parent, child);
            args1.RoutedEvent = DockLayoutManager.MergeEvent;
            BarMergeEventArgs e = args1;
            manager.RaiseEvent(e);
            return !e.Cancel;
        }

        private static bool RaiseUnMerge(DockLayoutManager manager, BarManager parent, BarManager child)
        {
            BarMergeEventArgs args1 = new BarMergeEventArgs(parent, child);
            args1.RoutedEvent = DockLayoutManager.UnMergeEvent;
            BarMergeEventArgs e = args1;
            manager.RaiseEvent(e);
            return !e.Cancel;
        }

        internal static void Restore(BaseLayoutItem item)
        {
            if (item is DocumentPanel)
            {
                DockLayoutManager manager = item.FindDockLayoutManager() ?? DockLayoutManager.FindManager(item);
                if ((manager != null) && !manager.IsDisposing)
                {
                    manager.MDIController.Restore((DocumentPanel) item);
                }
            }
        }

        private static void SetElementMergingBehavior(BaseDocument document, ElementMergingBehavior value)
        {
            Func<BaseDocument, BaseLayoutItem> evaluator = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<BaseDocument, BaseLayoutItem> local1 = <>c.<>9__10_0;
                evaluator = <>c.<>9__10_0 = x => x.LayoutItem;
            }
            BaseLayoutItem item = document.With<BaseDocument, BaseLayoutItem>(evaluator);
            if (item != null)
            {
                MergingProperties.SetElementMergingBehavior(item, value);
            }
        }

        public static void UnMergeMDIMenuItems(DependencyObject dObj)
        {
            Func<IMDIController, MDIMenuBar> evaluator = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<IMDIController, MDIMenuBar> local1 = <>c.<>9__12_0;
                evaluator = <>c.<>9__12_0 = x => x.MDIMenuBar;
            }
            MDIMenuBar bar = dObj.With<DependencyObject, DockLayoutManager>(new Func<DependencyObject, DockLayoutManager>(DockLayoutManager.GetDockLayoutManager)).With<DockLayoutManager, IMDIController>(new Func<DockLayoutManager, IMDIController>(MDIControllerHelper.GetMDIController)).With<IMDIController, MDIMenuBar>(evaluator);
            if (bar != null)
            {
                MergingProperties.SetElementMergingBehavior(bar, ElementMergingBehavior.None);
            }
        }

        public static void UnMergeMDITitles(DependencyObject dObj)
        {
            if (dObj != null)
            {
                DockLayoutManager dockLayoutManager = DockLayoutManager.GetDockLayoutManager(dObj);
                if (dockLayoutManager != null)
                {
                    dockLayoutManager.ResetMDIChildrenTitle();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MDIControllerHelper.<>c <>9 = new MDIControllerHelper.<>c();
            public static Func<BaseDocument, DocumentPanel> <>9__8_0;
            public static Func<DocumentPanel, DockLayoutManager> <>9__8_1;
            public static Func<BaseDocument, BaseLayoutItem> <>9__9_0;
            public static Func<BaseLayoutItem, ElementMergingBehavior> <>9__9_1;
            public static Func<ElementMergingBehavior> <>9__9_2;
            public static Func<BaseDocument, BaseLayoutItem> <>9__10_0;
            public static Func<IMDIController, MDIMenuBar> <>9__11_0;
            public static Func<IMDIController, MDIMenuBar> <>9__12_0;

            internal DocumentPanel <DoMergeOrUnMerge>b__8_0(BaseDocument x) => 
                x.DocumentPanel;

            internal DockLayoutManager <DoMergeOrUnMerge>b__8_1(DocumentPanel x) => 
                x.Manager;

            internal BaseLayoutItem <GetElementMergingBehavior>b__9_0(BaseDocument x) => 
                x.LayoutItem;

            internal ElementMergingBehavior <GetElementMergingBehavior>b__9_1(BaseLayoutItem x) => 
                MergingProperties.GetElementMergingBehavior(x);

            internal ElementMergingBehavior <GetElementMergingBehavior>b__9_2() => 
                ElementMergingBehavior.Undefined;

            internal MDIMenuBar <MergeMDIMenuItems>b__11_0(IMDIController x) => 
                x.MDIMenuBar;

            internal BaseLayoutItem <SetElementMergingBehavior>b__10_0(BaseDocument x) => 
                x.LayoutItem;

            internal MDIMenuBar <UnMergeMDIMenuItems>b__12_0(IMDIController x) => 
                x.MDIMenuBar;
        }
    }
}

