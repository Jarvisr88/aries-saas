namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Windows.Input;

    public static class KeyMapper
    {
        private static Dictionary<Key, Keys> keyTable;

        static KeyMapper()
        {
            InitKeyTable();
        }

        internal static int GetValue(Keys key) => 
            (int) key;

        private static void InitKeyTable()
        {
            keyTable = new Dictionary<Key, Keys>();
            keyTable[Key.None] = Keys.None;
            keyTable[Key.Back] = Keys.Back;
            keyTable[Key.Tab] = Keys.Tab;
            keyTable[Key.Return] = Keys.Enter;
            keyTable[Key.Escape] = Keys.Escape;
            keyTable[Key.Space] = Keys.Space;
            keyTable[Key.Prior] = Keys.PageUp;
            keyTable[Key.Next] = Keys.Next;
            keyTable[Key.End] = Keys.End;
            keyTable[Key.Home] = Keys.Home;
            keyTable[Key.Left] = Keys.Left;
            keyTable[Key.Up] = Keys.Up;
            keyTable[Key.Right] = Keys.Right;
            keyTable[Key.Down] = Keys.Down;
            keyTable[Key.Insert] = Keys.Insert;
            keyTable[Key.Delete] = Keys.Delete;
            keyTable[Key.D0] = Keys.D0;
            keyTable[Key.D1] = Keys.D1;
            keyTable[Key.D2] = Keys.D2;
            keyTable[Key.D3] = Keys.D3;
            keyTable[Key.D4] = Keys.D4;
            keyTable[Key.D5] = Keys.D5;
            keyTable[Key.D6] = Keys.D6;
            keyTable[Key.D7] = Keys.D7;
            keyTable[Key.D8] = Keys.D8;
            keyTable[Key.D9] = Keys.D9;
            keyTable[Key.A] = Keys.A;
            keyTable[Key.B] = Keys.B;
            keyTable[Key.C] = Keys.C;
            keyTable[Key.D] = Keys.D;
            keyTable[Key.E] = Keys.E;
            keyTable[Key.F] = Keys.F;
            keyTable[Key.G] = Keys.G;
            keyTable[Key.H] = Keys.H;
            keyTable[Key.I] = Keys.I;
            keyTable[Key.J] = Keys.J;
            keyTable[Key.K] = Keys.K;
            keyTable[Key.L] = Keys.L;
            keyTable[Key.M] = Keys.M;
            keyTable[Key.N] = Keys.N;
            keyTable[Key.O] = Keys.O;
            keyTable[Key.P] = Keys.P;
            keyTable[Key.Q] = Keys.Q;
            keyTable[Key.R] = Keys.R;
            keyTable[Key.S] = Keys.S;
            keyTable[Key.T] = Keys.T;
            keyTable[Key.U] = Keys.U;
            keyTable[Key.V] = Keys.V;
            keyTable[Key.W] = Keys.W;
            keyTable[Key.X] = Keys.X;
            keyTable[Key.Y] = Keys.Y;
            keyTable[Key.Z] = Keys.Z;
            keyTable[Key.F1] = Keys.F1;
            keyTable[Key.F2] = Keys.F2;
            keyTable[Key.F3] = Keys.F3;
            keyTable[Key.F4] = Keys.F4;
            keyTable[Key.F5] = Keys.F5;
            keyTable[Key.F6] = Keys.F6;
            keyTable[Key.F7] = Keys.F7;
            keyTable[Key.F8] = Keys.F8;
            keyTable[Key.F9] = Keys.F9;
            keyTable[Key.F10] = Keys.F10;
            keyTable[Key.F11] = Keys.F11;
            keyTable[Key.F12] = Keys.F12;
            keyTable[Key.NumPad0] = Keys.NumPad0;
            keyTable[Key.NumPad1] = Keys.NumPad1;
            keyTable[Key.NumPad2] = Keys.NumPad2;
            keyTable[Key.NumPad3] = Keys.NumPad3;
            keyTable[Key.NumPad4] = Keys.NumPad4;
            keyTable[Key.NumPad5] = Keys.NumPad5;
            keyTable[Key.NumPad6] = Keys.NumPad6;
            keyTable[Key.NumPad7] = Keys.NumPad7;
            keyTable[Key.NumPad8] = Keys.NumPad8;
            keyTable[Key.NumPad9] = Keys.NumPad9;
            keyTable[Key.Multiply] = Keys.Multiply;
            keyTable[Key.Add] = Keys.Add;
            keyTable[Key.Subtract] = Keys.Subtract;
            keyTable[Key.Decimal] = Keys.Decimal;
            keyTable[Key.Divide] = Keys.Divide;
            keyTable[Key.Capital] = Keys.Capital;
            keyTable[Key.LeftShift] = Keys.Shift;
            keyTable[Key.RightShift] = Keys.Shift;
            keyTable[Key.LeftCtrl] = Keys.Control;
            keyTable[Key.RightCtrl] = Keys.Control;
            keyTable[Key.LeftAlt] = Keys.Alt;
            keyTable[Key.RightAlt] = Keys.Alt;
        }

        public static int KeyToKeysValue(Key k)
        {
            Keys keys;
            return (keyTable.TryGetValue(k, out keys) ? GetValue(keys) : (((int) k) << 8));
        }
    }
}

