namespace DevExpress.DXTemplateGallery.Extensions
{
    using System;

    public interface IGalleryContext
    {
        void AddItem(object rootFolder, string strongName, string itemName);
        void AddItem(object rootFolder, string strongName, string itemName, IGalleryExtension parentExtensionObj, bool syncCall);
        void CancelDefault();
        void ChangeRootFolder(object rootFolder);

        Action<IGalleryContext, string> DoDefault { get; }

        object RootFolder { get; }

        bool IsCSharp { get; }

        bool IsVb { get; }

        IGalleryContext ParentContext { get; }

        IGalleryExtension ParentExtensionObj { get; }

        string ExtendedFieldValue { get; }

        object Dte { get; }
    }
}

