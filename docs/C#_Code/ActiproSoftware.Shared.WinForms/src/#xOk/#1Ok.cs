namespace #xOk
{
    using #H;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class #1Ok
    {
        public static string #0Ok(#S7b #S7b)
        {
            switch (#S7b)
            {
                case #S7b.#gPk:
                    return #G.#eg(0x64f);

                case #S7b.#hPk:
                    return #G.#eg(0x658);

                case #S7b.#iPk:
                    return #G.#eg(0x671);

                case #S7b.#jPk:
                    return #G.#eg(0x682);

                case #S7b.#kPk:
                    return #G.#eg(0x693);

                case #S7b.#lPk:
                    return #G.#eg(0x6a0);

                case #S7b.#mPk:
                    return #G.#eg(0x6ad);

                case #S7b.#nPk:
                    return #G.#eg(0x6b6);

                case #S7b.#oPk:
                    return #G.#eg(0x6cf);

                case #S7b.#pPk:
                    return #G.#eg(0x6dc);

                case #S7b.#qPk:
                    return #G.#eg(0x6e5);

                case #S7b.#rPk:
                    return #G.#eg(0x6f2);

                case #S7b.#sPk:
                    return #G.#eg(0x6fb);

                case #S7b.#tPk:
                    return #G.#eg(0x70c);

                case #S7b.#uPk:
                    return #G.#eg(0x721);

                case #S7b.#vPk:
                    return #G.#eg(0x72e);

                case #S7b.#wPk:
                    return #G.#eg(0x73b);

                case #S7b.#xPk:
                    return #G.#eg(0x748);

                case #S7b.#yPk:
                    return #G.#eg(0x751);

                case #S7b.#zPk:
                    return #G.#eg(0x75a);

                case #S7b.#APk:
                    return #G.#eg(0x76b);

                case #S7b.#BPk:
                    return #G.#eg(0x780);

                case #S7b.#CPk:
                    return #G.#eg(0x79d);

                case #S7b.#DPk:
                    return #G.#eg(0x7b2);

                case #S7b.#EPk:
                    return #G.#eg(0x7bb);

                case #S7b.#FPk:
                    return #G.#eg(0x7c4);

                case #S7b.#GPk:
                    return #G.#eg(0x7d1);

                case #S7b.#HPk:
                    return #G.#eg(0x7ea);

                case #S7b.#IPk:
                    return #G.#eg(0x803);

                case #S7b.#JPk:
                    return #G.#eg(0x820);

                case #S7b.#KPk:
                    return #G.#eg(0x83d);

                case #S7b.#LPk:
                    return #G.#eg(0x856);

                case #S7b.#MPk:
                    return #G.#eg(0x873);

                case #S7b.#NPk:
                    return #G.#eg(0x88c);

                case #S7b.#OPk:
                    return #G.#eg(0x8a5);

                case #S7b.#PPk:
                    return #G.#eg(0x8ba);

                case #S7b.#QPk:
                    return #G.#eg(0x8d3);

                case #S7b.#RPk:
                    return #G.#eg(0x8ec);

                case #S7b.#SPk:
                    return #G.#eg(0x8f9);

                case #S7b.#TPk:
                    return #G.#eg(0x90a);

                case #S7b.#UPk:
                    return #G.#eg(0x91f);

                case #S7b.#VPk:
                    return #G.#eg(0x938);

                case #S7b.#WPk:
                    return #G.#eg(0x94d);

                case #S7b.#XPk:
                    return #G.#eg(0x95a);

                case #S7b.#YPk:
                    return #G.#eg(0x967);

                case #S7b.#ZPk:
                    return #G.#eg(0x974);

                case #S7b.#0Pk:
                    return #G.#eg(0x985);

                case #S7b.#1Pk:
                    return #G.#eg(0x996);

                case #S7b.#2Pk:
                    return #G.#eg(0x9ac);

                case #S7b.#3Pk:
                    return #G.#eg(0x99f);

                case #S7b.#4Pk:
                    return #G.#eg(0x9c9);

                case #S7b.#5Pk:
                    return #G.#eg(0x9d2);

                case #S7b.#6Pk:
                    return #G.#eg(0x9df);

                case #S7b.#7Pk:
                    return #G.#eg(0x9f4);

                case #S7b.#8Pk:
                    return #G.#eg(0xa0d);

                case #S7b.#9Pk:
                    return #G.#eg(0xa22);

                case #S7b.#aQk:
                    return #G.#eg(0xa2b);

                case #S7b.#bQk:
                    return #G.#eg(0xa44);

                case #S7b.#cQk:
                    return #G.#eg(0xa4d);

                case #S7b.#dQk:
                    return #G.#eg(0xa5e);

                case #S7b.#eQk:
                    return #G.#eg(0xa6f);

                case #S7b.#fQk:
                    return #G.#eg(0xa7c);

                case #S7b.#gQk:
                    return #G.#eg(0xa85);

                case #S7b.#hQk:
                    return #G.#eg(0xa9e);

                case #S7b.#iQk:
                    return #G.#eg(0xaa7);

                case #S7b.#jQk:
                    return #G.#eg(0xab0);
            }
            throw new ArgumentOutOfRangeException(#G.#eg(0xac1));
        }

        public #1Ok(string language, params #S7b[] fontFamilyNames)
        {
            if (fontFamilyNames == null)
            {
                throw new ArgumentNullException(#G.#eg(0x63a));
            }
            this.Language = language;
            this.FontFamilyNames = new List<#S7b>();
            if (fontFamilyNames != null)
            {
                this.FontFamilyNames.AddRange(fontFamilyNames);
            }
        }

        public List<#S7b> FontFamilyNames { get; private set; }

        public string Language { get; private set; }
    }
}

