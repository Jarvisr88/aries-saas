namespace DevExpress.XtraReports.Native
{
    using DevExpress.XtraReports.Parameters;
    using System;

    public class ParameterPath
    {
        private readonly DevExpress.XtraReports.Parameters.Parameter parameter;
        private readonly string path;

        public ParameterPath(DevExpress.XtraReports.Parameters.Parameter parameter, string path)
        {
            this.parameter = parameter;
            this.path = path;
        }

        public DevExpress.XtraReports.Parameters.Parameter Parameter =>
            this.parameter;

        public string Path =>
            this.path;
    }
}

