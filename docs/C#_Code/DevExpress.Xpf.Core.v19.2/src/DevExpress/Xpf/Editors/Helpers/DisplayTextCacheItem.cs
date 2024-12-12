namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class DisplayTextCacheItem
    {
        protected bool Equals(DisplayTextCacheItem other) => 
            (this.StartIndex == other.StartIndex) && (string.Equals(this.DisplayText, other.DisplayText) && ((this.AutoComplete == other.AutoComplete) && ((this.SearchNext == other.SearchNext) && (this.IgnoreStartIndex == other.IgnoreStartIndex))));

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((DisplayTextCacheItem) obj) : false) : true) : false;

        public override int GetHashCode() => 
            (((((((this.StartIndex * 0x18d) ^ ((this.DisplayText != null) ? this.DisplayText.GetHashCode() : 0)) * 0x18d) ^ this.AutoComplete.GetHashCode()) * 0x18d) ^ this.SearchNext.GetHashCode()) * 0x18d) ^ this.IgnoreStartIndex.GetHashCode();

        public int StartIndex { get; set; }

        public string DisplayText { get; set; }

        public bool AutoComplete { get; set; }

        public bool SearchNext { get; set; }

        public bool IgnoreStartIndex { get; set; }
    }
}

