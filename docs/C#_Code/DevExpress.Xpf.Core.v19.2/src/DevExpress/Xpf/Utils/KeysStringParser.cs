namespace DevExpress.Xpf.Utils
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class KeysStringParser
    {
        private string keys;
        private bool alt;
        private bool ctrl;
        private bool shift;
        private List<KeyAction> keyActions;

        public KeysStringParser(string keys)
        {
            this.keys = keys;
            this.alt = false;
            this.ctrl = false;
            this.shift = false;
            this.keyActions = new List<KeyAction>();
        }

        protected KeyAction CreateKeyAction(char key)
        {
            KeyAction keyAction = new KeyAction {
                IsChar = true,
                VCode = key.ToString().ToUpper()[0],
                Character = key
            };
            return this.FillModifierState(keyAction);
        }

        protected KeyAction CreateKeyAction(Keys key)
        {
            KeyAction keyAction = new KeyAction {
                IsChar = false,
                VCode = Helper.GetKeyValue(key),
                Character = 0
            };
            return this.FillModifierState(keyAction);
        }

        protected KeyAction FillModifierState(KeyAction keyAction)
        {
            keyAction.Alt = this.Alt;
            keyAction.Ctrl = this.Ctrl;
            keyAction.Shift = this.Shift;
            keyAction.IsSealedKey = false;
            keyAction.SealedKeyState = false;
            return keyAction;
        }

        protected bool GetEnclosedChar(ref string keys)
        {
            Regex regex = new Regex(@"^{([\+\[\]\^%~{}])}");
            if (!regex.IsMatch(keys))
            {
                return false;
            }
            keys = regex.Replace(keys, "$1");
            return true;
        }

        protected Keys GetEnclosedKey(ref string keys)
        {
            Keys none = Keys.None;
            if ((keys[0] == '[') || (keys[0] == '{'))
            {
                char ch = (keys[0] == '[') ? ']' : '}';
                int index = keys.IndexOf(ch);
                int num2 = keys.IndexOf(keys[0], 1);
                if ((index < 0) || ((num2 > 0) && (index > num2)))
                {
                    return none;
                }
                string str = keys.Substring(1, index - 1);
                if (str == string.Empty)
                {
                    return none;
                }
                str = str.ToUpper();
                try
                {
                    none = (Keys) Enum.Parse(typeof(InputKeyTemplate), str, true);
                    keys = keys.Remove(0, index + 1);
                }
                catch
                {
                    using (IEnumerator enumerator = typeof(Keys).GetValues().GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            Keys current = (Keys) enumerator.Current;
                            if (current.ToString().ToUpper() == str)
                            {
                                none = current;
                                keys = keys.Remove(0, index + 1);
                                return none;
                            }
                        }
                    }
                }
            }
            return none;
        }

        protected Keys GetNonEclosedKey(ref string keys)
        {
            Keys none = Keys.None;
            char ch = keys[0];
            if (ch <= '+')
            {
                if (ch == '%')
                {
                    none = Keys.Alt;
                }
                else if (ch == '+')
                {
                    none = Keys.Shift;
                }
            }
            else if (ch == '^')
            {
                none = Keys.Control;
            }
            else if (ch == '~')
            {
                none = Keys.Enter;
            }
            if (none != Keys.None)
            {
                keys = keys.Remove(0, 1);
            }
            return none;
        }

        protected bool HasKey(ref string keys, out Keys key)
        {
            key = Keys.None;
            if (keys == string.Empty)
            {
                return false;
            }
            key = this.GetNonEclosedKey(ref keys);
            if (key != Keys.None)
            {
                return true;
            }
            if (this.GetEnclosedChar(ref keys))
            {
                return false;
            }
            key = this.GetEnclosedKey(ref keys);
            return (key != Keys.None);
        }

        protected virtual void InputKey(Keys key, ArrayList sealedKeyPressed)
        {
            if (Helper.IsSealedKey(key) && (sealedKeyPressed.IndexOf(key) < 0))
            {
                this.PressedSealedKey(key, ref sealedKeyPressed);
            }
            else
            {
                this.KeyActions.Add(this.CreateKeyAction(key));
            }
        }

        public List<KeyAction> Parse()
        {
            this.Parse(this.KeysString);
            return this.KeyActions;
        }

        protected void Parse(string keys)
        {
            ArrayList sealedKeyPressed = new ArrayList();
            Regex regex = new Regex(@"^\((.+?)\)");
            while (keys != string.Empty)
            {
                Keys keys2;
                if (this.HasKey(ref keys, out keys2))
                {
                    this.InputKey(keys2, sealedKeyPressed);
                    if (Helper.IsSealedKey(keys2))
                    {
                        continue;
                    }
                    this.UnPressedSealedKeys(ref sealedKeyPressed);
                    continue;
                }
                if ((sealedKeyPressed.Count != 0) && regex.IsMatch(keys))
                {
                    this.Parse(regex.Match(keys).Result("$1"));
                    keys = regex.Replace(keys, "");
                }
                else
                {
                    this.ParseCharKey(keys[0], ref sealedKeyPressed);
                    keys = keys.Remove(0, 1);
                }
                this.UnPressedSealedKeys(ref sealedKeyPressed);
            }
            this.UnPressedSealedKeys(ref sealedKeyPressed);
        }

        protected void ParseCharKey(char ch, ref ArrayList sealedKeyPressed)
        {
            uint num = Helper.VkKeyScan(ch);
            List<Keys> list = new List<Keys>();
            if (!this.Alt && ((num & 0x800) != 0))
            {
                list.Add(Keys.Alt);
            }
            if (!this.Ctrl && ((num & 0x200) != 0))
            {
                list.Add(Keys.Control);
            }
            if (!this.Shift && ((num & 0x100) != 0))
            {
                list.Add(Keys.Shift);
            }
            foreach (Keys keys in list)
            {
                this.PressedSealedKey(keys, ref sealedKeyPressed);
            }
            this.KeyActions.Add(this.CreateKeyAction((char) (num & 0xff)));
            foreach (Keys keys2 in list)
            {
                this.UnPressedSealedKey(keys2, ref sealedKeyPressed);
            }
        }

        protected void PressedSealedKey(Keys key, ref ArrayList sealedKeyPressed)
        {
            this.UpdateModifiersState(key, true);
            KeyAction item = this.CreateKeyAction(key);
            sealedKeyPressed.Add(key);
            item.IsSealedKey = true;
            item.SealedKeyState = true;
            this.KeyActions.Add(item);
        }

        protected void UnPressedSealedKey(Keys key)
        {
            this.UpdateModifiersState(key, false);
            KeyAction item = this.CreateKeyAction(key);
            item.IsSealedKey = true;
            this.KeyActions.Add(item);
        }

        protected void UnPressedSealedKey(Keys key, ref ArrayList sealedKeyPressed)
        {
            this.UnPressedSealedKey(key);
            for (int i = sealedKeyPressed.IndexOf(key); i > -1; i = sealedKeyPressed.IndexOf(key))
            {
                sealedKeyPressed.RemoveAt(i);
            }
        }

        protected void UnPressedSealedKeys(ref ArrayList sealedKeyPressed)
        {
            foreach (Keys keys in sealedKeyPressed)
            {
                this.UnPressedSealedKey(keys);
            }
            sealedKeyPressed.Clear();
        }

        protected void UpdateModifiersState(Keys key, bool state)
        {
            if (key == Keys.Alt)
            {
                this.Alt = state;
            }
            else if (key == Keys.Control)
            {
                this.Ctrl = state;
            }
            else if (key == Keys.Shift)
            {
                this.Shift = state;
            }
        }

        protected bool Alt
        {
            get => 
                this.alt;
            set => 
                this.alt = value;
        }

        protected bool Ctrl
        {
            get => 
                this.ctrl;
            set => 
                this.ctrl = value;
        }

        protected bool Shift
        {
            get => 
                this.shift;
            set => 
                this.shift = value;
        }

        protected string KeysString
        {
            get => 
                this.keys;
            set => 
                this.keys = value;
        }

        protected List<KeyAction> KeyActions =>
            this.keyActions;
    }
}

