namespace #xOk
{
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct #ytk : ITextBounds
    {
        private int #Zcc;
        private bool #8p;
        private int #0cc;
        private int #Zn;
        private int #0n;
        public #ytk(Rectangle bounds, bool isRightToLeft)
        {
            this.#Zn = bounds.X;
            this.#0n = bounds.Y;
            this.#0cc = bounds.Width;
            this.#Zcc = bounds.Height;
            this.#8p = isRightToLeft;
        }

        public int Bottom =>
            this.#0n + this.#Zcc;
        public override bool Equals(object #QOd)
        {
            ITextBounds bounds;
            if (2 == 0)
            {
                ITextBounds local1 = #QOd as ITextBounds;
            }
            else
            {
                bounds = #QOd as ITextBounds;
            }
            return ((bounds != null) ? ((this.#Zn == bounds.X) && ((this.#0n == bounds.Y) && ((this.#0cc == bounds.Width) && ((this.#Zcc == bounds.Height) && (this.#8p == bounds.IsRightToLeft))))) : false);
        }

        public override int GetHashCode() => 
            ((this.#Zn.GetHashCode() ^ this.#0n.GetHashCode()) ^ this.#0cc.GetHashCode()) ^ this.#Zcc.GetHashCode();

        public int Height =>
            this.#Zcc;
        public bool IsRightToLeft =>
            this.#8p;
        public int Left =>
            this.#Zn;
        public Rectangle Rect =>
            new Rectangle(this.#Zn, this.#0n, this.#0cc, this.#Zcc);
        public int Right =>
            this.#Zn + this.#0cc;
        public int Top =>
            this.#0n;
        public override string ToString()
        {
            Rectangle rect = this.Rect;
            return rect.ToString();
        }

        public int Width =>
            this.#0cc;
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="X")]
        public int X =>
            this.#Zn;
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="X")]
        public int Y =>
            this.#0n;
        public static bool operator ==(#ytk #1n, ITextBounds #3n) => 
            #1n.Equals(#3n);

        public static bool operator !=(#ytk #1n, ITextBounds #3n) => 
            #1n != #3n;
    }
}

