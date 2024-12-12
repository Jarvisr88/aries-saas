namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public interface ITreeSelectableItem
    {
        ITreeSelectableItem Parent { get; }

        List<ITreeSelectableItem> Children { get; }

        bool AllowSelect { get; }

        string Text { get; }
    }
}

