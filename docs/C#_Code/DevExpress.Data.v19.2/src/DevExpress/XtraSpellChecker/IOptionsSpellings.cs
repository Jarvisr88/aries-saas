namespace DevExpress.XtraSpellChecker
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public interface IOptionsSpellings
    {
        event EventHandler OptionChanged;

        void BeginUpdate();
        void EndUpdate();

        DefaultBoolean CheckFromCursorPos { get; set; }

        DefaultBoolean CheckSelectedTextFirst { get; set; }

        DefaultBoolean IgnoreEmails { get; set; }

        DefaultBoolean IgnoreMarkupTags { get; set; }

        DefaultBoolean IgnoreMixedCaseWords { get; set; }

        DefaultBoolean IgnoreRepeatedWords { get; set; }

        DefaultBoolean IgnoreUpperCaseWords { get; set; }

        DefaultBoolean IgnoreUrls { get; set; }

        DefaultBoolean IgnoreWordsWithNumbers { get; set; }
    }
}

