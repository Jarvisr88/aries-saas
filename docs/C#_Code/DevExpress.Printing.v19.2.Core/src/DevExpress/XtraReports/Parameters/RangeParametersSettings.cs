namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraReports.Parameters.RangeParametersSettings"), TypeConverter(typeof(RangeParametersSettingsTypeConverter))]
    public class RangeParametersSettings : ValueSourceSettings
    {
        private RangeStartParameter startParameter;
        private RangeEndParameter endParameter;

        public RangeParametersSettings() : this(new RangeStartParameter(), new RangeEndParameter())
        {
        }

        public RangeParametersSettings(RangeStartParameter startParameter, RangeEndParameter endParameter)
        {
            Guard.ArgumentNotNull(startParameter, "startParameter");
            Guard.ArgumentNotNull(endParameter, "endParameter");
            this.StartParameter = startParameter;
            this.EndParameter = endParameter;
        }

        internal static RangeParametersSettings Create(Parameter parameter) => 
            new RangeParametersSettings { 
                StartParameter = { Name = parameter.Name + "_Start" },
                EndParameter = { Name = parameter.Name + "_End" }
            };

        internal static bool IsSupportedType(Type parameterType) => 
            parameterType == typeof(DateTime);

        protected internal override void SyncParameterType(Type type)
        {
            this.StartParameter.Type = type;
            this.EndParameter.Type = type;
        }

        [Description("A range parameter's nested start parameter."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraReports.Parameters.RangeParametersSettings.StartParameter"), XtraSerializableProperty(XtraSerializationVisibility.Reference)]
        public RangeStartParameter StartParameter
        {
            get => 
                this.startParameter;
            private set
            {
                this.startParameter = value;
                this.startParameter.OwnerValueSource = this;
            }
        }

        [Description("A range parameter's nested end parameter."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraReports.Parameters.RangeParametersSettings.EndParameter"), XtraSerializableProperty(XtraSerializationVisibility.Reference)]
        public RangeEndParameter EndParameter
        {
            get => 
                this.endParameter;
            private set
            {
                this.endParameter = value;
                this.endParameter.OwnerValueSource = this;
            }
        }
    }
}

