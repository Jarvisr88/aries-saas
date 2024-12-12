namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class JBIG2HuffmanTableBuilder
    {
        public static unsafe int?[] AssignPrefixCodes(IList<int> preflen, int codesNumber, int prefixSize)
        {
            int[] numArray = new int[1 << (prefixSize & 0x1f)];
            foreach (int num3 in preflen)
            {
                int* numPtr1 = &(numArray[num3]);
                numPtr1[0]++;
            }
            int num = 0;
            int index = numArray.Length - 1;
            while (true)
            {
                if (index >= 0)
                {
                    if (numArray[index] <= 0)
                    {
                        index--;
                        continue;
                    }
                    num = index;
                }
                int num2 = 1;
                int[] numArray2 = new int[num + 1];
                int?[] nullableArray = new int?[codesNumber];
                numArray[0] = 0;
                while (num2 <= num)
                {
                    numArray2[num2] = (numArray2[num2 - 1] + numArray[num2 - 1]) * 2;
                    int num5 = numArray2[num2];
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= codesNumber)
                        {
                            num2++;
                            break;
                        }
                        if (preflen[num6] == num2)
                        {
                            nullableArray[num6] = new int?(num5);
                            num5++;
                        }
                        num6++;
                    }
                }
                return nullableArray;
            }
        }

        public static IHuffmanTreeNode BuildHuffmanTree(JBIG2HuffmanTableModel tableModel)
        {
            int?[] source = AssignPrefixCodes(tableModel.Preflen, tableModel.ntemp, tableModel.PrefixSize);
            int count = tableModel.Rangelen.Count;
            JBIG2HuffmanTreeBuilder builder = new JBIG2HuffmanTreeBuilder();
            for (int i = 0; i < count; i++)
            {
                if (i == (count - 2))
                {
                    builder.AddLowerRangeLine(new JBIG2HuffmanTableLine(source[i], tableModel.Preflen[i], tableModel.Rangelen[i], tableModel.Rangelow[i]));
                }
                else
                {
                    builder.AddTableLine(new JBIG2HuffmanTableLine(source[i], tableModel.Preflen[i], tableModel.Rangelen[i], tableModel.Rangelow[i]));
                }
            }
            if (tableModel.HTOOB)
            {
                builder.AddOOBLine(new JBIG2HuffmanTableLine(source.Last<int?>(), tableModel.Preflen.Last<int>(), 0, 0));
            }
            return builder.RootNode;
        }
    }
}

