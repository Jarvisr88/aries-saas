namespace DevExpress.Xpf.Editors.EditStrategy
{
    using System;
    using System.Runtime.CompilerServices;

    public class ChangeTextItem
    {
        public string Text { get; set; }

        public bool UpdateAutoCompleteSelection { get; set; }

        public bool AutoCompleteTextDeleted { get; set; }

        public bool AsyncLoading { get; set; }

        public bool AcceptedFromPopup { get; set; }
    }
}

