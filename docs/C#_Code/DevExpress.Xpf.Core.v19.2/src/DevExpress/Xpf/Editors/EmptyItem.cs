namespace DevExpress.Xpf.Editors
{
    using System;

    public class EmptyItem : CustomItem
    {
        private readonly string defaultText = EditorLocalizer.GetString(EditorStringId.EmptyItem);

        protected override string GetDisplayTextInternal() => 
            this.defaultText;

        protected internal override bool ShouldFilter =>
            false;
    }
}

