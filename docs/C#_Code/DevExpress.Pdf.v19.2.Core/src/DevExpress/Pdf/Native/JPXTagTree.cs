namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class JPXTagTree
    {
        private readonly int levelCount;
        private readonly JPXTagTreeNode[][,] data;
        private readonly Stack<JPXTagTreeIndex> indexStack = new Stack<JPXTagTreeIndex>();
        private int currentLevel;

        public JPXTagTree(int width, int height)
        {
            this.levelCount = ((int) Math.Ceiling(Math.Log((double) Math.Max(width, height), 2.0))) + 1;
            this.data = new JPXTagTreeNode[this.levelCount][,];
            for (int i = this.levelCount - 1; i >= 0; i--)
            {
                this.data[i] = new JPXTagTreeNode[width, height];
                width = (int) Math.Ceiling((double) (((double) width) / 2.0));
                height = (int) Math.Ceiling((double) (((double) height) / 2.0));
            }
        }

        private JPXTagTreeNode GetPreviousValue(int x, int y)
        {
            JPXTagTreeNode node;
            int num;
            this.currentLevel = this.levelCount - 1;
            do
            {
                node = this.data[this.currentLevel][x, y];
                if ((node == null) || !node.IsDefined)
                {
                    this.indexStack.Push(new JPXTagTreeIndex(x, y));
                }
                x = x >> 1;
                y = y >> 1;
                if (node != null)
                {
                    break;
                }
                num = this.currentLevel - 1;
                this.currentLevel = num;
            }
            while (num >= 0);
            this.currentLevel++;
            return (node ?? new JPXTagTreeNode(0, true));
        }

        public int Read(int x, int y, PdfBitReader bitReader)
        {
            int num = this.GetPreviousValue(x, y).Value;
            while (this.currentLevel < this.levelCount)
            {
                if (bitReader.GetBit() == 1)
                {
                    this.SetNextValue(num);
                    continue;
                }
                num++;
            }
            return num;
        }

        public int ReadInclusion(int x, int y, PdfBitReader bitReader, int maxValue)
        {
            JPXTagTreeNode previousValue = this.GetPreviousValue(x, y);
            if (previousValue.Value > maxValue)
            {
                return previousValue.Value;
            }
            if ((this.currentLevel > 0) && !previousValue.IsDefined)
            {
                this.currentLevel--;
            }
            int num = previousValue.Value;
            while (this.currentLevel < this.levelCount)
            {
                if (bitReader.GetBit() == 1)
                {
                    previousValue = this.SetNextValue(num);
                    continue;
                }
                if (++num > maxValue)
                {
                    JPXTagTreeIndex index = this.indexStack.Pop();
                    this.data[this.currentLevel][index.X, index.Y] = new JPXTagTreeNode(num, false);
                    return num;
                }
            }
            return num;
        }

        private JPXTagTreeNode SetNextValue(int value)
        {
            JPXTagTreeNode node;
            JPXTagTreeIndex index = this.indexStack.Pop();
            int currentLevel = this.currentLevel;
            this.currentLevel = currentLevel + 1;
            this.data[currentLevel][index.X, index.Y] = node = new JPXTagTreeNode(value, true);
            return node;
        }
    }
}

