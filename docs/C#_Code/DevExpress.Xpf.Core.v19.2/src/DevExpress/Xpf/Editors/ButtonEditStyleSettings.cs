namespace DevExpress.Xpf.Editors
{
    using System;

    public class ButtonEditStyleSettings : TextEditStyleSettings
    {
        protected internal virtual bool GetActualAllowDefaultButton(ButtonEdit editor) => 
            true;

        public virtual bool GetIsTextEditable(ButtonEdit editor) => 
            true;
    }
}

