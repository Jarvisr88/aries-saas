namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;

    public class GenericThemePartResourceDictionary : ThemePartResourceDictionary
    {
        private static List<GenericThemePartResourceDictionary> disabledResourceDictionaries = new List<GenericThemePartResourceDictionary>();

        public GenericThemePartResourceDictionary()
        {
            base.AllowExnernalSetIsEnabled = false;
            this.UpdateIsEnabled();
        }

        private void AddDisabledResourceDictionaries()
        {
            if (!disabledResourceDictionaries.Contains(this))
            {
                disabledResourceDictionaries.Add(this);
            }
        }

        public static void EnableResourceDictionaries()
        {
            if (disabledResourceDictionaries.Count != 0)
            {
                foreach (GenericThemePartResourceDictionary dictionary in disabledResourceDictionaries)
                {
                    dictionary.IsEnabled = true;
                }
            }
        }

        protected override void OnDisabledSourceChanged()
        {
            base.OnDisabledSourceChanged();
            this.UpdateIsEnabled();
        }

        private void UpdateIsEnabled()
        {
            if ((ThemeManager.EnableDefaultThemeLoading || (string.IsNullOrEmpty(ApplicationThemeHelper.ApplicationThemeName) || (ApplicationThemeHelper.ApplicationThemeName == Theme.DefaultThemeName))) && (base.DisabledSource != null))
            {
                base.IsEnabled = true;
            }
            else
            {
                base.IsEnabled = false;
                this.AddDisabledResourceDictionaries();
            }
        }
    }
}

