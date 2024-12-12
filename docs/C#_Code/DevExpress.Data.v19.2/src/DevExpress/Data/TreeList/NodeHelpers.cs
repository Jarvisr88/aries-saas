namespace DevExpress.Data.TreeList
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class NodeHelpers
    {
        public static void DoForAncestors(this TreeListNodeBase node, Func<TreeListNodeBase, bool> canContinue, Action<TreeListNodeBase> action);
        public static void DoForDescendants(this TreeListNodeBase node, Func<TreeListNodeBase, bool> action);
        public static void ProcessNodeAction(TreeListNodeBase node, Func<TreeListNodeBase, bool> action);
        public static List<TreeListNodeBase> ToList(this ITreeListNodeCollection nodes);
    }
}

