namespace ActiproSoftware.WinUICore.Rendering
{
    using #H;
    using #xOk;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class FontFamilyMapRepository
    {
        internal void #3Ok()
        {
            string ietfLanguageTag = CultureInfo.CurrentCulture.IetfLanguageTag;
            if (!string.IsNullOrEmpty(ietfLanguageTag))
            {
                int index = this.MapSets.Count - 1;
                while (index >= 0)
                {
                    #fPk pk = this.MapSets[index];
                    HashSet<#S7b> set = new HashSet<#S7b>();
                    int num2 = pk.Maps.Count - 1;
                    while (true)
                    {
                        if (num2 < 0)
                        {
                            if (pk.Maps.Count == 0)
                            {
                                this.MapSets.RemoveAt(index);
                            }
                            index--;
                            break;
                        }
                        #1Ok ok = pk.Maps[num2];
                        if (!string.IsNullOrEmpty(ok.Language) && !ietfLanguageTag.StartsWith(ok.Language, StringComparison.OrdinalIgnoreCase))
                        {
                            pk.Maps.RemoveAt(num2);
                        }
                        else
                        {
                            int num3 = ok.FontFamilyNames.Count - 1;
                            while (true)
                            {
                                if (num3 < 0)
                                {
                                    if (ok.FontFamilyNames.Count == 0)
                                    {
                                        pk.Maps.RemoveAt(num2);
                                    }
                                    break;
                                }
                                #S7b item = ok.FontFamilyNames[num3];
                                if (set.Contains(item))
                                {
                                    ok.FontFamilyNames.RemoveAt(num3);
                                }
                                else
                                {
                                    set.Add(item);
                                }
                                num3--;
                            }
                        }
                        num2--;
                    }
                }
            }
        }

        public static FontFamilyMapRepository #3x()
        {
            #fPk[] pkArray1 = new #fPk[0x9f];
            #fPk[] pkArray2 = new #fPk[0x9f];
            #S7b[] fontFamilyNames = new #S7b[8];
            fontFamilyNames[0] = #S7b.#5Pk;
            fontFamilyNames[1] = #S7b.#fQk;
            fontFamilyNames[3] = #S7b.#aQk;
            fontFamilyNames[4] = #S7b.#gQk;
            fontFamilyNames[5] = #S7b.#hPk;
            fontFamilyNames[6] = #S7b.#LPk;
            fontFamilyNames[7] = #S7b.#BPk;
            #1Ok[] okArray159 = new #1Ok[] { new #1Ok(null, fontFamilyNames) };
            #1Ok[] maps = new #1Ok[] { new #1Ok(null, fontFamilyNames) };
            pkArray2[0] = new #fPk(new UnicodeRange(0, 0x52f), maps);
            #S7b[] #sbArray2 = new #S7b[] { #S7b.#5Pk, #S7b.#eQk, #S7b.#hPk };
            #1Ok[] okArray1 = new #1Ok[] { new #1Ok(null, #sbArray2) };
            #fPk[] mapSets = pkArray2;
            mapSets[1] = new #fPk(new UnicodeRange(0x530, 0x58f), okArray1);
            #S7b[] #sbArray3 = new #S7b[8];
            #sbArray3[0] = #S7b.#5Pk;
            #sbArray3[1] = #S7b.#fQk;
            #sbArray3[3] = #S7b.#aQk;
            #sbArray3[4] = #S7b.#gQk;
            #sbArray3[5] = #S7b.#hPk;
            #sbArray3[6] = #S7b.#LPk;
            #sbArray3[7] = #S7b.#BPk;
            #1Ok[] okArray2 = new #1Ok[] { new #1Ok(null, #sbArray3) };
            mapSets[2] = new #fPk(new UnicodeRange(0x590, 0x6ff), okArray2);
            #S7b[] #sbArray4 = new #S7b[] { #S7b.#7Pk, #S7b.#nPk };
            #1Ok[] okArray3 = new #1Ok[] { new #1Ok(null, #sbArray4) };
            mapSets[3] = new #fPk(new UnicodeRange(0x700, 0x74f), okArray3);
            #S7b[] #sbArray5 = new #S7b[8];
            #sbArray5[0] = #S7b.#5Pk;
            #sbArray5[1] = #S7b.#fQk;
            #sbArray5[3] = #S7b.#aQk;
            #sbArray5[4] = #S7b.#gQk;
            #sbArray5[5] = #S7b.#hPk;
            #sbArray5[6] = #S7b.#LPk;
            #sbArray5[7] = #S7b.#BPk;
            #1Ok[] okArray4 = new #1Ok[] { new #1Ok(null, #sbArray5) };
            mapSets[4] = new #fPk(new UnicodeRange(0x750, 0x77f), okArray4);
            #S7b[] #sbArray6 = new #S7b[] { #S7b.#YPk };
            #1Ok[] okArray5 = new #1Ok[] { new #1Ok(null, #sbArray6) };
            mapSets[5] = new #fPk(new UnicodeRange(0x780, 0x7bf), okArray5);
            #S7b[] #sbArray7 = new #S7b[] { #S7b.#mPk };
            #1Ok[] okArray6 = new #1Ok[] { new #1Ok(null, #sbArray7) };
            mapSets[6] = new #fPk(new UnicodeRange(0x7c0, 0x7ff), okArray6);
            #S7b[] #sbArray8 = new #S7b[8];
            #sbArray8[0] = #S7b.#5Pk;
            #sbArray8[1] = #S7b.#fQk;
            #sbArray8[3] = #S7b.#aQk;
            #sbArray8[4] = #S7b.#gQk;
            #sbArray8[5] = #S7b.#hPk;
            #sbArray8[6] = #S7b.#LPk;
            #sbArray8[7] = #S7b.#BPk;
            #1Ok[] okArray7 = new #1Ok[] { new #1Ok(null, #sbArray8) };
            mapSets[7] = new #fPk(new UnicodeRange(0x8a0, 0x8ff), okArray7);
            #S7b[] #sbArray9 = new #S7b[] { #S7b.#0Pk, #S7b.#DPk };
            #1Ok[] okArray8 = new #1Ok[] { new #1Ok(null, #sbArray9) };
            mapSets[8] = new #fPk(new UnicodeRange(0x900, 0x97f), okArray8);
            #S7b[] #sbArray10 = new #S7b[] { #S7b.#0Pk, #S7b.#iQk };
            #1Ok[] okArray9 = new #1Ok[] { new #1Ok(null, #sbArray10) };
            mapSets[9] = new #fPk(new UnicodeRange(0x980, 0x9ff), okArray9);
            #S7b[] #sbArray11 = new #S7b[] { #S7b.#0Pk, #S7b.#4Pk };
            #1Ok[] okArray10 = new #1Ok[] { new #1Ok(null, #sbArray11) };
            mapSets[10] = new #fPk(new UnicodeRange(0xa00, 0xa7f), okArray10);
            #S7b[] #sbArray12 = new #S7b[] { #S7b.#0Pk, #S7b.#9Pk };
            #1Ok[] okArray11 = new #1Ok[] { new #1Ok(null, #sbArray12) };
            mapSets[11] = new #fPk(new UnicodeRange(0xa80, 0xaff), okArray11);
            #S7b[] #sbArray13 = new #S7b[] { #S7b.#0Pk, #S7b.#uPk };
            #1Ok[] okArray12 = new #1Ok[] { new #1Ok(null, #sbArray13) };
            mapSets[12] = new #fPk(new UnicodeRange(0xb00, 0xb7f), okArray12);
            #S7b[] #sbArray14 = new #S7b[] { #S7b.#0Pk, #S7b.#yPk };
            #1Ok[] okArray13 = new #1Ok[] { new #1Ok(null, #sbArray14) };
            mapSets[13] = new #fPk(new UnicodeRange(0xb80, 0xbff), okArray13);
            #S7b[] #sbArray15 = new #S7b[] { #S7b.#0Pk, #S7b.#qPk };
            #1Ok[] okArray14 = new #1Ok[] { new #1Ok(null, #sbArray15) };
            mapSets[14] = new #fPk(new UnicodeRange(0xc00, 0xc7f), okArray14);
            #S7b[] #sbArray16 = new #S7b[] { #S7b.#0Pk, #S7b.#hQk };
            #1Ok[] okArray15 = new #1Ok[] { new #1Ok(null, #sbArray16) };
            mapSets[15] = new #fPk(new UnicodeRange(0xc80, 0xcff), okArray15);
            #S7b[] #sbArray17 = new #S7b[] { #S7b.#0Pk, #S7b.#vPk };
            #1Ok[] okArray16 = new #1Ok[] { new #1Ok(null, #sbArray17) };
            mapSets[0x10] = new #fPk(new UnicodeRange(0xd00, 0xd7f), okArray16);
            #S7b[] #sbArray18 = new #S7b[] { #S7b.#0Pk, #S7b.#sPk };
            #1Ok[] okArray17 = new #1Ok[] { new #1Ok(null, #sbArray18) };
            mapSets[0x11] = new #fPk(new UnicodeRange(0xd80, 0xdff), okArray17);
            #S7b[] #sbArray19 = new #S7b[] { #S7b.#APk, #S7b.#zPk, #S7b.#fQk };
            #1Ok[] okArray18 = new #1Ok[] { new #1Ok(null, #sbArray19) };
            mapSets[0x12] = new #fPk(new UnicodeRange(0xe00, 0xe7f), okArray18);
            #S7b[] #sbArray20 = new #S7b[] { #S7b.#APk, #S7b.#xPk, #S7b.#lPk };
            #1Ok[] okArray19 = new #1Ok[] { new #1Ok(null, #sbArray20) };
            mapSets[0x13] = new #fPk(new UnicodeRange(0xe80, 0xeff), okArray19);
            #S7b[] #sbArray21 = new #S7b[] { #S7b.#GPk };
            #1Ok[] okArray20 = new #1Ok[] { new #1Ok(null, #sbArray21) };
            mapSets[20] = new #fPk(new UnicodeRange(0xf00, 0xfff), okArray20);
            #S7b[] #sbArray22 = new #S7b[] { #S7b.#ZPk };
            #1Ok[] okArray21 = new #1Ok[] { new #1Ok(null, #sbArray22) };
            mapSets[0x15] = new #fPk(new UnicodeRange(0x1000, 0x109f), okArray21);
            #S7b[] #sbArray23 = new #S7b[] { #S7b.#5Pk, #S7b.#eQk, #S7b.#hPk };
            #1Ok[] okArray22 = new #1Ok[] { new #1Ok(null, #sbArray23) };
            mapSets[0x16] = new #fPk(new UnicodeRange(0x10a0, 0x10ff), okArray22);
            #S7b[] #sbArray24 = new #S7b[] { #S7b.#CPk, #S7b.#rPk };
            #1Ok[] okArray23 = new #1Ok[] { new #1Ok(null, #sbArray24) };
            mapSets[0x17] = new #fPk(new UnicodeRange(0x1100, 0x11ff), okArray23);
            #S7b[] #sbArray25 = new #S7b[] { #S7b.#mPk, #S7b.#1Pk };
            #1Ok[] okArray24 = new #1Ok[] { new #1Ok(null, #sbArray25) };
            mapSets[0x18] = new #fPk(new UnicodeRange(0x1200, 0x139f), okArray24);
            #S7b[] #sbArray26 = new #S7b[] { #S7b.#pPk, #S7b.#2Pk };
            #1Ok[] okArray25 = new #1Ok[] { new #1Ok(null, #sbArray26) };
            mapSets[0x19] = new #fPk(new UnicodeRange(0x13a0, 0x13ff), okArray25);
            #S7b[] #sbArray27 = new #S7b[] { #S7b.#pPk, #S7b.#oPk };
            #1Ok[] okArray26 = new #1Ok[] { new #1Ok(null, #sbArray27) };
            mapSets[0x1a] = new #fPk(new UnicodeRange(0x1400, 0x167f), okArray26);
            #S7b[] #sbArray28 = new #S7b[] { #S7b.#7Pk, #S7b.#8Pk };
            #1Ok[] okArray27 = new #1Ok[] { new #1Ok(null, #sbArray28) };
            mapSets[0x1b] = new #fPk(new UnicodeRange(0x1680, 0x16ff), okArray27);
            #S7b[] #sbArray29 = new #S7b[] { #S7b.#APk, #S7b.#wPk, #S7b.#kPk };
            #1Ok[] okArray28 = new #1Ok[] { new #1Ok(null, #sbArray29) };
            mapSets[0x1c] = new #fPk(new UnicodeRange(0x1780, 0x17ff), okArray28);
            #S7b[] #sbArray30 = new #S7b[] { #S7b.#VPk };
            #1Ok[] okArray29 = new #1Ok[] { new #1Ok(null, #sbArray30) };
            mapSets[0x1d] = new #fPk(new UnicodeRange(0x1800, 0x18af), okArray29);
            #S7b[] #sbArray31 = new #S7b[] { #S7b.#pPk, #S7b.#oPk };
            #1Ok[] okArray30 = new #1Ok[] { new #1Ok(null, #sbArray31) };
            mapSets[30] = new #fPk(new UnicodeRange(0x18b0, 0x18ff), okArray30);
            #S7b[] #sbArray32 = new #S7b[] { #S7b.#MPk };
            #1Ok[] okArray31 = new #1Ok[] { new #1Ok(null, #sbArray32) };
            mapSets[0x1f] = new #fPk(new UnicodeRange(0x1950, 0x197f), okArray31);
            #S7b[] #sbArray33 = new #S7b[] { #S7b.#JPk };
            #1Ok[] okArray32 = new #1Ok[] { new #1Ok(null, #sbArray33) };
            mapSets[0x20] = new #fPk(new UnicodeRange(0x1980, 0x19df), okArray32);
            #S7b[] #sbArray34 = new #S7b[] { #S7b.#APk, #S7b.#wPk, #S7b.#kPk };
            #1Ok[] okArray33 = new #1Ok[] { new #1Ok(null, #sbArray34) };
            mapSets[0x21] = new #fPk(new UnicodeRange(0x19e0, 0x19ff), okArray33);
            #S7b[] #sbArray35 = new #S7b[] { #S7b.#APk };
            #1Ok[] okArray34 = new #1Ok[] { new #1Ok(null, #sbArray35) };
            mapSets[0x22] = new #fPk(new UnicodeRange(0x1a00, 0x1a1f), okArray34);
            #S7b[] #sbArray36 = new #S7b[] { #S7b.#0Pk };
            #1Ok[] okArray35 = new #1Ok[] { new #1Ok(null, #sbArray36) };
            mapSets[0x23] = new #fPk(new UnicodeRange(0x1c50, 0x1c7f), okArray35);
            #S7b[] #sbArray37 = new #S7b[8];
            #sbArray37[0] = #S7b.#5Pk;
            #sbArray37[1] = #S7b.#fQk;
            #sbArray37[3] = #S7b.#aQk;
            #sbArray37[4] = #S7b.#gQk;
            #sbArray37[5] = #S7b.#hPk;
            #sbArray37[6] = #S7b.#LPk;
            #sbArray37[7] = #S7b.#BPk;
            #1Ok[] okArray36 = new #1Ok[] { new #1Ok(null, #sbArray37) };
            mapSets[0x24] = new #fPk(new UnicodeRange(0x1c80, 0x1c8f), okArray36);
            #S7b[] #sbArray38 = new #S7b[8];
            #sbArray38[0] = #S7b.#5Pk;
            #sbArray38[1] = #S7b.#fQk;
            #sbArray38[3] = #S7b.#aQk;
            #sbArray38[4] = #S7b.#gQk;
            #sbArray38[5] = #S7b.#hPk;
            #sbArray38[6] = #S7b.#LPk;
            #sbArray38[7] = #S7b.#BPk;
            #1Ok[] okArray37 = new #1Ok[] { new #1Ok(null, #sbArray38) };
            mapSets[0x25] = new #fPk(new UnicodeRange(0x1d00, 0x1fff), okArray37);
            #S7b[] #sbArray39 = new #S7b[14];
            #sbArray39[0] = #S7b.#PPk;
            #sbArray39[1] = #S7b.#OPk;
            #sbArray39[2] = #S7b.#jQk;
            #sbArray39[3] = #S7b.#FPk;
            #sbArray39[4] = #S7b.#EPk;
            #sbArray39[5] = #S7b.#5Pk;
            #sbArray39[6] = #S7b.#8Pk;
            #sbArray39[7] = #S7b.#bQk;
            #sbArray39[8] = #S7b.#WPk;
            #sbArray39[9] = #S7b.#RPk;
            #sbArray39[10] = #S7b.#fQk;
            #sbArray39[11] = #S7b.#BPk;
            #sbArray39[13] = #S7b.#hPk;
            #1Ok[] okArray38 = new #1Ok[5];
            okArray38[0] = new #1Ok(#G.#eg(0xad6), #sbArray39);
            #S7b[] #sbArray40 = new #S7b[15];
            #sbArray40[0] = #S7b.#IPk;
            #sbArray40[1] = #S7b.#HPk;
            #sbArray40[2] = #S7b.#jQk;
            #sbArray40[3] = #S7b.#FPk;
            #sbArray40[4] = #S7b.#EPk;
            #sbArray40[5] = #S7b.#5Pk;
            #sbArray40[6] = #S7b.#8Pk;
            #sbArray40[7] = #S7b.#PPk;
            #sbArray40[8] = #S7b.#OPk;
            #sbArray40[9] = #S7b.#RPk;
            #sbArray40[10] = #S7b.#WPk;
            #sbArray40[11] = #S7b.#fQk;
            #sbArray40[12] = #S7b.#BPk;
            #sbArray40[14] = #S7b.#hPk;
            okArray38[1] = new #1Ok(#G.#eg(0xae3), #sbArray40);
            #S7b[] #sbArray41 = new #S7b[0x10];
            #sbArray41[0] = #S7b.#jQk;
            #sbArray41[1] = #S7b.#FPk;
            #sbArray41[2] = #S7b.#EPk;
            #sbArray41[3] = #S7b.#5Pk;
            #sbArray41[4] = #S7b.#8Pk;
            #sbArray41[5] = #S7b.#IPk;
            #sbArray41[6] = #S7b.#HPk;
            #sbArray41[7] = #S7b.#PPk;
            #sbArray41[8] = #S7b.#OPk;
            #sbArray41[9] = #S7b.#CPk;
            #sbArray41[10] = #S7b.#WPk;
            #sbArray41[11] = #S7b.#RPk;
            #sbArray41[12] = #S7b.#fQk;
            #sbArray41[13] = #S7b.#BPk;
            #sbArray41[15] = #S7b.#hPk;
            okArray38[2] = new #1Ok(#G.#eg(0xaf0), #sbArray41);
            #S7b[] #sbArray42 = new #S7b[0x11];
            #sbArray42[0] = #S7b.#CPk;
            #sbArray42[1] = #S7b.#5Pk;
            #sbArray42[2] = #S7b.#8Pk;
            #sbArray42[3] = #S7b.#jQk;
            #sbArray42[4] = #S7b.#FPk;
            #sbArray42[5] = #S7b.#EPk;
            #sbArray42[6] = #S7b.#IPk;
            #sbArray42[7] = #S7b.#HPk;
            #sbArray42[8] = #S7b.#PPk;
            #sbArray42[9] = #S7b.#OPk;
            #sbArray42[10] = #S7b.#rPk;
            #sbArray42[11] = #S7b.#WPk;
            #sbArray42[12] = #S7b.#RPk;
            #sbArray42[13] = #S7b.#fQk;
            #sbArray42[14] = #S7b.#BPk;
            #sbArray42[0x10] = #S7b.#hPk;
            okArray38[3] = new #1Ok(#G.#eg(0xaf5), #sbArray42);
            #S7b[] #sbArray43 = new #S7b[0x11];
            #sbArray43[0] = #S7b.#5Pk;
            #sbArray43[1] = #S7b.#8Pk;
            #sbArray43[2] = #S7b.#jQk;
            #sbArray43[3] = #S7b.#FPk;
            #sbArray43[4] = #S7b.#EPk;
            #sbArray43[5] = #S7b.#IPk;
            #sbArray43[6] = #S7b.#HPk;
            #sbArray43[7] = #S7b.#PPk;
            #sbArray43[8] = #S7b.#OPk;
            #sbArray43[9] = #S7b.#CPk;
            #sbArray43[10] = #S7b.#fQk;
            #sbArray43[11] = #S7b.#BPk;
            #sbArray43[13] = #S7b.#WPk;
            #sbArray43[14] = #S7b.#RPk;
            #sbArray43[15] = #S7b.#hPk;
            #sbArray43[0x10] = #S7b.#RPk;
            okArray38[4] = new #1Ok(null, #sbArray43);
            mapSets[0x26] = new #fPk(new UnicodeRange(0x2000, 0x202e), okArray38);
            #S7b[] #sbArray44 = new #S7b[] { #S7b.#VPk };
            #1Ok[] okArray39 = new #1Ok[] { new #1Ok(null, #sbArray44) };
            mapSets[0x27] = new #fPk(new UnicodeRange(0x202f, 0x202f), okArray39);
            #S7b[] #sbArray45 = new #S7b[14];
            #sbArray45[0] = #S7b.#PPk;
            #sbArray45[1] = #S7b.#OPk;
            #sbArray45[2] = #S7b.#jQk;
            #sbArray45[3] = #S7b.#FPk;
            #sbArray45[4] = #S7b.#EPk;
            #sbArray45[5] = #S7b.#5Pk;
            #sbArray45[6] = #S7b.#8Pk;
            #sbArray45[7] = #S7b.#bQk;
            #sbArray45[8] = #S7b.#WPk;
            #sbArray45[9] = #S7b.#RPk;
            #sbArray45[10] = #S7b.#fQk;
            #sbArray45[11] = #S7b.#BPk;
            #sbArray45[13] = #S7b.#hPk;
            #1Ok[] okArray40 = new #1Ok[5];
            okArray40[0] = new #1Ok(#G.#eg(0xad6), #sbArray45);
            #S7b[] #sbArray46 = new #S7b[15];
            #sbArray46[0] = #S7b.#IPk;
            #sbArray46[1] = #S7b.#HPk;
            #sbArray46[2] = #S7b.#jQk;
            #sbArray46[3] = #S7b.#FPk;
            #sbArray46[4] = #S7b.#EPk;
            #sbArray46[5] = #S7b.#5Pk;
            #sbArray46[6] = #S7b.#8Pk;
            #sbArray46[7] = #S7b.#PPk;
            #sbArray46[8] = #S7b.#OPk;
            #sbArray46[9] = #S7b.#RPk;
            #sbArray46[10] = #S7b.#WPk;
            #sbArray46[11] = #S7b.#fQk;
            #sbArray46[12] = #S7b.#BPk;
            #sbArray46[14] = #S7b.#hPk;
            okArray40[1] = new #1Ok(#G.#eg(0xae3), #sbArray46);
            #S7b[] #sbArray47 = new #S7b[0x10];
            #sbArray47[0] = #S7b.#jQk;
            #sbArray47[1] = #S7b.#FPk;
            #sbArray47[2] = #S7b.#EPk;
            #sbArray47[3] = #S7b.#5Pk;
            #sbArray47[4] = #S7b.#8Pk;
            #sbArray47[5] = #S7b.#IPk;
            #sbArray47[6] = #S7b.#HPk;
            #sbArray47[7] = #S7b.#PPk;
            #sbArray47[8] = #S7b.#OPk;
            #sbArray47[9] = #S7b.#CPk;
            #sbArray47[10] = #S7b.#WPk;
            #sbArray47[11] = #S7b.#RPk;
            #sbArray47[12] = #S7b.#fQk;
            #sbArray47[13] = #S7b.#BPk;
            #sbArray47[15] = #S7b.#hPk;
            okArray40[2] = new #1Ok(#G.#eg(0xaf0), #sbArray47);
            #S7b[] #sbArray48 = new #S7b[0x11];
            #sbArray48[0] = #S7b.#CPk;
            #sbArray48[1] = #S7b.#5Pk;
            #sbArray48[2] = #S7b.#8Pk;
            #sbArray48[3] = #S7b.#jQk;
            #sbArray48[4] = #S7b.#FPk;
            #sbArray48[5] = #S7b.#EPk;
            #sbArray48[6] = #S7b.#IPk;
            #sbArray48[7] = #S7b.#HPk;
            #sbArray48[8] = #S7b.#PPk;
            #sbArray48[9] = #S7b.#OPk;
            #sbArray48[10] = #S7b.#rPk;
            #sbArray48[11] = #S7b.#WPk;
            #sbArray48[12] = #S7b.#RPk;
            #sbArray48[13] = #S7b.#fQk;
            #sbArray48[14] = #S7b.#BPk;
            #sbArray48[0x10] = #S7b.#hPk;
            okArray40[3] = new #1Ok(#G.#eg(0xaf5), #sbArray48);
            #S7b[] #sbArray49 = new #S7b[0x11];
            #sbArray49[0] = #S7b.#5Pk;
            #sbArray49[1] = #S7b.#8Pk;
            #sbArray49[2] = #S7b.#jQk;
            #sbArray49[3] = #S7b.#FPk;
            #sbArray49[4] = #S7b.#EPk;
            #sbArray49[5] = #S7b.#IPk;
            #sbArray49[6] = #S7b.#HPk;
            #sbArray49[7] = #S7b.#PPk;
            #sbArray49[8] = #S7b.#OPk;
            #sbArray49[9] = #S7b.#CPk;
            #sbArray49[10] = #S7b.#fQk;
            #sbArray49[11] = #S7b.#BPk;
            #sbArray49[13] = #S7b.#WPk;
            #sbArray49[14] = #S7b.#RPk;
            #sbArray49[15] = #S7b.#hPk;
            #sbArray49[0x10] = #S7b.#RPk;
            okArray40[4] = new #1Ok(null, #sbArray49);
            mapSets[40] = new #fPk(new UnicodeRange(0x2030, 0x20cf), okArray40);
            #S7b[] #sbArray50 = new #S7b[] { #S7b.#8Pk };
            #1Ok[] okArray41 = new #1Ok[] { new #1Ok(null, #sbArray50) };
            mapSets[0x29] = new #fPk(new UnicodeRange(0x20d0, 0x20ff), okArray41);
            #S7b[] #sbArray51 = new #S7b[15];
            #sbArray51[0] = #S7b.#6Pk;
            #sbArray51[1] = #S7b.#PPk;
            #sbArray51[2] = #S7b.#OPk;
            #sbArray51[3] = #S7b.#jQk;
            #sbArray51[4] = #S7b.#FPk;
            #sbArray51[5] = #S7b.#EPk;
            #sbArray51[6] = #S7b.#5Pk;
            #sbArray51[7] = #S7b.#8Pk;
            #sbArray51[8] = #S7b.#bQk;
            #sbArray51[9] = #S7b.#WPk;
            #sbArray51[10] = #S7b.#RPk;
            #sbArray51[11] = #S7b.#fQk;
            #sbArray51[12] = #S7b.#BPk;
            #sbArray51[14] = #S7b.#hPk;
            #1Ok[] okArray42 = new #1Ok[5];
            okArray42[0] = new #1Ok(#G.#eg(0xad6), #sbArray51);
            #S7b[] #sbArray52 = new #S7b[0x10];
            #sbArray52[0] = #S7b.#6Pk;
            #sbArray52[1] = #S7b.#IPk;
            #sbArray52[2] = #S7b.#HPk;
            #sbArray52[3] = #S7b.#jQk;
            #sbArray52[4] = #S7b.#FPk;
            #sbArray52[5] = #S7b.#EPk;
            #sbArray52[6] = #S7b.#5Pk;
            #sbArray52[7] = #S7b.#8Pk;
            #sbArray52[8] = #S7b.#PPk;
            #sbArray52[9] = #S7b.#OPk;
            #sbArray52[10] = #S7b.#RPk;
            #sbArray52[11] = #S7b.#WPk;
            #sbArray52[12] = #S7b.#fQk;
            #sbArray52[13] = #S7b.#BPk;
            #sbArray52[15] = #S7b.#hPk;
            okArray42[1] = new #1Ok(#G.#eg(0xae3), #sbArray52);
            #S7b[] #sbArray53 = new #S7b[0x11];
            #sbArray53[0] = #S7b.#6Pk;
            #sbArray53[1] = #S7b.#jQk;
            #sbArray53[2] = #S7b.#FPk;
            #sbArray53[3] = #S7b.#EPk;
            #sbArray53[4] = #S7b.#5Pk;
            #sbArray53[5] = #S7b.#8Pk;
            #sbArray53[6] = #S7b.#IPk;
            #sbArray53[7] = #S7b.#HPk;
            #sbArray53[8] = #S7b.#PPk;
            #sbArray53[9] = #S7b.#OPk;
            #sbArray53[10] = #S7b.#CPk;
            #sbArray53[11] = #S7b.#WPk;
            #sbArray53[12] = #S7b.#RPk;
            #sbArray53[13] = #S7b.#fQk;
            #sbArray53[14] = #S7b.#BPk;
            #sbArray53[0x10] = #S7b.#hPk;
            okArray42[2] = new #1Ok(#G.#eg(0xaf0), #sbArray53);
            #S7b[] #sbArray54 = new #S7b[0x12];
            #sbArray54[0] = #S7b.#6Pk;
            #sbArray54[1] = #S7b.#CPk;
            #sbArray54[2] = #S7b.#5Pk;
            #sbArray54[3] = #S7b.#8Pk;
            #sbArray54[4] = #S7b.#jQk;
            #sbArray54[5] = #S7b.#FPk;
            #sbArray54[6] = #S7b.#EPk;
            #sbArray54[7] = #S7b.#IPk;
            #sbArray54[8] = #S7b.#HPk;
            #sbArray54[9] = #S7b.#PPk;
            #sbArray54[10] = #S7b.#OPk;
            #sbArray54[11] = #S7b.#rPk;
            #sbArray54[12] = #S7b.#WPk;
            #sbArray54[13] = #S7b.#RPk;
            #sbArray54[14] = #S7b.#fQk;
            #sbArray54[15] = #S7b.#BPk;
            #sbArray54[0x11] = #S7b.#hPk;
            okArray42[3] = new #1Ok(#G.#eg(0xaf5), #sbArray54);
            #S7b[] #sbArray55 = new #S7b[0x12];
            #sbArray55[0] = #S7b.#6Pk;
            #sbArray55[1] = #S7b.#5Pk;
            #sbArray55[2] = #S7b.#8Pk;
            #sbArray55[3] = #S7b.#jQk;
            #sbArray55[4] = #S7b.#FPk;
            #sbArray55[5] = #S7b.#EPk;
            #sbArray55[6] = #S7b.#IPk;
            #sbArray55[7] = #S7b.#HPk;
            #sbArray55[8] = #S7b.#PPk;
            #sbArray55[9] = #S7b.#OPk;
            #sbArray55[10] = #S7b.#CPk;
            #sbArray55[11] = #S7b.#fQk;
            #sbArray55[12] = #S7b.#BPk;
            #sbArray55[14] = #S7b.#WPk;
            #sbArray55[15] = #S7b.#RPk;
            #sbArray55[0x10] = #S7b.#hPk;
            #sbArray55[0x11] = #S7b.#RPk;
            okArray42[4] = new #1Ok(null, #sbArray55);
            mapSets[0x2a] = new #fPk(new UnicodeRange(0x2100, 0x214f), okArray42);
            #S7b[] #sbArray56 = new #S7b[14];
            #sbArray56[0] = #S7b.#PPk;
            #sbArray56[1] = #S7b.#OPk;
            #sbArray56[2] = #S7b.#jQk;
            #sbArray56[3] = #S7b.#FPk;
            #sbArray56[4] = #S7b.#EPk;
            #sbArray56[5] = #S7b.#5Pk;
            #sbArray56[6] = #S7b.#8Pk;
            #sbArray56[7] = #S7b.#bQk;
            #sbArray56[8] = #S7b.#WPk;
            #sbArray56[9] = #S7b.#RPk;
            #sbArray56[10] = #S7b.#fQk;
            #sbArray56[11] = #S7b.#BPk;
            #sbArray56[13] = #S7b.#hPk;
            #1Ok[] okArray43 = new #1Ok[5];
            okArray43[0] = new #1Ok(#G.#eg(0xad6), #sbArray56);
            #S7b[] #sbArray57 = new #S7b[15];
            #sbArray57[0] = #S7b.#IPk;
            #sbArray57[1] = #S7b.#HPk;
            #sbArray57[2] = #S7b.#jQk;
            #sbArray57[3] = #S7b.#FPk;
            #sbArray57[4] = #S7b.#EPk;
            #sbArray57[5] = #S7b.#5Pk;
            #sbArray57[6] = #S7b.#8Pk;
            #sbArray57[7] = #S7b.#PPk;
            #sbArray57[8] = #S7b.#OPk;
            #sbArray57[9] = #S7b.#RPk;
            #sbArray57[10] = #S7b.#WPk;
            #sbArray57[11] = #S7b.#fQk;
            #sbArray57[12] = #S7b.#BPk;
            #sbArray57[14] = #S7b.#hPk;
            okArray43[1] = new #1Ok(#G.#eg(0xae3), #sbArray57);
            #S7b[] #sbArray58 = new #S7b[0x10];
            #sbArray58[0] = #S7b.#jQk;
            #sbArray58[1] = #S7b.#FPk;
            #sbArray58[2] = #S7b.#EPk;
            #sbArray58[3] = #S7b.#5Pk;
            #sbArray58[4] = #S7b.#8Pk;
            #sbArray58[5] = #S7b.#IPk;
            #sbArray58[6] = #S7b.#HPk;
            #sbArray58[7] = #S7b.#PPk;
            #sbArray58[8] = #S7b.#OPk;
            #sbArray58[9] = #S7b.#CPk;
            #sbArray58[10] = #S7b.#WPk;
            #sbArray58[11] = #S7b.#RPk;
            #sbArray58[12] = #S7b.#fQk;
            #sbArray58[13] = #S7b.#BPk;
            #sbArray58[15] = #S7b.#hPk;
            okArray43[2] = new #1Ok(#G.#eg(0xaf0), #sbArray58);
            #S7b[] #sbArray59 = new #S7b[0x11];
            #sbArray59[0] = #S7b.#CPk;
            #sbArray59[1] = #S7b.#5Pk;
            #sbArray59[2] = #S7b.#8Pk;
            #sbArray59[3] = #S7b.#jQk;
            #sbArray59[4] = #S7b.#FPk;
            #sbArray59[5] = #S7b.#EPk;
            #sbArray59[6] = #S7b.#IPk;
            #sbArray59[7] = #S7b.#HPk;
            #sbArray59[8] = #S7b.#PPk;
            #sbArray59[9] = #S7b.#OPk;
            #sbArray59[10] = #S7b.#rPk;
            #sbArray59[11] = #S7b.#WPk;
            #sbArray59[12] = #S7b.#RPk;
            #sbArray59[13] = #S7b.#fQk;
            #sbArray59[14] = #S7b.#BPk;
            #sbArray59[0x10] = #S7b.#hPk;
            okArray43[3] = new #1Ok(#G.#eg(0xaf5), #sbArray59);
            #S7b[] #sbArray60 = new #S7b[0x11];
            #sbArray60[0] = #S7b.#5Pk;
            #sbArray60[1] = #S7b.#8Pk;
            #sbArray60[2] = #S7b.#jQk;
            #sbArray60[3] = #S7b.#FPk;
            #sbArray60[4] = #S7b.#EPk;
            #sbArray60[5] = #S7b.#IPk;
            #sbArray60[6] = #S7b.#HPk;
            #sbArray60[7] = #S7b.#PPk;
            #sbArray60[8] = #S7b.#OPk;
            #sbArray60[9] = #S7b.#CPk;
            #sbArray60[10] = #S7b.#fQk;
            #sbArray60[11] = #S7b.#BPk;
            #sbArray60[13] = #S7b.#WPk;
            #sbArray60[14] = #S7b.#RPk;
            #sbArray60[15] = #S7b.#hPk;
            #sbArray60[0x10] = #S7b.#RPk;
            okArray43[4] = new #1Ok(null, #sbArray60);
            mapSets[0x2b] = new #fPk(new UnicodeRange(0x2150, 0x22ff), okArray43);
            #S7b[] #sbArray61 = new #S7b[15];
            #sbArray61[0] = #S7b.#6Pk;
            #sbArray61[1] = #S7b.#PPk;
            #sbArray61[2] = #S7b.#OPk;
            #sbArray61[3] = #S7b.#jQk;
            #sbArray61[4] = #S7b.#FPk;
            #sbArray61[5] = #S7b.#EPk;
            #sbArray61[6] = #S7b.#5Pk;
            #sbArray61[7] = #S7b.#8Pk;
            #sbArray61[8] = #S7b.#bQk;
            #sbArray61[9] = #S7b.#WPk;
            #sbArray61[10] = #S7b.#RPk;
            #sbArray61[11] = #S7b.#fQk;
            #sbArray61[12] = #S7b.#BPk;
            #sbArray61[14] = #S7b.#hPk;
            #1Ok[] okArray44 = new #1Ok[5];
            okArray44[0] = new #1Ok(#G.#eg(0xad6), #sbArray61);
            #S7b[] #sbArray62 = new #S7b[0x10];
            #sbArray62[0] = #S7b.#6Pk;
            #sbArray62[1] = #S7b.#IPk;
            #sbArray62[2] = #S7b.#HPk;
            #sbArray62[3] = #S7b.#jQk;
            #sbArray62[4] = #S7b.#FPk;
            #sbArray62[5] = #S7b.#EPk;
            #sbArray62[6] = #S7b.#5Pk;
            #sbArray62[7] = #S7b.#8Pk;
            #sbArray62[8] = #S7b.#PPk;
            #sbArray62[9] = #S7b.#OPk;
            #sbArray62[10] = #S7b.#RPk;
            #sbArray62[11] = #S7b.#WPk;
            #sbArray62[12] = #S7b.#fQk;
            #sbArray62[13] = #S7b.#BPk;
            #sbArray62[15] = #S7b.#hPk;
            okArray44[1] = new #1Ok(#G.#eg(0xae3), #sbArray62);
            #S7b[] #sbArray63 = new #S7b[0x11];
            #sbArray63[0] = #S7b.#6Pk;
            #sbArray63[1] = #S7b.#jQk;
            #sbArray63[2] = #S7b.#FPk;
            #sbArray63[3] = #S7b.#EPk;
            #sbArray63[4] = #S7b.#5Pk;
            #sbArray63[5] = #S7b.#8Pk;
            #sbArray63[6] = #S7b.#IPk;
            #sbArray63[7] = #S7b.#HPk;
            #sbArray63[8] = #S7b.#PPk;
            #sbArray63[9] = #S7b.#OPk;
            #sbArray63[10] = #S7b.#CPk;
            #sbArray63[11] = #S7b.#WPk;
            #sbArray63[12] = #S7b.#RPk;
            #sbArray63[13] = #S7b.#fQk;
            #sbArray63[14] = #S7b.#BPk;
            #sbArray63[0x10] = #S7b.#hPk;
            okArray44[2] = new #1Ok(#G.#eg(0xaf0), #sbArray63);
            #S7b[] #sbArray64 = new #S7b[0x12];
            #sbArray64[0] = #S7b.#6Pk;
            #sbArray64[1] = #S7b.#CPk;
            #sbArray64[2] = #S7b.#5Pk;
            #sbArray64[3] = #S7b.#8Pk;
            #sbArray64[4] = #S7b.#jQk;
            #sbArray64[5] = #S7b.#FPk;
            #sbArray64[6] = #S7b.#EPk;
            #sbArray64[7] = #S7b.#IPk;
            #sbArray64[8] = #S7b.#HPk;
            #sbArray64[9] = #S7b.#PPk;
            #sbArray64[10] = #S7b.#OPk;
            #sbArray64[11] = #S7b.#rPk;
            #sbArray64[12] = #S7b.#WPk;
            #sbArray64[13] = #S7b.#RPk;
            #sbArray64[14] = #S7b.#fQk;
            #sbArray64[15] = #S7b.#BPk;
            #sbArray64[0x11] = #S7b.#hPk;
            okArray44[3] = new #1Ok(#G.#eg(0xaf5), #sbArray64);
            #S7b[] #sbArray65 = new #S7b[0x12];
            #sbArray65[0] = #S7b.#6Pk;
            #sbArray65[1] = #S7b.#5Pk;
            #sbArray65[2] = #S7b.#8Pk;
            #sbArray65[3] = #S7b.#jQk;
            #sbArray65[4] = #S7b.#FPk;
            #sbArray65[5] = #S7b.#EPk;
            #sbArray65[6] = #S7b.#IPk;
            #sbArray65[7] = #S7b.#HPk;
            #sbArray65[8] = #S7b.#PPk;
            #sbArray65[9] = #S7b.#OPk;
            #sbArray65[10] = #S7b.#CPk;
            #sbArray65[11] = #S7b.#fQk;
            #sbArray65[12] = #S7b.#BPk;
            #sbArray65[14] = #S7b.#WPk;
            #sbArray65[15] = #S7b.#RPk;
            #sbArray65[0x10] = #S7b.#hPk;
            #sbArray65[0x11] = #S7b.#RPk;
            okArray44[4] = new #1Ok(null, #sbArray65);
            mapSets[0x2c] = new #fPk(new UnicodeRange(0x2300, 0x23ff), okArray44);
            #S7b[] #sbArray66 = new #S7b[] { #S7b.#8Pk, #S7b.#BPk };
            #1Ok[] okArray45 = new #1Ok[] { new #1Ok(null, #sbArray66) };
            mapSets[0x2d] = new #fPk(new UnicodeRange(0x2400, 0x243f), okArray45);
            #S7b[] #sbArray67 = new #S7b[] { #S7b.#8Pk, #S7b.#hPk };
            #1Ok[] okArray46 = new #1Ok[] { new #1Ok(null, #sbArray67) };
            mapSets[0x2e] = new #fPk(new UnicodeRange(0x2440, 0x245f), okArray46);
            #S7b[] #sbArray68 = new #S7b[14];
            #sbArray68[0] = #S7b.#PPk;
            #sbArray68[1] = #S7b.#OPk;
            #sbArray68[2] = #S7b.#jQk;
            #sbArray68[3] = #S7b.#FPk;
            #sbArray68[4] = #S7b.#EPk;
            #sbArray68[5] = #S7b.#5Pk;
            #sbArray68[6] = #S7b.#8Pk;
            #sbArray68[7] = #S7b.#bQk;
            #sbArray68[8] = #S7b.#WPk;
            #sbArray68[9] = #S7b.#RPk;
            #sbArray68[10] = #S7b.#fQk;
            #sbArray68[11] = #S7b.#BPk;
            #sbArray68[13] = #S7b.#hPk;
            #1Ok[] okArray47 = new #1Ok[5];
            okArray47[0] = new #1Ok(#G.#eg(0xad6), #sbArray68);
            #S7b[] #sbArray69 = new #S7b[15];
            #sbArray69[0] = #S7b.#IPk;
            #sbArray69[1] = #S7b.#HPk;
            #sbArray69[2] = #S7b.#jQk;
            #sbArray69[3] = #S7b.#FPk;
            #sbArray69[4] = #S7b.#EPk;
            #sbArray69[5] = #S7b.#5Pk;
            #sbArray69[6] = #S7b.#8Pk;
            #sbArray69[7] = #S7b.#PPk;
            #sbArray69[8] = #S7b.#OPk;
            #sbArray69[9] = #S7b.#RPk;
            #sbArray69[10] = #S7b.#WPk;
            #sbArray69[11] = #S7b.#fQk;
            #sbArray69[12] = #S7b.#BPk;
            #sbArray69[14] = #S7b.#hPk;
            okArray47[1] = new #1Ok(#G.#eg(0xae3), #sbArray69);
            #S7b[] #sbArray70 = new #S7b[0x10];
            #sbArray70[0] = #S7b.#jQk;
            #sbArray70[1] = #S7b.#FPk;
            #sbArray70[2] = #S7b.#EPk;
            #sbArray70[3] = #S7b.#5Pk;
            #sbArray70[4] = #S7b.#8Pk;
            #sbArray70[5] = #S7b.#IPk;
            #sbArray70[6] = #S7b.#HPk;
            #sbArray70[7] = #S7b.#PPk;
            #sbArray70[8] = #S7b.#OPk;
            #sbArray70[9] = #S7b.#CPk;
            #sbArray70[10] = #S7b.#WPk;
            #sbArray70[11] = #S7b.#RPk;
            #sbArray70[12] = #S7b.#fQk;
            #sbArray70[13] = #S7b.#BPk;
            #sbArray70[15] = #S7b.#hPk;
            okArray47[2] = new #1Ok(#G.#eg(0xaf0), #sbArray70);
            #S7b[] #sbArray71 = new #S7b[0x11];
            #sbArray71[0] = #S7b.#CPk;
            #sbArray71[1] = #S7b.#5Pk;
            #sbArray71[2] = #S7b.#8Pk;
            #sbArray71[3] = #S7b.#jQk;
            #sbArray71[4] = #S7b.#FPk;
            #sbArray71[5] = #S7b.#EPk;
            #sbArray71[6] = #S7b.#IPk;
            #sbArray71[7] = #S7b.#HPk;
            #sbArray71[8] = #S7b.#PPk;
            #sbArray71[9] = #S7b.#OPk;
            #sbArray71[10] = #S7b.#rPk;
            #sbArray71[11] = #S7b.#WPk;
            #sbArray71[12] = #S7b.#RPk;
            #sbArray71[13] = #S7b.#fQk;
            #sbArray71[14] = #S7b.#BPk;
            #sbArray71[0x10] = #S7b.#hPk;
            okArray47[3] = new #1Ok(#G.#eg(0xaf5), #sbArray71);
            #S7b[] #sbArray72 = new #S7b[0x11];
            #sbArray72[0] = #S7b.#5Pk;
            #sbArray72[1] = #S7b.#8Pk;
            #sbArray72[2] = #S7b.#jQk;
            #sbArray72[3] = #S7b.#FPk;
            #sbArray72[4] = #S7b.#EPk;
            #sbArray72[5] = #S7b.#IPk;
            #sbArray72[6] = #S7b.#HPk;
            #sbArray72[7] = #S7b.#PPk;
            #sbArray72[8] = #S7b.#OPk;
            #sbArray72[9] = #S7b.#CPk;
            #sbArray72[10] = #S7b.#fQk;
            #sbArray72[11] = #S7b.#BPk;
            #sbArray72[13] = #S7b.#WPk;
            #sbArray72[14] = #S7b.#RPk;
            #sbArray72[15] = #S7b.#hPk;
            #sbArray72[0x10] = #S7b.#RPk;
            okArray47[4] = new #1Ok(null, #sbArray72);
            mapSets[0x2f] = new #fPk(new UnicodeRange(0x2460, 0x25ff), okArray47);
            #S7b[] #sbArray73 = new #S7b[15];
            #sbArray73[0] = #S7b.#6Pk;
            #sbArray73[1] = #S7b.#PPk;
            #sbArray73[2] = #S7b.#OPk;
            #sbArray73[3] = #S7b.#jQk;
            #sbArray73[4] = #S7b.#FPk;
            #sbArray73[5] = #S7b.#EPk;
            #sbArray73[6] = #S7b.#5Pk;
            #sbArray73[7] = #S7b.#8Pk;
            #sbArray73[8] = #S7b.#bQk;
            #sbArray73[9] = #S7b.#WPk;
            #sbArray73[10] = #S7b.#RPk;
            #sbArray73[11] = #S7b.#fQk;
            #sbArray73[12] = #S7b.#BPk;
            #sbArray73[14] = #S7b.#hPk;
            #1Ok[] okArray48 = new #1Ok[5];
            okArray48[0] = new #1Ok(#G.#eg(0xad6), #sbArray73);
            #S7b[] #sbArray74 = new #S7b[0x10];
            #sbArray74[0] = #S7b.#6Pk;
            #sbArray74[1] = #S7b.#IPk;
            #sbArray74[2] = #S7b.#HPk;
            #sbArray74[3] = #S7b.#jQk;
            #sbArray74[4] = #S7b.#FPk;
            #sbArray74[5] = #S7b.#EPk;
            #sbArray74[6] = #S7b.#5Pk;
            #sbArray74[7] = #S7b.#8Pk;
            #sbArray74[8] = #S7b.#PPk;
            #sbArray74[9] = #S7b.#OPk;
            #sbArray74[10] = #S7b.#RPk;
            #sbArray74[11] = #S7b.#WPk;
            #sbArray74[12] = #S7b.#fQk;
            #sbArray74[13] = #S7b.#BPk;
            #sbArray74[15] = #S7b.#hPk;
            okArray48[1] = new #1Ok(#G.#eg(0xae3), #sbArray74);
            #S7b[] #sbArray75 = new #S7b[0x11];
            #sbArray75[0] = #S7b.#6Pk;
            #sbArray75[1] = #S7b.#jQk;
            #sbArray75[2] = #S7b.#FPk;
            #sbArray75[3] = #S7b.#EPk;
            #sbArray75[4] = #S7b.#5Pk;
            #sbArray75[5] = #S7b.#8Pk;
            #sbArray75[6] = #S7b.#IPk;
            #sbArray75[7] = #S7b.#HPk;
            #sbArray75[8] = #S7b.#PPk;
            #sbArray75[9] = #S7b.#OPk;
            #sbArray75[10] = #S7b.#CPk;
            #sbArray75[11] = #S7b.#WPk;
            #sbArray75[12] = #S7b.#RPk;
            #sbArray75[13] = #S7b.#fQk;
            #sbArray75[14] = #S7b.#BPk;
            #sbArray75[0x10] = #S7b.#hPk;
            okArray48[2] = new #1Ok(#G.#eg(0xaf0), #sbArray75);
            #S7b[] #sbArray76 = new #S7b[0x12];
            #sbArray76[0] = #S7b.#6Pk;
            #sbArray76[1] = #S7b.#CPk;
            #sbArray76[2] = #S7b.#5Pk;
            #sbArray76[3] = #S7b.#8Pk;
            #sbArray76[4] = #S7b.#jQk;
            #sbArray76[5] = #S7b.#FPk;
            #sbArray76[6] = #S7b.#EPk;
            #sbArray76[7] = #S7b.#IPk;
            #sbArray76[8] = #S7b.#HPk;
            #sbArray76[9] = #S7b.#PPk;
            #sbArray76[10] = #S7b.#OPk;
            #sbArray76[11] = #S7b.#rPk;
            #sbArray76[12] = #S7b.#WPk;
            #sbArray76[13] = #S7b.#RPk;
            #sbArray76[14] = #S7b.#fQk;
            #sbArray76[15] = #S7b.#BPk;
            #sbArray76[0x11] = #S7b.#hPk;
            okArray48[3] = new #1Ok(#G.#eg(0xaf5), #sbArray76);
            #S7b[] #sbArray77 = new #S7b[0x12];
            #sbArray77[0] = #S7b.#6Pk;
            #sbArray77[1] = #S7b.#5Pk;
            #sbArray77[2] = #S7b.#8Pk;
            #sbArray77[3] = #S7b.#jQk;
            #sbArray77[4] = #S7b.#FPk;
            #sbArray77[5] = #S7b.#EPk;
            #sbArray77[6] = #S7b.#IPk;
            #sbArray77[7] = #S7b.#HPk;
            #sbArray77[8] = #S7b.#PPk;
            #sbArray77[9] = #S7b.#OPk;
            #sbArray77[10] = #S7b.#CPk;
            #sbArray77[11] = #S7b.#fQk;
            #sbArray77[12] = #S7b.#BPk;
            #sbArray77[14] = #S7b.#WPk;
            #sbArray77[15] = #S7b.#RPk;
            #sbArray77[0x10] = #S7b.#hPk;
            #sbArray77[0x11] = #S7b.#RPk;
            okArray48[4] = new #1Ok(null, #sbArray77);
            mapSets[0x30] = new #fPk(new UnicodeRange(0x2600, 0x27bf), okArray48);
            #S7b[] #sbArray78 = new #S7b[] { #S7b.#8Pk, #S7b.#iPk, #S7b.#WPk };
            #1Ok[] okArray49 = new #1Ok[] { new #1Ok(null, #sbArray78) };
            mapSets[0x31] = new #fPk(new UnicodeRange(0x27c0, 0x2bff), okArray49);
            #S7b[] #sbArray79 = new #S7b[] { #S7b.#7Pk, #S7b.#8Pk };
            #1Ok[] okArray50 = new #1Ok[] { new #1Ok(null, #sbArray79) };
            mapSets[50] = new #fPk(new UnicodeRange(0x2c00, 0x2c5f), okArray50);
            #S7b[] #sbArray80 = new #S7b[8];
            #sbArray80[0] = #S7b.#5Pk;
            #sbArray80[1] = #S7b.#fQk;
            #sbArray80[3] = #S7b.#aQk;
            #sbArray80[4] = #S7b.#gQk;
            #sbArray80[5] = #S7b.#hPk;
            #sbArray80[6] = #S7b.#LPk;
            #sbArray80[7] = #S7b.#BPk;
            #1Ok[] okArray51 = new #1Ok[] { new #1Ok(null, #sbArray80) };
            mapSets[0x33] = new #fPk(new UnicodeRange(0x2c60, 0x2c7f), okArray51);
            #S7b[] #sbArray81 = new #S7b[] { #S7b.#7Pk, #S7b.#8Pk };
            #1Ok[] okArray52 = new #1Ok[] { new #1Ok(null, #sbArray81) };
            mapSets[0x34] = new #fPk(new UnicodeRange(0x2c80, 0x2cff), okArray52);
            #S7b[] #sbArray82 = new #S7b[] { #S7b.#5Pk, #S7b.#eQk, #S7b.#hPk };
            #1Ok[] okArray53 = new #1Ok[] { new #1Ok(null, #sbArray82) };
            mapSets[0x35] = new #fPk(new UnicodeRange(0x2d00, 0x2d2f), okArray53);
            #S7b[] #sbArray83 = new #S7b[] { #S7b.#mPk };
            #1Ok[] okArray54 = new #1Ok[] { new #1Ok(null, #sbArray83) };
            mapSets[0x36] = new #fPk(new UnicodeRange(0x2d30, 0x2d7f), okArray54);
            #S7b[] #sbArray84 = new #S7b[] { #S7b.#mPk, #S7b.#1Pk };
            #1Ok[] okArray55 = new #1Ok[] { new #1Ok(null, #sbArray84) };
            mapSets[0x37] = new #fPk(new UnicodeRange(0x2d80, 0x2ddf), okArray55);
            #S7b[] #sbArray85 = new #S7b[] { #S7b.#5Pk, #S7b.#8Pk };
            #1Ok[] okArray56 = new #1Ok[] { new #1Ok(null, #sbArray85) };
            mapSets[0x38] = new #fPk(new UnicodeRange(0x2e00, 0x2e7f), okArray56);
            #S7b[] #sbArray86 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#RPk };
            #1Ok[] okArray57 = new #1Ok[5];
            okArray57[0] = new #1Ok(#G.#eg(0xad6), #sbArray86);
            #S7b[] #sbArray87 = new #S7b[] { #S7b.#IPk, #S7b.#PPk, #S7b.#OPk, #S7b.#RPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#bQk };
            okArray57[1] = new #1Ok(#G.#eg(0xae3), #sbArray87);
            #S7b[] #sbArray88 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#PPk, #S7b.#OPk, #S7b.#RPk, #S7b.#bQk };
            okArray57[2] = new #1Ok(#G.#eg(0xaf0), #sbArray88);
            #S7b[] #sbArray89 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#PPk, #S7b.#OPk, #S7b.#RPk, #S7b.#bQk };
            okArray57[3] = new #1Ok(#G.#eg(0xaf5), #sbArray89);
            #S7b[] #sbArray90 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#PPk, #S7b.#OPk, #S7b.#RPk, #S7b.#bQk };
            okArray57[4] = new #1Ok(null, #sbArray90);
            mapSets[0x39] = new #fPk(new UnicodeRange(0x2e80, 0x2eff), okArray57);
            #S7b[] #sbArray91 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk };
            #1Ok[] okArray58 = new #1Ok[] { new #1Ok(null, #sbArray91) };
            mapSets[0x3a] = new #fPk(new UnicodeRange(0x2f00, 0x2fdf), okArray58);
            #S7b[] #sbArray92 = new #S7b[] { #S7b.#PPk, #S7b.#bQk };
            #1Ok[] okArray59 = new #1Ok[] { new #1Ok(null, #sbArray92) };
            mapSets[0x3b] = new #fPk(new UnicodeRange(0x2ff0, 0x2fff), okArray59);
            #S7b[] #sbArray93 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk, #S7b.#WPk, #S7b.#hPk };
            #1Ok[] okArray60 = new #1Ok[5];
            okArray60[0] = new #1Ok(#G.#eg(0xad6), #sbArray93);
            #S7b[] #sbArray94 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#WPk, #S7b.#hPk };
            okArray60[1] = new #1Ok(#G.#eg(0xae3), #sbArray94);
            #S7b[] #sbArray95 = new #S7b[10];
            #sbArray95[0] = #S7b.#CPk;
            #sbArray95[1] = #S7b.#jQk;
            #sbArray95[2] = #S7b.#FPk;
            #sbArray95[3] = #S7b.#EPk;
            #sbArray95[4] = #S7b.#PPk;
            #sbArray95[5] = #S7b.#OPk;
            #sbArray95[6] = #S7b.#rPk;
            #sbArray95[7] = #S7b.#WPk;
            #sbArray95[8] = #S7b.#RPk;
            #sbArray95[9] = #S7b.#hPk;
            okArray60[2] = new #1Ok(#G.#eg(0xaf5), #sbArray95);
            #S7b[] #sbArray96 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#PPk, #S7b.#OPk, #S7b.#WPk, #S7b.#RPk, #S7b.#hPk };
            okArray60[3] = new #1Ok(#G.#eg(0xaf0), #sbArray96);
            #S7b[] #sbArray97 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#PPk, #S7b.#OPk, #S7b.#WPk, #S7b.#RPk, #S7b.#hPk };
            okArray60[4] = new #1Ok(null, #sbArray97);
            mapSets[60] = new #fPk(new UnicodeRange(0x3000, 0x30ff), okArray60);
            #S7b[] #sbArray98 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk, #S7b.#hPk };
            #1Ok[] okArray61 = new #1Ok[5];
            okArray61[0] = new #1Ok(#G.#eg(0xad6), #sbArray98);
            #S7b[] #sbArray99 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray61[1] = new #1Ok(#G.#eg(0xae3), #sbArray99);
            #S7b[] #sbArray100 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray61[2] = new #1Ok(#G.#eg(0xaf0), #sbArray100);
            #S7b[] #sbArray101 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray61[3] = new #1Ok(#G.#eg(0xaf5), #sbArray101);
            #S7b[] #sbArray102 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray61[4] = new #1Ok(null, #sbArray102);
            mapSets[0x3d] = new #fPk(new UnicodeRange(0x3100, 0x312f), okArray61);
            #S7b[] #sbArray103 = new #S7b[] { #S7b.#CPk, #S7b.#rPk };
            #1Ok[] okArray62 = new #1Ok[] { new #1Ok(null, #sbArray103) };
            mapSets[0x3e] = new #fPk(new UnicodeRange(0x3130, 0x318f), okArray62);
            #S7b[] #sbArray104 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#RPk };
            #1Ok[] okArray63 = new #1Ok[5];
            okArray63[0] = new #1Ok(#G.#eg(0xad6), #sbArray104);
            #S7b[] #sbArray105 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray63[1] = new #1Ok(#G.#eg(0xae3), #sbArray105);
            #S7b[] #sbArray106 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray63[2] = new #1Ok(#G.#eg(0xaf0), #sbArray106);
            #S7b[] #sbArray107 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray63[3] = new #1Ok(#G.#eg(0xaf5), #sbArray107);
            #S7b[] #sbArray108 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray63[4] = new #1Ok(null, #sbArray108);
            mapSets[0x3f] = new #fPk(new UnicodeRange(0x3190, 0x319f), okArray63);
            #S7b[] #sbArray109 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk, #S7b.#hPk };
            #1Ok[] okArray64 = new #1Ok[5];
            okArray64[0] = new #1Ok(#G.#eg(0xad6), #sbArray109);
            #S7b[] #sbArray110 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray64[1] = new #1Ok(#G.#eg(0xae3), #sbArray110);
            #S7b[] #sbArray111 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray64[2] = new #1Ok(#G.#eg(0xaf0), #sbArray111);
            #S7b[] #sbArray112 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray64[3] = new #1Ok(#G.#eg(0xaf5), #sbArray112);
            #S7b[] #sbArray113 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#hPk };
            okArray64[4] = new #1Ok(null, #sbArray113);
            mapSets[0x40] = new #fPk(new UnicodeRange(0x31a0, 0x31bf), okArray64);
            #S7b[] #sbArray114 = new #S7b[] { #S7b.#IPk, #S7b.#RPk };
            #1Ok[] okArray65 = new #1Ok[] { new #1Ok(null, #sbArray114) };
            mapSets[0x41] = new #fPk(new UnicodeRange(0x31c0, 0x31ef), okArray65);
            #S7b[] #sbArray115 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk, #S7b.#WPk, #S7b.#hPk };
            #1Ok[] okArray66 = new #1Ok[5];
            okArray66[0] = new #1Ok(#G.#eg(0xad6), #sbArray115);
            #S7b[] #sbArray116 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk, #S7b.#WPk, #S7b.#hPk };
            okArray66[1] = new #1Ok(#G.#eg(0xae3), #sbArray116);
            #S7b[] #sbArray117 = new #S7b[10];
            #sbArray117[0] = #S7b.#CPk;
            #sbArray117[1] = #S7b.#jQk;
            #sbArray117[2] = #S7b.#FPk;
            #sbArray117[3] = #S7b.#EPk;
            #sbArray117[4] = #S7b.#PPk;
            #sbArray117[5] = #S7b.#OPk;
            #sbArray117[6] = #S7b.#rPk;
            #sbArray117[7] = #S7b.#WPk;
            #sbArray117[8] = #S7b.#RPk;
            #sbArray117[9] = #S7b.#hPk;
            okArray66[2] = new #1Ok(#G.#eg(0xaf5), #sbArray117);
            #S7b[] #sbArray118 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#PPk, #S7b.#OPk, #S7b.#WPk, #S7b.#RPk, #S7b.#hPk };
            okArray66[3] = new #1Ok(#G.#eg(0xaf0), #sbArray118);
            #S7b[] #sbArray119 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#PPk, #S7b.#OPk, #S7b.#WPk, #S7b.#RPk, #S7b.#hPk };
            okArray66[4] = new #1Ok(null, #sbArray119);
            mapSets[0x42] = new #fPk(new UnicodeRange(0x31f0, 0x31ff), okArray66);
            #S7b[] #sbArray120 = new #S7b[] { #S7b.#CPk, #S7b.#rPk };
            #1Ok[] okArray67 = new #1Ok[] { new #1Ok(null, #sbArray120) };
            mapSets[0x43] = new #fPk(new UnicodeRange(0x3200, 0x321f), okArray67);
            #S7b[] #sbArray121 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#bQk, #S7b.#WPk };
            #1Ok[] okArray68 = new #1Ok[5];
            okArray68[0] = new #1Ok(#G.#eg(0xad6), #sbArray121);
            #S7b[] #sbArray122 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#RPk, #S7b.#WPk };
            okArray68[1] = new #1Ok(#G.#eg(0xae3), #sbArray122);
            #S7b[] #sbArray123 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#WPk };
            okArray68[2] = new #1Ok(#G.#eg(0xaf0), #sbArray123);
            #S7b[] #sbArray124 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray68[3] = new #1Ok(#G.#eg(0xaf5), #sbArray124);
            #S7b[] #sbArray125 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray68[4] = new #1Ok(null, #sbArray125);
            mapSets[0x44] = new #fPk(new UnicodeRange(0x3220, 0x324f), okArray68);
            #S7b[] #sbArray126 = new #S7b[] { #S7b.#jQk, #S7b.#FPk };
            #1Ok[] okArray69 = new #1Ok[] { new #1Ok(null, #sbArray126) };
            mapSets[0x45] = new #fPk(new UnicodeRange(0x3250, 0x3250), okArray69);
            #S7b[] #sbArray127 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#bQk, #S7b.#WPk };
            #1Ok[] okArray70 = new #1Ok[5];
            okArray70[0] = new #1Ok(#G.#eg(0xad6), #sbArray127);
            #S7b[] #sbArray128 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#RPk, #S7b.#WPk };
            okArray70[1] = new #1Ok(#G.#eg(0xae3), #sbArray128);
            #S7b[] #sbArray129 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#WPk };
            okArray70[2] = new #1Ok(#G.#eg(0xaf0), #sbArray129);
            #S7b[] #sbArray130 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray70[3] = new #1Ok(#G.#eg(0xaf5), #sbArray130);
            #S7b[] #sbArray131 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray70[4] = new #1Ok(null, #sbArray131);
            mapSets[70] = new #fPk(new UnicodeRange(0x3251, 0x325f), okArray70);
            #S7b[] #sbArray132 = new #S7b[] { #S7b.#CPk, #S7b.#rPk };
            #1Ok[] okArray71 = new #1Ok[] { new #1Ok(null, #sbArray132) };
            mapSets[0x47] = new #fPk(new UnicodeRange(0x3260, 0x327f), okArray71);
            #S7b[] #sbArray133 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#bQk, #S7b.#WPk };
            #1Ok[] okArray72 = new #1Ok[5];
            okArray72[0] = new #1Ok(#G.#eg(0xad6), #sbArray133);
            #S7b[] #sbArray134 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#RPk, #S7b.#WPk };
            okArray72[1] = new #1Ok(#G.#eg(0xae3), #sbArray134);
            #S7b[] #sbArray135 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#WPk };
            okArray72[2] = new #1Ok(#G.#eg(0xaf0), #sbArray135);
            #S7b[] #sbArray136 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray72[3] = new #1Ok(#G.#eg(0xaf5), #sbArray136);
            #S7b[] #sbArray137 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray72[4] = new #1Ok(null, #sbArray137);
            mapSets[0x48] = new #fPk(new UnicodeRange(0x3280, 0x32cb), okArray72);
            #S7b[] #sbArray138 = new #S7b[] { #S7b.#jQk, #S7b.#FPk };
            #1Ok[] okArray73 = new #1Ok[] { new #1Ok(null, #sbArray138) };
            mapSets[0x49] = new #fPk(new UnicodeRange(0x32cc, 0x32cf), okArray73);
            #S7b[] #sbArray139 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#bQk, #S7b.#WPk };
            #1Ok[] okArray74 = new #1Ok[5];
            okArray74[0] = new #1Ok(#G.#eg(0xad6), #sbArray139);
            #S7b[] #sbArray140 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#RPk, #S7b.#WPk };
            okArray74[1] = new #1Ok(#G.#eg(0xae3), #sbArray140);
            #S7b[] #sbArray141 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#WPk };
            okArray74[2] = new #1Ok(#G.#eg(0xaf0), #sbArray141);
            #S7b[] #sbArray142 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray74[3] = new #1Ok(#G.#eg(0xaf5), #sbArray142);
            #S7b[] #sbArray143 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray74[4] = new #1Ok(null, #sbArray143);
            mapSets[0x4a] = new #fPk(new UnicodeRange(0x32d0, 0x3376), okArray74);
            #S7b[] #sbArray144 = new #S7b[] { #S7b.#jQk, #S7b.#FPk };
            #1Ok[] okArray75 = new #1Ok[] { new #1Ok(null, #sbArray144) };
            mapSets[0x4b] = new #fPk(new UnicodeRange(0x3377, 0x337a), okArray75);
            #S7b[] #sbArray145 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#bQk, #S7b.#WPk };
            #1Ok[] okArray76 = new #1Ok[5];
            okArray76[0] = new #1Ok(#G.#eg(0xad6), #sbArray145);
            #S7b[] #sbArray146 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#RPk, #S7b.#WPk };
            okArray76[1] = new #1Ok(#G.#eg(0xae3), #sbArray146);
            #S7b[] #sbArray147 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#WPk };
            okArray76[2] = new #1Ok(#G.#eg(0xaf0), #sbArray147);
            #S7b[] #sbArray148 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray76[3] = new #1Ok(#G.#eg(0xaf5), #sbArray148);
            #S7b[] #sbArray149 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray76[4] = new #1Ok(null, #sbArray149);
            mapSets[0x4c] = new #fPk(new UnicodeRange(0x337b, 0x337f), okArray76);
            #S7b[] #sbArray150 = new #S7b[11];
            #sbArray150[0] = #S7b.#PPk;
            #sbArray150[1] = #S7b.#OPk;
            #sbArray150[2] = #S7b.#IPk;
            #sbArray150[3] = #S7b.#HPk;
            #sbArray150[4] = #S7b.#jQk;
            #sbArray150[5] = #S7b.#FPk;
            #sbArray150[6] = #S7b.#EPk;
            #sbArray150[7] = #S7b.#CPk;
            #sbArray150[8] = #S7b.#8Pk;
            #sbArray150[9] = #S7b.#bQk;
            #sbArray150[10] = #S7b.#WPk;
            #1Ok[] okArray77 = new #1Ok[5];
            okArray77[0] = new #1Ok(#G.#eg(0xad6), #sbArray150);
            #S7b[] #sbArray151 = new #S7b[9];
            #sbArray151[0] = #S7b.#IPk;
            #sbArray151[1] = #S7b.#HPk;
            #sbArray151[2] = #S7b.#jQk;
            #sbArray151[3] = #S7b.#FPk;
            #sbArray151[4] = #S7b.#EPk;
            #sbArray151[5] = #S7b.#CPk;
            #sbArray151[6] = #S7b.#8Pk;
            #sbArray151[7] = #S7b.#3Pk;
            #sbArray151[8] = #S7b.#WPk;
            okArray77[1] = new #1Ok(#G.#eg(0xae3), #sbArray151);
            #S7b[] #sbArray152 = new #S7b[] { #S7b.#CPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#rPk, #S7b.#WPk };
            okArray77[2] = new #1Ok(#G.#eg(0xaf5), #sbArray152);
            #S7b[] #sbArray153 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#WPk };
            okArray77[3] = new #1Ok(#G.#eg(0xaf0), #sbArray153);
            #S7b[] #sbArray154 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray77[4] = new #1Ok(null, #sbArray154);
            mapSets[0x4d] = new #fPk(new UnicodeRange(0x3380, 0x33df), okArray77);
            #S7b[] #sbArray155 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#bQk, #S7b.#WPk };
            #1Ok[] okArray78 = new #1Ok[5];
            okArray78[0] = new #1Ok(#G.#eg(0xad6), #sbArray155);
            #S7b[] #sbArray156 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#RPk, #S7b.#WPk };
            okArray78[1] = new #1Ok(#G.#eg(0xae3), #sbArray156);
            #S7b[] #sbArray157 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#8Pk, #S7b.#WPk };
            okArray78[2] = new #1Ok(#G.#eg(0xaf0), #sbArray157);
            #S7b[] #sbArray158 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray78[3] = new #1Ok(#G.#eg(0xaf5), #sbArray158);
            #S7b[] #sbArray159 = new #S7b[] { #S7b.#8Pk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray78[4] = new #1Ok(null, #sbArray159);
            mapSets[0x4e] = new #fPk(new UnicodeRange(0x33e0, 0x33ff), okArray78);
            #S7b[] #sbArray160 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk, #S7b.#cQk, #S7b.#dQk, #S7b.#RPk };
            #1Ok[] okArray79 = new #1Ok[6];
            okArray79[0] = new #1Ok(#G.#eg(0xad6), #sbArray160);
            #S7b[] #sbArray161 = new #S7b[9];
            #sbArray161[0] = #S7b.#IPk;
            #sbArray161[1] = #S7b.#HPk;
            #sbArray161[2] = #S7b.#PPk;
            #sbArray161[3] = #S7b.#OPk;
            #sbArray161[4] = #S7b.#TPk;
            #sbArray161[5] = #S7b.#RPk;
            #sbArray161[6] = #S7b.#cQk;
            #sbArray161[7] = #S7b.#dQk;
            #sbArray161[8] = #S7b.#bQk;
            okArray79[1] = new #1Ok(#G.#eg(0xafa), #sbArray161);
            #S7b[] #sbArray162 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#PPk, #S7b.#OPk, #S7b.#RPk, #S7b.#cQk, #S7b.#dQk, #S7b.#bQk };
            okArray79[2] = new #1Ok(#G.#eg(0xae3), #sbArray162);
            #S7b[] #sbArray163 = new #S7b[9];
            #sbArray163[0] = #S7b.#jQk;
            #sbArray163[1] = #S7b.#FPk;
            #sbArray163[2] = #S7b.#EPk;
            #sbArray163[3] = #S7b.#WPk;
            #sbArray163[4] = #S7b.#IPk;
            #sbArray163[5] = #S7b.#HPk;
            #sbArray163[6] = #S7b.#PPk;
            #sbArray163[7] = #S7b.#OPk;
            #sbArray163[8] = #S7b.#RPk;
            okArray79[3] = new #1Ok(#G.#eg(0xaf0), #sbArray163);
            #S7b[] #sbArray164 = new #S7b[9];
            #sbArray164[0] = #S7b.#CPk;
            #sbArray164[1] = #S7b.#rPk;
            #sbArray164[2] = #S7b.#IPk;
            #sbArray164[3] = #S7b.#HPk;
            #sbArray164[4] = #S7b.#PPk;
            #sbArray164[5] = #S7b.#OPk;
            #sbArray164[6] = #S7b.#WPk;
            #sbArray164[7] = #S7b.#RPk;
            #sbArray164[8] = #S7b.#bQk;
            okArray79[4] = new #1Ok(#G.#eg(0xaf5), #sbArray164);
            #S7b[] #sbArray165 = new #S7b[10];
            #sbArray165[0] = #S7b.#jQk;
            #sbArray165[1] = #S7b.#FPk;
            #sbArray165[2] = #S7b.#EPk;
            #sbArray165[3] = #S7b.#IPk;
            #sbArray165[4] = #S7b.#HPk;
            #sbArray165[5] = #S7b.#PPk;
            #sbArray165[6] = #S7b.#OPk;
            #sbArray165[7] = #S7b.#WPk;
            #sbArray165[8] = #S7b.#RPk;
            #sbArray165[9] = #S7b.#bQk;
            okArray79[5] = new #1Ok(null, #sbArray165);
            mapSets[0x4f] = new #fPk(new UnicodeRange(0x3400, 0x4dbf), okArray79);
            #S7b[] #sbArray166 = new #S7b[] { #S7b.#8Pk };
            #1Ok[] okArray80 = new #1Ok[] { new #1Ok(null, #sbArray166) };
            mapSets[80] = new #fPk(new UnicodeRange(0x4dc0, 0x4dff), okArray80);
            #S7b[] #sbArray167 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk, #S7b.#cQk, #S7b.#dQk, #S7b.#RPk };
            #1Ok[] okArray81 = new #1Ok[6];
            okArray81[0] = new #1Ok(#G.#eg(0xad6), #sbArray167);
            #S7b[] #sbArray168 = new #S7b[9];
            #sbArray168[0] = #S7b.#IPk;
            #sbArray168[1] = #S7b.#HPk;
            #sbArray168[2] = #S7b.#PPk;
            #sbArray168[3] = #S7b.#OPk;
            #sbArray168[4] = #S7b.#TPk;
            #sbArray168[5] = #S7b.#RPk;
            #sbArray168[6] = #S7b.#cQk;
            #sbArray168[7] = #S7b.#dQk;
            #sbArray168[8] = #S7b.#bQk;
            okArray81[1] = new #1Ok(#G.#eg(0xafa), #sbArray168);
            #S7b[] #sbArray169 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#PPk, #S7b.#OPk, #S7b.#RPk, #S7b.#cQk, #S7b.#dQk, #S7b.#bQk };
            okArray81[2] = new #1Ok(#G.#eg(0xae3), #sbArray169);
            #S7b[] #sbArray170 = new #S7b[9];
            #sbArray170[0] = #S7b.#jQk;
            #sbArray170[1] = #S7b.#FPk;
            #sbArray170[2] = #S7b.#EPk;
            #sbArray170[3] = #S7b.#WPk;
            #sbArray170[4] = #S7b.#IPk;
            #sbArray170[5] = #S7b.#HPk;
            #sbArray170[6] = #S7b.#PPk;
            #sbArray170[7] = #S7b.#OPk;
            #sbArray170[8] = #S7b.#RPk;
            okArray81[3] = new #1Ok(#G.#eg(0xaf0), #sbArray170);
            #S7b[] #sbArray171 = new #S7b[9];
            #sbArray171[0] = #S7b.#CPk;
            #sbArray171[1] = #S7b.#rPk;
            #sbArray171[2] = #S7b.#IPk;
            #sbArray171[3] = #S7b.#HPk;
            #sbArray171[4] = #S7b.#PPk;
            #sbArray171[5] = #S7b.#OPk;
            #sbArray171[6] = #S7b.#WPk;
            #sbArray171[7] = #S7b.#RPk;
            #sbArray171[8] = #S7b.#bQk;
            okArray81[4] = new #1Ok(#G.#eg(0xaf5), #sbArray171);
            #S7b[] #sbArray172 = new #S7b[10];
            #sbArray172[0] = #S7b.#jQk;
            #sbArray172[1] = #S7b.#FPk;
            #sbArray172[2] = #S7b.#EPk;
            #sbArray172[3] = #S7b.#IPk;
            #sbArray172[4] = #S7b.#HPk;
            #sbArray172[5] = #S7b.#PPk;
            #sbArray172[6] = #S7b.#OPk;
            #sbArray172[7] = #S7b.#WPk;
            #sbArray172[8] = #S7b.#RPk;
            #sbArray172[9] = #S7b.#bQk;
            okArray81[5] = new #1Ok(null, #sbArray172);
            mapSets[0x51] = new #fPk(new UnicodeRange(0x4e00, 0x9fff), okArray81);
            #S7b[] #sbArray173 = new #S7b[] { #S7b.#QPk, #S7b.#cQk, #S7b.#dQk };
            #1Ok[] okArray82 = new #1Ok[] { new #1Ok(null, #sbArray173) };
            mapSets[0x52] = new #fPk(new UnicodeRange(0xa000, 0xa4cf), okArray82);
            #S7b[] #sbArray174 = new #S7b[] { #S7b.#5Pk };
            #1Ok[] okArray83 = new #1Ok[] { new #1Ok(null, #sbArray174) };
            mapSets[0x53] = new #fPk(new UnicodeRange(0xa4d0, 0xa4ff), okArray83);
            #S7b[] #sbArray175 = new #S7b[] { #S7b.#mPk };
            #1Ok[] okArray84 = new #1Ok[] { new #1Ok(null, #sbArray175) };
            mapSets[0x54] = new #fPk(new UnicodeRange(0xa500, 0xa63f), okArray84);
            #S7b[] #sbArray176 = new #S7b[] { #S7b.#5Pk };
            #1Ok[] okArray85 = new #1Ok[] { new #1Ok(null, #sbArray176) };
            mapSets[0x55] = new #fPk(new UnicodeRange(0xa700, 0xa71f), okArray85);
            #S7b[] #sbArray177 = new #S7b[8];
            #sbArray177[0] = #S7b.#5Pk;
            #sbArray177[1] = #S7b.#fQk;
            #sbArray177[3] = #S7b.#aQk;
            #sbArray177[4] = #S7b.#gQk;
            #sbArray177[5] = #S7b.#hPk;
            #sbArray177[6] = #S7b.#LPk;
            #sbArray177[7] = #S7b.#BPk;
            #1Ok[] okArray86 = new #1Ok[] { new #1Ok(null, #sbArray177) };
            mapSets[0x56] = new #fPk(new UnicodeRange(0xa720, 0xa7ff), okArray86);
            #S7b[] #sbArray178 = new #S7b[] { #S7b.#KPk };
            #1Ok[] okArray87 = new #1Ok[] { new #1Ok(null, #sbArray178) };
            mapSets[0x57] = new #fPk(new UnicodeRange(0xa840, 0xa87f), okArray87);
            #S7b[] #sbArray179 = new #S7b[] { #S7b.#CPk, #S7b.#rPk };
            #1Ok[] okArray88 = new #1Ok[] { new #1Ok(null, #sbArray179) };
            mapSets[0x58] = new #fPk(new UnicodeRange(0xa960, 0xa97f), okArray88);
            #S7b[] #sbArray180 = new #S7b[] { #S7b.#tPk };
            #1Ok[] okArray89 = new #1Ok[] { new #1Ok(null, #sbArray180) };
            mapSets[0x59] = new #fPk(new UnicodeRange(0xa980, 0xa9df), okArray89);
            #S7b[] #sbArray181 = new #S7b[] { #S7b.#ZPk };
            #1Ok[] okArray90 = new #1Ok[] { new #1Ok(null, #sbArray181) };
            mapSets[90] = new #fPk(new UnicodeRange(0xa9e0, 0xa9ff), okArray90);
            #S7b[] #sbArray182 = new #S7b[] { #S7b.#ZPk };
            #1Ok[] okArray91 = new #1Ok[] { new #1Ok(null, #sbArray182) };
            mapSets[0x5b] = new #fPk(new UnicodeRange(0xaa60, 0xaa7f), okArray91);
            #S7b[] #sbArray183 = new #S7b[] { #S7b.#mPk, #S7b.#1Pk };
            #1Ok[] okArray92 = new #1Ok[] { new #1Ok(null, #sbArray183) };
            mapSets[0x5c] = new #fPk(new UnicodeRange(0xab00, 0xab2f), okArray92);
            #S7b[] #sbArray184 = new #S7b[8];
            #sbArray184[0] = #S7b.#5Pk;
            #sbArray184[1] = #S7b.#fQk;
            #sbArray184[3] = #S7b.#aQk;
            #sbArray184[4] = #S7b.#gQk;
            #sbArray184[5] = #S7b.#hPk;
            #sbArray184[6] = #S7b.#LPk;
            #sbArray184[7] = #S7b.#BPk;
            #1Ok[] okArray93 = new #1Ok[] { new #1Ok(null, #sbArray184) };
            mapSets[0x5d] = new #fPk(new UnicodeRange(0xab30, 0xab6f), okArray93);
            #S7b[] #sbArray185 = new #S7b[] { #S7b.#CPk, #S7b.#rPk };
            #1Ok[] okArray94 = new #1Ok[] { new #1Ok(null, #sbArray185) };
            mapSets[0x5e] = new #fPk(new UnicodeRange(0xac00, 0xd7ff), okArray94);
            #S7b[] #sbArray186 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray95 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray186) };
            mapSets[0x5f] = new #fPk(new UnicodeRange(0xe000, 0xe76b), okArray95);
            #S7b[] #sbArray187 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            #1Ok[] okArray96 = new #1Ok[3];
            okArray96[0] = new #1Ok(#G.#eg(0xb03), #sbArray187);
            #S7b[] #sbArray188 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            okArray96[1] = new #1Ok(#G.#eg(0xad6), #sbArray188);
            #S7b[] #sbArray189 = new #S7b[] { #S7b.#TPk };
            okArray96[2] = new #1Ok(#G.#eg(0xafa), #sbArray189);
            mapSets[0x60] = new #fPk(new UnicodeRange(0xe76c, 0xe76c), okArray96);
            #S7b[] #sbArray190 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray97 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray190) };
            mapSets[0x61] = new #fPk(new UnicodeRange(0xe76d, 0xe78c), okArray97);
            #S7b[] #sbArray191 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            #1Ok[] okArray98 = new #1Ok[3];
            okArray98[0] = new #1Ok(#G.#eg(0xb03), #sbArray191);
            #S7b[] #sbArray192 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            okArray98[1] = new #1Ok(#G.#eg(0xad6), #sbArray192);
            #S7b[] #sbArray193 = new #S7b[] { #S7b.#TPk };
            okArray98[2] = new #1Ok(#G.#eg(0xafa), #sbArray193);
            mapSets[0x62] = new #fPk(new UnicodeRange(0xe78d, 0xe796), okArray98);
            #S7b[] #sbArray194 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray99 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray194) };
            mapSets[0x63] = new #fPk(new UnicodeRange(0xe797, 0xe7c6), okArray99);
            #S7b[] #sbArray195 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            #1Ok[] okArray100 = new #1Ok[3];
            okArray100[0] = new #1Ok(#G.#eg(0xb03), #sbArray195);
            #S7b[] #sbArray196 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            okArray100[1] = new #1Ok(#G.#eg(0xad6), #sbArray196);
            #S7b[] #sbArray197 = new #S7b[] { #S7b.#TPk };
            okArray100[2] = new #1Ok(#G.#eg(0xafa), #sbArray197);
            mapSets[100] = new #fPk(new UnicodeRange(0xe7c7, 0xe7c8), okArray100);
            #S7b[] #sbArray198 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray101 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray198) };
            mapSets[0x65] = new #fPk(new UnicodeRange(0xe7c9, 0xe7e6), okArray101);
            #S7b[] #sbArray199 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            #1Ok[] okArray102 = new #1Ok[3];
            okArray102[0] = new #1Ok(#G.#eg(0xb03), #sbArray199);
            #S7b[] #sbArray200 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            okArray102[1] = new #1Ok(#G.#eg(0xad6), #sbArray200);
            #S7b[] #sbArray201 = new #S7b[] { #S7b.#TPk };
            okArray102[2] = new #1Ok(#G.#eg(0xafa), #sbArray201);
            mapSets[0x66] = new #fPk(new UnicodeRange(0xe7e7, 0xe7f3), okArray102);
            #S7b[] #sbArray202 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray103 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray202) };
            mapSets[0x67] = new #fPk(new UnicodeRange(0xe7f4, 0xe814), okArray103);
            #S7b[] #sbArray203 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            #1Ok[] okArray104 = new #1Ok[3];
            okArray104[0] = new #1Ok(#G.#eg(0xb03), #sbArray203);
            #S7b[] #sbArray204 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            okArray104[1] = new #1Ok(#G.#eg(0xad6), #sbArray204);
            #S7b[] #sbArray205 = new #S7b[] { #S7b.#TPk };
            okArray104[2] = new #1Ok(#G.#eg(0xafa), #sbArray205);
            mapSets[0x68] = new #fPk(new UnicodeRange(0xe815, 0xe864), okArray104);
            #S7b[] #sbArray206 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray105 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray206) };
            mapSets[0x69] = new #fPk(new UnicodeRange(0xe865, 0xeeb7), okArray105);
            #S7b[] #sbArray207 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray106 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray207) };
            mapSets[0x6a] = new #fPk(new UnicodeRange(0xf303, 0xf34b), okArray106);
            #S7b[] #sbArray208 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray107 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray208) };
            mapSets[0x6b] = new #fPk(new UnicodeRange(0xf3a0, 0xf5f1), okArray107);
            #S7b[] #sbArray209 = new #S7b[] { #S7b.#TPk };
            #1Ok[] okArray108 = new #1Ok[] { new #1Ok(#G.#eg(0xafa), #sbArray209) };
            mapSets[0x6c] = new #fPk(new UnicodeRange(0xf634, 0xf848), okArray108);
            #S7b[] #sbArray210 = new #S7b[10];
            #sbArray210[0] = #S7b.#PPk;
            #sbArray210[1] = #S7b.#OPk;
            #sbArray210[2] = #S7b.#IPk;
            #sbArray210[3] = #S7b.#HPk;
            #sbArray210[4] = #S7b.#jQk;
            #sbArray210[5] = #S7b.#FPk;
            #sbArray210[6] = #S7b.#EPk;
            #sbArray210[7] = #S7b.#bQk;
            #sbArray210[8] = #S7b.#3Pk;
            #sbArray210[9] = #S7b.#WPk;
            #1Ok[] okArray109 = new #1Ok[5];
            okArray109[0] = new #1Ok(#G.#eg(0xad6), #sbArray210);
            #S7b[] #sbArray211 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#3Pk, #S7b.#XPk };
            okArray109[1] = new #1Ok(#G.#eg(0xae3), #sbArray211);
            #S7b[] #sbArray212 = new #S7b[10];
            #sbArray212[0] = #S7b.#CPk;
            #sbArray212[1] = #S7b.#rPk;
            #sbArray212[2] = #S7b.#jQk;
            #sbArray212[3] = #S7b.#FPk;
            #sbArray212[4] = #S7b.#EPk;
            #sbArray212[5] = #S7b.#IPk;
            #sbArray212[6] = #S7b.#HPk;
            #sbArray212[7] = #S7b.#WPk;
            #sbArray212[8] = #S7b.#3Pk;
            #sbArray212[9] = #S7b.#bQk;
            okArray109[2] = new #1Ok(#G.#eg(0xaf5), #sbArray212);
            #S7b[] #sbArray213 = new #S7b[9];
            #sbArray213[0] = #S7b.#jQk;
            #sbArray213[1] = #S7b.#FPk;
            #sbArray213[2] = #S7b.#EPk;
            #sbArray213[3] = #S7b.#IPk;
            #sbArray213[4] = #S7b.#HPk;
            #sbArray213[5] = #S7b.#WPk;
            #sbArray213[6] = #S7b.#3Pk;
            #sbArray213[7] = #S7b.#bQk;
            #sbArray213[8] = #S7b.#rPk;
            okArray109[3] = new #1Ok(#G.#eg(0xaf0), #sbArray213);
            #S7b[] #sbArray214 = new #S7b[9];
            #sbArray214[0] = #S7b.#jQk;
            #sbArray214[1] = #S7b.#FPk;
            #sbArray214[2] = #S7b.#EPk;
            #sbArray214[3] = #S7b.#IPk;
            #sbArray214[4] = #S7b.#HPk;
            #sbArray214[5] = #S7b.#WPk;
            #sbArray214[6] = #S7b.#3Pk;
            #sbArray214[7] = #S7b.#bQk;
            #sbArray214[8] = #S7b.#rPk;
            okArray109[4] = new #1Ok(null, #sbArray214);
            mapSets[0x6d] = new #fPk(new UnicodeRange(0xf900, 0xfaff), okArray109);
            #S7b[] #sbArray215 = new #S7b[8];
            #sbArray215[0] = #S7b.#5Pk;
            #sbArray215[1] = #S7b.#fQk;
            #sbArray215[3] = #S7b.#aQk;
            #sbArray215[4] = #S7b.#gQk;
            #sbArray215[5] = #S7b.#hPk;
            #sbArray215[6] = #S7b.#LPk;
            #sbArray215[7] = #S7b.#BPk;
            #1Ok[] okArray110 = new #1Ok[] { new #1Ok(null, #sbArray215) };
            mapSets[110] = new #fPk(new UnicodeRange(0xfb00, 0xfb0f), okArray110);
            #S7b[] #sbArray216 = new #S7b[] { #S7b.#5Pk, #S7b.#eQk, #S7b.#hPk };
            #1Ok[] okArray111 = new #1Ok[] { new #1Ok(null, #sbArray216) };
            mapSets[0x6f] = new #fPk(new UnicodeRange(0xfb10, 0xfb1c), okArray111);
            #S7b[] #sbArray217 = new #S7b[8];
            #sbArray217[0] = #S7b.#5Pk;
            #sbArray217[1] = #S7b.#fQk;
            #sbArray217[3] = #S7b.#aQk;
            #sbArray217[4] = #S7b.#gQk;
            #sbArray217[5] = #S7b.#hPk;
            #sbArray217[6] = #S7b.#LPk;
            #sbArray217[7] = #S7b.#BPk;
            #1Ok[] okArray112 = new #1Ok[] { new #1Ok(null, #sbArray217) };
            mapSets[0x70] = new #fPk(new UnicodeRange(0xfb1d, 0xfbff), okArray112);
            #S7b[] #sbArray218 = new #S7b[] { #S7b.#fQk, #S7b.#aQk, #S7b.#gQk, #S7b.#hPk };
            #1Ok[] okArray113 = new #1Ok[] { new #1Ok(null, #sbArray218) };
            mapSets[0x71] = new #fPk(new UnicodeRange(0xfc00, 0xfdcf), okArray113);
            #S7b[] #sbArray219 = new #S7b[] { #S7b.#fQk, #S7b.#aQk, #S7b.#gQk, #S7b.#hPk };
            #1Ok[] okArray114 = new #1Ok[] { new #1Ok(null, #sbArray219) };
            mapSets[0x72] = new #fPk(new UnicodeRange(0xfdf0, 0xfdff), okArray114);
            #S7b[] #sbArray220 = new #S7b[] { #S7b.#PPk, #S7b.#OPk };
            #1Ok[] okArray115 = new #1Ok[5];
            okArray115[0] = new #1Ok(#G.#eg(0xad6), #sbArray220);
            #S7b[] #sbArray221 = new #S7b[] { #S7b.#IPk, #S7b.#HPk };
            okArray115[1] = new #1Ok(#G.#eg(0xae3), #sbArray221);
            #S7b[] #sbArray222 = new #S7b[] { #S7b.#CPk, #S7b.#IPk, #S7b.#HPk };
            okArray115[2] = new #1Ok(#G.#eg(0xaf5), #sbArray222);
            #S7b[] #sbArray223 = new #S7b[] { #S7b.#IPk, #S7b.#HPk };
            okArray115[3] = new #1Ok(#G.#eg(0xaf0), #sbArray223);
            #S7b[] #sbArray224 = new #S7b[] { #S7b.#IPk, #S7b.#HPk };
            okArray115[4] = new #1Ok(null, #sbArray224);
            mapSets[0x73] = new #fPk(new UnicodeRange(0xfe10, 0xfe1f), okArray115);
            #S7b[] #sbArray225 = new #S7b[] { #S7b.#fQk, #S7b.#aQk, #S7b.#gQk, #S7b.#hPk };
            #1Ok[] okArray116 = new #1Ok[] { new #1Ok(null, #sbArray225) };
            mapSets[0x74] = new #fPk(new UnicodeRange(0xfe20, 0xfe2f), okArray116);
            #S7b[] #sbArray226 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#bQk };
            #1Ok[] okArray117 = new #1Ok[5];
            okArray117[0] = new #1Ok(#G.#eg(0xad6), #sbArray226);
            #S7b[] #sbArray227 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray117[1] = new #1Ok(#G.#eg(0xae3), #sbArray227);
            #S7b[] #sbArray228 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray117[2] = new #1Ok(#G.#eg(0xaf0), #sbArray228);
            #S7b[] #sbArray229 = new #S7b[] { #S7b.#CPk, #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray117[3] = new #1Ok(#G.#eg(0xaf5), #sbArray229);
            #S7b[] #sbArray230 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray117[4] = new #1Ok(null, #sbArray230);
            mapSets[0x75] = new #fPk(new UnicodeRange(0xfe30, 0xfe6f), okArray117);
            #S7b[] #sbArray231 = new #S7b[] { #S7b.#fQk, #S7b.#aQk, #S7b.#gQk, #S7b.#hPk };
            #1Ok[] okArray118 = new #1Ok[] { new #1Ok(null, #sbArray231) };
            mapSets[0x76] = new #fPk(new UnicodeRange(0xfe70, 0xfefe), okArray118);
            #S7b[] #sbArray232 = new #S7b[] { #S7b.#5Pk };
            #1Ok[] okArray119 = new #1Ok[] { new #1Ok(null, #sbArray232) };
            mapSets[0x77] = new #fPk(new UnicodeRange(0xfeff, 0xfeff), okArray119);
            #S7b[] #sbArray233 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#WPk };
            #1Ok[] okArray120 = new #1Ok[5];
            okArray120[0] = new #1Ok(#G.#eg(0xad6), #sbArray233);
            #S7b[] #sbArray234 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#WPk };
            okArray120[1] = new #1Ok(#G.#eg(0xae3), #sbArray234);
            #S7b[] #sbArray235 = new #S7b[] { #S7b.#CPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#rPk, #S7b.#WPk };
            okArray120[2] = new #1Ok(#G.#eg(0xaf5), #sbArray235);
            #S7b[] #sbArray236 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray120[3] = new #1Ok(#G.#eg(0xaf0), #sbArray236);
            #S7b[] #sbArray237 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray120[4] = new #1Ok(null, #sbArray237);
            mapSets[120] = new #fPk(new UnicodeRange(0xff00, 0xff60), okArray120);
            #S7b[] #sbArray238 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#RPk };
            #1Ok[] okArray121 = new #1Ok[5];
            okArray121[0] = new #1Ok(#G.#eg(0xad6), #sbArray238);
            #S7b[] #sbArray239 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#RPk };
            okArray121[1] = new #1Ok(#G.#eg(0xae3), #sbArray239);
            #S7b[] #sbArray240 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray121[2] = new #1Ok(#G.#eg(0xaf0), #sbArray240);
            #S7b[] #sbArray241 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray121[3] = new #1Ok(#G.#eg(0xaf5), #sbArray241);
            #S7b[] #sbArray242 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk };
            okArray121[4] = new #1Ok(null, #sbArray242);
            mapSets[0x79] = new #fPk(new UnicodeRange(0xff61, 0xff9f), okArray121);
            #S7b[] #sbArray243 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#IPk, #S7b.#HPk };
            #1Ok[] okArray122 = new #1Ok[5];
            okArray122[0] = new #1Ok(#G.#eg(0xad6), #sbArray243);
            #S7b[] #sbArray244 = new #S7b[] { #S7b.#IPk, #S7b.#HPk };
            okArray122[1] = new #1Ok(#G.#eg(0xae3), #sbArray244);
            #S7b[] #sbArray245 = new #S7b[] { #S7b.#IPk, #S7b.#HPk };
            okArray122[2] = new #1Ok(#G.#eg(0xaf0), #sbArray245);
            #S7b[] #sbArray246 = new #S7b[] { #S7b.#CPk, #S7b.#IPk, #S7b.#HPk };
            okArray122[3] = new #1Ok(#G.#eg(0xaf5), #sbArray246);
            #S7b[] #sbArray247 = new #S7b[] { #S7b.#IPk, #S7b.#HPk };
            okArray122[4] = new #1Ok(null, #sbArray247);
            mapSets[0x7a] = new #fPk(new UnicodeRange(0xffa0, 0xffdc), okArray122);
            #S7b[] #sbArray248 = new #S7b[] { #S7b.#PPk, #S7b.#OPk, #S7b.#WPk, #S7b.#rPk };
            #1Ok[] okArray123 = new #1Ok[5];
            okArray123[0] = new #1Ok(#G.#eg(0xad6), #sbArray248);
            #S7b[] #sbArray249 = new #S7b[] { #S7b.#IPk, #S7b.#HPk, #S7b.#WPk, #S7b.#rPk };
            okArray123[1] = new #1Ok(#G.#eg(0xae3), #sbArray249);
            #S7b[] #sbArray250 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk, #S7b.#rPk };
            okArray123[2] = new #1Ok(#G.#eg(0xaf0), #sbArray250);
            #S7b[] #sbArray251 = new #S7b[] { #S7b.#CPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#IPk, #S7b.#HPk, #S7b.#rPk, #S7b.#WPk };
            okArray123[3] = new #1Ok(#G.#eg(0xaf5), #sbArray251);
            #S7b[] #sbArray252 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk, #S7b.#rPk };
            okArray123[4] = new #1Ok(null, #sbArray252);
            mapSets[0x7b] = new #fPk(new UnicodeRange(0xffe0, 0xffee), okArray123);
            #S7b[] #sbArray253 = new #S7b[] { #S7b.#5Pk, #S7b.#fQk };
            #1Ok[] okArray124 = new #1Ok[] { new #1Ok(null, #sbArray253) };
            mapSets[0x7c] = new #fPk(new UnicodeRange(0xfff0, 0xfffd), okArray124);
            #S7b[] #sbArray254 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray125 = new #1Ok[] { new #1Ok(null, #sbArray254) };
            mapSets[0x7d] = new #fPk(new UnicodeRange(0x10280, 0x1029f), okArray125);
            #S7b[] #sbArray255 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray126 = new #1Ok[] { new #1Ok(null, #sbArray255) };
            mapSets[0x7e] = new #fPk(new UnicodeRange(0x102a0, 0x102df), okArray126);
            #S7b[] #sbArray256 = new #S7b[] { #S7b.#7Pk, #S7b.#8Pk };
            #1Ok[] okArray127 = new #1Ok[] { new #1Ok(null, #sbArray256) };
            mapSets[0x7f] = new #fPk(new UnicodeRange(0x10300, 0x1034f), okArray127);
            #S7b[] #sbArray257 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray128 = new #1Ok[] { new #1Ok(null, #sbArray257) };
            mapSets[0x80] = new #fPk(new UnicodeRange(0x10380, 0x1039f), okArray128);
            #S7b[] #sbArray258 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray129 = new #1Ok[] { new #1Ok(null, #sbArray258) };
            mapSets[0x81] = new #fPk(new UnicodeRange(0x103a0, 0x103df), okArray129);
            #S7b[] #sbArray259 = new #S7b[] { #S7b.#8Pk };
            #1Ok[] okArray130 = new #1Ok[] { new #1Ok(null, #sbArray259) };
            mapSets[130] = new #fPk(new UnicodeRange(0x10400, 0x1044f), okArray130);
            #S7b[] #sbArray260 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray131 = new #1Ok[] { new #1Ok(null, #sbArray260) };
            mapSets[0x83] = new #fPk(new UnicodeRange(0x10450, 0x1047f), okArray131);
            #S7b[] #sbArray261 = new #S7b[] { #S7b.#mPk };
            #1Ok[] okArray132 = new #1Ok[] { new #1Ok(null, #sbArray261) };
            mapSets[0x84] = new #fPk(new UnicodeRange(0x10480, 0x104af), okArray132);
            #S7b[] #sbArray262 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray133 = new #1Ok[] { new #1Ok(null, #sbArray262) };
            mapSets[0x85] = new #fPk(new UnicodeRange(0x10800, 0x1083f), okArray133);
            #S7b[] #sbArray263 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray134 = new #1Ok[] { new #1Ok(null, #sbArray263) };
            mapSets[0x86] = new #fPk(new UnicodeRange(0x10840, 0x1085f), okArray134);
            #S7b[] #sbArray264 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray135 = new #1Ok[] { new #1Ok(null, #sbArray264) };
            mapSets[0x87] = new #fPk(new UnicodeRange(0x10900, 0x1091f), okArray135);
            #S7b[] #sbArray265 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray136 = new #1Ok[] { new #1Ok(null, #sbArray265) };
            mapSets[0x88] = new #fPk(new UnicodeRange(0x10920, 0x1093f), okArray136);
            #S7b[] #sbArray266 = new #S7b[] { #S7b.#7Pk, #S7b.#8Pk };
            #1Ok[] okArray137 = new #1Ok[] { new #1Ok(null, #sbArray266) };
            mapSets[0x89] = new #fPk(new UnicodeRange(0x109a0, 0x109ff), okArray137);
            #S7b[] #sbArray267 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray138 = new #1Ok[] { new #1Ok(null, #sbArray267) };
            mapSets[0x8a] = new #fPk(new UnicodeRange(0x10a00, 0x10a5f), okArray138);
            #S7b[] #sbArray268 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray139 = new #1Ok[] { new #1Ok(null, #sbArray268) };
            mapSets[0x8b] = new #fPk(new UnicodeRange(0x10a60, 0x10a7f), okArray139);
            #S7b[] #sbArray269 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray140 = new #1Ok[] { new #1Ok(null, #sbArray269) };
            mapSets[140] = new #fPk(new UnicodeRange(0x10b40, 0x10b5f), okArray140);
            #S7b[] #sbArray270 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray141 = new #1Ok[] { new #1Ok(null, #sbArray270) };
            mapSets[0x8d] = new #fPk(new UnicodeRange(0x10b60, 0x10b7f), okArray141);
            #S7b[] #sbArray271 = new #S7b[] { #S7b.#7Pk, #S7b.#8Pk };
            #1Ok[] okArray142 = new #1Ok[] { new #1Ok(null, #sbArray271) };
            mapSets[0x8e] = new #fPk(new UnicodeRange(0x10c00, 0x10c4f), okArray142);
            #S7b[] #sbArray272 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray143 = new #1Ok[] { new #1Ok(null, #sbArray272) };
            mapSets[0x8f] = new #fPk(new UnicodeRange(0x11000, 0x1107f), okArray143);
            #S7b[] #sbArray273 = new #S7b[] { #S7b.#0Pk };
            #1Ok[] okArray144 = new #1Ok[] { new #1Ok(null, #sbArray273) };
            mapSets[0x90] = new #fPk(new UnicodeRange(0x110d0, 0x110ff), okArray144);
            #S7b[] #sbArray274 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray145 = new #1Ok[] { new #1Ok(null, #sbArray274) };
            mapSets[0x91] = new #fPk(new UnicodeRange(0x12000, 0x123ff), okArray145);
            #S7b[] #sbArray275 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray146 = new #1Ok[] { new #1Ok(null, #sbArray275) };
            mapSets[0x92] = new #fPk(new UnicodeRange(0x12400, 0x1247f), okArray146);
            #S7b[] #sbArray276 = new #S7b[] { #S7b.#7Pk };
            #1Ok[] okArray147 = new #1Ok[] { new #1Ok(null, #sbArray276) };
            mapSets[0x93] = new #fPk(new UnicodeRange(0x13000, 0x1342f), okArray147);
            #S7b[] #sbArray277 = new #S7b[] { #S7b.#jQk, #S7b.#FPk };
            #1Ok[] okArray148 = new #1Ok[] { new #1Ok(null, #sbArray277) };
            mapSets[0x94] = new #fPk(new UnicodeRange(0x1b000, 0x1b0ff), okArray148);
            #S7b[] #sbArray278 = new #S7b[] { #S7b.#8Pk };
            #1Ok[] okArray149 = new #1Ok[] { new #1Ok(null, #sbArray278) };
            mapSets[0x95] = new #fPk(new UnicodeRange(0x1d300, 0x1d35f), okArray149);
            #S7b[] #sbArray279 = new #S7b[] { #S7b.#8Pk, #S7b.#iPk };
            #1Ok[] okArray150 = new #1Ok[] { new #1Ok(null, #sbArray279) };
            mapSets[150] = new #fPk(new UnicodeRange(0x1d400, 0x1d7ff), okArray150);
            #S7b[] #sbArray280 = new #S7b[] { #S7b.#6Pk, #S7b.#8Pk };
            #1Ok[] okArray151 = new #1Ok[] { new #1Ok(null, #sbArray280) };
            mapSets[0x97] = new #fPk(new UnicodeRange(0x1f000, 0x1f6ff), okArray151);
            #S7b[] #sbArray281 = new #S7b[14];
            #sbArray281[0] = #S7b.#PPk;
            #sbArray281[1] = #S7b.#OPk;
            #sbArray281[2] = #S7b.#jQk;
            #sbArray281[3] = #S7b.#FPk;
            #sbArray281[4] = #S7b.#EPk;
            #sbArray281[5] = #S7b.#5Pk;
            #sbArray281[6] = #S7b.#8Pk;
            #sbArray281[7] = #S7b.#bQk;
            #sbArray281[8] = #S7b.#WPk;
            #sbArray281[9] = #S7b.#RPk;
            #sbArray281[10] = #S7b.#fQk;
            #sbArray281[11] = #S7b.#BPk;
            #sbArray281[13] = #S7b.#hPk;
            #1Ok[] okArray152 = new #1Ok[5];
            okArray152[0] = new #1Ok(#G.#eg(0xad6), #sbArray281);
            #S7b[] #sbArray282 = new #S7b[15];
            #sbArray282[0] = #S7b.#IPk;
            #sbArray282[1] = #S7b.#HPk;
            #sbArray282[2] = #S7b.#jQk;
            #sbArray282[3] = #S7b.#FPk;
            #sbArray282[4] = #S7b.#EPk;
            #sbArray282[5] = #S7b.#5Pk;
            #sbArray282[6] = #S7b.#8Pk;
            #sbArray282[7] = #S7b.#PPk;
            #sbArray282[8] = #S7b.#OPk;
            #sbArray282[9] = #S7b.#RPk;
            #sbArray282[10] = #S7b.#WPk;
            #sbArray282[11] = #S7b.#fQk;
            #sbArray282[12] = #S7b.#BPk;
            #sbArray282[14] = #S7b.#hPk;
            okArray152[1] = new #1Ok(#G.#eg(0xae3), #sbArray282);
            #S7b[] #sbArray283 = new #S7b[0x10];
            #sbArray283[0] = #S7b.#jQk;
            #sbArray283[1] = #S7b.#FPk;
            #sbArray283[2] = #S7b.#EPk;
            #sbArray283[3] = #S7b.#5Pk;
            #sbArray283[4] = #S7b.#8Pk;
            #sbArray283[5] = #S7b.#IPk;
            #sbArray283[6] = #S7b.#HPk;
            #sbArray283[7] = #S7b.#PPk;
            #sbArray283[8] = #S7b.#OPk;
            #sbArray283[9] = #S7b.#CPk;
            #sbArray283[10] = #S7b.#WPk;
            #sbArray283[11] = #S7b.#RPk;
            #sbArray283[12] = #S7b.#fQk;
            #sbArray283[13] = #S7b.#BPk;
            #sbArray283[15] = #S7b.#hPk;
            okArray152[2] = new #1Ok(#G.#eg(0xaf0), #sbArray283);
            #S7b[] #sbArray284 = new #S7b[0x11];
            #sbArray284[0] = #S7b.#CPk;
            #sbArray284[1] = #S7b.#5Pk;
            #sbArray284[2] = #S7b.#8Pk;
            #sbArray284[3] = #S7b.#jQk;
            #sbArray284[4] = #S7b.#FPk;
            #sbArray284[5] = #S7b.#EPk;
            #sbArray284[6] = #S7b.#IPk;
            #sbArray284[7] = #S7b.#HPk;
            #sbArray284[8] = #S7b.#PPk;
            #sbArray284[9] = #S7b.#OPk;
            #sbArray284[10] = #S7b.#rPk;
            #sbArray284[11] = #S7b.#WPk;
            #sbArray284[12] = #S7b.#RPk;
            #sbArray284[13] = #S7b.#fQk;
            #sbArray284[14] = #S7b.#BPk;
            #sbArray284[0x10] = #S7b.#hPk;
            okArray152[3] = new #1Ok(#G.#eg(0xaf5), #sbArray284);
            #S7b[] #sbArray285 = new #S7b[0x11];
            #sbArray285[0] = #S7b.#5Pk;
            #sbArray285[1] = #S7b.#8Pk;
            #sbArray285[2] = #S7b.#jQk;
            #sbArray285[3] = #S7b.#FPk;
            #sbArray285[4] = #S7b.#EPk;
            #sbArray285[5] = #S7b.#IPk;
            #sbArray285[6] = #S7b.#HPk;
            #sbArray285[7] = #S7b.#PPk;
            #sbArray285[8] = #S7b.#OPk;
            #sbArray285[9] = #S7b.#CPk;
            #sbArray285[10] = #S7b.#fQk;
            #sbArray285[11] = #S7b.#BPk;
            #sbArray285[13] = #S7b.#WPk;
            #sbArray285[14] = #S7b.#RPk;
            #sbArray285[15] = #S7b.#hPk;
            #sbArray285[0x10] = #S7b.#RPk;
            okArray152[4] = new #1Ok(null, #sbArray285);
            mapSets[0x98] = new #fPk(new UnicodeRange(0x1f780, 0x1f7ff), okArray152);
            #S7b[] #sbArray286 = new #S7b[] { #S7b.#8Pk, #S7b.#iPk, #S7b.#WPk };
            #1Ok[] okArray153 = new #1Ok[] { new #1Ok(null, #sbArray286) };
            mapSets[0x99] = new #fPk(new UnicodeRange(0x1f800, 0x1f8ff), okArray153);
            #S7b[] #sbArray287 = new #S7b[] { #S7b.#6Pk, #S7b.#8Pk };
            #1Ok[] okArray154 = new #1Ok[] { new #1Ok(null, #sbArray287) };
            mapSets[0x9a] = new #fPk(new UnicodeRange(0x1f900, 0x1f9ff), okArray154);
            #S7b[] #sbArray288 = new #S7b[] { #S7b.#dQk };
            #1Ok[] okArray155 = new #1Ok[6];
            okArray155[0] = new #1Ok(#G.#eg(0xad6), #sbArray288);
            #S7b[] #sbArray289 = new #S7b[] { #S7b.#SPk };
            okArray155[1] = new #1Ok(#G.#eg(0xae3), #sbArray289);
            #S7b[] #sbArray290 = new #S7b[] { #S7b.#UPk, #S7b.#SPk };
            okArray155[2] = new #1Ok(#G.#eg(0xafa), #sbArray290);
            #S7b[] #sbArray291 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#WPk, #S7b.#SPk };
            okArray155[3] = new #1Ok(#G.#eg(0xaf0), #sbArray291);
            #S7b[] #sbArray292 = new #S7b[] { #S7b.#SPk };
            okArray155[4] = new #1Ok(#G.#eg(0xaf5), #sbArray292);
            #S7b[] #sbArray293 = new #S7b[] { #S7b.#SPk };
            okArray155[5] = new #1Ok(null, #sbArray293);
            mapSets[0x9b] = new #fPk(new UnicodeRange(0x20000, 0x2a6df), okArray155);
            #S7b[] #sbArray294 = new #S7b[] { #S7b.#UPk, #S7b.#SPk };
            #1Ok[] okArray156 = new #1Ok[6];
            okArray156[0] = new #1Ok(#G.#eg(0xafa), #sbArray294);
            #S7b[] #sbArray295 = new #S7b[] { #S7b.#SPk };
            okArray156[1] = new #1Ok(#G.#eg(0xad6), #sbArray295);
            #S7b[] #sbArray296 = new #S7b[] { #S7b.#SPk };
            okArray156[2] = new #1Ok(#G.#eg(0xae3), #sbArray296);
            #S7b[] #sbArray297 = new #S7b[] { #S7b.#SPk };
            okArray156[3] = new #1Ok(#G.#eg(0xaf0), #sbArray297);
            #S7b[] #sbArray298 = new #S7b[] { #S7b.#SPk };
            okArray156[4] = new #1Ok(#G.#eg(0xaf5), #sbArray298);
            #S7b[] #sbArray299 = new #S7b[] { #S7b.#SPk };
            okArray156[5] = new #1Ok(null, #sbArray299);
            mapSets[0x9c] = new #fPk(new UnicodeRange(0x2a700, 0x2b81f), okArray156);
            #S7b[] #sbArray300 = new #S7b[] { #S7b.#dQk };
            #1Ok[] okArray157 = new #1Ok[] { new #1Ok(null, #sbArray300) };
            mapSets[0x9d] = new #fPk(new UnicodeRange(0x2b820, 0x2ceaf), okArray157);
            #S7b[] #sbArray301 = new #S7b[] { #S7b.#dQk, #S7b.#SPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk };
            #1Ok[] okArray158 = new #1Ok[6];
            okArray158[0] = new #1Ok(#G.#eg(0xad6), #sbArray301);
            #S7b[] #sbArray302 = new #S7b[] { #S7b.#UPk, #S7b.#SPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk };
            okArray158[1] = new #1Ok(#G.#eg(0xafa), #sbArray302);
            #S7b[] #sbArray303 = new #S7b[] { #S7b.#SPk, #S7b.#jQk, #S7b.#FPk, #S7b.#EPk };
            okArray158[2] = new #1Ok(#G.#eg(0xae3), #sbArray303);
            #S7b[] #sbArray304 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#SPk };
            okArray158[3] = new #1Ok(#G.#eg(0xaf0), #sbArray304);
            #S7b[] #sbArray305 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#SPk };
            okArray158[4] = new #1Ok(#G.#eg(0xaf5), #sbArray305);
            #S7b[] #sbArray306 = new #S7b[] { #S7b.#jQk, #S7b.#FPk, #S7b.#EPk, #S7b.#SPk };
            okArray158[5] = new #1Ok(null, #sbArray306);
            mapSets[0x9e] = new #fPk(new UnicodeRange(0x2f800, 0x2fa1f), okArray158);
            return new FontFamilyMapRepository(mapSets);
        }

        internal void #4Ok()
        {
            HashSet<#S7b> set = new HashSet<#S7b>();
            Dictionary<string, #S7b> dictionary = #5Ok();
            foreach (string str in new InstalledFontCollection().Families.Where<FontFamily>((<>c.<>9__4_0 ??= new Func<FontFamily, bool>(this.#4Sk))).Select<FontFamily, string>(<>c.<>9__4_1 ??= new Func<FontFamily, string>(this.#5Sk)))
            {
                #S7b #sb;
                if (dictionary.TryGetValue(str, out #sb))
                {
                    set.Add(#sb);
                }
            }
            int index = this.MapSets.Count - 1;
            while (index >= 0)
            {
                #fPk pk = this.MapSets[index];
                int num2 = pk.Maps.Count - 1;
                while (true)
                {
                    if (num2 < 0)
                    {
                        if (pk.Maps.Count == 0)
                        {
                            this.MapSets.RemoveAt(index);
                        }
                        index--;
                        break;
                    }
                    #1Ok ok = pk.Maps[num2];
                    int num3 = ok.FontFamilyNames.Count - 1;
                    while (true)
                    {
                        if (num3 < 0)
                        {
                            if (ok.FontFamilyNames.Count == 0)
                            {
                                pk.Maps.RemoveAt(num2);
                            }
                            num2--;
                            break;
                        }
                        if (!set.Contains(ok.FontFamilyNames[num3]))
                        {
                            ok.FontFamilyNames.RemoveAt(num3);
                        }
                        num3--;
                    }
                }
            }
        }

        public static Dictionary<string, #S7b> #5Ok()
        {
            Dictionary<string, #S7b> dictionary = new Dictionary<string, #S7b>();
            foreach (object obj2 in Enum.GetValues(Type.GetTypeFromHandle(typeof(#S7b).TypeHandle)))
            {
                string str = #1Ok.#0Ok((#S7b) obj2);
                dictionary[str] = (#S7b) obj2;
            }
            return dictionary;
        }

        public IEnumerable<string> #6Ok(char #cvf)
        {
            #7Sk sk1 = new #7Sk(-2);
            #7Sk sk2 = new #7Sk(-2);
            sk2.#Xo = this;
            #7Sk local1 = sk2;
            #7Sk local2 = sk2;
            local2.#6Sk = #cvf;
            return local2;
        }

        private int #ehc(int #Ld)
        {
            int num = 0;
            int num2 = this.MapSets.Count - 1;
            while (num <= num2)
            {
                int num3 = (num + num2) / 2;
                UnicodeRange unicodeRange = this.MapSets[num3].UnicodeRange;
                if (unicodeRange.Contains(#Ld))
                {
                    return num3;
                }
                if (unicodeRange.Max > #Ld)
                {
                    num2 = num3 - 1;
                    continue;
                }
                num = num3 + 1;
            }
            return ((num2 < 0) ? -1 : ((this.MapSets[num2].UnicodeRange.Max <= #Ld) ? ~(num2 + 1) : ~num2));
        }

        public FontFamilyMapRepository(params #fPk[] mapSets)
        {
            this.MapSets = new List<#fPk>();
            if (mapSets != null)
            {
                this.MapSets.AddRange(mapSets);
                this.#3Ok();
                this.#4Ok();
            }
        }

        public List<#fPk> MapSets { get; private set; }

        [CompilerGenerated]
        private sealed class #7Sk : IEnumerable<string>, IEnumerator<string>, IDisposable, IEnumerable, IEnumerator
        {
            private int #Vo;
            private string #Uo;
            private int #Wo;
            public FontFamilyMapRepository #Xo;
            private char #cvf;
            public char #6Sk;
            private List<#1Ok>.Enumerator #F1i;
            private List<#S7b>.Enumerator #mL;

            [DebuggerHidden]
            private IEnumerator<string> #dlb()
            {
                FontFamilyMapRepository.#7Sk sk;
                if ((this.#Vo == -2) && (this.#Wo == Thread.CurrentThread.ManagedThreadId))
                {
                    this.#Vo = 0;
                    sk = this;
                }
                else
                {
                    sk = new FontFamilyMapRepository.#7Sk(0) {
                        #Xo = this.#Xo
                    };
                }
                sk.#cvf = this.#6Sk;
                return sk;
            }

            private void #G1i()
            {
                this.#Vo = -1;
                this.#F1i.Dispose();
            }

            private bool #gaf()
            {
                bool flag;
                try
                {
                    int num = this.#Vo;
                    FontFamilyMapRepository repository = this.#Xo;
                    if (num == 0)
                    {
                        this.#Vo = -1;
                        int num2 = repository.#ehc(this.#cvf);
                        if (num2 < 0)
                        {
                            goto TR_0003;
                        }
                        else
                        {
                            #fPk pk = repository.MapSets[num2];
                            this.#F1i = pk.Maps.GetEnumerator();
                            this.#Vo = -3;
                        }
                        goto TR_0006;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            this.#Vo = -4;
                        }
                        else
                        {
                            return false;
                        }
                        goto TR_000A;
                    }
                    return flag;
                TR_0003:
                    return false;
                TR_0006:
                    if (this.#F1i.MoveNext())
                    {
                        #1Ok current = this.#F1i.Current;
                        this.#mL = current.FontFamilyNames.GetEnumerator();
                        this.#Vo = -4;
                    }
                    else
                    {
                        this.#G1i();
                        this.#F1i = new List<#1Ok>.Enumerator();
                        goto TR_0003;
                    }
                TR_000A:
                    while (true)
                    {
                        if (this.#mL.MoveNext())
                        {
                            #S7b current = this.#mL.Current;
                            this.#Uo = #1Ok.#0Ok(current);
                            this.#Vo = 1;
                            flag = true;
                        }
                        else
                        {
                            this.#I1i();
                            this.#mL = new List<#S7b>.Enumerator();
                            goto TR_0006;
                        }
                        break;
                    }
                }
                fault
                {
                    this.#wC();
                }
                return flag;
            }

            private void #I1i()
            {
                this.#Vo = -3;
                this.#mL.Dispose();
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
                if (((num - -4) <= 1) || (num == 1))
                {
                    try
                    {
                        if ((num == -4) || (num == 1))
                        {
                            try
                            {
                            }
                            finally
                            {
                                this.#I1i();
                            }
                        }
                    }
                    finally
                    {
                        this.#G1i();
                    }
                }
            }

            [DebuggerHidden]
            public #7Sk(int <>1__state)
            {
                this.#Vo = <>1__state;
                this.#Wo = Thread.CurrentThread.ManagedThreadId;
            }

            private string System.Collections.Generic.IEnumerator<System.String>.Current =>
                this.#Uo;

            private object System.Collections.IEnumerator.Current =>
                this.#Uo;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontFamilyMapRepository.<>c <>9 = new FontFamilyMapRepository.<>c();
            public static Func<FontFamily, bool> <>9__4_0;
            public static Func<FontFamily, string> <>9__4_1;

            internal bool #4Sk(FontFamily #kIf) => 
                #kIf.IsStyleAvailable(FontStyle.Regular);

            internal string #5Sk(FontFamily #kIf) => 
                #kIf.GetName(0x409);
        }
    }
}

