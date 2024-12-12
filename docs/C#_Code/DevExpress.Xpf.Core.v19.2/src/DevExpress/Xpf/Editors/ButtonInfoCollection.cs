namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.ObjectModel;

    public class ButtonInfoCollection : ObservableCollection<ButtonInfoBase>
    {
        public void Add(ButtonInfo item)
        {
            base.Add(item);
        }

        public void Add(SpinButtonInfo item)
        {
            base.Add(item);
        }

        protected bool ButtonInfoEquals(ButtonInfoBase x, ButtonInfoBase y) => 
            x.IsClone(y);

        public override bool Equals(object obj)
        {
            ButtonInfoCollection infos = obj as ButtonInfoCollection;
            if ((infos == null) || (infos.Count != base.Count))
            {
                return false;
            }
            for (int i = 0; i < base.Count; i++)
            {
                if (!this.ButtonInfoEquals(infos[i], base[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => 
            base.Count;
    }
}

