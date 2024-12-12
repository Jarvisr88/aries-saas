namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    internal static class DisposeHelper
    {
        public static void DisposeVisualTree(DependencyObject treeRoot)
        {
            if (treeRoot != null)
            {
                List<IDisposable> list = new List<IDisposable>();
                VisualTreeEnumerator enumerator = new VisualTreeEnumerator(treeRoot);
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        foreach (IDisposable disposable2 in list)
                        {
                            disposable2.Dispose();
                        }
                        break;
                    }
                    IDisposable current = enumerator.Current as IDisposable;
                    if (current != null)
                    {
                        list.Add(current);
                    }
                }
            }
        }
    }
}

