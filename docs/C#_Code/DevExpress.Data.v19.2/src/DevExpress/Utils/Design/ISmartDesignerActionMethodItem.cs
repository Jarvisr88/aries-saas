namespace DevExpress.Utils.Design
{
    using System;

    public interface ISmartDesignerActionMethodItem
    {
        string MemberName { get; }

        string DisplayName { get; }

        int SortOrder { get; }

        Type TargetType { get; }

        SmartTagActionType FinalAction { get; }
    }
}

