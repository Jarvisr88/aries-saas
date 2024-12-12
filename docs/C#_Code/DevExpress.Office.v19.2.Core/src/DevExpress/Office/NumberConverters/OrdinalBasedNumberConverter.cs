namespace DevExpress.Office.NumberConverters
{
    using System;
    using System.Collections.Generic;

    public abstract class OrdinalBasedNumberConverter
    {
        protected OrdinalBasedNumberConverter()
        {
        }

        public string ConvertNumber(long value) => 
            this.ConvertNumberCore(Math.Max(value, this.MinValue) % this.MaxValue);

        public abstract string ConvertNumberCore(long value);
        public static OrdinalBasedNumberConverter CreateConverter(NumberingFormat format, LanguageId id)
        {
            if (format > NumberingFormat.Hex)
            {
                switch (format)
                {
                    case NumberingFormat.JapaneseCounting:
                        return new JapaneseCountingConverter();

                    case NumberingFormat.JapaneseDigitalTenThousand:
                    case NumberingFormat.JapaneseLegal:
                    case NumberingFormat.KoreanCounting:
                    case NumberingFormat.KoreanDigital:
                    case NumberingFormat.KoreanDigital2:
                    case NumberingFormat.KoreanLegal:
                    case NumberingFormat.None:
                        break;

                    case NumberingFormat.LowerLetter:
                        return new LowerLatinLetterNumberConverter();

                    case NumberingFormat.LowerRoman:
                        return new LowerRomanNumberConverterClassic();

                    case NumberingFormat.NumberInDash:
                        return new NumberInDashNumberConverter();

                    case NumberingFormat.Ordinal:
                        return GetOrdinalNumberConverterByLanguage(id);

                    case NumberingFormat.OrdinalText:
                        return GetDescriptiveOrdinalNumberConverterByLanguage(id);

                    case NumberingFormat.RussianLower:
                        return new RussianLowerNumberConverter();

                    case NumberingFormat.RussianUpper:
                        return new RussianUpperNumberConverter();

                    default:
                        if (format == NumberingFormat.UpperLetter)
                        {
                            return new UpperLatinLetterNumberConverter();
                        }
                        if (format != NumberingFormat.UpperRoman)
                        {
                            break;
                        }
                        return new UpperRomanNumberConverterClassic();
                }
            }
            else
            {
                switch (format)
                {
                    case NumberingFormat.Decimal:
                        return new DecimalNumberConverter();

                    case NumberingFormat.AIUEOHiragana:
                    case NumberingFormat.AIUEOFullWidthHiragana:
                        break;

                    case NumberingFormat.ArabicAbjad:
                        return new ArabicAbjadConverter();

                    case NumberingFormat.ArabicAlpha:
                        return new ArabicAlphaConverter();

                    case NumberingFormat.Bullet:
                        return new BulletNumberConverter();

                    case NumberingFormat.CardinalText:
                        return GetDescriptiveCardinalNumberConverterByLanguage(id);

                    case NumberingFormat.Chicago:
                        return new ChicagoNumberConverter();

                    default:
                        switch (format)
                        {
                            case NumberingFormat.DecimalEnclosedParentheses:
                                return new DecimalEnclosedParenthesesNumberConverter();

                            case NumberingFormat.DecimalZero:
                                return new DecimalZeroNumberConverter();

                            case NumberingFormat.Hebrew1:
                                return new Hebrew1Converter();

                            case NumberingFormat.Hebrew2:
                                return new Hebrew2Converter();

                            case NumberingFormat.Hex:
                                return new HexNumberConverter();

                            default:
                                break;
                        }
                        break;
                }
            }
            return new DecimalNumberConverter();
        }

        private static OrdinalBasedNumberConverter GetDescriptiveCardinalNumberConverterByLanguage(LanguageId id) => 
            !(id == LanguageId.English) ? (!(id == LanguageId.French) ? (!(id == LanguageId.German) ? (!(id == LanguageId.Italian) ? (!(id == LanguageId.Russian) ? (!(id == LanguageId.Swedish) ? (!(id == LanguageId.Turkish) ? (!(id == LanguageId.Greek) ? (!(id == LanguageId.Spanish) ? (!(id == LanguageId.Portuguese) ? (!(id == LanguageId.Ukrainian) ? (!(id == LanguageId.Hindi) ? ((OrdinalBasedNumberConverter) new DecimalNumberConverter()) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalHindiNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalUkrainianNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalPortugueseNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalSpanishNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalGreekNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalTurkishNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalSwedishNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalRussianNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalItalianNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalGermanNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalFrenchNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveCardinalEnglishNumberConverter());

        private static OrdinalBasedNumberConverter GetDescriptiveOrdinalNumberConverterByLanguage(LanguageId id) => 
            !(id == LanguageId.English) ? (!(id == LanguageId.French) ? (!(id == LanguageId.German) ? (!(id == LanguageId.Italian) ? (!(id == LanguageId.Russian) ? (!(id == LanguageId.Swedish) ? (!(id == LanguageId.Turkish) ? (!(id == LanguageId.Greek) ? (!(id == LanguageId.Spanish) ? (!(id == LanguageId.Portuguese) ? (!(id == LanguageId.Ukrainian) ? (!(id == LanguageId.Hindi) ? ((OrdinalBasedNumberConverter) new DecimalNumberConverter()) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalHindiNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalUkrainianNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalPortugueseNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalSpanishNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalGreekNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalTurkishNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalSwedishNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalRussianNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalItalianNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalGermanNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalFrenchNumberConverter())) : ((OrdinalBasedNumberConverter) new DescriptiveOrdinalEnglishNumberConverter());

        public static List<NumberingFormat> GetExtendedNumberingFormatList()
        {
            List<NumberingFormat> supportNumberingFormat = GetSupportNumberingFormat();
            supportNumberingFormat.Add(NumberingFormat.Hebrew1);
            supportNumberingFormat.Add(NumberingFormat.Hebrew2);
            supportNumberingFormat.Add(NumberingFormat.ArabicAlpha);
            supportNumberingFormat.Add(NumberingFormat.ArabicAbjad);
            return supportNumberingFormat;
        }

        private static OrdinalBasedNumberConverter GetOrdinalNumberConverterByLanguage(LanguageId id) => 
            !(id == LanguageId.English) ? (!(id == LanguageId.French) ? (!(id == LanguageId.German) ? (!(id == LanguageId.Italian) ? (!(id == LanguageId.Russian) ? (!(id == LanguageId.Swedish) ? (!(id == LanguageId.Turkish) ? (!(id == LanguageId.Greek) ? (!(id == LanguageId.Spanish) ? (!(id == LanguageId.Portuguese) ? (!(id == LanguageId.Ukrainian) ? ((OrdinalBasedNumberConverter) new DecimalNumberConverter()) : ((OrdinalBasedNumberConverter) new OrdinalUkrainianNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalPortugueseNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalSpanishNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalGreekNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalTurkishNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalSwedishNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalRussianNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalItalianNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalGermanNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalFrenchNumberConverter())) : ((OrdinalBasedNumberConverter) new OrdinalEnglishNumberConverter());

        public static List<NumberingFormat> GetSupportNumberingFormat() => 
            new List<NumberingFormat> { 
                NumberingFormat.Decimal,
                NumberingFormat.UpperRoman,
                NumberingFormat.LowerRoman,
                NumberingFormat.UpperLetter,
                NumberingFormat.LowerLetter,
                NumberingFormat.Ordinal,
                NumberingFormat.CardinalText,
                NumberingFormat.OrdinalText,
                NumberingFormat.DecimalZero,
                NumberingFormat.RussianUpper,
                NumberingFormat.RussianLower,
                NumberingFormat.DecimalEnclosedParentheses,
                NumberingFormat.NumberInDash,
                NumberingFormat.Chicago
            };

        protected internal abstract NumberingFormat Type { get; }

        protected internal virtual long MinValue =>
            -9223372036854775808L;

        protected internal virtual long MaxValue =>
            0x7fffffffffffffffL;
    }
}

