namespace #xOk
{
    using #H;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class #m7k
    {
        public static IList<#m7k> #j7k(int #YJd, #Bi.#FTk[] #1Sk, #Bi.#iTk[] #YSk, IList<#luk> #7sk)
        {
            List<#m7k> list = new List<#m7k>(#1Sk.Length);
            int num = 0;
            int count = #7sk.Count;
            int num3 = 0;
            int length = #1Sk.Length;
            int index = 0;
            while (num < #YJd)
            {
                int num6 = #YJd;
                int num7 = #YJd;
                if ((index + 1) < length)
                {
                    num7 = #1Sk[index + 1].#ETk;
                    num6 = Math.Min(num6, num7);
                }
                int characterIndex = #YJd;
                if ((num3 + 1) < count)
                {
                    characterIndex = #7sk[num3 + 1].CharacterIndex;
                    num6 = Math.Min(num6, characterIndex);
                }
                #m7k item = new #m7k();
                item.StartCharacterIndex = num;
                item.EndCharacterIndex = num6;
                item.Analysis = #1Sk[index].#lAf;
                item.Format = #7sk[num3].Format;
                item.ScriptTag = #YSk[index];
                list.Add(item);
                num = num6;
                if (num7 == num)
                {
                    index++;
                }
                if (characterIndex == num)
                {
                    num3++;
                }
            }
            return list;
        }

        public override string ToString() => 
            string.Format(#G.#eg(0xde2), this.StartCharacterIndex, this.EndCharacterIndex);

        public #Bi.#pTk Analysis { get; set; }

        public int EndCharacterIndex { get; set; }

        public #juk Format { get; set; }

        public #Bi.#iTk ScriptTag { get; set; }

        public int StartCharacterIndex { get; set; }
    }
}

