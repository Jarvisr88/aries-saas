namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;

    internal class DisplayCriteriaColumnTraits
    {
        public readonly object HeaderCaption;
        public readonly DevExpress.Xpf.Grid.ColumnFilterMode ColumnFilterMode;
        public readonly BaseEditSettings ActualEditSettings;
        public readonly string RoundDateDisplayFormat;
        public readonly Func<object, string> GetDisplayText;

        public DisplayCriteriaColumnTraits(object headerCaption, DevExpress.Xpf.Grid.ColumnFilterMode columnFilterMode, BaseEditSettings actualEditSettings, string roundDateDisplayFormat, Func<object, string> getDisplayText)
        {
            this.HeaderCaption = headerCaption;
            this.ColumnFilterMode = columnFilterMode;
            this.ActualEditSettings = actualEditSettings;
            this.RoundDateDisplayFormat = roundDateDisplayFormat;
            this.GetDisplayText = getDisplayText;
        }
    }
}

