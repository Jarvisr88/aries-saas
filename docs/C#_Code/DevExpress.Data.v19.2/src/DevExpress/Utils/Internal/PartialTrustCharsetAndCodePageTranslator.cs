namespace DevExpress.Utils.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class PartialTrustCharsetAndCodePageTranslator : DXCharsetAndCodePageTranslator
    {
        private static Dictionary<int, int> CharsetToCodePage = InitializeCharsetTable();

        public override int CharsetFromCodePage(int codePage)
        {
            int num2;
            using (Dictionary<int, int>.KeyCollection.Enumerator enumerator = CharsetToCodePage.Keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        int current = enumerator.Current;
                        if (CharsetToCodePage[current] != codePage)
                        {
                            continue;
                        }
                        num2 = current;
                    }
                    else
                    {
                        return 0;
                    }
                    break;
                }
            }
            return num2;
        }

        public override int CodePageFromCharset(int charset)
        {
            int num = -1;
            return (!CharsetToCodePage.TryGetValue(charset, out num) ? DXEncoding.GetEncodingCodePage(DXEncoding.Default) : num);
        }

        private static Dictionary<int, int> InitializeCharsetTable() => 
            new Dictionary<int, int> { 
                { 
                    0,
                    0x4e4
                },
                { 
                    2,
                    0x2a
                },
                { 
                    0x4d,
                    0x2710
                },
                { 
                    0x4e,
                    0x2711
                },
                { 
                    0x4f,
                    0x2713
                },
                { 
                    80,
                    0x2718
                },
                { 
                    0x51,
                    0x2712
                },
                { 
                    0x53,
                    0x2715
                },
                { 
                    0x54,
                    0x2714
                },
                { 
                    0x55,
                    0x2716
                },
                { 
                    0x56,
                    0x2761
                },
                { 
                    0x57,
                    0x2725
                },
                { 
                    0x58,
                    0x272d
                },
                { 
                    0x59,
                    0x2717
                },
                { 
                    0x80,
                    0x3a4
                },
                { 
                    0x81,
                    0x3b5
                },
                { 
                    130,
                    0x551
                },
                { 
                    0x86,
                    0x3a8
                },
                { 
                    0x88,
                    950
                },
                { 
                    0xa1,
                    0x4e5
                },
                { 
                    0xa2,
                    0x4e6
                },
                { 
                    0xa3,
                    0x4ea
                },
                { 
                    0xb1,
                    0x4e7
                },
                { 
                    0xb2,
                    0x4e8
                },
                { 
                    0xba,
                    0x4e9
                },
                { 
                    0xcc,
                    0x4e3
                },
                { 
                    0xde,
                    0x36a
                },
                { 
                    0xee,
                    0x4e2
                },
                { 
                    0xfe,
                    0x1b5
                },
                { 
                    0xff,
                    850
                }
            };
    }
}

