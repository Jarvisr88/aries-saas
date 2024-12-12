namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class ParametersExtensions
    {
        public static ReportParameter GetReportParameterStub(this ParameterModel model)
        {
            ReportParameter parameter1 = new ReportParameter();
            parameter1.Name = model.Name;
            parameter1.Path = model.Path;
            object obj1 = model.Value;
            ReportParameter parameter2 = parameter1;
            object obj2 = obj1;
            if (obj1 == null)
            {
                object local1 = obj1;
                obj2 = model.Parameter.Value;
            }
            parameter2.Value = obj2;
            return parameter2;
        }

        public static ReportParameter[] GetReportParameterStubs(this ParametersModel model)
        {
            List<ReportParameter> parameterStubs = new List<ReportParameter>();
            model.Parameters.ForEach<ParameterModel>(delegate (ParameterModel x) {
                parameterStubs.Add(x.GetReportParameterStub());
            });
            return parameterStubs.ToArray();
        }
    }
}

