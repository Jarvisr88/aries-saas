namespace DevExpress.XtraReports.Parameters.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Browsing;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class LookUpValuesProvider : ILookUpValuesProvider
    {
        private readonly IEnumerable<Parameter> parameters;
        private readonly DataContext dataContext;

        public LookUpValuesProvider(IEnumerable<Parameter> parameters, DataContext dataContext)
        {
            this.parameters = parameters;
            this.dataContext = dataContext;
        }

        private LookUpValueCollection GetLookUps(Parameter parameter, Dictionary<IParameter, object> actualParameterValues, DataContext dataContext)
        {
            EditorValuesProviderSimple parameterValueProvider = new EditorValuesProviderSimple(actualParameterValues);
            LookUpValueCollection lookUps = LookUpHelper.GetLookUpValues(parameter.LookUpSettings, dataContext, parameterValueProvider);
            if (lookUps != null)
            {
                actualParameterValues[parameter] = parameter.MultiValue ? LookUpHelper.GetUpdatedMultiValueParameterValue(actualParameterValues[parameter], lookUps) : LookUpHelper.GetUpdatedSingleValueParameterValue(actualParameterValues[parameter], lookUps);
            }
            return lookUps;
        }

        public Task<IEnumerable<ParameterLookUpValuesContainer>> GetLookUpValues(Parameter changedParameter, IParameterEditorValueProvider editorValueProvider) => 
            Task.Factory.StartNew<IEnumerable<ParameterLookUpValuesContainer>>(delegate {
                Func<Parameter, bool> <>9__3;
                Func<Parameter, object> <>9__2;
                Func<Parameter, IParameter> keySelector = <>c.<>9__3_1;
                if (<>c.<>9__3_1 == null)
                {
                    Func<Parameter, IParameter> local1 = <>c.<>9__3_1;
                    keySelector = <>c.<>9__3_1 = x => x;
                }
                Func<Parameter, object> elementSelector = <>9__2;
                if (<>9__2 == null)
                {
                    Func<Parameter, object> local2 = <>9__2;
                    elementSelector = <>9__2 = x => editorValueProvider.GetValue(x);
                }
                Dictionary<IParameter, object> actualParameterValues = this.parameters.ToDictionary<Parameter, IParameter, object>(keySelector, elementSelector);
                Func<Parameter, bool> predicate = <>9__3;
                if (<>9__3 == null)
                {
                    Func<Parameter, bool> local3 = <>9__3;
                    predicate = <>9__3 = x => !ReferenceEquals(x, changedParameter);
                }
                var selector = <>c.<>9__3_4;
                if (<>c.<>9__3_4 == null)
                {
                    var local4 = <>c.<>9__3_4;
                    selector = <>c.<>9__3_4 = x => new { 
                        parameter = x,
                        majorParameters = CascadingParametersService.GetMajorParameters(x)
                    };
                }
                var func8 = <>c.<>9__3_5;
                if (<>c.<>9__3_5 == null)
                {
                    var local5 = <>c.<>9__3_5;
                    func8 = <>c.<>9__3_5 = x => x.majorParameters.Any<Parameter>();
                }
                var source = this.parameters.Reverse<Parameter>().TakeWhile<Parameter>(predicate).Reverse<Parameter>().Select(selector).Where(func8);
                var func9 = <>c.<>9__3_6;
                if (<>c.<>9__3_6 == null)
                {
                    var local6 = <>c.<>9__3_6;
                    func9 = <>c.<>9__3_6 = x => x.majorParameters;
                }
                if (!source.SelectMany(func9).Contains<Parameter>(changedParameter))
                {
                    return Enumerable.Empty<ParameterLookUpValuesContainer>();
                }
                List<ParameterLookUpValuesContainer> list = new List<ParameterLookUpValuesContainer>();
                foreach (var type in source)
                {
                    LookUpValueCollection lookUpValues = this.GetLookUps(type.parameter, actualParameterValues, this.dataContext);
                    list.Add(new ParameterLookUpValuesContainer(type.parameter, lookUpValues));
                }
                return list;
            });

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LookUpValuesProvider.<>c <>9 = new LookUpValuesProvider.<>c();
            public static Func<Parameter, IParameter> <>9__3_1;
            public static Func<Parameter, <>f__AnonymousType1<Parameter, IEnumerable<Parameter>>> <>9__3_4;
            public static Func<<>f__AnonymousType1<Parameter, IEnumerable<Parameter>>, bool> <>9__3_5;
            public static Func<<>f__AnonymousType1<Parameter, IEnumerable<Parameter>>, IEnumerable<Parameter>> <>9__3_6;

            internal IParameter <GetLookUpValues>b__3_1(Parameter x) => 
                x;

            internal <>f__AnonymousType1<Parameter, IEnumerable<Parameter>> <GetLookUpValues>b__3_4(Parameter x) => 
                new { 
                    parameter = x,
                    majorParameters = CascadingParametersService.GetMajorParameters(x)
                };

            internal bool <GetLookUpValues>b__3_5(<>f__AnonymousType1<Parameter, IEnumerable<Parameter>> x) => 
                x.majorParameters.Any<Parameter>();

            internal IEnumerable<Parameter> <GetLookUpValues>b__3_6(<>f__AnonymousType1<Parameter, IEnumerable<Parameter>> x) => 
                x.majorParameters;
        }
    }
}

