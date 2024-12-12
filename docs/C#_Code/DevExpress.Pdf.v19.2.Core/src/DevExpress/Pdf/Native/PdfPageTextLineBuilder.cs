namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PdfPageTextLineBuilder
    {
        private int wordNumber = 1;
        private int currentDistancesCount;
        private double currentDistancesSum;

        private bool CheckHorizontalPosition(PdfPageWordBuilder wordBuilder, PdfPageTextCharacter previousCharacter, PdfPageTextCharacter currentCharacter)
        {
            PdfOrientedRectangle rectangle = currentCharacter.Character.Rectangle;
            PdfOrientedRectangle rectangle2 = previousCharacter.Character.Rectangle;
            double num = ((rectangle.Width + rectangle2.Width) / 2.0) * 0.3;
            if (PdfTextUtils.HasRTLMark(currentCharacter.Character.UnicodeData) || PdfTextUtils.HasRTLMark(previousCharacter.Character.UnicodeData))
            {
                PdfPoint leftBoundary = wordBuilder.LeftBoundary;
                double angle = rectangle.Angle;
                double num4 = PdfTextUtils.GetOrientedDistance(wordBuilder.RightBoundary, rectangle.TopLeft, angle);
                return ((PdfTextUtils.GetOrientedDistance(rectangle.TopRight, leftBoundary, angle) > num) || (num4 > num));
            }
            PdfTextBlock objB = previousCharacter.Block;
            PdfTextBlock block = currentCharacter.Block;
            double num5 = PdfTextUtils.GetOrientedDistance(previousCharacter.Location, currentCharacter.Location, objB.Angle) - rectangle2.Width;
            double num6 = num * 0.1;
            double num7 = (num + (!ReferenceEquals(block, objB) ? ((block.MinSpaceWidth + objB.MinSpaceWidth) / 2.0) : ((block.MaxSpaceWidth + objB.MaxSpaceWidth) / 2.0))) / 2.0;
            if ((objB.CharacterSpacing == 0.0) || (Math.Abs((double) (1.0 - (objB.CharacterSpacing / num5))) >= 0.3))
            {
                if ((num5 < -0.1) ? (Math.Abs(num5) > (rectangle2.Width + num7)) : (num7 <= (num5 * 1.1)))
                {
                    return ((this.currentDistancesCount == 0) || (Math.Abs(num5) >= ((this.currentDistancesSum / ((double) this.currentDistancesCount)) + num7)));
                }
                if (num5 > num6)
                {
                    this.currentDistancesSum += num5;
                    this.currentDistancesCount++;
                }
            }
            return false;
        }

        public PdfTextLine CreateLine(IReadOnlyList<PdfTextBlock> lineBlocks, PdfRectangle cropBox)
        {
            PdfPageTextCharacter? nullable = null;
            this.currentDistancesCount = 0;
            this.currentDistancesSum = 0.0;
            PdfPageWordBuilder wordBuilder = new PdfPageWordBuilder();
            List<PdfWordPart> parts = new List<PdfWordPart>();
            foreach (PdfPageTextCharacter character in PrepareLine(lineBlocks))
            {
                string unicodeData = character.Character.UnicodeData;
                bool wordEnded = PdfTextUtils.IsWhitespace(unicodeData) || ((nullable != null) && this.CheckHorizontalPosition(wordBuilder, nullable.Value, character));
                if (wordEnded | (PdfTextUtils.HasCJKSymbols(unicodeData) || ((nullable != null) && PdfTextUtils.IsSeparator(nullable.Value.Character.UnicodeData))))
                {
                    PdfWordPart part2 = wordBuilder.CreatePart(wordEnded);
                    if (part2.IsNotEmpty && !OverlapsWithPreviousWords(parts, part2))
                    {
                        parts.Add(part2);
                    }
                    this.currentDistancesCount = 0;
                    this.currentDistancesSum = 0.0;
                    wordBuilder = new PdfPageWordBuilder();
                }
                wordBuilder.AppendChar(cropBox, character.Location, character.Character);
                nullable = new PdfPageTextCharacter?(character);
            }
            PdfWordPart word = wordBuilder.CreatePart(true);
            if (word.IsNotEmpty && !OverlapsWithPreviousWords(parts, word))
            {
                parts.Add(word);
            }
            Func<PdfWordPart, double> keySelector = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<PdfWordPart, double> local1 = <>c.<>9__8_0;
                keySelector = <>c.<>9__8_0 = p => PdfTextUtils.RotatePoint(p.Rectangle.TopLeft, -p.Rectangle.Angle).X;
            }
            parts = parts.OrderBy<PdfWordPart, double>(keySelector).ToList<PdfWordPart>();
            foreach (PdfWordPart part3 in parts)
            {
                int wordNumber = this.wordNumber;
                this.wordNumber = wordNumber + 1;
                part3.WordNumber = wordNumber;
            }
            return ((parts.Count == 0) ? null : new PdfTextLine(parts));
        }

        private static bool IsDuplicate(PdfCharacter ch1, PdfCharacter ch2) => 
            (ch1.FontSize == ch2.FontSize) && ((ch1.UnicodeData == ch2.UnicodeData) && PdfTextUtils.DoOverlap(ch1.Rectangle, ch2.Rectangle));

        private static bool OverlapsWithPreviousWords(IEnumerable<PdfWordPart> parts, PdfWordPart word)
        {
            bool flag;
            using (IEnumerator<PdfWordPart> enumerator = parts.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfWordPart current = enumerator.Current;
                        if (!current.Overlaps(word))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        [IteratorStateMachine(typeof(<PrepareLine>d__0))]
        private static IEnumerable<PdfPageTextCharacter> PrepareLine(IReadOnlyList<PdfTextBlock> lineBlocks)
        {
            <PrepareLine>d__0 d__1 = new <PrepareLine>d__0(-2);
            d__1.<>3__lineBlocks = lineBlocks;
            return d__1;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPageTextLineBuilder.<>c <>9 = new PdfPageTextLineBuilder.<>c();
            public static Func<PdfWordPart, double> <>9__8_0;

            internal double <CreateLine>b__8_0(PdfWordPart p) => 
                PdfTextUtils.RotatePoint(p.Rectangle.TopLeft, -p.Rectangle.Angle).X;
        }

        [CompilerGenerated]
        private sealed class <PrepareLine>d__0 : IEnumerable<PdfPageTextCharacter>, IEnumerable, IEnumerator<PdfPageTextCharacter>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private PdfPageTextCharacter <>2__current;
            private int <>l__initialThreadId;
            private IReadOnlyList<PdfTextBlock> lineBlocks;
            public IReadOnlyList<PdfTextBlock> <>3__lineBlocks;
            private HashSet<PdfCharacter> <duplicates>5__1;
            private PdfTextBlock <block>5__2;
            private IEnumerable<PdfTextBlock> <nextBlocks>5__3;
            private int <j>5__4;
            private int <s>5__5;

            [DebuggerHidden]
            public <PrepareLine>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num2;
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<duplicates>5__1 = new HashSet<PdfCharacter>();
                    this.<s>5__5 = 0;
                    goto TR_0006;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                goto TR_001B;
            TR_0006:
                if (this.<s>5__5 >= this.lineBlocks.Count)
                {
                    return false;
                }
                this.<block>5__2 = this.lineBlocks[this.<s>5__5];
                this.<nextBlocks>5__3 = this.lineBlocks.Skip<PdfTextBlock>(this.<s>5__5 + 1);
                this.<j>5__4 = 0;
            TR_000B:
                while (true)
                {
                    if (this.<j>5__4 < this.<block>5__2.Characters.Count)
                    {
                        if (!this.<duplicates>5__1.Contains(this.<block>5__2.Characters[this.<j>5__4]))
                        {
                            this.<>2__current = new PdfPageTextCharacter(this.<block>5__2.Characters[this.<j>5__4], this.<block>5__2.CharLocations[this.<j>5__4], this.<block>5__2);
                            this.<>1__state = 1;
                            return true;
                        }
                    }
                    else
                    {
                        this.<block>5__2 = null;
                        this.<nextBlocks>5__3 = null;
                        num2 = this.<s>5__5;
                        this.<s>5__5 = num2 + 1;
                        goto TR_0006;
                    }
                    break;
                }
            TR_001B:
                while (true)
                {
                    foreach (PdfTextBlock block in this.<nextBlocks>5__3)
                    {
                        foreach (PdfCharacter character in block.Characters)
                        {
                            if (PdfPageTextLineBuilder.IsDuplicate(character, this.<block>5__2.Characters[this.<j>5__4]))
                            {
                                this.<duplicates>5__1.Add(character);
                            }
                        }
                    }
                    num2 = this.<j>5__4;
                    this.<j>5__4 = num2 + 1;
                    break;
                }
                goto TR_000B;
            }

            [DebuggerHidden]
            IEnumerator<PdfPageTextCharacter> IEnumerable<PdfPageTextCharacter>.GetEnumerator()
            {
                PdfPageTextLineBuilder.<PrepareLine>d__0 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new PdfPageTextLineBuilder.<PrepareLine>d__0(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.lineBlocks = this.<>3__lineBlocks;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Pdf.Native.PdfPageTextCharacter>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            PdfPageTextCharacter IEnumerator<PdfPageTextCharacter>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

