namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class ProductPreloadingHelper
    {
        private static volatile bool isPreloaded = false;
        private static readonly HashSet<string> PreloadedAssemblies = new HashSet<string>();
        internal static readonly Dictionary<ProductPreloading, IProductPreloadingItem> RegisteredItems = new Dictionary<ProductPreloading, IProductPreloadingItem>();

        static ProductPreloadingHelper()
        {
            RegisteredItems.Add(ProductPreloading.Editors, new EditorsProductPreloadingItem());
            RegisteredItems.Add(ProductPreloading.Bars, new BarsProductPreloadingItem());
            RegisteredItems.Add(ProductPreloading.Grid, new GridProductPreloadingItem());
        }

        public static void Perform(ProductPreloading products)
        {
            isPreloaded = false;
            Action mainThreadAction = <>c.<>9__4_1;
            if (<>c.<>9__4_1 == null)
            {
                Action local1 = <>c.<>9__4_1;
                mainThreadAction = <>c.<>9__4_1 = delegate {
                    while (!isPreloaded)
                    {
                        Thread.Sleep(50);
                    }
                };
            }
            BackgroundHelper.DoInBackground(() => PerformPreload(products), mainThreadAction, 200, ThreadPriority.Lowest, ApartmentState.STA);
        }

        private static void PerformPreload(ProductPreloading value)
        {
            new ProductPreloadingWindow(value).ShowDialog();
            isPreloaded = true;
        }

        internal static void PreloadAssembly(string assemblyFullName)
        {
            if (!PreloadedAssemblies.Contains(assemblyFullName))
            {
                PreloadedAssemblies.Add(assemblyFullName);
                if (!AppDomain.CurrentDomain.GetAssemblies().Any<Assembly>(x => (x.FullName == assemblyFullName)))
                {
                    Assembly.Load(assemblyFullName);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProductPreloadingHelper.<>c <>9 = new ProductPreloadingHelper.<>c();
            public static Action <>9__4_1;

            internal void <Perform>b__4_1()
            {
                while (!ProductPreloadingHelper.isPreloaded)
                {
                    Thread.Sleep(50);
                }
            }
        }
    }
}

