namespace DevExpress.Data.Mask
{
    using System;

    public sealed class BracketTransition : Transition
    {
        private readonly bool notMatch;
        private readonly BracketTransitionRange[] ranges;

        public BracketTransition(bool notMatch, BracketTransitionRange[] ranges);
        private BracketTransition(State target, bool notMatch, BracketTransitionRange[] ranges);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public override bool IsMatch(char input);
        public override string ToString();

        public override bool IsExact { get; }
    }
}

