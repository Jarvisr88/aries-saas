namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections.Generic;

    public class PatternElement
    {
        public int height;
        public List<int> pattern;
        public bool startBarBlack;

        public PatternElement();
        public PatternElement(int height, List<int> pattern, bool startBarBlack);
    }
}

