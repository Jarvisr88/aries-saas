namespace DevExpress.XtraSpellChecker
{
    using DevExpress.XtraSpellChecker.Parser;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public interface ISpellChecker
    {
        event AfterCheckWordEventHandler AfterCheckWord;

        event EventHandler CultureChanged;

        event EventHandler CustomDictionaryChanged;

        event EventHandler SpellCheckModeChanged;

        event WordAddedEventHandler WordAdded;

        void AddToDictionary(string word);
        void AddToDictionary(string word, CultureInfo culture);
        bool CanAddToDictionary();
        bool CanAddToDictionary(CultureInfo culture);
        void Check(object control);
        ISpellingErrorInfo Check(object control, ISpellCheckTextController controller, Position from, Position to);
        ICheckSpellingResult CheckText(object control, string text, int index, CultureInfo culture);
        IOptionsSpellings GetOptions(object control);
        string[] GetSuggestions(string word, CultureInfo culture);
        void Ignore(object control, string word, Position start, Position end);
        void IgnoreAll(object control, string word);
        void RegisterIgnoreList(object control, IIgnoreList ignoreList);
        void UnregisterIgnoreList(object control);

        bool IsChecking { get; }

        DevExpress.XtraSpellChecker.SpellCheckMode SpellCheckMode { get; set; }

        CultureInfo Culture { get; set; }
    }
}

