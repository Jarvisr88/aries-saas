namespace DevExpress.Data.Mask
{
    using System;

    public sealed class OneSymbolTransition : Transition
    {
        private readonly char input;

        public OneSymbolTransition(char input);
        private OneSymbolTransition(State target, char input);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public override bool IsMatch(char input);
        public override string ToString();

        public override bool IsExact { get; }
    }
}

