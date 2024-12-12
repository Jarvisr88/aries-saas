namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TokenInfo
    {
        public TokenInfo(int index, System.Windows.Size size)
        {
            this.VisibleIndex = index;
            this.Size = size;
        }

        public int VisibleIndex { get; private set; }

        public System.Windows.Size Size { get; private set; }
    }
}

