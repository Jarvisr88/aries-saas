namespace DevExpress.XtraReports.Parameters
{
    using System;
    using System.Collections.Generic;

    public class ParametersRequestEventArgs : EventArgs
    {
        private IList<ParameterInfo> parametersInfo;

        internal ParametersRequestEventArgs(IList<ParameterInfo> parametersInfo)
        {
            this.parametersInfo = parametersInfo;
        }

        public ParameterInfo[] ParametersInformation
        {
            get
            {
                if (this.parametersInfo == null)
                {
                    return null;
                }
                ParameterInfo[] array = new ParameterInfo[this.parametersInfo.Count];
                this.parametersInfo.CopyTo(array, 0);
                return array;
            }
        }
    }
}

