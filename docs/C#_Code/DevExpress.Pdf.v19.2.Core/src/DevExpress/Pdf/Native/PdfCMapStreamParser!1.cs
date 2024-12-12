namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfCMapStreamParser<TCID> : PdfDocumentParser
    {
        private readonly Dictionary<byte[], TCID> cMapTable;

        protected PdfCMapStreamParser(byte[] data) : base(null, 0, 0, new PdfArrayDataStream(data), 0)
        {
            this.cMapTable = new Dictionary<byte[], TCID>();
        }

        protected override bool CheckDictionaryAlphabeticalToken(string token) => 
            token == "def";

        protected abstract TCID GetCIDFromArray(byte[] bytes);
        protected abstract TCID GetCIDFromValue(int code);
        protected abstract TCID Increment(TCID cid);
        private static bool Increment(byte[] value, byte[] max)
        {
            bool flag = true;
            int length = value.Length;
            int index = 0;
            while (true)
            {
                if (index < length)
                {
                    if (value[index] == max[index])
                    {
                        index++;
                        continue;
                    }
                    flag = false;
                }
                if (!flag)
                {
                    for (int i = length - 1; i >= 0; i--)
                    {
                        byte num4 = value[i];
                        if (num4 != 0xff)
                        {
                            value[i] = (byte) (num4 + 1);
                            return true;
                        }
                        value[i] = 0;
                    }
                }
                return false;
            }
        }

        public IDictionary<byte[], TCID> Parse()
        {
            string str;
            int num3;
            int num = 0;
            goto TR_0040;
        TR_0000:
            return this.cMapTable;
        TR_0004:
            num3++;
        TR_0029:
            while (true)
            {
                if (num3 < num)
                {
                    byte[] sourceArray = this.ReadHexNumber();
                    if (sourceArray == null)
                    {
                        break;
                    }
                    int length = sourceArray.Length;
                    byte[] buffer4 = this.ReadHexNumber();
                    if (buffer4 == null)
                    {
                        break;
                    }
                    int num5 = buffer4.Length;
                    int destinationIndex = length - num5;
                    if (destinationIndex < 0)
                    {
                        byte[] destinationArray = new byte[num5];
                        Array.Copy(sourceArray, 0, destinationArray, -destinationIndex, length);
                        sourceArray = destinationArray;
                    }
                    else if (destinationIndex > 0)
                    {
                        byte[] destinationArray = new byte[length];
                        Array.Copy(buffer4, 0, destinationArray, destinationIndex, num5);
                        buffer4 = destinationArray;
                    }
                    object obj4 = base.ReadObject(false, false);
                    IList<object> list = obj4 as IList<object>;
                    if (list != null)
                    {
                        int num8 = 0;
                        int count = list.Count;
                        do
                        {
                            if (num8 >= count)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            byte[] bytes = list[num8++] as byte[];
                            if (bytes == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            this.cMapTable.Add((byte[]) sourceArray.Clone(), this.GetCIDFromArray(bytes));
                        }
                        while (PdfCMapStreamParser<TCID>.Increment(sourceArray, buffer4));
                        goto TR_0004;
                    }
                    else
                    {
                        byte[] bytes = obj4 as byte[];
                        if (bytes != null)
                        {
                            TCID cIDFromArray = this.GetCIDFromArray(bytes);
                            do
                            {
                                if (cIDFromArray != null)
                                {
                                    this.cMapTable.Add((byte[]) sourceArray.Clone(), cIDFromArray);
                                    cIDFromArray = this.Increment(cIDFromArray);
                                }
                            }
                            while (PdfCMapStreamParser<TCID>.Increment(sourceArray, buffer4));
                            goto TR_0004;
                        }
                        else
                        {
                            int? nullable3 = obj4 as int?;
                            if (nullable3 != null)
                            {
                                int code = nullable3.Value;
                                do
                                {
                                    this.cMapTable.Add((byte[]) sourceArray.Clone(), this.GetCIDFromValue(code));
                                    if (str != "beginnotdefrange")
                                    {
                                        code++;
                                    }
                                }
                                while (PdfCMapStreamParser<TCID>.Increment(sourceArray, buffer4));
                                goto TR_0004;
                            }
                        }
                    }
                }
                break;
            }
        TR_0040:
            while (true)
            {
                if (base.SkipSpaces())
                {
                    object obj2 = base.ReadObject(false, false);
                    if (obj2 == null)
                    {
                        continue;
                    }
                    str = obj2 as string;
                    if (str == null)
                    {
                        int? nullable = obj2 as int?;
                        if (nullable == null)
                        {
                            continue;
                        }
                        num = nullable.Value;
                        continue;
                    }
                    if (str == "endcmap")
                    {
                        goto TR_0000;
                    }
                    else
                    {
                        if ((str == "beginbfchar") || (str == "begincidchar"))
                        {
                            for (int i = 0; i < num; i++)
                            {
                                byte[] key = this.ReadHexNumber();
                                if (key == null)
                                {
                                    break;
                                }
                                object obj3 = base.ReadObject(false, false);
                                byte[] bytes = obj3 as byte[];
                                if (bytes != null)
                                {
                                    this.cMapTable.Add(key, this.GetCIDFromArray(bytes));
                                }
                                else
                                {
                                    int? nullable2 = obj3 as int?;
                                    if (nullable2 == null)
                                    {
                                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                                    }
                                    this.cMapTable.Add(key, this.GetCIDFromValue(nullable2.Value));
                                }
                            }
                            continue;
                        }
                        if ((str != "beginbfrange") && (str != "begincidrange"))
                        {
                            continue;
                        }
                        num3 = 0;
                    }
                }
                else
                {
                    goto TR_0000;
                }
                break;
            }
            goto TR_0029;
        }

        protected override object ReadAlphabeticalObject(bool isHexadecimalStringSeparatedUsingWhiteSpaces, bool isIndirect)
        {
            while (true)
            {
                object obj2 = base.ReadDictionaryOrStream(isHexadecimalStringSeparatedUsingWhiteSpaces, isIndirect);
                if (obj2 != null)
                {
                    return obj2;
                }
                string str = base.ReadToken();
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
                if (!base.ReadNext())
                {
                    return null;
                }
            }
        }

        private byte[] ReadHexNumber()
        {
            int num = 0;
            int num2 = 0;
            if (!base.SkipSpaces())
            {
                return null;
            }
            if (base.Current == 60)
            {
                if (!base.ReadNext())
                {
                    return null;
                }
                while (base.Current != 0x3e)
                {
                    if (!base.IsSpace)
                    {
                        num = (num << 4) + base.HexadecimalDigit;
                        num2++;
                    }
                    if (!base.ReadNext())
                    {
                        return null;
                    }
                }
            }
            base.ReadNext();
            int num3 = (int) Math.Ceiling((double) (((double) num2) / 2.0));
            byte[] buffer = new byte[num3];
            for (int i = num3 - 1; i >= 0; i--)
            {
                buffer[i] = (byte) (num & 0xff);
                num = num >> 8;
            }
            return buffer;
        }
    }
}

