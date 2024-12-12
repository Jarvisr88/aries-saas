namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections;
    using System.Reflection;

    public sealed class DXWebAttributeCollection
    {
        private DXStateBag bag;
        private DXCssStyleCollection styleCollection;

        public DXWebAttributeCollection(DXStateBag bag)
        {
            this.bag = bag;
        }

        public void Add(string key, string value)
        {
            if ((this.styleCollection != null) && (StringExtensions.CompareInvariantCultureIgnoreCase(key, "style") == 0))
            {
                this.styleCollection.Value = value;
            }
            else
            {
                this.bag[key] = value;
            }
        }

        public void AddAttributes(DXHtmlTextWriter writer)
        {
            if (this.bag.Count > 0)
            {
                IDictionaryEnumerator enumerator = this.bag.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    StateItem item = enumerator.Value as StateItem;
                    if (item != null)
                    {
                        string str = item.Value as string;
                        string key = enumerator.Key as string;
                        if ((key != null) && (str != null))
                        {
                            writer.AddAttribute(key, str, true);
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            this.bag.Clear();
            if (this.styleCollection != null)
            {
                this.styleCollection.Clear();
            }
        }

        public override bool Equals(object o)
        {
            bool flag;
            DXWebAttributeCollection attributes = o as DXWebAttributeCollection;
            if (attributes == null)
            {
                return false;
            }
            if (attributes.Count != this.bag.Count)
            {
                return false;
            }
            using (IDictionaryEnumerator enumerator = this.bag.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DictionaryEntry current = (DictionaryEntry) enumerator.Current;
                        if (this[(string) current.Key] == attributes[(string) current.Key])
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        public override int GetHashCode()
        {
            int hashState = HashCodeHelper.Start();
            foreach (DictionaryEntry entry in this.bag)
            {
                HashCodeHelper.Combine(hashState, HashCodeHelper.GetHashCode<object>(entry.Key), HashCodeHelper.GetHashCode<object>(entry.Value));
            }
            return HashCodeHelper.Finish(hashState);
        }

        public void Remove(string key)
        {
            if ((this.styleCollection != null) && DXWebStringUtil.EqualsIgnoreCase(key, "style"))
            {
                this.styleCollection.Clear();
            }
            else
            {
                this.bag.Remove(key);
            }
        }

        public void Render(DXHtmlTextWriter writer)
        {
            if (this.bag.Count > 0)
            {
                IDictionaryEnumerator enumerator = this.bag.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    StateItem item = enumerator.Value as StateItem;
                    if (item != null)
                    {
                        string str = item.Value as string;
                        string key = enumerator.Key as string;
                        if ((key != null) && (str != null))
                        {
                            writer.WriteAttribute(key, str, true);
                        }
                    }
                }
            }
        }

        public int Count =>
            this.bag.Count;

        public DXCssStyleCollection CssStyle
        {
            get
            {
                this.styleCollection ??= new DXCssStyleCollection(this.bag);
                return this.styleCollection;
            }
        }

        public string this[string key]
        {
            get => 
                ((this.styleCollection == null) || !DXWebStringUtil.EqualsIgnoreCase(key, "style")) ? (this.bag[key] as string) : this.styleCollection.Value;
            set => 
                this.Add(key, value);
        }

        public ICollection Keys =>
            this.bag.Keys;
    }
}

