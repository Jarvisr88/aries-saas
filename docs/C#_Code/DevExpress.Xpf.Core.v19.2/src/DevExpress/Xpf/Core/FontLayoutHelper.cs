namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class FontLayoutHelper
    {
        private string[] FontProp;
        private Dictionary<string, object> FontValues;

        public FontLayoutHelper()
        {
            this.FontProp = new string[] { "FontFamily", "FontSize", "FontStretch", "FontStyle", "FontWeight", "Foreground" };
            this.FontValues = new Dictionary<string, object>();
        }

        public FontLayoutHelper(object cc)
        {
            this.FontProp = new string[] { "FontFamily", "FontSize", "FontStretch", "FontStyle", "FontWeight", "Foreground" };
            this.FontValues = new Dictionary<string, object>();
            this.Assign(cc);
        }

        private void Assign(object cc)
        {
            if (cc != null)
            {
                foreach (PropertyInfo info in cc.GetType().GetProperties())
                {
                    if (this.FontProp.Contains<string>(info.Name))
                    {
                        this.FontValues.Add(info.Name, info.GetValue(cc, null));
                    }
                }
            }
        }

        public void SetFont(object tb)
        {
            if (tb != null)
            {
                foreach (PropertyInfo info in tb.GetType().GetProperties())
                {
                    if (this.FontValues.ContainsKey(info.Name))
                    {
                        tb.GetType().GetProperty(info.Name).SetValue(tb, this.FontValues[info.Name], null);
                    }
                }
            }
        }
    }
}

