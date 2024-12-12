namespace DevExpress.DXTemplateGallery.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IGalleryExtension
    {
        event EventHandler CanAddItemChanged;

        void AddItem(IGalleryContext context);
        UIElement CreateCustomizationControl();
        IEnumerable<KeyValuePair<string, string>> GetReplacementList(GalleryExtensionArgs args);
        IEnumerable<string> GetUsings(GalleryExtensionArgs args);
        IEnumerable<string> GetXmlns(GalleryExtensionArgs args);
        void Initialize(GalleryExtensionArgs args, string lang);
        string InitializeParam(GalleryExtensionArgs args, string key, string value);
        void InitOptionsPanel(IOptionsPanel panelOptions);
        void ProjectItemAdded(object projectItem);

        int? OptionsAreaWidth { get; }

        bool CanAddItem { get; }
    }
}

