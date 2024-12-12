namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct TextScanner
    {
        private int lastPosition;
        private int position;
        private int leadSpaceCount;
        private string text;
        private StringFormat stringFormat;
        private Word lastWord;
        public TextScanner(string text, StringFormat stringFormat)
        {
            this.lastPosition = this.position = 0;
            this.leadSpaceCount = 0;
            this.lastWord = null;
            this.text = text;
            this.stringFormat = stringFormat;
        }

        private CharKind GetKind(int pos)
        {
            char c = this.text[pos];
            if (c > '-')
            {
                if (c > ']')
                {
                    if (c == '{')
                    {
                        goto TR_0006;
                    }
                    else if (c == '}')
                    {
                        goto TR_0007;
                    }
                    else if (c == '\x00a0')
                    {
                        return CharKind.NonBreakingSpace;
                    }
                }
                else if (c == '?')
                {
                    goto TR_0005;
                }
                else
                {
                    switch (c)
                    {
                        case '[':
                            goto TR_0006;

                        case '\\':
                            return CharKind.BackSlash;

                        case ']':
                            goto TR_0007;

                        default:
                            break;
                    }
                }
                goto TR_0000;
            }
            else if (c > ' ')
            {
                if (c == '!')
                {
                    goto TR_0005;
                }
                else
                {
                    switch (c)
                    {
                        case '%':
                            goto TR_0005;

                        case '(':
                            goto TR_0006;

                        case ')':
                            break;

                        case '+':
                        case '-':
                            goto TR_0003;

                        default:
                            goto TR_0000;
                    }
                }
            }
            else
            {
                if (c == '\t')
                {
                    goto TR_0003;
                }
                else if (c == ' ')
                {
                    return CharKind.WhiteSpace;
                }
                goto TR_0000;
            }
            goto TR_0007;
        TR_0000:
            return (!char.IsDigit(c) ? CharKind.None : CharKind.Digit);
        TR_0003:
            return CharKind.Separator;
        TR_0005:
            return CharKind.InseparableEndSymbol;
        TR_0006:
            return CharKind.OpenBrace;
        TR_0007:
            return CharKind.CloseBrace;
        }

        private static bool IsNonSplittableWord(string text) => 
            !string.IsNullOrEmpty(text) && (text.StartsWith(",") || (text.StartsWith(".") || text.StartsWith(";")));

        private static bool IsSpacesWord(Word word) => 
            (word != null) && word.SpacesOnly;

        private bool ShouldReturnWord(int pos, out Word word)
        {
            word = this.lastWord;
            bool flag = (word != null) && !string.IsNullOrEmpty(word.Text);
            string text = this.text.Substring(this.lastPosition, pos - this.lastPosition);
            Word word2 = new Word(text, this.leadSpaceCount);
            if (IsSpacesWord(word) || (IsNonSplittableWord(text) && ((this.leadSpaceCount == 0) || (text.Length > 1))))
            {
                word2 = (this.lastWord == null) ? new Word(word2.TextWithSpaces, 0) : new Word(this.lastWord.Text + word2.TextWithSpaces, this.lastWord.LeadSpaceCount);
                flag = false;
            }
            this.lastWord = word2;
            this.lastPosition = pos;
            this.leadSpaceCount = 0;
            return flag;
        }

        private void IncrementLeadSpaceCount(int pos)
        {
            this.leadSpaceCount++;
            this.lastPosition = pos + 1;
        }

        [IteratorStateMachine(typeof(<GetWords>d__12))]
        public IEnumerable<Word> GetWords()
        {
            bool <needReturnWordOnSpace>5__1 = false;
            this.position = 0;
            goto TR_0022;
        TR_000A:
            this.position++;
            goto TR_0022;
        TR_000B:
            <needReturnWordOnSpace>5__1 = true;
            goto TR_000A;
        TR_000D:
            this.IncrementLeadSpaceCount(this.position);
            goto TR_000A;
        TR_0022:
            while (true)
            {
                Word word;
                if (this.position < ((this.text != null) ? this.text.Length : 0))
                {
                    CharKind kind = this.GetKind(this.position);
                    switch (kind)
                    {
                        case CharKind.WhiteSpace:
                            if (!<needReturnWordOnSpace>5__1)
                            {
                                goto TR_000D;
                            }
                            else if (this.ShouldReturnWord(this.position, out word))
                            {
                                yield return word;
                            }
                            <needReturnWordOnSpace>5__1 = false;
                            goto TR_000D;

                        case CharKind.OpenBrace:
                            if (!this.ShouldReturnWord(this.position, out word))
                            {
                                break;
                            }
                            yield return word;
                            goto TR_000B;

                        case CharKind.CloseBrace:
                        case CharKind.InseparableEndSymbol:
                            if (!this.ShouldReturnWord(this.position + 1, out word))
                            {
                                break;
                            }
                            yield return word;
                            goto TR_000B;

                        case CharKind.Separator:
                            if (this.ShouldReturnWord(this.position, out word))
                            {
                                yield return word;
                            }
                            if (this.ShouldReturnWord(this.position + 1, out word))
                            {
                                yield return word;
                            }
                            goto TR_000B;

                        case CharKind.BackSlash:
                            if ((this.position <= 0) || ((this.GetKind(this.position - 1) != CharKind.Digit) || !this.ShouldReturnWord(this.position, out word)))
                            {
                                break;
                            }
                            yield return word;
                            goto TR_000B;

                        case CharKind.NonBreakingSpace:
                        {
                            int pos = this.position + 1;
                            if ((this.text.Length <= pos) || (this.GetKind(pos) != CharKind.InseparableEndSymbol))
                            {
                                if (this.ShouldReturnWord(pos, out word))
                                {
                                    yield return word;
                                    goto TR_000B;
                                }
                            }
                            else
                            {
                                goto TR_000A;
                            }
                            break;
                        }
                        default:
                            break;
                    }
                }
                else
                {
                    if ((this.position > this.lastPosition) && this.ShouldReturnWord(this.position, out word))
                    {
                        yield return word;
                    }
                    while (true)
                    {
                        if ((this.leadSpaceCount > 0) && this.stringFormat.FormatFlags.HasFlag(StringFormatFlags.MeasureTrailingSpaces))
                        {
                            if (this.lastWord != null)
                            {
                                yield return new Word(this.lastWord.Text + new string(' ', this.leadSpaceCount), this.lastWord.LeadSpaceCount);
                            }
                            else
                            {
                                yield return new Word(string.Empty, this.leadSpaceCount);
                            }
                        }
                        else if (this.lastWord != null)
                        {
                            yield return this.lastWord;
                        }
                    }
                }
                break;
            }
            goto TR_000B;
        }
    }
}

