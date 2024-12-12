namespace #xOk
{
    using #H;
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal class #zOk : DisposableObject
    {
        private Rectangle #ahh;
        private Graphics #nYf;
        private IntPtr #8qe;
        private IntPtr #0Zf;
        private Color #qRk = Color.Empty;
        private #vDd #rRk;
        private IntPtr #sRk;
        private float #Ure;
        private static FontFamilyMapRepository #tRk;

        public #ZTk #1Rk(ref IntPtr #PRk, string #HRk, int #IRk, bool #2Rk, ref #Bi.#pTk #JRk, #Bi.#iTk #WRk, out ushort[] #XRk, out #Bi.#sTk[] #YRk, out ushort[] #ZRk, out #Bi.#DTk[] #0Rk)
        {
            int num = ((#IRk * 3) / 2) + 0x10;
            #XRk = new ushort[#IRk];
            #YRk = new #Bi.#sTk[#IRk];
            #ZRk = new ushort[num];
            #0Rk = new #Bi.#DTk[num];
            if (#2Rk)
            {
                #JRk.#oTk();
            }
            while (true)
            {
                uint num2 = #Bi.#gRk(this.#8qe, ref #PRk, ref #JRk, #WRk, #WRk, null, null, 0, #HRk, #IRk, num, #XRk, #YRk, #ZRk, #0Rk, out num);
                if (num2 > 0x8000000a)
                {
                    if (num2 == 0x80040200)
                    {
                        return #ZTk.#YTk;
                    }
                    if (num2 == 0x8007000e)
                    {
                        num *= 2;
                        Array.Resize<ushort>(ref #ZRk, num);
                        Array.Resize<#Bi.#DTk>(ref #0Rk, num);
                        continue;
                    }
                }
                else
                {
                    if (num2 == 0)
                    {
                        #Bi.#ATk tk;
                        Array.Resize<ushort>(ref #ZRk, num);
                        Array.Resize<#Bi.#DTk>(ref #0Rk, num);
                        #Bi.#RQk(this.#8qe, ref #PRk, out tk);
                        for (int i = 0; i < num; i++)
                        {
                            if (#ZRk[i] == tk.#wTk)
                            {
                                return #ZTk.#YTk;
                            }
                        }
                        return #ZTk.#8Tc;
                    }
                    if (num2 == 0x8000000a)
                    {
                        return #ZTk.#XTk;
                    }
                }
                return #ZTk.#XTk;
            }
        }

        private void #ARk()
        {
            if (this.#8qe != IntPtr.Zero)
            {
                if (this.#Ure > 1f)
                {
                    this.#BRk();
                }
                if (this.#sRk != IntPtr.Zero)
                {
                    #Bi.#bpe(this.#8qe, this.#sRk);
                }
                #Bi.#ape(this.#8qe, IntPtr.Zero);
                if (this.#0Zf != IntPtr.Zero)
                {
                    #Bi.#5oe(this.#0Zf);
                }
                this.#nYf.ReleaseHdc();
                this.#8qe = IntPtr.Zero;
            }
        }

        private void #BRk()
        {
            bool local1 = #Bi.#FQk(this.#8qe, IntPtr.Zero, 1);
            #Bi.#HQk(this.#8qe, 1);
        }

        public void #Bxe(Color #eUb)
        {
            if (this.#qRk != #eUb)
            {
                this.#qRk = #eUb;
                #Bi.#Bxe(this.#8qe, ColorTranslator.ToWin32(#eUb));
            }
        }

        private void #CRk(float #Ure)
        {
            int local1 = #Bi.#HQk(this.#8qe, 2);
            #Bi.#h1b #hb1 = new #Bi.#h1b();
            #Bi.#h1b #hb2 = new #Bi.#h1b();
            #hb2.#pYe = #Ure;
            #Bi.#h1b local2 = #hb2;
            local2.#qYe = 0f;
            local2.#rYe = 0f;
            local2.#sYe = #Ure;
            local2.#tYe = 0f;
            local2.#uYe = 0f;
            #Bi.#h1b #hb = local2;
            #Bi.#86e(this.#8qe, #hb);
        }

        public IEnumerable<string> #DRk(#vDd #vDd, char #cvf)
        {
            #93k #k1 = new #93k(-2);
            #93k #k2 = new #93k(-2);
            #k2.#6Sk = #cvf;
            return #k2;
        }

        public void #ERk(Rectangle #ahh)
        {
            #ahh = new Rectangle((int) (#ahh.X * this.#Ure), (int) (#ahh.Y * this.#Ure), (int) (#ahh.Width * this.#Ure), (int) (#ahh.Height * this.#Ure));
            IntPtr ptr = #Bi.#Y6e(#ahh.X, #ahh.Y, #ahh.Right, #ahh.Bottom);
            #Bi.#ape(this.#8qe, ptr);
            if (this.#0Zf != IntPtr.Zero)
            {
                #Bi.#5oe(this.#0Zf);
            }
            this.#0Zf = ptr;
        }

        public void #FRk(#vDd #vDd)
        {
            if (!ReferenceEquals(this.#rRk, #vDd))
            {
                this.#rRk = #vDd;
                IntPtr ptr = #Bi.#bpe(this.#8qe, #vDd.HFont);
                if (this.#sRk == IntPtr.Zero)
                {
                    this.#sRk = ptr;
                }
            }
        }

        public static bool #GRk(string #HRk, int #IRk, #Bi.#pTk #JRk, List<#Bi.#LTk> #KRk)
        {
            #Bi.#LTk[] tkArray;
            if (0 != 0)
            {
                #Bi.#LTk[] local1 = new #Bi.#LTk[#IRk];
            }
            else
            {
                tkArray = new #Bi.#LTk[#IRk];
            }
            #KRk.AddRange(tkArray);
            return (#Bi.#KQk(#HRk, #IRk, ref #JRk, tkArray) == 0);
        }

        public static bool #LRk(string #Hd, out #Bi.#FTk[] #MRk, out #Bi.#iTk[] #NRk)
        {
            int length;
            if (6 == 0)
            {
                int length = #Hd.Length;
            }
            else
            {
                length = #Hd.Length;
            }
            #Bi.#tTk tk = new #Bi.#tTk();
            #Bi.#OTk tk2 = new #Bi.#OTk();
            int newSize = 0x10;
            #MRk = null;
            #NRk = null;
            while (true)
            {
                int num3;
                Array.Resize<#Bi.#FTk>(ref #MRk, newSize);
                Array.Resize<#Bi.#iTk>(ref #NRk, newSize - 1);
                uint num4 = #Bi.#TQk(#Hd, length, newSize - 1, ref tk, ref tk2, #MRk, #NRk, out num3);
                if (num4 == 0)
                {
                    Array.Resize<#Bi.#FTk>(ref #MRk, num3 + 1);
                    Array.Resize<#Bi.#iTk>(ref #NRk, num3);
                    return true;
                }
                if (num4 != 0x8007000e)
                {
                    return false;
                }
                newSize *= 2;
            }
        }

        public bool #ORk(int #Zn, int #0n, ref IntPtr #PRk, #Bi.#pTk #JRk, int #QRk, int #RRk, ushort[] #SRk, int[] #TRk, #Bi.#eTk[] #URk) => 
            #Bi.#lRk(this.#8qe, ref #PRk, #Zn, #0n, 0, IntPtr.Zero, ref #JRk, IntPtr.Zero, 0, ref #SRk[#QRk], #RRk, ref #TRk[#QRk], IntPtr.Zero, ref #URk[#QRk]) == 0;

        public Size #r5e(string #Hd)
        {
            Size size = new Size();
            #Bi.#EQk(this.#8qe, #Hd, #Hd.Length, ref size);
            return size;
        }

        private void #uRk(Point #vRk, GraphicsPath #OK)
        {
            bool local1 = #Bi.#uQk(this.#8qe);
            for (int i = 0; i < #OK.PointCount; i++)
            {
                PointF tf = #OK.PathPoints[i];
                byte num1 = #OK.PathTypes[i];
                if ((num1 & 1) == 1)
                {
                    #Bi.#36e(this.#8qe, #vRk.X + ((int) tf.X), #vRk.Y + ((int) tf.Y));
                }
                else
                {
                    #Bi.#46e(this.#8qe, #vRk.X + ((int) tf.X), #vRk.Y + ((int) tf.Y), IntPtr.Zero);
                }
                if ((num1 & 0x80) == 0x80)
                {
                    #Bi.#vQk(this.#8qe);
                }
            }
            #Bi.#AQk(this.#8qe);
        }

        public bool #VRk(ref IntPtr #PRk, string #HRk, int #IRk, #Bi.#pTk #JRk, #Bi.#iTk #WRk, ushort[] #XRk, #Bi.#sTk[] #YRk, ushort[] #ZRk, #Bi.#DTk[] #0Rk, List<int> #TRk, List<#Bi.#eTk> #URk)
        {
            int length = #ZRk.Length;
            int[] collection = new int[length];
            #Bi.#eTk[] tkArray = new #Bi.#eTk[length];
            #Bi.#bTk tk = new #Bi.#bTk();
            #TRk.AddRange(collection);
            #URk.AddRange(tkArray);
            return (#Bi.#2Qk(this.#8qe, ref #PRk, ref #JRk, #WRk, #WRk, null, null, 0, #HRk, #XRk, #YRk, #IRk, #ZRk, #0Rk, length, collection, tkArray, ref tk) == 0);
        }

        private static float #wRk(Graphics #nYf, bool #xRk)
        {
            if (#nYf == null)
            {
                throw new ArgumentNullException(#G.#eg(0xb4f));
            }
            return (!#xRk ? 1f : (#nYf.DpiX / 100f));
        }

        public static void #xtk(IList<#USk> #b7k, bool #R6b, out int[] #c7k)
        {
            int count = #b7k.Count;
            #c7k = new int[count];
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i++)
            {
                #Bi.#pTk analysis = #b7k[i].Analysis;
                buffer[i] = analysis.#hUb.BidiLevel;
            }
            #Bi.#66k(count - (#R6b ? 1 : 0), buffer, #c7k, null);
            if (#R6b)
            {
                #c7k[count - 1] = count - 1;
            }
            bool flag = false;
            int index = 0;
            while (true)
            {
                if (index < count)
                {
                    if (#c7k[index] == index)
                    {
                        index++;
                        continue;
                    }
                    flag = true;
                }
                if (!flag)
                {
                    #c7k = null;
                }
                return;
            }
        }

        private static int #yRk(DashStyle #6Ue)
        {
            switch (#6Ue)
            {
                case DashStyle.Dash:
                    return 1;

                case DashStyle.Dot:
                    return 2;

                case DashStyle.DashDot:
                    return 3;
            }
            return 0;
        }

        private void #zRk()
        {
            if (this.#8qe == IntPtr.Zero)
            {
                RectangleF clipBounds = this.#nYf.ClipBounds;
                this.#ahh = new Rectangle((int) Math.Floor((double) clipBounds.X), (int) Math.Floor((double) clipBounds.Y), (int) Math.Ceiling((double) clipBounds.Width), (int) Math.Ceiling((double) clipBounds.Height));
                IntPtr hrgn = this.#nYf.Clip.GetHrgn(this.#nYf);
                this.#8qe = this.#nYf.GetHdc();
                if (this.#Ure > 1f)
                {
                    this.#CRk(this.#Ure);
                }
                #Bi.#Cxe(this.#8qe, 1);
                #Bi.#ape(this.#8qe, hrgn);
                #Bi.#5oe(hrgn);
            }
        }

        public #zOk(Graphics g, bool isForPrinter)
        {
            if (g == null)
            {
                throw new ArgumentNullException(#G.#eg(0xb4f));
            }
            this.#nYf = g;
            this.#Ure = #wRk(g, isForPrinter);
            this.#zRk();
        }

        protected override void Dispose(bool #Fee)
        {
            this.#ARk();
        }

        public void DrawEllipse(Rectangle #Bo, Color #eUb, DashStyle #6Ue, float #Nsk)
        {
            IntPtr ptr = #Bi.#Z6e(#yRk(#6Ue), (int) #Nsk, ColorTranslator.ToWin32(#eUb));
            IntPtr ptr2 = #Bi.#bpe(this.#8qe, ptr);
            IntPtr ptr3 = #Bi.#bpe(this.#8qe, #Bi.#CQk(5));
            #Bi.#PX(this.#8qe, #Bo.X, #Bo.Y, #Bo.Right, #Bo.Bottom);
            #Bi.#bpe(this.#8qe, ptr3);
            #Bi.#bpe(this.#8qe, ptr2);
            #Bi.#5oe(ptr);
        }

        public void DrawGeometry(Point #bVe, GraphicsPath #OK, Color #eUb, DashStyle #6Ue, float #Nsk)
        {
            IntPtr ptr = #Bi.#Z6e(#yRk(#6Ue), (int) #Nsk, ColorTranslator.ToWin32(#eUb));
            IntPtr ptr2 = #Bi.#bpe(this.#8qe, ptr);
            this.#uRk(#bVe, #OK);
            #Bi.#JQk(this.#8qe);
            #Bi.#bpe(this.#8qe, ptr2);
            #Bi.#5oe(ptr);
        }

        public void DrawImage(Point #bVe, Image #M0d)
        {
            if (#M0d != null)
            {
                using (Graphics graphics = Graphics.FromHdc(this.#8qe))
                {
                    graphics.DrawImage(#M0d, #bVe);
                }
            }
        }

        public void DrawLine(int #Id, int #iJf, int #Jd, int #jJf, Color #eUb, DashStyle #6Ue, float #Nsk)
        {
            IntPtr ptr = #Bi.#Z6e(#yRk(#6Ue), (int) #Nsk, ColorTranslator.ToWin32(#eUb));
            IntPtr ptr2 = #Bi.#bpe(this.#8qe, ptr);
            #Bi.#46e(this.#8qe, #Id, #iJf, IntPtr.Zero);
            #Bi.#36e(this.#8qe, #Jd, #jJf);
            #Bi.#bpe(this.#8qe, ptr2);
            #Bi.#5oe(ptr);
        }

        public void DrawRectangle(Rectangle #Bo, Color #eUb, DashStyle #6Ue, float #Nsk)
        {
            IntPtr ptr = #Bi.#Z6e(#yRk(#6Ue), (int) #Nsk, ColorTranslator.ToWin32(#eUb));
            IntPtr ptr2 = #Bi.#bpe(this.#8qe, ptr);
            IntPtr ptr3 = #Bi.#bpe(this.#8qe, #Bi.#CQk(5));
            #Bi.#OX(this.#8qe, #Bo.X, #Bo.Y, #Bo.Right, #Bo.Bottom);
            #Bi.#bpe(this.#8qe, ptr3);
            #Bi.#bpe(this.#8qe, ptr2);
            #Bi.#5oe(ptr);
        }

        public void DrawRoundedRectangle(Rectangle #Bo, float #2Df, Color #eUb, DashStyle #6Ue, float #Nsk)
        {
            IntPtr ptr = #Bi.#Z6e(#yRk(#6Ue), (int) #Nsk, ColorTranslator.ToWin32(#eUb));
            IntPtr ptr2 = #Bi.#bpe(this.#8qe, ptr);
            IntPtr ptr3 = #Bi.#bpe(this.#8qe, #Bi.#CQk(5));
            int num = (int) (#2Df * 2f);
            #Bi.#GQk(this.#8qe, #Bo.X, #Bo.Y, #Bo.Right, #Bo.Bottom, num, num);
            #Bi.#bpe(this.#8qe, ptr3);
            #Bi.#bpe(this.#8qe, ptr2);
            #Bi.#5oe(ptr);
        }

        public void FillEllipse(Rectangle #Bo, Color #eUb)
        {
            IntPtr ptr = #Bi.#Z6e(0, 1, ColorTranslator.ToWin32(#eUb));
            IntPtr ptr2 = #Bi.#bpe(this.#8qe, ptr);
            IntPtr ptr3 = #Bi.#06e(ColorTranslator.ToWin32(#eUb));
            IntPtr ptr4 = #Bi.#bpe(this.#8qe, ptr3);
            #Bi.#PX(this.#8qe, #Bo.X, #Bo.Y, #Bo.Right, #Bo.Bottom);
            #Bi.#bpe(this.#8qe, ptr4);
            #Bi.#5oe(ptr3);
            #Bi.#bpe(this.#8qe, ptr2);
            #Bi.#5oe(ptr);
        }

        public void FillGeometry(Point #bVe, GraphicsPath #OK, Color #eUb)
        {
            IntPtr ptr = #Bi.#Z6e(0, 1, ColorTranslator.ToWin32(#eUb));
            IntPtr ptr2 = #Bi.#bpe(this.#8qe, ptr);
            IntPtr ptr3 = #Bi.#06e(ColorTranslator.ToWin32(#eUb));
            IntPtr ptr4 = #Bi.#bpe(this.#8qe, ptr3);
            this.#uRk(#bVe, #OK);
            #Bi.#BQk(this.#8qe);
            #Bi.#bpe(this.#8qe, ptr4);
            #Bi.#5oe(ptr3);
            #Bi.#bpe(this.#8qe, ptr2);
            #Bi.#5oe(ptr);
        }

        public void FillRectangle(Rectangle #Bo, Color #eUb)
        {
            IntPtr ptr = #Bi.#06e(ColorTranslator.ToWin32(#eUb));
            #Bi.#26e(this.#8qe, new #Bi.#Fi(#Bo), ptr);
            #Bi.#5oe(ptr);
        }

        public void FillRoundedRectangle(Rectangle #Bo, float #2Df, Color #eUb)
        {
            int num = (int) (#2Df * 2f);
            #Bi.#GQk(this.#8qe, #Bo.X, #Bo.Y, #Bo.Right, #Bo.Bottom, num, num);
            #Bi.#5oe(#Bi.#06e(ColorTranslator.ToWin32(#eUb)));
        }

        public Rectangle ClipBounds =>
            this.#ahh;

        [CompilerGenerated]
        private sealed class #93k : IEnumerable<string>, IEnumerator<string>, IDisposable, IEnumerable, IEnumerator
        {
            private int #Vo;
            private string #Uo;
            private int #Wo;
            private char #cvf;
            public char #6Sk;
            private IEnumerator<string> #F1i;

            [DebuggerHidden]
            private IEnumerator<string> #dlb()
            {
                #zOk.#93k #k;
                if ((this.#Vo != -2) || (this.#Wo != Thread.CurrentThread.ManagedThreadId))
                {
                    #k = new #zOk.#93k(0);
                }
                else
                {
                    this.#Vo = 0;
                    #k = this;
                }
                #k.#cvf = this.#6Sk;
                return #k;
            }

            private void #G1i()
            {
                this.#Vo = -1;
                if (this.#F1i != null)
                {
                    this.#F1i.Dispose();
                }
            }

            private bool #gaf()
            {
                bool flag;
                try
                {
                    int num = this.#Vo;
                    if (num == 0)
                    {
                        this.#Vo = -1;
                        #zOk.#tRk ??= FontFamilyMapRepository.#3x();
                        this.#F1i = #zOk.#tRk.#6Ok(this.#cvf).GetEnumerator();
                        this.#Vo = -3;
                    }
                    else if (num == 1)
                    {
                        this.#Vo = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.#F1i.MoveNext())
                    {
                        this.#G1i();
                        this.#F1i = null;
                        flag = false;
                    }
                    else
                    {
                        string current = this.#F1i.Current;
                        this.#Uo = current;
                        this.#Vo = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.#wC();
                }
                return flag;
            }

            [DebuggerHidden]
            private IEnumerator #tC() => 
                this.#dlb();

            [DebuggerHidden]
            private void #vC()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            private void #wC()
            {
                int num = this.#Vo;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.#G1i();
                    }
                }
            }

            [DebuggerHidden]
            public #93k(int <>1__state)
            {
                this.#Vo = <>1__state;
                this.#Wo = Thread.CurrentThread.ManagedThreadId;
            }

            private string System.Collections.Generic.IEnumerator<System.String>.Current =>
                this.#Uo;

            private object System.Collections.IEnumerator.Current =>
                this.#Uo;
        }

        public enum #ZTk
        {
            #XTk,
            #YTk,
            #8Tc
        }
    }
}

