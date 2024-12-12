namespace DevExpress.Internal
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct EAState
    {
        private int value;
        public static readonly EAState Start;
        public static readonly EAState Error;
        public static readonly EAState ItsMe;
        private EAState(int value)
        {
            this.value = value;
        }

        public override bool Equals(object obj) => 
            (obj is EAState) && (this.value == ((EAState) obj).value);

        public override int GetHashCode() => 
            this.value.GetHashCode();

        public static implicit operator int(EAState state) => 
            state.value;

        public static explicit operator EAState(int state) => 
            new EAState(state);

        public static bool operator ==(EAState pos1, EAState pos2) => 
            pos1.value == pos2.value;

        public static bool operator !=(EAState pos1, EAState pos2) => 
            pos1.value != pos2.value;

        static EAState()
        {
            Start = new EAState(0);
            Error = new EAState(1);
            ItsMe = new EAState(2);
        }
    }
}

