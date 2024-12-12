namespace ActiproSoftware.WinUICore.Commands
{
    using ActiproSoftware.WinUICore.Input;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Windows.Forms;

    public class CommandLinkCollection : CollectionBase
    {
        public int Add(CommandLink commandLink) => 
            ((IList) this).Add(commandLink);

        public void AddRange(CommandLink[] commandLinks)
        {
            foreach (CommandLink link in commandLinks)
            {
                ((IList) this).Add(link);
            }
        }

        public bool Contains(CommandLink commandLink) => 
            this.IndexOf(commandLink) != -1;

        public static Keys FilterModifierKeys(Keys keyCode)
        {
            if ((keyCode & Keys.Control) == Keys.Control)
            {
                keyCode ^= Keys.Control;
            }
            if ((keyCode & Keys.Shift) == Keys.Shift)
            {
                keyCode ^= Keys.Shift;
            }
            if ((keyCode & Keys.Alt) == Keys.Alt)
            {
                keyCode ^= Keys.Alt;
            }
            return keyCode;
        }

        public CommandLink[] GetApplicableCommandLinks(Keys keyCode)
        {
            Keys keys = FilterModifierKeys(keyCode);
            ModifierKeys modifierKeys = GetModifierKeys(keyCode);
            ArrayList list = new ArrayList();
            for (int i = 0; i < base.Count; i++)
            {
                if (this[i].#Oxe(keys, modifierKeys))
                {
                    list.Add(this[i]);
                }
            }
            return (CommandLink[]) list.ToArray(typeof(CommandLink));
        }

        public static ModifierKeys GetCurrentModifierKeys() => 
            GetModifierKeys(Control.ModifierKeys);

        public static ModifierKeys GetModifierKeys(Keys keyCode)
        {
            ModifierKeys none = ModifierKeys.None;
            bool flag = (keyCode & Keys.Shift) == Keys.Shift;
            bool flag2 = (keyCode & Keys.Alt) == Keys.Alt;
            if ((keyCode & Keys.Control) == Keys.Control)
            {
                none |= ModifierKeys.Control;
            }
            if (flag)
            {
                none |= ModifierKeys.Shift;
            }
            if (flag2)
            {
                none |= ModifierKeys.Alt;
            }
            return none;
        }

        public virtual int IndexOf(ActiproSoftware.WinUICore.Commands.Command command)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (ReferenceEquals(this[i].Command, command))
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(CommandLink commandLink) => 
            ((IList) this).IndexOf(commandLink);

        public void Insert(int index, CommandLink commandLink)
        {
            ((IList) this).Insert(index, commandLink);
        }

        public void Remove(CommandLink commandLink)
        {
            ((IList) this).Remove(commandLink);
        }

        public CommandLink this[int index] =>
            (CommandLink) this.InnerList[index];
    }
}

