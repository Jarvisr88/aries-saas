namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class ParameterExtensions
    {
        [IteratorStateMachine(typeof(<ActualParameters>d__2))]
        public static IEnumerable<IParameter> ActualParameters(this IParameter parameter)
        {
            if ((parameter is IRangeRootParameter) && ((IRangeRootParameter) parameter).IsRange)
            {
                yield return ((IRangeRootParameter) parameter).StartParameter;
                yield return ((IRangeRootParameter) parameter).EndParameter;
            }
            yield return parameter;
        }

        [IteratorStateMachine(typeof(<AllParameters>d__1))]
        public static IEnumerable<IParameter> AllParameters(this IParameter parameter)
        {
            yield return parameter;
            if ((parameter is IRangeRootParameter) && ((IRangeRootParameter) parameter).IsRange)
            {
                yield return ((IRangeRootParameter) parameter).StartParameter;
                yield return ((IRangeRootParameter) parameter).EndParameter;
            }
        }

        public static bool HasCascadeLookUpSettings(this Parameter parameter) => 
            (parameter.LookUpSettings != null) && !string.IsNullOrEmpty(parameter.LookUpSettings.FilterString);


    }
}

