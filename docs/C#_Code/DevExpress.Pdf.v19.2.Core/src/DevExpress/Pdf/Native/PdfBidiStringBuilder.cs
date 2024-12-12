namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfBidiStringBuilder
    {
        private const string unicodeReplacementCharacter = "�";
        private StringBuilder resultBuilder = new StringBuilder();
        private IList<PdfBidiDirectedSequenceCollection> currentLine = new List<PdfBidiDirectedSequenceCollection>();
        private PdfBidiDirectedSequenceCollection currentDirectedSequence = new PdfBidiDirectedSequenceCollection(PdfBidiTextDirection.Unknown);
        private PdfBidiSequence currentBidiSequence = new PdfBidiSequence(PdfBidiCharacterClass.LTR);
        private bool endsWithNewLine;
        private bool empty = true;

        public void Append(string unicodeChar)
        {
            unicodeChar = (unicodeChar == "\0") ? " " : unicodeChar;
            if (unicodeChar.Length != 0)
            {
                this.empty = false;
                this.endsWithNewLine = false;
                PdfBidiCharacterClass characterClass = PdfBidiCharacterClasses.GetCharacterClass(unicodeChar[0]);
                if (characterClass != this.currentBidiSequence.CharacterClass)
                {
                    this.FinishSequence(characterClass);
                }
                this.currentBidiSequence.AppendChar(unicodeChar);
            }
        }

        public void AppendLine()
        {
            this.resultBuilder.AppendLine(this.FinishLineAndGetSequenceString());
            this.endsWithNewLine = true;
        }

        public string EndCurrentLineAndGetString()
        {
            this.resultBuilder.Append(this.FinishLineAndGetSequenceString());
            string unicodeChar = this.resultBuilder.ToString();
            return (PdfTextUtils.HasRTLMark(unicodeChar) ? PdfTextUtils.NormalizeAndCompose(unicodeChar) : unicodeChar);
        }

        private string FinishLineAndGetSequenceString()
        {
            this.FinishSequence(PdfBidiCharacterClass.LTR);
            StringBuilder builder = new StringBuilder();
            foreach (PdfBidiDirectedSequenceCollection sequences in this.currentLine)
            {
                builder.Append(sequences.GetDirectedString());
            }
            if (this.currentDirectedSequence.Count > 0)
            {
                builder.Append(this.currentDirectedSequence.GetDirectedString());
            }
            this.currentDirectedSequence.Clear();
            this.currentLine.Clear();
            return builder.ToString().Trim();
        }

        private void FinishSequence(PdfBidiCharacterClass newSequenceCharacterClass)
        {
            if (this.currentBidiSequence.IsNotEmpty)
            {
                PdfBidiCharacterClass characterClass = this.currentBidiSequence.CharacterClass;
                if (characterClass == PdfBidiCharacterClass.LTR)
                {
                    if (this.currentDirectedSequence.Direction == PdfBidiTextDirection.Unknown)
                    {
                        this.currentDirectedSequence.Direction = PdfBidiTextDirection.LeftToRight;
                    }
                    else if (this.currentDirectedSequence.Direction != PdfBidiTextDirection.LeftToRight)
                    {
                        this.currentLine.Add(this.currentDirectedSequence);
                        this.currentDirectedSequence = new PdfBidiDirectedSequenceCollection(PdfBidiTextDirection.LeftToRight);
                    }
                    this.currentDirectedSequence.Add(this.currentBidiSequence);
                }
                else if (characterClass != PdfBidiCharacterClass.RTL)
                {
                    this.currentDirectedSequence.Add(this.currentBidiSequence);
                }
                else
                {
                    if (this.currentDirectedSequence.Direction == PdfBidiTextDirection.Unknown)
                    {
                        this.currentDirectedSequence.Direction = PdfBidiTextDirection.RightToLeft;
                    }
                    else if (this.currentDirectedSequence.Direction != PdfBidiTextDirection.RightToLeft)
                    {
                        this.currentLine.Add(this.currentDirectedSequence);
                        this.currentDirectedSequence = new PdfBidiDirectedSequenceCollection(PdfBidiTextDirection.RightToLeft);
                    }
                    this.currentDirectedSequence.Add(this.currentBidiSequence);
                }
            }
            this.currentBidiSequence = new PdfBidiSequence(newSequenceCharacterClass);
        }

        public bool Empty =>
            this.empty;

        public bool EndsWithNewLine =>
            this.endsWithNewLine;
    }
}

