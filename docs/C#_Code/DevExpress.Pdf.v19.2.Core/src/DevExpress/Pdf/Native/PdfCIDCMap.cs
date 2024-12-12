namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class PdfCIDCMap : PdfCMap<short>
    {
        private const string encodingsResourceName = "DocumentModel.Fonts.CompositeFonts.encodings.bin";
        private static readonly ThreadLocal<WeakReferenceCache<string, PdfCIDCMap>> predefinedEncodingsCache = new ThreadLocal<WeakReferenceCache<string, PdfCIDCMap>>(() => new WeakReferenceCache<string, PdfCIDCMap>(new Func<string, PdfCIDCMap>(PdfCIDCMap.CreatePredefined)));

        private PdfCIDCMap(IDictionary<byte[], short> map) : base(map)
        {
        }

        private static PdfCIDCMap CreatePredefined(string cmapName)
        {
            if (!(cmapName.EndsWith("-V") || (cmapName == "V")))
            {
                return new PdfCIDCMap(GetPredefinedCMapData(cmapName));
            }
            Dictionary<byte[], short> predefinedCMapData = GetPredefinedCMapData(cmapName);
            cmapName = (cmapName == "V") ? "H" : (cmapName.Substring(0, cmapName.Length - 2) + "-H");
            return new PdfVerticalCIDCMap(predefinedCMapData, new PdfCIDCMap(GetPredefinedCMapData(cmapName)));
        }

        public short GetCID(byte[] code)
        {
            short num;
            base.TryMapCode(code, out num);
            return num;
        }

        public static PdfCIDCMap GetPredefined(string cmapName) => 
            predefinedEncodingsCache.Value.GetValue(cmapName);

        private static unsafe Dictionary<byte[], short> GetPredefinedCMapData(string cmapName)
        {
            Dictionary<byte[], short> dictionary2;
            using (Stream stream = PdfEmbeddedResourceProvider.GetDecompressedEmbeddedResourceStream("DocumentModel.Fonts.CompositeFonts.encodings.bin"))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int num = reader.ReadInt32();
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 < num)
                        {
                            int count = reader.ReadInt32();
                            string str = Encoding.ASCII.GetString(reader.ReadBytes(count));
                            int num4 = reader.ReadInt32();
                            long num5 = stream.Position + num4;
                            if (str != cmapName)
                            {
                                stream.Position = num5;
                                num2++;
                                continue;
                            }
                            Dictionary<byte[], short> dictionary = new Dictionary<byte[], short>();
                            while (true)
                            {
                                if (stream.Position == num5)
                                {
                                    dictionary2 = dictionary;
                                    break;
                                }
                                int num6 = reader.ReadInt32();
                                long num7 = reader.ReadInt32() + stream.Position;
                                while (stream.Position != num7)
                                {
                                    int num8 = reader.ReadInt32();
                                    byte[] key = reader.ReadBytes(num6);
                                    short num9 = reader.ReadInt16();
                                    dictionary.Add(key, num9);
                                    int num10 = 0;
                                    while (num10 < num8)
                                    {
                                        num9 = reader.ReadInt16();
                                        byte[] destinationArray = new byte[key.Length];
                                        Array.Copy(key, destinationArray, key.Length);
                                        int index = destinationArray.Length - 1;
                                        while (true)
                                        {
                                            if (index >= 0)
                                            {
                                                if (destinationArray[index] >= 0xff)
                                                {
                                                    destinationArray[index] = 0;
                                                    index--;
                                                    continue;
                                                }
                                                byte* numPtr1 = &(destinationArray[index]);
                                                numPtr1[0] = (byte) (numPtr1[0] + 1);
                                            }
                                            key = destinationArray;
                                            dictionary.Add(key, num9);
                                            num10++;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return dictionary2;
        }

        public PdfStringCommandData GetStringData(byte[] data, double[] glyphOffsets)
        {
            double[] numArray;
            int codeLength;
            List<byte[]> list = new List<byte[]>();
            List<double> list2 = new List<double>();
            List<short> list3 = new List<short>();
            int length = data.Length;
            for (int i = 0; i < length; i += codeLength)
            {
                PdfCMapFindResult<short> result = this.Find(data, i);
                codeLength = result.CodeLength;
                if (codeLength != 0)
                {
                    list3.Add(result.Value);
                }
                else
                {
                    list3.Add(0x20);
                    codeLength = 1;
                }
                byte[] destinationArray = new byte[codeLength];
                Array.Copy(data, i, destinationArray, 0, codeLength);
                list.Add(destinationArray);
                if (glyphOffsets != null)
                {
                    list2.Add(glyphOffsets[i]);
                }
            }
            if (glyphOffsets == null)
            {
                numArray = new double[list.Count + 1];
            }
            else
            {
                list2.Add(0.0);
                numArray = list2.ToArray();
            }
            return new PdfStringCommandData(list.ToArray(), list3.ToArray(), numArray);
        }

        public static PdfCIDCMap Parse(byte[] data) => 
            new PdfCIDCMap(PdfCIDCMapStreamParser.Parse(data));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfCIDCMap.<>c <>9 = new PdfCIDCMap.<>c();

            internal WeakReferenceCache<string, PdfCIDCMap> <.cctor>b__10_0() => 
                new WeakReferenceCache<string, PdfCIDCMap>(new Func<string, PdfCIDCMap>(PdfCIDCMap.CreatePredefined));
        }

        private class PdfVerticalCIDCMap : PdfCIDCMap
        {
            private readonly PdfCIDCMap horizontalMap;

            public PdfVerticalCIDCMap(IDictionary<byte[], short> map, PdfCIDCMap horizontalMap) : base(map)
            {
                this.horizontalMap = horizontalMap;
            }

            protected override PdfCMapFindResult<short> Find(byte[] code, int position)
            {
                PdfCMapFindResult<short> result = this.horizontalMap.Find(code, position);
                return ((result.CodeLength != 0) ? result : base.Find(code, position));
            }
        }
    }
}

