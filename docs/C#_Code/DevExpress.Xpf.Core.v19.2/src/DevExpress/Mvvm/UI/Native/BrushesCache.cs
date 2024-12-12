namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public static class BrushesCache
    {
        private static readonly Dictionary<Color, WeakReference> cache = new Dictionary<Color, WeakReference>();

        static BrushesCache()
        {
            AddDefaultBrushes();
        }

        private static void AddDefaultBrushes()
        {
            IEnumerable<PropertyInfo> source = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static).AsEnumerable<PropertyInfo>();
            Func<PropertyInfo, bool> predicate = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<PropertyInfo, bool> local1 = <>c.<>9__3_0;
                predicate = <>c.<>9__3_0 = x => x.PropertyType == typeof(SolidColorBrush);
            }
            source.Where<PropertyInfo>(predicate);
            foreach (PropertyInfo info in source)
            {
                SolidColorBrush target = (SolidColorBrush) info.GetValue(null, null);
                Color key = target.Color;
                if (!cache.ContainsKey(key))
                {
                    cache.Add(key, new WeakReference(target));
                }
            }
        }

        public static SolidColorBrush GetBrush(Color color)
        {
            WeakReference reference;
            BrushesCache.cache.TryGetValue(color, out reference);
            if ((reference != null) && reference.IsAlive)
            {
                return (SolidColorBrush) reference.Target;
            }
            SolidColorBrush target = new SolidColorBrush(color);
            target.Freeze();
            Dictionary<Color, WeakReference> cache = BrushesCache.cache;
            lock (cache)
            {
                BrushesCache.cache[color] = new WeakReference(target);
            }
            return target;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BrushesCache.<>c <>9 = new BrushesCache.<>c();
            public static Func<PropertyInfo, bool> <>9__3_0;

            internal bool <AddDefaultBrushes>b__3_0(PropertyInfo x) => 
                x.PropertyType == typeof(SolidColorBrush);
        }
    }
}

