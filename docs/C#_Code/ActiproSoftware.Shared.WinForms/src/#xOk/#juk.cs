namespace #xOk
{
    using #H;
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    internal struct #juk
    {
        private ulong #OVc;
        private const ulong #ztk = 0x80000000UL;
        private const int #Atk = 0;
        private const ulong #Btk = 0xfffUL;
        private const int #Ctk = 12;
        private const ulong #Dtk = 0xffUL;
        private const int #Etk = 20;
        private const ulong #Ftk = 0xffUL;
        private const ulong #Gtk = 0x40000000UL;
        private const ulong #Htk = 0x20000000UL;
        private const int #Itk = 0x30;
        private const ulong #Jtk = 0xfffUL;
        private const int #Ktk = 60;
        private const ulong #Ltk = 7UL;
        private const ulong #Mtk = 9223372036854775808UL;
        private const int #Ntk = 0x20;
        private const ulong #Otk = 0xfffUL;
        private const int #Ptk = 0x2c;
        private const ulong #Qtk = 7UL;
        private const ulong #Rtk = 0x800000000000UL;
        private const ulong #Stk = 0xeffff000UL;
        public const int #Ttk = -1;
        public #juk Clone() => 
            new #juk { #OVc = this.#OVc };

        public override bool Equals(object #QOd) => 
            (#QOd is #juk) && (this.#OVc == ((#juk) #QOd).#OVc);

        public int FontFamilyIndex
        {
            get => 
                (int) ((this.#OVc >> 12) & ((ulong) 0xffL));
            set
            {
                ulong num = (ulong) ((((ushort) ((value > 0xffL) ? 0 : value)) & 0xffL) << 12);
                ulong num2 = 0xff000UL;
                this.#OVc = (~num2 & this.#OVc) | num;
            }
        }
        public int FontSizeIndex
        {
            get => 
                (int) ((this.#OVc >> 20) & ((ulong) 0xffL));
            set
            {
                ulong num = (ulong) ((((ushort) ((value > 0xffL) ? 0 : value)) & 0xffL) << 20);
                ulong num2 = 0xff00000UL;
                this.#OVc = (~num2 & this.#OVc) | num;
            }
        }
        public int ForegroundIndex
        {
            get => 
                (int) (this.#OVc & ((ulong) 0xfffL));
            set
            {
                ulong num = (ulong) (((value > 0xfffL) ? ((ushort) 0) : ((ushort) value)) & 0xfffL);
                ulong num2 = 0xfffUL;
                this.#OVc = (~num2 & this.#OVc) | num;
            }
        }
        public override int GetHashCode() => 
            this.#OVc.GetHashCode();

        public bool IsBold
        {
            get => 
                (this.#OVc & 0x80000000UL) == 0x80000000UL;
            set
            {
                if (value)
                {
                    this.#OVc |= 0x80000000UL;
                }
                else
                {
                    this.#OVc &= 18446744071562067967UL;
                }
            }
        }
        public bool IsItalic
        {
            get => 
                (this.#OVc & ((ulong) 0x40000000L)) == 0x40000000L;
            set
            {
                if (value)
                {
                    this.#OVc |= (ulong) 0x40000000L;
                }
                else
                {
                    this.#OVc &= 18446744072635809791UL;
                }
            }
        }
        public bool IsSpacer
        {
            get => 
                (this.#OVc & ((ulong) 0x20000000L)) == 0x20000000L;
            set
            {
                if (value)
                {
                    this.#OVc |= (ulong) 0x20000000L;
                }
                else
                {
                    this.#OVc &= 18446744073172680703UL;
                }
            }
        }
        public int StrikethroughBrushIndex
        {
            get => 
                ((int) ((this.#OVc >> 0x30) & ((ulong) 0xfffL))) - 1;
            set
            {
                value++;
                ulong num = (ulong) ((((ushort) ((value > 0xfffL) ? 0 : value)) & 0xfffL) << 0x30);
                ulong num2 = 0xfff000000000000UL;
                this.#OVc = (~num2 & this.#OVc) | num;
            }
        }
        public LineKind StrikethroughKind
        {
            get => 
                (LineKind) ((int) ((this.#OVc >> 60) & ((ulong) 7L)));
            set
            {
                ulong num = (ulong) ((((byte) ((((long) value) > 7L) ? 0 : value)) & 7L) << 60);
                ulong num2 = 0x7000000000000000UL;
                this.#OVc = (~num2 & this.#OVc) | num;
            }
        }
        public TextLineWeight StrikethroughWeight
        {
            get => 
                ((this.#OVc & 9223372036854775808UL) == 9223372036854775808UL) ? TextLineWeight.Double : TextLineWeight.Single;
            set
            {
                if (value == TextLineWeight.Double)
                {
                    this.#OVc |= 9223372036854775808UL;
                }
                else
                {
                    this.#OVc &= (ulong) 0x7fffffffffffffffL;
                }
            }
        }
        public #juk #cuk() => 
            new #juk { #OVc = this.#OVc & 0xeffff000UL };

        public override string ToString()
        {
            bool flag = true;
            StringBuilder builder = new StringBuilder(#G.#eg(0xc18));
            if (this.ForegroundIndex > 0)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.AppendFormat(#G.#eg(0xc36), this.ForegroundIndex);
            }
            if (this.FontFamilyIndex > 0)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.AppendFormat(#G.#eg(0xc53), this.FontFamilyIndex);
            }
            if (this.FontSizeIndex > 0)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.AppendFormat(#G.#eg(0xc70), this.FontSizeIndex);
            }
            if (this.IsBold)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.Append(#G.#eg(0xc89));
            }
            if (this.IsItalic)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.Append(#G.#eg(0xc92));
            }
            if (this.StrikethroughKind != LineKind.None)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.AppendFormat(#G.#eg(0xc9b), this.StrikethroughKind, this.StrikethroughWeight, this.StrikethroughBrushIndex);
            }
            if (this.UnderlineKind != LineKind.None)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.AppendFormat(#G.#eg(0xcd0), this.UnderlineKind, this.UnderlineWeight, this.UnderlineBrushIndex);
            }
            if (this.IsSpacer)
            {
                if (!flag)
                {
                    builder.Append(#G.#eg(0xc31));
                }
                flag = false;
                builder.Append(#G.#eg(0xcfd));
            }
            if (flag)
            {
                builder.Append(#G.#eg(0xd06));
            }
            builder.Append(#G.#eg(0xd17));
            builder.Append(this.#OVc.ToString(#G.#eg(0xd20), CultureInfo.InvariantCulture));
            return builder.ToString();
        }

        public int UnderlineBrushIndex
        {
            get => 
                ((int) ((this.#OVc >> 0x20) & ((ulong) 0xfffL))) - 1;
            set
            {
                value++;
                ulong num = (ulong) ((((ushort) ((value > 0xfffL) ? 0 : value)) & 0xfffL) << 0x20);
                ulong num2 = 0xfff00000000UL;
                this.#OVc = (~num2 & this.#OVc) | num;
            }
        }
        public LineKind UnderlineKind
        {
            get => 
                (LineKind) ((int) ((this.#OVc >> 0x2c) & ((ulong) 7L)));
            set
            {
                ulong num = (ulong) ((((byte) ((((long) value) > 7L) ? 0 : value)) & 7L) << 0x2c);
                ulong num2 = 0x700000000000UL;
                this.#OVc = (~num2 & this.#OVc) | num;
            }
        }
        public TextLineWeight UnderlineWeight
        {
            get => 
                ((this.#OVc & ((ulong) 0x800000000000L)) == 0x800000000000L) ? TextLineWeight.Double : TextLineWeight.Single;
            set
            {
                if (value == TextLineWeight.Double)
                {
                    this.#OVc |= (ulong) 0x800000000000L;
                }
                else
                {
                    this.#OVc &= 18446603336221196287UL;
                }
            }
        }
        public static bool operator ==(#juk #1n, #juk #3n) => 
            #1n.Equals(#3n);

        public static bool operator !=(#juk #1n, #juk #3n) => 
            !#1n.Equals(#3n);
    }
}

