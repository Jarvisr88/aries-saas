namespace DevExpress.Xpf.Utils.Themes
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DefaultStyleThemeKeyExtension : ThemeKeyExtensionInternalBase<string>
    {
        private string fullName;
        private string traceString;

        protected override bool Equals(ThemeKeyExtensionGeneric other)
        {
            if (!base.Equals(other))
            {
                return false;
            }
            DefaultStyleThemeKeyExtension extension = (DefaultStyleThemeKeyExtension) other;
            return ((this.FullName == extension.FullName) && Equals(this.Assembly, extension.Assembly));
        }

        protected override int GenerateHashCode() => 
            HashCodeHelper.CalculateGeneric<int, string>(base.GenerateHashCode(), this.FullName);

        private bool IsInitialized(IServiceProvider serviceProvider)
        {
            IProvideValueTarget service = (IProvideValueTarget) serviceProvider.GetService(typeof(IProvideValueTarget));
            return ((service != null) && (service.TargetObject != null));
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!this.IsInitialized(serviceProvider))
            {
                return base.ProvideValue(serviceProvider);
            }
            this.RegisterThemeType();
            return this;
        }

        private void RegisterThemeType()
        {
            this.SetHashCode();
            if (this.IsValid)
            {
                ThemedElementsDictionary.RegisterThemeType(base.ThemeName, this.FullName, this);
            }
        }

        private void SetFullName(string value)
        {
            this.fullName = value;
            this.SetHashCode();
        }

        public override string ToString() => 
            this.traceString ?? base.ToString();

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string FullName
        {
            get => 
                this.fullName;
            set => 
                this.SetFullName(value);
        }

        public System.Type Type
        {
            set => 
                this.SetFullName(value?.FullName);
        }

        protected internal string AssemblyName { get; set; }

        protected virtual bool IsValid =>
            !string.IsNullOrEmpty(this.FullName) && (base.ThemeName != null);

        public override System.Reflection.Assembly Assembly
        {
            get
            {
                if (this.AssemblyName != null)
                {
                    System.Reflection.Assembly loadedAssembly = AssemblyHelper.GetLoadedAssembly(this.AssemblyName);
                    if (loadedAssembly != null)
                    {
                        return loadedAssembly;
                    }
                }
                return base.Assembly;
            }
        }
    }
}

