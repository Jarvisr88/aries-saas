namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfType1FontClassicFontPrivateData : PdfType1FontPrivateData
    {
        private readonly IList<object> rd;
        private readonly IList<object> nd;
        private readonly IList<object> np;
        private readonly IList<object> subrs;
        private readonly IList<object> otherSubrs;
        private readonly int uniqueID;
        private readonly int lenIV = 4;
        private readonly IList<object> erode;
        private readonly string source;

        internal PdfType1FontClassicFontPrivateData(PdfPostScriptDictionary dictionary)
        {
            base.BlueFuzz = 3;
            bool flag = true;
            using (IEnumerator<PdfPostScriptDictionaryEntry> enumerator = dictionary.GetEnumerator())
            {
                PdfPostScriptDictionaryEntry current;
                goto TR_0075;
            TR_0012:
                this.np = PdfType1FontClassicFontProgram.ToList(current.Value);
                goto TR_0075;
            TR_0015:
                this.nd = PdfType1FontClassicFontProgram.ToList(current.Value);
            TR_0075:
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        string key = current.Key;
                        uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
                        if (num > 0x4336ba8b)
                        {
                            if (num <= 0x7904f763)
                            {
                                if (num <= 0x5cdd136b)
                                {
                                    if (num != 0x4fcd670c)
                                    {
                                        if (num == 0x56a97c9f)
                                        {
                                            if (key != "BlueShift")
                                            {
                                                continue;
                                            }
                                            base.BlueShift = PdfType1FontClassicFontProgram.ToInt32(current.Value);
                                            continue;
                                        }
                                        if (num != 0x5cdd136b)
                                        {
                                            continue;
                                        }
                                        if (key == "lenIV")
                                        {
                                            break;
                                        }
                                        continue;
                                    }
                                    if (key != "-|")
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (num <= 0x634b4faa)
                                    {
                                        if (num != 0x61e2a3f8)
                                        {
                                            if (num != 0x634b4faa)
                                            {
                                                continue;
                                            }
                                            if (key != "OtherBlues")
                                            {
                                                continue;
                                            }
                                            base.OtherBlues = ToGlyphZones(current.Value);
                                            continue;
                                        }
                                        if (key != "Source")
                                        {
                                            continue;
                                        }
                                        byte[] buffer = current.Value as byte[];
                                        if (buffer == null)
                                        {
                                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                                        }
                                        this.source = PdfDocumentReader.ConvertToString(buffer);
                                        continue;
                                    }
                                    if (num == 0x735bb632)
                                    {
                                        if (key != "OtherSubrs")
                                        {
                                            continue;
                                        }
                                        this.otherSubrs = PdfType1FontClassicFontProgram.ToList(current.Value);
                                        continue;
                                    }
                                    if (num != 0x7904f763)
                                    {
                                        continue;
                                    }
                                    if (key != "RD")
                                    {
                                        continue;
                                    }
                                }
                                this.rd = PdfType1FontClassicFontProgram.ToList(current.Value);
                                continue;
                            }
                            if (num <= 0xc1d2a0ea)
                            {
                                if (num <= 0x8e247755)
                                {
                                    if (num == 0x81ae3421)
                                    {
                                        if (key != "StdVW")
                                        {
                                            continue;
                                        }
                                        base.StdVW = new double?(ToStemWidth(current.Value));
                                        continue;
                                    }
                                    if (num != 0x8e247755)
                                    {
                                        continue;
                                    }
                                    if (key != "ExpansionFactor")
                                    {
                                        continue;
                                    }
                                    base.ExpansionFactor = PdfDocumentReader.ConvertToDouble(current.Value);
                                    continue;
                                }
                                if (num != 0xbe69a46b)
                                {
                                    if (num != 0xc1d2a0ea)
                                    {
                                        continue;
                                    }
                                    if (key != "ForceBoldThreshold")
                                    {
                                        continue;
                                    }
                                    base.ForceBoldThreshold = new double?(PdfDocumentReader.ConvertToDouble(current.Value));
                                    continue;
                                }
                                if (key != "MinFeature")
                                {
                                    continue;
                                }
                                IList<object> list = PdfType1FontClassicFontProgram.ToList(current.Value);
                                if ((list.Count == 2) && ((PdfType1FontClassicFontProgram.ToInt32(list[0]) == 0x10) && (PdfType1FontClassicFontProgram.ToInt32(list[1]) == 0x10)))
                                {
                                    continue;
                                }
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                                continue;
                            }
                            if (num <= 0xd39a32d8)
                            {
                                if (num == 0xc81e129b)
                                {
                                    if (key != "UniqueID")
                                    {
                                        continue;
                                    }
                                    this.uniqueID = PdfType1FontClassicFontProgram.ToInt32(current.Value);
                                    continue;
                                }
                                if (num != 0xd39a32d8)
                                {
                                    continue;
                                }
                                if (key != "Erode")
                                {
                                    continue;
                                }
                                this.erode = PdfType1FontClassicFontProgram.ToList(current.Value);
                                continue;
                            }
                            if (num == 0xe8d5326e)
                            {
                                if (key != "FamilyBlues")
                                {
                                    continue;
                                }
                                base.FamilyBlues = ToGlyphZones(current.Value);
                                continue;
                            }
                            if (num != 0xf90c4a3b)
                            {
                                continue;
                            }
                            if (key != "|")
                            {
                                continue;
                            }
                            goto TR_0012;
                        }
                        else if (num > 0x364b5f18)
                        {
                            if (num > 0x3d33031e)
                            {
                                if (num <= 0x407a23fc)
                                {
                                    if (num == 0x3ee6ebf2)
                                    {
                                        if (key != "LanguageGroup")
                                        {
                                            continue;
                                        }
                                        base.LanguageGroup = PdfType1FontClassicFontProgram.ToInt32(current.Value);
                                        continue;
                                    }
                                    if (num != 0x407a23fc)
                                    {
                                        continue;
                                    }
                                    if (key != "StemSnapH")
                                    {
                                        continue;
                                    }
                                    base.StemSnapH = ToStemSnap(current.Value);
                                    continue;
                                }
                                if (num == 0x41e3e95b)
                                {
                                    if (key != "StdHW")
                                    {
                                        continue;
                                    }
                                    base.StdHW = new double?(ToStemWidth(current.Value));
                                    continue;
                                }
                                if (num != 0x4336ba8b)
                                {
                                    continue;
                                }
                                if (key != "LenIV")
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (num <= 0x36fa6002)
                                {
                                    if (num == 0x367a143e)
                                    {
                                        if (key != "StemSnapV")
                                        {
                                            continue;
                                        }
                                        base.StemSnapV = ToStemSnap(current.Value);
                                        continue;
                                    }
                                    if (num != 0x36fa6002)
                                    {
                                        continue;
                                    }
                                    if (key != "Subrs")
                                    {
                                        continue;
                                    }
                                    this.subrs = PdfType1FontClassicFontProgram.ToList(current.Value);
                                    continue;
                                }
                                if (num != 0x38e67d8f)
                                {
                                    if (num != 0x3d33031e)
                                    {
                                        continue;
                                    }
                                    if (key != "FamilyOtherBlues")
                                    {
                                        continue;
                                    }
                                    base.FamilyOtherBlues = ToGlyphZones(current.Value);
                                    continue;
                                }
                                if (key != "ND")
                                {
                                    continue;
                                }
                                goto TR_0015;
                            }
                        }
                        else
                        {
                            if (num <= 0x1f24e0f9)
                            {
                                if (num == 0x463e113)
                                {
                                    if (key != "BlueValues")
                                    {
                                        continue;
                                    }
                                    base.BlueValues = ToGlyphZones(current.Value);
                                    continue;
                                }
                                if (num != 0x1f03804f)
                                {
                                    if (num != 0x1f24e0f9)
                                    {
                                        continue;
                                    }
                                    if (key != "BlueScale")
                                    {
                                        continue;
                                    }
                                    base.BlueScale = PdfDocumentReader.ConvertToDouble(current.Value);
                                    continue;
                                }
                                if (key != "ForceBold")
                                {
                                    continue;
                                }
                                object obj2 = current.Value;
                                if (!(obj2 as bool))
                                {
                                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                                }
                                base.ForceBold = (bool) obj2;
                                continue;
                            }
                            if (num > 0x24e65e13)
                            {
                                if (num == 0x32041da8)
                                {
                                    if (key != "BlueFuzz")
                                    {
                                        continue;
                                    }
                                    base.BlueFuzz = PdfType1FontClassicFontProgram.ToInt32(current.Value);
                                    continue;
                                }
                                if (num != 0x364b5f18)
                                {
                                    continue;
                                }
                                if (key != "password")
                                {
                                    continue;
                                }
                                if (PdfType1FontClassicFontProgram.ToInt32(current.Value) != 0x16cf)
                                {
                                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                                }
                                flag = false;
                                continue;
                            }
                            if (num == 0x2458a0a2)
                            {
                                if (key != "|-")
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (num != 0x24e65e13)
                                {
                                    continue;
                                }
                                if (key != "NP")
                                {
                                    continue;
                                }
                                goto TR_0012;
                            }
                            goto TR_0015;
                        }
                    }
                    else
                    {
                        goto TR_0005;
                    }
                    break;
                }
                this.lenIV = PdfType1FontClassicFontProgram.ToInt32(current.Value);
                goto TR_0075;
            }
        TR_0005:
            if (flag)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            base.Validate();
        }

        private static PdfType1FontGlyphZone[] ToGlyphZones(object value)
        {
            IList<object> list = PdfType1FontClassicFontProgram.ToList(value);
            int count = list.Count;
            if ((count % 2) == 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            count /= 2;
            PdfType1FontGlyphZone[] zoneArray = new PdfType1FontGlyphZone[count];
            int index = 0;
            int num3 = 0;
            while (index < count)
            {
                zoneArray[index] = new PdfType1FontGlyphZone(PdfDocumentReader.ConvertToDouble(list[num3++]), PdfDocumentReader.ConvertToDouble(list[num3++]));
                index++;
            }
            return zoneArray;
        }

        private static double[] ToStemSnap(object value)
        {
            IList<object> list = PdfType1FontClassicFontProgram.ToList(value);
            int count = list.Count;
            double[] numArray = new double[count];
            for (int i = 0; i < count; i++)
            {
                numArray[i] = PdfDocumentReader.ConvertToDouble(list[i]);
            }
            return numArray;
        }

        private static double ToStemWidth(object value)
        {
            IList<object> list = PdfType1FontClassicFontProgram.ToList(value);
            if (list.Count != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfDocumentReader.ConvertToDouble(list[0]);
        }

        public IList<object> RD =>
            this.rd;

        public IList<object> ND =>
            this.nd;

        public IList<object> NP =>
            this.np;

        public IList<object> Subrs =>
            this.subrs;

        public IList<object> OtherSubrs =>
            this.otherSubrs;

        public int UniqueID =>
            this.uniqueID;

        public int LenIV =>
            this.lenIV;

        public IList<object> Erode =>
            this.erode;

        public string Source =>
            this.source;
    }
}

