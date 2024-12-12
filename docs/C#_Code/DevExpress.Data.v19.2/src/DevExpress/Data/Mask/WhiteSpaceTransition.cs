namespace DevExpress.Data.Mask
{
    using System;

    public sealed class WhiteSpaceTransition : Transition
    {
        private readonly bool notMatch;

        public WhiteSpaceTransition(bool notMatch);
        private WhiteSpaceTransition(State target, bool notMatch);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public override bool IsMatch(char input);
        public override string ToString();
    }
}

