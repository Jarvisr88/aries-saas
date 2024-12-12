namespace DevExpress.Data.Mask
{
    using System;

    public sealed class AnySymbolTransition : Transition
    {
        public AnySymbolTransition();
        private AnySymbolTransition(State target);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public override bool IsMatch(char input);
        public override string ToString();
    }
}

