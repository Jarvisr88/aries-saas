namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ILocalizableUIElement<TUIElementID> where TUIElementID: struct
    {
        TUIElementID GetID();

        string Name { get; }

        string Description { get; }
    }
}

