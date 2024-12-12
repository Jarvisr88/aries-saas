namespace DevExpress.DXTemplateGallery.Extensions
{
    using System;

    public interface IGalleryExtededFieldSupports
    {
        string GetSecondaryFieldTextOnPrimaryFieldChanged(string text);

        string DefaultPrimaryItemName { get; }

        string DefaultSecondaryItemName { get; }

        string PrimaryFieldLabel { get; }

        string SecondaryFieldLabel { get; }
    }
}

