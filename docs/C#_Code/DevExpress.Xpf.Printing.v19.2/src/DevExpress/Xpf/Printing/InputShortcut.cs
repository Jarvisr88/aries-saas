namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public abstract class InputShortcut
    {
        private readonly List<ModifierKeys> modifiers;

        public InputShortcut() : this(new ModifierKeys[0])
        {
        }

        public InputShortcut(ModifierKeys[] modifiers)
        {
            Guard.ArgumentNotNull(modifiers, "modifiers");
            modifiers = modifiers.Distinct<ModifierKeys>().ToArray<ModifierKeys>();
            this.modifiers = new List<ModifierKeys>(modifiers);
            if (this.modifiers.Contains(ModifierKeys.None))
            {
                this.modifiers.Remove(ModifierKeys.None);
            }
        }

        protected bool AreModifierArraysEqual(ModifierKeys[] first, ModifierKeys[] second)
        {
            if ((first == null) || (second == null))
            {
                return false;
            }
            if (first.Length != second.Length)
            {
                return false;
            }
            foreach (ModifierKeys keys in first)
            {
                if (!second.Contains<ModifierKeys>(keys))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            Converter<char, int> converter = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Converter<char, int> local1 = <>c.<>9__8_0;
                converter = <>c.<>9__8_0 = chr => chr;
            }
            return HashCodeHelper.CalcHashCode(ArrayHelper.ConvertAll<char, int>(this.DisplayString.ToCharArray(), converter));
        }

        public ModifierKeys[] Modifiers =>
            this.modifiers.ToArray();

        public virtual string DisplayString =>
            (this.Modifiers.Length != 0) ? string.Join<ModifierKeys>("+", this.Modifiers) : Enum.GetName(typeof(ModifierKeys), ModifierKeys.None);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InputShortcut.<>c <>9 = new InputShortcut.<>c();
            public static Converter<char, int> <>9__8_0;

            internal int <GetHashCode>b__8_0(char chr) => 
                chr;
        }
    }
}

