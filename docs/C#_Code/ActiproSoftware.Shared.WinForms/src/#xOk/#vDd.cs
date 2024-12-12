namespace #xOk
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class #vDd : DisposableObject
    {
        private int #kJ;
        private IntPtr #YVe;

        public #vDd(string fontFamilyName, float fontSize, FontStyle fontStyle)
        {
            System.Drawing.Font font = new System.Drawing.Font(fontFamilyName, fontSize, fontStyle);
            this.Font = font;
            this.#kJ = font.GetHashCode();
            FontFamily fontFamily = font.FontFamily;
            int emHeight = fontFamily.GetEmHeight(fontStyle);
            float num2 = font.Size / ((float) emHeight);
            int cellAscent = fontFamily.GetCellAscent(fontStyle);
            int cellDescent = fontFamily.GetCellDescent(fontStyle);
            int num5 = cellAscent + cellDescent;
            int lineSpacing = fontFamily.GetLineSpacing(fontStyle);
            this.Ascent = cellAscent * num2;
            this.Descent = cellDescent * num2;
            this.InternalLeading = (num5 - emHeight) * num2;
            this.ExternalLeading = (lineSpacing - num5) * num2;
            this.Height = font.Height;
            this.Baseline = this.Height - this.Descent;
        }

        protected override void Dispose(bool #Fee)
        {
            if (this.ScriptCache != IntPtr.Zero)
            {
                #Bi.#PQk(ref this.ScriptCache);
                this.ScriptCache = IntPtr.Zero;
            }
            if (this.#YVe != IntPtr.Zero)
            {
                #Bi.#5oe(this.#YVe);
                this.#YVe = IntPtr.Zero;
            }
            if (this.Font != null)
            {
                this.Font.Dispose();
                this.Font = null;
            }
        }

        public override bool Equals(object #QOd)
        {
            #vDd dd = #QOd as #vDd;
            return ((dd != null) && this.Font.Equals(dd.Font));
        }

        public override int GetHashCode() => 
            this.#kJ;

        public float Ascent { get; private set; }

        public float Baseline { get; private set; }

        public float Descent { get; private set; }

        public float ExternalLeading { get; private set; }

        public System.Drawing.Font Font { get; private set; }

        public IntPtr HFont
        {
            get
            {
                if (this.#YVe == IntPtr.Zero)
                {
                    this.#YVe = this.Font.ToHfont();
                }
                return this.#YVe;
            }
        }

        public int Height { get; private set; }

        public float InternalLeading { get; private set; }

        public IntPtr ScriptCache { get; set; }
    }
}

