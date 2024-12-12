namespace DevExpress.Data.Controls.DataAccess
{
    using DevExpress.Data.Localization;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.All)]
    public sealed class LocalizableCategoryAttribute : CategoryAttribute
    {
        public LocalizableCategoryAttribute(CommonStringId id);
        public LocalizableCategoryAttribute(string value);
        protected override string GetLocalizedString(string val);

        public string Value { get; }

        public CommonStringId Id { get; }
    }
}

