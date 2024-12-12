namespace ActiproSoftware.WinUICore.Rendering
{
    using #H;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class SpacerTextProvider : ITextProvider
    {
        private ITextSpacer[] #Usk;
        private ITextProvider #Vsk;
        internal const string #Wsk = "_";

        internal ITextSpacer #Xsk(int #VTc)
        {
            if (this.#Usk != null)
            {
                foreach (ITextSpacer spacer in this.#Usk)
                {
                    if (spacer.CharacterIndex == #VTc)
                    {
                        return spacer;
                    }
                }
            }
            return null;
        }

        private void #Ysk(ITextSpacer #Zsk)
        {
            if (#Zsk == null)
            {
                throw new ArgumentNullException(#G.#eg(0xb69));
            }
            if (this.#Usk == null)
            {
                this.#Usk = new ITextSpacer[] { #Zsk };
            }
            else
            {
                Array.Resize<ITextSpacer>(ref this.#Usk, this.#Usk.Length + 1);
                this.#Usk[this.#Usk.Length - 1] = #Zsk;
                Comparison<ITextSpacer> comparison = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Comparison<ITextSpacer> local1 = <>c.<>9__5_0;
                    comparison = <>c.<>9__5_0 = new Comparison<ITextSpacer>(this.#Buk);
                }
                Array.Sort<ITextSpacer>(this.#Usk, comparison);
            }
        }

        public SpacerTextProvider(ITextProvider wrappedProvider, IEnumerable<ITextSpacer> spacers)
        {
            if (wrappedProvider == null)
            {
                throw new ArgumentNullException(#G.#eg(0xb54));
            }
            this.#Vsk = wrappedProvider;
            if (spacers != null)
            {
                foreach (ITextSpacer spacer in spacers)
                {
                    this.#Ysk(spacer);
                }
            }
        }

        public string GetSubstring(int #VTc, int #5Ue)
        {
            if (this.#Usk == null)
            {
                return this.#Vsk.GetSubstring(#VTc, #5Ue);
            }
            int num = 0;
            int characterCount = #5Ue;
            StringBuilder builder = new StringBuilder();
            ITextSpacer[] spacerArray = this.#Usk;
            int index = 0;
            goto TR_000D;
        TR_0002:
            if (characterCount > 0)
            {
                builder.Append(this.#Vsk.GetSubstring(#VTc - num, characterCount));
            }
            return builder.ToString();
        TR_000D:
            while (true)
            {
                if (index < spacerArray.Length)
                {
                    ITextSpacer spacer = spacerArray[index];
                    if (#VTc < spacer.CharacterIndex)
                    {
                        int num4 = Math.Min(characterCount, spacer.CharacterIndex - #VTc);
                        builder.Append(this.#Vsk.GetSubstring(#VTc - num, num4));
                        #VTc += num4;
                        characterCount -= num4;
                    }
                    if (characterCount > 0)
                    {
                        num++;
                        if (#VTc != spacer.CharacterIndex)
                        {
                            break;
                        }
                        builder.Append(#G.#eg(0xb72));
                        #VTc++;
                        if (--characterCount > 0)
                        {
                            break;
                        }
                    }
                    goto TR_0002;
                }
                else
                {
                    goto TR_0002;
                }
                break;
            }
            index++;
            goto TR_000D;
        }

        public int Translate(int #VTc, TextProviderTranslateModes #BZd)
        {
            #BZd &= ~TextProviderTranslateModes.PositiveTracking;
            if (this.#Usk != null)
            {
                ITextSpacer[] spacerArray;
                int num;
                switch (#BZd)
                {
                    case TextProviderTranslateModes.FromSourceText:
                    case TextProviderTranslateModes.PositiveTracking:
                    {
                        #VTc = this.#Vsk.Translate(#VTc, #BZd | TextProviderTranslateModes.PositiveTracking);
                        bool flag = #VTc >= 0;
                        if (!flag)
                        {
                            #VTc = ~#VTc;
                        }
                        spacerArray = this.#Usk;
                        num = 0;
                        while ((num < spacerArray.Length) && (spacerArray[num].CharacterIndex < (#VTc + (flag ? 1 : 0))))
                        {
                            #VTc++;
                            num++;
                        }
                        return #VTc;
                    }
                    case TextProviderTranslateModes.ToSourceText:
                    case (TextProviderTranslateModes.PositiveTracking | TextProviderTranslateModes.ToSourceText):
                    {
                        bool flag2 = false;
                        int characterIndex = #VTc;
                        spacerArray = this.#Usk;
                        num = 0;
                        while (true)
                        {
                            if (num < spacerArray.Length)
                            {
                                ITextSpacer spacer = spacerArray[num];
                                if (spacer.CharacterIndex < #VTc)
                                {
                                    flag2 |= spacer.CharacterIndex == (#VTc - 1);
                                    characterIndex--;
                                    num++;
                                    continue;
                                }
                            }
                            if (flag2)
                            {
                                #BZd |= TextProviderTranslateModes.PositiveTracking;
                            }
                            return this.#Vsk.Translate(characterIndex, #BZd);
                        }
                    }
                    default:
                        break;
                }
            }
            return this.#Vsk.Translate(#VTc, #BZd);
        }

        public int Length =>
            this.#Vsk.Length + ((this.#Usk != null) ? this.#Usk.Length : 0);

        public IList<ITextSpacer> Spacers =>
            this.#Usk;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SpacerTextProvider.<>c <>9 = new SpacerTextProvider.<>c();
            public static Comparison<ITextSpacer> <>9__5_0;

            internal int #Buk(ITextSpacer #Zn, ITextSpacer #0n)
            {
                int characterIndex = #Zn.CharacterIndex;
                return characterIndex.CompareTo(#0n.CharacterIndex);
            }
        }
    }
}

