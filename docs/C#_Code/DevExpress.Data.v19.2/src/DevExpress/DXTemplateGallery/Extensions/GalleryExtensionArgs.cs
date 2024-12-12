namespace DevExpress.DXTemplateGallery.Extensions
{
    using System;
    using System.Runtime.CompilerServices;

    public class GalleryExtensionArgs
    {
        public GalleryExtensionArgs(object dte, object template) : this(dte, string.Empty, template, null, null)
        {
        }

        public GalleryExtensionArgs(object dte, string itemName, object template, IGalleryContext parentContext, IGalleryExtension extensionObj)
        {
            this.DTE = dte;
            this.ItemName = itemName;
            this.Template = template;
            this.ParentContext = parentContext;
            this.ParentExtensionObj = extensionObj;
        }

        public object DTE { get; private set; }

        public IGalleryContext ParentContext { get; private set; }

        public IGalleryExtension ParentExtensionObj { get; private set; }

        public string ItemName { get; private set; }

        public object Template { get; private set; }
    }
}

