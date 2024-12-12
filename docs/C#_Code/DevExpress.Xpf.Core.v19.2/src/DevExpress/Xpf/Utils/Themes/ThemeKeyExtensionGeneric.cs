namespace DevExpress.Xpf.Utils.Themes
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Interop;

    public abstract class ThemeKeyExtensionGeneric : ResourceKey
    {
        protected static readonly object defaultResourceCore = new object();
        private int hash;
        private object resourceKeyCore = defaultResourceCore;
        private string themeName;

        static ThemeKeyExtensionGeneric()
        {
            ClearAutomationEventsHelper.ClearAutomationEvents();
            if (BrowserInteropHelper.IsBrowserHosted && !OptionsXBAP.SuppressNotSupportedException)
            {
                throw new XbapNotSupportedException("Starting from v2011 vol 2, XBAP applications are not supported. Instead, we recommend that you use ClickOnce deployment (the most preferable way) or migrate your application to Silverlight. For more information, please refer to http://go.devexpress.com/SupportXBAP.aspx\r\n\r\nIf you still want to continue using DevExpress controls in XBAP applications, set the OptionsXBAP.SuppressNotSupportedException property to True.");
            }
        }

        protected ThemeKeyExtensionGeneric()
        {
        }

        private bool CompareWithAlias(string themeName0, string themeName1) => 
            string.IsNullOrEmpty(themeName0) && (string.IsNullOrEmpty(themeName1) || (themeName1 == "DeepBlue"));

        protected virtual bool Equals(ThemeKeyExtensionGeneric other) => 
            this.IsSameTheme(other);

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((ThemeKeyExtensionGeneric) obj) : false) : true) : false;

        protected virtual int GenerateHashCode() => 
            0;

        protected virtual System.Reflection.Assembly GetAssembly()
        {
            Theme theme = Theme.FindTheme(this.ThemeName);
            return theme?.Assembly;
        }

        public override int GetHashCode() => 
            this.hash;

        protected virtual bool IsSameTheme(ThemeKeyExtensionGeneric other) => 
            !Equals(this.ThemeName, other.ThemeName) ? (this.CompareWithAlias(this.ThemeName, other.ThemeName) || this.CompareWithAlias(other.ThemeName, this.ThemeName)) : true;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(this.ThemeName))
            {
                string themeName = ThemeNameHelper.GetThemeName(serviceProvider);
                if (!string.IsNullOrEmpty(themeName))
                {
                    this.ThemeName = themeName;
                }
            }
            return this;
        }

        public virtual string ResourceKeyToString() => 
            this.resourceKeyCore.ToString();

        protected virtual void SetHashCode()
        {
            this.hash = this.GenerateHashCode();
        }

        private void SetThemeName(string value)
        {
            this.themeName = value;
            this.SetHashCode();
        }

        private bool ShouldSerializeThemeName() => 
            !string.IsNullOrEmpty(this.ThemeName);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type TypeInTargetAssembly { get; set; }

        public string ThemeName
        {
            get => 
                this.themeName;
            set => 
                this.SetThemeName(value);
        }

        protected object ResourceKeyCore
        {
            get => 
                this.resourceKeyCore;
            set => 
                this.resourceKeyCore = value;
        }

        public override System.Reflection.Assembly Assembly =>
            (this.TypeInTargetAssembly == null) ? (string.IsNullOrEmpty(this.ThemeName) ? base.GetType().Assembly : this.GetAssembly()) : this.TypeInTargetAssembly.Assembly;
    }
}

