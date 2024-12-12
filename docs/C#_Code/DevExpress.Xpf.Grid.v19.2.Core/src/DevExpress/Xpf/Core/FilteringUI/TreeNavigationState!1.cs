namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class TreeNavigationState<T> where T: class
    {
        public TreeNavigationState(T node, NavigationPath path)
        {
            this.<Node>k__BackingField = node;
            this.<Path>k__BackingField = path;
        }

        internal static bool AreSame(TreeNavigationState<T> oldValue, TreeNavigationState<T> newValue) => 
            ((oldValue != null) || (newValue != null)) ? (!(ReferenceEquals(oldValue, null) ^ ReferenceEquals(newValue, null)) ? ((oldValue.Node == newValue.Node) && NavigationPathExtensions.AreSame(oldValue.Path, newValue.Path)) : false) : true;

        public T Node { get; }

        public NavigationPath Path { get; }
    }
}

