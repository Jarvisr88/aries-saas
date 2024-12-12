namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class DescendentsWalkerHelper
    {
        private object descendentsWalker;
        private object info;
        private readonly Func<DependencyObject, bool, bool> continueWalk;
        private readonly bool logicalTreeFirst;
        private static readonly Type tDescendentsWalker_object;
        private static readonly object eTreeWalkPriority_LogicalTree;
        private static readonly object eTreeWalkPriority_VisualTree;
        private static Action<object, DependencyObject, bool> dStartNode;
        private static readonly Type tVisitedCallback;
        private static readonly Action<object, object> set_startNode;
        private static readonly Action<object, int> set_recursionDepth;
        private static readonly Func<object, object> get_nodes;
        [ThreadStatic]
        private static Action<object> clearNodes;
        private Func<DependencyObject, bool, bool> localContinueWalk;

        static DescendentsWalkerHelper();
        private DescendentsWalkerHelper(bool logicalTreeFirst, Func<DependencyObject, bool, bool> continueWalk);
        private object CreateDescendentsWalker();
        public static DescendentsWalkerHelper CreateInstance(bool logicalTreeFirst = true, Func<DependencyObject, bool, bool> continueWalk = null);
        private bool OnVisitedCallback(DependencyObject d, object data, bool visitedViaVisualTree);
        private void PerformCleanup();
        public void StartWalk(DependencyObject startNode, Func<DependencyObject, bool, bool> localContinueWalk = null);
        public void StartWalk(DependencyObject startNode, bool skipStartNode, Func<DependencyObject, bool, bool> localContinueWalk = null);

        private object DescendentsWalker { get; }
    }
}

