namespace DevExpress.Data.Mask
{
    using System;

    public sealed class WordTransition : Transition
    {
        private readonly bool notMatch;

        public WordTransition(bool notMatch);
        private WordTransition(State target, bool notMatch);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public override bool IsMatch(char input);
        public override string ToString();
    }
}

