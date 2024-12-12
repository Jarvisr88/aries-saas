namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TextureCache : IDisposable
    {
        private ConditionalWeakTable<TextureKey, TextureContent> cache = new ConditionalWeakTable<TextureKey, TextureContent>();
        private readonly Dictionary<TextureKey, TextureKey> keysCache = new Dictionary<TextureKey, TextureKey>();

        public void Add(TextureKey key, TextureContent content)
        {
            TextureKey key2 = this.keysCache.GetValueOrDefault<TextureKey, TextureKey>(key, key);
            this.keysCache[key2] = key2;
            this.cache.Remove(key2);
            this.cache.Add(key2, content);
        }

        public void Dispose()
        {
            this.Reset();
        }

        public TextureContent Get(TextureKey key)
        {
            TextureKey key2 = this.keysCache.GetValueOrDefault<TextureKey, TextureKey>(key, null);
            if (key2 != null)
            {
                TextureContent content;
                if (this.cache.TryGetValue(key2, out content))
                {
                    return content;
                }
                this.keysCache.Remove(key);
            }
            return null;
        }

        public void Reset()
        {
            this.keysCache.Clear();
        }

        public void UpdateCache(IEnumerable<TextureKey> viewKeys)
        {
            List<TextureKey> list = new List<TextureKey>(this.keysCache.Keys);
            foreach (TextureKey key in viewKeys)
            {
                if (list.Contains(key))
                {
                    list.Remove(key);
                }
            }
            foreach (TextureKey key2 in list)
            {
                this.keysCache.Remove(key2);
                this.cache.Remove(key2);
            }
        }
    }
}

