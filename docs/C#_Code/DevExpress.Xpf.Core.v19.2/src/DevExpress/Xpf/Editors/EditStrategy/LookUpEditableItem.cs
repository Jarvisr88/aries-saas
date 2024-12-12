namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public class LookUpEditableItem : ICustomItem
    {
        public bool Completed { get; set; }

        public DevExpress.Xpf.Editors.EditStrategy.ChangeTextItem ChangeTextItem { get; set; }

        public object DisplayValue { get; set; }

        public object EditValue { get; set; }

        public bool ProcessNewValueCompleted { get; set; }

        public bool AsyncLoading { get; set; }

        public bool AutoCompleteTextDeleted { get; set; }

        public bool ForbidFindIncremental { get; set; }

        public bool AcceptedFromPopup { get; set; }
    }
}

