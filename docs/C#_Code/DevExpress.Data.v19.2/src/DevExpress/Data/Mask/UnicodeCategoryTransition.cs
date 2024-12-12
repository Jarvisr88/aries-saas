namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public sealed class UnicodeCategoryTransition : Transition
    {
        private readonly bool notMatch;
        private readonly string fCategory;
        private readonly UnicodeCategory[] fCategoriesCodes;
        private static readonly IDictionary<string, UnicodeCategory[]> fUnicodeCategoryNames;
        private const string sampleChars = "Aaǅʰƻ̀ः҈0Ⅰ\x00b2 \u2028\u2029\t܏\ud800\ue000_-()\x00ab\x00bb!+$^\x00a6Ƞ";

        static UnicodeCategoryTransition();
        private UnicodeCategoryTransition(State target, UnicodeCategoryTransition source);
        public UnicodeCategoryTransition(string category, bool notMatch);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public static UnicodeCategory[] GetUnicodeCategoryListFromCharacterClassName(string className);
        public override bool IsMatch(char input);
        public override string ToString();
    }
}

