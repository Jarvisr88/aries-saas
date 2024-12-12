namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Dfa
    {
        internal State initialState;
        internal State finalState;
        private readonly Dictionary<DfaWave, DfaWave> statesCache;
        private readonly StringKeyTable stringsToStatesCache;
        private StringKey lastState;
        private string lastText;

        private Dfa();
        private Dfa(Dfa source);
        public Dfa(Transition initialTransition);
        private StringKey CacheWave(StringKey next, char symbol, DfaWave candidateWave);
        private bool CanReturnFromFinalState();
        private bool CanReturnToInitialState();
        public ICollection<State> GetAllStates();
        public AutoCompleteInfo GetAutoCompleteInfo(string text);
        private DfaWave GetInitialStates();
        public string GetOptimisticHint(string displayText);
        public string GetPlaceHolders(string displayText, char anySymbolHolder);
        public object[] GetPlaceHoldersInfo(string displayText);
        internal DfaWave GetWave(string text);
        internal static Dfa HardAnd(Dfa head, Dfa tail);
        internal static Dfa HardOr(Dfa one, Dfa merged);
        public bool IsFinal(string input);
        public bool IsMatch(string input);
        public static bool IsMatch(string input, string pattern, CultureInfo cultureInfo);
        public bool IsValidStart(string start);
        public static Dfa operator &(Dfa left, Dfa right);
        public static Dfa operator |(Dfa left, Dfa right);
        public static Dfa Parse(string pattern, CultureInfo cultureInfo);
        public static Dfa Parse(string pattern, bool reverseAutomate, CultureInfo cultureInfo);
        public static Dfa Power(Dfa operand, int minMatches, int maxMatches);
        private static Dfa Power0Unlimited(Dfa operand);
        private static Dfa Power1Unlimited(Dfa operand);
        private static Dfa PowerExact(Dfa operand, int power);
        private static Dfa PowerOptional(Dfa operand, int count);
        public override string ToString();
        private string ToString(DfaWave wave);

        public static Dfa Empty { get; }

        public static Dfa EmptyTransitionDfa { get; }
    }
}

