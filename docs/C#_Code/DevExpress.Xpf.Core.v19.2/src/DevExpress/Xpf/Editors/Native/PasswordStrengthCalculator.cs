namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;

    public static class PasswordStrengthCalculator
    {
        private static readonly int magicNumber = 0x1a;
        private static readonly int punctuationSymbolsCount = 0x34;
        private static readonly int unicodeSymbolsCount = 0x1dee;

        private static double CalcStrength(string password)
        {
            if (((password != null) ? password.Length : 0) == 0)
            {
                return 0.0;
            }
            int num2 = 0;
            if (HasDigit(password))
            {
                num2 += 10;
            }
            if (HasLowercaseLetter(password))
            {
                num2 += magicNumber;
            }
            if (HasUpperCaseLetter(password))
            {
                num2 += magicNumber;
            }
            if (HasPunctuationLetter(password))
            {
                num2 += punctuationSymbolsCount;
            }
            if (HasAnotherLetter(password))
            {
                num2 += unicodeSymbolsCount;
            }
            return (password.Length * Math.Log((double) num2, 2.0));
        }

        public static PasswordStrength Calculate(string password)
        {
            double num = CalcStrength(password);
            return ((num >= 20.0) ? ((num >= 32.0) ? ((num >= 64.0) ? PasswordStrength.Strong : PasswordStrength.Good) : PasswordStrength.Fair) : PasswordStrength.Weak);
        }

        private static bool HasAnotherLetter(string password)
        {
            foreach (char ch in password)
            {
                string str2 = new string(ch, 1);
                if (!HasLowercaseLetter(str2) && (!HasUpperCaseLetter(str2) && (!HasPunctuationLetter(str2) && !HasDigit(str2))))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasDigit(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.DecimalDigitNumber)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasLowercaseLetter(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.LowercaseLetter)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasPunctuationLetter(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.ClosePunctuation) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.ConnectorPunctuation) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.DashPunctuation) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.FinalQuotePunctuation) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.InitialQuotePunctuation) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.ModifierLetter) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.ModifierSymbol) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.OpenPunctuation) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.OtherPunctuation) || ((CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.SpaceSeparator) || (CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.MathSymbol)))))))))))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasUpperCaseLetter(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(password, i) == UnicodeCategory.UppercaseLetter)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

