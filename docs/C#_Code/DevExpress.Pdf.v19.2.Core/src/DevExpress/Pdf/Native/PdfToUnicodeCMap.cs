namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PdfToUnicodeCMap : PdfCMap<string>
    {
        private PdfToUnicodeCMap(IDictionary<byte[], string> map) : base(map)
        {
        }

        public static byte[] CreateCharacterMappingData(IDictionary<int, string> charMap, string fontName, bool shouldUseTwoByteGlyphIndex)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("/CIDInit /ProcSet findresource begin");
                    writer.WriteLine("12 dict begin");
                    writer.WriteLine("begincmap");
                    writer.WriteLine("/CIDSystemInfo");
                    writer.WriteLine("<<");
                    writer.WriteLine("/Registry (Adobe)");
                    writer.WriteLine("/Ordering (" + fontName + ")");
                    writer.WriteLine("/Supplement 0");
                    writer.WriteLine(">> def");
                    writer.WriteLine("/CMapName /" + fontName + " def");
                    writer.WriteLine("/CMapType 2 def");
                    writer.WriteLine("1 begincodespacerange");
                    writer.WriteLine("<0000> <FFFF>");
                    writer.WriteLine("endcodespacerange");
                    List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>(charMap.Count);
                    List<KeyValuePair<int, string>> list2 = new List<KeyValuePair<int, string>>();
                    string format = shouldUseTwoByteGlyphIndex ? "X4" : "X2";
                    if (charMap.Count > 0)
                    {
                        foreach (KeyValuePair<int, string> pair in charMap)
                        {
                            if (pair.Value.Length == 1)
                            {
                                list.Add(pair);
                                continue;
                            }
                            if (!string.IsNullOrEmpty(pair.Value))
                            {
                                list2.Add(pair);
                            }
                        }
                        if (list.Count > 0)
                        {
                            writer.Write(Convert.ToString(list.Count));
                            writer.WriteLine(" beginbfchar");
                            foreach (KeyValuePair<int, string> pair2 in list)
                            {
                                string[] textArray1 = new string[5];
                                textArray1[0] = "<";
                                int key = pair2.Key;
                                textArray1[1] = key.ToString(format);
                                textArray1[2] = "> <";
                                ushort num2 = pair2.Value[0];
                                textArray1[3] = num2.ToString("X4");
                                textArray1[4] = ">";
                                writer.WriteLine(string.Concat(textArray1));
                            }
                            writer.WriteLine("endbfchar");
                        }
                        if (list2.Count > 0)
                        {
                            writer.Write(Convert.ToString(list2.Count));
                            writer.WriteLine(" beginbfrange");
                            foreach (KeyValuePair<int, string> pair3 in list2)
                            {
                                string str2 = pair3.Key.ToString(format);
                                string[] textArray2 = new string[] { "<", str2, "><", str2, "> [<" };
                                writer.Write(string.Concat(textArray2));
                                string str3 = pair3.Value;
                                int num3 = 0;
                                while (true)
                                {
                                    if (num3 >= str3.Length)
                                    {
                                        writer.WriteLine(">]");
                                        break;
                                    }
                                    writer.Write(((ushort) str3[num3]).ToString("X4"));
                                    num3++;
                                }
                            }
                            writer.WriteLine("endbfrange");
                        }
                    }
                    writer.WriteLine("endcmap");
                    writer.WriteLine("CMapName currentdict /CMap defineresource pop");
                    writer.WriteLine("end");
                    writer.WriteLine("end");
                }
                return stream.ToArray();
            }
        }

        public string GetUnicode(byte[] code)
        {
            string str;
            return (!base.TryMapCode(code, out str) ? null : str);
        }

        public static PdfToUnicodeCMap Parse(byte[] data) => 
            new PdfToUnicodeCMap(PdfToUnicodeCMapStreamParser.Parse(data));

        protected override string DefaultValue =>
            string.Empty;
    }
}

