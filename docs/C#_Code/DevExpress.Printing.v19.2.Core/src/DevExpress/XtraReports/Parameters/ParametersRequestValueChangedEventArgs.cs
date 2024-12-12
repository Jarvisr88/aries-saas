namespace DevExpress.XtraReports.Parameters
{
    using System;
    using System.Collections.Generic;

    public class ParametersRequestValueChangedEventArgs : ParametersRequestEventArgs
    {
        private ParameterInfo changedParameterInfo;

        internal ParametersRequestValueChangedEventArgs(IList<ParameterInfo> parametersInfo, ParameterInfo changedParameterInfo) : base(parametersInfo)
        {
            this.changedParameterInfo = changedParameterInfo;
        }

        public ParameterInfo ChangedParameterInfo =>
            this.changedParameterInfo;
    }
}

