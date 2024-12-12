namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GenerateEditSettingsWrapper
    {
        public static Type DefaultEditSettingsType = typeof(TextEditSettings);

        public GenerateEditSettingsWrapper()
        {
            this.Properties = new Dictionary<DependencyProperty, object>();
        }

        public Type EditSettingsType { get; set; }

        public Dictionary<DependencyProperty, object> Properties { get; private set; }
    }
}

