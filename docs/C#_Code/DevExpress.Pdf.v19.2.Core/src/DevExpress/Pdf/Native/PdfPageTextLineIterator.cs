namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class PdfPageTextLineIterator : IEnumerable<IReadOnlyList<PdfTextBlock>>, IEnumerable
    {
        private readonly IList<PdfTextBlock> pageBlocks;

        public PdfPageTextLineIterator(IList<PdfTextBlock> pageBlocks)
        {
            this.pageBlocks = pageBlocks;
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__4))]
        public IEnumerator<IReadOnlyList<PdfTextBlock>> GetEnumerator()
        {
            LineInfoAccumulator <accumulator>5__1;
            int <blockIndex>5__2;
            int num2;
            int num3;
            if (this.pageBlocks.Count != 0)
            {
                <accumulator>5__1 = new LineInfoAccumulator();
                num2 = 0;
                <blockIndex>5__2 = 1;
                goto TR_0006;
            }
        TR_0003:
            num3 = <blockIndex>5__2;
            <blockIndex>5__2 = num3 + 1;
        TR_0006:
            while (true)
            {
                if (<blockIndex>5__2 >= this.pageBlocks.Count)
                {
                    yield return new TextBlockListSegment(this.pageBlocks, num2, this.pageBlocks.Count - 1);
                    <accumulator>5__1 = null;
                }
                else
                {
                    if (!<accumulator>5__1.IsNewLine(this.pageBlocks[<blockIndex>5__2 - 1], this.pageBlocks[<blockIndex>5__2]))
                    {
                        break;
                    }
                    yield return new TextBlockListSegment(this.pageBlocks, num2, <blockIndex>5__2 - 1);
                    num2 = <blockIndex>5__2;
                }
                break;
            }
            goto TR_0003;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();


        private class LineInfoAccumulator
        {
            private double verticalOffsetAccumulator;

            private bool IsIndex(PdfTextBlock previousCharacterBlock, PdfTextBlock currentCharacterBlock)
            {
                PdfCharacter character = currentCharacterBlock.Characters[0];
                PdfCharacter character2 = previousCharacterBlock.Characters[0];
                double height = character2.Rectangle.Height;
                double num2 = character.Rectangle.Height;
                PdfPoint topLeft = character2.Rectangle.TopLeft;
                PdfPoint point2 = character.Rectangle.TopLeft;
                double angle = previousCharacterBlock.Angle;
                double num4 = currentCharacterBlock.Angle;
                if ((Math.Max(height, num2) / Math.Min(height, num2)) <= 1.4999)
                {
                    return false;
                }
                PdfPoint point = topLeft;
                PdfPoint point4 = point2;
                double num5 = height;
                double num6 = num2;
                double num7 = angle;
                double num8 = num4;
                if (height < num2)
                {
                    point = point2;
                    point4 = topLeft;
                    num5 = num2;
                    num6 = height;
                    num7 = num4;
                    num8 = angle;
                }
                PdfPoint point5 = PdfTextUtils.RotatePoint(point, -num7);
                PdfPoint point6 = PdfTextUtils.RotatePoint(point4, -num8);
                return ((PdfMathUtils.Min(point5.Y, point6.Y) - PdfMathUtils.Max(point5.Y - num5, point6.Y - num6)) >= (num6 * 0.45));
            }

            public bool IsNewLine(PdfTextBlock previousCharacterBlock, PdfTextBlock currentCharacterBlock)
            {
                if (previousCharacterBlock.Angle != currentCharacterBlock.Angle)
                {
                    this.verticalOffsetAccumulator = 0.0;
                    return true;
                }
                PdfPoint location = currentCharacterBlock.Location;
                double num3 = Math.Abs((double) (PdfTextUtils.RotatePoint(previousCharacterBlock.Location, -previousCharacterBlock.Angle).Y - PdfTextUtils.RotatePoint(location, -currentCharacterBlock.Angle).Y));
                double num4 = Math.Min(currentCharacterBlock.Characters[0].Rectangle.Height, previousCharacterBlock.Characters[0].Rectangle.Height);
                this.verticalOffsetAccumulator += num3;
                bool flag = this.verticalOffsetAccumulator > (num4 * 0.5);
                bool flag2 = this.IsIndex(previousCharacterBlock, currentCharacterBlock);
                if (flag | flag2)
                {
                    this.verticalOffsetAccumulator = 0.0;
                }
                return (flag && !flag2);
            }
        }

        private class TextBlockListSegment : IReadOnlyList<PdfTextBlock>, IReadOnlyCollection<PdfTextBlock>, IEnumerable<PdfTextBlock>, IEnumerable
        {
            private readonly IList<PdfTextBlock> blocks;
            private readonly int startIndex;
            private readonly int endIndex;

            public TextBlockListSegment(IList<PdfTextBlock> blocks, int startIndex, int endIndex)
            {
                this.blocks = blocks;
                this.startIndex = startIndex;
                this.endIndex = endIndex;
            }

            [IteratorStateMachine(typeof(<GetEnumerator>d__8))]
            public IEnumerator<PdfTextBlock> GetEnumerator()
            {
                <GetEnumerator>d__8 d__1 = new <GetEnumerator>d__8(0);
                d__1.<>4__this = this;
                return d__1;
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            public PdfTextBlock this[int index]
            {
                get
                {
                    if (index >= this.Count)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return this.blocks[this.startIndex + index];
                }
            }

            public int Count =>
                (this.endIndex - this.startIndex) + 1;

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__8 : IEnumerator<PdfTextBlock>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private PdfTextBlock <>2__current;
                public PdfPageTextLineIterator.TextBlockListSegment <>4__this;
                private int <i>5__1;

                [DebuggerHidden]
                public <GetEnumerator>d__8(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<i>5__1 = this.<>4__this.startIndex;
                    }
                    else
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                        int num2 = this.<i>5__1;
                        this.<i>5__1 = num2 + 1;
                    }
                    if (this.<i>5__1 > this.<>4__this.endIndex)
                    {
                        return false;
                    }
                    this.<>2__current = this.<>4__this.blocks[this.<i>5__1];
                    this.<>1__state = 1;
                    return true;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                PdfTextBlock IEnumerator<PdfTextBlock>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }
    }
}

