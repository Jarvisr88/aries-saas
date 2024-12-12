namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Input;

    public static class KeyEventArgsExtensions
    {
        private static int ConvertModKeys(int key)
        {
            switch (key)
            {
                case 160:
                case 0xa1:
                    return 0x10000;

                case 0xa2:
                case 0xa3:
                    return 0x20000;

                case 0xa4:
                case 0xa5:
                    return 0x40000;
            }
            return key;
        }

        private static int GetModKeys()
        {
            int num = 0;
            if (KeyboardHelper.IsShiftPressed)
            {
                num |= KeyMapper.GetValue(Keys.Shift);
            }
            if (KeyboardHelper.IsControlPressed)
            {
                num |= KeyMapper.GetValue(Keys.Control);
            }
            if (KeyboardHelper.IsAltPressed)
            {
                num |= KeyMapper.GetValue(Keys.Alt);
            }
            return num;
        }

        public static System.Windows.Forms.KeyEventArgs ToPlatformIndependent(this System.Windows.Input.KeyEventArgs e) => 
            new System.Windows.Forms.KeyEventArgs(((Keys) ConvertModKeys(KeyInterop.VirtualKeyFromKey((e.Key == Key.System) ? e.SystemKey : e.Key))) | ((Keys) GetModKeys()));
    }
}

