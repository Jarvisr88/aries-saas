namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfBidiSequence
    {
        private readonly PdfBidiCharacterClass characterClass;
        private readonly StringBuilder characters;
        public PdfBidiCharacterClass CharacterClass =>
            this.characterClass;
        public bool IsNotEmpty =>
            this.characters.Length != 0;
        public PdfBidiSequence(PdfBidiCharacterClass characterClass)
        {
            this.characterClass = characterClass;
            this.characters = new StringBuilder();
        }

        public void AppendChar(string unicodeChar)
        {
            if (!string.IsNullOrEmpty(unicodeChar))
            {
                if (this.characterClass == PdfBidiCharacterClass.RTL)
                {
                    for (int i = unicodeChar.Length - 1; i >= 0; i--)
                    {
                        this.characters.Append(unicodeChar[i]);
                    }
                }
                else
                {
                    this.characters.Append(unicodeChar);
                }
            }
        }

        public void AppendTo(StringBuilder builder)
        {
            builder.Append(this.characters.ToString());
        }

        public void AppendMirroredTo(StringBuilder builder)
        {
            for (int i = this.characters.Length - 1; i >= 0; i--)
            {
                builder.Append(PdfBidiBrackets.TryGetMirroredBracket(this.characters[i]));
            }
        }
    }
}

