namespace DevExpress.Data.Mask
{
    using System;

    public sealed class DecimalDigitTransition : Transition
    {
        private readonly bool notMatch;

        public DecimalDigitTransition(bool notMatch);
        private DecimalDigitTransition(State target, bool notMatch);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public override bool IsMatch(char input);
        public override string ToString();
    }
}

