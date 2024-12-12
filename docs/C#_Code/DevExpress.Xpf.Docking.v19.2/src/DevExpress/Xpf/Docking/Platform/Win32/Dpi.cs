namespace DevExpress.Xpf.Docking.Platform.Win32
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Dpi
    {
        public static readonly Dpi Default;
        public Dpi(int x, int y)
        {
            this = new Dpi();
            this.X = x;
            this.Y = y;
            this.ScaleX = ((float) this.X) / 96f;
            this.ScaleY = ((float) this.Y) / 96f;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public double ScaleX { get; private set; }
        public double ScaleY { get; private set; }
        public bool Equals(Dpi other) => 
            this == other;

        public override bool Equals(object other) => 
            (other != null) ? ((other is Dpi) && (this == ((Dpi) other))) : false;

        public override int GetHashCode() => 
            HashCodeHelper.Calculate(this.X, this.Y);

        public static bool operator ==(Dpi dpi1, Dpi dpi2) => 
            (dpi1.X == dpi2.X) && (dpi1.Y == dpi2.Y);

        public static bool operator !=(Dpi dpi1, Dpi dpi2) => 
            !(dpi1 == dpi2);

        static Dpi()
        {
            Default = new Dpi(0x60, 0x60);
        }
    }
}

