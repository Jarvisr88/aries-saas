namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Windows.Controls;

    public sealed class PropertySelectorBandModel : PropertySelectorModelBase
    {
        private PropertySelectorBandModel(object content, DataTemplateSelector captionTemplateSelector, int id, int parentID) : base(content, captionTemplateSelector, null, id, parentID)
        {
        }

        internal static PropertySelectorBandModel CreateSelfReferenceModel(object content, DataTemplateSelector captionSelector, int id, int parentID) => 
            new PropertySelectorBandModel(content, captionSelector, id, parentID);
    }
}

