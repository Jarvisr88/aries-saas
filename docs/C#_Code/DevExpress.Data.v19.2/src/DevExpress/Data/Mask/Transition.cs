namespace DevExpress.Data.Mask
{
    using System;

    public abstract class Transition
    {
        private readonly State target;

        protected Transition();
        protected Transition(State target);
        public abstract Transition Copy(State target);
        public abstract char GetSampleChar();
        public abstract bool IsMatch(char input);

        public State Target { get; }

        public virtual bool IsEmpty { get; }

        public virtual bool IsExact { get; }
    }
}

