namespace ActiproSoftware.WinUICore.Rendering
{
    using #H;
    using System;
    using System.Runtime.CompilerServices;

    public class StringTextProvider : ITextProvider
    {
        private string #Hd;

        public StringTextProvider(string text) : this(text, 0)
        {
        }

        public StringTextProvider(string text, int startCharacterIndex)
        {
            if (text == null)
            {
                throw new ArgumentNullException(#G.#eg(0xb77));
            }
            this.#Hd = text;
            this.Length = text.Length;
            this.StartCharacterIndex = startCharacterIndex;
        }

        public string GetSubstring(int characterIndex, int characterCount) => 
            ((characterIndex != 0) || (characterCount != this.Length)) ? this.#Hd.Substring(characterIndex, characterCount) : this.#Hd;

        public int Translate(int characterIndex, TextProviderTranslateModes modes)
        {
            switch (modes)
            {
                case TextProviderTranslateModes.FromSourceText:
                case TextProviderTranslateModes.PositiveTracking:
                    characterIndex -= this.StartCharacterIndex;
                    if ((characterIndex >= 0) && (characterIndex < this.Length))
                    {
                        break;
                    }
                    throw new ArgumentOutOfRangeException(#G.#eg(0xb80));

                case TextProviderTranslateModes.ToSourceText:
                case (TextProviderTranslateModes.PositiveTracking | TextProviderTranslateModes.ToSourceText):
                    if ((characterIndex < 0) || (characterIndex >= this.Length))
                    {
                        throw new ArgumentOutOfRangeException(#G.#eg(0xb80));
                    }
                    characterIndex += this.StartCharacterIndex;
                    break;

                default:
                    break;
            }
            return characterIndex;
        }

        public int Length { get; private set; }

        public int StartCharacterIndex { get; private set; }
    }
}

