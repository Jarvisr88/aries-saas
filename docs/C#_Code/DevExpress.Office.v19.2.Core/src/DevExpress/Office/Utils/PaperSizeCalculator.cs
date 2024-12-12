namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;

    public static class PaperSizeCalculator
    {
        private static readonly Dictionary<PaperKind, Size> paperSizeTable = CreatePaperSizeTable();

        public static PaperKind CalculatePaperKind(Size size, PaperKind defaultValue) => 
            CalculatePaperKind(size, defaultValue, 0, defaultValue);

        public static PaperKind CalculatePaperKind(Size size, PaperKind defaultValue, int tolerance, PaperKind badSizeDefaultValue)
        {
            PaperKind key;
            if ((size.Width == 0) || (size.Height == 0))
            {
                return badSizeDefaultValue;
            }
            using (Dictionary<PaperKind, Size>.Enumerator enumerator = paperSizeTable.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<PaperKind, Size> current = enumerator.Current;
                        Size size2 = current.Value;
                        if ((Math.Abs((int) (size.Width - size2.Width)) > tolerance) || (Math.Abs((int) (size.Height - size2.Height)) > tolerance))
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return defaultValue;
                    }
                    break;
                }
            }
            return key;
        }

        public static Size CalculatePaperSize(PaperKind paperKind)
        {
            Size size;
            if (!paperSizeTable.TryGetValue(paperKind, out size))
            {
                size = new Size(0x2fd0, 0x3de0);
            }
            return size;
        }

        private static Dictionary<PaperKind, Size> CreatePaperSizeTable() => 
            new Dictionary<PaperKind, Size> { 
                { 
                    PaperKind.Letter,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.LetterSmall,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.Tabloid,
                    new Size(0x3de0, 0x5fa0)
                },
                { 
                    PaperKind.Ledger,
                    new Size(0x5fa0, 0x3de0)
                },
                { 
                    PaperKind.Legal,
                    new Size(0x2fd0, 0x4ec0)
                },
                { 
                    PaperKind.Statement,
                    new Size(0x1ef0, 0x2fd0)
                },
                { 
                    PaperKind.Executive,
                    new Size(0x28c8, 0x3b10)
                },
                { 
                    PaperKind.A3,
                    new Size(0x41c7, 0x5d06)
                },
                { 
                    PaperKind.A4,
                    new Size(0x2e83, 0x41c7)
                },
                { 
                    PaperKind.A4Small,
                    new Size(0x2e83, 0x41c7)
                },
                { 
                    PaperKind.A5,
                    new Size(0x20c7, 0x2e83)
                },
                { 
                    PaperKind.B4,
                    new Size(0x38ec, 0x509f)
                },
                { 
                    PaperKind.B5,
                    new Size(0x284f, 0x38eb)
                },
                { 
                    PaperKind.Folio,
                    new Size(0x2fd0, 0x4920)
                },
                { 
                    PaperKind.Quarto,
                    new Size(0x2f9d, 0x3ce7)
                },
                { 
                    PaperKind.Standard10x14,
                    new Size(0x3840, 0x4ec0)
                },
                { 
                    PaperKind.Standard11x17,
                    new Size(0x3de0, 0x5fa0)
                },
                { 
                    PaperKind.Note,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.Number9Envelope,
                    new Size(0x15cc, 0x31ec)
                },
                { 
                    PaperKind.Number10Envelope,
                    new Size(0x1734, 0x3570)
                },
                { 
                    PaperKind.Number11Envelope,
                    new Size(0x1950, 0x3a5c)
                },
                { 
                    PaperKind.Number12Envelope,
                    new Size(0x1ab8, 0x3de0)
                },
                { 
                    PaperKind.Number14Envelope,
                    new Size(0x1c20, 0x40b0)
                },
                { 
                    PaperKind.CSheet,
                    new Size(0x5fa0, 0x7bc0)
                },
                { 
                    PaperKind.DSheet,
                    new Size(0x7bc0, 0xbf40)
                },
                { 
                    PaperKind.ESheet,
                    new Size(0xbf40, 0xf780)
                },
                { 
                    PaperKind.DLEnvelope,
                    new Size(0x185d, 0x30ba)
                },
                { 
                    PaperKind.C5Envelope,
                    new Size(0x23e1, 0x32b8)
                },
                { 
                    PaperKind.C3Envelope,
                    new Size(0x47c1, 0x656d)
                },
                { 
                    PaperKind.C4Envelope,
                    new Size(0x32b7, 0x47c1)
                },
                { 
                    PaperKind.C6Envelope,
                    new Size(0x193f, 0x23e0)
                },
                { 
                    PaperKind.C65Envelope,
                    new Size(0x193f, 0x32b7)
                },
                { 
                    PaperKind.B4Envelope,
                    new Size(0x375d, 0x4e2d)
                },
                { 
                    PaperKind.B5Envelope,
                    new Size(0x26fa, 0x375d)
                },
                { 
                    PaperKind.B6Envelope,
                    new Size(0x26fa, 0x1baf)
                },
                { 
                    PaperKind.ItalyEnvelope,
                    new Size(0x185c, 0x32ef)
                },
                { 
                    PaperKind.MonarchEnvelope,
                    new Size(0x15cc, 0x2a30)
                },
                { 
                    PaperKind.PersonalEnvelope,
                    new Size(0x1464, 0x2490)
                },
                { 
                    PaperKind.USStandardFanfold,
                    new Size(0x53ac, 0x3de0)
                },
                { 
                    PaperKind.GermanStandardFanfold,
                    new Size(0x2fd0, 0x4380)
                },
                { 
                    PaperKind.GermanLegalFanfold,
                    new Size(0x2fd0, 0x4920)
                },
                { 
                    PaperKind.IsoB4,
                    new Size(0x375d, 0x4e2d)
                },
                { 
                    PaperKind.JapanesePostcard,
                    new Size(0x1625, 0x20c7)
                },
                { 
                    PaperKind.Standard9x11,
                    new Size(0x32a0, 0x3de0)
                },
                { 
                    PaperKind.Standard10x11,
                    new Size(0x3840, 0x3de0)
                },
                { 
                    PaperKind.Standard15x11,
                    new Size(0x5460, 0x3de0)
                },
                { 
                    PaperKind.InviteEnvelope,
                    new Size(0x30b8, 0x30b8)
                },
                { 
                    PaperKind.LetterExtra,
                    new Size(0x3570, 0x4380)
                },
                { 
                    PaperKind.LegalExtra,
                    new Size(0x3570, 0x5460)
                },
                { 
                    PaperKind.TabloidExtra,
                    new Size(0x41c2, 0x6540)
                },
                { 
                    PaperKind.A4Extra,
                    new Size(0x3425, 0x4762)
                },
                { 
                    PaperKind.LetterTransverse,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.A4Transverse,
                    new Size(0x2e83, 0x41c7)
                },
                { 
                    PaperKind.LetterExtraTransverse,
                    new Size(0x3570, 0x4380)
                },
                { 
                    PaperKind.APlus,
                    new Size(0x3245, 0x4ed7)
                },
                { 
                    PaperKind.BPlus,
                    new Size(0x438b, 0x6bd9)
                },
                { 
                    PaperKind.LetterPlus,
                    new Size(0x2fd0, 0x4762)
                },
                { 
                    PaperKind.A4Plus,
                    new Size(0x2e83, 0x4915)
                },
                { 
                    PaperKind.A5Transverse,
                    new Size(0x20c7, 0x2e83)
                },
                { 
                    PaperKind.B5Transverse,
                    new Size(0x284f, 0x38eb)
                },
                { 
                    PaperKind.A3Extra,
                    new Size(0x474f, 0x628c)
                },
                { 
                    PaperKind.A5Extra,
                    new Size(0x2689, 0x340b)
                },
                { 
                    PaperKind.B5Extra,
                    new Size(0x2c83, 0x3d1f)
                },
                { 
                    PaperKind.A2,
                    new Size(0x5d03, 0x838c)
                },
                { 
                    PaperKind.A3Transverse,
                    new Size(0x41c7, 0x5d06)
                },
                { 
                    PaperKind.A3ExtraTransverse,
                    new Size(0x474f, 0x628c)
                },
                { 
                    PaperKind.JapaneseDoublePostcard,
                    new Size(0x2c4b, 0x20c7)
                },
                { 
                    PaperKind.A6,
                    new Size(0x1741, 0x20c7)
                },
                { 
                    PaperKind.JapaneseEnvelopeKakuNumber2,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeKakuNumber3,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeChouNumber3,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeChouNumber4,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.LetterRotated,
                    new Size(0x3de0, 0x2fd0)
                },
                { 
                    PaperKind.A3Rotated,
                    new Size(0x5d06, 0x41c7)
                },
                { 
                    PaperKind.A4Rotated,
                    new Size(0x41c7, 0x2e83)
                },
                { 
                    PaperKind.A5Rotated,
                    new Size(0x2e83, 0x20c7)
                },
                { 
                    PaperKind.B4JisRotated,
                    new Size(0x509c, 0x38ea)
                },
                { 
                    PaperKind.B5JisRotated,
                    new Size(0x38ea, 0x284e)
                },
                { 
                    PaperKind.JapanesePostcardRotated,
                    new Size(0x20c7, 0x1625)
                },
                { 
                    PaperKind.JapaneseDoublePostcardRotated,
                    new Size(0x20c7, 0x2c4b)
                },
                { 
                    PaperKind.A6Rotated,
                    new Size(0x20c7, 0x1741)
                },
                { 
                    PaperKind.JapaneseEnvelopeKakuNumber2Rotated,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeKakuNumber3Rotated,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeChouNumber3Rotated,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeChouNumber4Rotated,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.B6Jis,
                    new Size(0x1c59, 0x284e)
                },
                { 
                    PaperKind.B6JisRotated,
                    new Size(0x284e, 0x1c59)
                },
                { 
                    PaperKind.Standard12x11,
                    new Size(0x4380, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeYouNumber4,
                    new Size(0x2fd0, 0x3de0)
                },
                { 
                    PaperKind.JapaneseEnvelopeYouNumber4Rotated,
                    new Size(0x3de0, 0x2fd0)
                },
                { 
                    PaperKind.Prc16K,
                    new Size(0x2055, 0x2f9d)
                },
                { 
                    PaperKind.Prc32K,
                    new Size(0x157b, 0x2171)
                },
                { 
                    PaperKind.Prc32KBig,
                    new Size(0x157b, 0x2171)
                },
                { 
                    PaperKind.PrcEnvelopeNumber1,
                    new Size(0x1697, 0x248a)
                },
                { 
                    PaperKind.PrcEnvelopeNumber2,
                    new Size(0x1697, 0x26fa)
                },
                { 
                    PaperKind.PrcEnvelopeNumber3,
                    new Size(0x1baf, 0x26fa)
                },
                { 
                    PaperKind.PrcEnvelopeNumber4,
                    new Size(0x185c, 0x2e10)
                },
                { 
                    PaperKind.PrcEnvelopeNumber5,
                    new Size(0x185c, 0x30b8)
                },
                { 
                    PaperKind.PrcEnvelopeNumber6,
                    new Size(0x1a93, 0x32ef)
                },
                { 
                    PaperKind.PrcEnvelopeNumber7,
                    new Size(0x236f, 0x32ef)
                },
                { 
                    PaperKind.PrcEnvelopeNumber8,
                    new Size(0x1a93, 0x446e)
                },
                { 
                    PaperKind.PrcEnvelopeNumber9,
                    new Size(0x32b7, 0x47c1)
                },
                { 
                    PaperKind.PrcEnvelopeNumber10,
                    new Size(0x47c1, 0x656d)
                },
                { 
                    PaperKind.Prc16KRotated,
                    new Size(0x2f9d, 0x2055)
                },
                { 
                    PaperKind.Prc32KRotated,
                    new Size(0x2171, 0x157b)
                },
                { 
                    PaperKind.Prc32KBigRotated,
                    new Size(0x2171, 0x157b)
                },
                { 
                    PaperKind.PrcEnvelopeNumber1Rotated,
                    new Size(0x248a, 0x1697)
                },
                { 
                    PaperKind.PrcEnvelopeNumber2Rotated,
                    new Size(0x26fa, 0x1697)
                },
                { 
                    PaperKind.PrcEnvelopeNumber3Rotated,
                    new Size(0x26fa, 0x1baf)
                },
                { 
                    PaperKind.PrcEnvelopeNumber4Rotated,
                    new Size(0x2e10, 0x185c)
                },
                { 
                    PaperKind.PrcEnvelopeNumber5Rotated,
                    new Size(0x30b8, 0x185c)
                },
                { 
                    PaperKind.PrcEnvelopeNumber6Rotated,
                    new Size(0x32ef, 0x1a93)
                },
                { 
                    PaperKind.PrcEnvelopeNumber7Rotated,
                    new Size(0x32ef, 0x236f)
                },
                { 
                    PaperKind.PrcEnvelopeNumber8Rotated,
                    new Size(0x446e, 0x1a93)
                },
                { 
                    PaperKind.PrcEnvelopeNumber9Rotated,
                    new Size(0x47c1, 0x32b7)
                },
                { 
                    PaperKind.PrcEnvelopeNumber10Rotated,
                    new Size(0x656d, 0x47c1)
                }
            };

        internal static IDictionary<PaperKind, Size> PaperSizeTable =>
            paperSizeTable;
    }
}

