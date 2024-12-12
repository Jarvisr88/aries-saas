namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_FONT_FEATURE_TAG
    {
        public static readonly DWRITE_FONT_FEATURE_TAG ALTERNATIVE_FRACTIONS;
        public static readonly DWRITE_FONT_FEATURE_TAG PETITE_CAPITALS_FROM_CAPITALS;
        public static readonly DWRITE_FONT_FEATURE_TAG SMALL_CAPITALS_FROM_CAPITALS;
        public static readonly DWRITE_FONT_FEATURE_TAG CONTEXTUAL_ALTERNATES;
        public static readonly DWRITE_FONT_FEATURE_TAG CASE_SENSITIVE_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG GLYPH_COMPOSITION_DECOMPOSITION;
        public static readonly DWRITE_FONT_FEATURE_TAG CONTEXTUAL_LIGATURES;
        public static readonly DWRITE_FONT_FEATURE_TAG CAPITAL_SPACING;
        public static readonly DWRITE_FONT_FEATURE_TAG CONTEXTUAL_SWASH;
        public static readonly DWRITE_FONT_FEATURE_TAG CURSIVE_POSITIONING;
        public static readonly DWRITE_FONT_FEATURE_TAG DEFAULT;
        public static readonly DWRITE_FONT_FEATURE_TAG DISCRETIONARY_LIGATURES;
        public static readonly DWRITE_FONT_FEATURE_TAG EXPERT_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG FRACTIONS;
        public static readonly DWRITE_FONT_FEATURE_TAG FULL_WIDTH;
        public static readonly DWRITE_FONT_FEATURE_TAG HALF_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG HALANT_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG ALTERNATE_HALF_WIDTH;
        public static readonly DWRITE_FONT_FEATURE_TAG HISTORICAL_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG HORIZONTAL_KANA_ALTERNATES;
        public static readonly DWRITE_FONT_FEATURE_TAG HISTORICAL_LIGATURES;
        public static readonly DWRITE_FONT_FEATURE_TAG HALF_WIDTH;
        public static readonly DWRITE_FONT_FEATURE_TAG HOJO_KANJI_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG JIS04_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG JIS78_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG JIS83_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG JIS90_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG KERNING;
        public static readonly DWRITE_FONT_FEATURE_TAG STANDARD_LIGATURES;
        public static readonly DWRITE_FONT_FEATURE_TAG LINING_FIGURES;
        public static readonly DWRITE_FONT_FEATURE_TAG LOCALIZED_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG MARK_POSITIONING;
        public static readonly DWRITE_FONT_FEATURE_TAG MATHEMATICAL_GREEK;
        public static readonly DWRITE_FONT_FEATURE_TAG MARK_TO_MARK_POSITIONING;
        public static readonly DWRITE_FONT_FEATURE_TAG ALTERNATE_ANNOTATION_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG NLC_KANJI_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG OLD_STYLE_FIGURES;
        public static readonly DWRITE_FONT_FEATURE_TAG ORDINALS;
        public static readonly DWRITE_FONT_FEATURE_TAG PROPORTIONAL_ALTERNATE_WIDTH;
        public static readonly DWRITE_FONT_FEATURE_TAG PETITE_CAPITALS;
        public static readonly DWRITE_FONT_FEATURE_TAG PROPORTIONAL_FIGURES;
        public static readonly DWRITE_FONT_FEATURE_TAG PROPORTIONAL_WIDTHS;
        public static readonly DWRITE_FONT_FEATURE_TAG QUARTER_WIDTHS;
        public static readonly DWRITE_FONT_FEATURE_TAG REQUIRED_LIGATURES;
        public static readonly DWRITE_FONT_FEATURE_TAG RUBY_NOTATION_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_ALTERNATES;
        public static readonly DWRITE_FONT_FEATURE_TAG SCIENTIFIC_INFERIORS;
        public static readonly DWRITE_FONT_FEATURE_TAG SMALL_CAPITALS;
        public static readonly DWRITE_FONT_FEATURE_TAG SIMPLIFIED_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_1;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_2;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_3;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_4;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_5;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_6;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_7;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_8;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_9;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_10;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_11;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_12;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_13;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_14;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_15;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_16;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_17;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_18;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_19;
        public static readonly DWRITE_FONT_FEATURE_TAG STYLISTIC_SET_20;
        public static readonly DWRITE_FONT_FEATURE_TAG SUBSCRIPT;
        public static readonly DWRITE_FONT_FEATURE_TAG SUPERSCRIPT;
        public static readonly DWRITE_FONT_FEATURE_TAG SWASH;
        public static readonly DWRITE_FONT_FEATURE_TAG TITLING;
        public static readonly DWRITE_FONT_FEATURE_TAG TRADITIONAL_NAME_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG TABULAR_FIGURES;
        public static readonly DWRITE_FONT_FEATURE_TAG TRADITIONAL_FORMS;
        public static readonly DWRITE_FONT_FEATURE_TAG THIRD_WIDTHS;
        public static readonly DWRITE_FONT_FEATURE_TAG UNICASE;
        public static readonly DWRITE_FONT_FEATURE_TAG VERTICAL_WRITING;
        public static readonly DWRITE_FONT_FEATURE_TAG VERTICAL_ALTERNATES_AND_ROTATION;
        public static readonly DWRITE_FONT_FEATURE_TAG SLASHED_ZERO;
        private readonly int tag;
        private static DWRITE_FONT_FEATURE_TAG MakeOpenTypeTag(char a, char b, char c, char d) => 
            new DWRITE_FONT_FEATURE_TAG((((d << 0x18) | (c << 0x10)) | (b << 8)) | a);

        private DWRITE_FONT_FEATURE_TAG(int tag)
        {
            this.tag = tag;
        }

        static DWRITE_FONT_FEATURE_TAG()
        {
            ALTERNATIVE_FRACTIONS = MakeOpenTypeTag('a', 'f', 'r', 'c');
            PETITE_CAPITALS_FROM_CAPITALS = MakeOpenTypeTag('c', '2', 'p', 'c');
            SMALL_CAPITALS_FROM_CAPITALS = MakeOpenTypeTag('c', '2', 's', 'c');
            CONTEXTUAL_ALTERNATES = MakeOpenTypeTag('c', 'a', 'l', 't');
            CASE_SENSITIVE_FORMS = MakeOpenTypeTag('c', 'a', 's', 'e');
            GLYPH_COMPOSITION_DECOMPOSITION = MakeOpenTypeTag('c', 'c', 'm', 'p');
            CONTEXTUAL_LIGATURES = MakeOpenTypeTag('c', 'l', 'i', 'g');
            CAPITAL_SPACING = MakeOpenTypeTag('c', 'p', 's', 'p');
            CONTEXTUAL_SWASH = MakeOpenTypeTag('c', 's', 'w', 'h');
            CURSIVE_POSITIONING = MakeOpenTypeTag('c', 'u', 'r', 's');
            DEFAULT = MakeOpenTypeTag('d', 'f', 'l', 't');
            DISCRETIONARY_LIGATURES = MakeOpenTypeTag('d', 'l', 'i', 'g');
            EXPERT_FORMS = MakeOpenTypeTag('e', 'x', 'p', 't');
            FRACTIONS = MakeOpenTypeTag('f', 'r', 'a', 'c');
            FULL_WIDTH = MakeOpenTypeTag('f', 'w', 'i', 'd');
            HALF_FORMS = MakeOpenTypeTag('h', 'a', 'l', 'f');
            HALANT_FORMS = MakeOpenTypeTag('h', 'a', 'l', 'n');
            ALTERNATE_HALF_WIDTH = MakeOpenTypeTag('h', 'a', 'l', 't');
            HISTORICAL_FORMS = MakeOpenTypeTag('h', 'i', 's', 't');
            HORIZONTAL_KANA_ALTERNATES = MakeOpenTypeTag('h', 'k', 'n', 'a');
            HISTORICAL_LIGATURES = MakeOpenTypeTag('h', 'l', 'i', 'g');
            HALF_WIDTH = MakeOpenTypeTag('h', 'w', 'i', 'd');
            HOJO_KANJI_FORMS = MakeOpenTypeTag('h', 'o', 'j', 'o');
            JIS04_FORMS = MakeOpenTypeTag('j', 'p', '0', '4');
            JIS78_FORMS = MakeOpenTypeTag('j', 'p', '7', '8');
            JIS83_FORMS = MakeOpenTypeTag('j', 'p', '8', '3');
            JIS90_FORMS = MakeOpenTypeTag('j', 'p', '9', '0');
            KERNING = MakeOpenTypeTag('k', 'e', 'r', 'n');
            STANDARD_LIGATURES = MakeOpenTypeTag('l', 'i', 'g', 'a');
            LINING_FIGURES = MakeOpenTypeTag('l', 'n', 'u', 'm');
            LOCALIZED_FORMS = MakeOpenTypeTag('l', 'o', 'c', 'l');
            MARK_POSITIONING = MakeOpenTypeTag('m', 'a', 'r', 'k');
            MATHEMATICAL_GREEK = MakeOpenTypeTag('m', 'g', 'r', 'k');
            MARK_TO_MARK_POSITIONING = MakeOpenTypeTag('m', 'k', 'm', 'k');
            ALTERNATE_ANNOTATION_FORMS = MakeOpenTypeTag('n', 'a', 'l', 't');
            NLC_KANJI_FORMS = MakeOpenTypeTag('n', 'l', 'c', 'k');
            OLD_STYLE_FIGURES = MakeOpenTypeTag('o', 'n', 'u', 'm');
            ORDINALS = MakeOpenTypeTag('o', 'r', 'd', 'n');
            PROPORTIONAL_ALTERNATE_WIDTH = MakeOpenTypeTag('p', 'a', 'l', 't');
            PETITE_CAPITALS = MakeOpenTypeTag('p', 'c', 'a', 'p');
            PROPORTIONAL_FIGURES = MakeOpenTypeTag('p', 'n', 'u', 'm');
            PROPORTIONAL_WIDTHS = MakeOpenTypeTag('p', 'w', 'i', 'd');
            QUARTER_WIDTHS = MakeOpenTypeTag('q', 'w', 'i', 'd');
            REQUIRED_LIGATURES = MakeOpenTypeTag('r', 'l', 'i', 'g');
            RUBY_NOTATION_FORMS = MakeOpenTypeTag('r', 'u', 'b', 'y');
            STYLISTIC_ALTERNATES = MakeOpenTypeTag('s', 'a', 'l', 't');
            SCIENTIFIC_INFERIORS = MakeOpenTypeTag('s', 'i', 'n', 'f');
            SMALL_CAPITALS = MakeOpenTypeTag('s', 'm', 'c', 'p');
            SIMPLIFIED_FORMS = MakeOpenTypeTag('s', 'm', 'p', 'l');
            STYLISTIC_SET_1 = MakeOpenTypeTag('s', 's', '0', '1');
            STYLISTIC_SET_2 = MakeOpenTypeTag('s', 's', '0', '2');
            STYLISTIC_SET_3 = MakeOpenTypeTag('s', 's', '0', '3');
            STYLISTIC_SET_4 = MakeOpenTypeTag('s', 's', '0', '4');
            STYLISTIC_SET_5 = MakeOpenTypeTag('s', 's', '0', '5');
            STYLISTIC_SET_6 = MakeOpenTypeTag('s', 's', '0', '6');
            STYLISTIC_SET_7 = MakeOpenTypeTag('s', 's', '0', '7');
            STYLISTIC_SET_8 = MakeOpenTypeTag('s', 's', '0', '8');
            STYLISTIC_SET_9 = MakeOpenTypeTag('s', 's', '0', '9');
            STYLISTIC_SET_10 = MakeOpenTypeTag('s', 's', '1', '0');
            STYLISTIC_SET_11 = MakeOpenTypeTag('s', 's', '1', '1');
            STYLISTIC_SET_12 = MakeOpenTypeTag('s', 's', '1', '2');
            STYLISTIC_SET_13 = MakeOpenTypeTag('s', 's', '1', '3');
            STYLISTIC_SET_14 = MakeOpenTypeTag('s', 's', '1', '4');
            STYLISTIC_SET_15 = MakeOpenTypeTag('s', 's', '1', '5');
            STYLISTIC_SET_16 = MakeOpenTypeTag('s', 's', '1', '6');
            STYLISTIC_SET_17 = MakeOpenTypeTag('s', 's', '1', '7');
            STYLISTIC_SET_18 = MakeOpenTypeTag('s', 's', '1', '8');
            STYLISTIC_SET_19 = MakeOpenTypeTag('s', 's', '1', '9');
            STYLISTIC_SET_20 = MakeOpenTypeTag('s', 's', '2', '0');
            SUBSCRIPT = MakeOpenTypeTag('s', 'u', 'b', 's');
            SUPERSCRIPT = MakeOpenTypeTag('s', 'u', 'p', 's');
            SWASH = MakeOpenTypeTag('s', 'w', 's', 'h');
            TITLING = MakeOpenTypeTag('t', 'i', 't', 'l');
            TRADITIONAL_NAME_FORMS = MakeOpenTypeTag('t', 'n', 'a', 'm');
            TABULAR_FIGURES = MakeOpenTypeTag('t', 'n', 'u', 'm');
            TRADITIONAL_FORMS = MakeOpenTypeTag('t', 'r', 'a', 'd');
            THIRD_WIDTHS = MakeOpenTypeTag('t', 'w', 'i', 'd');
            UNICASE = MakeOpenTypeTag('u', 'n', 'i', 'c');
            VERTICAL_WRITING = MakeOpenTypeTag('v', 'e', 'r', 't');
            VERTICAL_ALTERNATES_AND_ROTATION = MakeOpenTypeTag('v', 'r', 't', '2');
            SLASHED_ZERO = MakeOpenTypeTag('z', 'e', 'r', 'o');
        }
    }
}

