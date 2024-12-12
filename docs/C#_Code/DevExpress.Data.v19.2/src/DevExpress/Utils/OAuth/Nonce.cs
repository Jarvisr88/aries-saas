namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Nonce
    {
        private static Random s_random;
        private string _value;
        public static Nonce CreateNonce()
        {
            s_random ??= new Random();
            return new Nonce(s_random.Next(0x3a7e68, 0x80d5f7) + DateTime.Now.Ticks);
        }

        public Nonce(long value)
        {
            if (value <= 0L)
            {
                throw new ArgumentOutOfRangeException("value");
            }
            this._value = value.ToString();
        }

        public Nonce(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            this._value = value;
        }

        public bool IsEmpty =>
            string.IsNullOrEmpty(this._value);
        public string Value
        {
            get
            {
                if (string.IsNullOrEmpty(this._value))
                {
                    throw new InvalidOperationException();
                }
                return this._value;
            }
        }
    }
}

