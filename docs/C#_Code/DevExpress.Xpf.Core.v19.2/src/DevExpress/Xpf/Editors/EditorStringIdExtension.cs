namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class EditorStringIdExtension : MarkupExtension
    {
        public EditorStringIdExtension(EditorStringId stringId)
        {
            this.StringId = stringId;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            EditorLocalizer.GetString(this.StringId);

        public EditorStringId StringId { get; set; }
    }
}

