namespace #xOk
{
    using #H;
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class #MSk : ITextLayoutLine
    {
        private int #aSk;
        private int[] #d7k;

        private int #ASk(int #Zn)
        {
            #USk sk = this.#wSk(this.#aSk + #Zn);
            if (sk == null)
            {
                return -1;
            }
            int num = this.#e7k(this.#aSk + #Zn);
            if (num < 0)
            {
                num = Math.Max(0, ~num - 1);
            }
            return (!sk.IsRightToLeft ? Array.IndexOf<int>(this.CharLogicalClusters, num, sk.StartCharacterIndex, sk.CharacterCount) : Array.LastIndexOf<int>(this.CharLogicalClusters, num, sk.EndCharacterIndex - 1, sk.CharacterCount));
        }

        private int #e7k(int #Zn)
        {
            if (this.#d7k == null)
            {
                int[] glyphXs = this.GlyphXs;
                int num6 = 0;
                int index = glyphXs.Length - 1;
                while (num6 <= index)
                {
                    int num8 = (num6 + index) / 2;
                    int num9 = glyphXs[num8];
                    if (num9 == #Zn)
                    {
                        return num8;
                    }
                    if (num9 > #Zn)
                    {
                        index = num8 - 1;
                        continue;
                    }
                    num6 = num8 + 1;
                }
                return ((index < 0) ? -1 : ((glyphXs[index] <= #Zn) ? ~(index + 1) : ~index));
            }
            int num = 0;
            int num2 = -1;
            int length = this.GlyphXs.Length;
            for (int i = 0; i < length; i++)
            {
                int num5 = this.GlyphXs[i];
                if (num5 == #Zn)
                {
                    return i;
                }
                if ((num5 < #Zn) && (num5 > num2))
                {
                    num = i;
                    num2 = num5;
                }
            }
            return ~Math.Min(length, num + 1);
        }

        internal #USk #f7k(int #g7k)
        {
            if (this.#d7k != null)
            {
                #g7k = this.#d7k[#g7k];
            }
            return this.Runs[#g7k];
        }

        internal void #h7k()
        {
            #zOk.#xtk(this.Runs, this.HasHardLineBreak, out this.#d7k);
            if (this.#d7k != null)
            {
                int count = this.Runs.Count;
                int num2 = 0;
                int index = 0;
                while (index < count)
                {
                    #USk sk = this.Runs[this.#d7k[index]];
                    sk.X = num2;
                    num2 += sk.Width;
                    int num4 = 0;
                    int length = sk.Length;
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= length)
                        {
                            index++;
                            break;
                        }
                        int num7 = this.CharLogicalClusters[sk.StartCharacterIndex + (sk.IsRightToLeft ? ((length - 1) - num6) : num6)];
                        this.GlyphXs[num7] = (this.#aSk + sk.X) + num4;
                        num4 += this.GlyphAdvanceWidths[num7];
                        num6++;
                    }
                }
            }
        }

        internal int #vSk(int #VTc)
        {
            IList<#USk> runs = this.Runs;
            int num = 0;
            int num2 = runs.Count - 1;
            while (num <= num2)
            {
                int num3 = (num + num2) / 2;
                #USk sk = runs[num3];
                if ((#VTc >= sk.StartCharacterIndex) && (#VTc < sk.EndCharacterIndex))
                {
                    return num3;
                }
                if (#VTc < sk.StartCharacterIndex)
                {
                    num2 = num3 - 1;
                    continue;
                }
                num = num3 + 1;
            }
            return -1;
        }

        private #USk #wSk(int #Zn)
        {
            IList<#USk> runs;
            if (6 == 0)
            {
                IList<#USk> runs = this.Runs;
            }
            else
            {
                runs = this.Runs;
            }
            int num = 0;
            int num2 = runs.Count - 1;
            while (num <= num2)
            {
                int index = (num + num2) / 2;
                #USk sk = runs[(this.#d7k != null) ? this.#d7k[index] : index];
                if ((#Zn >= sk.X) && (#Zn < (sk.X + sk.Width)))
                {
                    return sk;
                }
                if (#Zn < sk.X)
                {
                    num2 = index - 1;
                    continue;
                }
                num = index + 1;
            }
            return null;
        }

        internal #MSk(#xtk layout, IList<#USk> runs, int width, int height, float baseline, int previousTotalLineWidth, int[] charLogicalClusters, ushort[] glyphs, int[] glyphAdvanceWidths, #Bi.#eTk[] glyphOffsets, int[] glyphXs)
        {
            if (layout == null)
            {
                throw new ArgumentNullException(#G.#eg(0xd36));
            }
            if ((runs == null) || (runs.Count == 0))
            {
                throw new ArgumentNullException(#G.#eg(0xd3f));
            }
            if (charLogicalClusters == null)
            {
                throw new ArgumentNullException(#G.#eg(0xd48));
            }
            if (glyphs == null)
            {
                throw new ArgumentNullException(#G.#eg(0xd65));
            }
            if (glyphAdvanceWidths == null)
            {
                throw new ArgumentNullException(#G.#eg(0xd6e));
            }
            if (glyphOffsets == null)
            {
                throw new ArgumentNullException(#G.#eg(0xd87));
            }
            if (glyphXs == null)
            {
                throw new ArgumentNullException(#G.#eg(0xd98));
            }
            this.Layout = layout;
            this.Runs = runs;
            this.Width = width;
            this.Height = height;
            this.Baseline = baseline;
            this.#aSk = previousTotalLineWidth;
            this.CharLogicalClusters = charLogicalClusters;
            this.Glyphs = glyphs;
            this.GlyphAdvanceWidths = glyphAdvanceWidths;
            this.GlyphOffsets = glyphOffsets;
            this.GlyphXs = glyphXs;
        }

        internal void Draw(CanvasDrawContext #Uj, Point #bVe)
        {
            if ((#Uj != null) && (#Uj.PlatformRenderer != null))
            {
                bool isNativeRendering = #Uj.IsNativeRendering;
                #Uj.IsNativeRendering = true;
                this.Layout.DrawLine(#Uj.NativeRenderer, this, #bVe.X, #bVe.Y);
                #Uj.IsNativeRendering = isNativeRendering;
            }
        }

        public ITextBounds GetCharacterBounds(int #VTc, bool #tuk)
        {
            int num = this.CharacterCount - (this.HasHardLineBreak ? 1 : 0);
            int num2 = (#VTc - this.StartCharacterIndex) - num;
            if (num2 >= 0)
            {
                return new #ytk(new Rectangle(this.Width + (#tuk ? (num2 * this.Layout.SpaceWidth) : 0), 0, this.Layout.SpaceWidth, this.Height), false);
            }
            if ((#VTc - this.StartCharacterIndex) < num)
            {
                int num3 = this.#vSk(#VTc);
                if (num3 != -1)
                {
                    #USk sk = this.Runs[num3];
                    int index = this.CharLogicalClusters[#VTc];
                    int width = this.GlyphAdvanceWidths[index];
                    return new #ytk(new Rectangle(this.GlyphXs[index] - this.#aSk, 0, width, this.Height), sk.IsRightToLeft);
                }
            }
            return new #ytk(new Rectangle(0, 0, 0, this.Height), false);
        }

        public IEnumerable<ITextBounds> GetTextBounds(int #VTc, int #5Ue, bool #tuk)
        {
            #n7k #nk1 = new #n7k(-2);
            #n7k #nk2 = new #n7k(-2);
            #nk2.#Xo = this;
            #n7k local4 = #nk2;
            #n7k local5 = #nk2;
            local5.#Guk = #VTc;
            #MSk local2 = (#MSk) local5;
            #MSk local3 = (#MSk) local5;
            local3.#Huk = #5Ue;
            int local1 = (int) local3;
            local1.#Iuk = #tuk;
            return (IEnumerable<ITextBounds>) local1;
        }

        public int HitTest(Point #bVe)
        {
            Rectangle rectangle = new Rectangle(0, 0, this.Width + this.Layout.SpaceWidth, this.Height);
            if (!rectangle.Contains(#bVe))
            {
                return -1;
            }
            int num = this.#ASk(#bVe.X);
            return ((num == -1) ? (this.CharacterCount - (this.HasHardLineBreak ? 1 : 0)) : num);
        }

        internal int[] CharLogicalClusters { get; private set; }

        internal int EndCharacterIndex =>
            this.Runs[this.Runs.Count - 1].EndCharacterIndex;

        internal int[] GlyphAdvanceWidths { get; private set; }

        internal #Bi.#eTk[] GlyphOffsets { get; private set; }

        internal ushort[] Glyphs { get; private set; }

        internal int[] GlyphXs { get; private set; }

        internal #xtk Layout { get; private set; }

        internal IList<#USk> Runs { get; private set; }

        public float Baseline { get; private set; }

        public int CharacterCount =>
            this.EndCharacterIndex - this.StartCharacterIndex;

        public bool HasHardLineBreak { get; set; }

        public int Height { get; private set; }

        public int StartCharacterIndex =>
            this.Runs[0].StartCharacterIndex;

        public int Width { get; private set; }

        [CompilerGenerated]
        private sealed class #n7k : IEnumerable<ITextBounds>, IDisposable, IEnumerator<ITextBounds>, IEnumerable, IEnumerator
        {
            private int #Vo;
            private ITextBounds #Uo;
            private int #Wo;
            public #MSk #Xo;
            private int #VTc;
            public int #Guk;
            private int #5Ue;
            public int #Huk;
            private bool #tuk;
            public bool #Iuk;
            private int #Juk;
            private int #1Tk;
            private bool #2Tk;
            private IList<#USk> #3Tk;
            private #USk #4Tk;
            private int #5Tk;
            private int #6Tk;
            private int #7Tk;

            [DebuggerHidden]
            private IEnumerator<ITextBounds> #9Tk()
            {
                #MSk.#n7k #nk;
                if ((this.#Vo == -2) && (this.#Wo == Thread.CurrentThread.ManagedThreadId))
                {
                    this.#Vo = 0;
                    #nk = this;
                }
                else
                {
                    #nk = new #MSk.#n7k(0) {
                        #Xo = this.#Xo
                    };
                }
                #nk.#VTc = this.#Guk;
                #nk.#5Ue = this.#Huk;
                #nk.#tuk = this.#Iuk;
                return #nk;
            }

            private bool #gaf()
            {
                int num4;
                int num5;
                bool? nullable;
                int num = this.#Vo;
                #MSk sk = this.#Xo;
                switch (num)
                {
                    case 0:
                    {
                        this.#Vo = -1;
                        int num2 = sk.StartCharacterIndex - this.#VTc;
                        if (num2 > 0)
                        {
                            this.#VTc += num2;
                            this.#5Ue -= num2;
                        }
                        int num3 = (this.#VTc - sk.StartCharacterIndex) - (sk.CharacterCount - (sk.HasHardLineBreak ? 1 : 0));
                        this.#Juk = ((this.#VTc + this.#5Ue) - sk.StartCharacterIndex) - (sk.CharacterCount - (sk.HasHardLineBreak ? 1 : 0));
                        if (num3 >= 0)
                        {
                            if (this.#tuk)
                            {
                                this.#Uo = new #ytk(new Rectangle(sk.Width + (num3 * sk.Layout.SpaceWidth), 0, (this.#Juk - num3) * sk.Layout.SpaceWidth, sk.Height), false);
                                this.#Vo = 1;
                                return true;
                            }
                            this.#Uo = new #ytk(new Rectangle(sk.Width, 0, 0, sk.Height), false);
                            this.#Vo = 2;
                            return true;
                        }
                        this.#1Tk = sk.#vSk(this.#VTc);
                        this.#2Tk = false;
                        if (this.#1Tk == -1)
                        {
                            goto TR_000C;
                        }
                        else
                        {
                            this.#3Tk = sk.Runs;
                            num4 = 0;
                            num5 = -1;
                            nullable = null;
                        }
                        goto TR_0020;
                    }
                    case 1:
                        this.#Vo = -1;
                        goto TR_0029;

                    case 2:
                        this.#Vo = -1;
                        goto TR_0029;

                    case 3:
                        this.#Vo = -1;
                        goto TR_0015;

                    case 4:
                        this.#Vo = -1;
                        goto TR_000D;

                    case 5:
                        this.#Vo = -1;
                        break;

                    case 6:
                        this.#Vo = -1;
                        break;

                    case 7:
                        this.#Vo = -1;
                        break;

                    default:
                        return false;
                }
            TR_0008:
                return false;
            TR_000C:
                if (this.#Juk > 0)
                {
                    if (this.#tuk)
                    {
                        this.#Uo = new #ytk(new Rectangle(sk.Width, 0, this.#Juk * sk.Layout.SpaceWidth, sk.Height), false);
                        this.#Vo = 5;
                        return true;
                    }
                    if (sk.HasHardLineBreak)
                    {
                        this.#Uo = new #ytk(new Rectangle(sk.Width, 0, sk.Layout.SpaceWidth, sk.Height), false);
                        this.#Vo = 6;
                        return true;
                    }
                    if (!this.#2Tk)
                    {
                        this.#Uo = new #ytk(new Rectangle(sk.Width, 0, 0, sk.Height), false);
                        this.#Vo = 7;
                        return true;
                    }
                }
                goto TR_0008;
            TR_000D:
                this.#3Tk = null;
                goto TR_000C;
            TR_0010:
                if (nullable != null)
                {
                    int width = num5 - num4;
                    if (width >= 0)
                    {
                        this.#2Tk = true;
                        Rectangle bounds = new Rectangle(num4, 0, width, sk.Height);
                        this.#Uo = new #ytk(bounds, nullable.Value);
                        this.#Vo = 4;
                        return true;
                    }
                }
                goto TR_000D;
            TR_0012:
                this.#4Tk = null;
                int num11 = this.#1Tk;
                this.#1Tk = num11 + 1;
                goto TR_0020;
            TR_0013:
                this.#VTc += this.#5Tk;
                this.#5Ue -= this.#5Tk;
                if (this.#5Ue <= 0)
                {
                    goto TR_0010;
                }
                goto TR_0012;
            TR_0015:
                nullable = new bool?(this.#4Tk.IsRightToLeft);
                num4 = this.#6Tk;
                num5 = this.#7Tk;
                goto TR_0013;
            TR_0020:
                while (true)
                {
                    if (this.#1Tk < this.#3Tk.Count)
                    {
                        this.#4Tk = this.#3Tk[this.#1Tk];
                        int endCharacterIndex = this.#4Tk.EndCharacterIndex;
                        if (this.#VTc >= endCharacterIndex)
                        {
                            goto TR_0012;
                        }
                        else
                        {
                            this.#5Tk = Math.Min(this.#5Ue, endCharacterIndex - this.#VTc);
                            int num7 = sk.CharLogicalClusters[this.#VTc + Math.Max(0, this.#5Tk - 1)];
                            int num1 = sk.CharLogicalClusters[this.#VTc];
                            int index = Math.Min(num1, num7);
                            int num9 = Math.Max(num1, num7);
                            this.#6Tk = sk.GlyphXs[index] - sk.#aSk;
                            this.#7Tk = (sk.GlyphXs[num9] + ((this.#5Tk > 0) ? sk.GlyphAdvanceWidths[num9] : 0)) - sk.#aSk;
                            if (nullable == null)
                            {
                                nullable = new bool?(this.#4Tk.IsRightToLeft);
                                num4 = this.#6Tk;
                                num5 = this.#7Tk;
                                goto TR_0013;
                            }
                            else if ((num5 == this.#6Tk) || (this.#7Tk == num4))
                            {
                                num4 = Math.Min(num4, this.#6Tk);
                                num5 = Math.Max(num5, this.#7Tk);
                                goto TR_0013;
                            }
                            else
                            {
                                int width = num5 - num4;
                                if (width >= 0)
                                {
                                    this.#2Tk = true;
                                    Rectangle bounds = new Rectangle(num4, 0, width, sk.Height);
                                    this.#Uo = new #ytk(bounds, nullable.Value);
                                    this.#Vo = 3;
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        goto TR_0010;
                    }
                    break;
                }
                goto TR_0015;
            TR_0029:
                return false;
            }

            [DebuggerHidden]
            private IEnumerator #tC() => 
                this.#9Tk();

            [DebuggerHidden]
            private void #vC()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            private void #wC()
            {
            }

            [DebuggerHidden]
            public #n7k(int <>1__state)
            {
                this.#Vo = <>1__state;
                this.#Wo = Thread.CurrentThread.ManagedThreadId;
            }

            private ITextBounds System.Collections.Generic.IEnumerator<ActiproSoftware.WinUICore.Rendering.ITextBounds>.Current =>
                this.#Uo;

            private object System.Collections.IEnumerator.Current =>
                this.#Uo;
        }
    }
}

