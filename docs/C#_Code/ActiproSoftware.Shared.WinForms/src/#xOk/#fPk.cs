namespace #xOk
{
    using #H;
    using ActiproSoftware.WinUICore.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class #fPk
    {
        public #fPk(ActiproSoftware.WinUICore.Rendering.UnicodeRange unicodeRange, params #1Ok[] maps)
        {
            this.UnicodeRange = unicodeRange;
            this.Maps = new List<#1Ok>();
            if (maps != null)
            {
                this.Maps.AddRange(maps);
            }
        }

        public override string ToString()
        {
            string text1;
            string text2;
            int num1;
            string[] textArray1 = new string[5];
            string[] textArray2 = new string[5];
            textArray2[0] = #G.#eg(0xb0c);
            ActiproSoftware.WinUICore.Rendering.UnicodeRange unicodeRange = this.UnicodeRange;
            string[] textArray3 = textArray1;
            if (unicodeRange != null)
            {
                text1 = unicodeRange.ToString();
                text2 = text1;
                num1 = (int) text2;
            }
            else
            {
                ActiproSoftware.WinUICore.Rendering.UnicodeRange local1 = unicodeRange;
                text1 = null;
                text2 = text1;
                num1 = (int) text2;
            }
            num1[(int) text2] = (int) text1;
            string[] local2 = textArray3;
            local2[2] = #G.#eg(0xb39);
            local2[3] = this.Maps.Count.ToString();
            local2[4] = #G.#eg(0xb4a);
            return string.Concat(local2);
        }

        public List<#1Ok> Maps { get; private set; }

        public ActiproSoftware.WinUICore.Rendering.UnicodeRange UnicodeRange { get; private set; }
    }
}

