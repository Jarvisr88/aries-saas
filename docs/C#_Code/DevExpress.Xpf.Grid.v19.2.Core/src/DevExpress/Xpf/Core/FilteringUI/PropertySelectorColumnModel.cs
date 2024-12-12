namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public sealed class PropertySelectorColumnModel : PropertySelectorModelBase
    {
        private PropertySelectorColumnModel(string name, object content, DataTemplateSelector captionTemplateSelector, DataTemplateSelector selectedCaptionTemplateCaptionTemplateSelector, int id, int parentID) : base(content, captionTemplateSelector, selectedCaptionTemplateCaptionTemplateSelector, id, parentID)
        {
            this.<Name>k__BackingField = name;
        }

        internal static PropertySelectorColumnModel CreateSelfReferenceModel(string name, object content, DataTemplateSelector captionSelector, DataTemplateSelector selectedCaptionSelector, int id, int parentID) => 
            new PropertySelectorColumnModel(name, content, captionSelector, selectedCaptionSelector, id, parentID);

        internal static PropertySelectorColumnModel CreateStandAloneModel(string name) => 
            new PropertySelectorColumnModel(name, name, null, null, -100, -100);

        internal string Name { get; }
    }
}

