namespace #xOk
{
    using #H;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class #Bi
    {
        public const int #N3k = 1;
        public const int #O3k = 0;
        public const uint #Rhk = 0x8007000e;
        public const uint #Hhk = 0x8000000a;
        public const uint #Fhk = 0;
        public const int #kQk = 1;
        public const int #lQk = 2;
        public const int #mQk = 1;
        public const int #nQk = 5;
        public const int #oQk = 8;
        public const int #pQk = 0;
        public const int #qQk = 1;
        public const int #rQk = 2;
        public const int #sQk = 3;
        public const int #Eue = 1;
        public const uint #tQk = 0x80040200;

        [DllImport("Gdi32.dll", EntryPoint="CreateSolidBrush", CharSet=CharSet.Auto)]
        public static extern IntPtr #06e(int #43f);
        [DllImport("User32.dll", EntryPoint="FillRect", CharSet=CharSet.Auto)]
        public static extern void #26e(IntPtr #8qe, #Fi #n0f, IntPtr #83f);
        [DllImport("usp10.dll", EntryPoint="ScriptPlaceOpenType")]
        public static extern uint #2Qk([In] IntPtr #8qe, [In, Out] ref IntPtr #QQk, [In, Out] ref #pTk #NQk, [In, MarshalAs(UnmanagedType.Struct)] #iTk #3Qk, [In, MarshalAs(UnmanagedType.Struct)] #iTk #4Qk, [In] int[] #5Qk, [In] #WTk[] #6Qk, [In] int #7Qk, [In, MarshalAs(UnmanagedType.LPWStr)] string #LQk, [In] ushort[] #8Qk, [In] #sTk[] #9Qk, [In] int #MQk, [In] ushort[] #aRk, [In] #DTk[] #bRk, [In] int #cRk, [In, Out] int[] #dRk, [In, Out] #eTk[] #eRk, [In, Out] ref #bTk #fRk);
        [DllImport("Gdi32.dll", EntryPoint="LineTo", CharSet=CharSet.Auto)]
        public static extern bool #36e(IntPtr #8qe, int #Zn, int #0n);
        [DllImport("Gdi32.dll", EntryPoint="MoveToEx", CharSet=CharSet.Auto)]
        public static extern bool #46e(IntPtr #8qe, int #Zn, int #0n, IntPtr #93f);
        [DllImport("Gdi32.dll", EntryPoint="DeleteObject")]
        public static extern bool #5oe(IntPtr #YZf);
        [DllImport("usp10.dll", EntryPoint="ScriptLayout")]
        public static extern uint #66k([In] int #76k, [In] byte[] #86k, [In, Out] int[] #96k, [In, Out] int[] #a7k);
        [DllImport("Gdi32.dll", EntryPoint="SetWorldTransform")]
        public static extern bool #86e(IntPtr #8qe, #h1b #c4f);
        [DllImport("Gdi32.dll", EntryPoint="SelectClipRgn")]
        public static extern int #ape(IntPtr #8qe, IntPtr #vJf);
        [DllImport("Gdi32.dll", EntryPoint="EndPath")]
        public static extern bool #AQk(IntPtr #8qe);
        [DllImport("Gdi32.dll", EntryPoint="SelectObject")]
        public static extern IntPtr #bpe(IntPtr #8qe, IntPtr #53f);
        [DllImport("Gdi32.dll", EntryPoint="FillPath")]
        public static extern bool #BQk(IntPtr #8qe);
        [DllImport("Gdi32.dll", EntryPoint="SetTextColor")]
        public static extern int #Bxe(IntPtr #8qe, int #eUb);
        [DllImport("Gdi32.dll", EntryPoint="GetStockObject", CharSet=CharSet.Auto)]
        public static extern IntPtr #CQk(int #DQk);
        [DllImport("Gdi32.dll", EntryPoint="SetBkMode")]
        public static extern int #Cxe(IntPtr #8qe, int #ib);
        [DllImport("Gdi32.dll", EntryPoint="GetTextExtentPoint32W")]
        public static extern int #EQk(IntPtr #8qe, [MarshalAs(UnmanagedType.LPWStr)] string #ICf, int #Hvf, ref Size #xe);
        [DllImport("Gdi32.dll", EntryPoint="ModifyWorldTransform")]
        public static extern bool #FQk(IntPtr #8qe, IntPtr #c4f, int #ib);
        [DllImport("Gdi32.dll", EntryPoint="RoundRect", CharSet=CharSet.Auto)]
        public static extern void #GQk(IntPtr #8qe, int #wQk, int #xQk, int #yQk, int #zQk, int #0cc, int #Zcc);
        [DllImport("usp10.dll", EntryPoint="ScriptShapeOpenType")]
        public static extern uint #gRk([In] IntPtr #8qe, [In, Out] ref IntPtr #QQk, [In, Out] ref #pTk #NQk, [In, MarshalAs(UnmanagedType.Struct)] #iTk #3Qk, [In, MarshalAs(UnmanagedType.Struct)] #iTk #4Qk, [In] int[] #5Qk, [In] #WTk[] #6Qk, [In] int #7Qk, [In, MarshalAs(UnmanagedType.LPWStr)] string #LQk, [In] int #MQk, [In] int #hRk, [In, Out] ushort[] #8Qk, [In, Out] #sTk[] #9Qk, [In, Out] ushort[] #iRk, [In, Out] #DTk[] #jRk, out int #kRk);
        [DllImport("Gdi32.dll", EntryPoint="SetGraphicsMode")]
        public static extern int #HQk(IntPtr #8qe, int #IQk);
        [DllImport("Gdi32.dll", EntryPoint="StrokePath")]
        public static extern bool #JQk(IntPtr #8qe);
        [DllImport("usp10.dll", EntryPoint="ScriptBreak")]
        public static extern uint #KQk([In, MarshalAs(UnmanagedType.LPWStr)] string #LQk, [In] int #MQk, [In] ref #pTk #NQk, [In, Out] #LTk[] #OQk);
        [DllImport("usp10.dll", EntryPoint="ScriptTextOut")]
        public static extern uint #lRk([In] IntPtr #8qe, [In, Out] ref IntPtr #QQk, [In] int #Zn, [In] int #0n, [In] uint #mRk, [In] IntPtr #PLg, [In] ref #pTk #NQk, [In] IntPtr #nRk, [In] int #oRk, [In] ref ushort #aRk, [In] int #cRk, [In] ref int #dRk, [In] IntPtr #pRk, [In] ref #eTk #eRk);
        [DllImport("Gdi32.dll", EntryPoint="Rectangle", CharSet=CharSet.Auto)]
        public static extern void #OX(IntPtr #8qe, int #wQk, int #xQk, int #yQk, int #zQk);
        [DllImport("Gdi32.dll", EntryPoint="GdiAlphaBlend")]
        public static extern bool #P3k(IntPtr #Q3k, int #R3k, int #S3k, int #T3k, int #U3k, IntPtr #V3k, int #W3k, int #X3k, int #Y3k, int #Z3k, #03k #03k);
        [DllImport("usp10.dll", EntryPoint="ScriptFreeCache")]
        public static extern uint #PQk(ref IntPtr #QQk);
        [DllImport("Gdi32.dll", EntryPoint="Ellipse", CharSet=CharSet.Auto)]
        public static extern void #PX(IntPtr #8qe, int #wQk, int #xQk, int #yQk, int #zQk);
        [DllImport("usp10.dll", EntryPoint="ScriptGetFontProperties")]
        public static extern uint #RQk([In] IntPtr #8qe, [In, Out] ref IntPtr #QQk, [MarshalAs(UnmanagedType.Struct)] out #ATk #SQk);
        [DllImport("usp10.dll", EntryPoint="ScriptItemizeOpenType")]
        public static extern uint #TQk([In, MarshalAs(UnmanagedType.LPWStr)] string #UQk, [In] int #VQk, [In] int #WQk, [In] ref #tTk #XQk, [In] ref #OTk #YQk, [In, Out] #FTk[] #ZQk, [In, Out] #iTk[] #0Qk, out int #1Qk);
        [DllImport("Gdi32.dll", EntryPoint="BeginPath")]
        public static extern bool #uQk(IntPtr #8qe);
        [DllImport("Gdi32.dll", EntryPoint="CloseFigure")]
        public static extern bool #vQk(IntPtr #8qe);
        [DllImport("Gdi32.dll", EntryPoint="BitBlt")]
        public static extern bool #xxe(IntPtr #8qe, int #13k, int #23k, int #d0f, int #e0f, IntPtr #V3k, int #33k, int #43k, int #i0f);
        [DllImport("Gdi32.dll", EntryPoint="CreateRectRgn", CharSet=CharSet.Auto)]
        public static extern IntPtr #Y6e(int #Z3f, int #03f, int #13f, int #23f);
        [DllImport("Gdi32.dll", EntryPoint="CreatePen", CharSet=CharSet.Auto)]
        public static extern IntPtr #Z6e(int #33f, int #d0f, int #43f);
        [DllImport("Gdi32.dll", EntryPoint="CreateCompatibleDC")]
        public static extern IntPtr #zxe(IntPtr #8qe);

        [StructLayout(LayoutKind.Sequential)]
        public struct #03k
        {
            public byte #53k;
            public byte #63k;
            public byte #73k;
            public byte #83k;
            public #03k(byte op, byte flags, byte alpha, byte format)
            {
                this.#53k = op;
                this.#63k = flags;
                this.#73k = alpha;
                this.#83k = format;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #ATk
        {
            public int #uTk;
            public short #vTk;
            public short #wTk;
            public short #xTk;
            public short #yTk;
            public int #zTk;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #bTk
        {
            public int #8Sk;
            public int #9Sk;
            public int #aTk;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #DTk
        {
            public #Bi.#TTk #BTk;
            public short #CTk;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #eTk
        {
            public int #cTk;
            public int #dTk;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class #Fi
        {
            public int #1n;
            public int #2n;
            public int #3n;
            public int #4n;
            public #Fi(Rectangle rect)
            {
                this.#4n = rect.Bottom;
                this.#1n = rect.Left;
                this.#3n = rect.Right;
                this.#2n = rect.Top;
            }

            public #Fi(RectangleF rect)
            {
                this.#4n = (int) Math.Ceiling((double) rect.Bottom);
                this.#1n = (int) Math.Floor((double) rect.Left);
                this.#3n = (int) Math.Ceiling((double) rect.Right);
                this.#2n = (int) Math.Floor((double) rect.Top);
            }

            public #Fi(int left, int top, int right, int bottom)
            {
                this.#4n = bottom;
                this.#1n = left;
                this.#3n = right;
                this.#2n = top;
            }

            public #Fi(float left, float top, float right, float bottom)
            {
                this.#4n = (int) bottom;
                this.#1n = (int) left;
                this.#3n = (int) right;
                this.#2n = (int) top;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #FTk
        {
            public int #ETk;
            public #Bi.#pTk #lAf;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class #h1b
        {
            public float #pYe;
            public float #qYe;
            public float #rYe;
            public float #sYe;
            public float #tYe;
            public float #uYe;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #hTk
        {
            public #Bi.#iTk #fTk;
            public int #gTk;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #iTk
        {
            public uint #kob;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #LTk
        {
            public byte #jTk;
            private const byte #GTk = 1;
            private const byte #HTk = 2;
            private const byte #ITk = 4;
            public bool IsCharStop =>
                (this.#jTk & 4) != 0;
            public bool IsWhiteSpace =>
                (this.#jTk & 2) != 0;
            public bool IsSoftBreak =>
                (this.#jTk & 1) != 0;
            public override string ToString()
            {
                bool flag = true;
                StringBuilder builder = new StringBuilder(#G.#eg(0x3122));
                if (this.IsCharStop)
                {
                    if (!flag)
                    {
                        builder.Append(#G.#eg(0xc31));
                    }
                    flag = false;
                    builder.Append(#G.#eg(0x313b));
                }
                if (this.IsWhiteSpace)
                {
                    if (!flag)
                    {
                        builder.Append(#G.#eg(0xc31));
                    }
                    flag = false;
                    builder.Append(#G.#eg(0x3144));
                }
                if (this.IsSoftBreak)
                {
                    if (!flag)
                    {
                        builder.Append(#G.#eg(0xc31));
                    }
                    flag = false;
                    builder.Append(#G.#eg(0x3155));
                }
                if (flag)
                {
                    builder.Append(#G.#eg(0x2f11));
                }
                builder.Append(#G.#eg(0x3166));
                return builder.ToString();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #OTk
        {
            public ushort #jTk;
            private const ushort #MTk = 0x1f;
            public byte BidiLevel =>
                (byte) (this.#jTk & 0x1f);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #pTk
        {
            public ushort #jTk;
            public #Bi.#OTk #hUb;
            private const ushort #kTk = 0x3ff;
            private const ushort #lTk = 0x400;
            public int Script
            {
                get => 
                    this.#jTk & 0x3ff;
                set => 
                    this.#jTk = (ushort) ((this.#jTk & -1024) | value);
            }
            public bool IsRightToLeft =>
                (this.#jTk & 0x400) != 0;
            public void #oTk()
            {
                this.Script = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #sTk
        {
            private #xOk.#Bi.#sTk.#ED #ED;
            private static bool #eg(#xOk.#Bi.#sTk.#ED #ui, #xOk.#Bi.#sTk.#ED #ED) => 
                (#ui & #ED) != ((#xOk.#Bi.#sTk.#ED) 0);

            private static void #gMc(bool #Ld, ref #xOk.#Bi.#sTk.#ED #ui, #xOk.#Bi.#sTk.#ED #ED)
            {
                #ui = #Ld ? (#ui | #ED) : (#ui & ((ushort) ~#ED));
            }

            public bool fCanGlyphAlone
            {
                get => 
                    #eg(this.#ED, #xOk.#Bi.#sTk.#ED.#hUk);
                set => 
                    #gMc(value, ref this.#ED, #xOk.#Bi.#sTk.#ED.#hUk);
            }
            private enum #ED : ushort
            {
                #hUk = 1
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #tTk
        {
            public uint #jTk;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #TTk
        {
            private const ushort #PTk = 0x10;
            private const ushort #QTk = 0x40;
            public ushort #jTk;
            public bool IsClusterStart =>
                (this.#jTk & 0x10) != 0;
            public bool IsZeroWidth =>
                (this.#jTk & 0x40) != 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct #WTk
        {
            public #Bi.#hTk[] #UTk;
            public int #VTk;
        }
    }
}

