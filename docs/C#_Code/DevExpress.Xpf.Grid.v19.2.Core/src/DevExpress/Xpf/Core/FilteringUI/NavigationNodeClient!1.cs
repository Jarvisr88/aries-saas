namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class NavigationNodeClient<T> where T: class
    {
        public NavigationNodeClient(Func<T, IReadOnlyCollection<NavigationPath>> getPaths, Func<T, T[]> getChildren, Action<TreeNavigationState<T>, bool> setIsFocused)
        {
            if (getPaths == null)
            {
                throw new ArgumentNullException("getPaths");
            }
            if (getChildren == null)
            {
                throw new ArgumentNullException("getChildren");
            }
            if (setIsFocused == null)
            {
                throw new ArgumentNullException("setIsFocused");
            }
            this.<GetPaths>k__BackingField = getPaths;
            this.<GetChildren>k__BackingField = getChildren;
            this.<SetIsFocused>k__BackingField = setIsFocused;
        }

        public Func<T, IReadOnlyCollection<NavigationPath>> GetPaths { get; }

        public Func<T, T[]> GetChildren { get; }

        public Action<TreeNavigationState<T>, bool> SetIsFocused { get; }
    }
}

