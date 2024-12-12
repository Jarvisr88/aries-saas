namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using System;
    using System.Collections.Generic;

    internal static class DXHtmlEntities
    {
        private static string[] _entitiesList;
        private static Dictionary<string, char> entitiesLookup;
        private static object lookupLockObject;

        static DXHtmlEntities()
        {
            string[] textArray1 = new string[240];
            textArray1[0] = "\"-quot";
            textArray1[1] = "&-amp";
            textArray1[2] = "<-lt";
            textArray1[3] = ">-gt";
            textArray1[4] = "\x00a0-nbsp";
            textArray1[5] = "\x00a1-iexcl";
            textArray1[6] = "\x00a2-cent";
            textArray1[7] = "\x00a3-pound";
            textArray1[8] = "\x00a4-curren";
            textArray1[9] = "\x00a5-yen";
            textArray1[10] = "\x00a6-brvbar";
            textArray1[11] = "\x00a7-sect";
            textArray1[12] = "\x00a8-uml";
            textArray1[13] = "\x00a9-copy";
            textArray1[14] = "\x00aa-ordf";
            textArray1[15] = "\x00ab-laquo";
            textArray1[0x10] = "\x00ac-not";
            textArray1[0x11] = "\x00ad-shy";
            textArray1[0x12] = "\x00ae-reg";
            textArray1[0x13] = "\x00af-macr";
            textArray1[20] = "\x00b0-deg";
            textArray1[0x15] = "\x00b1-plusmn";
            textArray1[0x16] = "\x00b2-sup2";
            textArray1[0x17] = "\x00b3-sup3";
            textArray1[0x18] = "\x00b4-acute";
            textArray1[0x19] = "\x00b5-micro";
            textArray1[0x1a] = "\x00b6-para";
            textArray1[0x1b] = "\x00b7-middot";
            textArray1[0x1c] = "\x00b8-cedil";
            textArray1[0x1d] = "\x00b9-sup1";
            textArray1[30] = "\x00ba-ordm";
            textArray1[0x1f] = "\x00bb-raquo";
            textArray1[0x20] = "\x00bc-frac14";
            textArray1[0x21] = "\x00bd-frac12";
            textArray1[0x22] = "\x00be-frac34";
            textArray1[0x23] = "\x00bf-iquest";
            textArray1[0x24] = "\x00c0-Agrave";
            textArray1[0x25] = "\x00c1-Aacute";
            textArray1[0x26] = "\x00c2-Acirc";
            textArray1[0x27] = "\x00c3-Atilde";
            textArray1[40] = "\x00c4-Auml";
            textArray1[0x29] = "\x00c5-Aring";
            textArray1[0x2a] = "\x00c6-AElig";
            textArray1[0x2b] = "\x00c7-Ccedil";
            textArray1[0x2c] = "\x00c8-Egrave";
            textArray1[0x2d] = "\x00c9-Eacute";
            textArray1[0x2e] = "\x00ca-Ecirc";
            textArray1[0x2f] = "\x00cb-Euml";
            textArray1[0x30] = "\x00cc-Igrave";
            textArray1[0x31] = "\x00cd-Iacute";
            textArray1[50] = "\x00ce-Icirc";
            textArray1[0x33] = "\x00cf-Iuml";
            textArray1[0x34] = "\x00d0-ETH";
            textArray1[0x35] = "\x00d1-Ntilde";
            textArray1[0x36] = "\x00d2-Ograve";
            textArray1[0x37] = "\x00d3-Oacute";
            textArray1[0x38] = "\x00d4-Ocirc";
            textArray1[0x39] = "\x00d5-Otilde";
            textArray1[0x3a] = "\x00d6-Ouml";
            textArray1[0x3b] = "\x00d7-times";
            textArray1[60] = "\x00d8-Oslash";
            textArray1[0x3d] = "\x00d9-Ugrave";
            textArray1[0x3e] = "\x00da-Uacute";
            textArray1[0x3f] = "\x00db-Ucirc";
            textArray1[0x40] = "\x00dc-Uuml";
            textArray1[0x41] = "\x00dd-Yacute";
            textArray1[0x42] = "\x00de-THORN";
            textArray1[0x43] = "\x00df-szlig";
            textArray1[0x44] = "\x00e0-agrave";
            textArray1[0x45] = "\x00e1-aacute";
            textArray1[70] = "\x00e2-acirc";
            textArray1[0x47] = "\x00e3-atilde";
            textArray1[0x48] = "\x00e4-auml";
            textArray1[0x49] = "\x00e5-aring";
            textArray1[0x4a] = "\x00e6-aelig";
            textArray1[0x4b] = "\x00e7-ccedil";
            textArray1[0x4c] = "\x00e8-egrave";
            textArray1[0x4d] = "\x00e9-eacute";
            textArray1[0x4e] = "\x00ea-ecirc";
            textArray1[0x4f] = "\x00eb-euml";
            textArray1[80] = "\x00ec-igrave";
            textArray1[0x51] = "\x00ed-iacute";
            textArray1[0x52] = "\x00ee-icirc";
            textArray1[0x53] = "\x00ef-iuml";
            textArray1[0x54] = "\x00f0-eth";
            textArray1[0x55] = "\x00f1-ntilde";
            textArray1[0x56] = "\x00f2-ograve";
            textArray1[0x57] = "\x00f3-oacute";
            textArray1[0x58] = "\x00f4-ocirc";
            textArray1[0x59] = "\x00f5-otilde";
            textArray1[90] = "\x00f6-ouml";
            textArray1[0x5b] = "\x00f7-divide";
            textArray1[0x5c] = "\x00f8-oslash";
            textArray1[0x5d] = "\x00f9-ugrave";
            textArray1[0x5e] = "\x00fa-uacute";
            textArray1[0x5f] = "\x00fb-ucirc";
            textArray1[0x60] = "\x00fc-uuml";
            textArray1[0x61] = "\x00fd-yacute";
            textArray1[0x62] = "\x00fe-thorn";
            textArray1[0x63] = "\x00ff-yuml";
            textArray1[100] = "Œ-OElig";
            textArray1[0x65] = "œ-oelig";
            textArray1[0x66] = "Š-Scaron";
            textArray1[0x67] = "š-scaron";
            textArray1[0x68] = "Ÿ-Yuml";
            textArray1[0x69] = "ƒ-fnof";
            textArray1[0x6a] = "ˆ-circ";
            textArray1[0x6b] = "˜-tilde";
            textArray1[0x6c] = "Α-Alpha";
            textArray1[0x6d] = "Β-Beta";
            textArray1[110] = "Γ-Gamma";
            textArray1[0x6f] = "Δ-Delta";
            textArray1[0x70] = "Ε-Epsilon";
            textArray1[0x71] = "Ζ-Zeta";
            textArray1[0x72] = "Η-Eta";
            textArray1[0x73] = "Θ-Theta";
            textArray1[0x74] = "Ι-Iota";
            textArray1[0x75] = "Κ-Kappa";
            textArray1[0x76] = "Λ-Lambda";
            textArray1[0x77] = "Μ-Mu";
            textArray1[120] = "Ν-Nu";
            textArray1[0x79] = "Ξ-Xi";
            textArray1[0x7a] = "Ο-Omicron";
            textArray1[0x7b] = "Π-Pi";
            textArray1[0x7c] = "Ρ-Rho";
            textArray1[0x7d] = "Σ-Sigma";
            textArray1[0x7e] = "Τ-Tau";
            textArray1[0x7f] = "Υ-Upsilon";
            textArray1[0x80] = "Φ-Phi";
            textArray1[0x81] = "Χ-Chi";
            textArray1[130] = "Ψ-Psi";
            textArray1[0x83] = "Ω-Omega";
            textArray1[0x84] = "α-alpha";
            textArray1[0x85] = "β-beta";
            textArray1[0x86] = "γ-gamma";
            textArray1[0x87] = "δ-delta";
            textArray1[0x88] = "ε-epsilon";
            textArray1[0x89] = "ζ-zeta";
            textArray1[0x8a] = "η-eta";
            textArray1[0x8b] = "θ-theta";
            textArray1[140] = "ι-iota";
            textArray1[0x8d] = "κ-kappa";
            textArray1[0x8e] = "λ-lambda";
            textArray1[0x8f] = "μ-mu";
            textArray1[0x90] = "ν-nu";
            textArray1[0x91] = "ξ-xi";
            textArray1[0x92] = "ο-omicron";
            textArray1[0x93] = "π-pi";
            textArray1[0x94] = "ρ-rho";
            textArray1[0x95] = "ς-sigmaf";
            textArray1[150] = "σ-sigma";
            textArray1[0x97] = "τ-tau";
            textArray1[0x98] = "υ-upsilon";
            textArray1[0x99] = "φ-phi";
            textArray1[0x9a] = "χ-chi";
            textArray1[0x9b] = "ψ-psi";
            textArray1[0x9c] = "ω-omega";
            textArray1[0x9d] = "ϑ-thetasym";
            textArray1[0x9e] = "ϒ-upsih";
            textArray1[0x9f] = "ϖ-piv";
            textArray1[160] = " -ensp";
            textArray1[0xa1] = " -emsp";
            textArray1[0xa2] = " -thinsp";
            textArray1[0xa3] = "‌-zwnj";
            textArray1[0xa4] = "‍-zwj";
            textArray1[0xa5] = "‎-lrm";
            textArray1[0xa6] = "‏-rlm";
            textArray1[0xa7] = "–-ndash";
            textArray1[0xa8] = "—-mdash";
            textArray1[0xa9] = "‘-lsquo";
            textArray1[170] = "’-rsquo";
            textArray1[0xab] = "‚-sbquo";
            textArray1[0xac] = "“-ldquo";
            textArray1[0xad] = "”-rdquo";
            textArray1[0xae] = "„-bdquo";
            textArray1[0xaf] = "†-dagger";
            textArray1[0xb0] = "‡-Dagger";
            textArray1[0xb1] = "•-bull";
            textArray1[0xb2] = "…-hellip";
            textArray1[0xb3] = "‰-permil";
            textArray1[180] = "′-prime";
            textArray1[0xb5] = "″-Prime";
            textArray1[0xb6] = "‹-lsaquo";
            textArray1[0xb7] = "›-rsaquo";
            textArray1[0xb8] = "‾-oline";
            textArray1[0xb9] = "⁄-frasl";
            textArray1[0xba] = "€-euro";
            textArray1[0xbb] = "ℑ-image";
            textArray1[0xbc] = "℘-weierp";
            textArray1[0xbd] = "ℜ-real";
            textArray1[190] = "™-trade";
            textArray1[0xbf] = "ℵ-alefsym";
            textArray1[0xc0] = "←-larr";
            textArray1[0xc1] = "↑-uarr";
            textArray1[0xc2] = "→-rarr";
            textArray1[0xc3] = "↓-darr";
            textArray1[0xc4] = "↔-harr";
            textArray1[0xc5] = "↵-crarr";
            textArray1[0xc6] = "⇐-lArr";
            textArray1[0xc7] = "⇑-uArr";
            textArray1[200] = "⇒-rArr";
            textArray1[0xc9] = "⇓-dArr";
            textArray1[0xca] = "⇔-hArr";
            textArray1[0xcb] = "∀-forall";
            textArray1[0xcc] = "∂-part";
            textArray1[0xcd] = "∃-exist";
            textArray1[0xce] = "∅-empty";
            textArray1[0xcf] = "∇-nabla";
            textArray1[0xd0] = "∈-isin";
            textArray1[0xd1] = "∉-notin";
            textArray1[210] = "∋-ni";
            textArray1[0xd3] = "∏-prod";
            textArray1[0xd4] = "∑-sum";
            textArray1[0xd5] = "−-minus";
            textArray1[0xd6] = "∗-lowast";
            textArray1[0xd7] = "√-radic";
            textArray1[0xd8] = "∝-prop";
            textArray1[0xd9] = "∞-infin";
            textArray1[0xda] = "∠-ang";
            textArray1[0xdb] = "∧-and";
            textArray1[220] = "∨-or";
            textArray1[0xdd] = "∩-cap";
            textArray1[0xde] = "∪-cup";
            textArray1[0xdf] = "∫-int";
            textArray1[0xe0] = "∴-there4";
            textArray1[0xe1] = "∼-sim";
            textArray1[0xe2] = "≅-cong";
            textArray1[0xe3] = "≈-asymp";
            textArray1[0xe4] = "≠-ne";
            textArray1[0xe5] = "≡-equiv";
            textArray1[230] = "≤-le";
            textArray1[0xe7] = "≥-ge";
            textArray1[0xe8] = "⊂-sub";
            textArray1[0xe9] = "⊃-sup";
            textArray1[0xea] = "⊄-nsub";
            textArray1[0xeb] = "⊆-sube";
            textArray1[0xec] = "⊇-supe";
            textArray1[0xed] = "⊕-oplus";
            textArray1[0xee] = "⊗-otimes";
            textArray1[0xef] = "⊥-perp";
            _entitiesList = textArray1;
            lookupLockObject = new object();
        }

        internal static char Lookup(string entity)
        {
            char ch;
            if (entitiesLookup == null)
            {
                object lookupLockObject = DXHtmlEntities.lookupLockObject;
                lock (lookupLockObject)
                {
                    if (entitiesLookup == null)
                    {
                        Dictionary<string, char> dictionary = new Dictionary<string, char>();
                        string[] strArray = _entitiesList;
                        int index = 0;
                        while (true)
                        {
                            if (index >= strArray.Length)
                            {
                                entitiesLookup = dictionary;
                                break;
                            }
                            string str = strArray[index];
                            dictionary[str.Substring(2)] = str[0];
                            index++;
                        }
                    }
                }
            }
            return (!entitiesLookup.TryGetValue(entity, out ch) ? '\0' : ch);
        }
    }
}

