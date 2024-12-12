namespace DevExpress.Text.Fonts
{
    using System;
    using System.Runtime.CompilerServices;

    public class DXCharRange
    {
        public DXCharRange(int start, int end)
        {
            this.<Start>k__BackingField = start;
            this.<End>k__BackingField = end;
        }

        public int Start { get; }

        public int End { get; }
    }
}

