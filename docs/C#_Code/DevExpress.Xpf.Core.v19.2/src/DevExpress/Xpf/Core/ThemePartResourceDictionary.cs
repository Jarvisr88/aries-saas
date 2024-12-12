namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ThemePartResourceDictionary : ResourceDictionary
    {
        private bool isEnabled;
        private static List<ThemePartResourceDictionary> resourceDictionaries = new List<ThemePartResourceDictionary>();
        private Uri mutableSource;
        private object key;

        public ThemePartResourceDictionary()
        {
            this.AllowExnernalSetIsEnabled = true;
        }

        internal static void EnableSource(ThemePartResourceDictionary dictionary)
        {
            dictionary.IsEnabled = true;
        }

        internal static bool EnableSource(object key)
        {
            IEnumerable<ThemePartResourceDictionary> resourceDictionaries = GetResourceDictionaries(key);
            if (!resourceDictionaries.Any<ThemePartResourceDictionary>())
            {
                return false;
            }
            foreach (ThemePartResourceDictionary dictionary in resourceDictionaries)
            {
                if (dictionary.AllowExnernalSetIsEnabled)
                {
                    dictionary.IsEnabled = true;
                }
            }
            return true;
        }

        private static List<ThemePartResourceDictionary> GetResourceDictionaries(object key) => 
            (from resourceDictionary in ResourceDictionaries
                where key.Equals(resourceDictionary.Key)
                select resourceDictionary).ToList<ThemePartResourceDictionary>();

        protected virtual void OnDisabledSourceChanged()
        {
        }

        private void OnIsEnabledChanged()
        {
            if (this.IsEnabled)
            {
                ResourceDictionary item = new ResourceDictionary();
                item.Source = this.DisabledSource;
                base.MergedDictionaries.Add(item);
                ResourceDictionaries.Remove(this);
            }
        }

        private void OnKeyChanged()
        {
            ThemePartKeyExtension key = this.Key as ThemePartKeyExtension;
            if ((this.DisabledSource == null) && (key != null))
            {
                this.DisabledSource = key.Uri;
            }
            ResourceDictionaries.Add(this);
        }

        internal static List<ThemePartResourceDictionary> ResourceDictionaries =>
            resourceDictionaries;

        protected bool AllowExnernalSetIsEnabled { get; set; }

        protected bool IsEnabled
        {
            get => 
                this.isEnabled;
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.OnIsEnabledChanged();
                }
            }
        }

        public Uri DisabledSource
        {
            get => 
                this.mutableSource;
            set
            {
                if (this.mutableSource != value)
                {
                    this.mutableSource = value;
                    this.OnDisabledSourceChanged();
                }
            }
        }

        public object Key
        {
            get => 
                this.key;
            set
            {
                if (this.key != value)
                {
                    if (this.key != null)
                    {
                        throw new ArgumentException("Key");
                    }
                    this.key = value;
                    this.OnKeyChanged();
                }
            }
        }
    }
}

