namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class NavigationPathExtensions
    {
        public static bool AreSame(NavigationPath first, NavigationPath second) => 
            ((first != null) || (second != null)) ? (!(ReferenceEquals(first, null) ^ ReferenceEquals(second, null)) ? first.Value.SequenceEqual<int>(second.Value) : false) : true;

        private static int Compare(IReadOnlyCollection<int> first, IReadOnlyCollection<int> second)
        {
            for (int i = 0; i < Math.Min(first.Count, second.Count); i++)
            {
                int num2 = Comparer<int>.Default.Compare(first.ElementAt<int>(i), second.ElementAt<int>(i));
                if (num2 != 0)
                {
                    return num2;
                }
            }
            return Comparer<int>.Default.Compare(first.Count, second.Count);
        }

        private static NavigationPath FindBoundedPath(IEnumerable<NavigationPath> paths, Func<NavigationPath, NavigationPath, bool> predicate) => 
            paths.Aggregate<NavigationPath, NavigationPath>(null, (acc, current) => ((acc == null) || predicate(current, acc)) ? current : acc);

        public static bool IsToTheLeft(this NavigationPath first, NavigationPath second) => 
            Compare(first.Value, second.Value) < 0;

        public static bool IsToTheRight(this NavigationPath first, NavigationPath second) => 
            Compare(first.Value, second.Value) > 0;

        public static NavigationPath LeftMost(this IEnumerable<NavigationPath> paths) => 
            FindBoundedPath(paths, new Func<NavigationPath, NavigationPath, bool>(NavigationPathExtensions.IsToTheLeft));

        public static NavigationPath RightMost(this IEnumerable<NavigationPath> paths) => 
            FindBoundedPath(paths, new Func<NavigationPath, NavigationPath, bool>(NavigationPathExtensions.IsToTheRight));
    }
}

