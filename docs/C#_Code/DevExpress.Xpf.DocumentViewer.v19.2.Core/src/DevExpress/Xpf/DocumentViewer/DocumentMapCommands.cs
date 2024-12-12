namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class DocumentMapCommands
    {
        static DocumentMapCommands()
        {
            ExpandTopLevelNodesCommand = new RoutedCommand("ExpandTopLevelNodesCommand", typeof(DocumentMapCommands));
            CommandManager.RegisterClassCommandBinding(typeof(TreeListView), new CommandBinding(ExpandTopLevelNodesCommand, (d, e) => ExecuteExpandTopLevelNodesCommand((TreeListView) d, e), (d, e) => CanExecuteExpandTopLevelNodesCommand((TreeListView) d, e)));
            CollapseTopLevelNodesCommand = new RoutedCommand("CollapseTopLevelNodesCommand", typeof(DocumentMapCommands));
            CommandManager.RegisterClassCommandBinding(typeof(TreeListView), new CommandBinding(CollapseTopLevelNodesCommand, (d, e) => ExecuteCollapseTopLevelNodesCommand((TreeListView) d, e), (d, e) => CanExecuteCollapseTopLevelNodesCommand((TreeListView) d, e)));
            ExpandCurrentNodeCommand = new RoutedCommand("ExpandCurrentNodeCommand", typeof(DocumentMapCommands));
            CommandManager.RegisterClassCommandBinding(typeof(TreeListView), new CommandBinding(ExpandCurrentNodeCommand, (d, e) => ExecuteExpandCurrentNodeCommand((TreeListView) d, e), (d, e) => ExecuteCanExpandCurrentNodeCommand((TreeListView) d, e)));
            GoToNodeCommand = new RoutedCommand("GoToNode", typeof(DocumentMapCommands));
            CommandManager.RegisterClassCommandBinding(typeof(TreeListView), new CommandBinding(GoToNodeCommand, (d, e) => ExecuteGoToNodeCommand((FrameworkElement) d, e)));
            CommandManager.RegisterClassCommandBinding(typeof(GridControl), new CommandBinding(GoToNodeCommand, (d, e) => ExecuteGoToNodeCommand((FrameworkElement) d, e)));
        }

        public static void CanExecuteCollapseTopLevelNodesCommand(TreeListView d, CanExecuteRoutedEventArgs e)
        {
            Func<TreeListNode, bool> predicate = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Func<TreeListNode, bool> local1 = <>c.<>9__21_0;
                predicate = <>c.<>9__21_0 = x => x.IsExpanded;
            }
            e.CanExecute = d.Nodes.Any<TreeListNode>(predicate);
            e.Handled = true;
        }

        public static void CanExecuteExpandTopLevelNodesCommand(TreeListView d, CanExecuteRoutedEventArgs e)
        {
            Func<TreeListNode, bool> predicate = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<TreeListNode, bool> local1 = <>c.<>9__19_0;
                predicate = <>c.<>9__19_0 = x => !x.IsExpanded;
            }
            e.CanExecute = d.Nodes.All<TreeListNode>(predicate);
            e.Handled = true;
        }

        private static void ExecuteCanExpandCurrentNodeCommand(TreeListView d, CanExecuteRoutedEventArgs e)
        {
            Func<TreeListNode, bool> evaluator = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<TreeListNode, bool> local1 = <>c.<>9__17_0;
                evaluator = <>c.<>9__17_0 = x => x.HasChildren;
            }
            e.CanExecute = d.GetNodeByContent(e.Parameter).Return<TreeListNode, bool>(evaluator, <>c.<>9__17_1 ??= () => false);
            e.Handled = true;
        }

        private static void ExecuteCollapseTopLevelNodesCommand(TreeListView d, ExecutedRoutedEventArgs e)
        {
            d.DataControl.UnselectAll();
            foreach (TreeListNode node in d.Nodes)
            {
                node.IsExpanded = false;
            }
            d.DataControl.SelectItem(d.FocusedNode.RowHandle);
            d.ScrollIntoView(d.FocusedNode.RowHandle);
            e.Handled = true;
        }

        private static void ExecuteExpandCurrentNodeCommand(TreeListView d, ExecutedRoutedEventArgs e)
        {
            TreeListNode nodeByRowHandle = d.GetNodeByRowHandle(d.FocusedRowHandle);
            TreeListNode node2 = nodeByRowHandle;
            while (nodeByRowHandle != null)
            {
                node2 = nodeByRowHandle;
                nodeByRowHandle = nodeByRowHandle.Nodes.FirstOrDefault<TreeListNode>();
            }
            for (TreeListNode node3 = node2; node3 != null; node3 = node3.ParentNode)
            {
                node3.IsExpanded = true;
            }
            d.DataControl.UnselectAll();
            d.DataControl.SelectItem(node2.RowHandle);
            d.FocusedNode = node2;
            d.ScrollIntoView(d.FocusedNode.RowHandle);
        }

        private static void ExecuteExpandTopLevelNodesCommand(TreeListView d, ExecutedRoutedEventArgs e)
        {
            foreach (TreeListNode node in d.Nodes)
            {
                node.IsExpanded = true;
            }
            d.ScrollIntoView(d.FocusedNode.RowHandle);
            e.Handled = true;
        }

        public static void ExecuteGoToNodeCommand(FrameworkElement d, ExecutedRoutedEventArgs e)
        {
            Predicate<DependencyObject> predicate = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Predicate<DependencyObject> local1 = <>c.<>9__18_0;
                predicate = <>c.<>9__18_0 = o => o is DocumentMapControl;
            }
            DocumentMapControl control = (DocumentMapControl) LayoutHelper.FindLayoutOrVisualParentObject(d, predicate, false, null);
            if (control != null)
            {
                control.HighlightedItem = e.Parameter;
                control.ActualSettings.GoToCommand.TryExecute(e.Parameter);
                e.Handled = true;
            }
        }

        public static ICommand ExpandTopLevelNodesCommand { get; private set; }

        public static ICommand CollapseTopLevelNodesCommand { get; private set; }

        public static ICommand ExpandCurrentNodeCommand { get; private set; }

        public static ICommand GoToNodeCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentMapCommands.<>c <>9 = new DocumentMapCommands.<>c();
            public static Func<TreeListNode, bool> <>9__17_0;
            public static Func<bool> <>9__17_1;
            public static Predicate<DependencyObject> <>9__18_0;
            public static Func<TreeListNode, bool> <>9__19_0;
            public static Func<TreeListNode, bool> <>9__21_0;

            internal void <.cctor>b__16_0(object d, ExecutedRoutedEventArgs e)
            {
                DocumentMapCommands.ExecuteExpandTopLevelNodesCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__16_1(object d, CanExecuteRoutedEventArgs e)
            {
                DocumentMapCommands.CanExecuteExpandTopLevelNodesCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__16_2(object d, ExecutedRoutedEventArgs e)
            {
                DocumentMapCommands.ExecuteCollapseTopLevelNodesCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__16_3(object d, CanExecuteRoutedEventArgs e)
            {
                DocumentMapCommands.CanExecuteCollapseTopLevelNodesCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__16_4(object d, ExecutedRoutedEventArgs e)
            {
                DocumentMapCommands.ExecuteExpandCurrentNodeCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__16_5(object d, CanExecuteRoutedEventArgs e)
            {
                DocumentMapCommands.ExecuteCanExpandCurrentNodeCommand((TreeListView) d, e);
            }

            internal void <.cctor>b__16_6(object d, ExecutedRoutedEventArgs e)
            {
                DocumentMapCommands.ExecuteGoToNodeCommand((FrameworkElement) d, e);
            }

            internal void <.cctor>b__16_7(object d, ExecutedRoutedEventArgs e)
            {
                DocumentMapCommands.ExecuteGoToNodeCommand((FrameworkElement) d, e);
            }

            internal bool <CanExecuteCollapseTopLevelNodesCommand>b__21_0(TreeListNode x) => 
                x.IsExpanded;

            internal bool <CanExecuteExpandTopLevelNodesCommand>b__19_0(TreeListNode x) => 
                !x.IsExpanded;

            internal bool <ExecuteCanExpandCurrentNodeCommand>b__17_0(TreeListNode x) => 
                x.HasChildren;

            internal bool <ExecuteCanExpandCurrentNodeCommand>b__17_1() => 
                false;

            internal bool <ExecuteGoToNodeCommand>b__18_0(DependencyObject o) => 
                o is DocumentMapControl;
        }
    }
}

