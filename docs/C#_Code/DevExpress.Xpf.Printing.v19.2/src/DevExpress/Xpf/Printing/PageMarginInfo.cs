namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class PageMarginInfo : ImmutableObject
    {
        public PageMarginInfo(string displayName, System.Drawing.Printing.Margins margins)
        {
            this.DisplayName = displayName;
            this.Margins = margins;
        }

        public override bool Equals(object obj)
        {
            Func<bool> fallback = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Func<bool> local1 = <>c.<>9__10_1;
                fallback = <>c.<>9__10_1 = () => false;
            }
            return (obj as PageMarginInfo).Return<PageMarginInfo, bool>(x => (x.Margins == this.Margins), fallback);
        }

        public override int GetHashCode() => 
            this.Margins.GetHashCode() ^ typeof(PageMarginInfo).GetHashCode();

        public static bool operator ==(PageMarginInfo m1, PageMarginInfo m2) => 
            (m1 == null) ? ReferenceEquals(m2, null) : m1.Equals(m2);

        public static bool operator !=(PageMarginInfo m1, PageMarginInfo m2) => 
            !(m1 == m2);

        public string DisplayName { get; private set; }

        public System.Drawing.Printing.Margins Margins { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageMarginInfo.<>c <>9 = new PageMarginInfo.<>c();
            public static Func<bool> <>9__10_1;

            internal bool <Equals>b__10_1() => 
                false;
        }
    }
}

