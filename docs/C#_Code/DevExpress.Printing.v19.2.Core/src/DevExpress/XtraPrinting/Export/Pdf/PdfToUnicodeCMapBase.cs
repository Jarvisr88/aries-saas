namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;

    internal abstract class PdfToUnicodeCMapBase : PdfDocumentStreamObject
    {
        public static string map;

        public PdfToUnicodeCMapBase(bool compressed) : base(compressed)
        {
        }

        protected abstract string CreateCMapName();
        public override void FillUp()
        {
            base.Stream.SetStringLine("/CIDInit /ProcSet findresource begin");
            base.Stream.SetStringLine("12 dict begin");
            base.Stream.SetStringLine("begincmap");
            base.Stream.SetStringLine("/CIDSystemInfo");
            base.Stream.SetStringLine("<<");
            base.Stream.SetStringLine("/Registry (Adobe)");
            base.Stream.SetStringLine("/Ordering (" + this.CreateCMapName() + ")");
            base.Stream.SetStringLine("/Supplement 0");
            base.Stream.SetStringLine(">> def");
            base.Stream.SetStringLine("/CMapName /" + this.CreateCMapName() + " def");
            base.Stream.SetStringLine("/CMapType 2 def");
            base.Stream.SetStringLine("1 begincodespacerange");
            base.Stream.SetStringLine("<0000> <FFFF>");
            base.Stream.SetStringLine("endcodespacerange");
            if (map == null)
            {
                this.FillUpCharMap();
            }
            else
            {
                base.Stream.SetString(map);
            }
            base.Stream.SetStringLine("endcmap");
            base.Stream.SetStringLine("CMapName currentdict /CMap defineresource pop");
            base.Stream.SetStringLine("end");
            base.Stream.SetStringLine("end");
        }

        protected void FillUpCharMap()
        {
            ICollection<KeyValuePair<ushort, char[]>> charMap = this.GetCharCache().GetCharMap();
            List<KeyValuePair<ushort, char[]>> list = new List<KeyValuePair<ushort, char[]>>(charMap.Count);
            List<KeyValuePair<ushort, char[]>> list2 = new List<KeyValuePair<ushort, char[]>>();
            if (charMap.Count > 0)
            {
                ushort key;
                foreach (KeyValuePair<ushort, char[]> pair in charMap)
                {
                    if (pair.Value.Length == 1)
                    {
                        list.Add(pair);
                        continue;
                    }
                    list2.Add(pair);
                }
                if (list.Count > 0)
                {
                    base.Stream.SetString(Convert.ToString(list.Count));
                    base.Stream.SetStringLine(" beginbfchar");
                    foreach (KeyValuePair<ushort, char[]> pair2 in list)
                    {
                        string[] textArray1 = new string[5];
                        textArray1[0] = "<";
                        textArray1[1] = pair2.Key.ToString("X4");
                        textArray1[2] = "> <";
                        key = pair2.Value[0];
                        textArray1[3] = key.ToString("X4");
                        textArray1[4] = ">";
                        base.Stream.SetStringLine(string.Concat(textArray1));
                    }
                    base.Stream.SetStringLine("endbfchar");
                }
                if (list2.Count > 0)
                {
                    base.Stream.SetString(Convert.ToString(list2.Count));
                    base.Stream.SetStringLine(" beginbfrange");
                    foreach (KeyValuePair<ushort, char[]> pair3 in list2)
                    {
                        key = pair3.Key;
                        string str = key.ToString("X4");
                        string[] textArray2 = new string[] { "<", str, "><", str, "><" };
                        base.Stream.SetString(string.Concat(textArray2));
                        char[] chArray = pair3.Value;
                        int index = 0;
                        while (true)
                        {
                            if (index >= chArray.Length)
                            {
                                base.Stream.SetStringLine(">");
                                break;
                            }
                            char ch = chArray[index];
                            key = ch;
                            base.Stream.SetString(key.ToString("X4"));
                            index++;
                        }
                    }
                    base.Stream.SetStringLine("endbfrange");
                }
            }
        }

        protected abstract PdfCharCache GetCharCache();
    }
}

