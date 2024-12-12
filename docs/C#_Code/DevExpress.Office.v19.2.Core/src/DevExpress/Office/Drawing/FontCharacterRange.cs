namespace DevExpress.Office.Drawing
{
    using System;

    public class FontCharacterRange
    {
        private int low;
        private int hi;

        public FontCharacterRange(int low, int hi)
        {
            this.low = low;
            this.hi = hi;
        }

        public int Low =>
            this.low;

        public int Hi =>
            this.hi;
    }
}

