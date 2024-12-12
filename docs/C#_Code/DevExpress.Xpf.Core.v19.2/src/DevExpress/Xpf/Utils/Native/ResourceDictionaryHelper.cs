namespace DevExpress.Xpf.Utils.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;

    public class ResourceDictionaryHelper
    {
        public static ResourceDictionary CloneResourceDictionary(ResourceDictionary dict) => 
            !IsEmptyDictionary(dict) ? CloneResourceDictionary(new ResourceDictionary(), dict) : new ResourceDictionary();

        public static ResourceDictionary CloneResourceDictionary(ResourceDictionary clone, ResourceDictionary dict)
        {
            foreach (DictionaryEntry entry in dict)
            {
                clone.Add(entry.Key, entry.Value);
            }
            foreach (ResourceDictionary dictionary in dict.MergedDictionaries)
            {
                clone.MergedDictionaries.Add(CloneResourceDictionary(dictionary));
            }
            return clone;
        }

        public static ResourceDictionary GetResources(DependencyObject d)
        {
            if (d is FrameworkElement)
            {
                return ((FrameworkElement) d).Resources;
            }
            if (!(d is FrameworkContentElement))
            {
                throw new ArgumentException("element");
            }
            return ((FrameworkContentElement) d).Resources;
        }

        public static bool IsEmptyDictionary(ResourceDictionary resources)
        {
            if (resources != null)
            {
                if (resources.Count > 0)
                {
                    return false;
                }
                if (resources.MergedDictionaries.Count > 0)
                {
                    using (IEnumerator<ResourceDictionary> enumerator = resources.MergedDictionaries.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            ResourceDictionary current = enumerator.Current;
                            if (!IsEmptyDictionary(current))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}

