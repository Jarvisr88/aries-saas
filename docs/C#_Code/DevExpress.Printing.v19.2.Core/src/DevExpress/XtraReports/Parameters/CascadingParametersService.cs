namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CascadingParametersService
    {
        public static IEnumerable<Parameter> GetDependentParameters(Parameter analyzedParameter)
        {
            ParameterCollection owner = analyzedParameter.Owner;
            if (owner == null)
            {
                throw new InvalidOperationException("Parameter is not in the ParameterCollection.");
            }
            return (from p in owner.Cast<Parameter>()
                where !ReferenceEquals(p, analyzedParameter) && IsDependentParameter(analyzedParameter.Name, p)
                select p);
        }

        public static IEnumerable<Parameter> GetMajorParameters(Parameter analyzedParameter)
        {
            ParameterCollection owner = analyzedParameter.Owner;
            if (owner == null)
            {
                throw new InvalidOperationException("Parameter is not in the ParameterCollection.");
            }
            List<Parameter> list = new List<Parameter>();
            if ((analyzedParameter.LookUpSettings != null) && !string.IsNullOrEmpty(analyzedParameter.LookUpSettings.FilterString))
            {
                OperandValue[] valueArray;
                Func<Parameter, bool> <>9__0;
                CriteriaOperator @operator = CriteriaOperator.Parse(analyzedParameter.LookUpSettings.FilterString, out valueArray);
                Func<Parameter, bool> predicate = <>9__0;
                if (<>9__0 == null)
                {
                    Func<Parameter, bool> local1 = <>9__0;
                    predicate = <>9__0 = x => !ReferenceEquals(x, analyzedParameter);
                }
                foreach (Parameter parameter in owner.Cast<Parameter>().TakeWhile<Parameter>(predicate))
                {
                    if (valueArray.OfType<OperandParameter>().Any<OperandParameter>(x => x.ParameterName == parameter.Name))
                    {
                        list.Add(parameter);
                    }
                }
            }
            return list;
        }

        public static bool IsDependentParameter(string parentParameterName, Parameter analyzedParameter)
        {
            OperandValue[] valueArray;
            return ((analyzedParameter.LookUpSettings != null) ? ((CriteriaOperator.Parse(analyzedParameter.LookUpSettings.FilterString, out valueArray) != null) && (valueArray.FirstOrDefault<OperandValue>(x => ((x is OperandParameter) && (((OperandParameter) x).ParameterName == parentParameterName))) != null)) : false);
        }

        public static bool ValidateFilterString(LookUpSettings settings, out string error)
        {
            OperandValue[] valueArray;
            bool flag;
            if (settings.Parameter.Owner == null)
            {
                error = "Parameter is not in the ParameterCollection.";
                return false;
            }
            try
            {
                CriteriaOperator @operator = CriteriaOperator.Parse(settings.FilterString, out valueArray);
            }
            catch (Exception exception)
            {
                error = exception.Message;
                return false;
            }
            using (IEnumerator<OperandParameter> enumerator = valueArray.OfType<OperandParameter>().GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        OperandParameter operandParameter = enumerator.Current;
                        Parameter item = settings.Parameter.Owner.FirstOrDefault<Parameter>(x => x.Name == operandParameter.ParameterName);
                        if (item == null)
                        {
                            error = $"It is impossible to find a parameter which determines the filter string of Cascading Parameter. FilterString: {settings.FilterString}";
                            flag = false;
                        }
                        else
                        {
                            if (settings.Parameter.Owner.IndexOf(item) < settings.Parameter.Owner.IndexOf(settings.Parameter))
                            {
                                continue;
                            }
                            error = $"Cascading parameter can be only bound to parameters that are higher on the list of ParameterCollection. FilterString: {settings.FilterString}";
                            flag = false;
                        }
                    }
                    else
                    {
                        error = null;
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        public static bool ValidateFilterStrings(IEnumerable<Parameter> parameters, out string error)
        {
            bool flag;
            Func<Parameter, bool> predicate = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<Parameter, bool> local1 = <>c.<>9__4_0;
                predicate = <>c.<>9__4_0 = x => x.HasCascadeLookUpSettings();
            }
            using (IEnumerator<Parameter> enumerator = parameters.Where<Parameter>(predicate).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Parameter current = enumerator.Current;
                        if (ValidateFilterString(current.LookUpSettings, out error))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        error = null;
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CascadingParametersService.<>c <>9 = new CascadingParametersService.<>c();
            public static Func<Parameter, bool> <>9__4_0;

            internal bool <ValidateFilterStrings>b__4_0(Parameter x) => 
                x.HasCascadeLookUpSettings();
        }
    }
}

