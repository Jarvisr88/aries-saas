namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfFontOS2TableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const int BoldWeight = 700;
        internal const string EntryTag = "OS/2";
        private const short normalFontWeight = 400;
        private const short boldFontWeight = 700;
        private Version version;
        private short avgCharWidth;
        private short weightClass;
        private WidthClass widthClass;
        private EmbeddingType embeddingType;
        private short subscriptXSize;
        private short subscriptYSize;
        private short subscriptXOffset;
        private short subscriptYOffset;
        private short superscriptXSize;
        private short superscriptYSize;
        private short superscriptXOffset;
        private short superscriptYOffset;
        private short strikeoutSize;
        private short strikeoutPosition;
        private PdfFontFamilyClass familyClass;
        private PdfPanose panose;
        private UnicodeRange1 unicodeRange1;
        private UnicodeRange2 unicodeRange2;
        private UnicodeRange3 unicodeRange3;
        private UnicodeRange4 unicodeRange4;
        private string vendor;
        private Selection selection;
        private int firstCharIndex;
        private int lastCharIndex;
        private short typoAscender;
        private short typoDescender;
        private short typoLineGap;
        private short winAscent;
        private short winDescent;
        private CodePageRange1 codePageRange1;
        private CodePageRange2 codePageRange2;
        private short xHeight;
        private short capHeight;
        private short defaultChar;
        private short breakChar;
        private short maxContext;
        private bool shouldWrite;

        public PdfFontOS2TableDirectoryEntry(byte[] tableData) : base("OS/2", tableData)
        {
            this.breakChar = 0x20;
            PdfBinaryStream tableStream = base.TableStream;
            this.version = (Version) tableStream.ReadShort();
            this.avgCharWidth = tableStream.ReadShort();
            this.weightClass = tableStream.ReadShort();
            this.widthClass = (WidthClass) tableStream.ReadShort();
            this.embeddingType = (EmbeddingType) tableStream.ReadShort();
            this.subscriptXSize = tableStream.ReadShort();
            this.subscriptYSize = tableStream.ReadShort();
            this.subscriptXOffset = tableStream.ReadShort();
            this.subscriptYOffset = tableStream.ReadShort();
            this.superscriptXSize = tableStream.ReadShort();
            this.superscriptYSize = tableStream.ReadShort();
            this.superscriptXOffset = tableStream.ReadShort();
            this.superscriptYOffset = tableStream.ReadShort();
            this.strikeoutSize = tableStream.ReadShort();
            this.strikeoutPosition = tableStream.ReadShort();
            this.familyClass = (PdfFontFamilyClass) tableStream.ReadShort();
            this.panose = new PdfPanose(tableStream);
            this.unicodeRange1 = (UnicodeRange1) tableStream.ReadInt();
            this.unicodeRange2 = (UnicodeRange2) tableStream.ReadInt();
            this.unicodeRange3 = (UnicodeRange3) tableStream.ReadInt();
            this.unicodeRange4 = (UnicodeRange4) tableStream.ReadInt();
            this.vendor = tableStream.ReadString(4);
            this.selection = (Selection) tableStream.ReadShort();
            this.firstCharIndex = tableStream.ReadUshort();
            this.lastCharIndex = tableStream.ReadUshort();
            this.typoAscender = tableStream.ReadShort();
            this.typoDescender = tableStream.ReadShort();
            this.typoLineGap = tableStream.ReadShort();
            this.winAscent = tableStream.ReadShort();
            this.winDescent = tableStream.ReadShort();
            if (this.version > Version.TrueType_1_5)
            {
                this.codePageRange1 = (CodePageRange1) tableStream.ReadInt();
                this.codePageRange2 = (CodePageRange2) tableStream.ReadInt();
            }
            else
            {
                this.codePageRange1 = CodePageRange1.Empty;
                this.codePageRange2 = CodePageRange2.Empty;
            }
        }

        public PdfFontOS2TableDirectoryEntry(PdfFont font, IDictionary<short, short> charset, short ascent, short descent) : base("OS/2")
        {
            PdfFontFlags none;
            bool flag;
            double num3;
            double num4;
            double num5;
            this.breakChar = 0x20;
            this.version = Version.OpenType_1_5;
            double num = 0.0;
            int num2 = 0;
            foreach (double num9 in font.GlyphWidths)
            {
                if (num9 != 0.0)
                {
                    num += num9;
                    num2++;
                }
            }
            this.avgCharWidth = (num2 == 0) ? ((short) 0) : Convert.ToInt16(Math.Ceiling((double) (num / ((double) num2))));
            PdfFontDescriptor fontDescriptor = font.FontDescriptor;
            if (fontDescriptor == null)
            {
                none = PdfFontFlags.None;
                flag = false;
                this.weightClass = 400;
                this.widthClass = WidthClass.Medium;
                num3 = 0.0;
                num4 = 0.0;
                num5 = 0.0;
                this.xHeight = 0;
                this.capHeight = 0;
            }
            else
            {
                none = fontDescriptor.Flags;
                flag = (none & PdfFontFlags.ForceBold) == PdfFontFlags.ForceBold;
                if (flag)
                {
                    this.weightClass = 700;
                }
                else
                {
                    this.weightClass = (short) fontDescriptor.FontWeight;
                    flag = this.weightClass == 700;
                }
                switch (fontDescriptor.FontStretch)
                {
                    case PdfFontStretch.UltraCondensed:
                        this.widthClass = WidthClass.UltraCondensed;
                        break;

                    case PdfFontStretch.ExtraCondensed:
                        this.widthClass = WidthClass.ExtraCondensed;
                        break;

                    case PdfFontStretch.Condensed:
                        this.widthClass = WidthClass.Condensed;
                        break;

                    case PdfFontStretch.SemiCondensed:
                        this.widthClass = WidthClass.SemiCondensed;
                        break;

                    case PdfFontStretch.SemiExpanded:
                        this.widthClass = WidthClass.SemiExpanded;
                        break;

                    case PdfFontStretch.Expanded:
                        this.widthClass = WidthClass.Expanded;
                        break;

                    case PdfFontStretch.ExtraExpanded:
                        this.widthClass = WidthClass.ExtraExpanded;
                        break;

                    case PdfFontStretch.UltraExpanded:
                        this.widthClass = WidthClass.UltraExpanded;
                        break;

                    default:
                        this.widthClass = WidthClass.Medium;
                        break;
                }
                num3 = ascent;
                num4 = descent;
                num5 = Math.Abs(fontDescriptor.ItalicAngle);
                this.xHeight = (short) fontDescriptor.XHeight;
                this.capHeight = (short) fontDescriptor.CapHeight;
            }
            this.embeddingType = EmbeddingType.PreviewPrintEmbedding;
            double num6 = num3 - num4;
            this.subscriptXSize = (short) (num6 / 5.0);
            this.subscriptYSize = this.subscriptXSize;
            this.subscriptXOffset = Convert.ToInt16(Math.Min((double) 32767.0, (double) (num6 * Math.Sin(num5 * 0.017453292519943295))));
            this.subscriptYOffset = 0;
            this.superscriptXSize = this.subscriptXSize;
            this.superscriptYSize = this.subscriptYSize;
            this.superscriptXOffset = this.subscriptXOffset;
            this.superscriptYOffset = (short) ((num3 * 4.0) / 5.0);
            this.strikeoutSize = (short) (num6 / 10.0);
            this.strikeoutPosition = (short) (num3 / 2.0);
            this.familyClass = PdfFontFamilyClass.NoClassification;
            this.panose = new PdfPanose();
            this.unicodeRange1 = UnicodeRange1.Empty;
            this.unicodeRange2 = UnicodeRange2.Empty;
            this.unicodeRange3 = UnicodeRange3.Empty;
            this.unicodeRange4 = UnicodeRange4.Empty;
            this.vendor = "DX  ";
            this.selection = Selection.Empty;
            if ((none & PdfFontFlags.Italic) == PdfFontFlags.Italic)
            {
                this.selection |= Selection.ITALIC;
            }
            if (flag)
            {
                this.selection |= Selection.BOLD;
            }
            ushort num7 = 0xffff;
            ushort num8 = 0;
            foreach (ushort num10 in charset.Keys)
            {
                num7 = Math.Min(num10, num7);
                num8 = Math.Max(num10, num8);
            }
            this.firstCharIndex = num7;
            this.lastCharIndex = num8;
            this.typoAscender = Convert.ToInt16(Math.Min(32767.0, num3));
            this.typoDescender = Convert.ToInt16(Math.Max(-32768.0, num4));
            this.typoLineGap = Convert.ToInt16(Math.Min((double) 32767.0, (double) (1.2 * num6)));
            this.winAscent = this.typoAscender;
            this.winDescent = Convert.ToInt16(Math.Min(32767.0, Math.Abs(num4)));
            this.codePageRange1 = CodePageRange1.Empty | CodePageRange1.Latin1;
            this.codePageRange2 = CodePageRange2.Empty;
            this.shouldWrite = true;
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                PdfBinaryStream stream = base.CreateNewStream();
                stream.WriteShort((short) this.version);
                stream.WriteShort(this.avgCharWidth);
                stream.WriteShort(this.weightClass);
                stream.WriteShort((short) this.widthClass);
                stream.WriteShort((short) this.embeddingType);
                stream.WriteShort(this.subscriptXSize);
                stream.WriteShort(this.subscriptYSize);
                stream.WriteShort(this.subscriptXOffset);
                stream.WriteShort(this.subscriptYOffset);
                stream.WriteShort(this.superscriptXSize);
                stream.WriteShort(this.superscriptYSize);
                stream.WriteShort(this.superscriptXOffset);
                stream.WriteShort(this.superscriptYOffset);
                stream.WriteShort(this.strikeoutSize);
                stream.WriteShort(this.strikeoutPosition);
                stream.WriteShort((short) this.familyClass);
                this.panose.Write(stream);
                stream.WriteInt((int) this.unicodeRange1);
                stream.WriteInt((int) this.unicodeRange2);
                stream.WriteInt((int) this.unicodeRange3);
                stream.WriteInt((int) this.unicodeRange4);
                stream.WriteString(this.vendor);
                stream.WriteShort((short) this.selection);
                stream.WriteShort((short) this.firstCharIndex);
                stream.WriteShort((short) this.lastCharIndex);
                stream.WriteShort(this.typoAscender);
                stream.WriteShort(this.typoDescender);
                stream.WriteShort(this.typoLineGap);
                stream.WriteShort(this.winAscent);
                stream.WriteShort(this.winDescent);
                if (this.version >= Version.TrueType_1_66)
                {
                    stream.WriteInt((int) this.codePageRange1);
                    stream.WriteInt((int) this.codePageRange2);
                    if (this.version >= Version.OpenType_1_2)
                    {
                        stream.WriteShort(this.xHeight);
                        stream.WriteShort(this.capHeight);
                        stream.WriteShort(this.defaultChar);
                        stream.WriteShort(this.breakChar);
                        stream.WriteShort(this.maxContext);
                    }
                }
            }
        }

        public void Validate(PdfFont font, PdfFontHeadTableDirectoryEntry head, PdfFontHheaTableDirectoryEntry hhea)
        {
            if (hhea != null)
            {
                short ascender = hhea.Ascender;
                if (ascender > this.winAscent)
                {
                    this.winAscent = ascender;
                    this.shouldWrite = true;
                }
                short num2 = -hhea.Descender;
                if (num2 > this.winDescent)
                {
                    this.winDescent = num2;
                    this.shouldWrite = true;
                }
            }
            PdfFontDescriptor fontDescriptor = font.FontDescriptor;
            if ((head != null) && (fontDescriptor != null))
            {
                PdfRectangle fontBBox = fontDescriptor.FontBBox;
                if (fontBBox != null)
                {
                    double top = fontBBox.Top;
                    double bottom = fontBBox.Bottom;
                    double num5 = ((double) head.UnitsPerEm) / (top - bottom);
                    short num6 = (short) (num5 * top);
                    if (num6 > this.winAscent)
                    {
                        this.winAscent = num6;
                        this.shouldWrite = true;
                    }
                    short num7 = (short) (-num5 * bottom);
                    if (num7 > this.winDescent)
                    {
                        this.winDescent = num7;
                        this.shouldWrite = true;
                    }
                }
            }
        }

        public PdfPanose Panose =>
            this.panose;

        public short TypoLineGap =>
            this.typoLineGap;

        public short TypoAscender =>
            this.typoAscender;

        public short TypoDescender =>
            this.typoDescender;

        public short WinAscent =>
            this.winAscent;

        public short WinDescent =>
            this.winDescent;

        public bool UseTypoMetrics =>
            (this.selection & Selection.USE_TYPO_METRICS) != Selection.Empty;

        public bool IsSymbolic =>
            this.codePageRange1.HasFlag((CodePageRange1) (-2147483648));

        public bool IsBold =>
            (this.selection & Selection.BOLD) != Selection.Empty;

        public bool IsItalic =>
            (this.selection & Selection.ITALIC) != Selection.Empty;

        public short WeightClass =>
            this.weightClass;

        [Flags]
        private enum CodePageRange1 : uint
        {
            Empty = 0,
            Latin1 = 1,
            Latin2EasternEurope = 2,
            Cyrillic = 4,
            Greek = 8,
            Turkish = 0x10,
            Hebrew = 0x20,
            Arabic = 0x40,
            WindowsBaltic = 0x80,
            Vietnamese = 0x100,
            Thai = 0x10000,
            JISJapan = 0x20000,
            ChineseSimplified = 0x40000,
            KoreanWansung = 0x80000,
            ChineseTraditional = 0x100000,
            KoreanJohab = 0x200000,
            MacintoshCharacterSet = 0x20000000,
            OEMCharacterSet = 0x40000000,
            SymbolCharacterSet = 0x80000000
        }

        [Flags]
        private enum CodePageRange2 : uint
        {
            Empty = 0,
            IBMGreek = 0x10000,
            MSDOSRussian = 0x20000,
            MSDOSNordic = 0x40000,
            Arabic = 0x80000,
            MSDOSCanadianFrench = 0x100000,
            Hebrew = 0x200000,
            MSDOSIcelandic = 0x400000,
            MSDOSPortuguese = 0x800000,
            IBMTurkish = 0x1000000,
            IBMCyrillic = 0x2000000,
            Latin2 = 0x4000000,
            MSDOSBaltic = 0x8000000,
            GreekFormer437G = 0x10000000,
            ArabicASMO708 = 0x20000000,
            WELatin1 = 0x40000000,
            US = 0x80000000
        }

        [Flags]
        private enum EmbeddingType
        {
            InstallableEmbedding = 0,
            RestrictedLicense = 2,
            PreviewPrintEmbedding = 4,
            EditableEmbedding = 8,
            NoSubsetting = 0x100,
            BitmapEmbeddingOnly = 0x200
        }

        [Flags]
        private enum Selection
        {
            Empty = 0,
            ITALIC = 1,
            UNDERSCORE = 2,
            NEGATIVE = 4,
            OUTLINED = 8,
            STRIKEOUT = 0x10,
            BOLD = 0x20,
            REGULAR = 0x40,
            USE_TYPO_METRICS = 0x80,
            WWS = 0x100,
            OBLIQUE = 0x200
        }

        [Flags]
        private enum UnicodeRange1 : uint
        {
            Empty = 0,
            BasicLatin = 1,
            Latin1Supplement = 2,
            LatinExtendedA = 4,
            LatinExtendedB = 8,
            IPAExtensions = 0x10,
            SpacingModifiersLetters = 0x20,
            CombiningDiacriticalMarks = 0x40,
            GreekAndCoptic = 0x80,
            Coptic = 0x100,
            Cyrillic = 0x200,
            Armenian = 0x400,
            Hebrew = 0x800,
            Vai = 0x1000,
            Arabic = 0x2000,
            NKo = 0x4000,
            Devanagari = 0x8000,
            Bengali = 0x10000,
            Gurmukhi = 0x20000,
            Gujarati = 0x40000,
            Oriya = 0x80000,
            Tamil = 0x100000,
            Telugu = 0x200000,
            Kannada = 0x400000,
            Malayalam = 0x800000,
            Thai = 0x1000000,
            Lao = 0x2000000,
            Georgian = 0x4000000,
            Balinese = 0x8000000,
            HangulJamo = 0x10000000,
            LatinExtendedAdditional = 0x20000000,
            GreekExtended = 0x40000000,
            GeneralPunctuation = 0x80000000
        }

        [Flags]
        private enum UnicodeRange2 : uint
        {
            Empty = 0,
            SuperscriptsAndSubscripts = 1,
            CurrencySymbols = 2,
            CombiningDiacriticalMarksForSymbols = 4,
            LetterlikeSymbols = 8,
            NumberForms = 0x10,
            Arrows = 0x20,
            MathematicalOperators = 0x40,
            MiscellaneousTechnical = 0x80,
            ControlPictures = 0x100,
            OpticalCharacterRecognition = 0x200,
            EnclosedAlphanumerics = 0x400,
            BoxDrawing = 0x800,
            BlockElements = 0x1000,
            GeometricShapes = 0x2000,
            MiscellaneousSymbols = 0x4000,
            Dingbats = 0x8000,
            CJKSymbolsAndPunctuation = 0x10000,
            Hiragana = 0x20000,
            Katakana = 0x40000,
            Bopomofo = 0x80000,
            HangulCompatibilityJamo = 0x100000,
            PhagsPa = 0x200000,
            EnclosedCJKLettersAndMonths = 0x400000,
            CJKCompatibility = 0x800000,
            HangulSyllables = 0x1000000,
            NonPlane0 = 0x2000000,
            Phoenician = 0x4000000,
            CJKUnifiedIdeographs = 0x8000000,
            PrivateUseAreaPlane0 = 0x10000000,
            CJKStrokes = 0x20000000,
            AlphabeticPresentationForms = 0x40000000,
            ArabicPresentationFormsA = 0x80000000
        }

        [Flags]
        private enum UnicodeRange3 : uint
        {
            Empty = 0,
            CombiningHalfMarks = 1,
            VerticalForms = 2,
            SmallFormsVariants = 4,
            ArabicPresentationFormsB = 8,
            HalfwidthAndFullwidthForms = 0x10,
            Specials = 0x20,
            Tibetan = 0x40,
            Syriac = 0x80,
            Thaana = 0x100,
            Sinhala = 0x200,
            Myanmar = 0x400,
            Ethiopic = 0x800,
            Cherokee = 0x1000,
            UnifiedCanadianAboriginalSyllabics = 0x2000,
            Ogham = 0x4000,
            Runic = 0x8000,
            Khmer = 0x10000,
            Mongolian = 0x20000,
            BraillePatterns = 0x40000,
            YiSyllables = 0x80000,
            Tagalog = 0x100000,
            OldItalic = 0x200000,
            Gothic = 0x400000,
            Deseret = 0x800000,
            MusicalSymbols = 0x1000000,
            MathematicalAlphanumericSymbols = 0x2000000,
            PrivateUsePlane15_16 = 0x4000000,
            VariationSelectors = 0x8000000,
            Tags = 0x10000000,
            Limbu = 0x20000000,
            TaiLe = 0x40000000,
            NewTaiLe = 0x80000000
        }

        [Flags]
        private enum UnicodeRange4 : uint
        {
            Empty = 0,
            Buginese = 1,
            Glagolitic = 2,
            Tifinagh = 4,
            YijingHexagramSymbols = 8,
            SylotiNagri = 0x10,
            LinearBSyllabary = 0x20,
            AncientGreekNumbers = 0x40,
            Ugaritic = 0x80,
            OldPersian = 0x100,
            Shavian = 0x200,
            Osmanya = 0x400,
            CypriotSyllabary = 0x800,
            Kharoshthi = 0x1000,
            TaiXuanJingSymbols = 0x2000,
            Cuneiform = 0x4000,
            CountingRodNumerals = 0x8000,
            Sundanese = 0x10000,
            Lepcha = 0x20000,
            OlChiki = 0x40000,
            Saurashtra = 0x80000,
            KayahLi = 0x100000,
            Rejang = 0x200000,
            Cham = 0x400000,
            AncientSymbols = 0x800000,
            PhaistosDisc = 0x1000000,
            Carian = 0x2000000,
            DominoTiles = 0x4000000
        }

        private enum Version
        {
            TrueType_1_5,
            TrueType_1_66,
            OpenType_1_2,
            OpenType_1_4,
            OpenType_1_5
        }

        private enum WidthClass
        {
            UltraCondensed = 1,
            ExtraCondensed = 2,
            Condensed = 3,
            SemiCondensed = 4,
            Medium = 5,
            SemiExpanded = 6,
            Expanded = 7,
            ExtraExpanded = 8,
            UltraExpanded = 9
        }
    }
}

