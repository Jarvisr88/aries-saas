namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Threading;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    internal abstract class LocalizableUIElement<TUIElementID> : ILocalizableUIElement<TUIElementID> where TUIElementID: struct
    {
        protected readonly TUIElementID id;
        internal const string StringIdTypeSuffix = "Type";
        internal const string StringIdNameSuffix = "Name";
        internal const string StringIdDescriptionSuffix = "Description";
        internal static readonly string UIElementIDPrefix;

        static LocalizableUIElement()
        {
            string name = typeof(TUIElementID).Name;
            LocalizableUIElement<TUIElementID>.UIElementIDPrefix = name.Substring(0, name.LastIndexOf("Type"));
        }

        protected LocalizableUIElement(TUIElementID id)
        {
            this.id = id;
        }

        TUIElementID ILocalizableUIElement<TUIElementID>.GetID() => 
            this.id;

        public sealed override bool Equals(object obj)
        {
            ILocalizableUIElement<TUIElementID> element = obj as ILocalizableUIElement<TUIElementID>;
            return ((element != null) ? Equals(this.id, element.GetID()) : false);
        }

        internal static string GetDescription(TUIElementID id) => 
            FilterUIElementIDsResolver<TUIElementID>.GetDescription(id, Thread.CurrentThread.CurrentUICulture);

        protected abstract int GetHash(TUIElementID id);
        public sealed override int GetHashCode() => 
            this.GetHash(this.id);

        internal static string GetName(TUIElementID id) => 
            FilterUIElementIDsResolver<TUIElementID>.GetName(id, Thread.CurrentThread.CurrentUICulture);

        public sealed override string ToString() => 
            this.Name;

        public virtual string Name =>
            LocalizableUIElement<TUIElementID>.GetName(this.id);

        public virtual string Description =>
            LocalizableUIElement<TUIElementID>.GetDescription(this.id);

        private sealed class CultureKey : IEquatable<LocalizableUIElement<TUIElementID>.CultureKey>
        {
            private readonly TUIElementID id;
            private readonly string culture;

            public CultureKey(TUIElementID id, string culture)
            {
                this.id = id;
                this.culture = culture;
            }

            public bool Equals(LocalizableUIElement<TUIElementID>.CultureKey key) => 
                Equals(this.id, key.id) && string.Equals(this.culture, key.culture);

            public sealed override bool Equals(object obj)
            {
                LocalizableUIElement<TUIElementID>.CultureKey key = obj as LocalizableUIElement<TUIElementID>.CultureKey;
                return ((key != null) ? this.Equals(key) : false);
            }

            public sealed override int GetHashCode() => 
                HashCodeHelper.CalculateGeneric<TUIElementID, string>(this.id, this.culture);
        }

        internal static class FilterUIElementIDsResolver
        {
            private static readonly IDictionary<LocalizableUIElement<TUIElementID>.CultureKey, string> names;
            private static readonly IDictionary<LocalizableUIElement<TUIElementID>.CultureKey, string> descriptions;

            static FilterUIElementIDsResolver()
            {
                LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.names = new Dictionary<LocalizableUIElement<TUIElementID>.CultureKey, string>();
                LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.descriptions = new Dictionary<LocalizableUIElement<TUIElementID>.CultureKey, string>();
            }

            internal static string GetDescription(TUIElementID id, CultureInfo culture)
            {
                LocalizableUIElement<TUIElementID>.CultureKey key = new LocalizableUIElement<TUIElementID>.CultureKey(id, culture.Name);
                IDictionary<LocalizableUIElement<TUIElementID>.CultureKey, string> descriptions = LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.descriptions;
                lock (descriptions)
                {
                    string str;
                    if (!LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.descriptions.TryGetValue(key, out str))
                    {
                        str = LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.LoadDescription(key, id);
                    }
                    return str;
                }
            }

            internal static string GetName(TUIElementID id, CultureInfo culture)
            {
                LocalizableUIElement<TUIElementID>.CultureKey key = new LocalizableUIElement<TUIElementID>.CultureKey(id, culture.Name);
                IDictionary<LocalizableUIElement<TUIElementID>.CultureKey, string> names = LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.names;
                lock (names)
                {
                    string str;
                    if (!LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.names.TryGetValue(key, out str))
                    {
                        str = LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.LoadName(key, id);
                    }
                    return str;
                }
            }

            private static string LoadDescription(LocalizableUIElement<TUIElementID>.CultureKey key, TUIElementID id)
            {
                string str = LocalizableUIElement<TUIElementID>.UIElementIDPrefix + id.ToString() + "Description";
                string str2 = FilterUIElementLocalizer.GetString((FilterUIElementLocalizerStringId) Enum.Parse(typeof(FilterUIElementLocalizerStringId), str));
                LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.descriptions.Add(key, str2);
                return str2;
            }

            private static string LoadName(LocalizableUIElement<TUIElementID>.CultureKey key, TUIElementID id)
            {
                string str = LocalizableUIElement<TUIElementID>.UIElementIDPrefix + id.ToString() + "Name";
                string str2 = FilterUIElementLocalizer.GetString((FilterUIElementLocalizerStringId) Enum.Parse(typeof(FilterUIElementLocalizerStringId), str));
                LocalizableUIElement<TUIElementID>.FilterUIElementIDsResolver.names.Add(key, str2);
                return str2;
            }
        }
    }
}

