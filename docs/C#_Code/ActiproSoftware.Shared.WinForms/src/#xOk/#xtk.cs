namespace #xOk
{
    using #H;
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class #xtk : DisposableObject, IDisposable, ITextLayout
    {
        private float #x5b;
        private List<#luk> #7sk;
        private ITextLayoutLine[] #62c;
        private int #51c = 4;
        private TextLayoutWrapping #8sk;
        private int? #btk;
        private const int #6Rk = 500;

        private int #7Rk(IList<#MSk> #8Rk, IList<#USk> #9Rk, int #aSk, int #bSk, int #cSk, int[] #dSk, ushort[] #SRk, int[] #TRk, #Bi.#eTk[] #URk, int[] #eSk)
        {
            int width = 0;
            int num2 = 0;
            float num3 = 0f;
            #USk[] runs = new #USk[#cSk - #bSk];
            for (int i = 0; i < runs.Length; i++)
            {
                #USk sk2 = #9Rk[#bSk + i];
                width += sk2.Width;
                num2 = Math.Max(num2, sk2.Height);
                num3 = Math.Max(num3, sk2.Baseline);
                runs[i] = sk2;
            }
            #MSk item = new #MSk(this, runs, width, num2, num3, #aSk, #dSk, #SRk, #TRk, #URk, #eSk);
            #8Rk.Add(item);
            return item.Width;
        }

        private void #fSk(TextLayoutWrapping #8sk, IList<#MSk> #8Rk, IList<#USk> #9Rk, IList<#Bi.#LTk> #KRk, int[] #dSk, ushort[] #SRk, int[] #TRk, #Bi.#eTk[] #URk, int[] #eSk)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = -1;
            int num6 = -1;
            bool flag = false;
            int num8 = num4;
            while (true)
            {
                while (true)
                {
                    if (num8 >= #9Rk.Count)
                    {
                        if (num3 < #9Rk[#9Rk.Count - 1].EndCharacterIndex)
                        {
                            this.#7Rk(#8Rk, #9Rk, num, num4, #9Rk.Count, #dSk, #SRk, #TRk, #URk, #eSk);
                        }
                        return;
                    }
                    #USk sk = #9Rk[num8];
                    int num9 = 0;
                    int startCharacterIndex = sk.StartCharacterIndex;
                    while (true)
                    {
                        if (startCharacterIndex >= sk.EndCharacterIndex)
                        {
                            break;
                        }
                        #Bi.#LTk tk = #KRk[startCharacterIndex];
                        if (((tk.IsSoftBreak | flag) || ((#8sk == TextLayoutWrapping.WrapByCharacter) && !tk.IsWhiteSpace)) && (startCharacterIndex > num3))
                        {
                            num5 = num8;
                            num6 = startCharacterIndex;
                            flag = false;
                        }
                        if (tk.IsWhiteSpace)
                        {
                            flag = true;
                        }
                        int num11 = startCharacterIndex;
                        int index = #dSk[startCharacterIndex];
                        int num13 = #TRk[index];
                        while (true)
                        {
                            if (((startCharacterIndex + 1) < sk.EndCharacterIndex) && (#dSk[startCharacterIndex + 1] == index))
                            {
                                startCharacterIndex++;
                                continue;
                            }
                            if (((num2 + num13) <= this.#x5b) || (num11 <= num3))
                            {
                                num2 += num13;
                                num9 += num13;
                                startCharacterIndex++;
                            }
                            else
                            {
                                if (num5 == -1)
                                {
                                    if ((num2 - num9) <= 0)
                                    {
                                        num5 = num8;
                                        num6 = num11;
                                    }
                                    else
                                    {
                                        num2 -= num9;
                                        num9 = 0;
                                        num5 = num8 - 1;
                                        num6 = sk.StartCharacterIndex;
                                        flag = false;
                                    }
                                }
                                #USk sk2 = #9Rk[num5];
                                if (num6 <= sk2.StartCharacterIndex)
                                {
                                    num5--;
                                }
                                else
                                {
                                    #USk item = sk2.#Ogb(num6, #jSk(sk2.StartCharacterIndex, num6, #dSk, #TRk));
                                    #9Rk.Insert(num5 + 1, item);
                                }
                                num += this.#7Rk(#8Rk, #9Rk, num, num4, num5 + 1, #dSk, #SRk, #TRk, #URk, #eSk);
                                num2 = 0;
                                num9 = 0;
                                num3 = num6;
                                num4 = num5 + 1;
                                num8 = num5;
                                num5 = -1;
                                num6 = -1;
                                flag = false;
                                break;
                            }
                            break;
                        }
                    }
                    break;
                }
                num8++;
            }
        }

        private void #gSk(#zOk #IZd, Rectangle #ahh, int #Zn, int #0n, #MSk #Hh, #USk #hSk, int #QRk, int #RRk, ref IntPtr #PRk)
        {
            if (#IZd == null)
            {
                throw new ArgumentNullException(#G.#eg(0xbeb));
            }
            Rectangle rect = new Rectangle(#Zn, #0n, #hSk.Width, #hSk.Height);
            if (#ahh.IntersectsWith(rect))
            {
                #juk format = #hSk.Format;
                if (!format.IsSpacer)
                {
                    #IZd.#ORk(rect.X, rect.Y, ref #PRk, #hSk.Analysis, #QRk, #RRk, #Hh.Glyphs, #Hh.GlyphAdvanceWidths, #Hh.GlyphOffsets);
                    LineKind strikethroughKind = format.StrikethroughKind;
                    LineKind underlineKind = format.UnderlineKind;
                    if ((strikethroughKind != LineKind.None) || (underlineKind != LineKind.None))
                    {
                        #Gsk cache = this.Cache;
                        #vDd dd = cache.#wOk(format);
                        if (strikethroughKind != LineKind.None)
                        {
                            int num = (rect.Top + ((int) Math.Round((double) #hSk.Baseline, MidpointRounding.AwayFromZero))) - ((int) Math.Round((double) (dd.Ascent / 2f), MidpointRounding.AwayFromZero));
                            int strikethroughBrushIndex = format.StrikethroughBrushIndex;
                            Color color = cache.#ysk((strikethroughBrushIndex != -1) ? strikethroughBrushIndex : format.ForegroundIndex);
                            if (format.StrikethroughWeight != TextLineWeight.Double)
                            {
                                #IZd.DrawLine(rect.X, num, rect.Right, num, color, CanvasDrawContext.#Djc(strikethroughKind), 1f);
                            }
                            else
                            {
                                #IZd.DrawLine(rect.X, num - 1, rect.Right, num - 1, color, CanvasDrawContext.#Djc(strikethroughKind), 1f);
                                #IZd.DrawLine(rect.X, num + 1, rect.Right, num + 1, color, CanvasDrawContext.#Djc(strikethroughKind), 1f);
                            }
                        }
                        if (underlineKind != LineKind.None)
                        {
                            int num3 = rect.Top + ((int) Math.Round((double) #hSk.Baseline, MidpointRounding.AwayFromZero));
                            int underlineBrushIndex = format.UnderlineBrushIndex;
                            Color color2 = cache.#ysk((underlineBrushIndex != -1) ? underlineBrushIndex : format.ForegroundIndex);
                            if (format.UnderlineWeight == TextLineWeight.Double)
                            {
                                #IZd.DrawLine(rect.X, num3 - 1, rect.Right, num3 - 1, color2, CanvasDrawContext.#Djc(underlineKind), 1f);
                                #IZd.DrawLine(rect.X, num3 + 1, rect.Right, num3 + 1, color2, CanvasDrawContext.#Djc(underlineKind), 1f);
                            }
                            else
                            {
                                #IZd.DrawLine(rect.X, num3, rect.Right, num3, color2, CanvasDrawContext.#Djc(underlineKind), 1f);
                            }
                        }
                    }
                }
            }
        }

        private int #htk(int #VTc)
        {
            int num = 0;
            int num2 = this.#7sk.Count - 1;
            while (num <= num2)
            {
                int num3 = (num + num2) / 2;
                #luk #luk = this.#7sk[num3];
                int characterIndex = #luk.CharacterIndex;
                if (characterIndex == #VTc)
                {
                    return num3;
                }
                if (characterIndex > #VTc)
                {
                    num2 = num3 - 1;
                    continue;
                }
                num = num3 + 1;
            }
            return ((num2 < 0) ? -1 : ((this.#7sk[num2].CharacterIndex <= #VTc) ? ~(num2 + 1) : ~num2));
        }

        private int #iSk(int #VTc)
        {
            int num = this.#htk(#VTc);
            return ((num >= 0) ? num : Math.Max(0, ~num - 1));
        }

        private void #itk(int #VTc, int #5Ue, #muk #GAf, #juk #jtk, int #ahb, #luk #ktk)
        {
            if (#ktk.Format == #jtk)
            {
                return;
            }
            #luk item = new #luk(#VTc, #jtk);
            if (#ahb >= 0)
            {
                this.#7sk[#ahb] = item;
            }
            else
            {
                #ahb = ~#ahb;
                this.#7sk.Insert(#ahb, item);
            }
            #ahb++;
            int characterIndex = #VTc + #5Ue;
            while (true)
            {
                if (#ahb < this.#7sk.Count)
                {
                    #luk #luk2 = this.#7sk[#ahb];
                    if (#luk2.CharacterIndex >= characterIndex)
                    {
                        if (#luk2.CharacterIndex == characterIndex)
                        {
                            if (#luk2.Format == #jtk)
                            {
                                this.#7sk.RemoveAt(#ahb);
                            }
                            return;
                        }
                        goto TR_0003;
                    }
                    else
                    {
                        #ktk = #luk2;
                        #juk format = #ktk.Format;
                        if (#GAf > #muk.#vte)
                        {
                            if (#GAf == #muk.#T7b)
                            {
                                format.ForegroundIndex = #jtk.ForegroundIndex;
                            }
                            else if (#GAf == #muk.#8Zf)
                            {
                                format.StrikethroughBrushIndex = #jtk.StrikethroughBrushIndex;
                                format.StrikethroughKind = #jtk.StrikethroughKind;
                            }
                            else
                            {
                                if (#GAf != #muk.#7Zf)
                                {
                                    break;
                                }
                                format.UnderlineBrushIndex = #jtk.UnderlineBrushIndex;
                                format.UnderlineKind = #jtk.UnderlineKind;
                                format.UnderlineWeight = #jtk.UnderlineWeight;
                            }
                        }
                        else
                        {
                            switch (#GAf)
                            {
                                case #muk.#EEd:
                                    format.FontFamilyIndex = #jtk.FontFamilyIndex;
                                    break;

                                case #muk.#KK:
                                    format.FontSizeIndex = #jtk.FontSizeIndex;
                                    break;

                                case (#muk.#KK | #muk.#EEd):
                                    goto TR_0004;

                                case #muk.#ute:
                                    format.IsItalic = #jtk.IsItalic;
                                    break;

                                default:
                                    if (#GAf == #muk.#vte)
                                    {
                                        format.IsBold = #jtk.IsBold;
                                    }
                                    else
                                    {
                                        goto TR_0004;
                                    }
                                    break;
                            }
                        }
                        this.#7sk[#ahb++] = new #luk(#ktk.CharacterIndex, format);
                        continue;
                    }
                }
                else
                {
                    goto TR_0003;
                }
                break;
            }
            goto TR_0004;
        TR_0003:
            if ((characterIndex < this.TextProviderInternal.Length) && (#ktk.Format != #jtk))
            {
                #luk #luk3 = new #luk(characterIndex, #ktk.Format);
                this.#7sk.Insert(#ahb, #luk3);
            }
            return;
        TR_0004:
            throw new NotSupportedException();
        }

        private static int #jSk(int #kSk, int #lSk, IList<int> #dSk, IList<int> #TRk)
        {
            int num = 0;
            int num2 = #kSk;
            while (num2 < #lSk)
            {
                int num3 = #dSk[num2];
                int num4 = #TRk[num3];
                while (true)
                {
                    if (((num2 + 1) >= #lSk) || (#dSk[num2 + 1] != num3))
                    {
                        num += num4;
                        num2++;
                        break;
                    }
                    num2++;
                }
            }
            return num;
        }

        private void #ltk(int #VTc, int #5Ue)
        {
            int length = this.TextProvider.Length;
            if ((#VTc < 0) || (#VTc > (length - 1)))
            {
                throw new ArgumentOutOfRangeException(#G.#eg(0xb80));
            }
            if ((#5Ue <= 0) || (#5Ue > (length - #VTc)))
            {
                throw new ArgumentOutOfRangeException(#G.#eg(0xbc0));
            }
        }

        private void #qtk()
        {
            if (!this.AreMetricsValid)
            {
                this.RunTextFormatter();
            }
        }

        private void #rtk()
        {
            this.#stk();
            this.#62c = null;
        }

        private void #stk()
        {
        }

        private #MSk[] #wtk(#4sk #4sk)
        {
            #Bi.#FTk[] tkArray;
            #Bi.#iTk[] tkArray2;
            if (#4sk == null)
            {
                throw new ArgumentNullException(#G.#eg(0xc01));
            }
            List<#MSk> list = new List<#MSk>(1);
            SpacerTextProvider textProviderInternal = this.TextProviderInternal;
            int length = textProviderInternal.Length;
            #Gsk cache = this.Cache;
            int x = 0;
            int height = 0;
            float baseline = 0f;
            #zOk renderer = #4sk.Renderer;
            #juk format = this.#7sk[0].Format;
            #vDd dd = cache.#wOk(format);
            renderer.#FRk(dd);
            Size size = renderer.#r5e(#G.#eg(0xc0e));
            this.#btk = new int?(size.Width);
            int num5 = this.SpaceWidth * this.TabSize;
            List<#Bi.#LTk> list2 = new List<#Bi.#LTk>(length);
            List<int> list3 = new List<int>(length);
            List<ushort> list4 = new List<ushort>(length);
            List<int> list5 = new List<int>(length);
            List<#Bi.#eTk> list6 = new List<#Bi.#eTk>(length);
            List<int> list7 = new List<int>(length);
            List<#USk> list8 = new List<#USk>(4);
            string substring = textProviderInternal.GetSubstring(0, textProviderInternal.Length);
            substring.EndsWith(#G.#eg(0xc13), StringComparison.Ordinal);
            if (#zOk.#LRk(substring, out tkArray, out tkArray2))
            {
                foreach (#m7k local1 in #m7k.#j7k(length, tkArray, tkArray2, this.#7sk))
                {
                    ushort[] numArray5;
                    #Bi.#sTk[] tkArray4;
                    ushort[] numArray6;
                    #Bi.#DTk[] tkArray5;
                    int startCharacterIndex = local1.StartCharacterIndex;
                    int endCharacterIndex = local1.EndCharacterIndex;
                    int num8 = endCharacterIndex - startCharacterIndex;
                    string str = textProviderInternal.GetSubstring(startCharacterIndex, num8);
                    #Bi.#pTk analysis = local1.Analysis;
                    #Bi.#iTk scriptTag = local1.ScriptTag;
                    #juk #juk2 = local1.Format;
                    #zOk.#GRk(str, num8, analysis, list2);
                    dd = cache.#wOk(#juk2);
                    IntPtr scriptCache = dd.ScriptCache;
                    renderer.#FRk(dd);
                    #zOk.#ZTk tk3 = renderer.#1Rk(ref scriptCache, str, num8, false, ref analysis, scriptTag, out numArray5, out tkArray4, out numArray6, out tkArray5);
                    if (tk3 == #zOk.#ZTk.#YTk)
                    {
                        #juk #juk3 = #juk2;
                        #vDd dd2 = dd;
                        if (num8 > 0)
                        {
                            foreach (string str2 in renderer.#DRk(dd, str[0]))
                            {
                                if (!string.IsNullOrEmpty(str2))
                                {
                                    #juk2 = #juk3.Clone();
                                    #juk2.FontFamilyIndex = cache.#Bsk(str2);
                                    dd = cache.#wOk(#juk2);
                                    scriptCache = dd.ScriptCache;
                                    renderer.#FRk(dd);
                                    tk3 = renderer.#1Rk(ref scriptCache, str, num8, false, ref analysis, scriptTag, out numArray5, out tkArray4, out numArray6, out tkArray5);
                                    if (tk3 == #zOk.#ZTk.#8Tc)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (tk3 == #zOk.#ZTk.#YTk)
                        {
                            dd = dd2;
                            scriptCache = dd.ScriptCache;
                            renderer.#FRk(dd);
                            tk3 = renderer.#1Rk(ref scriptCache, str, num8, true, ref analysis, scriptTag, out numArray5, out tkArray4, out numArray6, out tkArray5);
                        }
                    }
                    if (tk3 != #zOk.#ZTk.#XTk)
                    {
                        int height = dd.Height;
                        float baseline = dd.Baseline;
                        height = Math.Max(height, height);
                        baseline = Math.Max(baseline, baseline);
                        int count = list4.Count;
                        list4.AddRange(numArray6);
                        int num12 = list4.Count;
                        ushort[] numArray7 = numArray5;
                        int index = 0;
                        while (true)
                        {
                            if (index >= numArray7.Length)
                            {
                                if (renderer.#VRk(ref scriptCache, str, num8, analysis, scriptTag, numArray5, tkArray4, numArray6, tkArray5, list5, list6))
                                {
                                    int width = 0;
                                    ITextSpacer spacer = null;
                                    if (#juk2.IsSpacer)
                                    {
                                        spacer = textProviderInternal.#Xsk(startCharacterIndex);
                                        if (spacer != null)
                                        {
                                            list5[count] = spacer.Size.Width;
                                            list7.Add(x + width);
                                            width = spacer.Size.Width;
                                            height = spacer.Size.Height;
                                            baseline = spacer.Baseline;
                                            height = Math.Max(height, height);
                                            baseline = Math.Max(baseline, baseline);
                                        }
                                    }
                                    if (spacer == null)
                                    {
                                        if ((num8 == 1) && (str[0] == '\t'))
                                        {
                                            list5[count] = num5 - (x % num5);
                                        }
                                        for (int i = count; i < num12; i++)
                                        {
                                            list7.Add(x + width);
                                            int num17 = list5[i];
                                            width += num17;
                                        }
                                    }
                                    #USk item = new #USk(startCharacterIndex, endCharacterIndex, analysis, x, width, height, baseline, #juk2);
                                    while (true)
                                    {
                                        if (item == null)
                                        {
                                            x += width;
                                            break;
                                        }
                                        if (item.CharacterCount <= 510)
                                        {
                                            list8.Add(item);
                                            item = null;
                                            continue;
                                        }
                                        int num18 = item.StartCharacterIndex + 500;
                                        int num19 = #jSk(item.StartCharacterIndex, num18, list3, list5);
                                        list8.Add(item);
                                        item = item.#Ogb(item.StartCharacterIndex + 500, num19);
                                    }
                                }
                                break;
                            }
                            ushort num14 = numArray7[index];
                            list3.Add(count + num14);
                            index++;
                        }
                    }
                    if (dd.ScriptCache == IntPtr.Zero)
                    {
                        dd.ScriptCache = scriptCache;
                    }
                }
            }
            if (list8.Count == 0)
            {
                dd = cache.#wOk(format);
                baseline = dd.Baseline;
                height = dd.Height;
                #Bi.#pTk analysis = new #Bi.#pTk();
                list8.Add(new #USk(0, length, analysis, 0, 0, height, baseline, format));
                renderer.#FRk(dd);
            }
            int[] numArray = list3.ToArray();
            ushort[] numArray2 = list4.ToArray();
            int[] numArray3 = list5.ToArray();
            #Bi.#eTk[] tkArray3 = list6.ToArray();
            int[] numArray4 = list7.ToArray();
            if ((this.#8sk != TextLayoutWrapping.NoWrap) && ((x > this.#x5b) && (length > 1)))
            {
                this.#fSk(this.#8sk, list, list8, list2, numArray, numArray2, numArray3, tkArray3, numArray4);
            }
            else
            {
                list.Add(new #MSk(this, list8, x, height, baseline, 0, numArray, numArray2, numArray3, tkArray3, numArray4));
            }
            if (list.Count > 0)
            {
                list[list.Count - 1].HasHardLineBreak = (textProviderInternal.Length > 0) ? (textProviderInternal.GetSubstring(textProviderInternal.Length - 1, 1) == #G.#eg(0xc13)) : false;
                using (List<#MSk>.Enumerator enumerator3 = list.GetEnumerator())
                {
                    while (enumerator3.MoveNext())
                    {
                        enumerator3.Current.#h7k();
                    }
                }
            }
            return list.ToArray();
        }

        internal #xtk(#Gsk cache, ITextProvider textProvider, float maxWidth, string fontFamilyName, float fontSize, Color? foreground, IEnumerable<ITextSpacer> spacers)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(#G.#eg(0xb95));
            }
            if (textProvider == null)
            {
                throw new ArgumentNullException(#G.#eg(0xb9e));
            }
            this.Cache = cache;
            this.TextProviderInternal = new SpacerTextProvider(textProvider, spacers);
            this.#x5b = Math.Max(1f, maxWidth);
            this.Initialize(fontFamilyName, fontSize, foreground);
        }

        protected override void Dispose(bool #Fee)
        {
            if (#Fee)
            {
                this.#rtk();
            }
        }

        internal void DrawLine(#zOk #IZd, #MSk #Hh, int #Zn, int #0n)
        {
            if (#IZd == null)
            {
                throw new ArgumentNullException(#G.#eg(0xbeb));
            }
            if (#Hh == null)
            {
                throw new ArgumentNullException(#G.#eg(0xbf8));
            }
            if (#Hh.Runs.Count != 0)
            {
                float baseline = #Hh.Baseline;
                #Gsk cache = this.Cache;
                Rectangle clipBounds = #IZd.ClipBounds;
                int count = this.#7sk.Count;
                int num3 = 0;
                int num4 = 0;
                bool flag = false;
                int num5 = #Hh.Runs.Count;
                for (int i = 0; i < num5; i++)
                {
                    #USk sk = #Hh.#f7k(i);
                    if (sk.StartCharacterIndex < sk.EndCharacterIndex)
                    {
                        if ((#Zn + sk.Width) > clipBounds.Left)
                        {
                            if (!flag)
                            {
                                num3 = this.#iSk(sk.StartCharacterIndex);
                                #IZd.#Bxe(cache.#ysk(this.#7sk[num3].Format.ForegroundIndex));
                                num4 = ((num3 + 1) < count) ? this.#7sk[num3 + 1].CharacterIndex : 0x7fffffff;
                                flag = true;
                            }
                            #vDd dd = cache.#wOk(sk.Format);
                            IntPtr scriptCache = dd.ScriptCache;
                            int num7 = (int) Math.Round((double) (baseline - dd.Baseline), MidpointRounding.AwayFromZero);
                            #IZd.#FRk(dd);
                            bool isRightToLeft = sk.IsRightToLeft;
                            int startCharacterIndex = sk.StartCharacterIndex;
                            while (true)
                            {
                                if (num4 >= sk.EndCharacterIndex)
                                {
                                    if (startCharacterIndex < sk.EndCharacterIndex)
                                    {
                                        int num11 = #Hh.CharLogicalClusters[isRightToLeft ? (sk.EndCharacterIndex - 1) : startCharacterIndex];
                                        this.#gSk(#IZd, clipBounds, #Zn, #0n + num7, #Hh, sk, num11, (#Hh.CharLogicalClusters[isRightToLeft ? startCharacterIndex : (sk.EndCharacterIndex - 1)] + 1) - num11, ref scriptCache);
                                        startCharacterIndex = num4;
                                    }
                                    if (dd.ScriptCache == IntPtr.Zero)
                                    {
                                        dd.ScriptCache = scriptCache;
                                    }
                                    break;
                                }
                                int num9 = #Hh.CharLogicalClusters[isRightToLeft ? (num4 - 1) : startCharacterIndex];
                                this.#gSk(#IZd, clipBounds, #Zn, #0n + num7, #Hh, sk, num9, (#Hh.CharLogicalClusters[isRightToLeft ? startCharacterIndex : (num4 - 1)] + 1) - num9, ref scriptCache);
                                startCharacterIndex = num4;
                                num3++;
                                #IZd.#Bxe(cache.#ysk(this.#7sk[num3].Format.ForegroundIndex));
                                num4 = ((num3 + 1) < count) ? this.#7sk[num3 + 1].CharacterIndex : 0x7fffffff;
                            }
                        }
                        #Zn += sk.Width;
                        if (#Zn > clipBounds.Right)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void Initialize(string #S7b, float #KK, Color? #T7b)
        {
            if (string.IsNullOrEmpty(#S7b))
            {
                throw new ArgumentNullException(#G.#eg(0xac1));
            }
            if (#T7b == null)
            {
                throw new ArgumentNullException(#G.#eg(0xbaf));
            }
            #Gsk cache = this.Cache;
            #juk format = new #juk {
                FontFamilyIndex = cache.#Bsk(#S7b),
                FontSizeIndex = cache.#Dsk(#KK),
                ForegroundIndex = cache.#zsk(#T7b)
            };
            List<#luk> list1 = new List<#luk>(4);
            list1.Add(new #luk(0, format));
            this.#7sk = list1;
            IList<ITextSpacer> spacers = this.TextProviderInternal.Spacers;
            if (spacers != null)
            {
                using (IEnumerator<ITextSpacer> enumerator = spacers.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        int characterIndex = enumerator.Current.CharacterIndex;
                        int num2 = this.#htk(characterIndex);
                        if (num2 >= 0)
                        {
                            #juk #juk2 = this.#7sk[num2].Format;
                            #juk2.IsSpacer = true;
                            this.#7sk[num2] = new #luk(characterIndex, #juk2);
                        }
                        else
                        {
                            #juk #juk3 = format;
                            #juk3.IsSpacer = true;
                            this.#7sk.Insert(~num2, new #luk(characterIndex, #juk3));
                        }
                        num2 = this.#htk(characterIndex + 1);
                        if (num2 < 0)
                        {
                            this.#7sk.Insert(~num2, new #luk(characterIndex + 1, format));
                        }
                    }
                }
            }
        }

        private void RunTextFormatter()
        {
            #MSk[] skArray = null;
            CanvasControl canvas = this.Cache.Canvas as CanvasControl;
            if (canvas != null)
            {
                using (#4sk #sk = (canvas.CreateTextBatch() as #4sk) ?? new #4sk(canvas))
                {
                    skArray = this.#wtk(#sk);
                }
            }
            this.#62c = (skArray != null) ? ((ITextLayoutLine[]) skArray) : ((ITextLayoutLine[]) new #MSk[0]);
        }

        public void RunTextFormatter(IDisposable #c3b)
        {
            if (!this.AreMetricsValid)
            {
                #4sk #sk = #c3b as #4sk;
                if (#sk != null)
                {
                    this.#62c = this.#wtk(#sk);
                }
                else
                {
                    this.RunTextFormatter();
                }
            }
        }

        public void SetFontFamily(int #VTc, int #5Ue, string #S7b)
        {
            this.#ltk(#VTc, #5Ue);
            if (string.IsNullOrEmpty(#S7b))
            {
                throw new ArgumentOutOfRangeException(#G.#eg(0xac1));
            }
            if (this.AreMetricsValid)
            {
                this.#rtk();
            }
            int num = this.#htk(#VTc);
            #luk #luk = this.#7sk[(num >= 0) ? num : Math.Max(0, ~num - 1)];
            #juk format = #luk.Format;
            format.FontFamilyIndex = this.Cache.#Bsk(#S7b);
            this.#itk(#VTc, #5Ue, #muk.#EEd, format, num, #luk);
        }

        public void SetFontSize(int #VTc, int #5Ue, float #KK)
        {
            this.#ltk(#VTc, #5Ue);
            if (#KK <= 0.0)
            {
                throw new ArgumentOutOfRangeException(#G.#eg(0xbd5));
            }
            if (this.AreMetricsValid)
            {
                this.#rtk();
            }
            int num = this.#htk(#VTc);
            #luk #luk = this.#7sk[(num >= 0) ? num : Math.Max(0, ~num - 1)];
            #juk format = #luk.Format;
            format.FontSizeIndex = this.Cache.#Dsk(#KK);
            this.#itk(#VTc, #5Ue, #muk.#KK, format, num, #luk);
        }

        public void SetFontStyle(int #VTc, int #5Ue, FontStyles #ute)
        {
            this.#ltk(#VTc, #5Ue);
            bool flag = #ute == FontStyles.Italic;
            if (this.AreMetricsValid)
            {
                this.#rtk();
            }
            int num = this.#htk(#VTc);
            #luk #luk = this.#7sk[(num >= 0) ? num : Math.Max(0, ~num - 1)];
            #juk format = #luk.Format;
            format.IsItalic = flag;
            this.#itk(#VTc, #5Ue, #muk.#ute, format, num, #luk);
        }

        public void SetFontWeight(int #VTc, int #5Ue, FontWeights #vte)
        {
            this.#ltk(#VTc, #5Ue);
            bool flag = #vte >= FontWeights.Bold;
            if (this.AreMetricsValid)
            {
                this.#rtk();
            }
            int num = this.#htk(#VTc);
            #luk #luk = this.#7sk[(num >= 0) ? num : Math.Max(0, ~num - 1)];
            #juk format = #luk.Format;
            format.IsBold = flag;
            this.#itk(#VTc, #5Ue, #muk.#vte, format, num, #luk);
        }

        public void SetForeground(int #VTc, int #5Ue, Color? #hwf)
        {
            this.#ltk(#VTc, #5Ue);
            if (#hwf == null)
            {
                throw new ArgumentNullException(#G.#eg(0xbe2));
            }
            if (this.AreMetricsValid)
            {
                this.#rtk();
            }
            int num = this.#htk(#VTc);
            #luk #luk = this.#7sk[(num >= 0) ? num : Math.Max(0, ~num - 1)];
            #juk format = #luk.Format;
            format.ForegroundIndex = this.Cache.#zsk(#hwf);
            this.#itk(#VTc, #5Ue, #muk.#T7b, format, num, #luk);
        }

        public void SetStrikethrough(int #VTc, int #5Ue, bool #mtk)
        {
            LineKind kind;
            if (0 != 0)
            {
                int local1 = #mtk ? 1 : 0;
            }
            else
            {
                kind = #mtk ? LineKind.Solid : LineKind.None;
            }
            Color? nullable = null;
            this.SetStrikethrough(#VTc, #5Ue, kind, nullable, TextLineWeight.Single);
        }

        public void SetStrikethrough(int #VTc, int #5Ue, LineKind #Msk, Color? #hwf, TextLineWeight #ntk = 0)
        {
            this.#ltk(#VTc, #5Ue);
            if (this.AreMetricsValid)
            {
                this.#rtk();
            }
            int num = this.#htk(#VTc);
            #luk #luk = this.#7sk[(num >= 0) ? num : Math.Max(0, ~num - 1)];
            #juk format = #luk.Format;
            format.StrikethroughKind = #Msk;
            format.StrikethroughWeight = #ntk;
            format.StrikethroughBrushIndex = (#hwf != null) ? this.Cache.#zsk(#hwf) : -1;
            this.#itk(#VTc, #5Ue, #muk.#8Zf, format, num, #luk);
        }

        public void SetUnderline(int #VTc, int #5Ue, bool #otk)
        {
            LineKind kind;
            if (0 != 0)
            {
                int local1 = #otk ? 1 : 0;
            }
            else
            {
                kind = #otk ? LineKind.Solid : LineKind.None;
            }
            Color? nullable = null;
            this.SetUnderline(#VTc, #5Ue, kind, nullable, TextLineWeight.Single);
        }

        public void SetUnderline(int #VTc, int #5Ue, LineKind #Msk, Color? #hwf, TextLineWeight #ntk = 0)
        {
            this.#ltk(#VTc, #5Ue);
            if (this.AreMetricsValid)
            {
                this.#rtk();
            }
            int num = this.#htk(#VTc);
            #luk #luk = this.#7sk[(num >= 0) ? num : Math.Max(0, ~num - 1)];
            #juk format = #luk.Format;
            format.UnderlineKind = #Msk;
            format.UnderlineWeight = #ntk;
            format.UnderlineBrushIndex = (#hwf != null) ? this.Cache.#zsk(#hwf) : -1;
            this.#itk(#VTc, #5Ue, #muk.#7Zf, format, num, #luk);
        }

        internal #Gsk Cache { get; private set; }

        internal SpacerTextProvider TextProviderInternal { get; private set; }

        public IList<ITextSpacer> Spacers =>
            this.TextProviderInternal.Spacers;

        public ITextProvider TextProvider =>
            this.TextProviderInternal;

        private bool AreMetricsValid =>
            this.#62c != null;

        public IList<ITextLayoutLine> Lines
        {
            get
            {
                this.#qtk();
                return this.#62c;
            }
        }

        public int TabSize
        {
            get => 
                this.#51c;
            set
            {
                value = Math.Max(1, Math.Min(0x10, value));
                if (this.#51c != value)
                {
                    this.#51c = value;
                    this.#rtk();
                }
            }
        }

        public TextLayoutWrapping TextWrapping
        {
            get => 
                this.#8sk;
            set
            {
                if (this.#8sk != value)
                {
                    this.#8sk = value;
                    this.#rtk();
                }
            }
        }

        public int SpaceWidth
        {
            get
            {
                int? nullable = this.#btk;
                return ((&nullable != null) ? nullable.GetValueOrDefault() : 8);
            }
        }
    }
}

