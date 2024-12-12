namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PdfWordPart
    {
        private readonly PdfOrientedRectangle rectangle;
        private readonly IList<PdfCharacter> characters;
        private string text;
        private bool wordEnded;

        internal PdfWordPart(PdfOrientedRectangle rectangle, IList<PdfCharacter> characters, bool wordEnded)
        {
            this.rectangle = rectangle;
            this.characters = characters;
            this.wordEnded = wordEnded;
        }

        private bool CharactersAreEqual(IList<PdfCharacter> anotherWordCharacters)
        {
            IList<PdfCharacter> characters = this.Characters;
            int count = characters.Count;
            if (count != anotherWordCharacters.Count)
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                if (!characters[i].UnicodeData.Equals(anotherWordCharacters[i].UnicodeData))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSuitable(int wordNumber, int offset) => 
            (this.WordNumber == wordNumber) && ((0 <= offset) && (offset <= this.characters.Count));

        internal bool Overlaps(PdfWordPart word) => 
            this.CharactersAreEqual(word.Characters) && PdfTextUtils.DoOverlap(this.Rectangle, word.Rectangle);

        public int WordNumber { get; set; }

        public int Length =>
            this.characters.Count;

        public PdfOrientedRectangle Rectangle =>
            this.rectangle;

        public bool WordEnded =>
            this.wordEnded;

        public int EndWordPosition =>
            this.wordEnded ? this.characters.Count : (this.characters.Count - 1);

        public string Text
        {
            get
            {
                if (this.text == null)
                {
                    PdfBidiStringBuilder builder = new PdfBidiStringBuilder();
                    foreach (PdfCharacter character in this.characters)
                    {
                        string unicodeData = character.UnicodeData;
                        builder.Append(unicodeData);
                    }
                    this.text = builder.EndCurrentLineAndGetString();
                }
                return this.text;
            }
        }

        public IList<PdfCharacter> Characters =>
            this.characters;

        public bool IsNotEmpty
        {
            get
            {
                bool flag;
                using (IEnumerator<PdfCharacter> enumerator = this.characters.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PdfCharacter current = enumerator.Current;
                            if (string.IsNullOrWhiteSpace(current.UnicodeData))
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
        }

        private bool WordHasNonDigitCharacters
        {
            get
            {
                int num = 0;
                while (num < (this.characters.Count - 1))
                {
                    string unicodeData = this.characters[num].UnicodeData;
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 >= unicodeData.Length)
                        {
                            num++;
                            break;
                        }
                        char c = unicodeData[num2];
                        if (!char.IsDigit(c))
                        {
                            return true;
                        }
                        num2++;
                    }
                }
                return false;
            }
        }

        public bool IsWrapped =>
            PdfTextUtils.IsWrapSymbol(this.characters.Last<PdfCharacter>().UnicodeData) && this.WordHasNonDigitCharacters;
    }
}

