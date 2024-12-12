namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class BarItemLinkActionBase : BarManagerControllerActionBase
    {
        public static readonly DependencyProperty ItemLinkIndexProperty;
        public static readonly DependencyProperty TargetProperty;
        public static readonly DependencyProperty TargetTypeProperty;

        static BarItemLinkActionBase();
        public static int GetItemLinkIndex(DependencyObject d);
        public static ILinksHolder GetLinksHolder(DependencyObject context, string name, IActionContainer container, ItemLinksHolderType targetType);
        public override object GetObjectCore();
        protected virtual ILinksHolder GetTarget(string name);
        public static string GetTarget(DependencyObject d);
        public static ItemLinksHolderType GetTargetType(DependencyObject d);
        public override bool IsEqual(BarManagerControllerActionBase action);
        private static void OnTargetTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args);
        public static void SetItemLinkIndex(DependencyObject d, int value);
        public static void SetTarget(DependencyObject d, string value);
        public static void SetTargetType(DependencyObject d, ItemLinksHolderType value);

        [Description("Gets or sets the index of the bar item link in the object's collection of bar item links. This is an attached property.")]
        public virtual int ItemLinkIndex { get; set; }

        [Description("Gets or sets the name of the target link container (a Bar, PopupMenu or BarLinkContainerItem object) for a bar item link.The Target property is in effect when the BarItemLinkActionBase.TargetType property is set to Other (default).The Target property is an attached property, which is in effect for BarItem and BarItemLink descendants when they act as actions (when they are added to the BarManagerActionContainer.Actions collection).")]
        public string Target { get; set; }

        [Description("Gets or sets the type of the target link container (a Bar, PopupMenu or BarLinkContainerItem object) for a bar item link.The BarItemLinkActionBase.Target property is not in effect when the TargetType property is set to MainMenu or StatusBar.The TargetType property is an attached property, which is in effect for BarItem and BarItemLink descendants when they act as actions (when they are added to the BarManagerActionContainer.Actions collection).")]
        public ItemLinksHolderType TargetType { get; set; }
    }
}

