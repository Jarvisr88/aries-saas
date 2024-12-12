namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class StyleSheet
    {
        public void AddStyle(string style, string value)
        {
            if (!this.Styles.ContainsKey(style))
            {
                this.Styles.Add(style, value);
            }
            else
            {
                this.Styles[style] = value;
            }
        }

        public override int GetHashCode()
        {
            int nullHash = HashCodeHelper.GetNullHash();
            foreach (KeyValuePair<string, string> pair in this.Styles)
            {
                nullHash = HashCodeHelper.CombineCharList(HashCodeHelper.CombineCharList(nullHash, pair.Key.ToCharArray()), pair.Value.ToCharArray());
            }
            return nullHash;
        }

        public Dictionary<string, string> Styles { get; } = new Dictionary<string, string>()
    }
}

