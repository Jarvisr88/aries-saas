namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Xml;

    public interface ILayoutControl : ILayoutGroup, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, ILayoutGroupModel, ILiveCustomizationAreasProvider, ILayoutControlModel
    {
        void ControlAdded(FrameworkElement control);
        void ControlRemoved(FrameworkElement control);
        void ControlVisibilityChanged(FrameworkElement control);
        LayoutGroup CreateGroup();
        void DeleteAvailableItem(FrameworkElement item);
        FrameworkElement FindControl(string id);
        string GetID(FrameworkElement control);
        Style GetPartStyle(LayoutGroupPartStyle style);
        void InitCustomizationController();
        void InitNewElement(FrameworkElement element);
        bool MakeControlVisible(FrameworkElement control);
        void ModelChanged(LayoutControlModelChangedEventArgs args);
        void ReadElementFromXML(XmlReader xml, FrameworkElement element);
        void SetID(FrameworkElement control);
        void SetID(FrameworkElement control, string id);
        void TabClicked(ILayoutGroup group, FrameworkElement selectedTabChild);
        void WriteElementToXML(XmlWriter xml, FrameworkElement element);
        void WriteToXML(XmlWriter xml);

        bool AllowItemSizing { get; }

        FrameworkElements AvailableItems { get; }

        FrameworkElements VisibleAvailableItems { get; }
    }
}

