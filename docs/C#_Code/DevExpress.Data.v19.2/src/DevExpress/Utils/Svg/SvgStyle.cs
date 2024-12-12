namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SvgStyle
    {
        private readonly SvgStyleElement styleElement = new SvgStyleElement();
        private readonly Dictionary<string, string> attributesCore = new Dictionary<string, string>();

        public SvgStyle DeepCopy()
        {
            SvgStyle style = new SvgStyle();
            foreach (KeyValuePair<string, string> pair in this.Attributes)
            {
                style.SetValue(pair.Key, pair.Value);
            }
            return style;
        }

        public void SetValue(string key, string value)
        {
            if (this.Attributes.ContainsKey(key))
            {
                this.Attributes[key] = value;
            }
            else
            {
                this.Attributes.Add(key, value);
            }
            SvgElementCreator.SetPropertyValue(this.styleElement, key, value, null);
        }

        public override string ToString() => 
            this.Name;

        public bool TryGetValue<T>(object key, T defaultValue, out T result) => 
            this.styleElement.TryGetValue<T>(key, defaultValue, out result);

        public string Name { get; set; }

        public Dictionary<string, string> Attributes =>
            this.attributesCore;
    }
}

