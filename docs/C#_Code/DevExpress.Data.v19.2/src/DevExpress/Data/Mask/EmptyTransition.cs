namespace DevExpress.Data.Mask
{
    using System;

    public sealed class EmptyTransition : Transition
    {
        public EmptyTransition();
        public EmptyTransition(State target);
        public override Transition Copy(State target);
        public override char GetSampleChar();
        public override bool IsMatch(char input);
        public override string ToString();

        public override bool IsEmpty { get; }
    }
}

