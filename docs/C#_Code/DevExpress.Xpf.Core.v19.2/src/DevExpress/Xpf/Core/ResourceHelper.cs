namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ResourceHelper
    {
        internal static object CorrectResourceKey(object key) => 
            key;

        private static object FindResource(object key) => 
            ((Application.Current == null) || !Application.Current.Resources.Contains(key)) ? null : Application.Current.Resources[key];

        private static object FindResource(List<ResourceDictionary> dicList, object key)
        {
            object obj2 = null;
            int num = dicList.Count - 1;
            while (true)
            {
                if (num >= 0)
                {
                    obj2 = FindResource(dicList[num], key);
                    if (obj2 == null)
                    {
                        num--;
                        continue;
                    }
                }
                return obj2;
            }
        }

        public static object FindResource(this FrameworkElement root, object key)
        {
            key = CorrectResourceKey(key);
            return root.TryFindResource(key);
        }

        private static object FindResource(ResourceDictionary dic, object key)
        {
            if ((dic == null) || (key == null))
            {
                return null;
            }
            if (dic.Contains(key))
            {
                return dic[key];
            }
            object obj2 = null;
            int num = dic.MergedDictionaries.Count - 1;
            while (true)
            {
                if (num >= 0)
                {
                    obj2 = FindResource(dic.MergedDictionaries[num], key);
                    if (obj2 == null)
                    {
                        num--;
                        continue;
                    }
                }
                return obj2;
            }
        }
    }
}

